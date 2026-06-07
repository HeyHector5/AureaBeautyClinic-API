<script setup>
import { ref } from 'vue'
import { register } from '@/Services/authService'
import { useRouter } from 'vue-router'
import Swal from 'sweetalert2' // 1. Importamos SweetAlert2

const router = useRouter()

const name = ref('')
const lastName = ref('')
const phone = ref('')
const email = ref('')
const password = ref('')
const confirmPassword = ref('')
const loading = ref(false)

// Quitamos la variable reactiva 'error' ya que SweetAlert2 manejará todos los flujos de avisos

const handleRegister = async () => {
  // Validación local de contraseñas con modal de advertencia tipo Warning
  if (password.value !== confirmPassword.value) {
    Swal.fire({
      icon: 'warning',
      title: 'Contraseñas no coinciden',
      text: 'Por favor, asegúrate de escribir la misma contraseña en ambos campos.',
      confirmButtonColor: '#FF3B30',
    })
    return
  }

  loading.value = true

  try {
    const res = await register(name.value, lastName.value, email.value, password.value, phone.value)

    if (!res.success) {
      // 2. Alerta si el correo ya existe o falla alguna validación del servidor
      Swal.fire({
        icon: 'error',
        title: 'No se pudo crear la cuenta',
        text: res.message || 'Hubo un inconveniente al procesar tus datos.',
        confirmButtonColor: '#FF3B30',
      })
      return
    }

    // 3. ¡ALERTA DE ÉXITO PREMIUM! 
    Swal.fire({
      icon: 'success',
      title: '¡Cuenta Creada con Éxito!',
      text: 'Tu perfil en Aurea Beauty Clinic ha sido registrado. Ya puedes iniciar sesión.',
      confirmButtonText: 'Ir al Login 🚀',
      confirmButtonColor: '#FF3B30',
      allowOutsideClick: false, // Evita que cierren el modal haciendo clic fuera
    }).then((result) => {
      if (result.isConfirmed) {
        // Redirección fluida mediante el router al Login
        router.push('/login')
      }
    })

  } catch (e) {
    console.error('Error:', e)
    // 4. Alerta de protección ante fallas de red o caídas del backend
    Swal.fire({
      icon: 'error',
      title: 'Error de conexión',
      text: 'No pudimos conectar con el servidor. Por favor, intenta de nuevo más tarde.',
      confirmButtonColor: '#FF3B30',
    })
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <div class="flex items-center justify-center min-h-[80vh] bg-gray-50 px-4 py-12">
    <div class="w-full max-w-md bg-white rounded-2xl shadow-xl p-8 border border-gray-100">
      <div class="text-center mb-8">
        <h2 class="text-3xl font-bold text-gray-800 tracking-tighter">Crea tu Cuenta</h2>
        <p class="text-gray-500 mt-2 text-sm">Únete a la experiencia de Aurea Beauty</p>
      </div>

      <form @submit.prevent="handleRegister" class="space-y-4">

        <!-- Nombre y Apellido en fila -->
        <div class="grid grid-cols-2 gap-4">
          <div>
            <label class="block text-sm font-semibold text-gray-700 mb-1">Nombre</label>
            <input type="text" v-model="name"
              class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all"
              placeholder="Juan" required />
          </div>
          <div>
            <label class="block text-sm font-semibold text-gray-700 mb-1">Apellido</label>
            <input type="text" v-model="lastName"
              class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all"
              placeholder="Pérez" required />
          </div>
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1">Teléfono</label>
          <input type="tel" v-model="phone"
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all"
            placeholder="809-555-0000" required />
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1">Correo Electrónico</label>
          <input type="email" v-model="email"
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all"
            placeholder="ejemplo@correo.com" required />
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1">Contraseña</label>
          <input type="password" v-model="password"
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all"
            placeholder="••••••••" required />
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1">Confirmar Contraseña</label>
          <input type="password" v-model="confirmPassword"
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all"
            placeholder="••••••••" required />
        </div>

        <!-- Removido el elemento <p v-if="error"> para evitar rupturas estéticas en el formulario -->

        <button
          type="submit"
          :disabled="loading"
          class="w-full bg-[#FF3B30] hover:bg-[#e0342a] text-white font-bold py-3 rounded-lg transition-all transform hover:scale-[1.02] cursor-pointer shadow-lg active:scale-95 disabled:opacity-50"
        >
          {{ loading ? 'Registrando...' : 'Registrarse ahora' }}
        </button>
      </form>

      <p class="text-center mt-8 text-sm text-gray-600">
        ¿Ya tienes una cuenta?
        <button @click="router.push('/login')" class="text-[#FF3B30] font-bold hover:underline cursor-pointer ml-1">
          Inicia sesión aquí
        </button>
      </p>
    </div>
  </div>
</template>