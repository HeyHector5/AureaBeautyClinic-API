import { http } from './http'

/**
 * AppointmentDTO: { appointmentId, userId, doctorId, scheduled, state,
 *                   notes, created, user: UserDTO, doctor: DoctorDTO }
 *
 * ⚠ El prefijo de este controlador es SINGULAR: /api/appointment
 * (el resto de recursos son plurales).
 */
const BASE = '/api/appointment'

/** Sólo Admin. */
export const getAppointments = () => http.get(BASE)

export const getAppointment = (id) => http.get(`${BASE}/${id}`)

export const getAppointmentsByUser = (userId) => http.get(`${BASE}/user/${userId}`)

export const getAppointmentsByDoctor = (doctorId) => http.get(`${BASE}/doctor/${doctorId}`)

/**
 * body: { userId, doctorId, scheduled, notes? }
 * El backend fuerza state = "Pending" y created = ahora.
 */
export const createAppointment = (payload) => http.post(BASE, payload)

/**
 * body: { scheduled, state, notes? } — sólo Admin.
 * userId y doctorId no se pueden cambiar a través de la API.
 */
export const updateAppointment = (id, payload) => http.put(`${BASE}/${id}`, payload)

/** Soft delete: la cita pasa a estado "Cancelled". */
export const cancelAppointment = (id) => http.del(`${BASE}/${id}`)
