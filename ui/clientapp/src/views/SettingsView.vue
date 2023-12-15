<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import useMonthOptions from '../composables/useMonthOptions.ts'
import useYearOptions from '@/composables/useYearOptions';
import { storeToRefs } from 'pinia';
import { useSettingsStore } from '@/stores/useSettingsStore';

// variables
const { t } = useI18n()

// stores
const settingsStore = useSettingsStore();
const { getMonth: month , getYear: year, getPeople: people } = storeToRefs(settingsStore)

//composables
const { monthOptions } = useMonthOptions()
const { yearOptions } = useYearOptions()

// functions
const saveNumberOfPeople = (value: { id: number; number: number }) => {
  console.log('saveNumberOfPeople', value.id, value.number)
}
const updateMonth = (value: number) => {
  console.log("settings select default month", value)
}
const updateYear = (value: number) => {
  console.log("settings select default year", value)
}
</script>

<template>
  <h1>{{ t('general.settings') }}</h1>
  <div>
    <span>{{ t('settings.numberOfPeople') }}</span>
    <NumberInput
      v-model:number="people"
      id="people"
      :only-addable="false"
      :can-delete="false"
      :placeholder="t('settings.people')"
      @save-number="saveNumberOfPeople"
    />
  </div>
  <div>
    <span>{{ t('settings.currentSelectedMonth') }}</span>
    <SingleSelect :select-options="monthOptions" :selected="month" @update:selected="updateMonth" :placeholder="t('general.selectMonth')" />
  </div>
  <div>
    <span>{{ t('settings.currentSelectedYear') }}</span>
    <SingleSelect :select-options="yearOptions" :selected="year" @update:selected="updateYear" :placeholder="t('general.selectYear')" />
  </div>
</template>
