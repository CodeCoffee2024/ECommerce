import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AuthGuard } from '../shared/guards/auth/auth.guard';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { UserPermissionPermission } from '../models/user-permission/user-permission';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    canActivate: [AuthGuard],
    data: { permission: "authenticated" },
    children : [
      { 
        path: 'user-permissions', 
        loadChildren: () => import('./user-permission/user-permission.module').then(m => m.UserPermissionModule),
        canActivate: [AuthGuard],
        data: { permission: UserPermissionPermission.UserEnableToViewUserPermission }, 
      },
    ]
  },
  {
    path: '401',
    component: UnauthorizedComponent,
    children: [
    ]
  },
  { path: '**', redirectTo: 'admin/dashboard' }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
