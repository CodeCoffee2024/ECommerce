import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from './shared/shared.module';
import { LoginComponent } from './login/login.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { ErrorInterceptor } from "./shared/interceptors/error/error.interceptor";
import { AuthInterceptor } from "./shared/interceptors/auth/auth.interceptor";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    SharedModule,
    BrowserAnimationsModule,
    NgbModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
