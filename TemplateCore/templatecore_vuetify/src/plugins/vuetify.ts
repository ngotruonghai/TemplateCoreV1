/**
 * plugins/vuetify.ts
 *
 * Framework documentation: https://vuetifyjs.com`
 */

// Styles
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
import { createVuetify, type ThemeDefinition } from 'vuetify'
import { VCalendar } from 'vuetify/labs/VCalendar'

const myCustomLightTheme: ThemeDefinition = {
  dark: false,
  colors: {
    background: '#FFFFFF',
    surface: '#FFFFFF',
    primary: '#6200EE',
    'primary-darken-1': '#3700B3',
    secondary: '#03DAC6',
    'secondary-darken-1': '#018786',
    error: '#B00020',
    info: '#2196F3',
    success: '#4CAF50',
    warning: '#FB8C00',
    tableHeader: '#FF5722', // Thêm biến màu header
  },
};


export default createVuetify({
  locale: {
    locale: 'vi', // Đặt ngôn ngữ mặc định là tiếng Việt
    messages: {
      vi: {
        dataFooter: {
          itemsPerPageText: 'Số mục mỗi trang', // Đổi "Items per page"
          itemsPerPageAll: 'Tất cả', // Đổi "All" hoặc "Off" thành "Tất cả" hoặc "Tắt"
          pageText: '{0}-{1} tổng {2} trang', // Thay đổi văn bản phân trang nếu cần
          noResultsText: 'Không có kết quả nào phù hợp', // Văn bản khi không có dữ liệu
          nextPage: 'Trang tiếp',
          prevPage: 'Trang trước',
        },
        dataIterator: {
          noResultsText: 'Không tìm thấy dữ liệu', // Văn bản khi không tìm thấy kết quả
          loadingText: 'Đang tải dữ liệu...', // Văn bản khi tải dữ liệu
        },
      },
    },
  },
  components: {
    VCalendar,
  },
  theme: {
    defaultTheme: 'myCustomLightTheme',
    themes: {
      myCustomLightTheme,
      
    },
    
  },
  
})
