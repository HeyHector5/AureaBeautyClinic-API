<template>
  <div class="min-h-screen bg-gray-50 py-12 px-4">
    <div class="max-w-2xl mx-auto">

      <!-- Header -->
      <div class="mb-10">
        <button
          @click="router.push('/')"
          class="text-sm text-gray-400 hover:text-[#FF3B30] transition-colors flex items-center gap-2 mb-6 cursor-pointer"
        >
          ← Volver al inicio
        </button>
        <h1 class="text-3xl font-bold text-gray-900">Reservar cita</h1>
        <p class="text-gray-400 mt-1">Completa los pasos para confirmar tu reserva</p>
      </div>

      <!-- Stepper -->
      <div class="flex items-center gap-2 mb-10">
        <div 
          v-for="(step, i) in steps" :key="i"
          class="flex items-center gap-2"
        >
          <div 
            class="w-8 h-8 rounded-full flex items-center justify-center text-sm font-bold transition-all"
            :class="currentStep > i 
              ? 'bg-[#FF3B30] text-white' 
              : currentStep === i 
                ? 'bg-[#FF3B30] text-white ring-4 ring-red-100' 
                : 'bg-gray-200 text-gray-400'"
          >
            <span v-if="currentStep > i">✓</span>
            <span v-else>{{ i + 1 }}</span>
          </div>
          <span 
            class="text-sm font-medium hidden sm:inline"
            :class="currentStep === i ? 'text-gray-900' : 'text-gray-400'"
          >
            {{ step }}
          </span>
          <div v-if="i < steps.length - 1" class="w-8 h-px bg-gray-300 mx-1"></div>
        </div>
      </div>

      <!-- Card contenedor -->
      <div class="bg-white rounded-2xl shadow-sm border border-gray-100 p-8">

        <!-- PASO 1: Confirmar servicio -->
        <div v-if="currentStep === 0">
          <h2 class="text-lg font-bold text-gray-900 mb-6">¿Qué servicio deseas?</h2>
          <div class="grid grid-cols-1 sm:grid-cols-2 gap-3">
            <button
              v-for="servicio in serviciosDisponibles"
              :key="servicio.id"
              @click="form.servicio = servicio.nombre"
              class="text-left p-4 rounded-xl border-2 transition-all cursor-pointer"
              :class="form.servicio === servicio.nombre 
                ? 'border-[#FF3B30] bg-red-50' 
                : 'border-gray-100 hover:border-gray-200'"
            >
              <div class="text-xl mb-2">{{ servicio.icono }}</div>
              <div class="text-[10px] font-bold uppercase tracking-wide text-[#FF3B30] mb-1">{{ servicio.categoria }}</div>
              <div class="font-semibold text-gray-800 text-sm">{{ servicio.nombre }}</div>
              <div class="text-xs text-gray-400 mt-1">{{ servicio.precio }}</div>
            </button>
          </div>
          <div v-if="!form.servicio" class="mt-4 text-xs text-gray-400">
            Selecciona un servicio para continuar
          </div>
        </div>

        <!-- PASO 2: Fecha y hora -->
        <div v-if="currentStep === 1">
          <h2 class="text-lg font-bold text-gray-900 mb-6">Elige fecha y hora</h2>
          <div class="space-y-5">
            <div>
              <label class="block text-sm font-semibold text-gray-700 mb-2">Fecha</label>
              <input 
                type="date" 
                v-model="form.fecha"
                :min="hoy"
                class="w-full border border-gray-200 rounded-xl px-4 py-3 text-sm focus:outline-none focus:ring-2 focus:ring-[#FF3B30] focus:border-transparent transition-all"
              />
            </div>
            <div>
              <label class="block text-sm font-semibold text-gray-700 mb-3">Hora disponible</label>
              <div class="grid grid-cols-3 sm:grid-cols-4 gap-2">
                <button
                  v-for="hora in horasDisponibles"
                  :key="hora"
                  @click="form.hora = hora"
                  class="py-2 px-3 rounded-lg text-sm font-medium border-2 transition-all cursor-pointer"
                  :class="form.hora === hora 
                    ? 'border-[#FF3B30] bg-[#FF3B30] text-white' 
                    : 'border-gray-100 text-gray-600 hover:border-gray-300'"
                >
                  {{ hora }}
                </button>
              </div>
            </div>
          </div>
        </div>

        <!-- PASO 3: Notas y confirmación -->
        <div v-if="currentStep === 2">
          <h2 class="text-lg font-bold text-gray-900 mb-6">Confirma tu reserva</h2>
          <div class="bg-gray-50 rounded-xl p-5 mb-6 space-y-3">
            <div class="flex justify-between text-sm">
              <span class="text-gray-500">Servicio</span>
              <span class="font-semibold text-gray-800">{{ form.servicio }}</span>
            </div>
            <div class="flex justify-between text-sm">
              <span class="text-gray-500">Fecha</span>
              <span class="font-semibold text-gray-800">{{ fechaFormateada }}</span>
            </div>
            <div class="flex justify-between text-sm">
              <span class="text-gray-500">Hora</span>
              <span class="font-semibold text-gray-800">{{ form.hora }}</span>
            </div>
            <div class="border-t border-gray-200 pt-3 flex justify-between text-sm">
              <span class="text-gray-500">Precio</span>
              <span class="font-bold text-[#FF3B30]">{{ precioSeleccionado }}</span>
            </div>
          </div>
          <div>
            <label class="block text-sm font-semibold text-gray-700 mb-2">
              Notas adicionales <span class="text-gray-400 font-normal">(opcional)</span>
            </label>
            <textarea
              v-model="form.notas"
              rows="3"
              placeholder="Alergias, preferencias, preguntas..."
              class="w-full border border-gray-200 rounded-xl px-4 py-3 text-sm focus:outline-none focus:ring-2 focus:ring-[#FF3B30] focus:border-transparent transition-all resize-none"
            ></textarea>
          </div>

          <!-- Confirmación exitosa -->
          <div v-if="reservaConfirmada" class="mt-6 bg-green-50 border border-green-200 rounded-xl p-5 text-center">
            <div class="text-3xl mb-2">✅</div>
            <p class="font-bold text-green-800">¡Reserva confirmada!</p>
            <p class="text-sm text-green-600 mt-1">Te esperamos el {{ fechaFormateada }} a las {{ form.hora }}</p>
            <button
              @click="router.push('/')"
              class="mt-4 text-sm text-[#FF3B30] font-semibold hover:underline cursor-pointer"
            >
              Volver al inicio
            </button>
          </div>
        </div>

        <!-- Navegación -->
        <div class="flex justify-between mt-8" v-if="!reservaConfirmada">
          <button
            v-if="currentStep > 0"
            @click="currentStep--"
            class="px-5 py-2.5 text-sm font-semibold text-gray-500 hover:text-gray-800 transition-colors cursor-pointer"
          >
            ← Atrás
          </button>
          <div v-else></div>

          <button
            v-if="currentStep < 2"
            @click="siguientePaso"
            :disabled="!puedeAvanzar"
            class="px-6 py-2.5 text-sm font-bold rounded-xl transition-all cursor-pointer"
            :class="puedeAvanzar 
              ? 'bg-[#FF3B30] text-white hover:bg-red-600' 
              : 'bg-gray-100 text-gray-300 cursor-not-allowed'"
          >
            Siguiente →
          </button>

          <button
            v-if="currentStep === 2"
            @click="confirmarReserva"
            class="px-6 py-2.5 text-sm font-bold bg-[#FF3B30] text-white rounded-xl hover:bg-red-600 transition-all cursor-pointer"
          >
            Confirmar reserva
          </button>
        </div>

      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { servicios } from '../store/services';

