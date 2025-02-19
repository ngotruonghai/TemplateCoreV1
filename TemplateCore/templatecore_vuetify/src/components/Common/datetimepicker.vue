<template>
  <VueDatePicker 
    v-model="selectedDates" 
    locale="en" 
    :auto-apply="true" 
    placeholder="Chọn ngày"  
    format="dd/MM/yyyy"  
    @update:modelValue="LoadValues">
  </VueDatePicker>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch } from 'vue';
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';

// Nhận props datetime (có thể là Date hoặc null)
const props = defineProps<{
  datetime?: Date | null;
}>();

const selectedDates = ref(new Date()); // Ngày mặc định là hôm nay

const emit = defineEmits<{
  (event: 'emit_datetime', datetime: Date): void;
}>();

function LoadValues(value: Date) {
  emit("emit_datetime", value); // Emit giá trị Date trực tiếp
}

// Khi component được mount, kiểm tra nếu có props.datetime thì gán giá trị từ props
onMounted(() => {
  if (props.datetime) {
    selectedDates.value = props.datetime;
  } else {
    selectedDates.value = new Date();
  }
  LoadValues(selectedDates.value);
});

// Theo dõi nếu props.datetime thay đổi, cập nhật giá trị tương ứng
watch(() => props.datetime, (newDate) => {
  if (newDate) {
    selectedDates.value = newDate;
    LoadValues(newDate);
  }
});
</script>
