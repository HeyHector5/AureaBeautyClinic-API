import { ref, watch } from 'vue'

const STORAGE_KEY = 'aurea_servicios'

export const CATEGORIAS = [
  'Medicina Facial',
  'Tratamientos Corporales',
  'Dermatología',
  'Cuidado Capilar',
]

const serviciosPorDefecto = [
  {
    id: 1,
    nombre: 'Limpieza facial profunda',
    descripcion: 'Exfoliación, extracción e hidratación para dejar tu piel visiblemente renovada.',
    precio: 'RD$2,500',
    icono: '🧖‍♀️',
    categoria: 'Medicina Facial',
    activo: true,
  },
  {
    id: 2,
    nombre: 'Microneedling',
    descripcion: 'Estimula colágeno natural para reducir cicatrices, líneas finas y textura irregular.',
    precio: 'RD$4,800',
    icono: '💉',
    categoria: 'Dermatología',
    activo: true,
  },
  {
    id: 3,
    nombre: 'Masaje relajante corporal',
    descripcion: 'Técnicas de liberación muscular y aceites esenciales para aliviar tensión y estrés.',
    precio: 'RD$3,200',
    icono: '💆‍♀️',
    categoria: 'Tratamientos Corporales',
    activo: true,
  },
  {
    id: 4,
    nombre: 'Depilación láser',
    descripcion: 'Reducción progresiva y permanente del vello con tecnología de última generación.',
    precio: 'RD$1,800',
    icono: '✨',
    categoria: 'Tratamientos Corporales',
    activo: true,
  },
  {
    id: 5,
    nombre: 'Peeling químico',
    descripcion: 'Renueva capas superficiales de la piel para un tono más uniforme y luminoso.',
    precio: 'RD$3,600',
    icono: '🌿',
    categoria: 'Dermatología',
    activo: true,
  },
  {
    id: 6,
    nombre: 'Consulta de valoración',
    descripcion: 'Evaluación personalizada con nuestros especialistas para diseñar tu plan de tratamiento.',
    precio: 'RD$800',
    icono: '📋',
    categoria: 'Medicina Facial',
    activo: true,
  },
]

function cargarServicios() {
  try {
    const raw = localStorage.getItem(STORAGE_KEY)
    if (raw) {
      const guardados = JSON.parse(raw)
      // Compatibilidad con datos guardados antes de agregar categorías
      return guardados.map(s => ({ categoria: CATEGORIAS[0], ...s }))
    }
  } catch (e) {
    console.error('No se pudieron leer los servicios guardados, se usan los de por defecto.', e)
  }
  return JSON.parse(JSON.stringify(serviciosPorDefecto))
}

export const servicios = ref(cargarServicios())

watch(
  servicios,
  (valor) => localStorage.setItem(STORAGE_KEY, JSON.stringify(valor)),
  { deep: true }
)

let nextId = servicios.value.reduce((max, s) => Math.max(max, s.id), 0) + 1

export function agregarServicio({ nombre, descripcion, precio, icono, categoria }) {
  servicios.value.push({
    id: nextId++,
    nombre: nombre.trim(),
    descripcion: (descripcion || '').trim(),
    precio: precio.trim(),
    icono: (icono || '💆').trim(),
    categoria: categoria || CATEGORIAS[0],
    activo: true,
  })
}

export function actualizarServicio(id, cambios) {
  const servicio = servicios.value.find(s => s.id === id)
  if (servicio) Object.assign(servicio, cambios)
}

export function eliminarServicio(id) {
  const index = servicios.value.findIndex(s => s.id === id)
  if (index !== -1) servicios.value.splice(index, 1)
}

export function alternarActivo(id) {
  const servicio = servicios.value.find(s => s.id === id)
  if (servicio) servicio.activo = !servicio.activo
}
