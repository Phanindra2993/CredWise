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
  {path:'apply-form',component:ApplyFormComponent},
  { path: 'apply-form/:id', component: ApplyFormComponent},
  { path: 'loan-status', component: LoanStatusComponent },
  {path:'apply-fd',component:ApplyFdComponent},
  {path:'fd-status',component:FdStatusComponent},
  {path:'repayments',component:RepaymentComponent},
  {path:'payment-history',component:RepaymentHistotyComponent},
  { path: '**', redirectTo: '' },

];
