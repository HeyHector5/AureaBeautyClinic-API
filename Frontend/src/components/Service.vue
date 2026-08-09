<script setup>
import { computed, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import LoadingSpinner from '@/components/ui/LoadingSpinner.vue'
import EmptyState from '@/components/ui/EmptyState.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import { getSpecialties } from '@/services/specialtyService'

const router = useRouter()

const specialties = ref([])
const loading = ref(true)
const failed = ref(false)

const activeSpecialties = computed(() => specialties.value.filter((s) => s.isActive))

/**
 * El catálogo vive en la API (SpecialtyDTO), que no incluye iconografía.
 * Se asigna un icono del set de la marca de forma estable según la posición.
 */
const ICONS = [
  '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M12 3c3.5 0 6.5 3 6.5 7 0 4.5-3 8-6.5 11-3.5-3-6.5-6.5-6.5-11 0-4 3-7 6.5-7Z" stroke="currentColor" stroke-width="1.6" stroke-linejoin="round"/><circle cx="12" cy="10" r="2.2" stroke="currentColor" stroke-width="1.6"/></svg>',
  '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M12 20V6" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/><path d="M9 8l3-3 3 3" stroke="currentColor" stroke-width="1.6" stroke-linecap="round" stroke-linejoin="round"/><circle cx="12" cy="19" r="1.4" fill="currentColor"/></svg>',
  '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M6 15c0-4 2.5-9 6-9s6 5 6 9" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/><path d="M4 17.5c1.5 1.5 3 2 4 2s2-.7 2-2 .8-2 2-2 2 1 2 2 1 2 2 2 2.5-.5 4-2" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/></svg>',
  '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M4 12h11" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/><path d="M15 8h3.5a1.5 1.5 0 0 1 1.5 1.5v5a1.5 1.5 0 0 1-1.5 1.5H15" stroke="currentColor" stroke-width="1.6" stroke-linejoin="round"/><path d="M4 9v6" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/></svg>',
  '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><circle cx="12" cy="12" r="7.5" stroke="currentColor" stroke-width="1.6"/><path d="M12 8v4l2.5 2.5" stroke="currentColor" stroke-width="1.6" stroke-linecap="round" stroke-linejoin="round"/></svg>',
  '<svg viewBox="0 0 24 24" fill="none" class="h-7 w-7"><path d="M8 10h8M8 14h5" stroke="currentColor" stroke-width="1.6" stroke-linecap="round"/><rect x="4" y="4.5" width="16" height="14" rx="3" stroke="currentColor" stroke-width="1.6"/></svg>'
]

const iconFor = (index) => ICONS[index % ICONS.length]

const reservar = (specialty) =>
  router.push({ path: '/reservar', query: { specialtyId: specialty.specialtyId } })

onMounted(async () => {
  try {
    specialties.value = (await getSpecialties()) ?? []
  } catch {
    // La página pública no debe alarmar con un modal: se muestra un estado vacío.
    failed.value = true
  } finally {
    loading.value = false
  }
})
</script>

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
        <span class="font-body text-sm font-semibold uppercase tracking-[0.2em] text-aurea">
          Servicios
        </span>
        <h2 class="mt-3 font-display text-4xl leading-tight text-gray-800 md:text-5xl">
          Tratamientos pensados <span class="italic text-aurea">para ti</span>
        </h2>
        <p class="mt-5 font-body text-gray-500">
          Cada servicio combina tecnología, productos de calidad y manos expertas
          para que veas y sientas resultados reales.
        </p>
      </div>

      <LoadingSpinner v-if="loading" label="Cargando servicios..." />

      <EmptyState
        v-else-if="!activeSpecialties.length"
        class="mt-10"
        :title="failed ? 'No pudimos cargar los servicios' : 'Estamos actualizando el catálogo'"
        description="Escríbenos y con gusto te contamos todo lo que ofrecemos."
      >
        <BaseButton variant="secondary" @click="router.push('/contactos')">Contáctanos</BaseButton>
      </EmptyState>

      <!-- Grid de servicios -->
      <div v-else class="mt-16 grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-3">
        <article
          v-for="(specialty, index) in activeSpecialties"
          :key="specialty.specialtyId"
          class="group relative flex flex-col rounded-3xl border border-gray-100 bg-white p-8 transition hover:-translate-y-1 hover:border-rose-100 hover:shadow-[0_20px_45px_-20px_rgba(255,59,48,0.25)]"
        >
          <div
            class="flex h-14 w-14 items-center justify-center rounded-2xl bg-rose-50 text-aurea transition group-hover:bg-aurea group-hover:text-white"
            aria-hidden="true"
            v-html="iconFor(index)"
          ></div>

          <h3 class="mt-6 font-display text-2xl text-gray-800">
            {{ specialty.name }}
          </h3>
          <p class="mt-3 font-body text-sm leading-relaxed text-gray-500">
            {{ specialty.description || 'Consulta con nuestros especialistas los detalles de este tratamiento.' }}
          </p>

          <div class="mt-auto flex items-center justify-between border-t border-gray-100 pt-5">
            <span class="font-body text-sm text-gray-400">Con cita previa</span>
            <button
              type="button"
              class="font-body text-sm font-semibold text-aurea transition hover:text-gray-800 cursor-pointer"
              @click="reservar(specialty)"
            >
              Reservar →
            </button>
          </div>
        </article>
      </div>
    </div>
  </section>
</template>
