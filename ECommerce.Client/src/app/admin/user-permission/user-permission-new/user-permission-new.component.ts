import { Component, OnInit } from '@angular/core';
import { UserPermissionService } from '../user-permission.service';
import { UserPermissionForm } from '../user-permission-form/user-permission.form';
import { FormErrorService } from '../../../shared/services/form-error/form-error.service';
import { Router } from '@angular/router';
import { ToastService } from '../../../shared/services/toast/toast.service';
import { CreateSuccess } from '../../../models/message';
import { ToastType } from '../../../models/toast';
import { LoadingService } from '../../../shared/services/loading/loading.service';

@Component({
  selector: 'app-user-permission-new',
  templateUrl: './user-permission-new.component.html',
  styleUrl: './user-permission-new.component.scss'
})
export class UserPermissionNewComponent implements OnInit {
  form: UserPermissionForm = new UserPermissionForm();
  constructor(
    private userPermissionService: UserPermissionService,
    private formErrorService: FormErrorService,
    private toastService: ToastService,
    private loadingService: LoadingService,
    private router: Router
  ) {

  }
  ngOnInit(): void {
    this.loadingService.show();
    this.userPermissionService.getPermissions().subscribe({
      next: (result) => {
        this.form.initializeForm(result.data);
        this.loadingService.hide();
      }
    })
  }
  submit(data) {
    this.userPermissionService.create(data).subscribe({
      next: (result) => {
        this.toastService.add("Success", CreateSuccess("User Permission"), ToastType.SUCCESS);
        this.router.navigate(['/admin/user-permissions/view/'+result.data.id]);

      }, error: (result) => {
        this.formErrorService.setServerErrors(this.form.form, result);
      }
    })
  }
}
