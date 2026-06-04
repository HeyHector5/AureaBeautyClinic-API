<template>
  <section class="relative w-full h-[650px] overflow-hidden bg-black">
    <!-- Contenedor de Imágenes con Animación -->
    <div 
      v-for="(img, index) in images" 
      :key="index"
      class="absolute inset-0 transition-opacity duration-[2000ms] ease-in-out"
      :class="currentSlide === index ? 'opacity-100 z-10' : 'opacity-0 z-0'"
    >
      <img 
        :src="img" 
        class="w-full h-full object-cover transform transition-transform duration-[6000ms] ease-linear"
        :class="currentSlide === index ? 'scale-110' : 'scale-100'"
        alt="Tratamiento Estético"
      />
      <!-- Overlay para legibilidad -->
      <div class="absolute inset-0 bg-black/40"></div>
    </div>

    <!-- Contenido Central -->
    <div class="relative z-20 flex flex-col items-center justify-center h-full text-center text-white px-4">
      <h1 class="text-4xl md:text-6xl font-serif mb-4 drop-shadow-2xl animate-fade-in-up">
        ¡Atrévete a verte y a sentirte bien!
      </h1>
      <p class="text-lg md:text-xl mb-8 font-light tracking-widest animate-fade-in-up delay-200">
        Echa un vistazo dentro de nuestro mundo maravilloso
      </p>
      
    </div>

    <!-- Indicadores de posición -->
    <div class="absolute bottom-8 left-1/2 -translate-x-1/2 flex space-x-4 z-30">
      <button 
        v-for="(_, index) in images" 
        :key="index"
        @click="currentSlide = index"
        class="w-3 h-3 rounded-full transition-all duration-300 border border-white"
        :class="currentSlide === index ? 'bg-white scale-125' : 'bg-transparent hover:bg-white/50'"
      ></button>
    </div>
  </section>
</template>

<script setup>
import { ref, onMounted } from 'vue';

// Importación de imágenes desde assets
import img1 from '../assets/cara.jpg';
import img2 from '../assets/exfoliante.jpg';
import img3 from '../assets/laser.jpg';

const images = [img1, img2, img3];
const currentSlide = ref(0);

onMounted(() => {
  setInterval(() => {
    currentSlide.value = (currentSlide.value + 1) % images.length;
  }, 5000);
});
</script>

<style scoped>
.animate-fade-in-up {
  animation: fadeInUp 1s ease-out forwards;
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.delay-200 { animation-delay: 0.2s; }
.delay-500 { animation-delay: 0.5s; }
</style>