<script setup lang="ts">
import ToggelSwitch from '@/components/general/ToggelSwitch.vue'
import MonthOverview from '../components/general/MonthOverview.vue'
import { useI18n } from 'vue-i18n'
import { storeToRefs } from 'pinia'
import useYearOptions from '@/composables/useYearOptions'
import { useSettingsStore } from '@/stores/useSettingsStore'
import { useInvoiceStore } from '../stores/useInvoiceStore.ts'

// variables
const { t } = useI18n()

//stores
const settingsStore = useSettingsStore()
const { getYear: year } = storeToRefs(settingsStore)
const invoiceStore = useInvoiceStore()
const { getMonthTotals: monthTotals } = storeToRefs(invoiceStore)

// composables
const { yearOptions } = useYearOptions()

// functions
const updateYear = (value: string) => {
  settingsStore.selectYear(value)
}
const switchToggeled = (value: Boolean) => {
  console.log('switchToggeled', value)
}
</script>

<template>
  <div class="pt-5">
    <h1 class="pb-4 d-flex justify-content-center">{{ t('general.dashboard') }}</h1>
    <div class="d-flex flex-column flex-sm-row gap-4 gap-sm-5 pt-5 pb-3">
      <div class="col">
        <!-- year -->
        <SingleSelect
          :select-options="yearOptions"
          :selected="year"
          @update:selected="updateYear"
          :placeholder="t('general.selectYear')"
        />
      </div>
      <div class="col d-flex justify-content-start justify-content-sm-end">
        <ToggelSwitch
          :TextBefore="t('general.total')"
          :TextAfter="t('general.perPerson')"
          :checked="false"
          @clicked="switchToggeled"
        />
      </div>
    </div>
    <div class="border">
      <ColumnChart
        :series="monthTotals"
        :categories="[
          'Jan',
          'Feb',
          'Mar',
          'Apr',
          'May',
          'Jun',
          'Jul',
          'Aug',
          'Sep',
          'Oct',
          'Nov',
          'Dec'
        ]"
        :chart-title="t('dashboard.monthlyInvoices')"
        :series-title="t('general.total')"
      />
    </div>
    <div class="d-flex flex-column flex-md-row gap-3 pt-3">
      <MonthOverview :month="t('general.january')" :sum="1234" />
      <MonthOverview :month="t('general.february')" :sum="8734" />
      <MonthOverview :month="t('general.march')" :sum="8734" />
    </div>
    <div class="d-flex flex-column flex-md-row gap-3 pt-3">
      <MonthOverview :month="t('general.april')" :sum="8734" />
      <MonthOverview :month="t('general.may')" :sum="8734" />
      <MonthOverview :month="t('general.june')" :sum="8734" />
    </div>
    <div class="d-flex flex-column flex-md-row gap-3 pt-3">
      <MonthOverview :month="t('general.july')" :sum="8734" />
      <MonthOverview :month="t('general.august')" :sum="8734" />
      <MonthOverview :month="t('general.september')" :sum="8734" />
    </div>
    <div class="d-flex flex-column flex-md-row gap-3 pt-3">
      <MonthOverview :month="t('general.october')" :sum="8734" />
      <MonthOverview :month="t('general.november')" :sum="873" />
      <MonthOverview :month="t('general.december')" :sum="8734" />
    </div>
  </div>
</template>
