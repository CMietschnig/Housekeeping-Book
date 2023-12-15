<script lang="ts">
export default {
  name: 'locale-changer',
  data() {
    return {
      langs: [
        { code: 'en', text: 'English' },
        { code: 'de', text: 'Deutsch' }
      ]
    }
  }
}
</script>

<template>
  <div class="locale-changer d-flex align-items-center py-2">
    <select v-model="$i18n.locale" class="select-lang">
      <option v-for="(lang, i) in langs" :key="`Lang${i}`" :value="lang.code">
        {{ lang.text }}
      </option>
    </select>
  </div>
</template>

<style scoped>
.select-lang {
  background-color: transparent;
  border: none;
}
</style>