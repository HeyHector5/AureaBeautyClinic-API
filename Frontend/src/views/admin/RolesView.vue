<script setup>
import { onMounted, reactive, ref } from 'vue'
import PageHeader from '@/components/ui/PageHeader.vue'
import DataTable from '@/components/ui/DataTable.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseModal from '@/components/ui/BaseModal.vue'
import BaseInput from '@/components/ui/BaseInput.vue'
import BaseTextarea from '@/components/ui/BaseTextarea.vue'
import BaseCheckbox from '@/components/ui/BaseCheckbox.vue'
import BaseBadge from '@/components/ui/BaseBadge.vue'
import { useCrud } from '@/composables/useCrud'
import { getRoles, createRole, updateRole, deactivateRole } from '@/services/roleService'

/**
 * Roles semilla del sistema. Se pueden renombrar/describir, pero desactivarlos
 * dejaría sin acceso a los usuarios asignados, así que la acción se bloquea.
 */
const SYSTEM_ROLES = ['Admin', 'Doctor', 'Patient']
const isSystemRole = (row) => SYSTEM_ROLES.includes(row.name)

const { items, loading, saving, load, save, confirmRemove } = useCrud({
  fetchAll: getRoles,
  create: createRole,
  update: updateRole,
  remove: deactivateRole,
  idKey: 'roleId',
  labels: { singular: 'Rol', plural: 'los roles' }
})

const COLUMNS = [
  { key: 'roleId', label: '#', width: '70px' },
  { key: 'name', label: 'Nombre' },
  { key: 'description', label: 'Descripción', sortable: false },
  { key: 'isActive', label: 'Estado', width: '120px' },
  { key: 'actions', label: 'Acciones', sortable: false, searchable: false, align: 'right', width: '190px' }
]

const modalOpen = ref(false)
const editingId = ref(null)
const form = reactive({ name: '', description: '', isActive: true })

const openCreate = () => {
  editingId.value = null
  Object.assign(form, { name: '', description: '', isActive: true })
  modalOpen.value = true
}

const openEdit = (row) => {
  editingId.value = row.roleId
  Object.assign(form, {
    name: row.name ?? '',
    description: row.description ?? '',
    isActive: row.isActive ?? true
  })
  modalOpen.value = true
}

const handleSubmit = async () => {
  const payload = {
    name: form.name.trim(),
    description: form.description.trim() || null,
    isActive: form.isActive
  }
  const ok = await save(payload, editingId.value)
  if (ok) modalOpen.value = false
}

onMounted(load)
</script>

<template>
  <div>
    <PageHeader
      eyebrow="Seguridad"
      title="Roles"
      description="Definen qué puede hacer cada usuario dentro del sistema. Los roles Admin, Doctor y Patient son parte del sistema y no pueden desactivarse."
    >
      <template #actions>
        <BaseButton @click="openCreate">Nuevo rol</BaseButton>
      </template>
    </PageHeader>

    <DataTable
      :columns="COLUMNS"
      :rows="items"
      row-key="roleId"
      :loading="loading"
      search-placeholder="Buscar por nombre..."
      empty-title="Aún no hay roles"
    >
      <template #cell-roleId="{ value }">
        <span class="text-gray-400">{{ value }}</span>
      </template>

      <template #cell-name="{ row }">
        <div class="flex items-center gap-2">
          <span class="font-semibold text-gray-800">{{ row.name }}</span>
          <BaseBadge v-if="isSystemRole(row)" variant="bg-aurea-tint text-aurea border-red-200">
            Sistema
          </BaseBadge>
        </div>
      </template>

      <template #cell-description="{ value }">
        <span class="text-gray-500 line-clamp-2">{{ value || '—' }}</span>
      </template>

      <template #cell-isActive="{ value }">
        <BaseBadge
          :variant="
            value !== false
              ? 'bg-emerald-50 text-emerald-700 border-emerald-200'
              : 'bg-gray-100 text-gray-500 border-gray-200'
          "
        >
          {{ value !== false ? 'Activo' : 'Inactivo' }}
        </BaseBadge>
      </template>

      <template #cell-actions="{ row }">
        <div class="flex justify-end gap-2">
          <BaseButton variant="secondary" size="sm" @click="openEdit(row)">Editar</BaseButton>
          <BaseButton
            v-if="row.isActive !== false && !isSystemRole(row)"
            variant="danger"
            size="sm"
            @click="confirmRemove(row, (r) => `El rol «${r.name}»`)"
          >
            Desactivar
          </BaseButton>
        </div>
      </template>

      <template #empty-action>
        <BaseButton @click="openCreate">Nuevo rol</BaseButton>
      </template>
    </DataTable>

    <BaseModal v-model="modalOpen" :title="editingId ? 'Editar rol' : 'Nuevo rol'">
      <form id="role-form" class="space-y-5" @submit.prevent="handleSubmit">
        <BaseInput
          v-model="form.name"
          label="Nombre"
          placeholder="Ej. Recepcionista"
          required
          maxlength="100"
        />
        <BaseTextarea
          v-model="form.description"
          label="Descripción"
          placeholder="¿Qué puede hacer este rol?"
          maxlength="500"
        />
        <BaseCheckbox v-model="form.isActive" label="Rol activo" />
      </form>

      <template #footer>
        <BaseButton variant="secondary" @click="modalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="role-form" :loading="saving">
          {{ editingId ? 'Guardar cambios' : 'Crear rol' }}
        </BaseButton>
      </template>
    </BaseModal>
  </div>
</template>
