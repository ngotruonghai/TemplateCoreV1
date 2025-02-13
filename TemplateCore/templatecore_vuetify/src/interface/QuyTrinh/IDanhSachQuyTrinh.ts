export interface IDanhSachQuyTrinh{
    id:number
    maQuyTrinh:string;
    created:Date;
    status:boolean;
    ngayBatDau:Date;
    userName:string
}

export interface DanhSachQuyTrinhResponse {
    succeeded: boolean | null;
    code: number | null;
    message: string | null;
    errors: Record<string, any> | null;
    data: IDanhSachQuyTrinh[];
  }