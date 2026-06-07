const USE_MOCK = false
const BASE_URL = 'https://aureabeautyclinicapi-gwdrcbg5d9e3fxf4.westus3-01.azurewebsites.net'

// ── MOCK LOGIN ──────────────────────────────────────────
const MOCK_USERS = [
  { id: 1, email: 'admin@aurea.com', password: 'Admin1234', name: 'Admin', lastName: 'Aurea', role: 'Admin' },
  { id: 2, email: 'cliente@aurea.com', password: 'Cliente1234', name: 'María', lastName: 'López', role: 'Client' }
]

const mockLogin = async (email, password) => {
  await new Promise(res => setTimeout(res, 500))
  const user = MOCK_USERS.find(u => u.email === email)
  if (!user || user.password !== password) {
    return { success: false, message: 'Invalid email or password.', data: null }
  }
  return {
    success: true,
    data: { token: `mock.token.${btoa(user.email)}`, user }
  }
}

const realLogin = async (email, password) => {
  console.log('Llamando API...') // ← agrega
  try {
    const res = await fetch(`${BASE_URL}/api/auth/login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, password })
    })
    const data = await res.json()
    console.log('Respuesta API:', data) // ← agrega
    return data
  } catch (err) {
    console.log('CORS o red:', err) // ← agrega
    throw err
  }
}

export const login = (email, password) =>
  USE_MOCK ? mockLogin(email, password) : realLogin(email, password)


// ── MOCK REGISTER ───────────────────────────────────────
const mockRegister = async (name, lastName, email, password) => {
  await new Promise(res => setTimeout(res, 500))
  const exists = MOCK_USERS.find(u => u.email === email)
  if (exists) return { success: false, message: 'Este correo ya está registrado.' }
  return { success: true, message: 'Registro exitoso.', data: null }
}

const realRegister = async (name, lastName, email, password, phone) => {
  const res = await fetch(`${BASE_URL}/api/auth/register`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      roleId: 1,       // Usuario normal por defecto
      name,
      lastName,
      email,
      password,
      phone
    })
  })
  return res.json()
}

export const register = (name, lastName, email, password, phone) =>
  USE_MOCK ? mockRegister(name, lastName, email, password) : realRegister(name, lastName, email, password, phone)