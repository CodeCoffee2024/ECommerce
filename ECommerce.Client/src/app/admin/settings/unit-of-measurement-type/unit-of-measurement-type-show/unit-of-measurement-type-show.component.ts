import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteConfirmationDialog, DisableConfirmationDialog, EnableConfirmationDialog } from '../../../../models/confirmation-dialog';
import { DeleteSuccess, DisableSuccess, EnableSuccess } from '../../../../models/message';
import { UnitOfMeasurementTypeResult } from '../../../../models/settings/unit-of-measurement-type/unit-of-measurement-type';
import { ToastType } from '../../../../models/toast';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementTypeFormComponent } from '../unit-of-measurement-type-form/unit-of-measurement-type-form.component';
import { UnitOfMeasurementTypeForm } from '../unit-of-measurement-type-form/unit-of-measurement-type.form';
import { UnitOfMeasurementTypeService } from '../unit-of-measurement-type.service';

@Component({
  selector: 'app-unit-of-measurement-type-show',
  templateUrl: './unit-of-measurement-type-show.component.html',
  styleUrl: './unit-of-measurement-type-show.component.scss'
})
export class UnitOfMeasurementTypeShowComponent {
  unitOfMeasurement:UnitOfMeasurementTypeResult = new UnitOfMeasurementTypeResult();
  form: UnitOfMeasurementTypeForm;
  constructor(
    private unitOfMeasurementTypeService: UnitOfMeasurementTypeService,
    private router: Router,
    private loadingService: LoadingService,
    private toastService: ToastService,
    private activeModal: NgbActiveModal,
    private modalService: ModalService
  ) {

  }
  cancel() {
    this.activeModal.close(false);
  }
  goToLog() {
    this.router.navigate(['/admin/settings/unit-of-measurement-types/activity-log/'+this.unitOfMeasurement.id]);
    this.cancel();
  }
  async update() {
    this.form = new UnitOfMeasurementTypeForm();
    this.form.initializeForm();
    console.log(this.form)
    this.form.fill(this.unitOfMeasurement);
    const result = await this.modalService.open(UnitOfMeasurementTypeFormComponent, {form: this.form, isUpdate: true});
    if (result) {
      this.activeModal.close(true);
    }
  }
  async delete() {
    const dialog = DeleteConfirmationDialog("Unit of Measurement Type");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.unitOfMeasurementTypeService.deleteUnitOfMeasurementType(this.unitOfMeasurement.id).subscribe({
        next:() => {
          this.toastService.add("Success", DeleteSuccess("Unit of Measurement Type"), ToastType.SUCCESS);
          this.activeModal.close(true);
      } })
    }
  }
  async disable() {
    const dialog = DisableConfirmationDialog("Unit of Measurement Type");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.unitOfMeasurementTypeService.disable(this.unitOfMeasurement.id).subscribe({
        next:() => {
          this.toastService.add("Success", DisableSuccess("Unit of Measurement Type"), ToastType.SUCCESS);
          this.activeModal.close(true);
      } })
    }
  }
  async enable() {
    const dialog = EnableConfirmationDialog("Unit of Measurement Type");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.unitOfMeasurementTypeService.enable(this.unitOfMeasurement.id).subscribe({
        next:() => {
          this.toastService.add("Success", EnableSuccess("Unit of Measurement Type"), ToastType.SUCCESS);
          this.activeModal.close(true);
      } })
    }
  }
}
