import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserPermissionListingComponent } from './user-permission-listing/user-permission-listing.component';
import { UserPermissionNewComponent } from './user-permission-new/user-permission-new.component';
import { UserPermissionShowComponent } from './user-permission-show/user-permission-show.component';
import { UserPermissionUpdateComponent } from './user-permission-update/user-permission-update.component';

const routes: Routes = [
  {
    path: '',
    component: UserPermissionListingComponent,
  },
  {
    path: 'new',
    component: UserPermissionNewComponent,
  },
  {
    path: 'view/:id',
    component: UserPermissionShowComponent,
  },
  {
    path: 'update/:id',
    component: UserPermissionUpdateComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserPermissionRoutingModule { }
