<script setup lang="ts">
import { ref } from 'vue';
import NumberInput from "@/components/general/NumberInput.vue";
import { useI18n } from "vue-i18n";

const { t } = useI18n();
const sum = ref<number | null>(null);


</script>

<template>
    <h1>{{ t("general.editMonth") }}</h1>
    <NumberInput :placeholder="t('editMonth.invoiceTotal')" v-model:number="sum"/>
</template>