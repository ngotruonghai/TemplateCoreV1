<template>
  <div class="ChillColor">
    <v-container class="ChillColor">
      <v-row>
        <v-col cols="12" md="6">

        </v-col>
        <v-col cols="12" md="6">
          <button class="btnAdd btn">
            <span class="icon-border">
              <i class="fas fa-plus"></i>
            </span> Thêm mới
          </button>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12">
          <div>
            <DanhSachUser :responseData="responseData" :items="items" />
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
import { callApi, callAuthenticationAPI } from '@/providers/data-provider';

let alert = ref({
  visible: false, // Trạng thái hiển thị snackbar
  color: '#CD3333', // Màu snackbar khi lỗi
  icon: 'mdi-cancel', // Icon thông báo lỗi
  title: 'Login Failed', // Tiêu đề thông báo (không dùng trong ví dụ này)
  text: 'Invalid username or password. Please try again.', // Nội dung thông báo
});

let items = [
  {
    name: 'Nebula GTX 3080',
    image: '1.png',
    price: 699.99,
    rating: 5,
    stock: true,
  },
  {
    name: 'Galaxy RTX 3080',
    image: '2.png',
    price: 799.99,
    rating: 4,
    stock: false,
  },
  {
    name: 'Orion RX 6800 XT',
    image: '3.png',
    price: 649.99,
    rating: 3,
    stock: true,
  },
  {
    name: 'Vortex RTX 3090',
    image: '4.png',
    price: 1499.99,
    rating: 4,
    stock: true,
  },
  {
    name: 'Cosmos GTX 1660 Super',
    image: '5.png',
    price: 299.99,
    rating: 4,
    stock: false,
  },
];
interface User {
  firstName: string;
  lastName: string;
  maNhanVien: null | number;
  id: string;
  userName: string;
  normalizedUserName: boolean;
  email: string;
  normalizedEmail: string;
  emailConfirmed: boolean;
  //passwordHash: string;
  securityStamp: string;
  concurrencyStamp: string;
  phoneNumber: null | string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd: null | string;
  lockoutEnabled: boolean;
  accessFailedCount: number;
}

interface APIResponse {
  succeeded: boolean | null;
  code: number | null;
  message: string | null;
  errors: Record<string, any> | null;
  data: User[];
}

let responseData = ref<APIResponse>({
  succeeded: null,
  code: null,
  message: null,
  errors: null,
  data: []
});

async function API_UserInfo() {
  try {
    const result = await callAuthenticationAPI('/api/account/GetAllUser', 'GET', {}, { timeout: 15000 });
    responseData.value = result as APIResponse; // Ép kiểu
  } catch (error) {
    alert.value.text = error as string;
    alert.value.visible = true;
  }
}
onMounted(async () => {
  await API_UserInfo();
});

</script>
<style>
.btn {
  float: right;
}
</style>