<template>
  <footer class="bg-gray-900 text-gray-300 pt-16 pb-8 border-t border-gray-800">
    <div class="max-w-7xl mx-auto px-8 grid grid-cols-1 md:grid-cols-4 gap-12 mb-12">
      
      <div class="space-y-4">
        <span class="text-2xl font-bold tracking-tighter text-white">
          <span class="text-[#FF3B30]">AUREA</span>
          <span class="text-gray-400 font-light ml-1 uppercase text-sm tracking-widest block md:inline">Clinic</span>
        </span>
        <p class="text-sm text-gray-400 leading-relaxed">
          Tu bienestar y belleza natural en manos de profesionales certificados.
        </p>
      </div>

      <div>
        <h4 class="text-white font-bold tracking-widest uppercase text-xs mb-4 text-[#FF3B30]">Navegación</h4>
        <ul class="space-y-2 text-sm">
          <li>
           
            <button 
              @click.prevent="$emit('change-view', 'home')" 
              class="hover:text-white transition-colors cursor-pointer text-left bg-transparent border-none p-0 outline-none"
            >
              Inicio
            </button>
          </li>
          <li><a href="#" class="hover:text-white transition-colors cursor-pointer">Servicios</a></li>
          <li><a href="#" class="hover:text-white transition-colors cursor-pointer">Nosotros</a></li>
          <li><a href="#" class="hover:text-white transition-colors cursor-pointer">Contactos</a></li>
        </ul>
      </div>

      <div>
        <h4 class="text-white font-bold tracking-widest uppercase text-xs mb-4 text-[#FF3B30]">Horarios</h4>
        <ul class="space-y-2 text-sm text-gray-400">
          <li><span class="font-semibold text-gray-300">Lunes - Viernes:</span> 9:00 AM - 7:00 PM</li>
          <li><span class="font-semibold text-gray-300">Sábados:</span> 9:00 AM - 4:00 PM</li>
          <li><span class="font-semibold text-gray-300">Domingos:</span> Cerrado</li>
        </ul>
      </div>

      <div>
        <h4 class="text-white font-bold tracking-widest uppercase text-xs mb-4 text-[#FF3B30]">Contacto</h4>
        <ul class="space-y-2 text-sm text-gray-400">
          <li>📍 Av. Maximo Gomez, Distrito Nacional, RD</li>
          <li>📞 829-619-8213</li>
          <li>✉️ info@aureabeauty.com</li>
        </ul>
      </div>

    </div>

    <div class="max-w-7xl mx-auto px-8 pt-8 border-t border-gray-800 text-center md:flex md:justify-between text-xs text-gray-500">
      <p>&copy; 2026 Aurea Beauty Clinic. Todos los derechos reservados.</p>
      <p class="mt-2 md:mt-0">Diseñado con excelencia médica y estética.</p>
    </div>
  </footer>
</template>

<script setup>
defineEmits(['change-view']);
</script>