<template>
    <div class="multi-select-phongbannode FontDefault">
        <!-- Hiển thị nút dropdown -->
        <div class="dropdown-header-phongbannode" @click="toggleDropdown">
            <span :class="{ 'placeholder': selectedId.length === 0 }">
                {{ selectedValues.length > 0
                    ? selectedValues.join(", ")
                    : "Chọn phòng ban..." }}
            </span>
            <span class="arrow">{{ dropdownOpenNhanSu ? "▲" : "▼" }}</span>
        </div>

        <!-- Dropdown danh sách -->
        <div v-if="dropdownOpenNhanSu" class="dropdown-phongbannode">
            <div class="dropdown-item-phongbannode">
                <input type="checkbox" id="checkAll" v-model="isAllSelected" @change="toggleSelectAll" />
                <label for="checkAll"><strong>Chọn tất cả</strong></label>
            </div>
            <!-- Trường tìm kiếm -->
            <input type="text" v-model="searchQuery" placeholder="Tìm kiếm..." style="width: 100%;margin-bottom: 10px;"
                @input="filterPhongBan" />
            <div v-for="phongbannode in responseDataFillter.data" :key="phongbannode.id"
                class="dropdown-item-phongbannode">
                <input type="checkbox" :value="phongbannode.id" v-model="selectedId" @change="LoadValuesChecked()" />
                <label>{{ phongbannode.tenPhongBan }}</label>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue';
import { callAuthenticationAPI } from '@/providers/data-provider';

const searchQuery = ref<string>("");
const selectedId = ref<number[]>([]);
const selectedValues = ref<string[]>([]);
const dropdownOpenNhanSu = ref<boolean>(false);

const props = defineProps<{
    phognbanId: number[],
}>();


interface data {
    maPhongBan: string,
    tenPhongBan: string,
    ghiChu: string,
    id: number,
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
let responseData = <APIResponse>({
    succeeded: null,
    code: null,
    message: null,
    errors: null,
    data: []
});
const responseDataFillter = ref<APIResponse>({
    succeeded: null,
    code: null,
    message: null,
    errors: null,
    data: []
});

const emit = defineEmits<{
    (event: 'emit_phonganId', data: number[]): void;
}>();

// Lọc thành phố dựa trên tìm kiếm
const filterPhongBan = () => {
    const query = searchQuery.value.toLowerCase();
    responseDataFillter.value.data = responseData.data;
    if (query.length >= 1) {
        responseDataFillter.value.data = responseData.data.filter(x =>
            x.tenPhongBan.toLowerCase().includes(query)
        );
    } else {
        responseDataFillter.value.data = responseData.data;
    }
};
// 🔥 1️⃣ Computed: Kiểm tra xem tất cả checkbox đã chọn hay chưa
const isAllSelected = computed(() => {
    return responseData.data.length > 0 &&
        selectedId.value.length === responseData.data.length;
});

// 🔥 2️⃣ Hàm "Chọn tất cả"
const toggleSelectAll = () => {
    if (isAllSelected.value) {
        selectedId.value = [];
        selectedValues.value = [];
    } else {
        selectedId.value = responseData.data.map(x => x.id);
        LoadValuesChecked();
    }
};
const toggleDropdown = () => {
    dropdownOpenNhanSu.value = !dropdownOpenNhanSu.value;
};

const handleOutsideClick = (event: MouseEvent) => {
    const dropdownElement = document.querySelector(".multi-select-phongbannode");
    if (dropdownElement && !dropdownElement.contains(event.target as Node)) {
        dropdownOpenNhanSu.value = false; // Đóng dropdown nếu click ra ngoài
    }
    emit("emit_phonganId", selectedId.value)
};

// Tải dữ liệu API
async function LoadDataAPI() {
    try {
        const response = await callAuthenticationAPI('/api/quanlythongtin/phongban/DanhSachPhongBan', 'GET', {}, { timeout: 15000 });
        responseData = response as APIResponse;
        responseData.data.sort((a, b) => a.id - b.id);
        responseDataFillter.value = { ...responseData, data: [...responseData.data] };
    } catch (error) {
        console.log("Lỗi combobox phòng ban: " + error);
    }
}

function LoadValuesChecked() {
    selectedValues.value = [];
    if (isAllSelected.value == true) {
        selectedValues.value.push("Tất cả");
        return;
    }
    selectedId.value.forEach((itemId) => {
        const phongbannode = responseData.data.find(x => x.id == itemId);

        if (phongbannode) {
            selectedValues.value.push(phongbannode.tenPhongBan);
        }
    });
}

watch(
    () => props.phognbanId, // Dùng getter để theo dõi prop
    (newVal, oldVal) => {
        if (newVal.length > 0) {
            selectedId.value = newVal;
            LoadValuesChecked();
        }
        else{
            selectedId.value = [];
            selectedValues.value = [];
            responseDataFillter.value.data = responseData.data;
        }
    },
    { deep: true, immediate: true }
);

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
.multi-select-phongbannode {
    width: 100%;
    position: relative;
    font-family: Arial, sans-serif;
}

.dropdown-header-phongbannode {
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

.dropdown-phongbannode {
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

.dropdown-item-phongbannode {
    display: flex;
    align-items: center;
    margin-bottom: 5px;
}

.dropdown-item-phongbannode input[type="checkbox"] {
    margin: 0;
    margin-right: 8px;
    cursor: pointer;
}

.dropdown-item-phongbannode label {
    margin: 0;
    cursor: pointer;
    white-space: nowrap;
}
</style>