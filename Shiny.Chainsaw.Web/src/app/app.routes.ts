import { Routes } from '@angular/router';
import { CheckinComponent } from './checkin/checkin.component';
import { CustomersComponent } from './customers/customers.component';
import { UsersComponent } from './users/users.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './services/auth-guard.service';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'home', component: CheckinComponent, canActivate: [AuthGuard] },
    { path: 'customers', component: CustomersComponent, canActivate: [AuthGuard]  },
    { path: 'users', component: UsersComponent, canActivate: [AuthGuard]  },
    { path: '**', redirectTo: 'login' }
];
