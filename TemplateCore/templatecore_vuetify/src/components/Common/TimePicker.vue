<template>
    <div>
      <label for="timePicker"></label>
      <input 
      locale="vi"
        type="time" 
        id="timePicker" 
        v-model="selectedTime" 
        min="00:00" 
        max="23:59"
      >
    </div>
  </template>
  
  <script>
  import { vi } from 'date-fns/locale';
  export default {
    data() {
      return {
        selectedTime: '' // Giá trị mặc định
      };
    }
  };
  </script>
  