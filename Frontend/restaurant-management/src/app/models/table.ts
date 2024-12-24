export interface Table {
    id: string;
    tableNumber: number;
    location: string;
    seatingCapacity: number;
    isActive: boolean;
    createdAt: Date;
    updatedAt: Date;
}
