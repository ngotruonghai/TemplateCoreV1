import axios from 'axios';
import type { AxiosRequestConfig, Method } from 'axios';
import { useRouter } from 'vue-router';
import { LocalStorageService } from '@/providers/LocalStorageServices';


// Lấy giá trị từ .env
const API_BASE_URL = import.meta.env.VITE_API_URL || '';
const API_PATH = import.meta.env.VITE_API_PATH || '';

// Tạo URL đầy đủ
const FULL_API_URL = `${API_BASE_URL}${API_PATH}`;
const router = useRouter();

// Khởi tạo Axios instance
const apiClient = axios.create({
  baseURL: FULL_API_URL,
  timeout: 10000, // Timeout 10 giây
});

// Hàm gọi API chung
export const callApi = async <T>(
  endpoint: string, // Endpoint cụ thể, ví dụ: '/getDetails'
  method: Method = 'GET',
  data: Record<string, any> = {},
  config: AxiosRequestConfig = {}
): Promise<T> => {
  try {
    const response = await apiClient({
      url: endpoint, // Tự động nối với FULL_API_URL
      method,
      data: method !== 'GET' ? data : undefined,
      params: method === 'GET' ? data : undefined,
      ...config,
    });
    //console.log(router.currentRoute.value.fullPath);
    return response.data as T;
  } catch (error: any) {
    const errorStatus = error.response.status;
    
    return error.response.data;
  }
};
export const callAuthenticationAPI = async <T>(
  endpoint: string, // Endpoint cụ thể, ví dụ: '/getDetails'
  method: Method = 'GET',
  data: Record<string, any> = {},
  config: AxiosRequestConfig = {}
): Promise<T> =>{
  try {
    const token = LocalStorageService.GetToken();
    const headers = {
      Authorization: token ? `Bearer ${token}` : '',
      ...config.headers,
    };
    const response = await apiClient({
      url: endpoint, // Tự động nối với FULL_API_URL
      method,
      data: method !== 'GET' ? data : undefined,
      params: method === 'GET' ? data : undefined,
      ...config,
      headers
    });
    //console.log(router.currentRoute.value.fullPath);
    return response.data as T;
  } catch (error: any) {
    return error.response.data;
  }
};
