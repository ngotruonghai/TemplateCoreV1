<template>
    <div class="ChillColor">
        <v-container>
        <v-row class="mb-6">
            <v-col cols="12" md="6">
                <v-row>
                    <v-col cols="12">
                        <div class="input-container">
                            <label for="account" class="FontDefault">Tài khoản</label>
                            <input type="text" id="account" :value="responseData.data.firstName" class="FontDefault"
                                readonly />
                        </div>
                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12">
                        <div class="input-container">
                            <label for="account" class="FontDefault">Quyền</label>
                            <input type="text" id="account" :value="responseData.data.normalizedUserName"
                                class="FontDefault" readonly />
                        </div>
                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12">
                        <div class="input-container">
                            <label for="account" class="FontDefault">Email</label>
                            <input type="text" id="account" :value="responseData.data.email" class="FontDefault"
                                readonly />
                        </div>
                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12">
                        <div class="input-container">
                            <label for="account" class="FontDefault">Họ</label>
                            <input type="text" id="account" :value="responseData.data.firstName" class="FontDefault"
                                readonly />
                        </div>
                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12">
                        <div class="input-container">
                            <label for="account" class="FontDefault">Tên</label>
                            <input type="text" id="account" :value="responseData.data.lastName" class="FontDefault"
                                readonly />
                        </div>
                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12">
                        <div class="input-container">
                            <label for="account" class="FontDefault">Số điện thoại</label>
                            <input type="text" id="account" :value="responseData.data.phoneNumber" class="FontDefault"
                                readonly />
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
    const response = await callAuthenticationAPI('/api/account?userId=' + UserId, 'GET', {}, { timeout: 15000 });
    responseData.value = response as APIResponse; // Gán trực tiếp nếu dùng reactive
}

/*================================================*/
onMounted(async () => {
    await LoadUserInfo(props.id);
});
</script>