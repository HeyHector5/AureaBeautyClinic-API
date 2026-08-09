<script setup>
import Navbar from '@/components/Navbar.vue'
import Footer from '@/components/Footer.vue'

const TABS = [
  { to: '/dashboard', label: 'Resumen' },
  { to: '/mis-reservas', label: 'Mis Reservas' },
  { to: '/historial', label: 'Historial' },
  { to: '/perfil', label: 'Mi Perfil' }
]
</script>

<template>
  <div class="min-h-screen flex flex-col justify-between bg-gray-50">
    <Navbar />

    <main class="flex-grow">
      <div class="border-b border-gray-100 bg-white">
        <nav class="max-w-6xl mx-auto px-6 lg:px-8 flex gap-2 overflow-x-auto" aria-label="Mi cuenta">
          <RouterLink
            v-for="tab in TABS"
            :key="tab.to"
            :to="tab.to"
            class="px-4 py-4 text-xs font-bold uppercase tracking-widest whitespace-nowrap border-b-2 transition-colors"
            :class="
              $route.path === tab.to
                ? 'border-aurea text-aurea'
                : 'border-transparent text-gray-400 hover:text-gray-700'
            "
          >
            {{ tab.label }}
          </RouterLink>
        </nav>
      </div>

      <div class="max-w-6xl mx-auto px-6 lg:px-8 py-12">
        <RouterView />
      </div>
    </main>

    <Footer />
  </div>
</template>
