<template>
    <div class="multi-select-nhansunode FontDefault">
        <!-- Hiển thị nút dropdown -->
        <div class="dropdown-header-nhansunode" @click="toggleDropdown">
            <span :class="{ 'placeholder': selectedId.length === 0 }">
                {{ selectedValues.length > 0
                    ? selectedValues.join(", ")
                    : "Chọn nhân sự..." }}
            </span>
            <span class="arrow">{{ dropdownOpenNhanSu ? "▲" : "▼" }}</span>
        </div>

        <!-- Dropdown danh sách -->
        <div v-if="dropdownOpenNhanSu" class="dropdown-nhansunode">
            <div class="dropdown-item-phongban">
                <input type="checkbox" id="checkAll" v-model="isAllSelected" @change="toggleSelectAll" />
                <label for="checkAll"><strong>Chọn tất cả</strong></label>
            </div>
            <!-- Trường tìm kiếm -->
            <input type="text" v-model="searchQuery" placeholder="Tìm kiếm..." style="width: 100%;margin-bottom: 10px;"
                @input="filterCities" />
            <div v-for="item in responseDataFiller.data" :key="item.id" class="dropdown-item-nhansunode">
                <input type="checkbox" :id="item.id" :value="item.id" v-model="selectedId"
                    @change="LoadValuesChecked()" />
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
const selectedValues = ref<string[]>([]);
const dropdownOpenNhanSu = ref<boolean>(false);

const props = defineProps<{
    ListPhongBanId: number[];
}>();

interface User {
    firstName: string;
    lastName: string;
    maNhanVien: string;
    id: string;
    userName: string;
    email: string;
    phoneNumber: string | null;
    phongBanId: number
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
    const dropdownElement = document.querySelector(".multi-select-nhansunode");
    if (dropdownElement && !dropdownElement.contains(event.target as Node)) {
        dropdownOpenNhanSu.value = false; // Đóng dropdown nếu click ra ngoài
    }
    emit("emit_nhansuId", selectedId.value);
};


function LoadValuesChecked() {
    selectedValues.value = [];
    if (isAllSelected.value == true) {
        selectedValues.value.push("Tất cả");
        return;
    }
    selectedId.value.forEach((itemId) => {
        const nhansu = responseData.data.find(x => x.id == itemId);
        if (nhansu) {
            selectedValues.value.push(nhansu.firstName + " " + nhansu.lastName);
        }
    });
}

// Tải dữ liệu API
async function LoadDataAPI() {
    try {
        const response = await callAuthenticationAPI('/api/account/GetAllUser', 'GET', {}, { timeout: 15000 });
        responseData = response as APIResponse;

        // coppy data
        responseDataFiller.value = { ...responseData, data: [...responseData.data] }
    } catch (error) {
        console.log("Lỗi comboxbo nhân sự " + error);
    }
}

// 🔥 1️⃣ Computed: Kiểm tra xem tất cả checkbox đã chọn hay chưa
const isAllSelected = computed(() => {
    return responseDataFiller.value.data.length > 0 &&
        selectedId.value.length === responseDataFiller.value.data.length;
});

// 🔥 2️⃣ Hàm "Chọn tất cả"
const toggleSelectAll = () => {
    if (isAllSelected.value) {
        selectedId.value = [];
        selectedValues.value = [];
    } else {
        selectedId.value = responseDataFiller.value.data.map(x => x.id);
        LoadValuesChecked();
    }
};

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
        selectedId.value = [];
        selectedValues.value = [];
        responseDataFiller.value.data = responseData.data;

        if (newVal.length > 0) {
            responseDataFiller.value.data = responseDataFiller.value.data.filter(x =>
                newVal.includes(x.phongBanId)
            );
        }
    },
    { deep: true, immediate: true }
);

</script>

<style>
.placeholder {
    color: #aaa;
}

/* CSS tùy chỉnh */
.multi-select-nhansunode {
    width: 100%;
    position: relative;
    font-family: Arial, sans-serif;
}

.dropdown-header-nhansunode {
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

.dropdown-nhansunode {
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

.dropdown-item-nhansunode {
    display: flex;
    align-items: center;
    margin-bottom: 5px;
}

.dropdown-item-nhansunode input[type="checkbox"] {
    margin: 0;
    margin-right: 8px;
    cursor: pointer;
}

.dropdown-item-nhansunode label {
    margin: 0;
    cursor: pointer;
    white-space: nowrap;
}
</style>