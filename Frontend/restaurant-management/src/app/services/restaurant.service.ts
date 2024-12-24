import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { TableResponse} from '../models/tableResponse';
import { AddReservation } from '../models/addReservation';
import { environment } from '../environment-config';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  private baseUrl = environment.apiBaseUrl;
  private endpoints = environment.apiEndpoints;
  constructor(private http: HttpClient) { }
  
  getTables(requestBody: {
    date?: string;
    fromTime?: string;
    toTime?: string;
    minSeatingCapacity: number;
    pageNumber: number;
    pageSize: number;
  }): Observable<TableResponse> {
    return this.http.post<TableResponse>(`${this.baseUrl}${this.endpoints.getTableData}`, requestBody);
  }
  
  submitReservation(reservation: AddReservation): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}${this.endpoints.addReservation}`, reservation);
  }
}
