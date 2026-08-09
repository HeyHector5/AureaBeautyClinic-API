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
const { loading, load, past } = useMyAppointments()

onMounted(load)
</script>

<template>
  <div>
    <PageHeader
      eyebrow="Mi cuenta"
      title="Historial"
      description="Tus visitas completadas, canceladas y las citas cuya fecha ya pasó."
    />

    <LoadingSpinner v-if="loading" />

    <div v-else-if="!past.length" class="border border-gray-100 rounded-2xl bg-white">
      <EmptyState
        title="Tu historial está vacío"
        description="Aquí guardaremos el registro de cada visita a la clínica."
      >
        <BaseButton @click="router.push('/reservar')">Reservar mi primera cita</BaseButton>
      </EmptyState>
    </div>

    <div v-else class="grid gap-6 md:grid-cols-2">
      <AppointmentCard
        v-for="appointment in past"
        :key="appointment.appointmentId"
        :appointment="appointment"
      />
    </div>
  </div>
</template>
