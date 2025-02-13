<template>
    <div>
        hai181
    </div>
</template>

<script lang="ts" setup>
const props = defineProps<{
    id: string;
}>();

onMounted(() => {
  alert(props.id);
});
</script>

<style></style>