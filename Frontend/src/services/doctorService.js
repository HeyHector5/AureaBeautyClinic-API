import { http } from './http'

/**
 * DoctorDTO: { doctorId, userId, specialtyId, licenseNumber, biography,
 *              photoURL, isActive, user: UserDTO, specialty: SpecialtyDTO }
 * Ojo con el casing: es `photoURL`, no `photoUrl`.
 */
export const getDoctors = () => http.get('/api/doctors')

export const getDoctor = (id) => http.get(`/api/doctors/${id}`)

/** body: { userId, specialtyId, licenseNumber?, biography?, photoURL?, isActive } */
export const createDoctor = (payload) => http.post('/api/doctors', payload)

/** body SIN userId: el backend conserva el usuario asociado. */
export const updateDoctor = (id, payload) => http.put(`/api/doctors/${id}`, payload)

/** Soft delete: marca isActive = false. */
export const deactivateDoctor = (id) => http.del(`/api/doctors/${id}`)
