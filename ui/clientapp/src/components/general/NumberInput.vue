<script setup lang="ts">
import { type PropType, ref } from 'vue'
import { useVModel } from '@vueuse/core'
import { BButton} from '../../../node_modules/bootstrap-vue-next'

const props = defineProps({
  placeholder: {
    type: String,
    default: 'test'
  },
  number: {
    type: [Number, null] as PropType<number | null>,
    required: true
  }
})

const emit = defineEmits(['update:number'])

const inputValue = useVModel(props, 'number', emit)
const isEditMode = ref(false)
const editInput = () => {
  isEditMode.value = !isEditMode.value
}
</script>

<template>
  <div v-if="!isEditMode" class="d-flex align-items-center">
    <span>{{ number }}</span>
    <BButton variant="primary" class="ms-4" @click="editInput()">
        <font-awesome-icon icon="fa-solid fa-pencil"/>
    </BButton>
  </div>
  <div v-else class="d-flex align-items-center">
    <input :placeholder="placeholder" v-model="inputValue" type="number" />
    <BButton variant="primary" class="ms-4" @click="editInput()">
        <font-awesome-icon icon="fa-regular fa-floppy-disk" />
    </BButton>
    <BButton variant="primary" class="ms-2" @click="editInput()">
        <font-awesome-icon icon="fa-solid fa-xmark" />
    </BButton>
  </div>
</template>
