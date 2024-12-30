<script>
import { useRouter, useRoute } from 'vue-router';


export default {
    data: () => ({
        open: [''],
        admins: [
            ['Quản lý user đăng nhập', 'mdi-account-multiple-outline', ''],
            ['Cấu hình 1', 'mdi-cog-outline', '/main/home'],
            ['Cấu hình 2', 'mdi-cog-outline', '/main/config2'],
            ['Cấu hình 3', 'mdi-cog-outline', '/main/config3'],
        ],
        CauHinhs: [
            ['Đăng xuất', 'mdi-plus-outline', '/Login'],
        ],
    }),
    methods: {
        navigateTo(route) {
            if (route) {
                this.$router.push(route); // Điều hướng đến route
            } else {
                //alert('Route không tồn tại'); // Hiển thị cảnh báo nếu không có route
            }
        },
    },
};
</script>

<template>
    <v-card class="mx-auto" width="">
        <v-list v-model:opened="open">
            <!-- <v-list-item prepend-icon="mdi-home" title="Home"></v-list-item> -->
            <v-list-group value="Admin">
                <template v-slot:activator="{ props }">
                    <v-list-item v-bind="props" title="Cấu hình" prepend-icon="mdi-cog"></v-list-item>
                </template>

                <v-list-item v-for="([title, icon, route], i) in admins" :key="i" :prepend-icon="icon" color="#009ACD"
                    :value="title" class="small-text">
                    <div v-on:click="navigateTo(route)">{{ title }}</div>
                </v-list-item>
            </v-list-group>

            <v-list-group value="CauHinh" no-action>
                <template v-slot:activator="{ props }">
                    <v-list-item v-bind="props" title="Cấu hình" prepend-icon="mdi-cog"></v-list-item>
                </template>

                <v-list-item v-for="([title, icon, route], i) in CauHinhs" :key="i" :prepend-icon="icon" color="#009ACD"
                    :value="title" class="small-text">
                    <div v-on:click="navigateTo(route)">{{ title }}</div>
                </v-list-item>
            </v-list-group>


        </v-list>
    </v-card>
</template>
<style scoped>
.small-text {
    font-size: 13px;
}
</style>
