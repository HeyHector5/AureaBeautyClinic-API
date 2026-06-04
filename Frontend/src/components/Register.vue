<script setup>
import { ref } from 'vue';


defineEmits(['switch']);

const name = ref('');
const email = ref('');
const password = ref('');
const confirmPassword = ref('');
const errorMessage = ref('');

const handleRegister = () => {
  if (password.value !== confirmPassword.value) {
    errorMessage.value = "Las contraseñas no coinciden.";
    return;
  }
  errorMessage.value = "";
  console.log("Registrando usuario...", name.value);
};
</script>

<template>
  <!-- Este contenedor se centrará debajo del Navbar que está en App.vue -->
  <div class="flex items-center justify-center min-h-[80vh] bg-gray-50 px-4 py-12">
    <div class="w-full max-w-md bg-white rounded-2xl shadow-xl p-8 border border-gray-100">
      <div class="text-center mb-8">
        <h2 class="text-3xl font-bold text-gray-800 tracking-tighter">Crea tu Cuenta</h2>
        <p class="text-gray-500 mt-2 text-sm">Únete a la experiencia de Aurea Beauty</p>
      </div>

      <form @submit.prevent="handleRegister" class="space-y-4">
        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1">Nombre Completo</label>
          <input type="text" v-model="name" class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all" placeholder="Juan Perez" required />
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1">Correo Electrónico</label>
          <input type="email" v-model="email" class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all" placeholder="ejemplo@correo.com" required />
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1">Contraseña</label>
          <input type="password" v-model="password" class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all" placeholder="••••••••" required />
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1">Confirmar Contraseña</label>
          <input type="password" v-model="confirmPassword" class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-[#FF3B30] outline-none transition-all" placeholder="••••••••" required />
        </div>

        <p v-if="errorMessage" class="text-red-500 text-xs font-bold animate-pulse">{{ errorMessage }}</p>

        <button type="submit" class="w-full bg-[#FF3B30] hover:bg-[#e0342a] text-white font-bold py-3 rounded-lg transition-all transform hover:scale-[1.02] cursor-pointer shadow-lg active:scale-95">
          Registrarse ahora
        </button>
      </form>

      <p class="text-center mt-8 text-sm text-gray-600">
        ¿Ya tienes una cuenta? 
        <button @click="$emit('switch', 'login')" class="text-[#FF3B30] font-bold hover:underline cursor-pointer ml-1">
          Inicia sesión aquí
        </button>
      </p>
    </div>
  </div>
</template>