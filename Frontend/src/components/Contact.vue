<script setup>
import { computed, onMounted, reactive, ref } from 'vue'
import { getSpecialties } from '@/services/specialtyService'
import { CLINIC } from '@/constants/clinic'

/**
 * No existe un endpoint de contacto en la API. En vez de simular un envío que
 * nunca ocurre, el formulario compone un correo (o un mensaje de WhatsApp) real
 * con los datos que la persona escribió.
 */

const formulario = reactive({
  nombre: '',
  telefono: '',
  correo: '',
  servicio: '',
  mensaje: ''
})

const especialidades = ref([])

const cuerpoMensaje = computed(() =>
  [
    `Nombre: ${formulario.nombre}`,
    formulario.telefono ? `Teléfono: ${formulario.telefono}` : null,
    `Correo: ${formulario.correo}`,
    formulario.servicio ? `Servicio de interés: ${formulario.servicio}` : null,
    '',
    formulario.mensaje || '(Sin mensaje adicional)'
  ]
    .filter((line) => line !== null)
    .join('\n')
)

const mailtoHref = computed(() => {
  const asunto = formulario.servicio
    ? `Consulta sobre ${formulario.servicio}`
    : 'Consulta desde el sitio web'
  return `mailto:${CLINIC.email}?subject=${encodeURIComponent(asunto)}&body=${encodeURIComponent(cuerpoMensaje.value)}`
})

const whatsappHref = computed(
  () => `https://wa.me/${CLINIC.whatsapp}?text=${encodeURIComponent(cuerpoMensaje.value)}`
)

const enviarPorCorreo = () => {
  window.location.href = mailtoHref.value
}

onMounted(async () => {
  try {
    const data = (await getSpecialties()) ?? []
    especialidades.value = data.filter((s) => s.isActive)
  } catch {
    // Si la API no responde, el desplegable queda con la opción genérica.
    especialidades.value = []
  }
})
</script>

