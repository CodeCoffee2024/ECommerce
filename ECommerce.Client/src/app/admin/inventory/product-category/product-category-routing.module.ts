import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductCategoryListingComponent } from './product-category-listing/product-category-listing.component';
import { ProductCategoryNewComponent } from './product-category-new/product-category-new.component';
import { ProductCategoryShowComponent } from './product-category-show/product-category-show.component';
import { ProductCategoryUpdateComponent } from './product-category-update/product-category-update.component';
import { ProductCategoryActivityLogComponent } from './product-category-activity-log/product-category-activity-log.component';
import { ProductCategoryActivityLogShowComponent } from './product-category-activity-log-show/product-category-activity-log-show.component';

const routes: Routes = [
  {
    path: '',
    component: ProductCategoryListingComponent,
  },
  {
    path: 'new',
    component: ProductCategoryNewComponent,
  },
  {
    path: 'view/:id',
    component: ProductCategoryShowComponent,
  },
  {
    path: 'update/:id',
    component: ProductCategoryUpdateComponent,
  },
  {
    path: 'activity-log/:id',
    component: ProductCategoryActivityLogComponent,
  },
  {
    path: 'activity-log/:id/:primaryKey',
    component: ProductCategoryActivityLogShowComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductCategoryRoutingModule { }
