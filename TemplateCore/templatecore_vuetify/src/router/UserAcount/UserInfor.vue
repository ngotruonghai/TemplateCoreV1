<template>
    <div class="ChillColor">
        <v-container>
            <v-row>
                <v-col cols="12" md="6">

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="account" class="FontDefault">Mã nhân viên</label>
                                <input type="text" id="account" :value="responseData.data.maNhanVien" class="FontDefault"
                                     />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="account" class="FontDefault">Tài khoản (user đăng nhập)</label>
                                <input type="text" id="account" :value="responseData.data.firstName" class="FontDefault"
                                     />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="account" class="FontDefault">Quyền</label>
                                <CbPhanQuyen :RoleId="responseData.data.roleId" />

                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="account" class="FontDefault">Email</label>
                                <input type="text" id="account" :value="responseData.data.email" class="FontDefault"
                                     />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="account" class="FontDefault">Họ</label>
                                <input type="text" id="account" :value="responseData.data.firstName" class="FontDefault"
                                     />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="account" class="FontDefault">Tên</label>
                                <input type="text" id="account" :value="responseData.data.lastName" class="FontDefault"
                                     />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="account" class="FontDefault">Số điện thoại</label>
                                <input type="text" id="account" :value="responseData.data.phoneNumber"
                                    class="FontDefault"  />
                            </div>
                        </v-col>
                    </v-row>


                </v-col>
                <v-col cols="12" style="text-align: center; margin-top: 50px;" md="6">
                    <v-avatar size="150" class="me-4">
                        <img src="https://via.placeholder.com/150" alt="User Avatar" />
                    </v-avatar>
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
    </div>
</template>
<style></style>
<script lang="ts" setup>
import { reactive, onMounted } from 'vue';
import { callAuthenticationAPI } from '@/providers/data-provider';

const props = defineProps<{
    id: string;
}>();
let alert = ref({
    visible: false, // Trạng thái hiển thị snackbar
    color: '#CD3333', // Màu snackbar khi lỗi
    icon: 'mdi-cancel', // Icon thông báo lỗi
    title: 'Login Failed', // Tiêu đề thông báo (không dùng trong ví dụ này)
    text: 'Invalid username or password. Please try again.', // Nội dung thông báo
});
// Định nghĩa interface để match với cấu trúc dữ liệu trả về
interface UserData {
    firstName: string;
    lastName: string;
    maNhanVien: string | null;
    id: string;
    userName: string;
    normalizedUserName: string;
    email: string;
    normalizedEmail: string;
    emailConfirmed: boolean;
    securityStamp: string;
    concurrencyStamp: string;
    phoneNumber: string | null;
    phoneNumberConfirmed: boolean;
    twoFactorEnabled: boolean;
    lockoutEnd: string | null;
    lockoutEnabled: boolean;
    accessFailedCount: number;
    roleId: string;
}

interface APIResponse {
    succeeded: boolean | null;
    code: number | null;
    message: string | null;
    errors: Record<string, any> | null;
    data: UserData; // Dữ liệu trả về là một đối tượng UserData
}

// Khai báo responseData là một đối tượng phản ứng với kiểu dữ liệu APIResponse
const responseData = ref<APIResponse>({
    succeeded: null,
    code: null,
    message: null,
    errors: null,
    data: {} as UserData, // khởi tạo data là một đối tượng trống
});


/*================================================*/
async function LoadUserInfo(UserId: string) {
    try {
        const response = await callAuthenticationAPI('/api/account/GetUserById?userId=' + UserId, 'GET', {}, { timeout: 15000 });
        responseData.value = response as APIResponse; // Gán trực tiếp nếu dùng reactive
    } catch (error) {
        alert.value.text = error as string;
        alert.value.visible = true;
    }
}

/*================================================*/
onMounted(async () => {
    await LoadUserInfo(props.id);
});
</script>