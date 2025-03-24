import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationDialogDTO } from '../../../models/confirmation-dialog';
import { ConfirmationDialogComponent } from '../../components/confirmation-dialog/confirmation-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  constructor(private modalService: NgbModal) {}

  open(component, data?) {
    const modalRef = this.modalService.open(component, { backdrop: 'static', keyboard: false });
    if (data) {
      Object.assign(modalRef.componentInstance, data);
    }
    return modalRef.result; // Returns a Promise
  }

  confirm(dialogDTO: ConfirmationDialogDTO): Promise<boolean> {
    return this.open(ConfirmationDialogComponent, { dialogDTO });
  }
}
