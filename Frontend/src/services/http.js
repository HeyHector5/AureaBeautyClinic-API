/**
 * Cliente HTTP único de la aplicación.
 *
 * El backend siempre responde con el sobre ApiResponse<T>:
 *   { success: boolean, message: string|null, data: T|null, errors: string[]|null }
 *
 * Este wrapper lo desempaqueta y devuelve directamente `data`, o lanza un ApiError.
 * Así las vistas sólo necesitan try/catch.
 */

const BASE_URL = (
  import.meta.env.VITE_API_URL ||
  'https://aureabeautyclinicapi-gwdrcbg5d9e3fxf4.westus3-01.azurewebsites.net'
).replace(/\/+$/, '')

export const TOKEN_KEY = 'token'
export const USER_KEY = 'user'

export class ApiError extends Error {
  constructor(message, { status = 0, errors = null } = {}) {
    super(message)
    this.name = 'ApiError'
    this.status = status
    this.errors = errors
  }

  /** Mensaje listo para mostrar, incluyendo los errores de validación del backend. */
  get detail() {
    if (this.errors?.length) return this.errors.join('\n')
    return this.message
  }
}

/**
 * Se invoca cuando el backend responde 401 (token ausente, inválido o vencido).
 * El router lo registra en main.js para evitar una dependencia circular.
 */
let onUnauthorized = null
export const setUnauthorizedHandler = (handler) => {
  onUnauthorized = handler
}

const buildHeaders = (hasBody) => {
  const headers = {}
  if (hasBody) headers['Content-Type'] = 'application/json'

  const token = localStorage.getItem(TOKEN_KEY)
  if (token) headers.Authorization = `Bearer ${token}`

  return headers
}

const request = async (method, path, body) => {
  const hasBody = body !== undefined && body !== null

  let res
  try {
    res = await fetch(`${BASE_URL}${path}`, {
      method,
      headers: buildHeaders(hasBody),
      body: hasBody ? JSON.stringify(body) : undefined
    })
  } catch (networkError) {
    // El fetch falló antes de recibir respuesta: backend apagado, puerto
    // equivocado, certificado no confiado o CORS. El detalle sólo está aquí.
    console.error(`[API] No se pudo alcanzar ${method} ${BASE_URL}${path}`, networkError)
    throw new ApiError('No pudimos conectar con el servidor. Verifica tu conexión e intenta de nuevo.')
  }

  if (res.status === 401) {
    localStorage.removeItem(TOKEN_KEY)
    localStorage.removeItem(USER_KEY)
    onUnauthorized?.()
    throw new ApiError('Tu sesión expiró. Vuelve a iniciar sesión.', { status: 401 })
  }

  if (res.status === 403) {
    throw new ApiError('No tienes permisos para realizar esta acción.', { status: 403 })
  }

  // 204 y respuestas sin cuerpo.
  if (res.status === 204) return null

  let payload = null
  try {
    payload = await res.json()
  } catch {
    payload = null
  }

  if (!res.ok || payload?.success === false) {
    throw new ApiError(
      payload?.message || `La solicitud falló (HTTP ${res.status}).`,
      { status: res.status, errors: payload?.errors ?? null }
    )
  }

  // Si por alguna razón la respuesta no viene envuelta, se devuelve tal cual.
  return payload && 'data' in payload ? payload.data : payload
}

export const http = {
  get: (path) => request('GET', path),
  post: (path, body) => request('POST', path, body),
  put: (path, body) => request('PUT', path, body),
  del: (path) => request('DELETE', path)
}

export { BASE_URL }
