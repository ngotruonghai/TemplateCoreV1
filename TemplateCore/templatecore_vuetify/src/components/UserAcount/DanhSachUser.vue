<template>
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
    <v-data-table v-model:search="search" :filter-keys="['firstName', 'lastName', 'userName', 'email', 'lockoutEnabled']"
      :items="dataResponse ?? []" :headers="headers" item-value="id" >
      <template v-slot:header.STT>
        <div class="HeaderTable FontDefault">STT</div>
      </template>
      <template v-slot:item.STT="{ index }">
        <div style="text-align: center;">{{ index + 1 }}</div>
      </template>

      <template v-slot:header.userName>
        <div class="HeaderTable FontDefault">Tên người dùng</div>
      </template>

      <template v-slot:header.normalizedUserName>
        <div class="HeaderTable FontDefault">Quyền</div>
      </template>

      <template v-slot:header.email>
        <div class="HeaderTable FontDefault">Email</div>
      </template>

      <template v-slot:header.maNhanVien>
        <div class="HeaderTable FontDefault">Mã nhân viên</div>
      </template>

      <template v-slot:header.lockoutEnabled>
        <div class="HeaderTable FontDefault">Trạng thái</div>
      </template>
      <template v-slot:item.lockoutEnabled="{ item }: { item: { lockoutEnabled: boolean } }">
        <div>
          <span v-if="item.lockoutEnabled" class="status-badge status-active">
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
          <button class="action-button view" v-on:click="btnViewEvent(item.id)">
            <i class="mdi mdi-eye"></i>
          </button>

          <!-- Sửa (Edit) -->
          <!-- <button class="action-button edit">
            <i class="mdi mdi-pencil"></i>
          </button> -->

          <!-- Xóa (Delete) -->
          <button class="action-button delete">
            <i class="mdi mdi-trash-can"></i>
          </button>
        </div>
      </template>


    </v-data-table>
  </v-card>
</template>

<script lang="ts" setup>
import { defineProps, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
const router = useRouter();

const props = defineProps<{
  responseData: { [key: string]: any };
}>();

let search = ref('');
let dataResponse = ref([]); // Ban đầu để rỗng, sẽ được cập nhật qua `watch`
let headers = [
  { text: 'STT', value: 'STT', width: '30px', sortable: true},
  { text: 'Tên người dùng', value: 'userName' },
  { text: 'Mã nhân viên', value: 'maNhanVien' },
  { text: 'Quyền', value: 'normalizedUserName' },
  { text: 'Email', value: 'email' },
  { text: 'Trạng thái hoạt động', value: 'lockoutEnabled' },
  { text: 'Thao tác', value: 'id' },
];

/*========================================================*/
function btnViewEvent(Id: string) {
  router.push(`/home/userId/${Id}`);
}

/*========================================================*/
watch(
  () => props.responseData,
  async (newValue) => {
    if (newValue?.data) {
      dataResponse.value = await newValue.data;
    } else {
      dataResponse.value = [];
    }
  },
  { immediate: true }
);

</script>
<style>
.status-badge {
  display: inline-block;
  padding: 0.5px 5px;
  border-radius: 5px;
  text-align: center;
  font-size: 12px;
}

.status-active {
  background-color: #e6f9e6;
  /* Nền xanh nhạt */
  color: #54c26e;
  /* Màu xanh đậm */
  border: 0.5px solid #9cebae;
}

.status-locked {
  background-color: #f9e6e6;
  /* Nền đỏ nhạt */
  color: #e95a68;
  /* Màu đỏ đậm */
  border: 0.5px solid #f0a4ac;
}

.action-buttons {
  display: flex;
  gap: 10px;
  /* Khoảng cách giữa các nút */
}

.action-button {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 20px;
  height: 20px;
  border: 1px solid #ddd;
  border-radius: 5px;
  background-color: white;
  cursor: pointer;
  transition: all 0.3s ease;
  padding-top: 2px;
}

/* Biểu tượng */
.action-button i {
  font-size: 15px;
  color: #333;
  /* Màu mặc định */
}

/* Hover: Thay đổi màu */
.action-button:hover {
  background-color: #f5f5f5;
  border-color: #aaa;
}

.action-button.view:hover i {
  color: #007bff;
  /* Màu xanh khi hover */
}

.action-button.edit:hover i {
  color: #007bff;
  /* Màu xanh khi hover */
}

.action-button.delete:hover i {
  color: #007bff;
  /* Màu xanh khi hover */
}

.action-button.delete:hover {
  background-color: #fdecea;
}
</style>
