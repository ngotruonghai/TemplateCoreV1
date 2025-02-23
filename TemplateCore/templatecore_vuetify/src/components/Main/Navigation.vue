<template>
    <div>
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



        <v-card class="mx-auto" width="" id="navigation">
            <v-list v-model:opened="open">
                <!-- <v-list-item prepend-icon="mdi-home" title="Home"></v-list-item> -->
                <v-list-group value="Admin">
                    <template v-slot:activator="{ props }">
                        <v-list-item v-bind="props" title="" prepend-icon="mdi-cog" style="font-size: 15px;"
                            class="FontDefaault">Thông tin</v-list-item>
                    </template>

                    <v-list-item v-on:click="navigateTo(route, title)"
                        v-for="([title, icon, route], i) in DanhMucs.admins" :key="i" :prepend-icon="icon"
                        :value="title" class="small-text">
                        <div>{{ title }}</div>
                    </v-list-item>
                </v-list-group>

                <v-list-group value="PhanQuyen" no-action>
                    <template v-slot:activator="{ props }">
                        <v-list-item v-bind="props" title="" prepend-icon="mdi-cog" style="font-size: 15px;"
                            class="FontDefaault">Phân quyền</v-list-item>
                    </template>

                    <v-list-item v-on:click="navigateTo(route, title)"
                        v-for="([title, icon, route], i) in DanhMucs.PhanQuyen" :key="i" :prepend-icon="icon"
                        :value="title" class="small-text">
                        <div>{{ title }}</div>
                    </v-list-item>
                </v-list-group>

                <v-list-group value="QuyTrinh" no-action>
                    <template v-slot:activator="{ props }">
                        <v-list-item v-bind="props" title="" prepend-icon="mdi-cog" style="font-size: 15px;"
                            class="FontDefault">Quy trình</v-list-item>
                    </template>

                    <v-list-item v-on:click="navigateTo(route, title)"
                        v-for="([title, icon, route], i) in DanhMucs.QuyTrinh" :key="i" :prepend-icon="icon"
                        :value="title" class="small-text">
                        <div>{{ title }}</div>
                    </v-list-item>
                </v-list-group>


                <v-list-group value="CauHinh" no-action>
                    <template v-slot:activator="{ props }">
                        <v-list-item v-bind="props" title="" prepend-icon="mdi-cog" style="font-size: 15px;"
                            class="FontDefault">Cấu hình</v-list-item>
                    </template>

                    <v-list-item v-on:click="navigateTo(route, title)"
                        v-for="([title, icon, route], i) in DanhMucs.CauHinhs" :key="i" :prepend-icon="icon"
                        :value="title" class="small-text">
                        <div>{{ title }}</div>
                    </v-list-item>
                </v-list-group>
                <v-list-item class="eventclick">
                    <v-list-item-content class="d-flex align-center">
                        <v-icon>
                            <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQr6WsCGy-o3brXcj2cmXGkHM_fE_p0gy4X8w&s"
                                width="24" height="24" />
                        </v-icon>

                        <v-list-item-title style="padding-left: 10px;">Item title</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
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
        DanhMucs: {
            admins: [
                ['Tài khoản', 'mdi-cog-outline', '/home/account'],
                ['Phòng ban', 'mdi-plus-outline', '/home/phongban']
            ],
            CauHinhs: [
                ['Đăng xuất', 'mdi-plus-outline', '/'],
            ],
            QuyTrinh: [
                ['Quy trình', 'mdi-plus-outline', '/home/danhsachquytrinh'],
            ],
            PhanQuyen: [
                ['Dashboard', 'mdi-cog-outline', '/home/dashboard'],
            ],
        },
        userName: ""
    }),
    methods: {
        navigateTo(route: string, Name: string) {
            if (route) {
                if (route == "/") {
                    localStorage.clear();
                    this.$router.push(route);
                }
                else {
                    localStorage.setItem("Url", route)
                    this.$emit('dataSent', Name);
                    this.$router.push(route); // Điều hướng đến route
                }

            } else {
                //alert('Route không tồn tại'); // Hiển thị cảnh báo nếu không có route
            }
        },

    },
    mounted() {
        this.userName = LocalStorageService.GetUserName() ?? "";
        let nametitle = this.DanhMucs.admins.findIndex(x => x.includes(this.$router.currentRoute.value.fullPath));

        if (nametitle != -1) {
            this.$emit('dataSent', this.DanhMucs.admins[nametitle][0]);

        }

    },
};
</script>
<style scoped>
.small-text {
    font-size: 13px;
}

.v-avatar img {
    object-fit: cover;
    /* Đảm bảo hình ảnh không bị méo */
}
.eventclick:hover{
    background-color: aqua;
}
</style>
