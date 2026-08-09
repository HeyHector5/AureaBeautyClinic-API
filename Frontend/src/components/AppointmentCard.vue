<script setup>
import BaseBadge from '@/components/ui/BaseBadge.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import { stateBadgeClass, stateLabel } from '@/constants/appointment'
import { formatDateTime } from '@/utils/format'

defineProps({
  appointment: { type: Object, required: true },
  cancellable: { type: Boolean, default: false }
})

defineEmits(['cancel'])
</script>

<template>
  <article
    class="border border-gray-100 rounded-2xl bg-white p-6 transition-all duration-300 hover:border-aurea hover:shadow-lg"
  >
    <div class="flex items-start justify-between gap-4 mb-4">
      <div class="min-w-0">
        <p class="text-xs font-bold tracking-[0.15em] uppercase text-gray-400 mb-1">
          {{ appointment.doctor?.specialty?.name }}
        </p>
        <h3 class="text-xl font-serif font-light text-gray-800">
          {{ formatDateTime(appointment.scheduled) }}
        </h3>
      </div>
      <BaseBadge :variant="stateBadgeClass(appointment.state)">
        {{ stateLabel(appointment.state) }}
      </BaseBadge>
    </div>

    <div class="flex items-center gap-3 mb-4">
      <img
        v-if="appointment.doctor?.photoURL"
        :src="appointment.doctor.photoURL"
        :alt="`Foto de ${appointment.doctor?.user?.name}`"
        class="h-10 w-10 rounded-full object-cover"
      />
      <span
        v-else
        class="h-10 w-10 rounded-full bg-aurea-tint text-aurea flex items-center justify-center text-xs font-bold"
        aria-hidden="true"
      >
        {{ (appointment.doctor?.user?.name?.[0] ?? '') + (appointment.doctor?.user?.lastName?.[0] ?? '') }}
      </span>
      <div class="min-w-0">
        <p class="text-sm font-semibold text-gray-800 truncate">
          {{ appointment.doctor?.user?.name }} {{ appointment.doctor?.user?.lastName }}
        </p>
        <p class="text-xs text-gray-400">Tu especialista</p>
      </div>
    </div>

    <p v-if="appointment.notes" class="text-sm text-gray-500 border-t border-gray-50 pt-4">
      {{ appointment.notes }}
    </p>

    <div v-if="cancellable" class="mt-4 pt-4 border-t border-gray-50 flex justify-end">
      <BaseButton variant="danger" size="sm" @click="$emit('cancel', appointment)">
        Cancelar cita
      </BaseButton>
    </div>
  </article>
</template>
