import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { GridModule } from '@progress/kendo-angular-grid';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { DrawerModule, LayoutModule } from '@progress/kendo-angular-layout';
import { AppBarModule } from '@progress/kendo-angular-navigation';
import { CommonModule } from '@angular/common';
import { DialogModule } from '@progress/kendo-angular-dialog';

@Component({
  selector: 'app-root',
  imports: [CommonModule,
    RouterOutlet,
    HttpClientModule,
    ReactiveFormsModule,
    GridModule,
    ButtonsModule,
    LayoutModule,
    DropDownsModule,
    InputsModule,
    DateInputsModule,
    AppBarModule,
    DrawerModule,
    DialogModule 
    ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  standalone: true
})
export class AppComponent {
  title = 'restaurant-management';
  constructor() {}

}
