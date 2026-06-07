import { createRouter, createWebHistory } from 'vue-router'

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
    component: () => import('@/components/HeroSection.vue') // temporal
  },
  {
    path: '/nosotros',
    name: 'Nosotros',
    component: () => import('@/components/NosotrosSection.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router