<template>
    <div class="combobox-container">
        <select id="email" class="combobox">
            <option value="superadmin@gmail.com">superadmin@gmail.com</option>
        </select>
        <!-- Icon -->
        <span class="combobox-icon">▼</span>
    </div>
</template>
<script lang="ts" setup>

const props = defineProps<{
  RoleId: string;
}>();

</script>

<style>
select {
    appearance: auto;
    /* Hiển thị mũi tên mặc định */
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
    /* Ẩn mũi tên mặc định */
    -webkit-appearance: none;
    -moz-appearance: none;
    background-color: white;
}

.combobox:focus {
    border-color: #007bff;
    outline: none;
    box-shadow: 0 0 3px #007bff;
}

/* Tùy chỉnh icon */
.combobox-icon {
    position: absolute;
    top: 50%;
    right: 12px;
    transform: translateY(-50%);
    pointer-events: none;
    /* Để icon không ảnh hưởng khi click */
    font-size: 14px;
    color: #aaa;
}
</style>