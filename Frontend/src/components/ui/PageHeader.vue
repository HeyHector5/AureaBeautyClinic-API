<script setup>
defineProps({
  eyebrow: { type: String, default: '' },
  title: { type: String, required: true },
  description: { type: String, default: '' }
})
</script>

<template>
  <div class="flex flex-col md:flex-row md:items-end md:justify-between gap-4 mb-8">
    <div>
      <span
        v-if="eyebrow"
        class="block text-aurea text-xs font-bold tracking-[0.2em] uppercase mb-2"
      >
        {{ eyebrow }}
      </span>
      <h1 class="text-3xl md:text-4xl font-serif font-light tracking-tight text-gray-800">
        {{ title }}
      </h1>
      <p v-if="description" class="text-sm text-gray-500 mt-2 max-w-2xl">{{ description }}</p>
    </div>

    <div class="flex items-center gap-3 shrink-0">
      <slot name="actions" />
    </div>
  </div>
</template>
