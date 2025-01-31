<template>
  <div class="multi-select-nhansu FontDefault">
    <!-- Hiển thị nút dropdown -->
    <div class="dropdown-header-nhansu" @click="toggleDropdown">
      <span :class="{ 'placeholder': selectedId.length === 0 }">
        {{ selectedId.length > 0
          ? selectedId.join(", ")
          : "Chọn nhân sự..." }}
      </span>
      <span class="arrow">{{ dropdownOpenNhanSu ? "▲" : "▼" }}</span>
    </div>

    <!-- Dropdown danh sách -->
    <div v-if="dropdownOpenNhanSu" class="dropdown-nhansu">
      <!-- Trường tìm kiếm -->
      <input type="text" v-model="searchQuery" placeholder="Tìm kiếm..." style="width: 100%;margin-bottom: 10px;"
        @input="filterCities" />
      <div v-for="item in responseDataFiller.data" :key="item.id" class="dropdown-item-nhansu">
        <input type="checkbox" :id="item.id" :value="item.firstName + item.lastName" v-model="selectedId" />
        <label :for="item.id">{{ item.firstName + item.lastName }}</label>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue';
import { callAuthenticationAPI } from '@/providers/data-provider';

// Danh sách thành phố

// Biến reactive
const searchQuery = ref<string>("");
const selectedId = ref<string[]>([]);
const dropdownOpenNhanSu = ref<boolean>(false);

const props = defineProps<{
  ListPhongBanId: string[];
}>();

interface User {
  firstName: string;
  lastName: string;
  maNhanVien: string;
  id: string;
  userName: string;
  normalizedUserName: boolean;
  email: string;
  normalizedEmail: string;
  emailConfirmed: boolean;
  //passwordHash: string;
  securityStamp: string;
  concurrencyStamp: string;
  phoneNumber: null | string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd: null | string;
  lockoutEnabled: boolean;
  accessFailedCount: number;
}

interface APIResponse {
  succeeded: boolean | null;
  code: number | null;
  message: string | null;
  errors: Record<string, any> | null;
  data: User[];
}
const responseDataFiller = ref<APIResponse>({
  succeeded: null,
  code: null,
  message: null,
  errors: null,
  data: []
});

let responseData = <APIResponse>{
  succeeded: null,
  code: null,
  message: null,
  errors: null,
  data: []
}
const emit = defineEmits<{
  (event: 'emit_nhansuId', data: string[]): void;
}>();

// Lọc thành phố dựa trên tìm kiếm
const filterCities = () => {
  const query = searchQuery.value.toLowerCase();
  responseDataFiller.value.data = responseData.data;
  if (query.length >= 1) {
    responseDataFiller.value.data = responseDataFiller.value.data.filter(x => (x.firstName + x.lastName).toLocaleLowerCase().includes(query));
  }
  else {
    responseDataFiller.value.data = responseData.data;
  }
};

// Toggle trạng thái dropdown
const toggleDropdown = () => {
  dropdownOpenNhanSu.value = !dropdownOpenNhanSu.value;
};

// Xử lý click bên ngoài dropdown
const handleOutsideClick = (event: MouseEvent) => {
  const dropdownElement = document.querySelector(".multi-select-nhansu");
  if (dropdownElement && !dropdownElement.contains(event.target as Node)) {
    dropdownOpenNhanSu.value = false; // Đóng dropdown nếu click ra ngoài
  }
  emit("emit_nhansuId", selectedId.value);
};

// Tải dữ liệu API
async function LoadDataAPI() {
  try {
    const response = await callAuthenticationAPI('/api/account/GetAllUser', 'GET', {}, { timeout: 15000 });
    responseData = response as APIResponse;
    responseData.data.push({
      firstName: 'Tất cả',
      lastName: '',
      maNhanVien: 'All',
      id: '0',
      userName: '',
      normalizedUserName: false,
      email: '',
      normalizedEmail: '',
      emailConfirmed: false,
      securityStamp: '',
      concurrencyStamp: '',
      phoneNumber: null,
      phoneNumberConfirmed: false,
      twoFactorEnabled: false,
      lockoutEnd: null,
      lockoutEnabled: false,
      accessFailedCount: 0
    });

    responseData.data.sort((a, b) => parseInt(a.id) - parseInt(b.id));

    // coppy data
    responseDataFiller.value = { ...responseData, data: [...responseData.data] }
  } catch (error) {
    console.log("Lỗi comboxbo nhân sự " + error);
  }
}

// Gọi khi component được mount
onMounted(() => {
  document.addEventListener("click", handleOutsideClick); // Lắng nghe sự kiện click bên ngoài
  LoadDataAPI();
});

// Gỡ bỏ sự kiện khi component bị hủy
onUnmounted(() => {
  document.removeEventListener("click", handleOutsideClick);
});


watch(
  () => props.ListPhongBanId, // Dùng getter để theo dõi prop
  (newVal, oldVal) => {
    console.log("ListPhongBanId thay đổi:", newVal, oldVal);
  },
  { deep: true, immediate: true }
);

</script>

<style>
.placeholder {
  color: #aaa;
}

/* CSS tùy chỉnh */
.multi-select-nhansu {
  width: 100%;
  position: relative;
  font-family: Arial, sans-serif;
}

.dropdown-header-nhansu {
  border: 1px solid #ccc;
  padding: 8px;
  border-radius: 5px;
  cursor: pointer;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #fff;
}

.arrow {
  font-size: 12px;
}

.dropdown-nhansu {
  position: absolute;
  top: 100%;
  left: 0;
  right: 0;
  border: 1px solid #ccc;
  border-radius: 5px;
  background-color: #fff;
  max-height: 200px;
  overflow-y: auto;
  z-index: 1000;
  padding: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.dropdown input {
  margin-bottom: 10px;
  padding: 5px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.dropdown-item-nhansu {
  display: flex;
  align-items: center;
  margin-bottom: 5px;
}

.dropdown-item-nhansu input[type="checkbox"] {
  margin: 0;
  margin-right: 8px;
  cursor: pointer;
}

.dropdown-item-nhansu label {
  margin: 0;
  cursor: pointer;
  white-space: nowrap;
}
</style>
