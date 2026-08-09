<template>
  <div class="min-h-screen bg-gray-50 py-12 px-4">
    <div class="max-w-4xl mx-auto">

      <div class="mb-10 flex items-start justify-between flex-wrap gap-4">
        <div>
          <h1 class="text-3xl font-bold text-gray-900">Panel de administración</h1>
          <p class="text-gray-400 mt-1">Gestiona los servicios que ven tus clientes</p>
        </div>
        <button
          @click="abrirFormularioNuevo"
          class="px-5 py-2.5 text-sm font-bold bg-[#FF3B30] text-white rounded-xl hover:bg-red-600 transition-all cursor-pointer"
        >
          + Agregar servicio
        </button>
      </div>

      <!-- Formulario de alta/edición -->
      <div v-if="formularioAbierto" class="bg-white rounded-2xl shadow-sm border border-gray-100 p-8 mb-8">
        <h2 class="text-lg font-bold text-gray-900 mb-6">
          {{ editandoId ? 'Editar servicio' : 'Nuevo servicio' }}
        </h2>

        <form @submit.prevent="guardarServicio" class="space-y-5">
          <div class="grid grid-cols-1 sm:grid-cols-[100px_1fr] gap-5">
            <div>
              <label class="block text-sm font-semibold text-gray-700 mb-2">Ícono</label>
              <input
                v-model="form.icono"
                type="text"
                maxlength="4"
                placeholder="✨"
                class="w-full border border-gray-200 rounded-xl px-4 py-3 text-center text-2xl focus:outline-none focus:ring-2 focus:ring-[#FF3B30] focus:border-transparent transition-all"
              />
            </div>
            <div>
              <label class="block text-sm font-semibold text-gray-700 mb-2">Nombre</label>
              <input
                v-model="form.nombre"
                type="text"
                required
                placeholder="Ej. Manicure spa"
                class="w-full border border-gray-200 rounded-xl px-4 py-3 text-sm focus:outline-none focus:ring-2 focus:ring-[#FF3B30] focus:border-transparent transition-all"
              />
            </div>
          </div>

          <div>
            <label class="block text-sm font-semibold text-gray-700 mb-2">Descripción</label>
            <textarea
              v-model="form.descripcion"
              rows="2"
              placeholder="Breve descripción del servicio"
              class="w-full border border-gray-200 rounded-xl px-4 py-3 text-sm focus:outline-none focus:ring-2 focus:ring-[#FF3B30] focus:border-transparent transition-all resize-none"
            ></textarea>
          </div>

          <div class="grid grid-cols-1 sm:grid-cols-2 gap-5">
            <div>
              <label class="block text-sm font-semibold text-gray-700 mb-2">Categoría</label>
              <select
                v-model="form.categoria"
                required
                class="w-full border border-gray-200 rounded-xl px-4 py-3 text-sm bg-white focus:outline-none focus:ring-2 focus:ring-[#FF3B30] focus:border-transparent transition-all"
              >
                <option v-for="categoria in CATEGORIAS" :key="categoria" :value="categoria">
                  {{ categoria }}
                </option>
              </select>
            </div>
            <div>
              <label class="block text-sm font-semibold text-gray-700 mb-2">Precio</label>
              <input
                v-model="form.precio"
                type="text"
                required
                placeholder="RD$2,500"
                class="w-full border border-gray-200 rounded-xl px-4 py-3 text-sm focus:outline-none focus:ring-2 focus:ring-[#FF3B30] focus:border-transparent transition-all"
              />
            </div>
          </div>

          <div class="flex gap-3 pt-2">
            <button
              type="submit"
              class="px-6 py-2.5 text-sm font-bold bg-[#FF3B30] text-white rounded-xl hover:bg-red-600 transition-all cursor-pointer"
            >
              {{ editandoId ? 'Guardar cambios' : 'Crear servicio' }}
            </button>
            <button
              type="button"
              @click="cerrarFormulario"
              class="px-6 py-2.5 text-sm font-semibold text-gray-500 hover:text-gray-800 transition-colors cursor-pointer"
            >
              Cancelar
            </button>
          </div>
        </form>
      </div>

      <!-- Listado de servicios agrupado por categoría -->
      <div class="space-y-10">
        <div v-for="grupo in serviciosPorCategoria" :key="grupo.categoria">
          <h2 class="text-xs font-bold tracking-widest uppercase text-gray-400 mb-4">
            {{ grupo.categoria }}
          </h2>

          <div class="space-y-4">
            <div
              v-for="servicio in grupo.items"
              :key="servicio.id"
              class="bg-white rounded-2xl shadow-sm border border-gray-100 p-6 flex items-start gap-4"
              :class="{ 'opacity-50': !servicio.activo }"
            >
              <div class="flex h-14 w-14 shrink-0 items-center justify-center rounded-2xl bg-rose-50 text-2xl">
                {{ servicio.icono }}
              </div>

              <div class="flex-1 min-w-0">
                <div class="flex items-center gap-2 flex-wrap">
                  <h3 class="font-bold text-gray-900">{{ servicio.nombre }}</h3>
                  <span
                    class="text-xs font-bold px-2 py-0.5 rounded-full"
                    :class="servicio.activo ? 'bg-green-50 text-green-600' : 'bg-gray-100 text-gray-400'"
                  >
                    {{ servicio.activo ? 'Activo' : 'Inactivo' }}
                  </span>
                </div>
                <p class="text-sm text-gray-400 mt-1">{{ servicio.descripcion }}</p>
                <p class="text-sm font-semibold text-[#FF3B30] mt-2">{{ servicio.precio }}</p>
              </div>

              <div class="flex flex-col sm:flex-row gap-2 shrink-0">
                <button
                  @click="alternarActivo(servicio.id)"
                  class="px-3 py-1.5 text-xs font-semibold text-gray-500 hover:text-gray-800 border border-gray-200 rounded-lg transition-colors cursor-pointer"
                >
                  {{ servicio.activo ? 'Desactivar' : 'Activar' }}
                </button>
                <button
                  @click="abrirFormularioEdicion(servicio)"
                  class="px-3 py-1.5 text-xs font-semibold text-gray-500 hover:text-gray-800 border border-gray-200 rounded-lg transition-colors cursor-pointer"
                >
                  Editar
                </button>
                <button
                  @click="confirmarEliminar(servicio)"
                  class="px-3 py-1.5 text-xs font-semibold text-red-500 hover:text-white hover:bg-red-500 border border-red-200 rounded-lg transition-colors cursor-pointer"
                >
                  Eliminar
                </button>
              </div>
            </div>
          </div>
        </div>

        <div v-if="!servicios.length" class="text-center text-sm text-gray-400 py-12">
          No hay servicios registrados todavía.
        </div>
      </div>

    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue';
