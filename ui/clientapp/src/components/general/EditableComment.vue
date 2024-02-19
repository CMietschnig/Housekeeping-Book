<script setup lang="ts">
import { type PropType, ref } from 'vue'
import { useVModel } from '@vueuse/core'
import { useI18n } from 'vue-i18n'

const props = defineProps({
  id: {
    type: [Number, String] as PropType<number | string>,
    required: true
  },
  comment: {
    type: String,
    required: false
  }
})

const emit = defineEmits(['update:comment', 'saveComment'])

const { t } = useI18n()
const isEditMode = ref(false)
const inputValue = useVModel(props, 'comment', emit)

const editComment = () => {
  isEditMode.value = !isEditMode.value
}
const saveComment = () => {
  emit('saveComment', props.comment)
  isEditMode.value = !isEditMode.value
}
</script>

<template>
  <span class="d-flex justify-content-center justify-content-sm-start">{{
    t('general.comment')
  }}</span>
  <!-- open comment -->
  <div v-if="!isEditMode" class="no-edit-mode">
    <div
      class="d-flex flex-column flex-sm-row justify-content-center justify-content-sm-start align-items-center align-items-sm-start gap-3"
    >
      <span v-if="!comment" class="no-comment p-4">{{ t('general.noCommentYet') }}</span>
      <span v-else class="comment p-4"> {{ comment }}</span>
      <div>
        <BButton variant="primary" class="edit-comment-btn" @click="editComment()"
          ><font-awesome-icon icon="fa-solid fa-pencil"
        /></BButton>
      </div>
    </div>
  </div>
  <!-- close comment -->
  <div
    v-else
    class="d-flex flex-column flex-sm-row justify-content-center justify-content-sm-start align-items-center align-items-sm-start gap-3 edit-mode"
  >
    <textarea
      name="comment"
      :id="'comment-' + id"
      cols="23"
      rows="10"
      v-model="inputValue"
      type="text"
      class="p-4 comment"
    ></textarea>
    <div>
      <BButton variant="primary" class="save-comment-btn" @click="saveComment()">
        <font-awesome-icon icon="fa-regular fa-floppy-disk" />
      </BButton>
    </div>
  </div>
</template>

<style scoped>
.comment,
.no-comment {
  max-width: 228px;
  border: 1px solid;
}
</style>
