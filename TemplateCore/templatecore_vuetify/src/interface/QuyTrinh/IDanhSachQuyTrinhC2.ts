export interface ApiResponse<QuyTrinhData> {
    succeeded: boolean;
    code: number;
    message: string | null;
    errors: any | null;
    data: QuyTrinhData;
}

export interface QuyTrinhData {
    maQuyTrinh: string;
    tenQuyTrinh: string;
    noiDung: string;
    thietLapMaPhieu: string;
    ghiChu: string;
    ngayBatDau: Date;
    nodeMapModels: NodeMapModel[];
    diagramNodeModels: DiagramNodeModel[];
    nhanSuIds: string[];
    phongBanIds: number[];
    nodeSttings: NodeSetting[];
    nextStepNodeModels : NextStepNodeModels[]
}

export interface NodeMapModel {
    keyId: string | null,
    type: number,
    index: number,
    tenNode: string | null
    x: number,
    y: number
}

export interface DiagramNodeModel {
    keyId: string;
    source: string;
    target: string;
    tenDiagram: string;
    danhSachQuyTrinhId: number;
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
    status: boolean;
    userParentId: string;
}

export interface NodeSetting {
    keyNode: string,
    ghiChu: string | null,
    isTaoTaskBaoCao: boolean,
    isGuiMailPhongBanTiepNhan: boolean,
    isGuiMailNhacNho: boolean,
    nhanSuNodeModels: string[],
    phongBanNodeModels: number[],
    cauHinhMailNhacNho: number
}
export interface NextStepNodeModels {
    nodeIdStart: string | null,
    nodeIdEnd: string | null,
    diagramId: string | null,
    actionName: string | null, // Tên Diagram
    action: number,
    typeNextStep: number
}