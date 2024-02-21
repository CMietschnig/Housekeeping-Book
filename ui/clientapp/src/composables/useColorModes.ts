import type { ISelectOption } from '@/interfaces/ISelectOption'
import { computed, ref } from 'vue'
import { useI18n } from 'vue-i18n'

export default function useColorModes() {
  const { t } = useI18n()

  const colorModes = ref<ISelectOption[]>([
    {
      value: 'light',
      text: computed(() => t('general.light'))
    },
    {
      value: 'dark',
      text: computed(() => t('general.dark'))
    }
  ])

  const getTextByValue = (value: string): string | undefined => {
    const mode: ISelectOption | undefined = colorModes.value.find((mode) => mode.value === value)

    return mode?.text.toString()
  }

  return {
    colorModes,
    getTextByValue
  }
}
