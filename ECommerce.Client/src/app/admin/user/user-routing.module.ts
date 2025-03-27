import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListingComponent } from './user-listing/user-listing.component';
import { UserShowComponent } from './user-show/user-show.component';

const routes: Routes = [
  {
    path: '',
    component: UserListingComponent,
  },
  {
    path: 'view/:id',
    component: UserShowComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
