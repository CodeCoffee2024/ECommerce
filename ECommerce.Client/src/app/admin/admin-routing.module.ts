import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AuthGuard } from '../shared/guards/auth/auth.guard';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { UserPermissionPermission } from '../models/user-permission/user-permission';
import { UserPermission } from '../models/user/user';
import { UnitOfMeasurementTypePermission } from '../models/settings/unit-of-measurement-type/unit-of-measurement-type';
import { UnitOfMeasurementPermission } from '../models/settings/unit-of-measurement/unit-of-measurement';
import { ProductCategoryPermission } from '../models/inventory/product-category/product-category';

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
      { 
        path: 'inventory', 
        loadChildren: () => import('./inventory/inventory.module').then(m => m.InventoryModule),
        canActivate: [AuthGuard],
        data: { 
          optionalPermission: 
            ProductCategoryPermission.UserEnableToViewProductCategory + "," 
            // UnitOfMeasurementPermission.UserEnableToViewUnitOfMeasurement
        }, 
      },
      { 
        path: 'users', 
        loadChildren: () => import('./user/user.module').then(m => m.UserModule),
        canActivate: [AuthGuard],
        data: { permission: UserPermission.UserEnableToViewUser }, 
      },
      { 
        path: 'settings', 
        loadChildren: () => import('./settings/settings.module').then(m => m.SettingsModule),
        canActivate: [AuthGuard],
        data: { 
          optionalPermission: 
            UnitOfMeasurementTypePermission.UserEnableToViewUnitOfMeasurementType + "," + 
            UnitOfMeasurementPermission.UserEnableToViewUnitOfMeasurement
        }, 
      },
      {
        path: '401',
        component: UnauthorizedComponent,
      }
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
