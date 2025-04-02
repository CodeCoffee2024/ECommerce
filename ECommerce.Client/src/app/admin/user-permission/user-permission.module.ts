import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserPermissionRoutingModule } from './user-permission-routing.module';
import { UserPermissionFormComponent } from './user-permission-form/user-permission-form.component';
import { UserPermissionNewComponent } from './user-permission-new/user-permission-new.component';
import { UserPermissionUpdateComponent } from './user-permission-update/user-permission-update.component';
import { SharedModule } from '../../shared/shared.module';
import { UserPermissionShowComponent } from './user-permission-show/user-permission-show.component';
import { UserPermissionActivityLogComponent } from './user-permission-activity-log/user-permission-activity-log.component';
import { UserPermissionActivityLogShowComponent } from './user-permission-activity-log-show/user-permission-activity-log-show.component';


@NgModule({
  declarations: [
    UserPermissionFormComponent,
    UserPermissionNewComponent,
    UserPermissionUpdateComponent,
    UserPermissionShowComponent,
    UserPermissionActivityLogComponent,
    UserPermissionActivityLogShowComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    UserPermissionRoutingModule
  ]
})
export class UserPermissionModule { }
