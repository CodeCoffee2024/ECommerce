import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserPermissionForm } from './user-permission.form';
import { InputTypes } from '../../../shared/constants/input-type';

@Component({
  selector: 'app-user-permission-form',
  templateUrl: './user-permission-form.component.html',
  styleUrl: './user-permission-form.component.scss'
})
export class UserPermissionFormComponent {
  @Input() form: UserPermissionForm;
  InputTypes = InputTypes;
  @Output() formData = new EventEmitter<{name, permissions}>();
  submit() {
    this.formData.emit(this.form.submitData);
  }
}
