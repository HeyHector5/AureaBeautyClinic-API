import { computed, ref } from 'vue'
import { TOKEN_KEY, USER_KEY } from '@/services/http'

/**
 * Estado de sesión compartido por toda la app.
 *
 * Los refs viven a nivel de módulo, así que todos los componentes que llamen a
 * useAuth() comparten la misma instancia (antes cada componente releía
 * localStorage por su cuenta en cada cambio de ruta).
 */

const EXPIRES_KEY = 'tokenExpiresAt'

const token = ref(null)
const user = ref(null)

/** El backend anida el rol como user.role.name; lo aplanamos a string. */
const normalizeUser = (raw) => {
  if (!raw) return null
  return {
    ...raw,
    role: raw.role?.name ?? raw.role ?? 'Patient'
  }
}

const isExpired = () => {
  const expiresAt = localStorage.getItem(EXPIRES_KEY)
  if (!expiresAt) return false
  return new Date(expiresAt).getTime() <= Date.now()
}

const clearStorage = () => {
  localStorage.removeItem(TOKEN_KEY)
  localStorage.removeItem(USER_KEY)
  localStorage.removeItem(EXPIRES_KEY)
}

/** Lee localStorage y sincroniza el estado. Se llama al arrancar la app. */
const hydrate = () => {
  const storedToken = localStorage.getItem(TOKEN_KEY)
  const storedUser = localStorage.getItem(USER_KEY)

  if (!storedToken || isExpired()) {
    clearStorage()
    token.value = null
    user.value = null
    return
  }

  token.value = storedToken
  try {
    user.value = storedUser ? JSON.parse(storedUser) : null
  } catch {
    user.value = null
  }
}

/** Persiste la respuesta de /api/auth/login o /api/auth/register. */
const setSession = (authResponse) => {
  const normalized = normalizeUser(authResponse.user)

  localStorage.setItem(TOKEN_KEY, authResponse.token)
  localStorage.setItem(USER_KEY, JSON.stringify(normalized))
  if (authResponse.expiresAt) {
    localStorage.setItem(EXPIRES_KEY, authResponse.expiresAt)
  }

  token.value = authResponse.token
  user.value = normalized
}

const logout = () => {
  clearStorage()
  token.value = null
  user.value = null
}

/** Refresca los datos del usuario en sesión sin tocar el token (p. ej. tras editar el perfil). */
const patchUser = (partial) => {
  if (!user.value) return
  const merged = { ...user.value, ...normalizeUser(partial) }
  user.value = merged
  localStorage.setItem(USER_KEY, JSON.stringify(merged))
}

hydrate()

// Mantiene la sesión sincronizada entre pestañas.
if (typeof window !== 'undefined') {
  window.addEventListener('storage', (event) => {
    if (event.key === TOKEN_KEY || event.key === USER_KEY) hydrate()
  })
}

export function useAuth() {
  return {
    token,
    user,
    isLoggedIn: computed(() => !!token.value),
    isAdmin: computed(() => user.value?.role === 'Admin'),
    isDoctor: computed(() => user.value?.role === 'Doctor'),
    userId: computed(() => user.value?.userId ?? null),
    fullName: computed(() =>
      user.value ? `${user.value.name ?? ''} ${user.value.lastName ?? ''}`.trim() : ''
    ),
    initials: computed(() => {
      const a = user.value?.name?.[0] ?? ''
      const b = user.value?.lastName?.[0] ?? ''
      return (a + b).toUpperCase() || 'U'
    }),
    setSession,
    logout,
    hydrate,
    patchUser
  }
}
