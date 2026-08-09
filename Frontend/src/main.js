import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router/index.js'
import { setUnauthorizedHandler } from './services/http'
import { useAuth } from './composables/useAuth'

// Si el backend responde 401, la capa HTTP ya limpió el almacenamiento;
// aquí sincronizamos el estado en memoria y mandamos al login.
setUnauthorizedHandler(() => {
  useAuth().logout()
  const redirect = router.currentRoute.value.fullPath
  router.push({ path: '/login', query: redirect === '/login' ? undefined : { redirect } })
})

createApp(App).use(router).mount('#app')
