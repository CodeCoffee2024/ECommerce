import { FormGroup } from "@angular/forms";

export class BaseModel {
    protected formGroup: FormGroup;
    protected setForm(form: FormGroup) {
        this.formGroup = form;
    }
    getServerError(controlName: string): string | null {
        const errors = this.formGroup.errors?.['serverError'];
        return errors && errors[controlName] ? errors[controlName] : null;
    }
}