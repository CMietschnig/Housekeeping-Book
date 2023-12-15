<script setup lang="ts">
import { useI18n } from 'vue-i18n'

const props = defineProps({
  TextBefore: String,
  TextAfter: String,
  checked: Boolean
})

const emit = defineEmits(['clicked'])
let checkedValue = props.checked

// variables
const { t } = useI18n()

// functions
const toggleCheckbox = () => {
  checkedValue = !checkedValue
  emit('clicked', checkedValue)
}
</script>

<template>
  <div class="d-flex align-items-center">
    <span class="pe-2">{{ TextBefore }}</span>
    <label class="switch">
      <input v-bind="$attrs" type="checkbox" :checked="checkedValue" @change="toggleCheckbox" />
      <span class="slider round"></span>
    </label>
    <span class="px-2">{{ TextAfter }}</span>
    <font-awesome-icon icon="fa-solid fa-circle-info" v-b-tooltip.hover.top="t('general.perPersonSetting')" />
  </div>
</template>

<style scoped>
.switch {
  position: relative;
  display: inline-block;
  width: 40px;
  height: 24px;
}

.switch input {
  opacity: 0;
  width: 0;
  height: 0;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: green;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

.slider:before {
  position: absolute;
  content: '';
  height: 20px;
  width: 20px;
  top: 2px;
  left: 2px;
  bottom: 2px;
  background-color: white;
  -webkit-transition: 0.4s;
  transition: 0.4s;
}

input:checked + .slider {
  background-color: #2196f3;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196f3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(16px);
  -ms-transform: translateX(16px);
  transform: translateX(16px);
}

/* Rounded sliders */
.slider.round {
  border-radius: 20px;
}

.slider.round:before {
  border-radius: 50%;
}
</style>
