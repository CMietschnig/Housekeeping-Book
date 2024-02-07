<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import useMonthOptions from '../composables/useMonthOptions.ts'
import useYearOptions from '@/composables/useYearOptions'
import { storeToRefs } from 'pinia'
import { ref, onBeforeMount, watch } from 'vue'
import { useSettingsStore } from '@/stores/useSettingsStore'
import type { IUpdateSettings } from '@/interfaces/IUpdateSettings'

// stores
const settingsStore = useSettingsStore()
const {
  getMonthId: month,
  getYear: year,
  getContributionMembersCount: savedContributionMembersCount
} = storeToRefs(settingsStore)

//composables
const { monthOptions } = useMonthOptions()
const { yearOptions } = useYearOptions()

onBeforeMount(() => {
  // only one user so id is hardcoded
  settingsStore.getSettingsById(1)
})
// variables
const { t } = useI18n()
const contributionMembersCount = ref(savedContributionMembersCount.value)

// update contributionMembersCount
watch(savedContributionMembersCount, (newCount) => {
  contributionMembersCount.value = newCount
})

// functions
const updateSettingsById = (value: { id: number; number: number }) => {
  // add year and month from select
  const updateSettingsModel: IUpdateSettings = {
    SettingsId: value.id,
    ContributionMembersCount: value.number,
    Year: '2023',
    MonthId: 1
  }
  settingsStore.updateSettingsById(updateSettingsModel)
}
const updateMonth = (value: number) => {
  // not implemented delete from data base
  console.log('settings select default month', value)
}
const updateYear = (value: number) => {
  // not implemented delete from data base
  console.log('settings select default year', value)
}
</script>

<template>
  <div class="pt-5">
    <h1 class="pb-4 d-flex justify-content-center">{{ t('general.settings') }}</h1>
    <div class="d-flex flex-wrap justify-content-between gap-4">
      <div>
        <span>{{ t('settings.contributionMembersCount') }}</span>
        <NumberInput
          v-model:number="contributionMembersCount"
          :id="1"
          :only-addable="false"
          :can-delete="false"
          :display-rounded-number="false"
          :placeholder="t('settings.contributionMembers')"
          @update-number="updateSettingsById"
        />
      </div>
      <div>
        <span>{{ t('settings.preferredColorMode') }}</span>
        <!-- Add the correct single select
          <SingleSelect
          :select-options="monthOptions"
          :selected="month"
          @update:selected="updateMonth"
          :placeholder="t('general.selectMonth')"
        />-->
      </div>
    </div>
  </div>
</template>
