import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ApiErrorResponse } from '../../../models/api-error-response.model';
import { ToastService } from '../../services/toast/toast.service';
import { ToastType } from '../../../models/toast';
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private toastService: ToastService) {

  }
  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 400 && error.error?.errors) {
          return throwError(() => this.formatErrors(error.error as ApiErrorResponse));
        }
        return throwError(() => error);
      })
    );
  }

  private formatErrors(apiResponse: ApiErrorResponse): Record<string, string> {
    const formattedErrors: Record<string, string> = {};
    if (apiResponse.error) {
      this.toastService.add("Error", apiResponse.error.description, ToastType.ERROR);
    }
    if (apiResponse.errors) {
      apiResponse.errors.forEach((err) => {
        formattedErrors[err.name] = err.description;
      });
    }
    return formattedErrors;
  }
}