<script lang="ts">
export default {
  name: "locale-changer",
  data() {
    return {
      langs: [
        { code: "en", text: "English" },
        { code: "de", text: "Deutsch" },
      ],
    };
  },
};
</script>

<template>
  <div class="locale-changer">
    <select v-model="$i18n.locale">
      <option v-for="(lang, i) in langs" :key="`Lang${i}`" :value="lang.code">
        {{ lang.text }}
      </option>
    </select>
  </div>
</template>
