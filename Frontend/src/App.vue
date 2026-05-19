<script setup>
import { ref } from 'vue';
import Navbar from './components/Navbar.vue';
import HeroSection from './components/HeroSection.vue';
import Login from './components/Login.vue'; // Faltaba esta importación
import Register from './components/Register.vue'; // Faltaba esta importación

const currentView = ref('home'); // 'home', 'login', o 'register'

// Función para cambiar la vista
const setView = (v) => {
  currentView.value = v;
};
</script>

<template>
  <!-- Escuchamos el evento 'change-view' que vendrá del Navbar -->
  <Navbar @change-view="setView" />
  
  <div v-if="currentView === 'home'">
    <HeroSection />
    <!-- Aquí puedes agregar más secciones del home luego -->
  </div>
  
  <!-- Pasamos la función setView para que Login y Register puedan alternar entre ellos -->
  <Login v-if="currentView === 'login'" @switch="setView" />
  <Register v-if="currentView === 'register'" @switch="setView" />
</template>