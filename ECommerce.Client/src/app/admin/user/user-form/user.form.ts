import { AbstractControl, FormBuilder, ValidationErrors, Validators } from "@angular/forms";
import { BaseModel } from "../../../models/base.model";
import { UserResult } from "../../../models/user/user";

export class UserForm extends BaseModel {
    constructor(private fb: FormBuilder = new FormBuilder()) {
        super();
    }
    initializeForm(isUpdate = false) {
        this.fb = new FormBuilder();
        const form = isUpdate ? 
        this.fb.group(
            {
                lastName: ['', [Validators.required, Validators.minLength(2)]],
                firstName: ['', [Validators.required, Validators.minLength(2)]],
                middleName: [''],
                img: [''],
                imgFile: [null],
                email: ['', [Validators.required, Validators.email]],
                userName: ['', [Validators.required, Validators.minLength(4)]],
                repeatPassword: ['', [Validators.required]], // Will be validated separately
                birthDate: ['', [Validators.required, Validators.pattern(/^\d{4}-\d{2}-\d{2}$/)]], // YYYY-MM-DD
                userPermissions: [''], // Will be validated separately
            },
            { validators: this.mustMatchValidator('password', 'repeatPassword') } // Apply custom validator
        )
        :this.fb.group(
            {
                lastName: ['', [Validators.required, Validators.minLength(2)]],
                firstName: ['', [Validators.required, Validators.minLength(2)]],
                middleName: [''],
                img: [''],
                email: ['', [Validators.required, Validators.email]],
                userName: ['', [Validators.required, Validators.minLength(4)]],
                password: ['', [Validators.required, Validators.minLength(6)]],
                repeatPassword: ['', [Validators.required]], // Will be validated separately
                birthDate: ['', [Validators.required, Validators.pattern(/^\d{4}-\d{2}-\d{2}$/)]], // YYYY-MM-DD
                userPermissions: [''], // Will be validated separately
            },
            { validators: this.mustMatchValidator('password', 'repeatPassword') } // Apply custom validator
        );

        this.setForm(form);
    }
    
    get form() {
        return this.formGroup;
    }
    
    get submitData() {
        return {
            lastName: this.form.get('lastName')?.value,
            firstName: this.form.get('firstName')?.value,
            middleName: this.form.get('middleName')?.value,
            birthDate: this.form.get('birthDate')?.value,
            userName: this.form.get('userName')?.value,
            img: this.form.get('imgFile')?.value,
            email: this.form.get('email')?.value,
            password: this.form.get('password')?.value,
            userPermissions: this.form.get('userPermissions')?.value,
        };
    }
    get updateProfileData() {
        const formData = new FormData();
        const selectedFile = this.form.get('imgFile')?.value;
        Object.keys(this.submitData).forEach(key => {
            if (key != 'img') {
                formData.append(key, this.form.value[key]);
            }
        });
        if (selectedFile) {
            formData.append('img', selectedFile);
        }
        return formData;
    }
    
    private mustMatchValidator(passwordKey: string, confirmPasswordKey: string) {
        return (formGroup: AbstractControl): ValidationErrors | null => {
            const password = formGroup.get(passwordKey);
            const confirmPassword = formGroup.get(confirmPasswordKey);

            if (!password || !confirmPassword) return null;
            if (confirmPassword.errors && !confirmPassword.errors[passwordKey]) return null;

            if (password.value !== confirmPassword.value) {
                confirmPassword.setErrors({ mustMatch: passwordKey });
            } else {
                confirmPassword.setErrors(null);
            }

            return null;
        };
    }
    fill(user: UserResult) {
        this.id = user.id;
        this.form.get('lastName').setValue(user.lastName);
        this.form.get('firstName').setValue(user.firstName);
        this.form.get('middleName').setValue(user.middleName);
        this.form.get('img').setValue(user.img ?? '/assets/profile-placeholder.png');
        const birthDate = user.birthDate ? user.birthDate.toString().split('T')[0] : '';
        this.form.get('birthDate')!.setValue(birthDate);
        this.form.get('userName').setValue(user.userName);
        this.form.get('email').setValue(user.email);
        this.form.get('userPermissions').setValue(JSON.stringify(user.permissions));
    }
}