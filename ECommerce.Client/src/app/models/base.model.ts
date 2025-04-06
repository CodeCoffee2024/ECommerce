import { AbstractControl, FormGroup, ValidationErrors } from "@angular/forms";

export class BaseModel {
    id: string;
    protected formGroup: FormGroup;
    protected setForm(form: FormGroup) {
        this.formGroup = form;
    }
    getServerError(controlName: string): string | null {
        const errors = this.formGroup.errors?.['serverError'];
        return errors && errors[controlName] ? errors[controlName] : null;
    }
    errorKeys(errors: ValidationErrors): string[] {
        return Object.keys(errors).map(key => errors[key]);
    }
    requiredNoWhitespace(control: AbstractControl): ValidationErrors | null {
        const value = control.value;
        if (typeof value === 'string' && value.trim().length === 0) {
            return { required: true }; // <-- trigger the standard 'required' error
        }
        return null;
    }
}