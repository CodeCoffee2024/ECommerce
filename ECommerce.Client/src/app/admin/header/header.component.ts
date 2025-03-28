import { Component, EventEmitter, Input, Output } from '@angular/core';
import { LoginService } from '../../login/login.service';
import { Router } from '@angular/router';
import { UserService } from '../user/user.service';
import { ModalService } from '../../shared/services/modal/modal.service';
import { ToastService } from '../../shared/services/toast/toast.service';
import { UserResult } from '../../models/user/user';
import { finalize } from 'rxjs';
import { LoadingService } from '../../shared/services/loading/loading.service';
import { Failed } from '../../models/message';
import { ToastType } from '../../models/toast';
import { UserForm } from '../user/user-form/user.form';
import { UserFormComponent } from '../user/user-form/user-form.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

  @Input() isSidebarOpen = false;
  @Input() isDesktop = false;
  @Output() toggleSidebar = new EventEmitter<void>();
  user: UserResult;
  form = new UserForm();
  constructor(
    private loginService: LoginService,
    private userService: UserService,
    private modalService: ModalService,
    private toastService: ToastService,
    private loadingService: LoadingService,
    private router: Router
  ) {

  }

  onToggle(): void {
    this.toggleSidebar.emit();
  }

  updateProfile() {
    this.userService.profile()
    .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
    .subscribe({
      next: (result) => {
        this.user = result.data;
        this.form = new UserForm();
        this.form.initializeForm(true);
        this.form.fill(this.user);
        this.modalService.open(UserFormComponent, {form: this.form, isUpdate: true, isProfileUpdate: true});
      },
      error: () => {
        this.toastService.add("Error", Failed("User"), ToastType.ERROR);
      }
    });
  }
  get img() {
    return localStorage.getItem('img') ?? null;
  }
  get name() {
    return localStorage.getItem('name') ?? null;
  }
  logout(): void {
    this.loginService.logout();
    this.router.navigate(['/'])
    // Implement actual logout logic here (e.g., remove token, redirect)
  }
}
