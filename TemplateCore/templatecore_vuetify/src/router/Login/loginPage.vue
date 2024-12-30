<template>
  <v-sheet class="mx-auto" width="300">
    <v-form @submit.prevent>
      <v-text-field v-model="username" :rules="rules" label="Tài khoản"></v-text-field>
      <v-text-field v-model="password" :rules="rules" :type="showPassword ? 'text' : 'password'"
        label="Mật khẩu"></v-text-field>
      <v-checkbox label="Hiển thị mật khẩu" v-model="value" value="value"
        v-on:click="togglePasswordVisibility()"></v-checkbox>
      <v-btn class="mt-2" type="submit" v-on:click="LoginEvent" block>Đăng nhập</v-btn>
    </v-form>

    <!-- Snackbar thông báo lỗi -->
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
  </v-sheet>
</template>

<script lang="ts">
import { callApi } from '@/providers/data-provider';
import { defineComponent } from 'vue';

export default defineComponent({
  data() {
    return {
      alert: {
        visible: false, // Trạng thái hiển thị snackbar
        color: '#CD3333', // Màu snackbar khi lỗi
        icon: 'mdi-cancel', // Icon thông báo lỗi
        title: 'Login Failed', // Tiêu đề thông báo (không dùng trong ví dụ này)
        text: 'Invalid username or password. Please try again.', // Nội dung thông báo
      },
      username: '',
      password: '',
      showPassword: false,
      value: '',
      responseData: {
        Succeeded: null,
        Code: null,
        Message: null,
        errors: null,
        data: {
          succeeded: null,
          code: null,
          message: null,
          errors: null,
          data: {
            id: null,
            userName: null,
            email: null,
            roles: [],
            isVerified: null,
            jwToken: null,
            permission: {
              role: null,
              permissions: [],
            },
          },
        },
      },
      rules: [
        (value: string) => {
          if (value) return true;
          return 'Vui lòng nhập đầy đủ thông tin';
        },
      ],
    };
  },
  beforeMount() {
    const url = localStorage.getItem("Url")??"";
    localStorage.clear();
    localStorage.setItem("Url",url);
  },
  methods: {
    togglePasswordVisibility() {
      this.showPassword = !this.showPassword;
    },

    // Phương thức LoginEvent sử dụng callApi
    async LoginEvent() {
      try {
        this.responseData = await callApi('/api/account/AuthenticateAsync', 'POST', {
          userName: this.username,
          password: this.password,
        }, {
          timeout: 15000
        });

        // Kiểm tra kết quả trả về từ API
        if (this.responseData.Succeeded === false) {
          this.alert.text = this.responseData.Message || 'Invalid username or password.'; // Cập nhật thông báo
          this.alert.visible = true; // Hiển thị snackbar lỗi
        } else {
          const token = this.responseData?.data?.data?.jwToken;
          const url  = localStorage.getItem("Url")??"";
          // Đăng nhập thành công, chuyển hướng đến trang dashboard hoặc lưu token
          localStorage.setItem('Token', token??"");
          localStorage.setItem("Url","/");

          // Ví dụ chuyển hướng sang trang dashboard
          this.$router.push(url);
        }
      } catch (error) {
        // Xử lý lỗi khi gọi API (sự cố mạng, lỗi máy chủ, v.v.)
        console.error('Login error:', error);
        this.alert.text = 'Lỗi kết nối server';
        this.alert.visible = true; // Hiển thị snackbar lỗi
      }
    },
  },
});
</script>

<style scoped>
/* CSS tùy chỉnh */
</style>
