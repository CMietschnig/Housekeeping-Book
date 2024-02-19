<script setup lang="ts">
import type { IInvoice } from '@/interfaces/IInvoice'
import { ref, type PropType } from 'vue'
import { useI18n } from 'vue-i18n'

defineProps({
  invoices: {
    type: Array as PropType<Array<IInvoice>>,
    required: true
  }
})

defineEmits(['deleteNumber', 'updateNumber', 'addNumber'])

// variables
const { t } = useI18n()
const number = ref(null)
</script>

<template>
  <div class="d-flex flex-column">
    <!-- Add new invoices -->
    <EditableNumberInput
      v-model:number="number"
      id="addNewInvoice"
      :placeholder="t('editMonth.invoiceTotal')"
      :only-addable="true"
      @add-number="$emit('addNumber', $event)"
      ref="addNewInvoice"
    />
    <!-- no invoices created yet -->
    <div v-if="invoices.length === 0" class="no-invoices pb-3">
      {{ t('editMonth.noInvoicesCreatedYet') }}
    </div>

    <!-- list of invoices -->
    <div v-else v-for="(invoice, id) in invoices" :key="id" class="invoices-list">
      <EditableNumberInput
        v-model:number="invoice.Total"
        :id="invoice.InvoiceId"
        @delete-number="$emit('deleteNumber', $event)"
        @update-number="$emit('updateNumber', $event)"
        :placeholder="t('editMonth.invoiceTotal')"
        :only-addable="false"
        ref="invoice"
      />
    </div>
  </div>
</template>
