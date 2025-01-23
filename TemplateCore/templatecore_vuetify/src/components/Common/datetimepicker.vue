<template>
  <VueDatePicker v-model="selectedDate" locale="vi" :auto-apply="true" placeholder="Chọn ngày"  format="dd/MM/yyyy"></VueDatePicker>
</template>

<script setup>
import { ref } from 'vue';
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';

// Import locale tiếng Việt từ date-fns
import { vi } from 'date-fns/locale';

const selectedDate = ref();

// Đặt locale tiếng Việt cho VueDatePicker
const viLocale = vi;

const resetDate = () => {
  selectedDate.value = null;
};
</script>
