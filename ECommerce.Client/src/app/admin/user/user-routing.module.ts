import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListingComponent } from './user-listing/user-listing.component';
import { UserShowComponent } from './user-show/user-show.component';
import { UserActivityLogComponent } from './user-activity-log/user-activity-log.component';
import { UserActivityLogShowComponent } from './user-activity-log-show/user-activity-log-show.component';

const routes: Routes = [
  {
    path: '',
    component: UserListingComponent,
  },
  {
    path: 'view/:id',
    component: UserShowComponent,
  },
  {
    path: 'activity-log/:id',
    component: UserActivityLogComponent,
  },
  {
    path: 'activity-log/:id/:primaryKey',
    component: UserActivityLogShowComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
