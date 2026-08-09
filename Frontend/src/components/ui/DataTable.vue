<script setup>
import { computed, ref, watch } from 'vue'
import LoadingSpinner from './LoadingSpinner.vue'
import EmptyState from './EmptyState.vue'

/**
 * Tabla con búsqueda, orden y paginación EN CLIENTE.
 * Ningún endpoint del backend pagina: GetAll() devuelve la colección completa.
 *
 * Cada columna: { key, label, sortable?, searchable?, width?, align? }
 *
 * El contenido de una celda se puede personalizar con el slot `cell-<key>`, donde
 * los puntos de las claves anidadas se sustituyen por guiones: la columna
 * `user.fullName` usa el slot `cell-user-fullName` (Vue interpretaría un punto en
 * el nombre del slot como un modificador y el slot nunca coincidiría).
 */
const props = defineProps({
  columns: { type: Array, required: true },
  rows: { type: Array, default: () => [] },
  rowKey: { type: String, required: true },
  loading: { type: Boolean, default: false },
  searchable: { type: Boolean, default: true },
  searchPlaceholder: { type: String, default: 'Buscar...' },
  pageSize: { type: Number, default: 10 },
  emptyTitle: { type: String, default: 'Sin resultados' },
  emptyDescription: { type: String, default: '' }
})

const search = ref('')
const sortKey = ref('')
const sortAsc = ref(true)
const page = ref(1)

const cellValue = (row, key) =>
  key.split('.').reduce((acc, part) => (acc == null ? acc : acc[part]), row)

/** `user.fullName` → `cell-user-fullName` */
const slotName = (key) => `cell-${key.replace(/\./g, '-')}`

const searchableKeys = computed(() =>
  props.columns.filter((c) => c.searchable !== false).map((c) => c.key)
)

const filtered = computed(() => {
  const term = search.value.trim().toLowerCase()
  if (!term) return props.rows

  return props.rows.filter((row) =>
    searchableKeys.value.some((key) => {
      const value = cellValue(row, key)
      return value != null && String(value).toLowerCase().includes(term)
    })
  )
})

const sorted = computed(() => {
  if (!sortKey.value) return filtered.value

  return [...filtered.value].sort((a, b) => {
    const x = cellValue(a, sortKey.value)
    const y = cellValue(b, sortKey.value)

    if (x == null) return 1
    if (y == null) return -1

    const result =
      typeof x === 'number' && typeof y === 'number'
        ? x - y
        : String(x).localeCompare(String(y), 'es', { numeric: true })

    return sortAsc.value ? result : -result
  })
})

const totalPages = computed(() => Math.max(1, Math.ceil(sorted.value.length / props.pageSize)))

const paged = computed(() => {
  const start = (page.value - 1) * props.pageSize
  return sorted.value.slice(start, start + props.pageSize)
})

// Al filtrar u ordenar, la página actual puede quedar fuera de rango.
watch([filtered, sorted], () => {
  if (page.value > totalPages.value) page.value = totalPages.value
})
watch(search, () => {
  page.value = 1
})

const toggleSort = (column) => {
  if (column.sortable === false) return
  if (sortKey.value === column.key) {
    sortAsc.value = !sortAsc.value
  } else {
    sortKey.value = column.key
    sortAsc.value = true
  }
}

const alignClass = (column) =>
  column.align === 'right' ? 'text-right' : column.align === 'center' ? 'text-center' : 'text-left'
</script>

<template>
  <div class="bg-white border border-gray-100 rounded-xl shadow-sm overflow-hidden">
    <div
      v-if="searchable || $slots.filters"
      class="flex flex-col sm:flex-row gap-3 sm:items-center p-4 border-b border-gray-100"
    >
      <div v-if="searchable" class="relative flex-1 min-w-0">
        <svg
          class="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-gray-400"
          fill="none"
          stroke="currentColor"
          stroke-width="2"
          viewBox="0 0 24 24"
          aria-hidden="true"
        >
          <path stroke-linecap="round" stroke-linejoin="round" d="M21 21l-4.35-4.35M17 11a6 6 0 11-12 0 6 6 0 0112 0z" />
        </svg>
        <input
          v-model="search"
          type="search"
          :placeholder="searchPlaceholder"
          :aria-label="searchPlaceholder"
          class="w-full pl-9 pr-4 py-2.5 text-sm rounded-lg border border-gray-200 outline-none transition-all focus:ring-2 focus:ring-aurea focus:border-transparent"
        />
      </div>

      <div class="flex flex-wrap items-center gap-3">
        <slot name="filters" />
      </div>
    </div>

    <LoadingSpinner v-if="loading" />

    <EmptyState
      v-else-if="!sorted.length"
      :title="search ? 'Sin coincidencias' : emptyTitle"
      :description="search ? `Ningún registro coincide con «${search}».` : emptyDescription"
    >
      <slot name="empty-action" />
    </EmptyState>

    <template v-else>
      <div class="overflow-x-auto">
        <table class="w-full text-sm">
          <thead>
            <tr class="border-b border-gray-100 bg-gray-50/60">
              <th
                v-for="column in columns"
                :key="column.key"
                scope="col"
                :style="column.width ? { width: column.width } : undefined"
                :class="[
                  'px-4 py-3 text-xs font-bold uppercase tracking-wider text-gray-500 whitespace-nowrap',
                  alignClass(column),
                  column.sortable === false ? '' : 'cursor-pointer select-none hover:text-aurea'
                ]"
                @click="toggleSort(column)"
              >
                <span class="inline-flex items-center gap-1">
                  {{ column.label }}
                  <span v-if="sortKey === column.key" aria-hidden="true">
                    {{ sortAsc ? '↑' : '↓' }}
                  </span>
                </span>
              </th>
            </tr>
          </thead>

          <tbody>
            <tr
              v-for="row in paged"
              :key="row[rowKey]"
              class="border-b border-gray-50 last:border-0 hover:bg-aurea-tint/40 transition-colors"
            >
              <td
                v-for="column in columns"
                :key="column.key"
                :class="['px-4 py-3 text-gray-700 align-middle', alignClass(column)]"
              >
                <slot :name="slotName(column.key)" :row="row" :value="cellValue(row, column.key)">
                  {{ cellValue(row, column.key) ?? '—' }}
                </slot>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div
        v-if="totalPages > 1"
        class="flex items-center justify-between gap-4 px-4 py-3 border-t border-gray-100"
      >
        <p class="text-xs text-gray-400">
          {{ sorted.length }} registro{{ sorted.length === 1 ? '' : 's' }} · página
          {{ page }} de {{ totalPages }}
        </p>

        <div class="flex items-center gap-2">
          <button
            type="button"
            class="px-3 py-1.5 text-xs font-bold rounded-lg border border-gray-200 text-gray-600 transition-colors hover:border-aurea hover:text-aurea disabled:opacity-40 disabled:cursor-not-allowed disabled:hover:border-gray-200 disabled:hover:text-gray-600 cursor-pointer"
            :disabled="page === 1"
            @click="page--"
          >
            Anterior
          </button>
          <button
            type="button"
            class="px-3 py-1.5 text-xs font-bold rounded-lg border border-gray-200 text-gray-600 transition-colors hover:border-aurea hover:text-aurea disabled:opacity-40 disabled:cursor-not-allowed disabled:hover:border-gray-200 disabled:hover:text-gray-600 cursor-pointer"
            :disabled="page === totalPages"
            @click="page++"
          >
            Siguiente
          </button>
        </div>
      </div>
    </template>
  </div>
</template>
