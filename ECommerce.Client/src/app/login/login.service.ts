import { Injectable } from '@angular/core';
import { GenericService } from '../shared/services/generic/generic.service';
import { HttpClient } from '@angular/common/http';
import { LoginForm } from '../models/login.model';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { ApiResult } from '../models/result.model';
import { TokenResponse } from '../models/token-response.model';
import { UserUserPermissionResponse } from '../models/user-permission/user-permission-response';

@Injectable({
  providedIn: 'root'
})
export class LoginService extends GenericService {
  private controller='auth/';
  private refreshTokenSubject = new BehaviorSubject<string | null>(null);
  private isRefreshing = false;
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }
  login(payload: LoginForm): Observable<ApiResult<TokenResponse>> {
    return this.post(this.controller, payload.form.value);
  }
  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  refreshToken(): Observable<ApiResult<TokenResponse>> {
    const refreshToken = this.getRefreshToken();
    if (!refreshToken) return throwError(() => new Error('No refresh token available'));

    return this.post<ApiResult<TokenResponse>>(`${this.controller}refresh`, { refreshToken }).pipe(
      tap(result => {
        if (result.isSuccess && result.data) {
          this.storeTokens(result.data.accessToken, result.data.refreshToken);
          this.refreshTokenSubject.next(result.data.accessToken);
        }
      }),
      catchError(error => throwError(() => error))
    );
  }

  getUserPermissions(): Observable<ApiResult<UserUserPermissionResponse>> {
    return this.get(this.controller+"GetUserAccess", null, true);
  }

  storeTokens(accessToken: string, refreshToken: string): void {
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('refreshToken', refreshToken);
  }
  storeUserInfo(key: string, value: string) {
    localStorage.setItem(key, value);
  }
  logout(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('permission');
    localStorage.removeItem('refreshToken');
  }
}
