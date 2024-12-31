<template>
   <div >
    <div>
        <v-container class="d-flex align-center">
    <!-- Avatar -->
    <v-avatar size="50" class="me-4">
      <img src="https://via.placeholder.com/150" alt="User Avatar" />
    </v-avatar>
    <!-- User Name -->
    <div>
      <strong class="text-muted" style="color: white;">{{ userName }}</strong>
    </div>
  </v-container>
    </div>
    <hr style="border: none; height: 1px; background-color: white;">



    <v-card class="mx-auto" width="">
        <v-list v-model:opened="open">
            <!-- <v-list-item prepend-icon="mdi-home" title="Home"></v-list-item> -->
            <v-list-group value="Admin">
                <template v-slot:activator="{ props }">
                    <v-list-item v-bind="props" title="Thông tin" prepend-icon="mdi-cog" style="font-size: 15px;"></v-list-item>
                </template>

                <v-list-item v-on:click="navigateTo(route)" v-for="([title, icon, route], i) in admins" :key="i" :prepend-icon="icon" color="#E0E0E0"
                    :value="title" class="small-text">
                    <div>{{ title }}</div>
                </v-list-item>
            </v-list-group>

            <v-list-group value="CauHinh" no-action>
                <template v-slot:activator="{ props }">
                    <v-list-item v-bind="props" title="Cấu hình" prepend-icon="mdi-cog" style="font-size: 15px;"></v-list-item>
                </template>

                <v-list-item v-on:click="navigateTo(route)" v-for="([title, icon, route], i) in CauHinhs" :key="i" :prepend-icon="icon" color="#009ACD"
                    :value="title" class="small-text">
                    <div>{{ title }}</div>
                </v-list-item>
            </v-list-group>


        </v-list>
    </v-card>
   </div>
</template>
<script lang="ts">
import { useRouter, useRoute } from 'vue-router';
import { LocalStorageService } from '@/providers/LocalStorageServices';
export default {
    data: () => ({
        open: [''],
        admins: [
            ['Quản lý user đăng nhập', 'mdi-cog-outline', '/home/acount'],
            ['Dashboard', 'mdi-cog-outline', '/home/dashboard'],
        ],
        CauHinhs: [
            ['Đăng xuất', 'mdi-plus-outline', '/Login'],
        ],
        userName:""
    }),
    methods: {
        navigateTo(route:string) {
            if (route) {
                this.$router.push(route); // Điều hướng đến route
            } else {
                //alert('Route không tồn tại'); // Hiển thị cảnh báo nếu không có route
            }
        },
    },
    mounted() {
        this.userName = LocalStorageService.GetUserName()??"";
        debugger;
    },
};
</script>
<style scoped>
.small-text {
    font-size: 13px;
}
.mx-auto{
    background-color: #2e628d; 
    color: white;
}
.v-avatar img {
  object-fit: cover; /* Đảm bảo hình ảnh không bị méo */
}
</style>
