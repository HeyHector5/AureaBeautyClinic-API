<template>
  <div>

    <!-- SERVICIOS DESTACADOS -->
    <section class="bg-white py-24 px-8">
      <div class="max-w-6xl mx-auto">
        <div class="text-center mb-16">
          <span class="block text-[#FF3B30] text-xs font-bold tracking-[0.2em] mb-4">NUESTROS SERVICIOS</span>
          <h2 class="text-4xl font-serif font-light tracking-tight text-gray-800">
            Tratamientos para tu bienestar
          </h2>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          <div
            v-for="service in services"
            :key="service.name"
            class="group border border-gray-100 rounded-xl p-8 hover:border-[#FF3B30] hover:shadow-lg transition-all duration-300 cursor-pointer"
            @click="router.push('/servicios')"
          >
            <div class="w-20 h-20 rounded-full overflow-hidden mb-6 border-2 border-gray-100 group-hover:border-[#FF3B30] transition-all duration-300">
              <img :src="service.img" :alt="service.name" class="w-full h-full object-cover" />
            </div>
            <h3 class="text-lg font-bold text-gray-800 mb-3">{{ service.name }}</h3>
            <p class="text-sm text-gray-500 leading-relaxed mb-6">{{ service.desc }}</p>
            <span class="text-[#FF3B30] text-sm font-bold tracking-wide group-hover:underline">
              Ver más →
            </span>
          </div>
        </div>
      </div>
    </section>

    <!-- CÓMO FUNCIONA -->
    <section class="bg-black py-24 px-8">
      <div class="max-w-6xl mx-auto">
        <div class="text-center mb-16">
          <span class="block text-[#FF3B30] text-xs font-bold tracking-[0.2em] mb-4">EL PROCESO</span>
          <h2 class="text-4xl font-serif font-light tracking-tight text-white">
            Así de simple es comenzar
          </h2>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-3 gap-12">
          <div
            v-for="(step, i) in steps"
            :key="step.title"
            class="relative flex flex-col items-center text-center"
          >
            <div
              v-if="i < steps.length - 1"
              class="hidden md:block absolute top-8 left-1/2 w-full h-px bg-gray-800 z-0"
            ></div>
            <div class="relative z-10 w-16 h-16 rounded-full border border-[#FF3B30] flex items-center justify-center mb-6 bg-black">
              <span class="text-[#FF3B30] text-xl font-bold font-serif">{{ i + 1 }}</span>
            </div>
            <h4 class="text-white font-bold text-sm mb-3 tracking-wide uppercase">{{ step.title }}</h4>
            <p class="text-gray-500 text-sm leading-relaxed">{{ step.desc }}</p>
          </div>
        </div>
      </div>
    </section>

    <!-- CTA -->
    <section class="bg-[#FF3B30] py-24 px-8">
      <div class="max-w-3xl mx-auto text-center">
        <h2 class="text-4xl font-serif font-light text-white mb-6 tracking-tight">
          ¿Lista para transformarte?
        </h2>
        <p class="text-white/80 text-base leading-relaxed mb-10">
          Agenda tu cita hoy y da el primer paso hacia la mejor versión de ti.
        </p>
        <div class="flex flex-col sm:flex-row gap-4 justify-center">
          <button
            @click="router.push('/register')"
            class="bg-white text-[#FF3B30] font-bold py-4 px-10 rounded-lg hover:bg-gray-100 transition-colors cursor-pointer text-sm tracking-wide"
          >
            Crear cuenta gratis
          </button>
          <button
            @click="router.push('/nosotros')"
            class="border border-white text-white font-bold py-4 px-10 rounded-lg hover:bg-white/10 transition-colors cursor-pointer text-sm tracking-wide"
          >
            Conocer más
          </button>
        </div>
      </div>
    </section>

  </div>
</template>

<script setup>
import { useRouter } from 'vue-router'
import cara from '@/assets/cara.jpg'
import trataCorp from '@/assets/trataCorp.jpg'
import dermatologia from '@/assets/dermartologia.jpg'
import capilar from '@/assets/capilar.jpg'   

const router = useRouter()

const services = [
  {
    name: 'Medicina Facial',
    desc: 'Botox, rellenos dérmicos e hidratación profunda para un rostro rejuvenecido.',
    img: cara
  },
  {
    name: 'Tratamientos Corporales',
    desc: 'Reducción de medidas, reafirmación y sculpting con tecnología láser.',
    img: trataCorp
  },
  {
    name: 'Dermatología',
    desc: 'Diagnóstico y tratamiento de condiciones cutáneas con enfoque médico.',
    img: dermatologia
  },
  {
    name: 'Cuidado Capilar',
    desc: 'Plasma rico en plaquetas y mesoterapia capilar contra la alopecia.',
    img: capilar
  },
]

const steps = [
  {
    title: 'Agenda tu cita',
    desc: 'Regístrate y selecciona el tratamiento y horario que prefieras.'
  },
  {
    title: 'Consulta con el especialista',
    desc: 'Nuestro equipo evalúa tu caso y diseña el plan ideal para ti.'
  },
  {
    title: 'Disfruta los resultados',
    desc: 'Vive la transformación con el respaldo de nuestros expertos.'
  },
]
</script>