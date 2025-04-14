import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnitOfMeasurementActivityLogShowComponent } from './unit-of-measurement-activity-log-show/unit-of-measurement-activity-log-show.component';
import { UnitOfMeasurementActivityLogComponent } from './unit-of-measurement-activity-log/unit-of-measurement-activity-log.component';
import { UnitOfMeasurementListingComponent } from './unit-of-measurement-listing/unit-of-measurement-listing.component';
import { UnitOfMeasurementNewComponent } from './unit-of-measurement-new/unit-of-measurement-new.component';
import { UnitOfMeasurementShowComponent } from './unit-of-measurement-show/unit-of-measurement-show.component';
import { UnitOfMeasurementUpdateComponent } from './unit-of-measurement-update/unit-of-measurement-update.component';

const routes: Routes = [
  {
    path: '',
    component: UnitOfMeasurementListingComponent,
  },
  {
    path: 'new',
    component: UnitOfMeasurementNewComponent,
  },
  {
    path: 'view/:id',
    component: UnitOfMeasurementShowComponent,
  },
  {
    path: 'update/:id',
    component: UnitOfMeasurementUpdateComponent,
  },
  {
    path: 'activity-log/:id',
    component: UnitOfMeasurementActivityLogComponent,
  },
  {
    path: 'activity-log/:id/:primaryKey',
    component: UnitOfMeasurementActivityLogShowComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UnitOfMeasurementRoutingModule { }
