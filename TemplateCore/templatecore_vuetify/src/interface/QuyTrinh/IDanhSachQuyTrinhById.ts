export interface ApiResponse<WorkflowData> {
    succeeded: boolean;
    code: number;
    message: string | null;
    errors: any | null;
    data: WorkflowData;
  }
  
  export interface WorkflowData {
    maQuyTrinh: string;
    thietLapMaPhieu: string;
    tenQuyTrinh: string;
    userId: string;
    userName: string;
    ghiChu: string;
    ngayBatDau: string;
    noiDung: string;
    trangThaiQuyTrinh: number;
    diagramNodes: DiagramNode[];
    nodes: Node[];
    nhanSuTheoDoiQuyTrinhs: Personnel[];
    phongBanTheoDoiQuyTrinhs: Department[];
    nextSteps: NextStep[];
  }
  
  export interface DiagramNode {
    keyId: string;
    source: string;
    target: string;
    tenDiagram: string | null;
    danhSachQuyTrinhId: number;
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
    status: boolean;
    userParentId: string;
  }
  
  export interface Node {
    keyId: string;
    type: number;
    index: number;
    tenNode: string;
    x: number;
    y: number;
    danhSachQuyTrinhId: number;
    nodeSettings: NodeSetting[];
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
    status: boolean;
    userParentId: string;
  }
  
  export interface NodeSetting {
    ghiChu: string;
    cauHinhMailNhacNho: number;
    isTaoTaskBaoCao: boolean;
    isGuiMailPhongBanTiepNhan: boolean;
    isGuiMailNhacNho: boolean;
    nhanSuTiepNhanNodes: string[];
    phongBanTiepNhanNodes: number[];
    nodeId: number;
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
    status: boolean;
    userParentId: string;
  }
  
  export interface Personnel {
    userId: string;
    hoTen: string;
    loaiNhanSu: number;
    nodeSettingId?: number;
    danhSachQuyTrinhId?: number;
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
    status: boolean;
    userParentId: string;
  }
  
  export interface Department {
    phongbanId: number;
    tenPhongBan: string;
    loaiPhongBan: number;
    nodeSettingId?: number;
    danhSachQuyTrinhId?: number;
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
    status: boolean;
    userParentId: string;
  }
  
  export interface NextStep {
    nodeIdStart: string;
    nodeIdEnd: string;
    diagramId: string;
    actionName: string;
    action: number;
    typeNextStep: number;
    danhSachQuyTrinhId: number;
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
    status: boolean;
    userParentId: string;
  }
  
  // Example Usage
  type WorkflowResponse = ApiResponse<WorkflowData>;
  