import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { routes as childRoutes } from './dashboard/dashboard.module';
import { AuthGuard } from './guards/auth.guard';
import { RedirectComponent } from './redirect/redirect.component';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';

export const routes: Routes = [
  {
    path: 'login/:id',
    component: LoginComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [ AuthGuard ],
    children: childRoutes
  },
  {
    // Путь, на который перешёл пользователь
    path: '',
    // Путь переадресации
    redirectTo: 'dashboard',
    // Указанный адрес должен полностью
    // соответствовать маршруту
    pathMatch: 'full'
  },
  { path: 'auth-callback',
    component: AuthCallbackComponent
  },
  {
    path:':id',
    component: RedirectComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
