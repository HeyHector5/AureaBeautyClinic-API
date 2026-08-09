<script setup>
import { CLINIC } from '@/constants/clinic'

const NAV = [
  { to: '/', label: 'Inicio' },
  { to: '/servicios', label: 'Servicios' },
  { to: '/nosotros', label: 'Nosotros' },
  { to: '/contactos', label: 'Contactos' }
]

const year = new Date().getFullYear()
</script>

<template>
  <footer class="bg-gray-900 text-gray-300 pt-16 pb-8 border-t border-gray-800">
    <div class="max-w-7xl mx-auto px-8 grid grid-cols-1 md:grid-cols-4 gap-12 mb-12">
      <div class="space-y-4">
        <span class="text-2xl font-bold tracking-tighter text-white">
          <span class="text-aurea">{{ CLINIC.shortName }}</span>
          <span class="text-gray-400 font-light ml-1 uppercase text-sm tracking-widest block md:inline">
            Clinic
          </span>
        </span>
        <p class="text-sm text-gray-400 leading-relaxed">
          Tu bienestar y belleza natural en manos de profesionales certificados.
        </p>

        <div class="flex gap-3 pt-2">
          <a
            :href="CLINIC.social.instagram"
            target="_blank"
            rel="noopener noreferrer"
            aria-label="Instagram"
            class="h-9 w-9 rounded-full border border-gray-800 flex items-center justify-center text-gray-400 hover:border-aurea hover:text-aurea transition-colors"
          >
            <svg class="h-4 w-4" fill="none" stroke="currentColor" stroke-width="1.8" viewBox="0 0 24 24">
              <rect x="3" y="3" width="18" height="18" rx="5" />
              <circle cx="12" cy="12" r="4" />
              <circle cx="17" cy="7" r="1" fill="currentColor" stroke="none" />
            </svg>
          </a>
          <a
            :href="CLINIC.social.facebook"
            target="_blank"
            rel="noopener noreferrer"
            aria-label="Facebook"
            class="h-9 w-9 rounded-full border border-gray-800 flex items-center justify-center text-gray-400 hover:border-aurea hover:text-aurea transition-colors"
          >
            <svg class="h-4 w-4" fill="none" stroke="currentColor" stroke-width="1.8" viewBox="0 0 24 24">
              <path d="M14 8h2V5h-2a4 4 0 00-4 4v2H8v3h2v6h3v-6h2.5l.5-3H13V9a1 1 0 011-1z" />
            </svg>
          </a>
        </div>
      </div>

      <div>
        <h4 class="font-bold tracking-widest uppercase text-xs mb-4 text-aurea">Navegación</h4>
        <ul class="space-y-2 text-sm">
          <li v-for="item in NAV" :key="item.to">
            <RouterLink :to="item.to" class="hover:text-white transition-colors">
              {{ item.label }}
            </RouterLink>
          </li>
        </ul>
      </div>

      <div>
        <h4 class="font-bold tracking-widest uppercase text-xs mb-4 text-aurea">Horarios</h4>
        <ul class="space-y-2 text-sm text-gray-400">
          <li v-for="slot in CLINIC.hours" :key="slot.days">
            <span class="font-semibold text-gray-300">{{ slot.days }}:</span> {{ slot.time }}
          </li>
        </ul>
      </div>

      <div>
        <h4 class="font-bold tracking-widest uppercase text-xs mb-4 text-aurea">Contacto</h4>
        <ul class="space-y-2 text-sm text-gray-400">
          <li>📍 {{ CLINIC.address }}</li>
          <li>
            📞
            <a :href="CLINIC.phoneHref" class="hover:text-white transition-colors">
              {{ CLINIC.phone }}
            </a>
          </li>
          <li>
            ✉️
            <a :href="`mailto:${CLINIC.email}`" class="hover:text-white transition-colors">
              {{ CLINIC.email }}
            </a>
          </li>
        </ul>
      </div>
    </div>

    <div
      class="max-w-7xl mx-auto px-8 pt-8 border-t border-gray-800 text-center md:flex md:justify-between text-xs text-gray-500"
    >
      <p>&copy; {{ year }} {{ CLINIC.name }}. Todos los derechos reservados.</p>
      <p class="mt-2 md:mt-0">Diseñado con excelencia médica y estética.</p>
    </div>
  </footer>
</template>
