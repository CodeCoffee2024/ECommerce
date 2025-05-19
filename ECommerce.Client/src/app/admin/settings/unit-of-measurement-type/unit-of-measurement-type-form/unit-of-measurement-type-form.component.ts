import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginService } from '../../../../login/login.service';
import { CreateSuccess, UpdateSuccess } from '../../../../models/message';
import { ToastType } from '../../../../models/toast';
import { InputTypes } from '../../../../shared/constants/input-type';
import { FormErrorService } from '../../../../shared/services/form-error/form-error.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementTypeService } from '../unit-of-measurement-type.service';
import { UnitOfMeasurementTypeForm } from './unit-of-measurement-type.form';

@Component({
  selector: 'app-unit-of-measurement-type-form',
  templateUrl: './unit-of-measurement-type-form.component.html',
  styleUrl: './unit-of-measurement-type-form.component.scss'
})
export class UnitOfMeasurementTypeFormComponent {
  form: UnitOfMeasurementTypeForm;
  InputTypes = InputTypes;
  isUpdate = false;
  isDropdownLoading = false;
  isLoading = false;
  constructor(
    public activeModal: NgbActiveModal,
    private unitOfMeasurementService: UnitOfMeasurementTypeService,
    private toastService: ToastService,
    private loginService: LoginService,
    private formErrorService: FormErrorService
  ) {
  }
  get title() {
    return this.isUpdate ? 'Unit of Measurement Type Update':'Unit of Measurement Type New';
  }
  submit() {
    if(!this.isUpdate) {
      this.unitOfMeasurementService.create(this.form.submitData).subscribe({
        next: () => {
          this.toastService.add("Success", CreateSuccess("Unit of Measurement Type"), ToastType.SUCCESS);
          this.activeModal.close(true);
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    } else {
      this.unitOfMeasurementService.update(this.form.submitData, this.form.id).subscribe({
        next: () => {
          this.toastService.add("Success", UpdateSuccess("Unit of Measurement Type"), ToastType.SUCCESS);
          this.activeModal.close(true);
        },
        error: (result) => {
          this.formErrorService.setServerErrors(this.form.form, result);
        }
      });
    }
  }
  cancel() {
    this.activeModal.dismiss(false);
  }

}
