<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { storeToRefs } from 'pinia'
import { useInvoiceStore } from '../stores/useInvoiceStore.ts'
import { useSettingsStore } from '@/stores/useSettingsStore'
import useMonthOptions from '../composables/useMonthOptions.ts'
import useYearOptions from '@/composables/useYearOptions'
import { ref } from 'vue'

// variables
const { t } = useI18n()
const comment = ref('')

//stores
const invoiceStore = useInvoiceStore()
const { getInvoices: invoices } = storeToRefs(invoiceStore)
const settingsStore = useSettingsStore()
const { getMonth: month, getYear: year } = storeToRefs(settingsStore)

// composables
const { monthOptions } = useMonthOptions()
const { yearOptions } = useYearOptions()

// onbeforeMount(getDataForSelectedMonthAndYear)
// das aktuelle Jahr und Monat als standartwert einstellen

// functions
const deleteNumber = (id: number) => {
  invoiceStore.deleteNumber(id)
}
const saveNumber = (value: { id: number; number: number }) => {
  invoiceStore.saveNumber(value.id, value.number)
}
const saveComment = (value: { id: number; comment: string }) => {
  console.log('save comment', value.id, value.comment)
}
const addNumber = (number: number) => {
  invoiceStore.addNumber(number)
}
const updateMonth = (value: number) => {
  settingsStore.selectMonth(value)
  //select month und year wird zu einer methode
}
const updateYear = (value: number) => {
  settingsStore.selectYear(value)
}
const switchToggeled = (value: Boolean) => {
  console.log('switchToggeled', value)
}
</script>

<template>
  <div class="pt-5">
    <h1 class="pb-4 d-flex justify-content-center">{{ t('general.editMonth') }}</h1>
    <div class="d-flex flex-column flex-sm-row pb-5 gap-4 gap-sm-5">
      <!-- month -->
      <SingleSelect
        :select-options="monthOptions"
        :selected="month"
        @update:selected="updateMonth"
        :placeholder="t('general.selectMonth')"
      />
      <!-- year -->
      <SingleSelect
        :select-options="yearOptions"
        :selected="year"
        @update:selected="updateYear"
        :placeholder="t('general.selectYear')"
      />
    </div>
    <div class="d-flex flex-column flex-sm-row invoices pb-3 gap-5">
      <div class="pb-5">
        <!-- invoices count -->
        <NumberOfInvoices :invoices="invoices" />
        <!-- comment -->
        <EditableComment v-model:comment="comment" id="jannuary" @save-comment="saveComment" />
      </div>

      <div>
        <!-- invoices -->
        <InvoicesPerMonthList
          :invoices="invoices"
          @delete-number="deleteNumber"
          @save-number="saveNumber"
          @add-number="addNumber"
        />
        <!--sum  -->
        <MonthlySum :sum="2" :show-text="false" />
        <div class="pt-4">
          <!-- switch -->
          <ToggelSwitch
            :TextBefore="t('general.total')"
            :TextAfter="t('general.perPerson')"
            :checked="false"
            @clicked="switchToggeled"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.invoices {
  justify-content: space-around;
}
</style>
