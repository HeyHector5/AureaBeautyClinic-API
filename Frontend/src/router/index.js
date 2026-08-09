import { createRouter, createWebHistory } from 'vue-router'
import Swal from 'sweetalert2'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('@/components/HomePage.vue') // ← cambiado
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/components/Login.vue')
  },
  {
    path: '/register',
    name: 'Register',
    component: () => import('@/components/Register.vue')
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: () => import('@/components/HeroSection.vue') // temporal
  },
  {
    path: '/admin',
    name: 'Admin',
    component: () => import('@/components/AdminServices.vue'),
    meta: { requiresAuth: true, requiresAdmin: true }
  },
  {
    path: '/nosotros',
    name: 'Nosotros',
    component: () => import('@/components/NosotrosSection.vue')
  },
  {
    path: '/servicios',
    name: 'Servicios',
    component: () => import('@/components/Service.vue')
  },
  {
    path: '/reservar',
    name: 'Reservar',
    component: () => import('@/components/Booking.vue'),
    meta: { requiresAuth: true, blockAdmin: true }
  },
  {
    path: '/contactos',
    name: 'Contacto',
    component: () => import('@/components/Contact.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// Protege rutas que requieren sesión iniciada (ej: reservar cita) o rol de administrador
router.beforeEach((to) => {
  const isLoggedIn = !!localStorage.getItem('token')

  if (to.meta.requiresAuth && !isLoggedIn) {
    return { path: '/login', query: { redirect: to.fullPath } }
  }

  const user = JSON.parse(localStorage.getItem('user') || '{}')

  if (to.meta.requiresAdmin && user.role !== 'Admin') {
    return { path: '/' }
  }

  if (to.meta.blockAdmin && user.role === 'Admin') {
    Swal.fire({
      icon: 'info',
      title: 'Acción no disponible',
      text: 'Las cuentas de administrador no pueden reservar citas.',
      confirmButtonColor: '#FF3B30',
    })
    return { path: '/admin' }
  }
})

export default router