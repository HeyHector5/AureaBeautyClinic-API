import { ref } from 'vue'
import { confirmAction, notifyApiError, toast } from '@/utils/notify'

/**
 * Lógica compartida por los módulos administrativos: cargar la lista, crear,
 * editar y desactivar (soft delete), con confirmación y toasts.
 *
 * @param {object}   options
 * @param {Function} options.fetchAll  () => Promise<item[]>
 * @param {Function} [options.create]  (payload) => Promise<item>
 * @param {Function} [options.update]  (id, payload) => Promise<item>
 * @param {Function} [options.remove]  (id) => Promise<item>   soft delete
 * @param {string}   options.idKey     nombre del campo identificador, p. ej. 'userId'
 * @param {object}   options.labels    { singular, plural, removeTitle?, removeText?, removeConfirm?, removed? }
 */
export function useCrud({ fetchAll, create, update, remove, idKey, labels }) {
  const items = ref([])
  const loading = ref(false)
  const saving = ref(false)
  const error = ref('')

  const load = async () => {
    loading.value = true
    error.value = ''
    try {
      items.value = (await fetchAll()) ?? []
    } catch (e) {
      error.value = e?.detail || e?.message || 'No pudimos cargar la información.'
      items.value = []
      notifyApiError(e, `No pudimos cargar ${labels.plural}`)
    } finally {
      loading.value = false
    }
  }

  /**
   * Crea (si `id` es null) o actualiza. Recarga la lista al terminar.
   * Devuelve true si la operación fue exitosa.
   */
  const save = async (payload, id = null) => {
    saving.value = true
    try {
      if (id == null) {
        await create(payload)
        toast(`${labels.singular} creado correctamente.`)
      } else {
        await update(id, payload)
        toast(`${labels.singular} actualizado correctamente.`)
      }
      await load()
      return true
    } catch (e) {
      notifyApiError(e, 'No pudimos guardar los cambios')
      return false
    } finally {
      saving.value = false
    }
  }

  /**
   * Soft delete con confirmación. En esta app nunca se borra una fila:
   * se marca como inactiva (o la cita pasa a "Cancelled").
   */
  const confirmRemove = async (row, describe = (r) => r?.name ?? '') => {
    const confirmed = await confirmAction({
      title: labels.removeTitle ?? `¿Desactivar ${labels.singular.toLowerCase()}?`,
      text:
        labels.removeText ??
        `${describe(row)} dejará de estar disponible. El registro no se elimina y puedes reactivarlo después.`,
      confirmText: labels.removeConfirm ?? 'Sí, desactivar'
    })
    if (!confirmed) return false

    try {
      await remove(row[idKey])
      toast(labels.removed ?? `${labels.singular} desactivado.`)
      await load()
      return true
    } catch (e) {
      notifyApiError(e, 'No pudimos completar la acción')
      return false
    }
  }

  return { items, loading, saving, error, load, save, confirmRemove }
}
