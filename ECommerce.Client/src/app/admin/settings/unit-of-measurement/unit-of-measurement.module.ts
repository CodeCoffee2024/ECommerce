import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UnitOfMeasurementRoutingModule } from './unit-of-measurement-routing.module';
import { UnitOfMeasurementShowComponent } from './unit-of-measurement-show/unit-of-measurement-show.component';
import { UnitOfMeasurementActivityLogComponent } from './unit-of-measurement-activity-log/unit-of-measurement-activity-log.component';
import { UnitOfMeasurementActivityLogShowComponent } from './unit-of-measurement-activity-log-show/unit-of-measurement-activity-log-show.component';
import { UnitOfMeasurementFormComponent } from './unit-of-measurement-form/unit-of-measurement-form.component';
import { UnitOfMeasurementListingComponent } from './unit-of-measurement-listing/unit-of-measurement-listing.component';
import { UnitOfMeasurementNewComponent } from './unit-of-measurement-new/unit-of-measurement-new.component';
import { UnitOfMeasurementUpdateComponent } from './unit-of-measurement-update/unit-of-measurement-update.component';
import { SharedModule } from '../../../shared/shared.module';
import { UnitOfMeasurementUpdateFormComponent } from './unit-of-measurement-update-form/unit-of-measurement-update-form.component';


@NgModule({
  declarations: [
    UnitOfMeasurementShowComponent,
    UnitOfMeasurementActivityLogComponent,
    UnitOfMeasurementActivityLogShowComponent,
    UnitOfMeasurementFormComponent,
    UnitOfMeasurementListingComponent,
    UnitOfMeasurementNewComponent,
    UnitOfMeasurementUpdateComponent,
    UnitOfMeasurementUpdateFormComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    UnitOfMeasurementRoutingModule
  ]
})
export class UnitOfMeasurementModule { }
