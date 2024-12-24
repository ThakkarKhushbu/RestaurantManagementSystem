import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogRef, DialogService } from '@progress/kendo-angular-dialog';
import { GridDataResult, GridModule } from '@progress/kendo-angular-grid';
import { ReservationFormComponent } from '../reservation-form/reservation-form.component';
import { RestaurantService } from '../../services/restaurant.service';
import { Table } from '../../models/table';
import {  HttpClientModule } from '@angular/common/http';
import { NumericTextBoxModule } from '@progress/kendo-angular-inputs';

@Component({
  selector: 'app-table-list',
  imports: [GridModule,CommonModule,HttpClientModule,NumericTextBoxModule],
  providers:[RestaurantService],
  templateUrl: './table-list.component.html',
  styleUrl: './table-list.component.scss',
  standalone:true
})
export class TableListComponent implements OnInit {
  constructor(private dialogService: DialogService,private restaurantService: RestaurantService) {}
  
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

  loadItems(capacity?: number): void {
    this.restaurantService.getTables().subscribe({
      next: (response) => {
        // Apply optional filtering by seating capacity
        const filteredData = capacity
          ? response.filter((table) => table.seatingCapacity >= capacity)
          : response;
  
        // Format the data for Kendo Grid
        this.gridData = {
          data: filteredData,
          total: filteredData.length // Optional, if you need total records
        };
      },
      error: (error) => {
        console.error('Error loading tables:', error);
        // Optionally, handle error UI or logging here
      }
    });
  }
  
  
  openDialog(dataItem:Table): void {
    this.dialogRef =  this.dialogService.open({
      content: ReservationFormComponent,
      title: "Make a Reservation",
      width: 800,
      height: 750,
      actions: [{ text: 'Close' }]
    });
    const reservationFormInstance = this.dialogRef.content.instance as ReservationFormComponent;
    reservationFormInstance.setTableData(dataItem);
  }

  onFilterChange(value: any, column: any): void {
    if (!column || !column.field) {
      return;
    }
    this.loadItems(value);
    }
  
}
