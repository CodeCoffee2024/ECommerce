import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../../login/login.service';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';
import { UserPermissionListingReponse } from '../../../models/user-permission/user-permission-listing-response';
import { UserPermission } from '../../../models/user/user';
import { UserListingReponse } from '../../../models/user/user-listing-response';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { ModalService } from '../../../shared/services/modal/modal.service';
import { TitleService } from '../../../shared/services/title/title.service';
import { UserPermissionListingOption } from '../../user-permission/user-permission-listing/user-permission-listing.option';
import { UserPermissionService } from '../../user-permission/user-permission.service';
import { UserFormComponent } from '../user-form/user-form.component';
import { UserForm } from '../user-form/user.form';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-listing',
  templateUrl: './user-listing.component.html',
  styleUrl: './user-listing.component.scss'
})
export class UserListingComponent extends BaseComponent implements OnInit {
  title = 'Users';
  isDropdownOpen = false;
  isDropdownLoading = false;
  userPermissions = [];
  hasMore = false;
  UserEnableToModifyUser = UserPermission.UserEnableToModifyUser;
  listingOptionPermission = new UserPermissionListingOption();
  form: UserForm = new UserForm();
  listingFormat: UserListingReponse[];
  constructor(
    private authService: LoginService,
    private titleService: TitleService,
    protected userPermissionService: UserPermissionService,
    private loadingService: LoadingService,
    private userService: UserService,
    private modalService: ModalService,
  ) {
    super(authService, titleService, loadingService);
    this.titleService.setTitle(this.title);
    this.setGenerics(
      new UserPermissionListingOption(),
      'User',
      this.userService,
      GenericListingResult<UserPermissionListingReponse[]>,
      this.listingFormat
    )
    this.listingOptionPermission = new UserPermissionListingOption();
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
  formatPermission(permissions: string): string {
    if (!permissions?.trim()) return "";
  
    return permissions
      .split(",")
      .map(permission => 
        `<small class='bg-secondary text-white mr-2 rounded p-1 m-1 text-nowrap d-inline-block'>${permission.trim()}</small>`
      )
      .join("");
  }
  async onSearchChanged({ search, page, clear = false }: { search: string; page: number, clear: boolean }) {
    this.isDropdownLoading = true;
    this.listingOptionPermission.search = search;
    this.listingOptionPermission.page = page;
    if (clear) {
      this.userPermissions = [];
    }
    this.userPermissionService.dropdown(this.listingOptionPermission).subscribe(it => {
      this.hasMore = page * it.data.totalRecords < it.data.totalPages * it.data.totalRecords;
      this.userPermissions = clear
      ? it.data.result
      : [...this.userPermissions, ...it.data.result];
      this.isDropdownLoading = false;
    })
  }

  onSelectionChange(selected:UserPermissionListingReponse[]): void {
    this.listingOptionPermission.exclude = selected.map(it => it.name).join(",");
    this.listingOption.userPermissions = selected.map(it => it.name).join(",");
    this.refresh();
  }
  async new() {
    this.form.initializeForm();
    const result = await this.modalService.open(UserFormComponent, {form: this.form});
    if (result) {
      this.refresh();
    }
  }
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
  goTo(page: number) {
    this.listingOption.page = page;
    this.refresh();
  }
}
