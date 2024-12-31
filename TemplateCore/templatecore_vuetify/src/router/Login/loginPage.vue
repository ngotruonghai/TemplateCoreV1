<template>
  <div id="login">
    <v-container>
      <v-row no-gutters>
        <v-col cols="12" md="4"></v-col>
        <v-col cols="12" md="4">
          <div class="container">
            <div class="heading">Quản lý thông tin</div>
            <div class="heading" style="font-size: 15px;">Quản lý thông tin người dùng và quy trình</div>

            <form class="form" action="" onsubmit="return false;">
              <input placeholder="Tài khoản" v-model="username" id="username" name="username" type="text" class="input"
                required oninvalid="this.setCustomValidity('Vui lòng nhập tên tài khoản')"
                oninput="this.setCustomValidity('')" />
              <input placeholder="Mật khẩu" v-model="password" id="password" name="password"
                :type="showPassword ? 'text' : 'password'" class="input" required
                oninvalid="this.setCustomValidity('Vui lòng nhập tên mật khẩu')" oninput="this.setCustomValidity('')" />

              <v-checkbox label="Hiển thị mật khẩu" v-model="value" value="value"
                v-on:click="togglePasswordVisibility()" style="height: 50px; margin-top: 10px;"></v-checkbox>

              <span class="forgot-password"><a href="#">Quên tài khoản mật khẩu ?</a></span>
              <input value="Đăng nhập" type="submit" class="login-button" v-on:click="LoginEvent" />
            </form>

            <span class="agreement"><a href="#">Phiên bản 1.0.0</a></span>
          </div>
        </v-col>
        <v-col cols="12" md="4"></v-col>
      </v-row>
    </v-container>
  </div>
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
</template>


<script lang="ts">
import { callApi, callAuthenticationAPI } from '@/providers/data-provider';
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
    const url = localStorage.getItem("Url") ?? "";
    localStorage.clear();
    localStorage.setItem("Url", url);
  },
  methods: {
    togglePasswordVisibility() {
      this.showPassword = !this.showPassword;
    },

    // Phương thức LoginEvent sử dụng callApi
    async LoginEvent() {
      if (this.username == '' || this.password == '') {
        return;
      }
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
          const url = localStorage.getItem("Url") ?? "";
          // Đăng nhập thành công, chuyển hướng đến trang dashboard hoặc lưu token
          localStorage.setItem('Token', token ?? "");
          localStorage.setItem("Url", "/");

          if (url == "/") {
            this.$router.push('/home/dashboard');
          }
          else {
            this.$router.push(url);
          }

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
@keyframes slide-down {
  from {
    opacity: 0;
    transform: translateY(150px);
  }

  to {
    opacity: 1;
    transform: translateY(0);
  }
}

#login {
  background-color: #2e628d;
  height: 100%;
}

.container {
  max-width: 400px;
  background: #f8f9fd;
  background: linear-gradient(0deg, rgb(255, 255, 255) 0%, rgb(244, 247, 251) 100%);
  border-radius: 40px;
  border: 5px solid rgb(255, 255, 255);
  box-shadow: rgba(133, 189, 215, 0.8784313725) 0px 30px 30px -20px;
  margin-top: 50px;
  animation: slide-down 2s ease-out;
}

.heading {
  text-align: center;
  font-weight: 900;
  font-size: 30px;
  color: rgb(16, 137, 211);
}

.form {
  margin-top: 20px;
}

.form .input {
  width: 100%;
  background: white;
  border: none;
  padding: 15px 20px;
  border-radius: 20px;
  margin-top: 15px;
  box-shadow: #cff0ff 0px 10px 10px -5px;
  border-inline: 2px solid transparent;
}

.form .input::placeholder {
  color: rgb(170, 170, 170);
}

.form .input:focus {
  outline: none;
  border-inline: 2px solid #12b1d1;
}

.form .forgot-password {
  display: block;
  margin-top: 10px;
  margin-left: 10px;
}

.form .forgot-password a {
  font-size: 11px;
  color: #0099ff;
  text-decoration: none;
}

.form .login-button {
  display: block;
  width: 100%;
  font-weight: bold;
  background: linear-gradient(45deg, rgb(16, 137, 211) 0%, rgb(18, 177, 209) 100%);
  color: white;
  padding-block: 15px;
  margin: 20px auto;
  border-radius: 20px;
  box-shadow: rgba(133, 189, 215, 0.8784313725) 0px 20px 10px -15px;
  border: none;
  transition: all 0.2s ease-in-out;
}

.form .login-button:hover {
  transform: scale(1.03);
  box-shadow: rgba(133, 189, 215, 0.8784313725) 0px 23px 10px -20px;
}

.form .login-button:active {
  transform: scale(0.95);
  box-shadow: rgba(133, 189, 215, 0.8784313725) 0px 15px 10px -10px;
}

.agreement {
  display: block;
  text-align: center;
  margin-top: 15px;
}

.agreement a {
  text-decoration: none;
  color: #0099ff;
  font-size: 9px;
}
</style>
