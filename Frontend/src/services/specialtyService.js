import { http } from './http'

/** SpecialtyDTO: { specialtyId, name, description, isActive } */
export const getSpecialties = () => http.get('/api/specialties')

export const getSpecialty = (id) => http.get(`/api/specialties/${id}`)

/** body: { name, description?, isActive } */
export const createSpecialty = (payload) => http.post('/api/specialties', payload)

export const updateSpecialty = (id, payload) => http.put(`/api/specialties/${id}`, payload)

/** Soft delete: marca isActive = false. */
export const deactivateSpecialty = (id) => http.del(`/api/specialties/${id}`)
