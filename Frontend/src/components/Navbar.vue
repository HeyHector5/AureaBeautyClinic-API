<script setup>
import { onBeforeUnmount, onMounted, ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useAuth } from '@/composables/useAuth'
import { CLINIC } from '@/constants/clinic'

const router = useRouter()
const route = useRoute()
const { isLoggedIn, isAdmin, user, logout } = useAuth()

const isDropdownOpen = ref(false)
const isMobileMenuOpen = ref(false)
const dropdownRef = ref(null)

const NAV = [
  { to: '/', label: 'Inicio' },
  { to: '/servicios', label: 'Servicios' },
  { to: '/nosotros', label: 'Nosotros' },
  { to: '/contactos', label: 'Contactos' }
]

const ACCOUNT_LINKS = [
  { to: '/mis-reservas', label: 'Mis Reservas' },
  { to: '/historial', label: 'Historial' },
  { to: '/perfil', label: 'Mi Perfil' }
]

const closeMenus = () => {
  isDropdownOpen.value = false
  isMobileMenuOpen.value = false
}

const go = (path) => {
  closeMenus()
  router.push(path)
}

const handleLogout = () => {
  logout()
  closeMenus()
  router.push('/')
}

// Cierra el menú de usuario al hacer clic fuera o pulsar Escape.
const onClickOutside = (event) => {
  if (dropdownRef.value && !dropdownRef.value.contains(event.target)) {
    isDropdownOpen.value = false
  }
}
const onKeydown = (event) => {
  if (event.key === 'Escape') closeMenus()
}

onMounted(() => {
  document.addEventListener('click', onClickOutside)
  document.addEventListener('keydown', onKeydown)
})
onBeforeUnmount(() => {
  document.removeEventListener('click', onClickOutside)
  document.removeEventListener('keydown', onKeydown)
})

watch(() => route.path, closeMenus)
</script>

<template>
  <nav class="bg-white shadow-sm relative z-30">
    <div class="flex items-center justify-between px-6 md:px-8 py-4">
      <RouterLink to="/" class="text-2xl font-bold tracking-tighter shrink-0">
        <span class="text-aurea">{{ CLINIC.shortName }}</span>
        <span class="text-gray-400 font-light ml-1 uppercase">{{ CLINIC.tagline }}</span>
      </RouterLink>

      <div
        class="hidden md:flex items-center space-x-10 text-sm font-bold tracking-widest uppercase"
      >
        <RouterLink
          v-for="item in NAV"
          :key="item.to"
          :to="item.to"
          class="transition-opacity hover:opacity-70"
          :class="route.path === item.to ? 'text-aurea underline underline-offset-8' : 'text-aurea'"
        >
          {{ item.label }}
        </RouterLink>
      </div>

      <div class="flex items-center gap-2">
        <div ref="dropdownRef" class="relative">
          <button
            type="button"
            class="flex items-center text-gray-800 hover:text-aurea transition-colors focus:outline-none cursor-pointer"
            aria-label="Menú de usuario"
            :aria-expanded="isDropdownOpen"
            @click="isDropdownOpen = !isDropdownOpen"
          >
            <svg xmlns="http://www.w3.org/2000/svg" class="h-8 w-8" fill="currentColor" viewBox="0 0 24 24" aria-hidden="true">
              <path d="M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z" />
            </svg>
          </button>

          <div
            v-if="isDropdownOpen"
            class="absolute right-0 mt-3 w-56 bg-white border border-gray-100 rounded-lg shadow-xl z-50 py-2 origin-top-right"
          >
            <!-- ESTADO: NO AUTENTICADO -->
            <template v-if="!isLoggedIn">
              <div class="px-4 py-2 text-xs text-gray-400 uppercase font-bold">Bienvenido</div>
              <button
                type="button"
                class="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-aurea-tint hover:text-aurea cursor-pointer"
                @click="go('/login')"
              >
                Iniciar Sesión
              </button>
              <button
                type="button"
                class="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-aurea-tint hover:text-aurea cursor-pointer"
                @click="go('/register')"
              >
                Registrarse
              </button>
            </template>

            <!-- ESTADO: AUTENTICADO -->
            <template v-else>
              <div class="px-4 py-2 border-b border-gray-50">
                <p class="text-sm font-bold text-gray-800">{{ user?.name }} {{ user?.lastName }}</p>
                <p class="text-xs text-gray-500">{{ user?.role }}</p>
              </div>

              <button
                v-if="isAdmin"
                type="button"
                class="block w-full text-left px-4 py-2 mt-1 text-sm font-bold text-aurea hover:bg-aurea-tint cursor-pointer"
                @click="go('/admin')"
              >
                Panel de administración
              </button>

              <button
                type="button"
                class="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-aurea-tint hover:text-aurea cursor-pointer"
                :class="{ 'mt-1': !isAdmin }"
                @click="go('/dashboard')"
              >
                Mi cuenta
              </button>

              <button
                v-for="link in ACCOUNT_LINKS"
                :key="link.to"
                type="button"
                class="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-aurea-tint hover:text-aurea cursor-pointer"
                @click="go(link.to)"
              >
                {{ link.label }}
              </button>

              <hr class="my-2 border-gray-100" />
              <button
                type="button"
                class="w-full text-left px-4 py-2 text-sm text-red-600 font-bold hover:bg-red-50 cursor-pointer"
                @click="handleLogout"
              >
                Cerrar Sesión
              </button>
            </template>
          </div>
        </div>

        <button
          type="button"
          class="md:hidden text-gray-800 hover:text-aurea transition-colors cursor-pointer"
          aria-label="Abrir menú de navegación"
          :aria-expanded="isMobileMenuOpen"
          @click="isMobileMenuOpen = !isMobileMenuOpen"
        >
          <svg class="h-6 w-6" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24" aria-hidden="true">
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              :d="isMobileMenuOpen ? 'M6 18L18 6M6 6l12 12' : 'M4 6h16M4 12h16M4 18h16'"
            />
          </svg>
        </button>
      </div>
    </div>

    <!-- Navegación móvil: sin esto no había ninguna forma de navegar en pantallas pequeñas. -->
    <div v-if="isMobileMenuOpen" class="md:hidden border-t border-gray-100 px-6 py-4 space-y-1">
      <RouterLink
        v-for="item in NAV"
        :key="item.to"
        :to="item.to"
        class="block py-2 text-sm font-bold tracking-widest uppercase transition-colors"
        :class="route.path === item.to ? 'text-gray-800' : 'text-aurea'"
      >
        {{ item.label }}
      </RouterLink>
    </div>
  </nav>
</template>
