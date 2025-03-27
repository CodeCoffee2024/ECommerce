import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CreateSuccess, UpdateSuccess } from '../../../models/message';
import { ToastType } from '../../../models/toast';
import { UserPermissionListingReponse } from '../../../models/user-permission/user-permission-listing-response';
import { InputTypes } from '../../../shared/constants/input-type';
import { FormErrorService } from '../../../shared/services/form-error/form-error.service';
import { ToastService } from '../../../shared/services/toast/toast.service';
import { UserPermissionListingOption } from '../../user-permission/user-permission-listing/user-permission-listing.option';
import { UserPermissionService } from '../../user-permission/user-permission.service';
import { UserService } from '../user.service';
import { UserForm } from './user.form';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrl: './user-form.component.scss'
})
export class UserFormComponent implements OnInit {
  form: UserForm;
  InputTypes = InputTypes;
  hasMore = false;
  isUpdate = false;
  isDropdownLoading = false;
  userPermissions = [];
  selectedItems = [];
  listingOptionPermission;
  constructor(
    public activeModal: NgbActiveModal,
    private userService: UserService,
    private userPermissionService: UserPermissionService,
    private toastService: ToastService,
    private formErrorService: FormErrorService
  ) {
    this.listingOptionPermission = new UserPermissionListingOption();
  }
  ngOnInit(): void {
    if (this.isUpdate) {
      this.selectedItems = JSON.parse(this.form.form.get("userPermissions").value);
      this.listingOptionPermission.exclude = this.selectedItems.map(it => it.name).join(",");
    }
  }
  submit() {
    if(!this.isUpdate) {
      this.userService.create(this.form.submitData).subscribe({
        next: () => {
          this.toastService.add("Success", CreateSuccess("User"), ToastType.SUCCESS);
          this.activeModal.close(true);
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    } else {
      this.userService.update(this.form.submitData, this.form.id).subscribe({
        next: () => {
          this.toastService.add("Success", UpdateSuccess("User"), ToastType.SUCCESS);
          this.activeModal.close(true);
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    }
  }

  cancel() {
    this.activeModal.dismiss(false);
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
    this.form.form.get('userPermissions').setValue(selected.map(it => it.id).join(","));
    // this.refresh();
  }
}
