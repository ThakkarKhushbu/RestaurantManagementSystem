import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: '/tables', pathMatch: 'full' },
  { 
    path: 'tables', 
    loadComponent: () => import('./components/table-list/table-list.component')
      .then(m => m.TableListComponent)
  },
  { 
    path: 'reservation', 
    loadComponent: () => import('./components/reservation-form/reservation-form.component')
      .then(m => m.ReservationFormComponent)
  }
];