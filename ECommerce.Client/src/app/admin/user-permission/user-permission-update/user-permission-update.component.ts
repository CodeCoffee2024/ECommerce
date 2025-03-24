import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Failed, NotFound, UpdateSuccess } from '../../../models/message';
import { FormErrorService } from '../../../shared/services/form-error/form-error.service';
import { ToastService } from '../../../shared/services/toast/toast.service';
import { UserPermissionForm } from '../user-permission-form/user-permission.form';
import { UserPermissionService } from '../user-permission.service';
import { ToastType } from '../../../models/toast';
import { UserPermissionResult } from '../../../models/user-permission/user-permission';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { catchError, finalize, forkJoin } from 'rxjs';

@Component({
  selector: 'app-user-permission-update',
  templateUrl: './user-permission-update.component.html',
  styleUrl: './user-permission-update.component.scss'
})
export class UserPermissionUpdateComponent implements OnInit {
  form: UserPermissionForm = new UserPermissionForm();
  Id: string;
  userPermission:UserPermissionResult = new UserPermissionResult();
  constructor(
    private userPermissionService: UserPermissionService,
    private formErrorService: FormErrorService,
    private toastService: ToastService,
    private loadingService: LoadingService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {

  }
  ngOnInit(): void {
    this.loadingService.show();
    this.Id = this.activatedRoute.snapshot.paramMap.get('id') || null;

    if (!this.Id) {
      this.toastService.add("Error", NotFound("User Permission"), ToastType.ERROR);
      this.router.navigate(['../../']);
      return;
    }

    forkJoin({
      permissions: this.userPermissionService.getPermissions(),
      userPermission: this.userPermissionService.show(this.Id)
    })
    .pipe(
      finalize(() => this.loadingService.hide()), // Ensure loading is hidden after execution
      catchError((error) => {
        this.toastService.add("Error", Failed("User Permission"), ToastType.ERROR);
        this.router.navigate(['../../']);
        throw error;
      })
    )
    .subscribe(({ permissions, userPermission }) => {
      this.form.initializeForm(permissions.data);
      this.userPermission = userPermission.data;
      this.form.fill(userPermission.data);
    });
  }
  submit(data) {
    this.userPermissionService.update(data, this.Id).subscribe({
      next: (result) => {
        this.toastService.add("Success", UpdateSuccess("User Permission"), ToastType.SUCCESS);
        this.router.navigate(['/admin/user-permissions/view/'+result.data.id]);

      }, error: (result) => {
        this.formErrorService.setServerErrors(this.form.form, result);
      }
    })
  }
}
