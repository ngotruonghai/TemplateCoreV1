<template>
  <div class="multi-select FontDefault">
    <!-- Hiển thị nút dropdown -->
    <div class="dropdown-header" @click="toggleDropdown">
      <span :class="{ 'placeholder': selectedCities.length === 0 }">
        {{ selectedCities.length > 0
          ? selectedCities.join(", ")
          : "Chọn..." }}
      </span>
      <span class="arrow">{{ dropdownOpen ? "▲" : "▼" }}</span>
    </div>

    <!-- Dropdown danh sách -->
    <div v-if="dropdownOpen" class="dropdown">
      <!-- Trường tìm kiếm -->
      <input type="text" v-model="searchQuery" placeholder="Tìm kiếm..." style="width: 100%;" @input="filterCities" />
      <div v-for="city in filteredCities" :key="city" class="dropdown-item">
        <input type="checkbox" :id="city" :value="city" v-model="selectedCities" />
        <label :for="city">{{ city }}</label>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue';
import { callAuthenticationAPI } from '@/providers/data-provider';

// Danh sách thành phố
const cities = ref<string[]>([]);
const filteredCities = ref<string[]>([]);

// Biến reactive
const searchQuery = ref<string>("");
const selectedCities = ref<string[]>([]);
const dropdownOpen = ref<boolean>(false);

interface DataItem {
  id: string;
  name: string | null;
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

// Lọc thành phố dựa trên tìm kiếm
const filterCities = () => {
  const query = searchQuery.value.toLowerCase();
  filteredCities.value = cities.value.filter((city) =>
    city.toLowerCase().includes(query)
  );
};

// Toggle trạng thái dropdown
const toggleDropdown = () => {
  dropdownOpen.value = !dropdownOpen.value;
};

// Xử lý click bên ngoài dropdown
const handleOutsideClick = (event: MouseEvent) => {
  const dropdownElement = document.querySelector(".multi-select");
  if (dropdownElement && !dropdownElement.contains(event.target as Node)) {
    dropdownOpen.value = false; // Đóng dropdown nếu click ra ngoài
  }
};

// Tải dữ liệu API
async function LoadDataAPI() {
  try {
    const response = await callAuthenticationAPI('/api/account/GetRole', 'GET', {}, { timeout: 15000 });
    responseData.value = response as APIResponse;
    responseData.value.data.forEach((item) => {
      return cities.value.push(item.name as string);
    });
    filteredCities.value = cities.value; // Thiết lập filteredCities ban đầu sau khi đã có dữ liệu
  } catch (error) {
    console.log(error);
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
</script>

<style>
.placeholder {
  color: #aaa;
}

/* CSS tùy chỉnh */
.multi-select {
  width: 100%;
  position: relative;
  font-family: Arial, sans-serif;
}

.dropdown-header {
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

.dropdown {
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

.dropdown-item {
  display: flex;
  align-items: center;
  margin-bottom: 5px;
}

.dropdown-item input[type="checkbox"] {
  margin: 0;
  margin-right: 8px;
  cursor: pointer;
}

.dropdown-item label {
  margin: 0;
  cursor: pointer;
  white-space: nowrap;
}
</style>
