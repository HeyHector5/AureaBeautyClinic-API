<template>
  <nav class="flex items-center justify-between px-8 py-4 bg-white shadow-sm relative">
    <div class="flex items-center">
      <span class="text-2xl font-bold tracking-tighter">
        <span class="text-[#FF3B30]">AUREA</span>
        <span class="text-gray-400 font-light ml-1 uppercase">Beauty Clinic</span>
      </span>
    </div>

    <!--Links -->
    <div class="hidden md:flex items-center space-x-10 text-sm font-bold tracking-widest text-[#FF3B30] uppercase">
      <a 
        href="#" 
        @click.prevent="$emit('change-view', 'home')" 
        class="hover:opacity-70 transition-opacity cursor-pointer"
      >
        Inicio
      </a>
      
      <a href="#" class="hover:opacity-70 transition-opacity cursor-pointer">Servicios</a>
      <a href="#" class="hover:opacity-70 transition-opacity cursor-pointer">Nosotros</a>
      <a href="#" class="hover:opacity-70 transition-opacity cursor-pointer">Contactos</a>
    </div>

    <!-- Lado Derecho: Lógica de Usuario -->
    <div class="relative">
      <button 
        @click="isDropdownOpen = !isDropdownOpen"
        class="flex items-center text-gray-800 hover:text-[#FF3B30] transition-colors focus:outline-none cursor-pointer"
      >
        <svg xmlns="http://www.w3.org/2000/svg" class="h-8 w-8" fill="currentColor" viewBox="0 0 24 24">
          <path d="M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z"/>
        </svg>
      </button>

      <!-- Menú Desplegable Dinámico -->
      <div 
        v-if="isDropdownOpen" 
        class="absolute right-0 mt-3 w-56 bg-white border border-gray-100 rounded-lg shadow-xl z-50 py-2 origin-top-right"
      >
        <!-- ESTADO: NO LOGUEADO -->
        <template v-if="!isLoggedIn">
          <div class="px-4 py-2 text-xs text-gray-400 uppercase font-bold">Bienvenido</div>
          <a href="#" 
             @click.prevent="$emit('change-view', 'login'); isDropdownOpen = false" 
             class="block px-4 py-2 text-sm text-gray-700 hover:bg-red-50 cursor-pointer">
            Iniciar Sesión
          </a>
          <a href="#" 
             @click.prevent="$emit('change-view', 'register'); isDropdownOpen = false" 
             class="block px-4 py-2 text-sm text-gray-700 hover:bg-red-50 cursor-pointer">
            Registrarse
          </a>
        </template>

        <!-- ESTADO: LOGUEADO -->
        <template v-else>
          <div class="px-4 py-2 border-b border-gray-50">
            <p class="text-sm font-bold text-gray-800">Jael Jimenez</p>
            <p class="text-xs text-gray-500">Cliente VIP</p>
          </div>
          <a href="#" class="block px-4 py-2 mt-1 text-sm text-gray-700 hover:bg-red-50 hover:text-[#FF3B30] cursor-pointer">
            Mis Reservas
          </a>
          <a href="#" class="block px-4 py-2 text-sm text-gray-700 hover:bg-red-50 hover:text-[#FF3B30] cursor-pointer">
            Historial
          </a>
          <a href="#" class="block px-4 py-2 text-sm text-gray-700 hover:bg-red-50 hover:text-[#FF3B30] cursor-pointer">
            Mi Perfil
          </a>
          <hr class="my-2 border-gray-100">
          <button 
            @click="logoutSimulado" 
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
import { ref } from 'vue';

//Eventos
defineEmits(['change-view']);

const isDropdownOpen = ref(false);
const isLoggedIn = ref(false);

const loginSimulado = () => {
  isLoggedIn.value = true;
  isDropdownOpen.value = false;
};

const logoutSimulado = () => {
  isLoggedIn.value = false;
  isDropdownOpen.value = false;
};
</script>