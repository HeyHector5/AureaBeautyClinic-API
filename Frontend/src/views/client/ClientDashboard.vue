<script setup>
import { onMounted } from 'vue'
import { useRouter } from 'vue-router'
import PageHeader from '@/components/ui/PageHeader.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import EmptyState from '@/components/ui/EmptyState.vue'
import LoadingSpinner from '@/components/ui/LoadingSpinner.vue'
import AppointmentCard from '@/components/AppointmentCard.vue'
import { useAuth } from '@/composables/useAuth'
import { useMyAppointments } from '@/composables/useMyAppointments'

const router = useRouter()
const { user } = useAuth()
const { loading, load, upcoming, past, nextAppointment, cancel } = useMyAppointments()

const QUICK_LINKS = [
  {
    to: '/reservar',
    title: 'Reservar una cita',
    text: 'Elige especialidad, doctor y horario en un minuto.'
  },
  {
    to: '/mis-reservas',
    title: 'Mis reservas',
    text: 'Consulta y gestiona tus citas próximas.'
  },
  {
    to: '/perfil',
    title: 'Mi perfil',
    text: 'Actualiza tus datos de contacto.'
  }
]

onMounted(load)
</script>

<template>
  <div>
    <PageHeader
      eyebrow="Mi cuenta"
      :title="`Hola, ${user?.name || 'bienvenida'}`"
      description="Aquí puedes ver tu próxima visita y gestionar tus reservas."
    >
      <template #actions>
        <BaseButton @click="router.push('/reservar')">Reservar cita</BaseButton>
      </template>
    </PageHeader>

    <LoadingSpinner v-if="loading" />

    <template v-else>
      <section class="mb-12">
        <h2 class="text-xs font-bold tracking-[0.2em] uppercase text-aurea mb-4">Tu próxima cita</h2>

        <AppointmentCard
          v-if="nextAppointment"
          :appointment="nextAppointment"
          cancellable
          @cancel="cancel"
        />

        <div v-else class="border border-gray-100 rounded-2xl bg-white">
          <EmptyState
            title="No tienes citas programadas"
            description="Reserva tu próxima visita y te esperamos en la clínica."
          >
            <BaseButton @click="router.push('/reservar')">Reservar ahora</BaseButton>
          </EmptyState>
        </div>
      </section>

      <section class="grid gap-6 sm:grid-cols-3 mb-12">
        <button
          v-for="link in QUICK_LINKS"
          :key="link.to"
          type="button"
          class="group text-left border border-gray-100 rounded-xl bg-white p-6 transition-all duration-300 hover:border-aurea hover:shadow-lg hover:-translate-y-1 cursor-pointer"
          @click="router.push(link.to)"
        >
          <h3 class="font-serif text-lg font-light text-gray-800 mb-1">{{ link.title }}</h3>
          <p class="text-sm text-gray-500">{{ link.text }}</p>
          <span class="mt-3 inline-block text-xs font-bold tracking-widest uppercase text-aurea">
            Ir →
          </span>
        </button>
      </section>

      <section v-if="upcoming.length > 1 || past.length">
        <div class="flex items-center justify-between gap-4 mb-4">
          <h2 class="text-xs font-bold tracking-[0.2em] uppercase text-gray-400">Resumen</h2>
        </div>
        <div class="grid gap-4 sm:grid-cols-2">
          <div class="border border-gray-100 rounded-xl bg-white p-6">
            <p class="text-3xl font-serif font-light text-gray-800">{{ upcoming.length }}</p>
            <p class="text-sm text-gray-500 mt-1">
              cita{{ upcoming.length === 1 ? '' : 's' }} próxima{{ upcoming.length === 1 ? '' : 's' }}
            </p>
          </div>
          <div class="border border-gray-100 rounded-xl bg-white p-6">
            <p class="text-3xl font-serif font-light text-gray-800">{{ past.length }}</p>
            <p class="text-sm text-gray-500 mt-1">
              visita{{ past.length === 1 ? '' : 's' }} en tu historial
            </p>
          </div>
        </div>
      </section>
    </template>
  </div>
</template>
