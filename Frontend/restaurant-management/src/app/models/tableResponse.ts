import { Table } from "./table";

export interface TableResponse {
    items: Table[];
    pageSize: number;
    totalItems: number;
}
