<script setup>
defineProps({
  title: { type: String, default: 'Nada por aquí todavía' },
  description: { type: String, default: '' }
})
</script>

<template>
  <div class="flex flex-col items-center justify-center gap-3 py-16 px-6 text-center">
    <div class="h-14 w-14 rounded-full bg-aurea-tint flex items-center justify-center">
      <svg
        class="h-6 w-6 text-aurea"
        fill="none"
        stroke="currentColor"
        stroke-width="1.5"
        viewBox="0 0 24 24"
        aria-hidden="true"
      >
        <path
          stroke-linecap="round"
          stroke-linejoin="round"
          d="M20 13V6a2 2 0 00-2-2H6a2 2 0 00-2 2v7m16 0v5a2 2 0 01-2 2H6a2 2 0 01-2-2v-5m16 0h-2.586a1 1 0 00-.707.293l-2.414 2.414a1 1 0 01-.707.293h-3.172a1 1 0 01-.707-.293l-2.414-2.414A1 1 0 006.586 13H4"
        />
      </svg>
    </div>
    <h3 class="text-lg font-serif font-light text-gray-800">{{ title }}</h3>
    <p v-if="description" class="text-sm text-gray-500 max-w-sm">{{ description }}</p>
    <div class="mt-2">
      <slot />
    </div>
  </div>
</template>
