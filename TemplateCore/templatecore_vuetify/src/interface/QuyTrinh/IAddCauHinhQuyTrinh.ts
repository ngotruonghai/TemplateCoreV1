export interface IDataThongTin {
    index: number;
    tenThonTin: string | null;
    noiDung: string | null;
    thongBao: string | null;
    loaiThongTin:number;
    kichThuocKyTu:number;
    isBatBuocnhap:boolean;
    isFileDinhKem:boolean;
    danhSachCauHinhBuoc: string[];
}

export interface IDanhSachNode{
    keyId: string | null,
    type: number,
    index: number,
    tenNode: String | null
    x: number,
    y: number
}