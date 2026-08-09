<script setup>
import { computed, useId } from 'vue'

const props = defineProps({
  modelValue: { type: String, default: '' },
  label: { type: String, default: '' },
  placeholder: { type: String, default: '' },
  rows: { type: [String, Number], default: 4 },
  hint: { type: String, default: '' },
  error: { type: String, default: '' },
  required: { type: Boolean, default: false },
  disabled: { type: Boolean, default: false },
  maxlength: { type: [String, Number], default: undefined }
})

defineEmits(['update:modelValue'])

const id = useId()

const classes = computed(() => [
  'w-full px-4 py-3 rounded-lg border outline-none transition-all resize-y',
  'focus:ring-2 focus:ring-aurea focus:border-transparent',
  'disabled:bg-gray-50 disabled:text-gray-400 disabled:cursor-not-allowed',
  props.error ? 'border-red-400' : 'border-gray-300'
])

const counter = computed(() =>
  props.maxlength ? `${props.modelValue?.length ?? 0}/${props.maxlength}` : ''
)
</script>

<template>
  <div>
    <label v-if="label" :for="id" class="block text-sm font-medium text-gray-700 mb-1">
      {{ label }}
      <span v-if="required" class="text-aurea">*</span>
    </label>

    <textarea
      :id="id"
      :value="modelValue"
      :placeholder="placeholder"
      :rows="rows"
      :required="required"
      :disabled="disabled"
      :maxlength="maxlength"
      :aria-invalid="!!error"
      :class="classes"
      @input="$emit('update:modelValue', $event.target.value)"
    />

    <div class="mt-1 flex justify-between gap-4">
      <p v-if="error" class="text-xs text-red-600">{{ error }}</p>
      <p v-else-if="hint" class="text-xs text-gray-400">{{ hint }}</p>
      <span v-if="counter" class="ml-auto text-xs text-gray-300 shrink-0">{{ counter }}</span>
    </div>
  </div>
</template>
