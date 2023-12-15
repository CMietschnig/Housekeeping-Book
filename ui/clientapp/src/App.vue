<script setup lang="ts">
import { RouterView } from 'vue-router'
import { useI18n } from 'vue-i18n'

// variables
const { t } = useI18n()
</script>

<template>
  <header>
    <div class="wrapper">
      <h1>{{ t('general.housekeepingBook') }}</h1>
      <NavBar />
    </div>
  </header>

  <RouterView />
</template>
