<script setup lang="ts">
import { useI18n } from 'vue-i18n'

// variables
const { t } = useI18n()

const changeMode = (mode: string) => {
  document.documentElement.setAttribute('data-bs-theme', mode.toLowerCase())
  // add settingsstore method to save the preferred color mode in database
}
</script>

<template>
  <BDropdown
    :text="t('general.theme')"
    variant="primary"
    size="md"
    class="color-mode-switcher"
    menu-class="w-100"
  >
    <BDropdownItem @click="changeMode('light')" variant="primary">{{ t('general.light') }}</BDropdownItem>
    <BDropdownItem @click="changeMode('dark')" variant="primary">{{ t('general.dark') }}</BDropdownItem>
  </BDropdown>
</template>
