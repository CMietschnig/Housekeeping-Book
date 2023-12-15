<script setup lang="ts">
import ToggelSwitch from '@/components/general/ToggelSwitch.vue'
import MonthOverview from '../components/general/MonthOverview.vue'
import { useI18n } from 'vue-i18n'

// variables
const { t } = useI18n()

// functions
const switchToggeled = (value: Boolean) => {
  console.log('switchToggeled', value)
}
</script>

<template>
  <h1>{{ t('general.dashboard') }}</h1>
  <ToggelSwitch
    :TextBefore="t('general.total')"
    :TextAfter="t('general.perPerson')"
    :checked="false"
    @clicked="switchToggeled"
  />
  <MonthOverview :month="t('general.january')" :sum="1234" />
  <MonthOverview :month="t('general.february')" :sum="8734" />
  <MonthOverview :month="t('general.march')" :sum="8734" />
  <MonthOverview :month="t('general.april')" :sum="8734" />
  <MonthOverview :month="t('general.may')" :sum="8734" />
  <MonthOverview :month="t('general.june')" :sum="8734" />
  <MonthOverview :month="t('general.july')" :sum="8734" />
  <MonthOverview :month="t('general.august')" :sum="8734" />
  <MonthOverview :month="t('general.september')" :sum="8734" />
  <MonthOverview :month="t('general.october')" :sum="8734" />
  <MonthOverview :month="t('general.november')" :sum="873" />
  <MonthOverview :month="t('general.december')" :sum="8734" />
</template>
