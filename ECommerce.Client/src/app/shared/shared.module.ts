import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ActivityLogEntityComponent } from './components/activity-log-entity/activity-log-entity.component';
import { ActivityLogListComponent } from './components/activity-log-list/activity-log-list.component';
import { AuditComponent } from './components/audit/audit.component';
import { BaseComponent } from './components/base/base.component';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { FieldLabelComponent } from './components/field-label/field-label.component';
import { InputComponent } from './components/input/input.component';
import { ListingSummaryComponent } from './components/listing-summary/listing-summary.component';
import { LoadingComponent } from './components/loading/loading.component';
import { SearchMonoComponent } from './components/search-mono/search-mono.component';
import { SearchMultiSelectComponent } from './components/search-multi-select/search-multi-select.component';
import { ThHeaderComponent } from './components/th-header/th-header.component';
import { ToastComponent } from './components/toast/toast.component';
import { FormatNullablePipe } from './pipes/format-nullable.pipe';
import { StatusLabelComponent } from './components/status-label/status-label.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    ToastComponent,
    InputComponent,
    SearchMonoComponent,
    LoadingComponent,
    ThHeaderComponent,
    ListingSummaryComponent,
    ConfirmationDialogComponent,
    SearchMultiSelectComponent,
    FieldLabelComponent,
    AuditComponent,
    ActivityLogListComponent,
    ActivityLogEntityComponent,
    BaseComponent,
    StatusLabelComponent,
  ],
  imports: [
    RouterModule,
    CommonModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    FormatNullablePipe
  ],
  exports : [
    ToastComponent,
    InputComponent,
    ReactiveFormsModule,
    NgbModule,
    HttpClientModule,
    SearchMonoComponent,
    FormsModule,
    LoadingComponent,
    ThHeaderComponent,
    ListingSummaryComponent,
    FormatNullablePipe,
    SearchMultiSelectComponent,
    FieldLabelComponent,
    AuditComponent,
    ActivityLogListComponent,
    ActivityLogEntityComponent,
    BaseComponent,
    StatusLabelComponent
  ]
})
export class SharedModule { }
