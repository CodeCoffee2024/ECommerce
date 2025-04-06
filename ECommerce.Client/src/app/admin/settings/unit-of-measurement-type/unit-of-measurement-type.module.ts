import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UnitOfMeasurementTypeRoutingModule } from './unit-of-measurement-type-routing.module';
import { UnitOfMeasurementTypeListingComponent } from './unit-of-measurement-type-listing/unit-of-measurement-type-listing.component';
import { UnitOfMeasurementTypeNewComponent } from './unit-of-measurement-type-new/unit-of-measurement-type-new.component';
import { UnitOfMeasurementTypeUpdateComponent } from './unit-of-measurement-type-update/unit-of-measurement-type-update.component';
import { UnitOfMeasurementTypeShowComponent } from './unit-of-measurement-type-show/unit-of-measurement-type-show.component';
import { UnitOfMeasurementTypeActivityLogComponent } from './unit-of-measurement-type-activity-log/unit-of-measurement-type-activity-log.component';
import { UnitOfMeasurementTypeActivityLogShowComponent } from './unit-of-measurement-type-activity-log-show/unit-of-measurement-type-activity-log-show.component';
import { UnitOfMeasurementTypeFormComponent } from './unit-of-measurement-type-form/unit-of-measurement-type-form.component';
import { SharedModule } from '../../../shared/shared.module';


@NgModule({
  declarations: [
    UnitOfMeasurementTypeListingComponent,
    UnitOfMeasurementTypeNewComponent,
    UnitOfMeasurementTypeUpdateComponent,
    UnitOfMeasurementTypeShowComponent,
    UnitOfMeasurementTypeActivityLogComponent,
    UnitOfMeasurementTypeActivityLogShowComponent,
    UnitOfMeasurementTypeFormComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    UnitOfMeasurementTypeRoutingModule
  ]
})
export class UnitOfMeasurementTypeModule { }
