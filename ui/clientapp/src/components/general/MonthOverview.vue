<script setup lang="ts">
const props = defineProps({
  month: String,
  sum: Number,
});
</script>

<template>
  <div class="border p-3 m-2">
    <h2>{{ month }}</h2>
    <span> {{ sum }} €</span>
  </div>
</template>
