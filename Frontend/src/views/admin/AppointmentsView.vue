<script setup>
import { computed, onMounted, reactive, ref } from 'vue'
import PageHeader from '@/components/ui/PageHeader.vue'
import DataTable from '@/components/ui/DataTable.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseModal from '@/components/ui/BaseModal.vue'
import BaseInput from '@/components/ui/BaseInput.vue'
import BaseSelect from '@/components/ui/BaseSelect.vue'
import BaseTextarea from '@/components/ui/BaseTextarea.vue'
import BaseBadge from '@/components/ui/BaseBadge.vue'
import { useCrud } from '@/composables/useCrud'
import {
  getAppointments,
  updateAppointment,
  cancelAppointment
} from '@/services/appointmentService'
import { getDoctors } from '@/services/doctorService'
import {
  APPOINTMENT_STATES,
  APPOINTMENT_STATE_OPTIONS,
  stateBadgeClass,
  stateLabel
} from '@/constants/appointment'
import { formatDateTime, toDateTimeLocal } from '@/utils/format'
import { notifyApiError, toast } from '@/utils/notify'

/**
 * Las citas no se crean desde el panel: nacen del flujo de reserva del cliente
 * (POST /api/appointment exige el userId del paciente). Aquí se gestionan.
 */
const { items, loading, saving, load, save, confirmRemove } = useCrud({
  fetchAll: getAppointments,
  update: updateAppointment,
  remove: cancelAppointment,
  idKey: 'appointmentId',
  labels: {
    singular: 'Cita',
    plural: 'las citas',
    removeTitle: '¿Cancelar la cita?',
    removeConfirm: 'Sí, cancelar',
    removed: 'Cita cancelada.'
  }
})

const doctors = ref([])
const stateFilter = ref('')
const doctorFilter = ref('')

const COLUMNS = [
  { key: 'scheduled', label: 'Fecha y hora', width: '190px' },
  { key: 'user.fullName', label: 'Paciente' },
  { key: 'doctor.user.fullName', label: 'Doctor' },
  { key: 'doctor.specialty.name', label: 'Especialidad', width: '170px' },
  { key: 'state', label: 'Estado', width: '130px' },
  { key: 'actions', label: 'Acciones', sortable: false, searchable: false, align: 'right', width: '250px' }
]

const doctorOptions = computed(() =>
  doctors.value.map((d) => ({
    value: String(d.doctorId),
    label: `${d.user?.name} ${d.user?.lastName}`
  }))
)

const filteredAppointments = computed(() =>
  items.value.filter(
    (a) =>
      (!stateFilter.value || a.state === stateFilter.value) &&
      (!doctorFilter.value || String(a.doctorId) === doctorFilter.value)
  )
)

const counts = computed(() => ({
  pending: items.value.filter((a) => a.state === APPOINTMENT_STATES.PENDING).length,
  confirmed: items.value.filter((a) => a.state === APPOINTMENT_STATES.CONFIRMED).length
}))

const modalOpen = ref(false)
const editing = ref(null)
const form = reactive({ scheduled: '', state: '', notes: '' })

const openEdit = (row) => {
  editing.value = row
  Object.assign(form, {
    scheduled: toDateTimeLocal(row.scheduled),
    state: row.state,
    notes: row.notes ?? ''
  })
  modalOpen.value = true
}

const handleSubmit = async () => {
  // La API sólo permite cambiar scheduled, state y notes: el paciente y el
  // doctor de una cita no se pueden reasignar.
  const payload = {
    scheduled: new Date(form.scheduled).toISOString(),
    state: form.state,
    notes: form.notes.trim() || null
  }
  const ok = await save(payload, editing.value.appointmentId)
  if (ok) modalOpen.value = false
}

/** Cambio rápido de estado desde la fila, sin abrir el modal. */
const quickSetState = async (row, state) => {
  try {
    await updateAppointment(row.appointmentId, {
      scheduled: row.scheduled,
      state,
      notes: row.notes ?? null
    })
    toast(`Cita marcada como ${stateLabel(state).toLowerCase()}.`)
    await load()
  } catch (e) {
    notifyApiError(e, 'No pudimos actualizar la cita')
  }
}

onMounted(async () => {
  await load()
  try {
    doctors.value = (await getDoctors()) ?? []
  } catch (e) {
    notifyApiError(e, 'No pudimos cargar los doctores')
  }
})
</script>

