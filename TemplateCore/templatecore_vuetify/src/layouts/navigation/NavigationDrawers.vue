<script>
export default {
    data: () => ({
        open: [''],
        admins: [
            ['Quản lý user đăng nhập', 'mdi-account-multiple-outline'],
            ['Cấu hình 1', 'mdi-cog-outline'],
            ['Cấu hình 2', 'mdi-cog-outline'],
            ['Cấu hình 3', 'mdi-cog-outline'],
        ],
        cruds: [
            ['Create', 'mdi-plus-outline'],
            ['Read', 'mdi-file-outline'],
            ['Update', 'mdi-update'],
            ['Delete', 'mdi-delete'],
        ],
    }),
}
</script>
<template>
    <v-card class="mx-auto" width="">
        <v-list v-model:opened="open">
            <!-- <v-list-item prepend-icon="mdi-home" title="Home"></v-list-item> -->
            <v-list-group value="Admin">
                <template v-slot:activator="{ props }">
                    <v-list-item v-bind="props" title="Cấu hình" prepend-icon="mdi-cog"></v-list-item>
                </template>

                <v-list-item v-for="([title, icon], i) in admins" :key="i" :prepend-icon="icon"
                    color="#009ACD"
                    :value="title" class="small-text">{{ title }}</v-list-item>
            </v-list-group>
        </v-list>
    </v-card>
</template>
<style scoped>

.small-text {
    font-size: 13px;
}

</style>
