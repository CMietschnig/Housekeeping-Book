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

defineEmits(['deleteNumber', 'saveNumber', 'addNumber'])

// variables
const { t } = useI18n()
const number = ref(null)
</script>

<template>
  <div class="d-flex flex-column">
    <!-- Add new invoices -->
    <NumberInput
      v-model:number="number"
      id="addNewInvoice"
      :placeholder="t('editMonth.invoiceTotal')"
      :only-addable="true"
      @add-number="$emit('addNumber', $event)"
    />
    <!-- no invoices created yet -->
    <div v-if="invoices.length === 0" class="pb-3">
      {{ t('editMonth.noInvoicesCreatedYet') }}
    </div>

    <!-- list of invoices -->
    <div v-else v-for="(invoice, id) in invoices" :key="id">
      <NumberInput
        v-model:number="invoice.sum"
        :id="invoice.id"
        @delete-number="$emit('deleteNumber', $event)"
        @save-number="$emit('saveNumber', $event)"
        :placeholder="t('editMonth.invoiceTotal')"
        :only-addable="false"
      />
    </div>
  </div>
</template>