<template>
  <div>
    <PageHeader
      eyebrow="Agenda"
      title="Citas"
      :description="`${counts.pending} pendiente${counts.pending === 1 ? '' : 's'} y ${counts.confirmed} confirmada${counts.confirmed === 1 ? '' : 's'}. Las citas las generan los pacientes desde el sitio.`"
    />

    <DataTable
      :columns="COLUMNS"
      :rows="filteredAppointments"
      row-key="appointmentId"
      :loading="loading"
      search-placeholder="Buscar por paciente, doctor o especialidad..."
      empty-title="Aún no hay citas"
      empty-description="Cuando un paciente reserve desde el sitio, la cita aparecerá aquí como pendiente."
    >
      <template #filters>
        <select
          v-model="stateFilter"
          aria-label="Filtrar por estado"
          class="px-3 py-2.5 text-sm rounded-lg border border-gray-200 bg-white outline-none transition-all focus:ring-2 focus:ring-aurea focus:border-transparent cursor-pointer"
        >
          <option value="">Todos los estados</option>
          <option v-for="opt in APPOINTMENT_STATE_OPTIONS" :key="opt.value" :value="opt.value">
            {{ opt.label }}
          </option>
        </select>

        <select
          v-model="doctorFilter"
          aria-label="Filtrar por doctor"
          class="px-3 py-2.5 text-sm rounded-lg border border-gray-200 bg-white outline-none transition-all focus:ring-2 focus:ring-aurea focus:border-transparent cursor-pointer"
        >
          <option value="">Todos los doctores</option>
          <option v-for="opt in doctorOptions" :key="opt.value" :value="opt.value">
            {{ opt.label }}
          </option>
        </select>
      </template>

      <template #cell-scheduled="{ value }">
        <span class="font-semibold text-gray-800">{{ formatDateTime(value) }}</span>
      </template>

      <template #cell-user-fullName="{ row }">
        <div class="min-w-0">
          <p class="font-medium text-gray-800 truncate">
            {{ row.user?.name }} {{ row.user?.lastName }}
          </p>
          <p class="text-xs text-gray-400 truncate">{{ row.user?.email }}</p>
        </div>
      </template>

      <template #cell-doctor-user-fullName="{ row }">
        <span class="text-gray-700">
          {{ row.doctor?.user?.name }} {{ row.doctor?.user?.lastName }}
        </span>
      </template>

      <template #cell-doctor-specialty-name="{ value }">
        <BaseBadge variant="bg-gray-100 text-gray-700 border-gray-200">{{ value }}</BaseBadge>
      </template>

      <template #cell-state="{ value }">
        <BaseBadge :variant="stateBadgeClass(value)">{{ stateLabel(value) }}</BaseBadge>
      </template>

      <template #cell-actions="{ row }">
        <div class="flex justify-end gap-2">
          <BaseButton
            v-if="row.state === APPOINTMENT_STATES.PENDING"
            size="sm"
            @click="quickSetState(row, APPOINTMENT_STATES.CONFIRMED)"
          >
            Confirmar
          </BaseButton>
          <BaseButton
            v-else-if="row.state === APPOINTMENT_STATES.CONFIRMED"
            size="sm"
            @click="quickSetState(row, APPOINTMENT_STATES.COMPLETED)"
          >
            Completar
          </BaseButton>

          <BaseButton variant="secondary" size="sm" @click="openEdit(row)">Editar</BaseButton>

          <BaseButton
            v-if="row.state !== APPOINTMENT_STATES.CANCELLED"
            variant="danger"
            size="sm"
            @click="
              confirmRemove(
                row,
                (r) => `La cita de ${r.user?.name} ${r.user?.lastName}`
              )
            "
          >
            Cancelar
          </BaseButton>
        </div>
      </template>
    </DataTable>

    <BaseModal
      v-model="modalOpen"
      title="Editar cita"
      :subtitle="
        editing
          ? `${editing.user?.name} ${editing.user?.lastName} con ${editing.doctor?.user?.name} ${editing.doctor?.user?.lastName}`
          : ''
      "
    >
      <form id="appointment-form" class="space-y-5" @submit.prevent="handleSubmit">
        <BaseInput v-model="form.scheduled" label="Fecha y hora" type="datetime-local" required />

        <BaseSelect
          v-model="form.state"
          label="Estado"
          :options="APPOINTMENT_STATE_OPTIONS"
          required
        />

        <BaseTextarea
          v-model="form.notes"
          label="Notas"
          placeholder="Observaciones internas sobre la cita."
          maxlength="500"
        />

        <p class="text-xs text-gray-400">
          El paciente y el doctor asignados no se pueden modificar desde aquí.
        </p>
      </form>

      <template #footer>
        <BaseButton variant="secondary" @click="modalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="appointment-form" :loading="saving">
          Guardar cambios
        </BaseButton>
      </template>
    </BaseModal>
  </div>
</template>