import Swal from 'sweetalert2';
import { servicios, agregarServicio, actualizarServicio, eliminarServicio, alternarActivo, CATEGORIAS } from '../store/services';

const formularioAbierto = ref(false);
const editandoId = ref(null);
const form = ref({ nombre: '', descripcion: '', precio: '', icono: '', categoria: CATEGORIAS[0] });

const serviciosPorCategoria = computed(() =>
  CATEGORIAS
    .map(categoria => ({
      categoria,
      items: servicios.value.filter(s => s.categoria === categoria),
    }))
    .filter(grupo => grupo.items.length)
);

function abrirFormularioNuevo() {
  editandoId.value = null;
  form.value = { nombre: '', descripcion: '', precio: '', icono: '', categoria: CATEGORIAS[0] };
  formularioAbierto.value = true;
}

function abrirFormularioEdicion(servicio) {
  editandoId.value = servicio.id;
  form.value = {
    nombre: servicio.nombre,
    descripcion: servicio.descripcion,
    precio: servicio.precio,
    icono: servicio.icono,
    categoria: servicio.categoria,
  };
  formularioAbierto.value = true;
}

function cerrarFormulario() {
  formularioAbierto.value = false;
  editandoId.value = null;
}

function guardarServicio() {
  if (editandoId.value) {
    actualizarServicio(editandoId.value, { ...form.value });
  } else {
    agregarServicio({ ...form.value });
  }
  cerrarFormulario();
}

function confirmarEliminar(servicio) {
  Swal.fire({
    icon: 'warning',
    title: '¿Eliminar servicio?',
    text: `"${servicio.nombre}" ya no estará disponible para reservar.`,
    showCancelButton: true,
    confirmButtonText: 'Eliminar',
    cancelButtonText: 'Cancelar',
    confirmButtonColor: '#FF3B30',
  }).then((result) => {
    if (result.isConfirmed) {
      eliminarServicio(servicio.id);
      if (editandoId.value === servicio.id) cerrarFormulario();
    }
  });
}
</script>
