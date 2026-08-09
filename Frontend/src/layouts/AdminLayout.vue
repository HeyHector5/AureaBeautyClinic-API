<script setup>
import { ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useAuth } from '@/composables/useAuth'
import { confirmAction } from '@/utils/notify'
import { CLINIC } from '@/constants/clinic'

const route = useRoute()
const router = useRouter()
const { fullName, initials, user, logout } = useAuth()

const sidebarOpen = ref(false)

const NAV = [
  { to: '/admin', label: 'Resumen', exact: true, icon: 'M3 12l9-9 9 9M5 10v10h14V10' },
  { to: '/admin/citas', label: 'Citas', icon: 'M8 7V3m8 4V3M4 11h16M5 21h14a1 1 0 001-1V7a1 1 0 00-1-1H5a1 1 0 00-1 1v13a1 1 0 001 1z' },
  { to: '/admin/usuarios', label: 'Usuarios', icon: 'M17 20h5v-2a3 3 0 00-5.36-1.86M17 20H7m10 0v-2c0-.66-.13-1.3-.36-1.86m0 0A5 5 0 0012 13a5 5 0 00-4.64 3.14M7 20H2v-2a3 3 0 015.36-1.86M7 20v-2c0-.66.13-1.3.36-1.86m0 0a5 5 0 019.28 0M15 7a3 3 0 11-6 0 3 3 0 016 0z' },
  { to: '/admin/doctores', label: 'Doctores', icon: 'M9 3v2m6-2v2M5 8h14M6 21h12a2 2 0 002-2V7a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2zm6-9v6m-3-3h6' },
  { to: '/admin/especialidades', label: 'Especialidades', icon: 'M7 7h.01M7 3h5a2 2 0 011.41.59l7 7a2 2 0 010 2.82l-5 5a2 2 0 01-2.82 0l-7-7A2 2 0 015 10V5a2 2 0 012-2z' },
  { to: '/admin/roles', label: 'Roles', icon: 'M12 3l8 4v5c0 4.42-3.4 8.55-8 9.8-4.6-1.25-8-5.38-8-9.8V7l8-4z' }
]

const isActive = (item) =>
  item.exact ? route.path === item.to : route.path.startsWith(item.to)

// En móvil el menú se cierra al navegar.
watch(() => route.path, () => {
  sidebarOpen.value = false
})

const handleLogout = async () => {
  const confirmed = await confirmAction({
    title: '¿Cerrar sesión?',
    text: 'Volverás a la página pública de la clínica.',
    confirmText: 'Cerrar sesión',
    icon: 'question'
  })
  if (!confirmed) return

  logout()
  router.push('/')
}
</script>

<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Fondo oscuro del menú en móvil -->
    <div
      v-if="sidebarOpen"
      class="fixed inset-0 z-30 bg-gray-900/40 lg:hidden"
      @click="sidebarOpen = false"
    />

    <aside
      :class="[
        'fixed inset-y-0 left-0 z-40 w-64 bg-black text-white flex flex-col transition-transform duration-300',
        sidebarOpen ? 'translate-x-0' : '-translate-x-full',
        'lg:translate-x-0'
      ]"
    >
      <div class="px-6 py-6 border-b border-gray-800">
        <RouterLink to="/" class="text-xl font-bold tracking-tighter block">
          <span class="text-aurea">{{ CLINIC.shortName }}</span>
          <span class="text-gray-400 font-light ml-1 uppercase text-xs">{{ CLINIC.tagline }}</span>
        </RouterLink>
        <p class="mt-1 text-[10px] font-bold tracking-[0.2em] uppercase text-gray-500">
          Panel administrativo
        </p>
      </div>

      <nav class="flex-1 overflow-y-auto py-4 px-3 space-y-1">
        <RouterLink
          v-for="item in NAV"
          :key="item.to"
          :to="item.to"
          :class="[
            'flex items-center gap-3 px-3 py-2.5 rounded-lg text-sm font-medium transition-colors',
            isActive(item)
              ? 'bg-aurea text-white'
              : 'text-gray-400 hover:bg-gray-900 hover:text-white'
          ]"
        >
          <svg class="h-5 w-5 shrink-0" fill="none" stroke="currentColor" stroke-width="1.7" viewBox="0 0 24 24" aria-hidden="true">
            <path stroke-linecap="round" stroke-linejoin="round" :d="item.icon" />
          </svg>
          {{ item.label }}
        </RouterLink>
      </nav>

      <div class="border-t border-gray-800 p-4">
        <RouterLink
          to="/"
          class="flex items-center gap-2 px-3 py-2 rounded-lg text-xs font-bold uppercase tracking-widest text-gray-500 hover:text-white transition-colors"
        >
          ← Ver sitio público
        </RouterLink>
      </div>
    </aside>

    <div class="lg:pl-64">
      <header
        class="sticky top-0 z-20 flex items-center justify-between gap-4 bg-white border-b border-gray-100 px-4 sm:px-8 py-4"
      >
        <button
          type="button"
          class="lg:hidden text-gray-600 hover:text-aurea transition-colors cursor-pointer"
          aria-label="Abrir menú"
          :aria-expanded="sidebarOpen"
          @click="sidebarOpen = !sidebarOpen"
        >
          <svg class="h-6 w-6" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" d="M4 6h16M4 12h16M4 18h16" />
          </svg>
        </button>

        <div class="ml-auto flex items-center gap-3">
          <div class="text-right hidden sm:block">
            <p class="text-sm font-bold text-gray-800 leading-tight">{{ fullName }}</p>
            <p class="text-xs text-gray-400">{{ user?.role }}</p>
          </div>
          <div
            class="h-9 w-9 rounded-full bg-aurea text-white flex items-center justify-center text-xs font-bold"
            aria-hidden="true"
          >
            {{ initials }}
          </div>
          <button
            type="button"
            class="px-3 py-1.5 text-xs font-bold rounded-lg border border-gray-200 text-gray-600 hover:border-aurea hover:text-aurea transition-colors cursor-pointer"
            @click="handleLogout"
          >
            Salir
          </button>
        </div>
      </header>

      <main class="p-4 sm:p-8">
        <RouterView />
      </main>
    </div>
  </div>
</template>
