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
import { LoginService } from '../../../login/login.service';
import { environment } from '../../../../../environment';

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
  isProfileUpdate = false;
  isDropdownLoading = false;
  invalidDimension = false;
  isLoading = false;
  invalidFileSize = false;
  isImgModified = false;
  userPermissions = [];
  selectedItems = [];
  listingOptionPermission;
  constructor(
    public activeModal: NgbActiveModal,
    private userService: UserService,
    private userPermissionService: UserPermissionService,
    private toastService: ToastService,
    private loginService: LoginService,
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
  get title() {
    return this.isUpdate ? 'User Update':'User New';
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
    } else if(!this.isProfileUpdate) {
      this.userService.update(this.form.submitData, this.form.id).subscribe({
        next: () => {
          this.toastService.add("Success", UpdateSuccess("User"), ToastType.SUCCESS);
          this.activeModal.close(true);
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    } else {
      this.isLoading = true;
      this.userService.updateProfile(this.form.updateProfileData).subscribe({
        next: () => {
          this.toastService.add("Success", UpdateSuccess("User"), ToastType.SUCCESS);
          this.userService.profile().subscribe({
            next: (result) => {
              this.isLoading = false;
              this.loginService.storeUserInfo('name', result.data.firstName + " " + result.data.lastName);
              this.loginService.storeUserInfo('img', environment.folderPath + result.data.img);
              this.isImgModified = false;
              this.activeModal.close(true);
            }
          })
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    }
  }

  get img() {
    return this.isImgModified ? this.form.form.get('img').value : environment.folderPath + this.form.form.get('img').value;
  }
  
  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
        const file = input.files[0];
        const reader = new FileReader();
        reader.readAsDataURL(file);
        const maxSizeMB = 1;
        if (file.size > maxSizeMB * 1024 * 1024) {
          this.invalidFileSize = true;
          return;
        }
        
        reader.onload = () => {
          const img = new Image();
          img.src = reader.result as string;
          img.onload = () =>{
            if (img.width !== img.height) {
              this.invalidDimension = true;
              return;
            }
            this.isImgModified = true;
            this.invalidFileSize = false;
            this.invalidDimension = false;
            this.form.form.patchValue({ img: reader.result });
            this.form.form.patchValue({ imgFile: file });
          }
        };
    }
  }
  get invalidMessages() {
    return this.invalidDimension ? 'File must be square image (e.g. 100x100)' : this.invalidFileSize ? 'File must not exceed 1MB' : '';
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
