<script setup>
import { computed, onMounted, reactive, ref } from 'vue'
import PageHeader from '@/components/ui/PageHeader.vue'
import DataTable from '@/components/ui/DataTable.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseModal from '@/components/ui/BaseModal.vue'
import BaseInput from '@/components/ui/BaseInput.vue'
import BaseSelect from '@/components/ui/BaseSelect.vue'
import BaseTextarea from '@/components/ui/BaseTextarea.vue'
import BaseCheckbox from '@/components/ui/BaseCheckbox.vue'
import BaseBadge from '@/components/ui/BaseBadge.vue'
import { useCrud } from '@/composables/useCrud'
import { getDoctors, createDoctor, updateDoctor, deactivateDoctor } from '@/services/doctorService'
import { getSpecialties } from '@/services/specialtyService'
import { getUsers } from '@/services/userService'
import { notifyApiError } from '@/utils/notify'

const { items, loading, saving, load, save, confirmRemove } = useCrud({
  fetchAll: getDoctors,
  create: createDoctor,
  update: updateDoctor,
  remove: deactivateDoctor,
  idKey: 'doctorId',
  labels: { singular: 'Doctor', plural: 'los doctores' }
})

const specialties = ref([])
const users = ref([])
const specialtyFilter = ref('')

const COLUMNS = [
  { key: 'doctorId', label: '#', width: '70px' },
  { key: 'user.fullName', label: 'Doctor' },
  { key: 'specialty.name', label: 'Especialidad', width: '180px' },
  { key: 'licenseNumber', label: 'Licencia', width: '140px' },
  { key: 'isActive', label: 'Estado', width: '110px' },
  { key: 'actions', label: 'Acciones', sortable: false, searchable: false, align: 'right', width: '190px' }
]

const specialtyOptions = computed(() =>
  specialties.value
    .filter((s) => s.isActive)
    .map((s) => ({ value: String(s.specialtyId), label: s.name }))
)

/**
 * Un doctor se asocia a un usuario existente. Se ofrecen todos los usuarios
 * activos que aún no tienen ficha, con los de rol Doctor primero.
 *
 * No se filtra sólo por rol 'Doctor' a propósito: el conjunto de roles varía
 * entre entornos y ese filtro dejaba el desplegable vacío cuando el rol no existe.
 */
const availableUserOptions = computed(() => {
  const taken = new Set(items.value.map((d) => d.userId))

  return users.value
    .filter((u) => u.isActive && !taken.has(u.userId))
    .sort((a, b) => {
      const rank = (u) => (u.role?.name === 'Doctor' ? 0 : 1)
      return rank(a) - rank(b) || a.name.localeCompare(b.name, 'es')
    })
    .map((u) => ({
      value: String(u.userId),
      label: `${u.name} ${u.lastName} — ${u.email} (${u.role?.name ?? 'sin rol'})`
    }))
})

const filteredDoctors = computed(() =>
  specialtyFilter.value
    ? items.value.filter((d) => String(d.specialtyId) === specialtyFilter.value)
    : items.value
)

const modalOpen = ref(false)
const editing = ref(null)

const form = reactive({
  userId: '',
  specialtyId: '',
  licenseNumber: '',
  biography: '',
  photoURL: '',
  isActive: true
})

const openCreate = () => {
  editing.value = null
  Object.assign(form, {
    userId: '',
    specialtyId: '',
    licenseNumber: '',
    biography: '',
    photoURL: '',
    isActive: true
  })
  modalOpen.value = true
}

const openEdit = (row) => {
  editing.value = row
  Object.assign(form, {
    userId: String(row.userId),
    specialtyId: String(row.specialtyId),
    licenseNumber: row.licenseNumber ?? '',
    biography: row.biography ?? '',
    photoURL: row.photoURL ?? '',
    isActive: row.isActive
  })
  modalOpen.value = true
}

const handleSubmit = async () => {
  // Ojo con el casing: el backend espera `photoURL`, no `photoUrl`.
  const base = {
    specialtyId: Number(form.specialtyId),
    licenseNumber: form.licenseNumber.trim() || null,
    biography: form.biography.trim() || null,
    photoURL: form.photoURL.trim() || null,
    isActive: form.isActive
  }

  // En la actualización el backend ignora userId, así que sólo se envía al crear.
  const payload = editing.value ? base : { ...base, userId: Number(form.userId) }

  const ok = await save(payload, editing.value?.doctorId ?? null)
  if (ok) modalOpen.value = false
}

onMounted(async () => {
  await load()
  try {
    const [s, u] = await Promise.all([getSpecialties(), getUsers()])
    specialties.value = s ?? []
    users.value = u ?? []
  } catch (e) {
    notifyApiError(e, 'No pudimos cargar especialidades y usuarios')
  }
})
</script>

