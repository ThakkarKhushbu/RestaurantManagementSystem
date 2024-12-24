import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DialogsModule } from "@progress/kendo-angular-dialog";
import { RestaurantService } from '../../services/restaurant.service';
import { Subscription } from 'rxjs';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AddReservation } from '../../models/addReservation';

@Component({
  selector: 'app-reservation-form',
  imports: [CommonModule,DialogsModule,FormsModule,HttpClientModule],
  providers:[RestaurantService],
  templateUrl: './reservation-form.component.html',
  styleUrl: './reservation-form.component.scss',
  standalone: true
})
export class ReservationFormComponent implements OnInit,OnDestroy {
  tableNumber: number =0;
  tableID : string='';
  customerName: string = '';
  contactNumber: string = '';
  guestCount: number = 0;
  reservationDate: string = '';
  fromTime: string = '';
  toTime: string = '';
  maxGuestCount :number=0;
  private tableDataSubscription: Subscription;

  constructor(private route: ActivatedRoute,private restaurantService : RestaurantService) {
    this.tableDataSubscription = this.restaurantService.dataItem$.subscribe(dataItem => {
      if(dataItem){
        this.tableNumber = dataItem.tableNumber;
        this.tableID = dataItem.id
        this.maxGuestCount = dataItem.seatingCapacity;
      }
    });
  }

  ngOnInit(): void {
  }
  
  submitForm(): void {
    debugger
    const requestBody: AddReservation = {
      customerName: this.customerName,
      contactNumber: this.contactNumber,
      guestCount: this.guestCount, // Assuming guestCount is a form field
      reservationDate: this.reservationDate, // Assuming reservationDate is a form field
      fromTime: this.fromTime, // Assuming fromTime is a form field
      toTime: this.toTime, // Assuming toTime is a form field
      tableId: this.tableID // Assuming tableId is passed or available in the form
    };
  
      // Handle form submission logic, such as calling a service to post the data to the backend
  this.restaurantService.submitReservation(requestBody).subscribe(response => {
    // Handle the response after submitting the reservation
    console.log('Reservation response:', response);
  });
  }

  ngOnDestroy(): void {
    this.tableDataSubscription.unsubscribe();
  }
}
