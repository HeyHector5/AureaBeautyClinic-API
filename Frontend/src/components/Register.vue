<script setup>
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'
import Swal from 'sweetalert2'
import BaseButton from '@/components/ui/BaseButton.vue'
import { register } from '@/services/authService'
import { useAuth } from '@/composables/useAuth'
import { BRAND_RED } from '@/constants/clinic'
import { notifyApiError, notifyWarning } from '@/utils/notify'

const router = useRouter()
const { setSession } = useAuth()

const name = ref('')
const lastName = ref('')
const phone = ref('')
const email = ref('')
const password = ref('')
const confirmPassword = ref('')
const loading = ref(false)

/** El backend exige entre 8 y 100 caracteres (RegisterRequest). */
const MIN_PASSWORD = 8
const passwordTooShort = computed(
  () => password.value.length > 0 && password.value.length < MIN_PASSWORD
)
const passwordsMismatch = computed(
  () => confirmPassword.value.length > 0 && password.value !== confirmPassword.value
)

const handleRegister = async () => {
  if (password.value.length < MIN_PASSWORD) {
    notifyWarning(
      'Contraseña demasiado corta',
      `Usa al menos ${MIN_PASSWORD} caracteres para proteger tu cuenta.`
    )
    return
  }

  if (password.value !== confirmPassword.value) {
    notifyWarning(
      'Las contraseñas no coinciden',
      'Asegúrate de escribir la misma contraseña en ambos campos.'
    )
    return
  }

  loading.value = true
  try {
    const auth = await register(
      name.value,
      lastName.value,
      email.value,
      password.value,
      phone.value
    )

    // El registro ya devuelve un token: iniciamos sesión directamente.
    setSession(auth)

    await Swal.fire({
      icon: 'success',
      title: '¡Cuenta creada con éxito!',
      text: 'Tu perfil en Aurea Beauty Clinic quedó listo. Reserva tu primera cita cuando quieras.',
      confirmButtonText: 'Ir a mi cuenta',
      confirmButtonColor: BRAND_RED,
      allowOutsideClick: false
    })

    router.push('/dashboard')
  } catch (e) {
    notifyApiError(e, 'No se pudo crear la cuenta')
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <div class="flex items-center justify-center min-h-[80vh] bg-gray-50 px-4 py-12">
    <div class="w-full max-w-md bg-white rounded-2xl shadow-xl p-8 border border-gray-100">
      <div class="text-center mb-8">
        <h1 class="text-3xl font-serif font-light text-gray-800">Crea tu cuenta</h1>
        <p class="text-gray-500 mt-2 text-sm">Únete a la experiencia de Aurea Beauty</p>
      </div>

      <form class="space-y-4" @submit.prevent="handleRegister">
        <div class="grid grid-cols-2 gap-4">
          <div>
            <label for="reg-name" class="block text-sm font-semibold text-gray-700 mb-1">Nombre</label>
            <input
              id="reg-name"
              v-model="name"
              type="text"
              class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-aurea focus:border-transparent outline-none transition-all"
              placeholder="Juan"
              autocomplete="given-name"
              required
            />
          </div>
          <div>
            <label for="reg-lastname" class="block text-sm font-semibold text-gray-700 mb-1">Apellido</label>
            <input
              id="reg-lastname"
              v-model="lastName"
              type="text"
              class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-aurea focus:border-transparent outline-none transition-all"
              placeholder="Pérez"
              autocomplete="family-name"
              required
            />
          </div>
        </div>

        <div>
          <label for="reg-phone" class="block text-sm font-semibold text-gray-700 mb-1">Teléfono</label>
          <input
            id="reg-phone"
            v-model="phone"
            type="tel"
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-aurea focus:border-transparent outline-none transition-all"
            placeholder="809-555-0000"
            autocomplete="tel"
            required
          />
        </div>

        <div>
          <label for="reg-email" class="block text-sm font-semibold text-gray-700 mb-1">
            Correo electrónico
          </label>
          <input
            id="reg-email"
            v-model="email"
            type="email"
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-aurea focus:border-transparent outline-none transition-all"
            placeholder="ejemplo@correo.com"
            autocomplete="email"
            required
          />
        </div>

        <div>
          <label for="reg-password" class="block text-sm font-semibold text-gray-700 mb-1">
            Contraseña
          </label>
          <input
            id="reg-password"
            v-model="password"
            type="password"
            :minlength="MIN_PASSWORD"
            class="w-full px-4 py-3 rounded-lg border focus:ring-2 focus:ring-aurea focus:border-transparent outline-none transition-all"
            :class="passwordTooShort ? 'border-red-400' : 'border-gray-300'"
            placeholder="••••••••"
            autocomplete="new-password"
            required
          />
          <p class="mt-1 text-xs" :class="passwordTooShort ? 'text-red-600' : 'text-gray-400'">
            Mínimo {{ MIN_PASSWORD }} caracteres.
          </p>
        </div>

        <div>
          <label for="reg-confirm" class="block text-sm font-semibold text-gray-700 mb-1">
            Confirmar contraseña
          </label>
          <input
            id="reg-confirm"
            v-model="confirmPassword"
            type="password"
            class="w-full px-4 py-3 rounded-lg border focus:ring-2 focus:ring-aurea focus:border-transparent outline-none transition-all"
            :class="passwordsMismatch ? 'border-red-400' : 'border-gray-300'"
            placeholder="••••••••"
            autocomplete="new-password"
            required
          />
          <p v-if="passwordsMismatch" class="mt-1 text-xs text-red-600">
            Las contraseñas no coinciden.
          </p>
        </div>

        <BaseButton type="submit" size="lg" :loading="loading" class="mt-2">
          {{ loading ? 'Registrando...' : 'Registrarse ahora' }}
        </BaseButton>
      </form>

      <p class="text-center mt-8 text-sm text-gray-600">
        ¿Ya tienes una cuenta?
        <RouterLink to="/login" class="text-aurea font-bold hover:underline ml-1">
          Inicia sesión aquí
        </RouterLink>
      </p>
    </div>
  </div>
</template>
