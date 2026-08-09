<script setup>
import { computed, onMounted, reactive, ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseInput from '@/components/ui/BaseInput.vue'
import BaseTextarea from '@/components/ui/BaseTextarea.vue'
import LoadingSpinner from '@/components/ui/LoadingSpinner.vue'
import EmptyState from '@/components/ui/EmptyState.vue'
import { getSpecialties } from '@/services/specialtyService'
import { getDoctors } from '@/services/doctorService'
import { createAppointment, getAppointmentsByDoctor } from '@/services/appointmentService'
import { useAuth } from '@/composables/useAuth'
import { isOpenAppointment } from '@/constants/appointment'
import { formatDateTime, toDateTimeLocal } from '@/utils/format'
import { notifyApiError, notifySuccess } from '@/utils/notify'

const route = useRoute()
const router = useRouter()
const { userId } = useAuth()

const loading = ref(true)
const submitting = ref(false)

const specialties = ref([])
const doctors = ref([])
const busySlots = ref([])

const form = reactive({
  specialtyId: '',
  doctorId: '',
  scheduled: '',
  notes: ''
})

const activeSpecialties = computed(() => specialties.value.filter((s) => s.isActive))

const doctorsForSpecialty = computed(() =>
  doctors.value.filter((d) => d.isActive && String(d.specialtyId) === String(form.specialtyId))
)

const selectedSpecialty = computed(() =>
  activeSpecialties.value.find((s) => String(s.specialtyId) === String(form.specialtyId))
)

const selectedDoctor = computed(() =>
  doctors.value.find((d) => String(d.doctorId) === String(form.doctorId))
)

/** No se puede reservar en el pasado. */
const minDateTime = computed(() => toDateTimeLocal(new Date()))

const canSubmit = computed(
  () => form.specialtyId && form.doctorId && form.scheduled && !submitting.value
)

const selectSpecialty = (specialty) => {
  form.specialtyId = String(specialty.specialtyId)
  form.doctorId = ''
  busySlots.value = []
}

/**
 * Muestra los horarios ya ocupados del doctor como referencia. El backend no
 * valida solapamientos, así que esto es una ayuda visual, no una restricción.
 */
watch(
  () => form.doctorId,
  async (doctorId) => {
    busySlots.value = []
    if (!doctorId) return
    try {
      const existing = (await getAppointmentsByDoctor(doctorId)) ?? []
      busySlots.value = existing
        .filter((a) => isOpenAppointment(a.state) && new Date(a.scheduled).getTime() >= Date.now())
        .map((a) => a.scheduled)
        .sort((a, b) => new Date(a) - new Date(b))
        .slice(0, 6)
    } catch {
      // La agenda del doctor es informativa: si falla, la reserva sigue disponible.
      busySlots.value = []
    }
  }
)

const handleSubmit = async () => {
  submitting.value = true
  try {
    await createAppointment({
      userId: userId.value,
      doctorId: Number(form.doctorId),
      scheduled: new Date(form.scheduled).toISOString(),
      notes: form.notes.trim() || null
    })

    await notifySuccess(
      '¡Cita reservada!',
      'Te confirmaremos por teléfono o correo en las próximas horas.'
    )
    router.push('/mis-reservas')
  } catch (e) {
    notifyApiError(e, 'No pudimos reservar tu cita')
  } finally {
    submitting.value = false
  }
}

onMounted(async () => {
  try {
    const [s, d] = await Promise.all([getSpecialties(), getDoctors()])
    specialties.value = s ?? []
    doctors.value = d ?? []

    // Permite llegar desde la página de servicios con la especialidad preseleccionada.
    const preselected = route.query.specialtyId
    if (preselected && specialties.value.some((x) => String(x.specialtyId) === String(preselected))) {
      form.specialtyId = String(preselected)
    }
  } catch (e) {
    notifyApiError(e, 'No pudimos cargar la disponibilidad')
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <section class="py-16 md:py-24 px-6 lg:px-8 bg-white">
    <div class="max-w-4xl mx-auto">
      <header class="text-center mb-14">
        <span class="block text-aurea text-xs font-bold tracking-[0.2em] uppercase mb-4">
          Agenda tu visita
        </span>
        <h1 class="font-display text-4xl md:text-5xl font-light tracking-tight text-gray-800">
          Reserva tu cita
        </h1>
        <p class="mt-4 text-gray-500 max-w-xl mx-auto">
          Elige el tratamiento, tu especialista y el horario que mejor te convenga.
        </p>
      </header>

      <LoadingSpinner v-if="loading" label="Cargando disponibilidad..." />

      <EmptyState
        v-else-if="!activeSpecialties.length"
        title="Aún no hay servicios disponibles"
        description="Estamos actualizando nuestra agenda. Vuelve pronto o escríbenos para coordinar tu visita."
      >
        <BaseButton variant="secondary" @click="router.push('/contactos')">Contáctanos</BaseButton>
      </EmptyState>

      <form v-else class="space-y-12" @submit.prevent="handleSubmit">
        <!-- Paso 1 -->
        <fieldset>
          <legend class="flex items-center gap-3 mb-5">
            <span
              class="h-7 w-7 rounded-full bg-aurea text-white text-xs font-bold flex items-center justify-center"
            >
              1
            </span>
            <span class="text-lg font-serif font-light text-gray-800">Elige el tratamiento</span>
          </legend>

          <div class="grid gap-4 sm:grid-cols-2">
            <button
              v-for="specialty in activeSpecialties"
              :key="specialty.specialtyId"
              type="button"
              :class="[
                'text-left border rounded-2xl p-5 transition-all duration-300 cursor-pointer',
                String(form.specialtyId) === String(specialty.specialtyId)
                  ? 'border-aurea bg-aurea-tint shadow-lg'
                  : 'border-gray-100 hover:border-aurea hover:shadow-lg'
              ]"
              @click="selectSpecialty(specialty)"
            >
              <p class="font-semibold text-gray-800">{{ specialty.name }}</p>
              <p v-if="specialty.description" class="mt-1 text-sm text-gray-500 line-clamp-2">
                {{ specialty.description }}
              </p>
            </button>
          </div>
        </fieldset>

        <!-- Paso 2 -->
        <fieldset v-if="form.specialtyId">
          <legend class="flex items-center gap-3 mb-5">
            <span
              class="h-7 w-7 rounded-full bg-aurea text-white text-xs font-bold flex items-center justify-center"
            >
              2
            </span>
            <span class="text-lg font-serif font-light text-gray-800">Elige a tu especialista</span>
          </legend>

          <p v-if="!doctorsForSpecialty.length" class="text-sm text-gray-500">
            No hay especialistas disponibles para {{ selectedSpecialty?.name }} en este momento.
            <RouterLink to="/contactos" class="text-aurea font-bold hover:underline">
              Escríbenos
            </RouterLink>
            y coordinamos tu visita.
          </p>

          <div v-else class="grid gap-4 sm:grid-cols-2">
            <button
              v-for="doctor in doctorsForSpecialty"
              :key="doctor.doctorId"
              type="button"
              :class="[
                'flex items-center gap-4 text-left border rounded-2xl p-5 transition-all duration-300 cursor-pointer',
                String(form.doctorId) === String(doctor.doctorId)
                  ? 'border-aurea bg-aurea-tint shadow-lg'
                  : 'border-gray-100 hover:border-aurea hover:shadow-lg'
              ]"
              @click="form.doctorId = String(doctor.doctorId)"
            >
              <img
                v-if="doctor.photoURL"
                :src="doctor.photoURL"
                :alt="`Foto de ${doctor.user?.name}`"
                class="h-12 w-12 rounded-full object-cover shrink-0"
              />
              <span
                v-else
                class="h-12 w-12 shrink-0 rounded-full bg-white border border-gray-100 text-aurea flex items-center justify-center text-sm font-bold"
                aria-hidden="true"
              >
                {{ (doctor.user?.name?.[0] ?? '') + (doctor.user?.lastName?.[0] ?? '') }}
              </span>
              <span class="min-w-0">
                <span class="block font-semibold text-gray-800 truncate">
                  {{ doctor.user?.name }} {{ doctor.user?.lastName }}
                </span>
                <span class="block text-xs text-gray-500 truncate">
                  {{ doctor.specialty?.name }}
                </span>
              </span>
            </button>
          </div>
        </fieldset>

        <!-- Paso 3 -->
        <fieldset v-if="form.doctorId">
          <legend class="flex items-center gap-3 mb-5">
            <span
              class="h-7 w-7 rounded-full bg-aurea text-white text-xs font-bold flex items-center justify-center"
            >
              3
            </span>
            <span class="text-lg font-serif font-light text-gray-800">Fecha, hora y detalles</span>
          </legend>

          <div class="space-y-5">
            <BaseInput
              v-model="form.scheduled"
              label="Fecha y hora"
              type="datetime-local"
              :min="minDateTime"
              required
            />

            <div v-if="busySlots.length" class="rounded-xl bg-gray-50 border border-gray-100 p-4">
              <p class="text-xs font-bold uppercase tracking-widest text-gray-400 mb-2">
                Horarios ya ocupados de {{ selectedDoctor?.user?.name }}
              </p>
              <ul class="flex flex-wrap gap-2">
                <li
                  v-for="slot in busySlots"
                  :key="slot"
                  class="px-3 py-1 rounded-full bg-white border border-gray-200 text-xs text-gray-500"
                >
                  {{ formatDateTime(slot) }}
                </li>
              </ul>
            </div>

            <BaseTextarea
              v-model="form.notes"
              label="Notas para tu especialista"
              placeholder="Cuéntanos si tienes alguna condición, alergia o preferencia."
              maxlength="500"
            />
          </div>
        </fieldset>

        <div
          v-if="form.doctorId"
          class="border-t border-gray-100 pt-8 flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4"
        >
          <p class="text-sm text-gray-500">
            <span class="font-semibold text-gray-800">{{ selectedSpecialty?.name }}</span>
            con {{ selectedDoctor?.user?.name }} {{ selectedDoctor?.user?.lastName }}
            <template v-if="form.scheduled"> · {{ formatDateTime(form.scheduled) }}</template>
          </p>

          <BaseButton type="submit" :disabled="!canSubmit" :loading="submitting">
            Confirmar reserva
          </BaseButton>
        </div>
      </form>
    </div>
  </section>
</template>
