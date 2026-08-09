<script setup>
import { onMounted, reactive, ref } from 'vue'
import PageHeader from '@/components/ui/PageHeader.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseInput from '@/components/ui/BaseInput.vue'
import LoadingSpinner from '@/components/ui/LoadingSpinner.vue'
import { useAuth } from '@/composables/useAuth'
import { getUser, updateUser } from '@/services/userService'
import { formatDate } from '@/utils/format'
import { notifyApiError, toast } from '@/utils/notify'

const { userId, user, patchUser, initials } = useAuth()

const loading = ref(true)
const saving = ref(false)
const profile = ref(null)
const form = reactive({ name: '', lastName: '', phone: '' })

const hydrateForm = (data) => {
  profile.value = data
  Object.assign(form, {
    name: data.name ?? '',
    lastName: data.lastName ?? '',
    phone: data.phoneNumber ?? ''
  })
}

const handleSubmit = async () => {
  saving.value = true
  try {
    // El campo se llama `phone` en la petición, aunque el DTO lo devuelva como `phoneNumber`.
    const updated = await updateUser(userId.value, {
      name: form.name.trim(),
      lastName: form.lastName.trim(),
      phone: form.phone.trim() || null
    })
    hydrateForm(updated)
    patchUser(updated)
    toast('Tus datos fueron actualizados.')
  } catch (e) {
    notifyApiError(e, 'No pudimos guardar tus datos')
  } finally {
    saving.value = false
  }
}

onMounted(async () => {
  try {
    hydrateForm(await getUser(userId.value))
  } catch (e) {
    notifyApiError(e, 'No pudimos cargar tu perfil')
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <div>
    <PageHeader
      eyebrow="Mi cuenta"
      title="Mi perfil"
      description="Mantén tus datos al día para que podamos contactarte sobre tus citas."
    />

    <LoadingSpinner v-if="loading" />

    <div v-else class="grid gap-8 lg:grid-cols-3">
      <aside class="lg:col-span-1">
        <div class="border border-gray-100 rounded-2xl bg-white p-8 text-center">
          <div
            class="h-20 w-20 mx-auto rounded-full bg-aurea text-white flex items-center justify-center text-2xl font-bold"
            aria-hidden="true"
          >
            {{ initials }}
          </div>
          <h2 class="mt-4 text-xl font-serif font-light text-gray-800">
            {{ profile?.name }} {{ profile?.lastName }}
          </h2>
          <p class="text-sm text-gray-500">{{ profile?.email }}</p>

          <dl class="mt-6 pt-6 border-t border-gray-50 space-y-3 text-left">
            <div class="flex justify-between gap-4">
              <dt class="text-xs text-gray-400 uppercase tracking-widest">Rol</dt>
              <dd class="text-sm text-gray-700">{{ user?.role }}</dd>
            </div>
            <div class="flex justify-between gap-4">
              <dt class="text-xs text-gray-400 uppercase tracking-widest">Miembro desde</dt>
              <dd class="text-sm text-gray-700">{{ formatDate(profile?.registerDate) }}</dd>
            </div>
          </dl>
        </div>
      </aside>

      <div class="lg:col-span-2">
        <form
          class="border border-gray-100 rounded-2xl bg-white p-8 space-y-6"
          @submit.prevent="handleSubmit"
        >
          <div class="grid sm:grid-cols-2 gap-6">
            <BaseInput v-model="form.name" label="Nombre" required maxlength="100" />
            <BaseInput v-model="form.lastName" label="Apellido" required maxlength="100" />
          </div>

          <BaseInput
            v-model="form.phone"
            label="Teléfono"
            type="tel"
            placeholder="+1 (809) 000-0000"
          />

          <BaseInput
            :model-value="profile?.email"
            label="Correo electrónico"
            type="email"
            disabled
            hint="El correo no se puede modificar. Escríbenos si necesitas cambiarlo."
          />

          <div class="flex justify-end pt-2">
            <BaseButton type="submit" :loading="saving">Guardar cambios</BaseButton>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
