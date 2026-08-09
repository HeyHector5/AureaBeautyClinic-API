<script setup>
import { computed } from 'vue'

const props = defineProps({
  variant: { type: String, default: 'primary' }, // primary | secondary | ghost | danger
  size: { type: String, default: 'md' }, // sm | md | lg
  type: { type: String, default: 'button' },
  loading: { type: Boolean, default: false },
  disabled: { type: Boolean, default: false },
  block: { type: Boolean, default: false }
})

const VARIANTS = {
  primary: 'bg-aurea hover:bg-aurea-dark text-white shadow-lg',
  secondary: 'bg-white border border-gray-300 text-gray-700 hover:border-aurea hover:text-aurea',
  ghost: 'bg-transparent text-gray-600 hover:bg-aurea-tint hover:text-aurea',
  danger: 'bg-white border border-red-200 text-red-600 hover:bg-red-50'
}

const SIZES = {
  sm: 'px-3 py-1.5 text-xs',
  md: 'px-5 py-2.5 text-sm',
  lg: 'w-full py-3 text-base'
}

const classes = computed(() => [
  'inline-flex items-center justify-center gap-2 font-bold rounded-lg transition-all duration-300 cursor-pointer',
  'disabled:opacity-50 disabled:cursor-not-allowed',
  'focus:outline-none focus-visible:ring-2 focus-visible:ring-aurea focus-visible:ring-offset-2',
  VARIANTS[props.variant] ?? VARIANTS.primary,
  SIZES[props.size] ?? SIZES.md,
  props.block ? 'w-full' : ''
])
</script>

<template>
  <button :type="type" :disabled="disabled || loading" :class="classes">
    <svg
      v-if="loading"
      class="h-4 w-4 animate-spin"
      viewBox="0 0 24 24"
      fill="none"
      aria-hidden="true"
    >
      <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4" />
      <path
        class="opacity-75"
        fill="currentColor"
        d="M4 12a8 8 0 018-8V0C5.4 0 0 5.4 0 12h4z"
      />
    </svg>
    <slot />
  </button>
</template>
