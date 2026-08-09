<script setup>
import { computed, onMounted, reactive, ref } from 'vue'
import PageHeader from '@/components/ui/PageHeader.vue'
import DataTable from '@/components/ui/DataTable.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseModal from '@/components/ui/BaseModal.vue'
import BaseInput from '@/components/ui/BaseInput.vue'
import BaseTextarea from '@/components/ui/BaseTextarea.vue'
import BaseCheckbox from '@/components/ui/BaseCheckbox.vue'
import BaseBadge from '@/components/ui/BaseBadge.vue'
import { useCrud } from '@/composables/useCrud'
import {
  getSpecialties,
  createSpecialty,
  updateSpecialty,
  deactivateSpecialty
} from '@/services/specialtyService'

const { items, loading, saving, load, save, confirmRemove } = useCrud({
  fetchAll: getSpecialties,
  create: createSpecialty,
  update: updateSpecialty,
  remove: deactivateSpecialty,
  idKey: 'specialtyId',
  labels: { singular: 'Especialidad', plural: 'las especialidades' }
})

const COLUMNS = [
  { key: 'specialtyId', label: '#', width: '70px' },
  { key: 'name', label: 'Nombre' },
  { key: 'description', label: 'Descripción', sortable: false },
  { key: 'isActive', label: 'Estado', width: '120px' },
  { key: 'actions', label: 'Acciones', sortable: false, searchable: false, align: 'right', width: '190px' }
]

const modalOpen = ref(false)
const editingId = ref(null)
const form = reactive({ name: '', description: '', isActive: true })

const activeCount = computed(() => items.value.filter((s) => s.isActive).length)

const openCreate = () => {
  editingId.value = null
  Object.assign(form, { name: '', description: '', isActive: true })
  modalOpen.value = true
}

const openEdit = (row) => {
  editingId.value = row.specialtyId
  Object.assign(form, {
    name: row.name ?? '',
    description: row.description ?? '',
    isActive: row.isActive
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
      eyebrow="Catálogo"
      title="Especialidades"
      description="Los tratamientos que la clínica ofrece. Las especialidades activas son las que se muestran en la página pública de servicios."
    >
      <template #actions>
        <BaseButton @click="openCreate">Nueva especialidad</BaseButton>
      </template>
    </PageHeader>

    <DataTable
      :columns="COLUMNS"
      :rows="items"
      row-key="specialtyId"
      :loading="loading"
      search-placeholder="Buscar por nombre o descripción..."
      empty-title="Aún no hay especialidades"
      empty-description="Crea la primera especialidad para que aparezca en el sitio y puedas asignar doctores."
    >
      <template #filters>
        <span class="text-xs text-gray-400">
          {{ activeCount }} activa{{ activeCount === 1 ? '' : 's' }} de {{ items.length }}
        </span>
      </template>

      <template #cell-specialtyId="{ value }">
        <span class="text-gray-400">{{ value }}</span>
      </template>

      <template #cell-name="{ row }">
        <span class="font-semibold text-gray-800">{{ row.name }}</span>
      </template>

      <template #cell-description="{ value }">
        <span class="text-gray-500 line-clamp-2">{{ value || '—' }}</span>
      </template>

      <template #cell-isActive="{ value }">
        <BaseBadge
          :variant="
            value
              ? 'bg-emerald-50 text-emerald-700 border-emerald-200'
              : 'bg-gray-100 text-gray-500 border-gray-200'
          "
        >
          {{ value ? 'Activa' : 'Inactiva' }}
        </BaseBadge>
      </template>

      <template #cell-actions="{ row }">
        <div class="flex justify-end gap-2">
          <BaseButton variant="secondary" size="sm" @click="openEdit(row)">Editar</BaseButton>
          <BaseButton
            v-if="row.isActive"
            variant="danger"
            size="sm"
            @click="confirmRemove(row, (r) => `La especialidad «${r.name}»`)"
          >
            Desactivar
          </BaseButton>
        </div>
      </template>

      <template #empty-action>
        <BaseButton @click="openCreate">Nueva especialidad</BaseButton>
      </template>
    </DataTable>

    <BaseModal
      v-model="modalOpen"
      :title="editingId ? 'Editar especialidad' : 'Nueva especialidad'"
      subtitle="Los cambios se reflejan de inmediato en la página de servicios."
    >
      <form id="specialty-form" class="space-y-5" @submit.prevent="handleSubmit">
        <BaseInput
          v-model="form.name"
          label="Nombre"
          placeholder="Ej. Dermatología estética"
          required
          maxlength="100"
        />
        <BaseTextarea
          v-model="form.description"
          label="Descripción"
          placeholder="Describe brevemente en qué consiste el tratamiento."
          maxlength="500"
        />
        <BaseCheckbox
          v-model="form.isActive"
          label="Especialidad activa"
          hint="Sólo las activas se muestran en el sitio público."
        />
      </form>

      <template #footer>
        <BaseButton variant="secondary" @click="modalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="specialty-form" :loading="saving">
          {{ editingId ? 'Guardar cambios' : 'Crear especialidad' }}
        </BaseButton>
      </template>
    </BaseModal>
  </div>
</template>
