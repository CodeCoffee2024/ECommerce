import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../../login/login.service';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';
import { UserPermissionPermission } from '../../../models/user-permission/user-permission';
import { UserPermissionListingReponse } from '../../../models/user-permission/user-permission-listing-response';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { TitleService } from '../../../shared/services/title/title.service';
import { UserPermissionService } from '../user-permission.service';
import { UserPermissionListingOption } from './user-permission-listing.option';

@Component({
  selector: 'app-user-permission-listing',
  templateUrl: './user-permission-listing.component.html',
  styleUrl: './user-permission-listing.component.scss'
})
export class UserPermissionListingComponent extends BaseComponent implements OnInit {
  title = 'User Permissions';
  isDropdownOpen = false;
  UserEnableToModifyUserPermission = UserPermissionPermission.UserEnableToModifyUserPermission;
  listingFormat: UserPermissionListingReponse[];
  constructor(
    private authService: LoginService,
    private titleService: TitleService,
    protected userPermissionService: UserPermissionService,
    private loadingService: LoadingService,
  ) {
    super(authService, titleService, loadingService);
    this.titleService.setTitle(this.title);
    this.setGenerics(
      new UserPermissionListingOption(),
      'UserPermission',
      this.userPermissionService,
      GenericListingResult<UserPermissionListingReponse[]>,
      this.listingFormat
    )
  }
  ngOnInit(): void {
    this.loadingService.loading$.subscribe(status => {
      this.isLoading = status;
    });
    this.refresh();
    this.refreshBySort.subscribe(it => {
      if (it.value == true) {
        this.listingOption.sortBy = it.sortBy;
        this.listingOption.sortDirection = it.sortDirection;
        this.refresh();
        this.turnOffSortEvent();
      }
    })
  }
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
  goTo(page: number) {
    this.listingOption.page = page;
    this.refresh();
  }

}
