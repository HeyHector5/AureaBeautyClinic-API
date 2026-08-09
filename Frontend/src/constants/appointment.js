/**
 * Estados de cita. Los valores son EXACTAMENTE los que valida el backend
 * (AureaBeautyClinic.Shared/Constants/AppointmentStatus.cs) y distinguen mayúsculas.
 */

export const APPOINTMENT_STATES = {
  PENDING: 'Pending',
  CONFIRMED: 'Confirmed',
  CANCELLED: 'Cancelled',
  COMPLETED: 'Completed'
}

export const APPOINTMENT_STATE_META = {
  Pending: {
    label: 'Pendiente',
    badge: 'bg-amber-50 text-amber-700 border-amber-200'
  },
  Confirmed: {
    label: 'Confirmada',
    badge: 'bg-emerald-50 text-emerald-700 border-emerald-200'
  },
  Cancelled: {
    label: 'Cancelada',
    badge: 'bg-gray-100 text-gray-600 border-gray-200'
  },
  Completed: {
    label: 'Completada',
    badge: 'bg-sky-50 text-sky-700 border-sky-200'
  }
}

/** Opciones listas para un <select>. */
export const APPOINTMENT_STATE_OPTIONS = Object.entries(APPOINTMENT_STATE_META).map(
  ([value, meta]) => ({ value, label: meta.label })
)

export const stateLabel = (state) => APPOINTMENT_STATE_META[state]?.label ?? state

export const stateBadgeClass = (state) =>
  APPOINTMENT_STATE_META[state]?.badge ?? 'bg-gray-100 text-gray-600 border-gray-200'

/** Una cita sigue "viva" mientras no esté cancelada ni completada. */
export const isOpenAppointment = (state) =>
  state === APPOINTMENT_STATES.PENDING || state === APPOINTMENT_STATES.CONFIRMED
