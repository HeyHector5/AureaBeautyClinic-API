import { createRouter, createWebHistory } from 'vue-router'
import { useAuth } from '@/composables/useAuth'
import { notifyWarning } from '@/utils/notify'

const routes = [
  // ── Sitio público ────────────────────────────────────────
  {
    path: '/',
    component: () => import('@/layouts/PublicLayout.vue'),
    children: [
      { path: '', name: 'Home', component: () => import('@/components/HomePage.vue') },
      { path: 'nosotros', name: 'Nosotros', component: () => import('@/components/NosotrosSection.vue') },
      { path: 'servicios', name: 'Servicios', component: () => import('@/components/Service.vue') },
      { path: 'contactos', name: 'Contacto', component: () => import('@/components/Contact.vue') },
      { path: 'login', name: 'Login', component: () => import('@/components/Login.vue'), meta: { guestOnly: true } },
      { path: 'register', name: 'Register', component: () => import('@/components/Register.vue'), meta: { guestOnly: true } },
      {
        path: 'reservar',
        name: 'Reservar',
        component: () => import('@/views/BookingView.vue'),
        meta: { requiresAuth: true }
      },
      { path: ':pathMatch(.*)*', name: 'NotFound', component: () => import('@/views/NotFound.vue') }
    ]
  },

  // ── Área del cliente ─────────────────────────────────────
  {
    path: '/',
    component: () => import('@/layouts/ClientLayout.vue'),
    meta: { requiresAuth: true },
    children: [
      { path: 'dashboard', name: 'Dashboard', component: () => import('@/views/client/ClientDashboard.vue') },
      { path: 'mis-reservas', name: 'MisReservas', component: () => import('@/views/client/MyAppointmentsView.vue') },
      { path: 'historial', name: 'Historial', component: () => import('@/views/client/HistoryView.vue') },
      { path: 'perfil', name: 'Perfil', component: () => import('@/views/client/ProfileView.vue') }
    ]
  },

  // ── Panel administrativo ─────────────────────────────────
  {
    path: '/admin',
    component: () => import('@/layouts/AdminLayout.vue'),
    meta: { requiresAuth: true, roles: ['Admin'] },
    children: [
      { path: '', name: 'Admin', component: () => import('@/views/admin/AdminDashboard.vue') },
      { path: 'citas', name: 'AdminCitas', component: () => import('@/views/admin/AppointmentsView.vue') },
      { path: 'usuarios', name: 'AdminUsuarios', component: () => import('@/views/admin/UsersView.vue') },
      { path: 'doctores', name: 'AdminDoctores', component: () => import('@/views/admin/DoctorsView.vue') },
      { path: 'especialidades', name: 'AdminEspecialidades', component: () => import('@/views/admin/SpecialtiesView.vue') },
      { path: 'roles', name: 'AdminRoles', component: () => import('@/views/admin/RolesView.vue') }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior: (to, from, saved) => saved ?? { top: 0 }
})

/** Página de aterrizaje según el rol tras iniciar sesión. */
export const homeForRole = (role) => (role === 'Admin' ? '/admin' : '/dashboard')

router.beforeEach((to) => {
  const { isLoggedIn, user } = useAuth()

  const requiresAuth = to.matched.some((r) => r.meta.requiresAuth)
  const allowedRoles = to.matched.find((r) => r.meta.roles)?.meta.roles

  if (requiresAuth && !isLoggedIn.value) {
    return { path: '/login', query: { redirect: to.fullPath } }
  }

  if (allowedRoles && !allowedRoles.includes(user.value?.role)) {
    notifyWarning('Acceso restringido', 'No tienes permisos para entrar a esta sección.')
    return { path: homeForRole(user.value?.role) }
  }

  // Un usuario con sesión no debería volver a login/register.
  if (to.meta.guestOnly && isLoggedIn.value) {
    return { path: homeForRole(user.value?.role) }
  }

  return true
})

export default router
