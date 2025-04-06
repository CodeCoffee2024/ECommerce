import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnitOfMeasurementTypeListingComponent } from './unit-of-measurement-type-listing/unit-of-measurement-type-listing.component';
import { UnitOfMeasurementTypeNewComponent } from './unit-of-measurement-type-new/unit-of-measurement-type-new.component';
import { UnitOfMeasurementTypeShowComponent } from './unit-of-measurement-type-show/unit-of-measurement-type-show.component';
import { UnitOfMeasurementTypeUpdateComponent } from './unit-of-measurement-type-update/unit-of-measurement-type-update.component';
import { UnitOfMeasurementTypeActivityLogComponent } from './unit-of-measurement-type-activity-log/unit-of-measurement-type-activity-log.component';
import { UnitOfMeasurementTypeActivityLogShowComponent } from './unit-of-measurement-type-activity-log-show/unit-of-measurement-type-activity-log-show.component';

const routes: Routes = [
  {
    path: '',
    component: UnitOfMeasurementTypeListingComponent,
  },
  {
    path: 'new',
    component: UnitOfMeasurementTypeNewComponent,
  },
  {
    path: 'view/:id',
    component: UnitOfMeasurementTypeShowComponent,
  },
  {
    path: 'update/:id',
    component: UnitOfMeasurementTypeUpdateComponent,
  },
  {
    path: 'activity-log/:id',
    component: UnitOfMeasurementTypeActivityLogComponent,
  },
  {
    path: 'activity-log/:id/:primaryKey',
    component: UnitOfMeasurementTypeActivityLogShowComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UnitOfMeasurementTypeRoutingModule { }
