import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ToastComponent } from './components/toast/toast.component';
import { InputComponent } from './components/input/input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { SearchMonoComponent } from './components/search-mono/search-mono.component';
import { LoadingComponent } from './components/loading/loading.component';
import { ThHeaderComponent } from './components/th-header/th-header.component';
import { ListingSummaryComponent } from './components/listing-summary/listing-summary.component';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { FormatNullablePipe } from './pipes/format-nullable.pipe';
import { SearchMultiSelectComponent } from './components/search-multi-select/search-multi-select.component';
import { FieldLabelComponent } from './components/field-label/field-label.component';
import { AuditComponent } from './components/audit/audit.component';


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
  ],
  imports: [
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
  ]
})
export class SharedModule { }
