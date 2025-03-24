import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DeleteSuccess, Failed, NotFound } from '../../../models/message';
import { ToastType } from '../../../models/toast';
import { UserPermissionResult } from '../../../models/user-permission/user-permission';
import { ToastService } from '../../../shared/services/toast/toast.service';
import { UserPermissionService } from '../user-permission.service';
import { ModuleDTO } from '../../../models/module/module';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { finalize } from 'rxjs';
import { ModalService } from '../../../shared/services/modal/modal.service';
import { DeleteConfirmationDialog } from '../../../models/confirmation-dialog';

@Component({
  selector: 'app-user-permission-show',
  templateUrl: './user-permission-show.component.html',
  styleUrl: './user-permission-show.component.scss'
})
export class UserPermissionShowComponent implements OnInit {
  Id: string;
  userPermission:UserPermissionResult = new UserPermissionResult();
  constructor(
    private userPermissionService: UserPermissionService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private loadingService: LoadingService,
    private toastService: ToastService,
    private modalService: ModalService
  ) {

  }
  ngOnInit(): void {
    this.loadingService.show();
    this.Id = this.activatedRoute.snapshot.paramMap.get('id') || null;
    if (!this.Id) {
      this.toastService.add("Error", NotFound("User Permission"), ToastType.ERROR);
      this.router.navigate(['/admin/user-permissions']);
    } else {
      this.userPermissionService.show(this.Id)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.userPermission = result.data;
        },
        error: () => {
          this.toastService.add("Error", Failed("User Permission"), ToastType.ERROR);
          this.router.navigate(['/admin/user-permissions']);
        }
      });
    }
  }
  get modules() {
    return this.userPermission.modulePermissions;
  }
  getPermissions(module: ModuleDTO) {
    return module.permissions;
  }
  isChecked(permission) {
    return this.userPermissions.find(it=> it == permission) ? 'bi bi-check-square' : 'bi bi-square';
  }
  async delete() {
    const dialog = DeleteConfirmationDialog("User Permission");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.userPermissionService.deletePermission(this.Id).subscribe({
        next:() => {
          this.toastService.add("Success", DeleteSuccess("User Permission"), ToastType.SUCCESS);
          this.router.navigate(['/admin/user-permissions']);
      } })
    }
  }
  get userPermissions() {
    return this.userPermission.permissions.split(",");
  }
}
