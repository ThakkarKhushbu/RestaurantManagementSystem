import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { TableResponse} from '../models/tableResponse';
import { AddReservation } from '../models/addReservation';
import { environment } from '../environment-config';
import { Table } from '../models/table';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  private baseUrl = environment.apiBaseUrl;
  private endpoints = environment.apiEndpoints;
  constructor(private http: HttpClient) { }
  
  getTables(
    date?: string,
    fromTime?: string,
    toTime?: string,
) {
    let params = new HttpParams();
    if (date && fromTime && toTime) {
        params = params.set('date', date)
            .set('fromTime', fromTime)
            .set('toTime', toTime);
    }
    return this.http.get<Table[]>(`${this.baseUrl}${this.endpoints.getTableData}`, { params });
}
  
  submitReservation(reservation: AddReservation): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}${this.endpoints.addReservation}`, reservation);
  }
}