const route = useRoute();
const router = useRouter();

const currentStep = ref(0);
const reservaConfirmada = ref(false);
const steps = ['Servicio', 'Fecha y hora', 'Confirmación'];

const serviciosDisponibles = computed(() => servicios.value.filter(s => s.activo));

const horasDisponibles = [
  '9:00 AM', '9:30 AM', '10:00 AM', '10:30 AM',
  '11:00 AM', '11:30 AM', '2:00 PM',  '2:30 PM',
  '3:00 PM',  '3:30 PM',  '4:00 PM',  '4:30 PM',
];

const form = ref({
  servicio: '',
  fecha: '',
  hora: '',
  notas: '',
});

const hoy = new Date().toISOString().split('T')[0];

//Watch: pre-selecciona servicio y salta al paso 2 si viene desde un card (?servicio=... en la URL)
watch(
  () => route.query.servicio,
  (nombreServicio) => {
    const servicio = servicios.value.find(s => s.nombre === nombreServicio);
    if (servicio) {
      form.value.servicio = servicio.nombre;
      form.value.fecha = '';
      form.value.hora = '';
      form.value.notas = '';
      reservaConfirmada.value = false;
      currentStep.value = 1; // salta directo a fecha y hora
    } else {
      // Reset completo si no hay servicio (entrada manual)
      form.value = { servicio: '', fecha: '', hora: '', notas: '' };
      currentStep.value = 0;
      reservaConfirmada.value = false;
    }
  },
  { immediate: true }
);

const fechaFormateada = computed(() => {
  if (!form.value.fecha) return '';
  const [y, m, d] = form.value.fecha.split('-');
  return `${d}/${m}/${y}`;
});

const precioSeleccionado = computed(() => {
  return servicios.value.find(s => s.nombre === form.value.servicio)?.precio ?? '';
});

const puedeAvanzar = computed(() => {
  if (currentStep.value === 0) return !!form.value.servicio;
  if (currentStep.value === 1) return !!form.value.fecha && !!form.value.hora;
  return true;
});

const siguientePaso = () => {
  if (puedeAvanzar.value) currentStep.value++;
};

const confirmarReserva = () => {
  reservaConfirmada.value = true;
};
</script>