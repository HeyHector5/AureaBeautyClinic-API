<script setup>
import { onMounted } from 'vue'
import { useRouter } from 'vue-router'
import PageHeader from '@/components/ui/PageHeader.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import EmptyState from '@/components/ui/EmptyState.vue'
import LoadingSpinner from '@/components/ui/LoadingSpinner.vue'
import AppointmentCard from '@/components/AppointmentCard.vue'
import { useMyAppointments } from '@/composables/useMyAppointments'

const router = useRouter()
const { loading, load, upcoming, cancel } = useMyAppointments()

onMounted(load)
</script>

<template>
  <div>
    <PageHeader
      eyebrow="Mi cuenta"
      title="Mis reservas"
      description="Tus citas pendientes y confirmadas. Las visitas ya realizadas están en el historial."
    >
      <template #actions>
        <BaseButton @click="router.push('/reservar')">Reservar cita</BaseButton>
      </template>
    </PageHeader>

    <LoadingSpinner v-if="loading" />

    <div v-else-if="!upcoming.length" class="border border-gray-100 rounded-2xl bg-white">
      <EmptyState
        title="No tienes reservas activas"
        description="Cuando reserves una cita aparecerá aquí hasta que se complete o la canceles."
      >
        <BaseButton @click="router.push('/reservar')">Reservar ahora</BaseButton>
      </EmptyState>
    </div>

    <div v-else class="grid gap-6 md:grid-cols-2">
      <AppointmentCard
        v-for="appointment in upcoming"
        :key="appointment.appointmentId"
        :appointment="appointment"
        cancellable
        @cancel="cancel"
      />
    </div>
  </div>
</template>
