<template>
  <section id="servicios" class="relative overflow-hidden bg-white py-24">
    <!-- Forma orgánica decorativa, coherente con Contacto -->
    <div
      class="pointer-events-none absolute -left-32 -top-20 h-[460px] w-[460px] rounded-full bg-rose-50"
      aria-hidden="true"
    ></div>

    <div class="relative mx-auto max-w-6xl px-6 lg:px-8">
      <!-- Encabezado -->
      <div class="mx-auto max-w-2xl text-center">
        <span class="font-body text-sm font-semibold uppercase tracking-[0.2em] text-[#FF3B30]">
          Servicios
        </span>
        <h2 class="mt-3 font-display text-4xl leading-tight text-neutral-800 md:text-5xl">
          Tratamientos pensados <span class="italic text-[#FF3B30]">para ti</span>
        </h2>
        <p class="mt-5 font-body text-neutral-500">
          Cada servicio combina tecnología, productos de calidad y manos expertas
          para que veas y sientas resultados reales.
        </p>
      </div>

      <!-- Grid de servicios -->
      <div class="mt-16 grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-3">
        <article
          v-for="servicio in servicios"
          :key="servicio.nombre"
          class="group relative rounded-3xl border border-neutral-100 bg-white p-8 transition hover:-translate-y-1 hover:border-rose-100 hover:shadow-[0_20px_45px_-20px_rgba(255,59,48,0.25)]"
        >
          <div
            class="flex h-14 w-14 items-center justify-center rounded-2xl bg-rose-50 text-[#FF3B30] transition group-hover:bg-[#FF3B30] group-hover:text-white"
            v-html="servicio.icono"
          ></div>

          <h3 class="mt-6 font-display text-2xl text-neutral-800">
            {{ servicio.nombre }}
          </h3>
          <p class="mt-3 font-body text-sm leading-relaxed text-neutral-500">
            {{ servicio.descripcion }}
          </p>

          <div class="mt-6 flex items-center justify-between border-t border-neutral-100 pt-5">
            <span class="font-body text-sm font-semibold text-neutral-800">
              Desde {{ servicio.precio }}
            </span>
            <button
              type="button"
              class="font-body text-sm font-semibold text-[#FF3B30] transition hover:text-neutral-800"
              @click="seleccionarServicio(servicio)"
            >
              Reservar →
            </button>
          </div>
        </article>
      </div>
    </div>
  </section>
</template>

<script setup>
import { defineEmits } from 'vue'

const emit = defineEmits(['seleccionar'])

const servicios = [
  {
    nombre: 'Limpieza facial profunda',
    descripcion:
      'Exfoliación, extracción e hidratación para dejar tu piel visiblemente renovada.',
    precio: 'RD$2,500',
    icono:
      '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M12 3c3.5 0 6.5 3 6.5 7 0 4.5-3 8-6.5 11-3.5-3-6.5-6.5-6.5-11 0-4 3-7 6.5-7Z" stroke="currentColor" stroke-width="1.6" stroke-linejoin="round"/><circle cx="12" cy="10" r="2.2" stroke="currentColor" stroke-width="1.6"/></svg>',
  },
  {
    nombre: 'Microneedling',
    descripcion:
      'Estimula colágeno natural para reducir cicatrices, líneas finas y textura irregular.',
    precio: 'RD$4,800',
    icono:
      '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M12 20V6" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/><path d="M9 8l3-3 3 3" stroke="currentColor" stroke-width="1.6" stroke-linecap="round" stroke-linejoin="round"/><circle cx="12" cy="19" r="1.4" fill="currentColor"/></svg>',
  },
  {
    nombre: 'Masaje relajante corporal',
    descripcion:
      'Técnicas de liberación muscular y aceites esenciales para aliviar tensión y estrés.',
    precio: 'RD$3,200',
    icono:
      '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M6 15c0-4 2.5-9 6-9s6 5 6 9" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/><path d="M4 17.5c1.5 1.5 3 2 4 2s2-.7 2-2 .8-2 2-2 2 1 2 2 1 2 2 2 2.5-.5 4-2" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/></svg>',
  },
  {
    nombre: 'Depilación láser',
    descripcion:
      'Reducción progresiva y permanente del vello con tecnología de última generación.',
    precio: 'RD$1,800',
    icono:
      '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M4 12h11" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/><path d="M15 8h3.5a1.5 1.5 0 0 1 1.5 1.5v5a1.5 1.5 0 0 1-1.5 1.5H15" stroke="currentColor" stroke-width="1.6" stroke-linejoin="round"/><path d="M4 9v6" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/></svg>',
  },
  {
    nombre: 'Peeling químico',
    descripcion:
      'Renueva capas superficiales de la piel para un tono más uniforme y luminoso.',
    precio: 'RD$3,600',
    icono:
      '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><circle cx="12" cy="12" r="7.5" stroke="currentColor" stroke-width="1.6"/><path d="M12 8v4l2.5 2.5" stroke="currentColor" stroke-width="1.6" stroke-linecap="round" stroke-linejoin="round"/></svg>',
  },
  {
    nombre: 'Consulta de valoración',
    descripcion:
      'Evaluación personalizada con nuestros especialistas para diseñar tu plan de tratamiento.',
    precio: 'RD$800',
    icono:
      '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M8 10h8M8 14h5" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/><rect x="4" y="4.5" width="16" height="14" rx="3" stroke="currentColor" stroke-width="1.6"/></svg>',
  },
]

function seleccionarServicio(servicio) {
  emit('seleccionar', servicio)
}
</script>

<style scoped>
.font-display {
  font-family: 'Playfair Display', serif;
}
.font-body {
  font-family: 'Poppins', sans-serif;
}
</style>