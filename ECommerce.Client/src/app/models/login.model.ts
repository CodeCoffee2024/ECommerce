import { FormBuilder, Validators } from "@angular/forms";
import { BaseModel } from "./base.model";

export class LoginForm extends BaseModel {
    constructor(private fb: FormBuilder = new FormBuilder()) {
        super();
        const form = fb.group({
            usernameEmail: ['', Validators.required],
            password: ['', Validators.required],
            rememberMe: [false]
        });
        this.setForm(form);
    }
    get form() {
        return this.formGroup;
    }
    fill() {
        
    }

}