<script setup>
import { ref } from 'vue';
import Navbar from './components/Navbar.vue';
import HeroSection from './components/HeroSection.vue';
import Login from './components/Login.vue';
import Register from './components/Register.vue';
import Footer from './components/Footer.vue'; 

const currentView = ref('home'); 

// Función para cambiar la vista
const setView = (v) => {
  currentView.value = v;
};
</script>

<template>
  <div class="min-h-screen flex flex-col justify-between bg-gray-50">
    
    <Navbar @change-view="setView" />
    
    <main class="flex-grow">
      <div v-if="currentView === 'home'">
        <HeroSection />
      </div>
      <Login v-if="currentView === 'login'" @switch="setView" />
      <Register v-if="currentView === 'register'" @switch="setView" />
    </main>
    
    <Footer @change-view="setView" /> 
    
  </div>
</template>