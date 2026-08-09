<script setup>
defineProps({
  /** Clases Tailwind del chip; por defecto, un gris neutro. */
  variant: { type: String, default: 'bg-gray-100 text-gray-600 border-gray-200' }
})
</script>

<template>
  <span
    :class="[
      'inline-flex items-center px-3 py-1 rounded-full border text-xs font-semibold whitespace-nowrap',
      variant
    ]"
  >
    <slot />
  </span>
</template>
