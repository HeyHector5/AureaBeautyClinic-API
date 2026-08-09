<script setup>
import { useId } from 'vue'

defineProps({
  modelValue: { type: Boolean, default: false },
  label: { type: String, default: '' },
  hint: { type: String, default: '' },
  disabled: { type: Boolean, default: false }
})

defineEmits(['update:modelValue'])

const id = useId()
</script>

<template>
  <div class="flex items-start gap-3">
    <input
      :id="id"
      type="checkbox"
      :checked="modelValue"
      :disabled="disabled"
      class="mt-0.5 h-4 w-4 rounded border-gray-300 text-aurea accent-aurea focus:ring-2 focus:ring-aurea cursor-pointer disabled:cursor-not-allowed"
      @change="$emit('update:modelValue', $event.target.checked)"
    />
    <label :for="id" class="cursor-pointer select-none">
      <span class="block text-sm font-medium text-gray-700">{{ label }}</span>
      <span v-if="hint" class="block text-xs text-gray-400">{{ hint }}</span>
    </label>
  </div>
</template>
