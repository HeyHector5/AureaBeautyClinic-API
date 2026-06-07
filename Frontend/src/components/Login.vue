<template>
  <div class="flex items-center justify-center min-h-[80vh] bg-gray-50 px-4">
    <div class="w-full max-w-md bg-white rounded-2xl shadow-xl p-8">
      <div class="text-center mb-8">
        <h2 class="text-3xl font-bold text-gray-800">Bienvenido</h2>
        <p class="text-gray-500 mt-2">Ingresa a tu cuenta de Aurea Beauty</p>
      </div>

      <form @submit.prevent="handleLogin" class="space-y-6">
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Correo Electrónico</label>
          <input 
            type="email" 
            v-model="email"
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] focus:border-transparent outline-none transition-all"
            placeholder="ejemplo@correo.com"
            required
          />
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Contraseña</label>
          <input 
            type="password" 
            v-model="password"
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] focus:border-transparent outline-none transition-all"
            placeholder="••••••••"
            required
          />
        </div>

        <button 
          type="submit"
          :disabled="loading"
          class="w-full bg-[#FF3B30] hover:bg-[#e0342a] text-white font-bold py-3 rounded-lg transition-colors cursor-pointer shadow-lg disabled:opacity-50"
        >
          {{ loading ? 'Cargando...' : 'Iniciar Sesión' }}
        </button>
      </form>

      <p class="text-center mt-6 text-sm text-gray-600">
        ¿No tienes una cuenta? 
        <a href="#" @click.prevent="$emit('switch', 'register')" class="text-[#FF3B30] font-bold hover:underline cursor-pointer">Regístrate aquí</a>
      </p>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { login } from '../Services/authService'
import { useRouter } from 'vue-router'
import Swal from 'sweetalert2' // 1. Importamos SweetAlert2

const email    = ref('')
const password = ref('')
const loading  = ref(false)
const router   = useRouter()

// Quitamos la variable reactiva 'error' porque ahora manejaremos los mensajes con SweetAlert2

const handleLogin = async () => {
  console.log('handleLogin ejecutado', email.value, password.value)
  loading.value = true

  try {
    const res = await login(email.value, password.value)
    console.log('Respuesta API:', res)

    if (!res.success) {
      // 2. Alerta elegante si las credenciales fallan o la API devuelve éxito falso
      Swal.fire({
        icon: 'error',
        title: 'Error de Autenticación',
        text: res.message || 'Credenciales incorrectas, verifica tus datos.',
        confirmButtonColor: '#FF3B30', // El rojo coral de tu marca
      })
      return
    }

    // Guarda el token y el usuario en LocalStorage
    localStorage.setItem('token', res.data.token)

    const user = res.data.user
      ? {
          ...res.data.user,
          role: res.data.user.role?.name ?? res.data.user.role ?? 'User'
        }
      : { email: email.value, role: 'User' }

    localStorage.setItem('user', JSON.stringify(user))

    // 3. Ventana de éxito estilizada antes de redirigir al Dashboard / Admin
    Swal.fire({
      icon: 'success',
      title: '¡Bienvenido de vuelta!',
      text: 'Sesión iniciada correctamente.',
      showConfirmButton: false,
      timer: 2000, // Se cierra sola en 2 segundos
      timerProgressBar: true,
    }).then(() => {
      // Redirige justo después de que la alerta se cierre
      if (user.role === 'Admin') router.push('/admin')
      else router.push('/dashboard')
    })

  } catch (e) {
    console.log('Error:', e)
    
    // 4. Alerta si hay un desplome en el servidor o caída de internet
    Swal.fire({
      icon: 'error',
      title: 'Error de conexión',
      text: 'No pudimos conectar con el servidor. Por favor, intenta más tarde.',
      confirmButtonColor: '#FF3B30',
    })
  } finally {
    loading.value = false
  }
}
</script>