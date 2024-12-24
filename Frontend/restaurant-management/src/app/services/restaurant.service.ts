import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Table } from '../models/table';
import { HttpClient } from '@angular/common/http';
import { TableResponse} from '../models/tableResponse';
import { AddReservation } from '../models/addReservation';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  constructor(private http: HttpClient) { }
  private dataItemSource = new BehaviorSubject<Table | null>(null);
  private getTableDataUrl = 'https://localhost:7092/Table/GetAll'; // Base API URL
  dataItem$ = this.dataItemSource.asObservable();

  setTableData(dataItem: Table): void {
    this.dataItemSource.next(dataItem);
  }

  getTables(requestBody: {
    date: string;
    fromTime?: string;
    toTime?: string;
    minSeatingCapacity: number;
    pageNumber: number;
    pageSize: number;
  }): Observable<TableResponse> {
    return this.http.post<TableResponse>(this.getTableDataUrl, requestBody);
  }
  
  submitReservation(reservation: AddReservation): Observable<any> {
    return this.http.post<any>('https://localhost:7092/Reservation/Submit', reservation);
  }
}
