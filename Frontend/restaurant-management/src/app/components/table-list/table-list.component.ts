import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogRef, DialogService } from '@progress/kendo-angular-dialog';
import { GridDataResult, GridModule } from '@progress/kendo-angular-grid';
import { ReservationFormComponent } from '../reservation-form/reservation-form.component';
import { RestaurantService } from '../../services/restaurant.service';
import { Table } from '../../models/table';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-table-list',
  imports: [GridModule,CommonModule,HttpClientModule ],
  providers:[RestaurantService],
  templateUrl: './table-list.component.html',
  styleUrl: './table-list.component.scss',
  standalone:true
})
export class TableListComponent implements OnInit {
  constructor(private router: Router,private dialogService: DialogService,private restaurantService: RestaurantService) {}
  
  title = 'restaurant-management';
  public tableId: string | null = null;
  public pageSize = 10;
  public skip = 0;
  public gridData: GridDataResult = {
    data: [],
    total: 0
  };
  dialogRef: DialogRef | null = null;

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(): void {
    const requestBody = {
      date: '2024-12-24',
      minSeatingCapacity: 0,
      pageNumber: 1,
      pageSize: 10,
    };

    this.restaurantService.getTables(requestBody).subscribe(response => {
      // Mapping the response data to match Kendo Grid's format
      this.gridData = {
        data: response.items, // Using the items from the response
        total: response.totalItems // Total number of items
      };
    });
  }

  reserveTable(dataItem: Table): void {
    this.restaurantService.setTableData(dataItem);
    this.openDialog();
  }

  openDialog(): void {
    this.dialogRef =  this.dialogService.open({
      content: ReservationFormComponent,
      title: "Make a Reservation",
      width: 600,
      height: 400,
      actions: [{ text: 'Close' }]
    });
  }
}
