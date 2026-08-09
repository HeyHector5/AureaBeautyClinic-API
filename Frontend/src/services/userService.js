import { http } from './http'

/**
 * UserDTO: { userId, roleId, name, lastName, email, phoneNumber,
 *            registerDate, isActive, role: RoleDTO, fullName }
 *
 * Todos los endpoints requieren token. GET / y POST / y DELETE son sólo Admin.
 */
export const getUsers = () => http.get('/api/users')

export const getUser = (id) => http.get(`/api/users/${id}`)

/** POST body: { roleId, name, lastName, email, password, phone? } */
export const createUser = (payload) => http.post('/api/users', payload)

/**
 * PUT body parcial: { name?, lastName?, phone?, isActive? }
 * Ojo: el campo se llama `phone` en la petición, pero `phoneNumber` en el DTO.
 * `isActive` sólo lo aplica el backend si quien llama es Admin.
 */
export const updateUser = (id, payload) => http.put(`/api/users/${id}`, payload)

/** Soft delete: marca isActive = false. */
export const deactivateUser = (id) => http.del(`/api/users/${id}`)
