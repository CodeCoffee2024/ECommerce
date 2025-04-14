import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SettingsRoutingModule } from './settings-routing.module';
import { SettingsComponent } from './settings.component';
import { UnitOfMeasurementTypeComponent } from './unit-of-measurement-type/unit-of-measurement-type.component';


@NgModule({
  declarations: [
    SettingsComponent,
    UnitOfMeasurementTypeComponent,
  ],
  imports: [
    CommonModule,
    SettingsRoutingModule
  ]
})
export class SettingsModule { }
