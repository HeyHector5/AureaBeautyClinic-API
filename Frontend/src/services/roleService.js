import { http } from './http'

/** RoleDTO: { roleId, name, description, isActive } */
export const getRoles = () => http.get('/api/roles')

export const getRole = (id) => http.get(`/api/roles/${id}`)

/** body: { name, description?, isActive } */
export const createRole = (payload) => http.post('/api/roles', payload)

export const updateRole = (id, payload) => http.put(`/api/roles/${id}`, payload)

/** Soft delete: marca isActive = false. */
export const deactivateRole = (id) => http.del(`/api/roles/${id}`)
