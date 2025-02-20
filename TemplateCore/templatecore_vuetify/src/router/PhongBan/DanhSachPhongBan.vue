<template>
  <div class="ChillColor">
    <v-container>
      <v-row>
        <v-col cols="12" md="6">

        </v-col>
        <v-col cols="12" md="12">
          <button class="btnAdd btn" v-on:click="LinkAdduser()" style="float: right;">
            <span class="icon-border">
              <i class="fas fa-plus"></i>
            </span> Thêm mới
          </button>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12">
          <div>
            <!-- <DanhSachUser :responseData="responseData"/> -->
            <v-card flat>
              <v-card-title class="d-flex align-center pe-2">
                <v-spacer></v-spacer>
                <v-spacer></v-spacer>
                <v-spacer></v-spacer>
                <v-spacer></v-spacer>
                <div class="FontDefault" style="margin-right: 10px;">Tìm kiếm</div>
                <v-text-field v-model="search" density="compact" label="Search" prepend-inner-icon="mdi-magnify"
                  variant="solo-filled" flat hide-details single-line></v-text-field>
              </v-card-title>

              <v-divider></v-divider>
              <v-data-table v-model:search="search" :filter-keys="['maPhongBan', 'tenPhongBan']"
                :items="responseData.data" :headers="headers" item-value="id"  class="custom-table" density="compact"
                item-key="name">
                <template v-slot:header.STT>
                  <div class="HeaderTable FontDefault">STT</div>
                </template>
                <template v-slot:item.STT="{ index }">
                  <div style="text-align: center;">{{ index + 1 }}</div>
                </template>

                <template v-slot:header.maPhongBan>
                  <div class="HeaderTable FontDefault">Mã phòng ban</div>
                </template>

                <template v-slot:header.tenPhongBan>
                  <div class="HeaderTable FontDefault">Tên phòng ban</div>
                </template>

                <template v-slot:header.created>
                  <div class="HeaderTable FontDefault">Ngày tạo</div>
                </template>
                <template v-slot:item.created="{ item }: { item: { created: Date } }">
                  <div>
                   {{ convertToDate(item.created as unknown as string) }}
                  </div>
                </template>

                <template v-slot:header.status>
                  <div class="HeaderTable FontDefault">Trạng thái</div>
                </template>
                <template v-slot:item.status="{ item }: { item: { status: boolean } }">
                  <div>
                    <span v-if="item.status" class="status-badge status-active">
                      Đã kích hoạt
                    </span>
                    <span v-else class="status-badge status-locked">
                      Chưa kích hoạt
                    </span>
                  </div>
                </template>

                <template v-slot:header.id>
                  <div class="HeaderTable FontDefault">Thao tác</div>
                </template>
                <template v-slot:item.id="{ item }: { item: { id: string } }">
                  <div class="action-buttons">
                    <!-- Xem (View) -->
                    <button class="action-button view" v-on:click="">
                      <i class="mdi mdi-eye"></i>
                    </button>
                    <button class="action-button delete">
                      <i class="mdi mdi-trash-can"></i>
                    </button>
                  </div>
                </template>
              </v-data-table>
            </v-card>
          </div>
        </v-col>
      </v-row>
      <v-snackbar v-model="alert.visible" :timeout="2000" :color="alert.color">
        <v-container>
          <v-row>
            <v-col cols="1">
              <v-icon color="white">{{ alert.icon }}</v-icon>
            </v-col>
            <v-col cols="11" style="padding-top: 14px;">{{ alert.text }}</v-col>
          </v-row>
        </v-container>
      </v-snackbar>
    </v-container>
    <!-- Snackbar thông báo lỗi -->
  </div>


</template>

<script lang="ts" setup>
import { callApi, callAuthenticationAPI, convertToDate } from '@/providers/data-provider';
import { useRouter } from 'vue-router';
const router = useRouter();

let search = ref('');
let dataResponse = ref([]); // Ban đầu để rỗng, sẽ được cập nhật qua `watch`
let headers = [
  { text: 'STT', value: 'STT', width: '30px', sortable: true },
  { text: 'Mã phòng ban', value: 'maPhongBan' },
  { text: 'Tên phòng ban', value: 'tenPhongBan' },
  { text: 'Ngày tạo', value: 'created' },
  { text: 'Trạng thái hoạt động', value: 'status' },
  { text: 'Thao tác', value: 'id' },
];

let alert = ref({
  visible: false, // Trạng thái hiển thị snackbar
  color: '#CD3333', // Màu snackbar khi lỗi
  icon: 'mdi-cancel', // Icon thông báo lỗi
  title: 'Login Failed', // Tiêu đề thông báo (không dùng trong ví dụ này)
  text: 'Invalid username or password. Please try again.', // Nội dung thông báo
});

interface data {
  maPhongBan: string,
  tenPhongBan: string,
  ghiChu: string,
  id: string,
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

let responseData = ref<APIResponse>({
  succeeded: null,
  code: null,
  message: null,
  errors: null,
  data: []
});


async function LoadDanhSachPhongBan() {
  try {
    const result = await callAuthenticationAPI('/api/quanlythongtin/PhongBan/DanhSachPhongBan', 'GET', {}, { timeout: 15000 });
    responseData.value = result as APIResponse; // Ép kiểu
  } catch (error) {
    alert.value.text = error as string;
    alert.value.visible = true;
  }
}

function LinkAdduser(){
  router.push("/home/taophongban");
}

onMounted(() => {
  LoadDanhSachPhongBan();
});


</script>

<style>
</style>