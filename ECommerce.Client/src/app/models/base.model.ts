import { FormGroup, ValidationErrors } from "@angular/forms";

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
}