import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardGuard } from './auth-guard.guard';
import { LoadTestConfigComponent } from './components/load-test-config/load-test-config.component';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    canActivate: [AuthGuardGuard],
    title: 'Home',
  },
  {
    path: 'load-testing/:id',
    component: LoadTestConfigComponent,
    canActivate: [AuthGuardGuard],
    title: 'Config',
  },
  {
    path: 'login',
    component: LoginComponent,
    title: 'Login',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
