<template>
  <section id="nosotros">

    <div class="bg-black text-white px-16 pt-24 pb-0">
      <div class="grid grid-cols-1 md:grid-cols-2 gap-16 items-center max-w-7xl mx-auto pb-16">
        
        <div class="max-w-2xl">
          <span class="block text-[#FF3B30] text-xs font-bold tracking-[0.2em] mb-4">NUESTRA HISTORIA</span>
          <h1 class="text-5xl font-light leading-tight tracking-tight mb-6">
            Donde la ciencia<br>encuentra la belleza
          </h1>
          <p class="text-gray-400 text-base leading-relaxed font-sans max-w-lg">
            Desde 2015, Aurea Beauty Clinic ha redefinido los estándares de la medicina estética
            en República Dominicana, combinando tecnología de vanguardia con el arte del cuidado personal.
          </p>
        </div>

        <div class="w-full h-[350px] overflow-hidden rounded-sm shadow-2xl border border-gray-900">
          <img 
            :src="sillaImg" 
            alt="Silla de tratamiento Aurea Clinic" 
            class="w-full h-full object-cover grayscale hover:grayscale-0 transition-all duration-700 ease-in-out"
          />
        </div>

      </div>

      <div class="flex border-t border-gray-800 max-w-7xl mx-auto">
        <div v-for="stat in stats" :key="stat.label" class="flex-1 py-8 border-r border-gray-800 last:border-r-0">
          <span class="block text-4xl font-light text-[#FF3B30] tracking-tight">{{ stat.number }}</span>
          <span class="block font-sans text-xs text-gray-500 tracking-widest mt-1">{{ stat.label }}</span>
        </div>
      </div>
    </div>

    <div class="bg-white px-16 py-24 grid grid-cols-1 md:grid-cols-2 gap-20 items-center max-w-7xl mx-auto">
      <div>
        <span class="block text-[#FF3B30] text-xs font-bold tracking-[0.2em] mb-4">QUIÉNES SOMOS</span>
        <h2 class="text-3xl font-light leading-snug tracking-tight mb-6">
          Una clínica nacida de la pasión por el bienestar
        </h2>
        <p class="font-sans text-sm text-gray-500 leading-relaxed mb-4">
          Aurea Beauty Clinic fue founded con una visión clara: ofrecer tratamientos estéticos de élite
          accesibles para todos, sin sacrificar calidad ni seguridad. Nuestro nombre, inspirado en el latín
          <em class="italic">aurea</em> (dorado), refleja el estándar de excelencia que perseguimos en cada procedimiento.
        </p>
        <p class="font-sans text-sm text-gray-500 leading-relaxed">
          Comenzamos como un pequeño consultorio con tres especialistas y hoy somos un centro de referencia
          con más de 20 profesionales certificados, equipos de última generación y miles de pacientes
          que confían en nosotros año tras año.
        </p>
      </div>

      <div class="bg-[#FF3B30] text-white p-10 rounded-sm shadow-xl">
        <div class="text-7xl leading-none opacity-30 font-serif mb-4">"</div>
        <p class="text-lg italic leading-relaxed mb-8">
          La belleza no es un lujo, es un acto de amor propio que merece los mejores cuidados.
        </p>
        <div class="flex items-center gap-4">
          <div class="w-11 h-11 rounded-full bg-white/20 flex items-center justify-center text-xs font-bold font-sans shrink-0">
            DM
          </div>
          <div>
            <strong class="block font-sans text-sm font-bold">Dra. Daniela Marte</strong>
            <span class="font-sans text-xs opacity-75">Fundadora &amp; Directora Médica</span>
          </div>
        </div>
      </div>
    </div>

    <div class="bg-gray-50 px-16 py-24">
      <div class="grid grid-cols-1 md:grid-cols-3 gap-8 max-w-7xl mx-auto">
        <div
          v-for="item in mvv"
          :key="item.title"
          class="bg-white border border-gray-200 p-10 rounded-sm hover:border-[#FF3B30] hover:-translate-y-1 transition-all duration-200"
        >
          <div class="w-8 h-8 mb-6">
            <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path :d="item.iconPath" stroke="#FF3B30" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
          </div>
          <h3 class="text-xl font-light mb-3 tracking-tight">{{ item.title }}</h3>
          <p class="font-sans text-sm text-gray-500 leading-relaxed">{{ item.text }}</p>
        </div>
      </div>
    </div>

    <div class="bg-gray-50 px-16 py-24 border-t border-gray-200">
      <div class="max-w-7xl mx-auto">
        <span class="block text-[#FF3B30] text-xs font-bold tracking-[0.2em] mb-4 text-center">POR QUÉ ELEGIRNOS</span>
        <h2 class="text-4xl font-light tracking-tight text-center mb-16">El estándar Aurea</h2>
        <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-8">
          <div v-for="pillar in pillars" :key="pillar.title">
            <div class="w-8 h-0.5 bg-[#FF3B30] mb-5"></div>
            <h4 class="font-sans font-bold text-sm mb-3 tracking-tight">{{ pillar.title }}</h4>
            <p class="font-sans text-sm text-gray-500 leading-relaxed">{{ pillar.desc }}</p>
          </div>
        </div>
      </div>
    </div>

  </section>
</template>

<script setup>
import { ref } from 'vue'
// Importación correcta de tu imagen desde la carpeta assets
import sillaImg from '../assets/silla.jpg' 

const stats = [
  { number: '10+', label: 'Años de experiencia' },
  { number: '8K+', label: 'Pacientes atendidos' },
  { number: '20+', label: 'Especialistas certificados' },
  { number: '98%', label: 'Tasa de satisfacción' },
]

const mvv = [
  {
    title: 'Misión',
    text: 'Brindar tratamientos estéticos seguros y efectivos que potencien la confianza y el bienestar de nuestros pacientes, con los más altos estándares médicos.',
    iconPath: 'M12 21a9 9 0 1 0 0-18 9 9 0 0 0 0 18Zm0-5a4 4 0 1 0 0-8 4 4 0 0 0 0 8Zm0-2a2 2 0 1 0 0-4 2 2 0 0 0 0 4Z',
  },
  {
    title: 'Visión',
    text: 'Ser el centro de medicina estética de mayor confianza en el Caribe, reconocido por la excelencia clínica, la innovación tecnológica y el trato humano.',
    iconPath: 'M3 12s3-7 9-7 9 7 9 7-3 7-9 7-9-7-9-7Zm9 3a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z',
  },
  {
    title: 'Valores',
    text: 'Integridad, excelencia, empatía e innovación. Cada decisión que tomamos está guiada por el bienestar del paciente y el compromiso con la ética médica.',
    iconPath: 'M12 21C12 21 4 13.5 4 8.5a5 5 0 0 1 8-4 5 5 0 0 1 8 4c0 5-8 12.5-8 12.5Z',
  },
]

const pillars = [
  { title: 'Tecnología de punta', desc: 'Invertimos continuamente en los equipos más avanzados del mercado para garantizar resultados óptimos.' },
  { title: 'Equipo certificado', desc: 'Todos nuestros médicos cuentan con formación especializada y certificaciones internacionales.' },
  { title: 'Atención personalizada', desc: 'Cada plan de tratamiento es único. Escuchamos, evaluamos y diseñamos la solución ideal para ti.' },
  { title: 'Seguridad ante todo', desc: 'Cumplimos con los más estrictos protocolos de bioseguridad y usamos solo productos aprobados.' },
]
</script>