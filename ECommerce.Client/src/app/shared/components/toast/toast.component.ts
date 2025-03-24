import { Component } from '@angular/core';
import { ToastDTO } from '../../../models/toast';
import { ToastService } from '../../services/toast/toast.service';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss'
})
export class ToastComponent {
  toasts: ToastDTO[] = [];
  constructor(private toastService: ToastService) {
    this.toastService.toast$.subscribe(toast => {
      if (toast) {
        this.toasts.push(toast);

        const interval = setInterval(() => {
          toast.timeLeft -= 10;
          if (toast.timeLeft <= 0) {
            clearInterval(interval);
            this.remove(this.toasts.indexOf(toast));
          }
        }, 10);
      }
    });
  }

  remove(index: number) {
    this.toasts.splice(index, 1);
  }
}
