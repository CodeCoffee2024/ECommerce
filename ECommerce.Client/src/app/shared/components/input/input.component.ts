import { Component, Input } from '@angular/core';
import { ControlValueAccessor, FormControl, FormGroup } from '@angular/forms';
import { InputTypes } from '../../constants/input-type';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss'
})
export class InputComponent implements ControlValueAccessor {
  @Input() inputType: InputTypes = InputTypes.Text;
  @Input() label = '';
  @Input() inputId = 'input';
  @Input() controlName!: string;
  @Input() erorField: string;
  @Input() formGroup!: FormGroup;

  isPasswordVisible = false;
  get formControl(): FormControl {
    return this.formGroup?.get(this.controlName) as FormControl;
  }

  togglePasswordVisibility(): void {
    this.isPasswordVisible = !this.isPasswordVisible;
  }
  get errors(): string[] {
    if (!this.formControl) return [];
    const controlErrors = this.formControl.errors || {};
  
    const errorMessages: string[] = [];
    Object.keys(controlErrors).forEach(errorKey => {
      if (errorKey === 'required') {
        errorMessages.push(`${this.label} is required`);
      } else if (errorKey === 'serverError') {
        const errorWords = controlErrors['serverError'].split(' ');
        const errorString = errorWords.slice(1).join(' '); // Skip the first word
        errorMessages.push(`${this.label} ${errorString}`);
      } else {
        errorMessages.push(`${this.label} ${errorKey.replace(/([A-Z])/g, ' $1').toLowerCase()}`);
      }
    });
  
    // console.log(errorMessages)
  
    return errorMessages;
  }
  get isRequired(): boolean {
    return !!this.formControl?.errors?.['required'];
  }

  /** ControlValueAccessor Implementation */
  writeValue(value): void {
    if (this.formControl) {
      this.formControl.setValue(value, { emitEvent: false });
    }
  }

  registerOnChange(fn): void {
    this.formControl?.valueChanges.subscribe(fn);
  }

  registerOnTouched(): void {}

  setDisabledState(isDisabled: boolean): void {
    if (isDisabled) {
      this.formControl.disable();
    } else {
      this.formControl.enable();
    }
  }
}