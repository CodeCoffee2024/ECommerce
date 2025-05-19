import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProductCategoryActivityLogShowComponent } from './product-category-activity-log-show/product-category-activity-log-show.component';
import { ProductCategoryActivityLogComponent } from './product-category-activity-log/product-category-activity-log.component';
import { ProductCategoryFormComponent } from './product-category-form/product-category-form.component';
import { ProductCategoryListingComponent } from './product-category-listing/product-category-listing.component';
import { ProductCategoryNewComponent } from './product-category-new/product-category-new.component';
import { ProductCategoryRoutingModule } from './product-category-routing.module';
import { ProductCategoryShowComponent } from './product-category-show/product-category-show.component';
import { ProductCategoryUpdateComponent } from './product-category-update/product-category-update.component';
import { SharedModule } from '../../../shared/shared.module';


@NgModule({
  declarations: [
      ProductCategoryActivityLogComponent,
      ProductCategoryActivityLogShowComponent,
      ProductCategoryNewComponent,
      ProductCategoryListingComponent,
      ProductCategoryShowComponent,
      ProductCategoryFormComponent,
      ProductCategoryUpdateComponent
    ],
  imports: [
    CommonModule,
    SharedModule,
    ProductCategoryRoutingModule
  ]
})
export class ProductCategoryModule { }
