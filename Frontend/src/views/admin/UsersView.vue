<script setup>
import { computed, onMounted, reactive, ref } from 'vue'
import PageHeader from '@/components/ui/PageHeader.vue'
import DataTable from '@/components/ui/DataTable.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseModal from '@/components/ui/BaseModal.vue'
import BaseInput from '@/components/ui/BaseInput.vue'
import BaseSelect from '@/components/ui/BaseSelect.vue'
import BaseCheckbox from '@/components/ui/BaseCheckbox.vue'
import BaseBadge from '@/components/ui/BaseBadge.vue'
import { useCrud } from '@/composables/useCrud'
import { getUsers, createUser, updateUser, deactivateUser } from '@/services/userService'
import { getRoles } from '@/services/roleService'
import { formatDate } from '@/utils/format'
import { notifyApiError } from '@/utils/notify'

const { items, loading, saving, load, save, confirmRemove } = useCrud({
  fetchAll: getUsers,
  create: createUser,
  update: updateUser,
  remove: deactivateUser,
  idKey: 'userId',
  labels: { singular: 'Usuario', plural: 'los usuarios' }
})

const roles = ref([])
const roleFilter = ref('')

const COLUMNS = [
  { key: 'fullName', label: 'Nombre' },
  { key: 'email', label: 'Correo' },
  { key: 'phoneNumber', label: 'Teléfono' },
  { key: 'role.name', label: 'Rol', width: '130px' },
  { key: 'registerDate', label: 'Registro', width: '140px' },
  { key: 'isActive', label: 'Estado', width: '110px' },
  { key: 'actions', label: 'Acciones', sortable: false, searchable: false, align: 'right', width: '190px' }
]

const roleOptions = computed(() =>
  roles.value.map((r) => ({ value: String(r.roleId), label: r.name }))
)

const filteredUsers = computed(() =>
  roleFilter.value ? items.value.filter((u) => String(u.roleId) === roleFilter.value) : items.value
)

const modalOpen = ref(false)
const editing = ref(null)

const form = reactive({
  roleId: '',
  name: '',
  lastName: '',
  email: '',
  password: '',
  phone: '',
  isActive: true
})

const openCreate = () => {
  editing.value = null
  Object.assign(form, {
    roleId: '',
    name: '',
    lastName: '',
    email: '',
    password: '',
    phone: '',
    isActive: true
  })
  modalOpen.value = true
}

const openEdit = (row) => {
  editing.value = row
  Object.assign(form, {
    roleId: String(row.roleId),
    name: row.name ?? '',
    lastName: row.lastName ?? '',
    email: row.email ?? '',
    password: '',
    phone: row.phoneNumber ?? '',
    isActive: row.isActive
  })
  modalOpen.value = true
}

const handleSubmit = async () => {
  // La API no permite cambiar el correo ni el rol de un usuario existente:
  // en edición sólo se envían los campos que UpdateUserRequest acepta.
  const payload = editing.value
    ? {
        name: form.name.trim(),
        lastName: form.lastName.trim(),
        phone: form.phone.trim() || null,
        isActive: form.isActive
      }
    : {
        roleId: Number(form.roleId),
        name: form.name.trim(),
        lastName: form.lastName.trim(),
        email: form.email.trim(),
        password: form.password,
        phone: form.phone.trim() || null
      }

  const ok = await save(payload, editing.value?.userId ?? null)
  if (ok) modalOpen.value = false
}

onMounted(async () => {
  await load()
  try {
    roles.value = (await getRoles()) ?? []
  } catch (e) {
    notifyApiError(e, 'No pudimos cargar los roles')
  }
})
</script>

<template>
  <div>
    <PageHeader
      eyebrow="Administración"
      title="Usuarios"
      description="Todas las cuentas del sistema. El correo y el rol se definen al crear la cuenta y no pueden cambiarse después."
    >
      <template #actions>
        <BaseButton @click="openCreate">Nuevo usuario</BaseButton>
      </template>
    </PageHeader>

    <DataTable
      :columns="COLUMNS"
      :rows="filteredUsers"
      row-key="userId"
      :loading="loading"
      search-placeholder="Buscar por nombre, correo o teléfono..."
      empty-title="Aún no hay usuarios"
    >
      <template #filters>
        <select
          v-model="roleFilter"
          aria-label="Filtrar por rol"
          class="px-3 py-2.5 text-sm rounded-lg border border-gray-200 bg-white outline-none transition-all focus:ring-2 focus:ring-aurea focus:border-transparent cursor-pointer"
        >
          <option value="">Todos los roles</option>
          <option v-for="opt in roleOptions" :key="opt.value" :value="opt.value">
            {{ opt.label }}
          </option>
        </select>
      </template>

      <template #cell-fullName="{ row }">
        <div class="flex items-center gap-3">
          <span
            class="h-8 w-8 shrink-0 rounded-full bg-aurea-tint text-aurea flex items-center justify-center text-xs font-bold"
            aria-hidden="true"
          >
            {{ (row.name?.[0] ?? '') + (row.lastName?.[0] ?? '') }}
          </span>
          <span class="font-semibold text-gray-800">{{ row.name }} {{ row.lastName }}</span>
        </div>
      </template>

      <template #cell-phoneNumber="{ value }">
        <span class="text-gray-500">{{ value || '—' }}</span>
      </template>

      <template #cell-role-name="{ value }">
        <BaseBadge variant="bg-gray-100 text-gray-700 border-gray-200">{{ value }}</BaseBadge>
      </template>

      <template #cell-registerDate="{ value }">
        <span class="text-gray-500 text-xs">{{ formatDate(value) }}</span>
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
            @click="confirmRemove(row, (r) => `La cuenta de ${r.name} ${r.lastName}`)"
          >
            Desactivar
          </BaseButton>
        </div>
      </template>

      <template #empty-action>
        <BaseButton @click="openCreate">Nuevo usuario</BaseButton>
      </template>
    </DataTable>

    <BaseModal
      v-model="modalOpen"
      :title="editing ? 'Editar usuario' : 'Nuevo usuario'"
      :subtitle="editing ? 'El correo y el rol no se pueden modificar.' : ''"
    >
      <form id="user-form" class="space-y-5" @submit.prevent="handleSubmit">
        <div class="grid sm:grid-cols-2 gap-5">
          <BaseInput v-model="form.name" label="Nombre" required maxlength="100" />
          <BaseInput v-model="form.lastName" label="Apellido" required maxlength="100" />
        </div>

        <BaseInput
          v-model="form.email"
          label="Correo electrónico"
          type="email"
          placeholder="ejemplo@correo.com"
          :required="!editing"
          :disabled="!!editing"
        />

        <BaseInput
          v-if="!editing"
          v-model="form.password"
          label="Contraseña"
          type="password"
          placeholder="••••••••"
          hint="Mínimo 8 caracteres."
          minlength="8"
          required
        />

        <BaseSelect
          v-if="!editing"
          v-model="form.roleId"
          label="Rol"
          :options="roleOptions"
          placeholder="Selecciona un rol"
          required
        />

        <BaseInput
          v-model="form.phone"
          label="Teléfono"
          type="tel"
          placeholder="+1 (809) 000-0000"
        />

        <BaseCheckbox
          v-if="editing"
          v-model="form.isActive"
          label="Cuenta activa"
          hint="Una cuenta inactiva no puede iniciar sesión."
        />
      </form>

      <template #footer>
        <BaseButton variant="secondary" @click="modalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="user-form" :loading="saving">
          {{ editing ? 'Guardar cambios' : 'Crear usuario' }}
        </BaseButton>
      </template>
    </BaseModal>
  </div>
</template>
