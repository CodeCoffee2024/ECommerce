import { Component, OnInit } from '@angular/core';
import { UserResult } from '../../../models/user/user';
import { UserService } from '../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoadingService } from '../../../shared/services/loading/loading.service';
import { ToastService } from '../../../shared/services/toast/toast.service';
import { ModalService } from '../../../shared/services/modal/modal.service';
import { DeleteSuccess, Failed, NotFound } from '../../../models/message';
import { ToastType } from '../../../models/toast';
import { finalize } from 'rxjs';
import { DeleteConfirmationDialog } from '../../../models/confirmation-dialog';
import { UserFormComponent } from '../user-form/user-form.component';
import { UserForm } from '../user-form/user.form';

@Component({
  selector: 'app-user-show',
  templateUrl: './user-show.component.html',
  styleUrl: './user-show.component.scss'
})
export class UserShowComponent implements OnInit {
  Id: string;
  user:UserResult = new UserResult();
  form: UserForm;
  constructor(
    private userService: UserService,
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
      this.toastService.add("Error", NotFound("User"), ToastType.ERROR);
      this.router.navigate(['/admin/users']);
    } else {
      this.userService.show(this.Id)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.user = result.data;
        },
        error: () => {
          this.toastService.add("Error", Failed("User"), ToastType.ERROR);
          this.router.navigate(['/admin/users']);
        }
      });
    }
  }
  async update() {
    this.form = new UserForm();
    this.form.initializeForm();
    this.form.fill(this.user);
    const result = await this.modalService.open(UserFormComponent, {form: this.form, isUpdate: true});
    if (result) {
      console.log(result);
    }
  }
  async delete() {
    const dialog = DeleteConfirmationDialog("User");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.userService.deleteUser(this.Id).subscribe({
        next:() => {
          this.toastService.add("Success", DeleteSuccess("User"), ToastType.SUCCESS);
          this.router.navigate(['/admin/user-permissions']);
      } })
    }
  }
}
