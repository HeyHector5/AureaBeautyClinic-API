<script setup>
defineProps({
  label: { type: String, required: true },
  value: { type: [String, Number], default: '—' },
  hint: { type: String, default: '' },
  loading: { type: Boolean, default: false }
})
</script>

<template>
  <div
    class="border border-gray-100 rounded-xl bg-white p-6 transition-all duration-300 hover:border-aurea hover:shadow-lg"
  >
    <div class="flex items-start justify-between gap-4">
      <div class="min-w-0">
        <p class="text-xs font-bold tracking-[0.15em] uppercase text-gray-400">{{ label }}</p>
        <p v-if="loading" class="mt-3 h-8 w-16 rounded bg-gray-100 animate-pulse" />
        <p v-else class="mt-2 text-3xl font-serif font-light text-gray-800">{{ value }}</p>
        <p v-if="hint && !loading" class="mt-1 text-xs text-gray-400">{{ hint }}</p>
      </div>

      <div class="h-10 w-10 shrink-0 rounded-full bg-aurea-tint flex items-center justify-center text-aurea">
        <slot name="icon">
          <svg class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="1.8" viewBox="0 0 24 24" aria-hidden="true">
            <path stroke-linecap="round" stroke-linejoin="round" d="M3 17l6-6 4 4 8-8" />
          </svg>
        </slot>
      </div>
    </div>
  </div>
</template>
