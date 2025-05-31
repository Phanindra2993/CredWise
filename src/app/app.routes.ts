import { Routes } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { LoanDetailsComponent } from './pages/loan-details/loan-details.component';
import { ApplyFormComponent } from './pages/apply-form/apply-form.component';
import { LoanStatusComponent } from './pages/loan-status/loan-status.component';
import { ApplyFdComponent } from './pages/apply-fd/apply-fd.component';
import { FdStatusComponent } from './pages/fd-status/fd-status.component';
import { RepaymentComponent } from './pages/repayment/repayment.component';
import { RepaymentHistotyComponent } from './pages/repayment-histoty/repayment-histoty.component';
import { authGuardTsGuard } from '../_guards/auth.guard.ts.guard';
export const routes: Routes = [
  { path: '', component:DashboardComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'loan-details/:id', component: LoanDetailsComponent },
  {path:'apply-form',component:ApplyFormComponent,canActivate:[authGuardTsGuard]},
  { path: 'apply-form/:id', component: ApplyFormComponent,canActivate:[authGuardTsGuard]},
  { path: 'loan-status', component: LoanStatusComponent,canActivate:[authGuardTsGuard]},
  {path:'apply-fd',component:ApplyFdComponent,canActivate:[authGuardTsGuard]},
  {path:'fd-status',component:FdStatusComponent,canActivate:[authGuardTsGuard]},
  {path:'repayments',component:RepaymentComponent,canActivate:[authGuardTsGuard]},
  {path:'payment-history',component:RepaymentHistotyComponent,canActivate:[authGuardTsGuard]},
  { path: '**', redirectTo: '' },

];
