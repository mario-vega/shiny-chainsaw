import { Routes } from '@angular/router';
import { CheckinComponent } from './checkin/checkin.component';
import { CustomersComponent } from './customers/customers.component';
import { UsersComponent } from './users/users.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
    { path: 'home', component: CheckinComponent },
    { path: 'customers', component: CustomersComponent },
    { path: 'users', component: UsersComponent },
    { path: 'login', component: LoginComponent },
    { path: '', redirectTo: 'login', pathMatch: 'full' }
];
