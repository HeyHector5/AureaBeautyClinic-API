<script setup>
import BaseButton from '@/components/ui/BaseButton.vue'
import { useRouter } from 'vue-router'

const router = useRouter()
</script>

<template>
  <section class="flex items-center justify-center min-h-[70vh] px-6 py-24 bg-gray-50">
    <div class="text-center max-w-md">
      <p class="text-aurea text-xs font-bold tracking-[0.2em] uppercase mb-4">Error 404</p>
      <h1 class="font-display text-6xl md:text-7xl font-light text-gray-800 mb-4">
        Página no encontrada
      </h1>
      <p class="text-gray-500 mb-10">
        La página que buscas no existe o fue movida. Te acompañamos de vuelta al inicio.
      </p>

      <div class="flex flex-wrap justify-center gap-3">
        <BaseButton @click="router.push('/')">Volver al inicio</BaseButton>
        <BaseButton variant="secondary" @click="router.push('/servicios')">
          Ver servicios
        </BaseButton>
      </div>
    </div>
  </section>
</template>
