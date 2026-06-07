<template>
  <nav class="flex items-center justify-between px-8 py-4 bg-white shadow-sm relative">
    <div class="flex items-center">
      <span class="text-2xl font-bold tracking-tighter cursor-pointer" @click="router.push('/')">
        <span class="text-[#FF3B30]">AUREA</span>
        <span class="text-gray-400 font-light ml-1 uppercase">Beauty Clinic</span>
      </span>
    </div>

    <div class="hidden md:flex items-center space-x-10 text-sm font-bold tracking-widest text-[#FF3B30] uppercase">
      <a @click.prevent="router.push('/')" href="#" class="hover:opacity-70 transition-opacity cursor-pointer">Inicio</a>
      <a @click.prevent="router.push('/servicios')" href="#" class="hover:opacity-70 transition-opacity cursor-pointer">Servicios</a>
      <a @click.prevent="router.push('/nosotros')" href="#" class="hover:opacity-70 transition-opacity cursor-pointer">Nosotros</a>
      <a @click.prevent="router.push('/contactos')" href="#" class="hover:opacity-70 transition-opacity cursor-pointer">Contactos</a>
    </div>

    <div class="relative">
      <button
        @click="isDropdownOpen = !isDropdownOpen"
        class="flex items-center text-gray-800 hover:text-[#FF3B30] transition-colors focus:outline-none cursor-pointer"
      >
        <svg xmlns="http://www.w3.org/2000/svg" class="h-8 w-8" fill="currentColor" viewBox="0 0 24 24">
          <path d="M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z"/>
        </svg>
      </button>

      <div
        v-if="isDropdownOpen"
        class="absolute right-0 mt-3 w-56 bg-white border border-gray-100 rounded-lg shadow-xl z-50 py-2 origin-top-right"
      >
        <!-- ESTADO: NO AUTENTICADO -->
        <template v-if="!isLoggedIn">
          <div class="px-4 py-2 text-xs text-gray-400 uppercase font-bold">Bienvenido</div>
          <a href="#"
             @click.prevent="router.push('/login'); isDropdownOpen = false"
             class="block px-4 py-2 text-sm text-gray-700 hover:bg-red-50 cursor-pointer">
            Iniciar Sesión
          </a>
          <a href="#"
             @click.prevent="router.push('/register'); isDropdownOpen = false"
             class="block px-4 py-2 text-sm text-gray-700 hover:bg-red-50 cursor-pointer">
            Registrarse
          </a>
        </template>

        <!-- ESTADO: AUTENTICADO -->
        <template v-else>
          <div class="px-4 py-2 border-b border-gray-50">
            <p class="text-sm font-bold text-gray-800">{{ user.name }} {{ user.lastName }}</p>
            <p class="text-xs text-gray-500">{{ user.role }}</p>
          </div>
          <a href="#" class="block px-4 py-2 mt-1 text-sm text-gray-700 hover:bg-red-50 hover:text-[#FF3B30] cursor-pointer">Mis Reservas</a>
          <a href="#" class="block px-4 py-2 text-sm text-gray-700 hover:bg-red-50 hover:text-[#FF3B30] cursor-pointer">Historial</a>
          <a href="#" class="block px-4 py-2 text-sm text-gray-700 hover:bg-red-50 hover:text-[#FF3B30] cursor-pointer">Mi Perfil</a>
          <hr class="my-2 border-gray-100">
          <button
            @click="logout"
            class="w-full text-left px-4 py-2 text-sm text-red-600 font-bold hover:bg-red-50 cursor-pointer"
          >
            Cerrar Sesión
          </button>
        </template>
      </div>
    </div>
  </nav>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'

const router = useRouter()
const route = useRoute()
const isDropdownOpen = ref(false)

// Convertimos las variables en estados reactivos reales
const isLoggedIn = ref(false)
const user = ref({})

// Función encargada de leer el localStorage de manera manual
const updateAuthStatus = () => {
  const token = localStorage.getItem('token')
  const u = localStorage.getItem('user')
  
  isLoggedIn.value = !!token
  user.value = u ? JSON.parse(u) : {}
}

// Evaluamos el estado cuando el componente se monta en pantalla
onMounted(() => {
  updateAuthStatus()
})

// VIGILANTE: Cada vez que cambie la ruta actual, se reevalúan las credenciales en el navegador.
// Esto detecta instantáneamente cuando vienes desde el Login tras usar SweetAlert2.
watch(() => route.path, () => {
  updateAuthStatus()
})

const logout = () => {
  localStorage.removeItem('token')
  localStorage.removeItem('user')
  
  // Forzamos la actualización inmediata del estado local para limpiar la interfaz
  isLoggedIn.value = false
  user.value = {}
  isDropdownOpen.value = false
  
  router.push('/')
}
</script>