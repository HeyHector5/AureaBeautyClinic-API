/**
 * Datos de contacto y marca de la clínica.
 * Fuente única: antes estaban duplicados y en conflicto entre Footer.vue y Contact.vue.
 */

export const BRAND_RED = '#FF3B30'

export const CLINIC = {
  name: 'Aurea Beauty Clinic',
  shortName: 'AUREA',
  tagline: 'Beauty Clinic',
  phone: '+1 (829) 619-8213',
  phoneHref: 'tel:+18296198213',
  whatsapp: '18296198213',
  email: 'info@aureabeautyclinic.com',
  address: 'Av. Winston Churchill 1099, Santo Domingo, R.D.',
  hours: [
    { days: 'Lunes a Viernes', time: '9:00 AM – 7:00 PM' },
    { days: 'Sábados', time: '9:00 AM – 4:00 PM' },
    { days: 'Domingos', time: 'Cerrado' }
  ],
  social: {
    instagram: 'https://instagram.com/aureabeautyclinic',
    facebook: 'https://facebook.com/aureabeautyclinic'
  }
}
