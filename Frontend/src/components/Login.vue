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
          class="w-full bg-[#FF3B30] hover:bg-[#e0342a] text-white font-bold py-3 rounded-lg transition-colors cursor-pointer shadow-lg"
        >
          Iniciar Sesión
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
import { ref } from 'vue';

const email = ref('');
const password = ref('');

const handleLogin = () => {
  console.log("Login intentado con:", email.value);
  //Backkend
};
</script>