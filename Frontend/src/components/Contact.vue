<template>
  <section id="contacto" class="relative overflow-hidden bg-white py-24">
    <!-- Forma orgánica decorativa (referencia a las formas suaves del spa) -->
    <div
      class="pointer-events-none absolute -right-32 -top-24 h-[520px] w-[520px] rounded-full bg-rose-50 blur-0"
      aria-hidden="true"
    ></div>
    <div
      class="pointer-events-none absolute -left-40 bottom-0 h-72 w-72 rounded-full bg-rose-50/70"
      aria-hidden="true"
    ></div>

    <div class="relative mx-auto grid max-w-6xl grid-cols-1 gap-16 px-6 lg:grid-cols-2 lg:px-8">
      <!-- Columna izquierda: información -->
      <div class="flex flex-col justify-center">
        <span class="font-body text-sm font-semibold uppercase tracking-[0.2em] text-red-500">
          Contacto
        </span>
        <h2 class="mt-3 font-display text-4xl leading-tight text-neutral-800 md:text-5xl">
          Hablemos de <span class="italic text-red-500">tu piel</span>
        </h2>
        <p class="mt-5 max-w-md font-body text-neutral-500">
          Escríbenos, llámanos o visítanos. Nuestro equipo te ayudará a encontrar
          el tratamiento perfecto para ti.
        </p>

        <ul class="mt-10 space-y-6">
          <li class="flex items-start gap-4">
            <span class="mt-1 h-2.5 w-2.5 shrink-0 rounded-full bg-red-500"></span>
            <div>
              <p class="font-body text-sm font-semibold text-neutral-800">Dirección</p>
              <p class="font-body text-neutral-500">Av. Maximo Gomez, Distrito Nacional, RD</p>
            </div>
          </li>
          <li class="flex items-start gap-4">
            <span class="mt-1 h-2.5 w-2.5 shrink-0 rounded-full bg-red-500"></span>
            <div>
              <p class="font-body text-sm font-semibold text-neutral-800">Teléfono</p>
              <p class="font-body text-neutral-500">+1 (809) 555-0134</p>
            </div>
          </li>
          <li class="flex items-start gap-4">
            <span class="mt-1 h-2.5 w-2.5 shrink-0 rounded-full bg-red-500"></span>
            <div>
              <p class="font-body text-sm font-semibold text-neutral-800">Correo</p>
              <p class="font-body text-neutral-500">hola@aureabeautyclinic.com</p>
            </div>
          </li>
          <li class="flex items-start gap-4">
            <span class="mt-1 h-2.5 w-2.5 shrink-0 rounded-full bg-red-500"></span>
            <div>
              <p class="font-body text-sm font-semibold text-neutral-800">Horario</p>
              <p class="font-body text-neutral-500">Lun – Sáb, 9:00 am – 6:00 pm</p>
            </div>
          </li>
        </ul>

        <div class="mt-10 flex gap-3">
          <a
            href="#"
            aria-label="Instagram"
            class="flex h-11 w-11 items-center justify-center rounded-full border border-neutral-200 text-neutral-500 transition hover:border-red-500 hover:bg-red-50 hover:text-red-500"
          >
            <svg viewBox="0 0 24 24" fill="none" class="h-5 w-5">
              <rect x="3" y="3" width="18" height="18" rx="5" stroke="currentColor" stroke-width="1.6" />
              <circle cx="12" cy="12" r="4.2" stroke="currentColor" stroke-width="1.6" />
              <circle cx="17.2" cy="6.8" r="1.1" fill="currentColor" />
            </svg>
          </a>

          <a
            href="#"
            aria-label="Facebook"
            class="flex h-11 w-11 items-center justify-center rounded-full border border-neutral-200 text-neutral-500 transition hover:border-red-500 hover:bg-red-50 hover:text-red-500"
          >
            <svg viewBox="0 0 24 24" fill="none" class="h-5 w-5">
              <path
                d="M14.5 8.5h2V5.6h-2.3c-2.3 0-3.7 1.4-3.7 3.7v1.9H8.6v3h1.9V19h3v-5.8h2.2l.4-3h-2.6V9.6c0-.7.3-1.1 1-1.1Z"
                fill="currentColor"
              />
            </svg>
          </a>

          <a
            href="#"
            aria-label="WhatsApp"
            class="flex h-11 w-11 items-center justify-center rounded-full border border-neutral-200 text-neutral-500 transition hover:border-red-500 hover:bg-red-50 hover:text-red-500"
          >
            <svg viewBox="0 0 24 24" fill="none" class="h-5 w-5">
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
        <div class="rounded-3xl border border-rose-100 bg-white p-8 shadow-[0_20px_60px_-15px_rgba(224,62,54,0.15)] md:p-10">
          <form class="space-y-6" @submit.prevent="enviarFormulario">
            <div class="grid grid-cols-1 gap-6 sm:grid-cols-2">
              <div class="relative">
                <input
                  id="nombre"
                  v-model="formulario.nombre"
                  type="text"
                  required
                  placeholder=" "
                  class="peer w-full border-b border-neutral-300 bg-transparent px-1 pb-2 pt-4 font-body text-neutral-800 outline-none focus:border-red-500"
                />
                <label
                  for="nombre"
                  class="pointer-events-none absolute left-1 top-4 font-body text-neutral-400 transition-all peer-focus:top-0 peer-focus:text-xs peer-focus:text-red-500 peer-[:not(:placeholder-shown)]:top-0 peer-[:not(:placeholder-shown)]:text-xs"
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
                  class="peer w-full border-b border-neutral-300 bg-transparent px-1 pb-2 pt-4 font-body text-neutral-800 outline-none focus:border-red-500"
                />
                <label
                  for="telefono"
                  class="pointer-events-none absolute left-1 top-4 font-body text-neutral-400 transition-all peer-focus:top-0 peer-focus:text-xs peer-focus:text-red-500 peer-[:not(:placeholder-shown)]:top-0 peer-[:not(:placeholder-shown)]:text-xs"
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
                class="peer w-full border-b border-neutral-300 bg-transparent px-1 pb-2 pt-4 font-body text-neutral-800 outline-none focus:border-red-500"
              />
              <label
                for="correo"
                class="pointer-events-none absolute left-1 top-4 font-body text-neutral-400 transition-all peer-focus:top-0 peer-focus:text-xs peer-focus:text-red-500 peer-[:not(:placeholder-shown)]:top-0 peer-[:not(:placeholder-shown)]:text-xs"
              >
                Correo electrónico
              </label>
            </div>

            <div class="relative">
              <select
                id="servicio"
                v-model="formulario.servicio"
                required
                class="peer w-full border-b border-neutral-300 bg-transparent px-1 pb-2 pt-4 font-body text-neutral-800 outline-none focus:border-red-500"
              >
                <option value="" disabled selected></option>
                <option value="facial">Tratamiento facial</option>
                <option value="corporal">Tratamiento corporal</option>
                <option value="microneedling">Microneedling</option>
                <option value="otro">Otro</option>
              </select>
              <label
                for="servicio"
                class="pointer-events-none absolute left-1 top-0 font-body text-xs text-red-500"
              >
                Servicio de interés
              </label>
            </div>

            <div class="relative">
              <textarea
                id="mensaje"
                v-model="formulario.mensaje"
                rows="3"
                placeholder=" "
                class="peer w-full resize-none border-b border-neutral-300 bg-transparent px-1 pb-2 pt-4 font-body text-neutral-800 outline-none focus:border-red-500"
              ></textarea>
              <label
                for="mensaje"
                class="pointer-events-none absolute left-1 top-4 font-body text-neutral-400 transition-all peer-focus:top-0 peer-focus:text-xs peer-focus:text-red-500 peer-[:not(:placeholder-shown)]:top-0 peer-[:not(:placeholder-shown)]:text-xs"
              >
                Cuéntanos qué buscas
              </label>
            </div>

            <button
              type="submit"
              :disabled="enviando"
              class="w-full rounded-full bg-red-500 py-3.5 font-body text-sm font-semibold uppercase tracking-wide text-white transition hover:bg-red-600 disabled:cursor-not-allowed disabled:opacity-60"
            >
              {{ enviando ? 'Enviando…' : 'Enviar mensaje' }}
            </button>

            <p v-if="mensajeEstado" class="text-center font-body text-sm" :class="huboError ? 'text-red-500' : 'text-emerald-600'">
              {{ mensajeEstado }}
            </p>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
import { reactive, ref } from 'vue'

const formulario = reactive({
  nombre: '',
  telefono: '',
  correo: '',
  servicio: '',
  mensaje: '',
})

const enviando = ref(false)
const mensajeEstado = ref('')
const huboError = ref(false)

async function enviarFormulario() {
  enviando.value = true
  mensajeEstado.value = ''
  huboError.value = false

  try {
    // Reemplazar con la llamada real al backend (por ejemplo, un endpoint /api/contacto)
    await new Promise((resolve) => setTimeout(resolve, 900))

    mensajeEstado.value = '¡Gracias! Te contactaremos muy pronto.'
    formulario.nombre = ''
    formulario.telefono = ''
    formulario.correo = ''
    formulario.servicio = ''
    formulario.mensaje = ''
  } catch (error) {
    huboError.value = true
    mensajeEstado.value = 'Ocurrió un error al enviar tu mensaje. Intenta de nuevo.'
  } finally {
    enviando.value = false
  }
}
</script>

<style scoped>
/* Tipografías consistentes con el resto del sitio */
.font-display {
  font-family: 'Playfair Display', serif;
}
.font-body {
  font-family: 'Poppins', sans-serif;
}
</style>