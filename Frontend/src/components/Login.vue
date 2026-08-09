<script setup>
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import BaseButton from '@/components/ui/BaseButton.vue'
import { login } from '@/services/authService'
import { useAuth } from '@/composables/useAuth'
import { homeForRole } from '@/router/index.js'
import { notifyApiError, notifySuccess } from '@/utils/notify'

const router = useRouter()
const route = useRoute()
const { setSession } = useAuth()

const email = ref('')
const password = ref('')
const showPassword = ref(false)
const loading = ref(false)

const handleLogin = async () => {
  loading.value = true
  try {
    const auth = await login(email.value, password.value)
    setSession(auth)

    await notifySuccess('¡Bienvenido de vuelta!', 'Sesión iniciada correctamente.')

    // Si el guard nos mandó aquí desde una ruta protegida, volvemos a ella.
    const redirect = route.query.redirect
    router.push(redirect || homeForRole(auth.user?.role?.name ?? auth.user?.role))
  } catch (e) {
    notifyApiError(e, 'Error de autenticación')
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <div class="flex items-center justify-center min-h-[80vh] bg-gray-50 px-4 py-16">
    <div class="w-full max-w-md bg-white rounded-2xl shadow-xl p-8">
      <div class="text-center mb-8">
        <h1 class="text-3xl font-serif font-light text-gray-800">Bienvenido</h1>
        <p class="text-gray-500 mt-2">Ingresa a tu cuenta de Aurea Beauty</p>
      </div>

      <form class="space-y-6" @submit.prevent="handleLogin">
        <div>
          <label for="login-email" class="block text-sm font-medium text-gray-700 mb-1">
            Correo Electrónico
          </label>
          <input
            id="login-email"
            v-model="email"
            type="email"
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-aurea focus:border-transparent outline-none transition-all"
            placeholder="ejemplo@correo.com"
            autocomplete="email"
            required
          />
        </div>

        <div>
          <label for="login-password" class="block text-sm font-medium text-gray-700 mb-1">
            Contraseña
          </label>
          <div class="relative">
            <input
              id="login-password"
              v-model="password"
              :type="showPassword ? 'text' : 'password'"
              class="w-full px-4 py-3 pr-12 rounded-lg border border-gray-300 focus:ring-2 focus:ring-aurea focus:border-transparent outline-none transition-all"
              placeholder="••••••••"
              autocomplete="current-password"
              required
            />
            <button
              type="button"
              class="absolute right-3 top-1/2 -translate-y-1/2 text-xs font-bold uppercase tracking-widest text-gray-400 hover:text-aurea transition-colors cursor-pointer"
              :aria-label="showPassword ? 'Ocultar contraseña' : 'Mostrar contraseña'"
              @click="showPassword = !showPassword"
            >
              {{ showPassword ? 'Ocultar' : 'Ver' }}
            </button>
          </div>
        </div>

        <BaseButton type="submit" size="lg" :loading="loading">
          {{ loading ? 'Cargando...' : 'Iniciar Sesión' }}
        </BaseButton>
      </form>

      <p class="text-center mt-6 text-sm text-gray-600">
        ¿No tienes una cuenta?
        <RouterLink to="/register" class="text-aurea font-bold hover:underline">
          Regístrate aquí
        </RouterLink>
      </p>
    </div>
  </div>
</template>
