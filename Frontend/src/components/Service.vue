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

      <!-- Filtro por categoría -->
      <div class="mt-10 flex flex-wrap justify-center gap-2">
        <button
          v-for="opcion in ['Todas', ...CATEGORIAS]"
          :key="opcion"
          type="button"
          @click="categoriaSeleccionada = opcion"
          class="px-4 py-2 rounded-full text-xs font-bold uppercase tracking-wide transition-colors cursor-pointer"
          :class="categoriaSeleccionada === opcion
            ? 'bg-[#FF3B30] text-white'
            : 'bg-rose-50 text-neutral-500 hover:text-[#FF3B30]'"
        >
          {{ opcion }}
        </button>
      </div>

      <!-- Grid de servicios -->
      <div class="mt-12 grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-3">
        <article
          v-for="servicio in serviciosFiltrados"
          :key="servicio.id"
          class="group relative rounded-3xl border border-neutral-100 bg-white p-8 transition hover:-translate-y-1 hover:border-rose-100 hover:shadow-[0_20px_45px_-20px_rgba(255,59,48,0.25)]"
        >
          <div
            class="flex h-14 w-14 items-center justify-center rounded-2xl bg-rose-50 text-2xl transition group-hover:bg-[#FF3B30]"
          >
            {{ servicio.icono }}
          </div>

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
              v-if="!esAdmin"
              type="button"
              class="font-body text-sm font-semibold text-[#FF3B30] transition hover:text-neutral-800"
              @click="seleccionarServicio(servicio)"
            >
              Reservar →
            </button>
          </div>
        </article>

        <p v-if="!serviciosFiltrados.length" class="col-span-full text-center text-neutral-400 font-body">
          No hay servicios en esta categoría por el momento.
        </p>
      </div>
    </div>
  </section>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { servicios, CATEGORIAS } from '../store/services'

const router = useRouter()

const categoriaSeleccionada = ref('Todas')

const esAdmin = computed(() => {
  const user = JSON.parse(localStorage.getItem('user') || '{}')
  return user.role === 'Admin'
})

const serviciosActivos = computed(() => servicios.value.filter(s => s.activo))

const serviciosFiltrados = computed(() =>
  categoriaSeleccionada.value === 'Todas'
    ? serviciosActivos.value
    : serviciosActivos.value.filter(s => s.categoria === categoriaSeleccionada.value)
)

function seleccionarServicio(servicio) {
  router.push({ path: '/reservar', query: { servicio: servicio.nombre } })
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