import { Component } from '@angular/core';
import { ConfirmationDialogDTO } from '../../../models/confirmation-dialog';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrl: './confirmation-dialog.component.scss'
})
export class ConfirmationDialogComponent {
  dialogDTO: ConfirmationDialogDTO = new ConfirmationDialogDTO();

  constructor(public activeModal: NgbActiveModal) {}

  confirm() {
    this.activeModal.close(true);
  }

  cancel() {
    this.activeModal.dismiss(false);
  }
}
