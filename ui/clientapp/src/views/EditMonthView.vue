<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { storeToRefs } from 'pinia'
import { useInvoiceStore } from '../stores/useInvoiceStore.ts'
import { useSettingsStore } from '@/stores/useSettingsStore'
import useMonthOptions from '../composables/useMonthOptions.ts'
import useYearOptions from '@/composables/useYearOptions'
import { ref, watch } from 'vue'
import { onBeforeMount } from 'vue'

//stores
const invoiceStore = useInvoiceStore()
const { getInvoices: invoices, getComment: savedComment } = storeToRefs(invoiceStore)
const settingsStore = useSettingsStore()
const { getMonth: month, getYear: year } = storeToRefs(settingsStore)

// composables
const { monthOptions } = useMonthOptions()
const { yearOptions } = useYearOptions()


onBeforeMount(() => {
  invoiceStore.getInvoicesPerMonthAndYear(month.value, year.value.toString());
  invoiceStore.getCommentPerMonthAndYear(month.value, year.value.toString());
});

// variables
const { t } = useI18n()
var comment = ref(savedComment.value);
// update comment because
// comment is loaded faster than getComment from the store
watch(savedComment, (newComment) => {
  comment.value = newComment;
})

// functions
const deleteInvoice = async (id: number) => {
  await invoiceStore.deleteInvoiceById(id)
  await invoiceStore.getInvoicesPerMonthAndYear(month.value, year.value.toString())
}
const updateInvoice = (value: { id: number; invoiceTotal: number }) => {
  invoiceStore.updateInvoiceById(value.id, value.invoiceTotal)
}
const updateComment = (comment: string) => {
  invoiceStore.updateCommentByMonthAndYear(month.value, year.value.toString(), comment)
}
const addInvoice = async (invoiceTotal: number) => {
  await invoiceStore.addInvoiceToMonthAndYear(month.value, year.value.toString(), invoiceTotal)
  await invoiceStore.getInvoicesPerMonthAndYear(month.value, year.value.toString())
}
const updateMonth = async (value: number) => {
  await settingsStore.selectMonth(value)
  await invoiceStore.getInvoicesPerMonthAndYear(value, year.value.toString());
  await invoiceStore.getCommentPerMonthAndYear(value, year.value.toString());
}
const updateYear = async(value: number) => {
  await settingsStore.selectYear(value)
  await invoiceStore.getInvoicesPerMonthAndYear(month.value, value.toString());
  await invoiceStore.getCommentPerMonthAndYear(month.value, value.toString());
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