<template>
  <div>
    <PageHeader
      eyebrow="Equipo"
      title="Doctores"
      description="Cada doctor se vincula a una cuenta de usuario con rol Doctor y a una especialidad."
    >
      <template #actions>
        <BaseButton @click="openCreate">Nuevo doctor</BaseButton>
      </template>
    </PageHeader>

    <DataTable
      :columns="COLUMNS"
      :rows="filteredDoctors"
      row-key="doctorId"
      :loading="loading"
      search-placeholder="Buscar por nombre, especialidad o licencia..."
      empty-title="Aún no hay doctores"
      empty-description="Crea primero un usuario con rol Doctor y luego asócialo aquí a una especialidad."
    >
      <template #filters>
        <select
          v-model="specialtyFilter"
          aria-label="Filtrar por especialidad"
          class="px-3 py-2.5 text-sm rounded-lg border border-gray-200 bg-white outline-none transition-all focus:ring-2 focus:ring-aurea focus:border-transparent cursor-pointer"
        >
          <option value="">Todas las especialidades</option>
          <option v-for="opt in specialtyOptions" :key="opt.value" :value="opt.value">
            {{ opt.label }}
          </option>
        </select>
      </template>

      <template #cell-doctorId="{ value }">
        <span class="text-gray-400">{{ value }}</span>
      </template>

      <template #cell-user-fullName="{ row }">
        <div class="flex items-center gap-3">
          <img
            v-if="row.photoURL"
            :src="row.photoURL"
            :alt="`Foto de ${row.user?.name}`"
            class="h-9 w-9 rounded-full object-cover shrink-0"
          />
          <span
            v-else
            class="h-9 w-9 shrink-0 rounded-full bg-aurea-tint text-aurea flex items-center justify-center text-xs font-bold"
            aria-hidden="true"
          >
            {{ (row.user?.name?.[0] ?? '') + (row.user?.lastName?.[0] ?? '') }}
          </span>
          <div class="min-w-0">
            <p class="font-semibold text-gray-800 truncate">
              {{ row.user?.name }} {{ row.user?.lastName }}
            </p>
            <p class="text-xs text-gray-400 truncate">{{ row.user?.email }}</p>
          </div>
        </div>
      </template>

      <template #cell-specialty-name="{ value }">
        <BaseBadge variant="bg-gray-100 text-gray-700 border-gray-200">{{ value }}</BaseBadge>
      </template>

      <template #cell-licenseNumber="{ value }">
        <span class="text-gray-500">{{ value || '—' }}</span>
      </template>

      <template #cell-isActive="{ value }">
        <BaseBadge
          :variant="
            value
              ? 'bg-emerald-50 text-emerald-700 border-emerald-200'
              : 'bg-gray-100 text-gray-500 border-gray-200'
          "
        >
          {{ value ? 'Activo' : 'Inactivo' }}
        </BaseBadge>
      </template>

      <template #cell-actions="{ row }">
        <div class="flex justify-end gap-2">
          <BaseButton variant="secondary" size="sm" @click="openEdit(row)">Editar</BaseButton>
          <BaseButton
            v-if="row.isActive"
            variant="danger"
            size="sm"
            @click="confirmRemove(row, (r) => `El doctor ${r.user?.name} ${r.user?.lastName}`)"
          >
            Desactivar
          </BaseButton>
        </div>
      </template>

      <template #empty-action>
        <BaseButton @click="openCreate">Nuevo doctor</BaseButton>
      </template>
    </DataTable>

    <BaseModal
      v-model="modalOpen"
      :title="editing ? 'Editar doctor' : 'Nuevo doctor'"
      :subtitle="
        editing
          ? `Cuenta asociada: ${editing.user?.email}`
          : 'Selecciona la cuenta de usuario que corresponde a este doctor.'
      "
      size="lg"
    >
      <form id="doctor-form" class="space-y-5" @submit.prevent="handleSubmit">
        <BaseSelect
          v-if="!editing"
          v-model="form.userId"
          label="Usuario"
          :options="availableUserOptions"
          placeholder="Selecciona un usuario con rol Doctor"
          :hint="
            availableUserOptions.length
              ? 'Aparecen los usuarios activos que aún no tienen ficha de doctor.'
              : 'No hay usuarios disponibles. Crea uno primero en la sección Usuarios.'
          "
          required
        />

        <BaseSelect
          v-model="form.specialtyId"
          label="Especialidad"
          :options="specialtyOptions"
          placeholder="Selecciona una especialidad"
          required
        />

        <div class="grid sm:grid-cols-2 gap-5">
          <BaseInput
            v-model="form.licenseNumber"
            label="Número de licencia"
            placeholder="Ej. EX-12345"
            maxlength="50"
          />
          <BaseInput
            v-model="form.photoURL"
            label="URL de la foto"
            type="url"
            placeholder="https://..."
            hint="Debe ser una URL completa y válida."
          />
        </div>

        <div v-if="form.photoURL" class="flex items-center gap-3">
          <img
            :src="form.photoURL"
            alt="Vista previa de la foto"
            class="h-16 w-16 rounded-full object-cover border border-gray-100"
          />
          <span class="text-xs text-gray-400">Vista previa</span>
        </div>

        <BaseTextarea
          v-model="form.biography"
          label="Biografía"
          placeholder="Formación, años de experiencia, enfoque profesional..."
          :rows="5"
          maxlength="1000"
        />

        <BaseCheckbox
          v-model="form.isActive"
          label="Doctor activo"
          hint="Sólo los doctores activos pueden recibir reservas."
        />
      </form>

      <template #footer>
        <BaseButton variant="secondary" @click="modalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="doctor-form" :loading="saving">
          {{ editing ? 'Guardar cambios' : 'Crear doctor' }}
        </BaseButton>
      </template>
    </BaseModal>
  </div>
</template>
