import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ToastDTO, ToastType } from '../../../models/toast';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toast: ToastDTO = null;
  CONFIG_TOAST_DURATION_SECONDS = 3;
  private toastSubject = new BehaviorSubject<ToastDTO>(this.toast);
  toast$ = this.toastSubject.asObservable();

  add(title: string, message: string, type: ToastType) {
    const toast = new ToastDTO();
    toast.title = title;
    toast.message = message;
    toast.type = type;
    toast.duration = this.CONFIG_TOAST_DURATION_SECONDS * 1000;
    toast.timeLeft = toast.duration;
    this.toastSubject.next(toast);
  }
}
