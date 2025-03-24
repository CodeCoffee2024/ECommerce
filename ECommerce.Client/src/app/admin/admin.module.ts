import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AdminComponent } from './admin.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { UserPermissionListingComponent } from './user-permission/user-permission-listing/user-permission-listing.component';


@NgModule({
  declarations: [
    HeaderComponent,
    SidebarComponent,
    AdminComponent,
    UnauthorizedComponent,
    UserPermissionListingComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
