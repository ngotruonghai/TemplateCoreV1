<template>
    <v-alert v-if="alert.visible" :color="alert.color" :icon="alert.icon" prominent>
        <strong>{{ alert.title }}</strong>
        <div>{{ alert.text }}</div>
    </v-alert>
</template>


<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    data() {
        return {
            alert: {
                visible: false,
                color: 'error',
                icon: '$error',
                title: 'Login Failed',
                text: 'Invalid username or password. Please try again.',
            },
        }
    },
    methods: {
        showAlert(color: string, icon: string, title: string, text: string) {
            this.alert = { visible: true, color, icon, title, text };

            // Tự động ẩn alert sau 3 giây
            setTimeout(() => {
                this.alert.visible = false;
            }, 3000);
        },
    }
});

</script>

<style scoped>
/* CSS tùy chỉnh */
</style>