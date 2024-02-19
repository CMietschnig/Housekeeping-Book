import { useI18n } from 'vue-i18n'
import { computed, ref } from 'vue'
import type { ISelectOption } from '@/interfaces/ISelectOption'
import { Month } from '@/enums/MonthEnums'

export default function useMonthOptions() {
  const { t } = useI18n()

  const monthOptions = ref<ISelectOption[]>([
    {
      value: Month.January,
      text: computed(() => t('general.january'))
    },
    {
      value: Month.February,
      text: computed(() => t('general.february'))
    },
    {
      value: Month.March,
      text: computed(() => t('general.march'))
    },
    {
      value: Month.April,
      text: computed(() => t('general.april'))
    },
    {
      value: Month.May,
      text: computed(() => t('general.may'))
    },
    {
      value: Month.June,
      text: computed(() => t('general.june'))
    },
    {
      value: Month.July,
      text: computed(() => t('general.july'))
    },
    {
      value: Month.August,
      text: computed(() => t('general.august'))
    },
    {
      value: Month.September,
      text: computed(() => t('general.september'))
    },
    {
      value: Month.October,
      text: computed(() => t('general.october'))
    },
    {
      value: Month.November,
      text: computed(() => t('general.november'))
    },
    {
      value: Month.December,
      text: computed(() => t('general.december'))
    }
  ])

  const getTextByValue = (value: number): string | undefined => {
    const option: ISelectOption | undefined = monthOptions.value.find(
      (option) => option.value === value
    )

    return option?.text.toString()
  }

  return {
    monthOptions,
    getTextByValue
  }
}
