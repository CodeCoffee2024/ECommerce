import { Component, OnInit } from '@angular/core';
import { GenericComponentListing } from '../../../models/abstractions/generic-component-listing';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';
import { UserListingReponse } from '../../../models/user/user-listing-response';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { TitleService } from '../../../shared/services/title/title.service';
import { UserPermissionListingOption } from '../../user-permission/user-permission-listing/user-permission-listing.option';
import { UserPermissionService } from '../../user-permission/user-permission.service';
import { UserForm } from '../user-form/user.form';
import { UserService } from '../user.service';
import { UserListingOption } from './user-listing.option';
import { ModalService } from '../../../shared/services/modal/modal.service';
import { UserFormComponent } from '../user-form/user-form.component';
import { UserPermissionListingReponse } from '../../../models/user-permission/user-permission-listing-response';

@Component({
  selector: 'app-user-listing',
  templateUrl: './user-listing.component.html',
  styleUrl: './user-listing.component.scss'
})
export class UserListingComponent extends GenericComponentListing<UserListingOption> implements OnInit {
  title = 'Users';
  isLoading = false;
  isDropdownLoading = false;
  isDropdownOpen = false;
  results: UserListingReponse[];
  hasMore = false;
  userPermissions = [];
  listingOptionPermission = new UserPermissionListingOption();
  listingData: GenericListingResult<UserListingReponse[]>;
  form: UserForm;
  constructor(
    private titleService: TitleService,
    private userService: UserService,
    private loadingService: LoadingService,
    private userPermissionService: UserPermissionService,
    private modalService: ModalService
  ) {
    super();
    this.titleService.setTitle(this.title);
    this.listingOption = new UserListingOption();
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
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
  goTo(page: number) {
    this.listingOption.page = page;
    this.refresh();
  }
  searchChanged(searchValue) {
    this.listingOption.search = searchValue;
    this.refresh();
  }
  refresh() {
    this.loadingService.show();
    this.userService.getList(this.listingOption).subscribe({
      next: (result) => {
        this.results = result.data.result;
        this.listingData = result.data;
        this.loadingService.hide();
        this.form = new UserForm();
        this.form.initializeForm();
      }
    })
  }
  exportToPDF() {
    this.userService.export(this.listingOption, 'pdf').subscribe({
      next: (result) => {
        this.downloadExportedFile(result, 'pdf', 'Users');
      }
    })
  }
  exportToExcel() {
    this.userService.export(this.listingOption).subscribe({
      next: (result) => {
        this.downloadExportedFile(result, 'excel', 'Users');
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
    const result = await this.modalService.open(UserFormComponent, {form: this.form});
    if (result) {
      this.refresh();
    }
  }
}
