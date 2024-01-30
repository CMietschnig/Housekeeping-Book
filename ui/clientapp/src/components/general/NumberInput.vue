<script setup lang="ts">
import { type PropType, ref } from 'vue'
import { useVModel } from '@vueuse/core'
import { BButton } from '../../../node_modules/bootstrap-vue-next'

const props = defineProps({
  onlyAddable: {
    type: Boolean,
    required: false,
    default: false
  },
  canDelete: {
    type: Boolean,
    required: false,
    default: true
  },
  displayRoundedNumber: {
    type: Boolean,
    required: false,
    default: true
  },
  placeholder: {
    type: String,
    required: true
  },
  number: {
    type: [Number, null] as PropType<number | null>,
    required: true
  },
  id: {
    type: [Number, String],
    required: true
  }
})

const emit = defineEmits(['update:number', 'deleteNumber', 'updateNumber', 'addNumber'])

// variables
const oldNumber = props.number
const inputValue = useVModel(props, 'number', emit)
const isEditMode = ref(false)

// functions
const editInput = () => {
  isEditMode.value = !isEditMode.value
}
const resetInput = () => {
  inputValue.value = oldNumber
  isEditMode.value = !isEditMode.value
}
const updateInput = () => {
  if (props.number) {
    emit('updateNumber', { id: props.id, number: props.number })
    isEditMode.value = !isEditMode.value
  }
}
const addInput = () => {
  if (props.number) {
    emit('addNumber', props.number)
    inputValue.value = null
  }
}
</script>

<template>
  <div class="pb-2">
    <!-- change existing number -->
    <div v-if="!onlyAddable">
      <!-- edit or delete number -->
      <div
        v-if="!isEditMode"
        class="d-flex flex-wrap justify-content-center justify-content-sm-start align-items-center gap-3 edit-number"
      >
        <span class="number">{{ displayRoundedNumber ? number?.toFixed(2) : number }}</span>
        <div class="d-flex gap-2">
          <BButton variant="primary" class="edit-btn hide" @click="editInput()">
            <font-awesome-icon icon="fa-solid fa-pencil" />
          </BButton>
          <BButton
            v-if="canDelete"
            variant="primary"
            class="delete-btn hide"
            @click="$emit('deleteNumber', id)"
          >
            <font-awesome-icon icon="fa-solid fa-trash-can" />
          </BButton>
        </div>
      </div>
      <!-- save number -->
      <div
        v-else
        class="d-flex flex-wrap justify-content-center justify-content-sm-start align-items-center gap-3"
      >
        <input
          :placeholder="placeholder"
          v-model="inputValue"
          type="number"
          :id="'id-' + id"
          class="update-input-field"
        />
        <div class="d-flex gap-2">
          <BButton variant="primary" class="update-btn" @click="updateInput()">
            <font-awesome-icon icon="fa-regular fa-floppy-disk" />
          </BButton>
          <BButton variant="primary" class="reset-btn" @click="resetInput()">
            <font-awesome-icon icon="fa-solid fa-xmark" />
          </BButton>
        </div>
      </div>
    </div>
    <!-- create new number input -->
    <div
      v-else
      class="d-flex flex-wrap justify-content-center justify-content-sm-start align-items-center gap-3 pb-4"
    >
      <input
        :placeholder="placeholder"
        v-model="inputValue"
        type="number"
        :id="'id-' + id"
        class="add-input-field"
      />
      <BButton variant="primary" class="add-btn" @click="addInput()">
        <font-awesome-icon icon="fa-solid fa-plus" />
      </BButton>
    </div>
  </div>
</template>

<style scoped>
.number {
  min-width: 11.25rem;
}
.hide {
  /*display: none; */
  opacity: 0.1;
}
.edit-number {
  min-height: 38px;
  background-color: grey;
}
.edit-number:hover .hide {
  /*display: block; */
  opacity: 1;
}
</style>
