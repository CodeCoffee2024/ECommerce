import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductCategoryPermission } from '../../models/inventory/product-category/product-category';
import { AuthGuard } from '../../shared/guards/auth/auth.guard';

const routes: Routes = [
  {
    path: '',
    // component: SettingsComponent,
    canActivate: [AuthGuard],
    data: { permission: "authenticated" },
    children : [
      { 
        path: 'product-categories', 
        loadChildren: () => import('./product-category/product-category.module').then(m => m.ProductCategoryModule),
        canActivate: [AuthGuard],
        data: { permission: ProductCategoryPermission.UserEnableToViewProductCategory }, 
      }
    ]
  },
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InventoryRoutingModule { }
