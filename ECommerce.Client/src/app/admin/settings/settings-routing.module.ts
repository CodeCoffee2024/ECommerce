import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SettingsComponent } from './settings.component';
import { AuthGuard } from '../../shared/guards/auth/auth.guard';
import { UnitOfMeasurementTypePermission } from '../../models/settings/unit-of-measurement-type/unit-of-measurement-type';
import { UnitOfMeasurementPermission } from '../../models/settings/unit-of-measurement/unit-of-measurement';

const routes: Routes = [
  {
    path: '',
    component: SettingsComponent,
    canActivate: [AuthGuard],
    data: { permission: "authenticated" },
    children : [
      { 
        path: 'unit-of-measurement-types', 
        loadChildren: () => import('./unit-of-measurement-type/unit-of-measurement-type.module').then(m => m.UnitOfMeasurementTypeModule),
        canActivate: [AuthGuard],
        data: { permission: UnitOfMeasurementTypePermission.UserEnableToViewUnitOfMeasurementType }, 
      },
      { 
        path: 'unit-of-measurements', 
        loadChildren: () => import('./unit-of-measurement/unit-of-measurement.module').then(m => m.UnitOfMeasurementModule),
        canActivate: [AuthGuard],
        data: { permission: UnitOfMeasurementPermission.UserEnableToViewUnitOfMeasurement }, 
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
