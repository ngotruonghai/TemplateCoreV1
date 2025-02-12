<template>
    <div class="multi-select-danhsachnode FontDefault">
        <!-- Hiển thị nút dropdown -->
        <div class="dropdown-header-danhsachnode" @click="toggleDropdown">
            <span :class="{ 'placeholder': selectedId.length === 0 }">
                {{ selectedValues.length > 0
                    ? selectedValues.join(", ")
                    : "Chọn bước..." }}
            </span>
            <span class="arrow">{{ dropdownOpenNhanSu ? "▲" : "▼" }}</span>
        </div>

        <!-- Dropdown danh sách -->
        <div v-if="dropdownOpenNhanSu" class="dropdown-danhsachnode">
            <div class="dropdown-item-phongban">
                <input type="checkbox" id="checkAll" v-model="isAllSelected" @change="toggleSelectAll" />
                <label for="checkAll"><strong>Chọn tất cả</strong></label>
            </div>
            <div v-for="item in responseDataFiller" :key="item.index" class="dropdown-item-danhsachnode">
                <input type="checkbox" :value="item.keyId" v-model="selectedId" @change="LoadValuesChecked()" />

                <label>{{ item.tenNode }}</label>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue';
import type { IDanhSachNode } from "@/interface/QuyTrinh/IAddCauHinhQuyTrinh";

const selectedId = ref<string[]>([]);
const selectedValues = ref<string[]>([]);
const dropdownOpenNhanSu = ref<boolean>(false);

const responseDataFiller = ref<IDanhSachNode[]>([]);

const emit = defineEmits<{
    (event: 'emit_dansachNode', data: string[]): void;
}>();


const props = defineProps<{
    DanhSachNode: IDanhSachNode[],
    LoadNode: string[]
}>();


// Toggle trạng thái dropdown
const toggleDropdown = () => {
    dropdownOpenNhanSu.value = !dropdownOpenNhanSu.value;
};

// Xử lý click bên ngoài dropdown
const handleOutsideClick = (event: MouseEvent) => {
    const dropdownElement = document.querySelector(".multi-select-danhsachnode");
    if (dropdownElement && !dropdownElement.contains(event.target as Node)) {
        dropdownOpenNhanSu.value = false; // Đóng dropdown nếu click ra ngoài
    }
    emit("emit_dansachNode", selectedId.value);
};


function LoadValuesChecked() {
    selectedValues.value = [];
    if (isAllSelected.value == true) {
        selectedValues.value.push("Tất cả");
        return;
    }
    selectedId.value.forEach((itemId) => {
        const data = responseDataFiller.value.find(x => x.keyId??"" == itemId);
        if (data) {
            selectedValues.value.push(data.tenNode?.toString()??"");
        }
    });
}


const isAllSelected = computed(() => {
    return responseDataFiller.value.length > 0 &&
        selectedId.value.length === responseDataFiller.value.length;
});

const toggleSelectAll = () => {
    if (isAllSelected.value) {
        selectedId.value = [];
        selectedValues.value = [];
    } else {
        selectedId.value = responseDataFiller.value.map(x => x.keyId??"");
        LoadValuesChecked();
    }
};

// Gọi khi component được mount
onMounted(() => {
    document.addEventListener("click", handleOutsideClick);
});

// Gỡ bỏ sự kiện khi component bị hủy
onUnmounted(() => {
    document.removeEventListener("click", handleOutsideClick);
});

watch(() => props.DanhSachNode, (newVal, oldVal) => {
   responseDataFiller.value = newVal;

}, { deep: true });


watch(() => props.LoadNode, (newVal, oldVal) => {
    selectedId.value = newVal
    LoadValuesChecked();
}, { deep: true });

</script>

<style>
.placeholder {
    color: #aaa;
}

/* CSS tùy chỉnh */
.multi-select-danhsachnode {
    width: 100%;
    position: relative;
    font-family: Arial, sans-serif;
}

.dropdown-header-danhsachnode {
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

.dropdown-danhsachnode {
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

.dropdown-item-danhsachnode {
    display: flex;
    align-items: center;
    margin-bottom: 5px;
}

.dropdown-item-danhsachnode input[type="checkbox"] {
    margin: 0;
    margin-right: 8px;
    cursor: pointer;
}

.dropdown-item-danhsachnode label {
    margin: 0;
    cursor: pointer;
    white-space: nowrap;
}
</style>