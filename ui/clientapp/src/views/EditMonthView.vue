<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { storeToRefs } from 'pinia'
import { useInvoiceStore } from '../stores/useInvoiceStore.ts'
import { useSettingsStore } from '@/stores/useSettingsStore'
import useMonthOptions from '../composables/useMonthOptions.ts'
import useYearOptions from '@/composables/useYearOptions'
import { ref } from 'vue'
import type { IInvoice } from '@/interfaces/IInvoice'
import { onBeforeMount, onMounted } from 'vue'

// variables
const { t } = useI18n()


//stores
const invoiceStore = useInvoiceStore()
const { getInvoices: invoices, getComment: commentFromStore } = storeToRefs(invoiceStore)
const settingsStore = useSettingsStore()
const { getMonth: month, getYear: year } = storeToRefs(settingsStore)


// composables
const { monthOptions } = useMonthOptions()
const { yearOptions } = useYearOptions()

onBeforeMount(() => {
  invoiceStore.getInvoicesPerMonthAndYear(month.value, year.value.toString());
  invoiceStore.getCommentPerMonthAndYear(month.value, year.value.toString());
});


// commentfromstore ist zu früh drann es ist leer wenn die seite neu geladen wird
var comment = ref(commentFromStore.value);
console.log(" comment form store", commentFromStore.value)
// das aktuelle Jahr und Monat als standartwert einstellen

// functions
const deleteInvoice = (id: number) => {
  invoiceStore.deleteInvoiceById(id)
}
const updateInvoice = (value: { id: number; invoiceTotal: number }) => {
  invoiceStore.updateInvoiceById(value.id, value.invoiceTotal)
}
const updateComment = (comment: string) => {
  invoiceStore.updateCommentByMonthAndYear(month.value, year.value, comment)
}
const addInvoice = (invoiceTotal: number) => {
  invoiceStore.addInvoiceToMonthAndYear(month.value, year.value, invoiceTotal)
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
        <EditableComment v-model:comment="comment" id="jannuary" @save-comment="updateComment"/>
      </div>

      <div>
        <!-- invoices -->
        <InvoicesPerMonthList
          :invoices="invoices"
          @delete-number="deleteInvoice"
          @update-number="updateInvoice"
          @add-number="addInvoice"
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
