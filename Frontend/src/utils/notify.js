import Swal from 'sweetalert2'
import { BRAND_RED } from '@/constants/clinic'

/**
 * Envoltorios de SweetAlert2 con el color de marca aplicado una sola vez.
 * Antes `confirmButtonColor: '#FF3B30'` estaba repetido en cada componente.
 */

export const notifySuccess = (title, text) =>
  Swal.fire({
    icon: 'success',
    title,
    text,
    showConfirmButton: false,
    timer: 2000,
    timerProgressBar: true
  })

export const notifyError = (title, text) =>
  Swal.fire({
    icon: 'error',
    title,
    text,
    confirmButtonColor: BRAND_RED
  })

export const notifyWarning = (title, text) =>
  Swal.fire({
    icon: 'warning',
    title,
    text,
    confirmButtonColor: BRAND_RED
  })

/** Toast discreto en la esquina, para acciones rápidas dentro del panel. */
export const toast = (title, icon = 'success') =>
  Swal.fire({
    toast: true,
    position: 'top-end',
    icon,
    title,
    showConfirmButton: false,
    timer: 2500,
    timerProgressBar: true
  })

/**
 * Confirmación para acciones destructivas. Devuelve true si el usuario confirma.
 * Nota: en esta app "eliminar" siempre es un soft delete (desactivar / cancelar).
 */
export const confirmAction = async ({
  title,
  text,
  confirmText = 'Sí, continuar',
  cancelText = 'Cancelar',
  icon = 'warning'
}) => {
  const result = await Swal.fire({
    icon,
    title,
    text,
    showCancelButton: true,
    confirmButtonText: confirmText,
    cancelButtonText: cancelText,
    confirmButtonColor: BRAND_RED,
    cancelButtonColor: '#9ca3af',
    reverseButtons: true
  })
  return result.isConfirmed
}

/** Muestra el mensaje de un ApiError, incluyendo los errores de validación. */
export const notifyApiError = (error, fallbackTitle = 'Ocurrió un error') =>
  notifyError(fallbackTitle, error?.detail || error?.message || 'Inténtalo de nuevo más tarde.')
