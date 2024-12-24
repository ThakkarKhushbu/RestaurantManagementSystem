import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DialogRef, DialogsModule } from "@progress/kendo-angular-dialog";
import { RestaurantService } from '../../services/restaurant.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AddReservation } from '../../models/addReservation';
import { Table } from '../../models/table';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-reservation-form',
  imports: [CommonModule, DialogsModule, FormsModule, HttpClientModule, MatSnackBarModule],
  providers: [RestaurantService],
  templateUrl: './reservation-form.component.html',
  styleUrls: ['./reservation-form.component.scss'],
  standalone: true
})
export class ReservationFormComponent implements OnInit {
  tableNumber: number = 0;
  tableID: string = '';
  customerName: string = '';
  contactNumber: string = '';
  guestCount: number = 0;
  reservationDate: string = '';
  fromTime: string = '';
  toTime: string = '';
  maxGuestCount: number = 0;
  minDate: string = ''; // Minimum date for reservation

  constructor(
    private restaurantService: RestaurantService, 
    private snackBar: MatSnackBar,
    private dialogRef: DialogRef
  ) {}

  ngOnInit(): void {
    const today = new Date();
    this.minDate = today.toISOString().split('T')[0]; // Format: YYYY-MM-DD
  }

  setTableData(dataItem: Table): void {
    if (dataItem) {
      this.tableNumber = dataItem.tableNumber;
      this.tableID = dataItem.id;
      this.maxGuestCount = dataItem.seatingCapacity;
    }
  }

  submitForm(): void {
    if (this.guestCount > this.maxGuestCount) {
      this.snackBar.open(
        `Guest count exceeds the maximum seating capacity of ${this.maxGuestCount}`,
        'Close',
        {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'right',
        }
      );
      return;
    }

    if (this.toTime < this.fromTime) {
      this.snackBar.open(
        `From time cannot be greater than To Time.`,
        'Close',
        {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'right',
        }
      );
      return;
    }

    const requestBody: AddReservation = {
      customerName: this.customerName,
      contactNumber: this.contactNumber,
      guestCount: this.guestCount,
      reservationDate: this.reservationDate,
      fromTime: this.fromTime,
      toTime: this.toTime,
      tableId: this.tableID,
    };

    this.restaurantService.submitReservation(requestBody).subscribe({
      next: (response) => {
        this.snackBar.open(
          `Table reserved successfully.`,
          'Close',
          {
            duration: 3000,
            verticalPosition: 'top',
            horizontalPosition: 'right',
          }
        );
        this.dialogRef.close(); 
      },
      error: (error) => {
        this.snackBar.open(
          error,
          'Close',
          {
            duration: 3000,
            verticalPosition: 'top',
            horizontalPosition: 'right',
          }
        );
      }
    });
  }
}
