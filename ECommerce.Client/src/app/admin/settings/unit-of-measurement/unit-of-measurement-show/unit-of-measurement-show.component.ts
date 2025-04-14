import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { DeleteConfirmationDialog, DisableConfirmationDialog, EnableConfirmationDialog } from '../../../../models/confirmation-dialog';
import { DeleteSuccess, DisableSuccess, EnableSuccess, NotFound } from '../../../../models/message';
import { UnitOfMeasurementResult } from '../../../../models/settings/unit-of-measurement/unit-of-measurement';
import { ToastType } from '../../../../models/toast';
import { LoadingService } from '../../../../shared/services/loading/loading.service';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { ToastService } from '../../../../shared/services/toast/toast.service';
import { UnitOfMeasurementForm } from '../unit-of-measurement-form/unit-of-measurement.form';
import { UnitOfMeasurementService } from '../unit-of-measurement.service';

@Component({
  selector: 'app-unit-of-measurement-show',
  templateUrl: './unit-of-measurement-show.component.html',
  styleUrl: './unit-of-measurement-show.component.scss'
})
export class UnitOfMeasurementShowComponent implements OnInit {
  unitOfMeasurement:UnitOfMeasurementResult = new UnitOfMeasurementResult();
  form: UnitOfMeasurementForm;
  Id: string;
  constructor(
    private unitOfMeasurementService: UnitOfMeasurementService,
    private router: Router,
    private loadingService: LoadingService,
    private toastService: ToastService,
    private activatedRoute: ActivatedRoute,
    private modalService: ModalService
  ) {

  }
  ngOnInit(): void {
    this.loadingService.show();
    this.Id = this.activatedRoute.snapshot.paramMap.get('id') || null;
    if (!this.Id) {
      this.toastService.add("Error", NotFound("Unit of Measurement"), ToastType.ERROR);
      this.router.navigate(['/admin/settings/unit-of-measurements']);
    } else {
      this.unitOfMeasurementService.show(this.Id)
      .pipe(finalize(() => this.loadingService.hide())) // Ensure loading hides after request
      .subscribe({
        next: (result) => {
          this.unitOfMeasurement = result.data;
        },
        error: () => {
          this.toastService.add("Error", NotFound("Unit of Measurement"), ToastType.ERROR);
          this.router.navigate(['/admin/settings/unit-of-measurements']);
        }
      });
    }
  }
  goToLog() {
    this.router.navigate(['/admin/settings/unit-of-measurements/activity-log/'+this.unitOfMeasurement.id]);
    // this.cancel();
  }
  async delete() {
    const dialog = DeleteConfirmationDialog("Unit of Measurement");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.unitOfMeasurementService.deleteUnitOfMeasurement(this.unitOfMeasurement.id).subscribe({
        next:() => {
          this.toastService.add("Success", DeleteSuccess("Unit of Measurement"), ToastType.SUCCESS);
          // this.activeModal.close(true);
      } })
    }
  }
  async disable() {
    const dialog = DisableConfirmationDialog("Unit of Measurement");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.unitOfMeasurementService.disable(this.unitOfMeasurement.id).subscribe({
        next:() => {
          this.toastService.add("Success", DisableSuccess("Unit of Measurement"), ToastType.SUCCESS);
          // this.activeModal.close(true);
      } })
    }
  }
  async enable() {
    const dialog = EnableConfirmationDialog("Unit of Measurement");
    const result = await this.modalService.confirm(dialog);
    if (result) {
      this.unitOfMeasurementService.enable(this.unitOfMeasurement.id).subscribe({
        next:() => {
          this.toastService.add("Success", EnableSuccess("Unit of Measurement "), ToastType.SUCCESS);
          // this.activeModal.close(true);
      } })
    }
  }
}