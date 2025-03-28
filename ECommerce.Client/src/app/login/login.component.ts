import { Component, OnInit } from '@angular/core';
import { LoginForm } from '../models/login.model';
import { InputTypes } from '../shared/constants/input-type';
import { LoginService } from './login.service';
import { FormErrorService } from '../shared/services/form-error/form-error.service';
import { PermissionService } from '../shared/services/permission/permission.service';
import { Router } from '@angular/router';
import { LoadingService } from '../shared/services/loading/loading.service';
import { finalize, switchMap } from 'rxjs/operators';
import { UserService } from '../admin/user/user.service';
import { environment } from '../../../environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  InputTypes = InputTypes;
  loginForm: LoginForm = new LoginForm();

  constructor(
    private loginService: LoginService,
    private permissionService: PermissionService,
    private formErrorService: FormErrorService,
    private loadingService: LoadingService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadingService.hide();
  }
  onSubmit(): void {
    
    this.loginForm.form.markAllAsTouched();

    this.loadingService.show();

    this.loginService.login(this.loginForm).pipe(
      switchMap((result) => {
        this.loginService.storeTokens(result.data.accessToken, result.data.refreshToken);
        this.userService.profile().subscribe({
          next: (result) => {
            this.loginService.storeUserInfo("name", result.data.firstName+" "+result.data.lastName);
            this.loginService.storeUserInfo("img", environment.folderPath + result.data.img);
          }
        })
        return this.loginService.getUserPermissions();
      }),
      finalize(() => this.loadingService.hide()) // Ensures loader is hidden after request completes
    ).subscribe({
      next: (result) => {
        this.permissionService.storePermission(result.data?.permissions);
        this.router.navigate(['/admin']);
      },
      error: (error) => {
        this.formErrorService.setServerErrors(this.loginForm.form, error);
      }
    });
  }
}
