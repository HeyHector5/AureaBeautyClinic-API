<script setup>
import { computed, onMounted, ref } from 'vue'
import PageHeader from '@/components/ui/PageHeader.vue'
import StatCard from '@/components/ui/StatCard.vue'
import BaseBadge from '@/components/ui/BaseBadge.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import EmptyState from '@/components/ui/EmptyState.vue'
import LoadingSpinner from '@/components/ui/LoadingSpinner.vue'
import { getAppointments } from '@/services/appointmentService'
import { getUsers } from '@/services/userService'
import { getDoctors } from '@/services/doctorService'
import { getSpecialties } from '@/services/specialtyService'
import {
  APPOINTMENT_STATES,
  isOpenAppointment,
  stateBadgeClass,
  stateLabel
} from '@/constants/appointment'
import { formatDateTime, isToday } from '@/utils/format'
import { useAuth } from '@/composables/useAuth'
import { notifyApiError } from '@/utils/notify'

const { fullName } = useAuth()

const loading = ref(true)
const appointments = ref([])
const users = ref([])
const doctors = ref([])
const specialties = ref([])

const stats = computed(() => ({
  todayCount: appointments.value.filter((a) => isToday(a.scheduled) && isOpenAppointment(a.state))
    .length,
  pendingCount: appointments.value.filter((a) => a.state === APPOINTMENT_STATES.PENDING).length,
  activeUsers: users.value.filter((u) => u.isActive).length,
  activeDoctors: doctors.value.filter((d) => d.isActive).length,
  activeSpecialties: specialties.value.filter((s) => s.isActive).length
}))

/** Próximas citas vivas, ordenadas por fecha. */
const upcoming = computed(() =>
  appointments.value
    .filter((a) => isOpenAppointment(a.state) && new Date(a.scheduled).getTime() >= Date.now())
    .sort((a, b) => new Date(a.scheduled) - new Date(b.scheduled))
    .slice(0, 6)
)

onMounted(async () => {
  try {
    const [a, u, d, s] = await Promise.all([
      getAppointments(),
      getUsers(),
      getDoctors(),
      getSpecialties()
    ])
    appointments.value = a ?? []
    users.value = u ?? []
    doctors.value = d ?? []
    specialties.value = s ?? []
  } catch (e) {
    notifyApiError(e, 'No pudimos cargar el resumen')
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <div>
    <PageHeader
      eyebrow="Panel"
      :title="`Hola, ${fullName || 'Administrador'}`"
      description="Un vistazo rápido al estado de la clínica."
    >
      <template #actions>
        <BaseButton variant="secondary" @click="$router.push('/admin/citas')">
          Ver todas las citas
        </BaseButton>
      </template>
    </PageHeader>

    <div class="grid gap-6 sm:grid-cols-2 xl:grid-cols-4 mb-10">
      <StatCard
        label="Citas de hoy"
        :value="stats.todayCount"
        hint="Pendientes o confirmadas"
        :loading="loading"
      >
        <template #icon>
          <svg class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="1.8" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" d="M8 7V3m8 4V3M4 11h16M5 21h14a1 1 0 001-1V7a1 1 0 00-1-1H5a1 1 0 00-1 1v13a1 1 0 001 1z" />
          </svg>
        </template>
      </StatCard>

      <StatCard
        label="Por confirmar"
        :value="stats.pendingCount"
        hint="Citas en estado pendiente"
        :loading="loading"
      >
        <template #icon>
          <svg class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="1.8" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>
        </template>
      </StatCard>

      <StatCard
        label="Doctores activos"
        :value="stats.activeDoctors"
        :hint="`${stats.activeSpecialties} especialidades activas`"
        :loading="loading"
      >
        <template #icon>
          <svg class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="1.8" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" d="M12 12a4 4 0 100-8 4 4 0 000 8zm0 2c-4 0-8 2-8 5v1h16v-1c0-3-4-5-8-5z" />
          </svg>
        </template>
      </StatCard>

      <StatCard
        label="Usuarios activos"
        :value="stats.activeUsers"
        :hint="`${users.length} cuentas en total`"
        :loading="loading"
      >
        <template #icon>
          <svg class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="1.8" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" d="M17 20h5v-2a3 3 0 00-5.36-1.86M17 20H7m10 0v-2c0-.66-.13-1.3-.36-1.86M7 20H2v-2a3 3 0 015.36-1.86M15 7a3 3 0 11-6 0 3 3 0 016 0z" />
          </svg>
        </template>
      </StatCard>
    </div>

    <section class="bg-white border border-gray-100 rounded-xl shadow-sm overflow-hidden">
      <header class="flex items-center justify-between gap-4 px-6 py-4 border-b border-gray-100">
        <div>
          <h2 class="text-lg font-serif font-light text-gray-800">Próximas citas</h2>
          <p class="text-xs text-gray-400">Las 6 más cercanas que siguen abiertas.</p>
        </div>
        <BaseButton variant="ghost" size="sm" @click="$router.push('/admin/citas')">
          Ver todas →
        </BaseButton>
      </header>

      <LoadingSpinner v-if="loading" />

      <EmptyState
        v-else-if="!upcoming.length"
        title="No hay citas próximas"
        description="Cuando un paciente reserve desde el sitio, aparecerá aquí."
      />

      <ul v-else class="divide-y divide-gray-50">
        <li
          v-for="appointment in upcoming"
          :key="appointment.appointmentId"
          class="flex flex-wrap items-center gap-4 px-6 py-4 hover:bg-aurea-tint/40 transition-colors"
        >
          <div class="min-w-[170px]">
            <p class="text-sm font-semibold text-gray-800">
              {{ formatDateTime(appointment.scheduled) }}
            </p>
          </div>

          <div class="flex-1 min-w-[180px]">
            <p class="text-sm text-gray-800">
              {{ appointment.user?.name }} {{ appointment.user?.lastName }}
            </p>
            <p class="text-xs text-gray-400">
              con {{ appointment.doctor?.user?.name }} {{ appointment.doctor?.user?.lastName }} ·
              {{ appointment.doctor?.specialty?.name }}
            </p>
          </div>

          <BaseBadge :variant="stateBadgeClass(appointment.state)">
            {{ stateLabel(appointment.state) }}
          </BaseBadge>
        </li>
      </ul>
    </section>
  </div>
</template>