<template>
  <section id="contacto" class="relative overflow-hidden bg-white py-24">
    <!-- Formas orgánicas decorativas (referencia a las formas suaves del spa) -->
    <div
      class="pointer-events-none absolute -right-32 -top-24 h-[520px] w-[520px] rounded-full bg-rose-50"
      aria-hidden="true"
    ></div>
    <div
      class="pointer-events-none absolute -left-40 bottom-0 h-72 w-72 rounded-full bg-rose-50/70"
      aria-hidden="true"
    ></div>

    <div class="relative mx-auto grid max-w-6xl grid-cols-1 gap-16 px-6 lg:grid-cols-2 lg:px-8">
      <!-- Columna izquierda: información -->
      <div class="flex flex-col justify-center">
        <span class="font-body text-sm font-semibold uppercase tracking-[0.2em] text-aurea">
          Contacto
        </span>
        <h1 class="mt-3 font-display text-4xl leading-tight text-gray-800 md:text-5xl">
          Hablemos de <span class="italic text-aurea">tu piel</span>
        </h1>
        <p class="mt-5 max-w-md font-body text-gray-500">
          Escríbenos, llámanos o visítanos. Nuestro equipo te ayudará a encontrar
          el tratamiento perfecto para ti.
        </p>

        <ul class="mt-10 space-y-6">
          <li class="flex items-start gap-4">
            <span class="mt-1 h-2.5 w-2.5 shrink-0 rounded-full bg-aurea"></span>
            <div>
              <p class="font-body text-sm font-semibold text-gray-800">Dirección</p>
              <p class="font-body text-gray-500">{{ CLINIC.address }}</p>
            </div>
          </li>
          <li class="flex items-start gap-4">
            <span class="mt-1 h-2.5 w-2.5 shrink-0 rounded-full bg-aurea"></span>
            <div>
              <p class="font-body text-sm font-semibold text-gray-800">Teléfono</p>
              <a :href="CLINIC.phoneHref" class="font-body text-gray-500 hover:text-aurea transition">
                {{ CLINIC.phone }}
              </a>
            </div>
          </li>
          <li class="flex items-start gap-4">
            <span class="mt-1 h-2.5 w-2.5 shrink-0 rounded-full bg-aurea"></span>
            <div>
              <p class="font-body text-sm font-semibold text-gray-800">Correo</p>
              <a
                :href="`mailto:${CLINIC.email}`"
                class="font-body text-gray-500 hover:text-aurea transition"
              >
                {{ CLINIC.email }}
              </a>
            </div>
          </li>
          <li class="flex items-start gap-4">
            <span class="mt-1 h-2.5 w-2.5 shrink-0 rounded-full bg-aurea"></span>
            <div>
              <p class="font-body text-sm font-semibold text-gray-800">Horario</p>
              <p v-for="slot in CLINIC.hours" :key="slot.days" class="font-body text-gray-500">
                {{ slot.days }}: {{ slot.time }}
              </p>
            </div>
          </li>
        </ul>

        <div class="mt-10 flex gap-3">
          <a
            :href="CLINIC.social.instagram"
            target="_blank"
            rel="noopener noreferrer"
            aria-label="Instagram"
            class="flex h-11 w-11 items-center justify-center rounded-full border border-gray-200 text-gray-500 transition hover:border-aurea hover:bg-aurea-tint hover:text-aurea"
          >
            <svg viewBox="0 0 24 24" fill="none" class="h-5 w-5" aria-hidden="true">
              <rect x="3" y="3" width="18" height="18" rx="5" stroke="currentColor" stroke-width="1.6" />
              <circle cx="12" cy="12" r="4.2" stroke="currentColor" stroke-width="1.6" />
              <circle cx="17.2" cy="6.8" r="1.1" fill="currentColor" />
            </svg>
          </a>

          <a
            :href="CLINIC.social.facebook"
            target="_blank"
            rel="noopener noreferrer"
            aria-label="Facebook"
            class="flex h-11 w-11 items-center justify-center rounded-full border border-gray-200 text-gray-500 transition hover:border-aurea hover:bg-aurea-tint hover:text-aurea"
          >
            <svg viewBox="0 0 24 24" fill="none" class="h-5 w-5" aria-hidden="true">
              <path
                d="M14.5 8.5h2V5.6h-2.3c-2.3 0-3.7 1.4-3.7 3.7v1.9H8.6v3h1.9V19h3v-5.8h2.2l.4-3h-2.6V9.6c0-.7.3-1.1 1-1.1Z"
                fill="currentColor"
              />
            </svg>
          </a>

          <a
            :href="`https://wa.me/${CLINIC.whatsapp}`"
            target="_blank"
            rel="noopener noreferrer"
            aria-label="WhatsApp"
            class="flex h-11 w-11 items-center justify-center rounded-full border border-gray-200 text-gray-500 transition hover:border-aurea hover:bg-aurea-tint hover:text-aurea"
          >
            <svg viewBox="0 0 24 24" fill="none" class="h-5 w-5" aria-hidden="true">
              <path
                d="M12 4.2a7.6 7.6 0 0 0-6.5 11.5L4.5 19.8l4.2-1.1A7.6 7.6 0 1 0 12 4.2Z"
                stroke="currentColor"
                stroke-width="1.6"
                stroke-linejoin="round"
              />
              <path
                d="M9.3 9.4c.2-.5.4-.5.7-.5h.5c.2 0 .4 0 .5.4.2.5.6 1.5.6 1.6.1.1.1.3 0 .4-.1.2-.1.3-.3.5-.1.2-.3.3-.1.6.2.3.8 1.2 1.7 1.9 1.1.9 1.3.6 1.7.5.2 0 .3-.2.5-.4.1-.2.3-.2.5-.1.2.1 1.4.7 1.6.8.2.1.3.1.4.2.1.2.1.9-.2 1.3-.3.5-1.3 1-2 .9-.5-.1-1.9-.5-3.2-1.7-1.5-1.4-2.3-2.9-2.5-3.3-.2-.4-.9-1.6-.9-2.6 0-.9.4-1.3.6-1.5Z"
                fill="currentColor"
              />
            </svg>
          </a>
        </div>
      </div>

      <!-- Columna derecha: formulario -->
      <div class="relative">
        <div
          class="rounded-3xl border border-rose-100 bg-white p-8 shadow-[0_20px_60px_-15px_rgba(224,62,54,0.15)] md:p-10"
        >
          <form class="space-y-6" @submit.prevent="enviarPorCorreo">
            <div class="grid grid-cols-1 gap-6 sm:grid-cols-2">
              <div class="relative">
                <input
                  id="nombre"
                  v-model="formulario.nombre"
                  type="text"
                  required
                  placeholder=" "
                  class="peer w-full border-b border-gray-300 bg-transparent px-1 pb-2 pt-4 font-body text-gray-800 outline-none focus:border-aurea"
                />
                <label
                  for="nombre"
                  class="pointer-events-none absolute left-1 top-4 font-body text-gray-400 transition-all peer-focus:top-0 peer-focus:text-xs peer-focus:text-aurea peer-[:not(:placeholder-shown)]:top-0 peer-[:not(:placeholder-shown)]:text-xs"
                >
                  Nombre
                </label>
              </div>

              <div class="relative">
                <input
                  id="telefono"
                  v-model="formulario.telefono"
                  type="tel"
                  placeholder=" "
                  class="peer w-full border-b border-gray-300 bg-transparent px-1 pb-2 pt-4 font-body text-gray-800 outline-none focus:border-aurea"
                />
                <label
                  for="telefono"
                  class="pointer-events-none absolute left-1 top-4 font-body text-gray-400 transition-all peer-focus:top-0 peer-focus:text-xs peer-focus:text-aurea peer-[:not(:placeholder-shown)]:top-0 peer-[:not(:placeholder-shown)]:text-xs"
                >
                  Teléfono
                </label>
              </div>
            </div>

            <div class="relative">
              <input
                id="correo"
                v-model="formulario.correo"
                type="email"
                required
                placeholder=" "
                class="peer w-full border-b border-gray-300 bg-transparent px-1 pb-2 pt-4 font-body text-gray-800 outline-none focus:border-aurea"
              />
              <label
                for="correo"
                class="pointer-events-none absolute left-1 top-4 font-body text-gray-400 transition-all peer-focus:top-0 peer-focus:text-xs peer-focus:text-aurea peer-[:not(:placeholder-shown)]:top-0 peer-[:not(:placeholder-shown)]:text-xs"
              >
                Correo electrónico
              </label>
            </div>

            <div class="relative">
              <select
                id="servicio"
                v-model="formulario.servicio"
                class="peer w-full border-b border-gray-300 bg-transparent px-1 pb-2 pt-4 font-body text-gray-800 outline-none focus:border-aurea"
              >
                <option value="">Sin preferencia</option>
                <option
                  v-for="especialidad in especialidades"
                  :key="especialidad.specialtyId"
                  :value="especialidad.name"
                >
                  {{ especialidad.name }}
                </option>
              </select>
              <label for="servicio" class="pointer-events-none absolute left-1 top-0 font-body text-xs text-aurea">
                Servicio de interés
              </label>
            </div>

            <div class="relative">
              <textarea
                id="mensaje"
                v-model="formulario.mensaje"
                rows="3"
                placeholder=" "
                class="peer w-full resize-none border-b border-gray-300 bg-transparent px-1 pb-2 pt-4 font-body text-gray-800 outline-none focus:border-aurea"
              ></textarea>
              <label
                for="mensaje"
                class="pointer-events-none absolute left-1 top-4 font-body text-gray-400 transition-all peer-focus:top-0 peer-focus:text-xs peer-focus:text-aurea peer-[:not(:placeholder-shown)]:top-0 peer-[:not(:placeholder-shown)]:text-xs"
              >
                Cuéntanos qué buscas
              </label>
            </div>

            <button
              type="submit"
              class="w-full rounded-full bg-aurea py-3.5 font-body text-sm font-semibold uppercase tracking-wide text-white transition hover:bg-aurea-dark cursor-pointer"
            >
              Enviar por correo
            </button>

            <a
              :href="whatsappHref"
              target="_blank"
              rel="noopener noreferrer"
              class="block w-full rounded-full border border-gray-200 py-3.5 text-center font-body text-sm font-semibold uppercase tracking-wide text-gray-600 transition hover:border-aurea hover:text-aurea"
            >
              Escribir por WhatsApp
            </a>

            <p class="text-center font-body text-xs text-gray-400">
              Al enviar se abrirá tu aplicación de correo o WhatsApp con el mensaje ya redactado.
              ¿Prefieres reservar directamente?
              <RouterLink to="/reservar" class="font-semibold text-aurea hover:underline">
                Agenda tu cita en línea
              </RouterLink>
              .
            </p>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>
