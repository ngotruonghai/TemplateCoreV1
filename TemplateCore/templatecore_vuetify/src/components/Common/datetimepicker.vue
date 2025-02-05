<template>
  <VueDatePicker v-model="selectedDate" locale="en" :auto-apply="true" placeholder="Chọn ngày"  format="dd/MM/yyyy"  @update:modelValue="LoadValues"></VueDatePicker>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';

// Import locale tiếng Việt từ date-fns
import { vi } from 'date-fns/locale';

const selectedDate = ref("");

const emit = defineEmits<{
    (event: 'emit_datetime', datetime: Date): void;
}>();

function LoadValues(){
  const datetimes = new Date(selectedDate.value);
  emit("emit_datetime",datetimes);
}
</script>
