<script setup>
import { onBeforeUnmount, onMounted, watch } from 'vue'

const props = defineProps({
  modelValue: { type: Boolean, default: false },
  title: { type: String, default: '' },
  subtitle: { type: String, default: '' },
  size: { type: String, default: 'md' } // sm | md | lg
})

const emit = defineEmits(['update:modelValue'])

const SIZES = {
  sm: 'max-w-sm',
  md: 'max-w-lg',
  lg: 'max-w-3xl'
}

const close = () => emit('update:modelValue', false)

const onKeydown = (e) => {
  if (e.key === 'Escape' && props.modelValue) close()
}

// Evita que la página de fondo se desplace mientras el modal está abierto.
watch(
  () => props.modelValue,
  (open) => {
    document.body.style.overflow = open ? 'hidden' : ''
  }
)

onMounted(() => window.addEventListener('keydown', onKeydown))
onBeforeUnmount(() => {
  window.removeEventListener('keydown', onKeydown)
  document.body.style.overflow = ''
})
</script>

<template>
  <Teleport to="body">
    <Transition
      enter-active-class="transition-opacity duration-200"
      enter-from-class="opacity-0"
      leave-active-class="transition-opacity duration-150"
      leave-to-class="opacity-0"
    >
      <div
        v-if="modelValue"
        class="fixed inset-0 z-50 flex items-center justify-center bg-gray-900/50 backdrop-blur-sm p-4"
        role="dialog"
        aria-modal="true"
        @click.self="close"
      >
        <div
          :class="[
            'w-full bg-white rounded-2xl shadow-xl max-h-[90vh] flex flex-col',
            SIZES[size] ?? SIZES.md
          ]"
        >
          <header
            v-if="title || $slots.header"
            class="px-8 pt-8 pb-4 border-b border-gray-100 shrink-0"
          >
            <slot name="header">
              <h3 class="text-2xl font-serif font-light text-gray-800">{{ title }}</h3>
              <p v-if="subtitle" class="text-sm text-gray-500 mt-1">{{ subtitle }}</p>
            </slot>
          </header>

          <div class="px-8 py-6 overflow-y-auto">
            <slot />
          </div>

          <footer
            v-if="$slots.footer"
            class="px-8 pb-8 pt-4 border-t border-gray-100 flex justify-end gap-3 shrink-0"
          >
            <slot name="footer" />
          </footer>
        </div>
      </div>
    </Transition>
  </Teleport>
</template>
