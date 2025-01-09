<template>
  <div class="combobox-container">
    <select id="email" class="combobox" v-model="selectedRole" @change="emitRoleId">
      <option value="" key="0"></option>
      <option v-for="(item, index) in responseData.data" :key="item.id" :value="item.id">
        {{ item.name }}
      </option>
    </select>
    <span class="combobox-icon">▼</span>
  </div>
</template>

<script lang="ts" setup>
import { defineProps, defineEmits, ref, onMounted, watch } from 'vue';
import { callAuthenticationAPI } from '@/providers/data-provider';

const emit = defineEmits<{
  (event: 'update:RoleId', data: string): void;
}>();

const props = defineProps<{
  RoleId: string; // Nhận giá trị RoleId từ component cha
}>();

const selectedRole = ref<string>(props.RoleId); // Gắn giá trị RoleId từ props vào selectedRole

interface DataItem {
  id: string;
  name: string;
  normalizedName: string;
  concurrencyStamp: string | null;
}

interface APIResponse {
  succeeded: boolean | null;
  code: number | null;
  message: string | null;
  errors: Record<string, any> | null;
  data: DataItem[];
}

const responseData = ref<APIResponse>({
  succeeded: null,
  code: null,
  message: null,
  errors: null,
  data: []
});

async function LoadDataAPI() {
  try {
    const response = await callAuthenticationAPI('/api/account/GetRole', 'GET', {}, { timeout: 15000 });
    responseData.value = response as APIResponse;
  } catch (error) {
    console.log(error);
  }
}

// Gửi giá trị selectedRole trở lại component cha qua sự kiện
function emitRoleId() {
  emit('update:RoleId', selectedRole.value);
}

// Theo dõi sự thay đổi của props.RoleId và cập nhật lại selectedRole
watch(
  () => props.RoleId,
  (newRoleId) => {
    selectedRole.value = newRoleId;
  }
);

onMounted(async () => {
  await LoadDataAPI();
});
</script>

<style scoped>
select {
  appearance: auto;
  -webkit-appearance: auto;
  -moz-appearance: auto;
}

.combobox-container {
  position: relative;
  width: 100%;
}

.combobox {
  width: 100%;
  padding: 8px;
  font-size: 14px;
  border: 1px solid #ccc;
  border-radius: 4px;
  appearance: none;
  -webkit-appearance: none;
  -moz-appearance: none;
  background-color: white;
}

.combobox:focus {
  border-color: #007bff;
  outline: none;
  box-shadow: 0 0 3px #007bff;
}

.combobox-icon {
  position: absolute;
  top: 50%;
  right: 12px;
  transform: translateY(-50%);
  pointer-events: none;
  font-size: 14px;
  color: #aaa;
}
</style>
