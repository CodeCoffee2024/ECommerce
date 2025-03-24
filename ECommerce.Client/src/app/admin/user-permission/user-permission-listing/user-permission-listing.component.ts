import { Component, OnInit } from '@angular/core';
import { TitleService } from '../../../shared/services/title/title.service';
import { UserPermissionService } from '../user-permission.service';
import { UserPermissionListingOption } from './user-permission-listing.option';
import { UserPermissionListingReponse } from '../../../models/user-permission/user-permission-listing-response';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { GenericComponentListing } from '../../../models/abstractions/generic-component-listing';

@Component({
  selector: 'app-user-permission-listing',
  templateUrl: './user-permission-listing.component.html',
  styleUrl: './user-permission-listing.component.scss'
})
export class UserPermissionListingComponent extends GenericComponentListing<UserPermissionListingOption> implements OnInit {
  title = 'User Permissions';
  isLoading = false;
  isDropdownOpen = false;
  results: UserPermissionListingReponse[];
  listingData: GenericListingResult<UserPermissionListingReponse[]>;
  constructor(
    private titleService: TitleService,
    private userPermissionService: UserPermissionService,
    private loadingService: LoadingService,
  ) {
    super();
    this.titleService.setTitle(this.title);
    this.listingOption = new UserPermissionListingOption();
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
    this.userPermissionService.getList(this.listingOption).subscribe({
      next: (result) => {
        this.results = result.data.result;
        this.listingData = result.data;
        this.loadingService.hide();
      }
    })
  }
  exportToPDF() {
    this.userPermissionService.export(this.listingOption, 'pdf').subscribe({
      next: (result) => {
        this.downloadExportedFile(result, 'pdf', 'UserPermission');
      }
    })
  }
  exportToExcel() {
    this.userPermissionService.export(this.listingOption).subscribe({
      next: (result) => {
        this.downloadExportedFile(result, 'excel', 'UserPermission');
      }
    })
  }

}
