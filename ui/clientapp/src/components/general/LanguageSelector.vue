<script setup lang="ts">
import { computed, ref } from 'vue'
import { useI18n } from 'vue-i18n'

const langs = ref([
  { code: 'en', text: 'English' },
  { code: 'de', text: 'Deutsch' }
])

const { locale } = useI18n()

const currentLang = computed(() => {
  return langs.value.find((lang) => lang.code === locale.value) || null
})

const changeLocale = (newLocale: string) => {
  locale.value = newLocale
}
</script>

<template>
  <BDropdown
    :text="currentLang?.text"
    variant="primary"
    size="md"
    class="color-mode-switcher"
    menu-class="w-100"
  >
    <BDropdownItem
      v-for="(lang, i) in langs"
      :key="`Lang${i}`"
      @click="changeLocale(lang.code)"
      variant="primary"
      >{{ lang.text }}</BDropdownItem
    >
  </BDropdown>
</template>
