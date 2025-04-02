import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { UserFormComponent } from './user-form/user-form.component';
import { UserListingComponent } from './user-listing/user-listing.component';
import { UserNewComponent } from './user-new/user-new.component';
import { UserShowComponent } from './user-show/user-show.component';
import { UserUpdateComponent } from './user-update/user-update.component';
import { UserActivityLogComponent } from './user-activity-log/user-activity-log.component';
import { UserActivityLogShowComponent } from './user-activity-log-show/user-activity-log-show.component';


@NgModule({
  declarations: [
    UserFormComponent,
    UserListingComponent,
    UserNewComponent,
    UserShowComponent,
    UserUpdateComponent,
    UserActivityLogComponent,
    UserActivityLogShowComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    UserRoutingModule
  ]
})
export class UserModule { }
