<template>
    <div class="ChillColor">
        <v-container>
            <v-row>
                <v-col cols="12" md="12">
                    <button class="btnAdd btn" v-on:click="btnCreateClick" style="float: right;">
                        <span class="icon-border">
                            <i class="fas fa-plus"></i>
                        </span> Thêm mới quy trình
                    </button>
                </v-col>
                <v-col cols="12" md="12">

                    <!-- <DanhSachUser :responseData="responseData"/> -->
                    <v-card flat>
                        <v-card-title class="d-flex align-center pe-2">
                            <v-spacer></v-spacer>
                            <v-spacer></v-spacer>
                            <v-spacer></v-spacer>
                            <v-spacer></v-spacer>
                            <div class="FontDefault" style="margin-right: 10px;">Tìm kiếm</div>
                            <v-text-field v-model="search" density="compact" label="Search"
                                prepend-inner-icon="mdi-magnify" variant="solo-filled" flat hide-details
                                single-line></v-text-field>
                        </v-card-title>

                        <v-divider></v-divider>
                        <v-data-table :items-per-page="10" v-model:search="search"
                            :filter-keys="['maQuyTrinh', 'tenQuyTrinh','userName','ngayBatDau','ngayBatDau']" :items="responseData.data" :headers="headers"
                            item-value="id" class="custom-table"
                            density="compact"
                             :loading="loading"
      loading-text="Đang tải dữ liệu..."
                            item-key="name">
                            <template v-slot:header.STT>
                                <div class="HeaderTable FontDefault">STT</div>
                            </template>
                            <template v-slot:item.STT="{ index }">
                                <div style="text-align: center; width: 20px;">{{ index + 1 }}</div>
                            </template>

                            <template v-slot:header.maQuyTrinh :sortable = "true">
                                <div class="HeaderTable FontDefault" style="width: 100px;">Mã quy trinh</div>
                            </template>
                            <template v-slot:item.maQuyTrinh="{ item } ">
                                <div>
                                    <a style="font-weight: bold;color: black;" :href="`/home/quytrinh/${item.id}`">{{ item.maQuyTrinh }}</a>
                                </div>
                            </template>

                            <template v-slot:header.tenQuyTrinh>
                                <div class="HeaderTable FontDefault" style="width: 250px;">Tên quy trình</div>
                            </template>

                            <template v-slot:header.created>
                                <div class="HeaderTable FontDefault" style="width: 100px;">Ngày tạo</div>
                            </template>
                            <template v-slot:item.created="{ item }: { item: { created: Date } }">
                                <div>
                                    {{ convertToDate(item.created as unknown as string) }}
                                </div>
                            </template>

                            <template v-slot:header.ngayBatDau>
                                <div class="HeaderTable FontDefault" style="width: 100px;">Ngày kích hoạt</div>
                            </template>
                            <template v-slot:item.ngayBatDau="{ item }: { item: { ngayBatDau: Date } }">
                                <div>
                                    {{ convertToDate(item.ngayBatDau as unknown as string) }}
                                </div>
                            </template>

                            <template v-slot:header.status>
                                <div class="HeaderTable FontDefault" style="width: 100px;">Trạng thái</div>
                            </template>
                            <template v-slot:item.status="{ item }: { item: { status: boolean } }">
                                <div >
                                    <span v-if="item.status" class="status-badge status-active">
                                        Đã kích hoạt
                                    </span>
                                    <span v-else class="status-badge status-locked">
                                        Chưa kích hoạt
                                    </span>
                                </div>
                            </template>
                            
                            <template v-slot:header.userName>
                                <div class="HeaderTable FontDefault" style="width: 150px;">Người tạo</div>
                            </template>

                            <template v-slot:header.noiDung>
                                <div class="HeaderTable FontDefault" style="width: 300px;">Nội dung</div>
                            </template>

                        </v-data-table>
                    </v-card>

                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script lang="ts" setup>
import type { DanhSachQuyTrinhResponse } from "@/interface/QuyTrinh/IDanhSachQuyTrinh";
import { callApi, callAuthenticationAPI, convertToDate } from '@/providers/data-provider';
import { useRouter } from 'vue-router';
const router = useRouter();

let search = ref('');
let loading =ref(false);

let responseData = ref<DanhSachQuyTrinhResponse>({
    succeeded: null,
    code: null,
    message: null,
    errors: null,
    data: []
});

let headers = [
    { text: 'STT', value: 'STT',title:"STT"},
    { text: 'Mã quy trình', value: 'maQuyTrinh' , sortable: true, title :"Mã quy trình"},
    { text: 'Tên quy trình', value: 'tenQuyTrinh' ,title:"Tên quy trình", sortable: true},
    { text: 'Người tạo', value: 'userName' ,title:"Người tạo", sortable: true},
    { text: 'Ngày tạo', value: 'created' ,title:"Ngày tạo", sortable: true},
    { text: 'Ngày bắt đầu', value: 'ngayBatDau',title:"Ngày bắt đầu", sortable: true },
    { text: 'Trạng thái', value: 'status',title:"Trang thái" , sortable: true},  
    { text: 'Nội dung', value: 'noiDung',title:"Nội dung" },      

];

async function LoadDanhSachQuyTrinh() {
    try {
        loading.value=true;
        const result = await callAuthenticationAPI('/api/quanlythongtin/QuyTrinhNode/GetDanhSachQuyTinh', 'GET', {}, { timeout: 15000 });
        responseData.value = result as DanhSachQuyTrinhResponse;
        loading.value=false;
    } catch (error) {
        loading.value=false;
    }
}

function btnCreateClick() {
    router.push("/home/taoquytrinh");
}

onMounted(() => {
    LoadDanhSachQuyTrinh();
});
</script>
<style>

</style>