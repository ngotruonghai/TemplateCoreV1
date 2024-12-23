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
    </v-sheet>
</template>

<script lang="ts">
import { callApi } from '@/providers/data-provider';
import { defineComponent } from 'vue';

export default defineComponent({
  data() {
    return {
      username: '',
      password: '',
      showPassword: false,
      value: '',
      rules: [
        (value: string) => {
          if (value) return true;
          return 'Vui lòng nhập đầy đủ thông tin';
        },
      ],
    };
  },
  methods: {
    togglePasswordVisibility() {
      this.showPassword = !this.showPassword;
    },

    // Phương thức LoginEvent sử dụng callApi
    async LoginEvent() {
      try {
        const response = await callApi<{ token: string }>('/api/account/AuthenticateAsync', 'POST', {
          userName: this.username,
          password: this.password,
        });

        // Xử lý khi đăng nhập thành công
        console.log('Login successful:', response.token);
        // Lưu trữ token hoặc chuyển hướng, ví dụ:
        //localStorage.setItem('token', response.token);
        //this.$router.push('/dashboard'); // Ví dụ chuyển hướng sang trang dashboard

      } catch (error) {
        console.error('Login error:', error);
        // Xử lý lỗi, có thể hiển thị thông báo cho người dùng
      }
    },
  },
});
</script>

<style scoped>
/* CSS tùy chỉnh */
</style>
