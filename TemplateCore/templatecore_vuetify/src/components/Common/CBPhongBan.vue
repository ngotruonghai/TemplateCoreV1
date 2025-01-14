<template>
  <div class="combobox-container">
    <select id="email" class="combobox" v-model="id" @change="emitId">
      <option value="" key="0"></option>
      <option v-for="(item, index) in responseData.data" :key="item.id" :value="item.id">
        {{ item.tenPhongBan }}
      </option>
    </select>
    <span class="combobox-icon">▼</span>
  </div>
</template>

<script lang="ts" setup>
import { defineProps, defineEmits, ref, onMounted, watch } from 'vue';
import { callAuthenticationAPI } from '@/providers/data-provider';

const emit = defineEmits<{
  (event: 'update:Id', data: string): void;
}>();

const props = defineProps<{
  Id: string; // Nhận giá trị RoleId từ component cha
}>();

const id = ref<string>(props.Id); // Gắn giá trị RoleId từ props vào selectedRole

interface data {
  maPhongBan: string,
  tenPhongBan: string,
  ghiChu: string,
  id: string,
  status: boolean,
  created: Date
}
interface APIResponse {
  succeeded: boolean | null;
  code: number | null;
  message: string | null;
  errors: Record<string, any> | null;
  data: data[];
}

let responseData = ref<APIResponse>({
  succeeded: null,
  code: null,
  message: null,
  errors: null,
  data: []
});


async function LoadDanhSachPhongBan() {
  try {
    const result = await callAuthenticationAPI('/api/quanlythongtin/PhongBan/DanhSachPhongBan', 'GET', {}, { timeout: 15000 });
    responseData.value = result as APIResponse; // Ép kiểu
    console.log(responseData.value);
  } catch (error) {

  }
}

// Gửi giá trị selectedRole trở lại component cha qua sự kiện
function emitId() {
  emit('update:Id', id.value);
}

// Theo dõi sự thay đổi của props.RoleId và cập nhật lại selectedRole
watch(
  () => props.Id,
  (newRoleId) => {
    id.value = newRoleId;
  }
);

onMounted(async () => {
  await LoadDanhSachPhongBan();
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