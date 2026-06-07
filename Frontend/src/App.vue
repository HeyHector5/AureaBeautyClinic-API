<script setup>
import Navbar from './components/Navbar.vue'
import Footer from './components/Footer.vue'
</script>

<template>
  <div class="min-h-screen flex flex-col justify-between bg-gray-50">
    <Navbar />
    <main class="flex-grow">
      <RouterView />
    </main>
    <Footer />
  </div>
</template>