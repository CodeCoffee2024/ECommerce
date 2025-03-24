import { Component, OnInit } from '@angular/core';
import { LoginForm } from '../models/login.model';
import { InputTypes } from '../shared/constants/input-type';
import { LoginService } from './login.service';
import { FormErrorService } from '../shared/services/form-error/form-error.service';
import { PermissionService } from '../shared/services/permission/permission.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  InputTypes = InputTypes;
  loginForm: LoginForm;

  constructor(
    private loginService: LoginService,
    private permissionService: PermissionService,
    private formErrorService: FormErrorService,
    private router: Router,
  ) {
  }
  ngOnInit(): void {
    this.loginForm = new LoginForm();
  }
  onSubmit(): void {
    this.loginForm.form.markAllAsTouched();
    this.loginService.login(this.loginForm).subscribe({
      next: (result) => {
        this.loginService.storeTokens(result.data.accessToken, result.data.refreshToken);
        this.loginService.getUserPermissions().subscribe({
          next:(result) => {;
            this.permissionService.storePermission(result.data?.permissions);
            this.router.navigate(['/admin']);
          }
        })
      }, error: (result) => {
        this.formErrorService.setServerErrors(this.loginForm.form, result);
      }
    });
  }

}
