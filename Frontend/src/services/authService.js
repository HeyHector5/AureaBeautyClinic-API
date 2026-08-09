import { http } from './http'

/**
 * POST /api/auth/login → AuthResponse { token, expiresAt, user }
 */
export const login = (email, password) =>
  http.post('/api/auth/login', { email, password })

/**
 * POST /api/auth/register → AuthResponse
 *
 * El rol no se envía: el servidor asigna siempre "Patient", resolviéndolo por
 * nombre. Las cuentas de Admin/Doctor se crean desde el panel con POST /api/users.
 */
export const register = (name, lastName, email, password, phone) =>
  http.post('/api/auth/register', { name, lastName, email, password, phone })
