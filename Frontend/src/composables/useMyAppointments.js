import { computed, ref } from 'vue'
import { cancelAppointment, getAppointmentsByUser } from '@/services/appointmentService'
import { useAuth } from '@/composables/useAuth'
import { isOpenAppointment } from '@/constants/appointment'
import { confirmAction, notifyApiError, toast } from '@/utils/notify'

/**
 * Citas del usuario en sesión, separadas en próximas e históricas.
 * Lo usan el dashboard del cliente, "Mis Reservas" e "Historial".
 */
export function useMyAppointments() {
  const { userId } = useAuth()

  const appointments = ref([])
  const loading = ref(false)

  const load = async () => {
    if (!userId.value) return
    loading.value = true
    try {
      appointments.value = (await getAppointmentsByUser(userId.value)) ?? []
    } catch (e) {
      appointments.value = []
      notifyApiError(e, 'No pudimos cargar tus citas')
    } finally {
      loading.value = false
    }
  }

  const byDateAsc = (a, b) => new Date(a.scheduled) - new Date(b.scheduled)
  const byDateDesc = (a, b) => new Date(b.scheduled) - new Date(a.scheduled)

  /** Pendientes o confirmadas y todavía en el futuro. */
  const upcoming = computed(() =>
    appointments.value
      .filter((a) => isOpenAppointment(a.state) && new Date(a.scheduled).getTime() >= Date.now())
      .sort(byDateAsc)
  )

  /** Canceladas, completadas o ya pasadas. */
  const past = computed(() =>
    appointments.value
      .filter((a) => !isOpenAppointment(a.state) || new Date(a.scheduled).getTime() < Date.now())
      .sort(byDateDesc)
  )

  const nextAppointment = computed(() => upcoming.value[0] ?? null)

  const cancel = async (appointment) => {
    const confirmed = await confirmAction({
      title: '¿Cancelar tu cita?',
      text: 'Si cambias de opinión tendrás que reservar de nuevo según la disponibilidad.',
      confirmText: 'Sí, cancelar'
    })
    if (!confirmed) return

    try {
      await cancelAppointment(appointment.appointmentId)
      toast('Tu cita fue cancelada.')
      await load()
    } catch (e) {
      notifyApiError(e, 'No pudimos cancelar la cita')
    }
  }

  return { appointments, loading, load, upcoming, past, nextAppointment, cancel }
}
