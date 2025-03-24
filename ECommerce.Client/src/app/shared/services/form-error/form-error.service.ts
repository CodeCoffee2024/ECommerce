import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { toCamelCase } from '../../../utils/string.util';

@Injectable({
  providedIn: 'root'
})
export class FormErrorService {
  constructor() {}

  /**
   * Maps API errors to form controls dynamically
   * @param form FormGroup instance
   * @param errorResponse API error response
   */
  setServerErrors(formGroup: FormGroup, errors: Record<string, string>): void {
    const globalErrors: Record<string, string> = {};

    Object.entries(errors).forEach(([key, message]) => {
      const controlKey = toCamelCase(key);

      if (formGroup.controls[controlKey]) {
        formGroup.controls[controlKey].setErrors({ serverError: message });
      } else {
        globalErrors[controlKey] = message;
      }
    });

    // Only set global errors if there are any
    if (Object.keys(globalErrors).length > 0) {
      formGroup.setErrors({ serverError: globalErrors });
    }
  }

}