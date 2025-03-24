import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { BaseModel } from "../../../models/base.model";
import { ModuleDTO } from "../../../models/module/module";
import { UserPermissionResult } from "../../../models/user-permission/user-permission";

export class UserPermissionForm extends BaseModel {
    constructor(private fb: FormBuilder = new FormBuilder()) {
        super();
        this.fb = new FormBuilder();
        const form = this.fb.group({
            name: ['', Validators.required],
            modules: this.fb.array([]) // Array of modules
        });
        this.setForm(form);
    }
    initializeForm(data: ModuleDTO[]) {
        const moduleArray = this.form.get('modules') as FormArray;
        moduleArray.clear(); // Clear previous data
    
        data.forEach(module => {
            moduleArray.push(
                this.fb.group({
                    name: new FormControl(module.name),
                    description: new FormControl(module.description),
                    userPermissions: this.fb.array(
                        module.permissions.map(permission =>
                        this.fb.group({
                            name: new FormControl(permission.name),
                            description: new FormControl(permission.description),
                            permission: new FormControl(permission.name),
                            dependencies: new FormControl(permission.dependencies),
                            selected: new FormControl(false) // Checkbox for selection
                        })
                        )
                    )
                })
            );
        });
    }
    toggleDependency(moduleIndex: number, permIndex: number) {
        const moduleControl = this.form.get('modules') as FormArray;
        const userPermissions = moduleControl.at(moduleIndex).get('userPermissions') as FormArray;
        const permissionControl = userPermissions.at(permIndex) as FormGroup;
    
        const isChecked = permissionControl.get('selected')?.value;
        const permissionName = permissionControl.get('permission')?.value;
    
        if (!isChecked) {
            console.log(permissionName);
            console.log(permissionControl);
            // If unchecked, recursively uncheck dependent permissions
            this.uncheckDependentPermissions(moduleIndex, permissionName);
        } else {
            // If checked, make sure all dependencies are also checked
            this.checkDependencies(moduleIndex, permissionControl.get('dependencies')?.value || '');
        }
    }
    
    checkDependencies(moduleIndex: number, dependencies: string) {
        if (!dependencies) return;
    
        const moduleControl = this.form.get('modules') as FormArray;
        const userPermissions = moduleControl.at(moduleIndex).get('userPermissions') as FormArray;
    
        dependencies.split(',').map(dep => dep.trim()).forEach(dependency => {
            userPermissions.controls.forEach((perm) => {
                if (perm.get('permission')?.value === dependency) {
                    perm.get('selected')?.setValue(true); // Check dependencies
                }
            });
        });
    }
    
    uncheckDependentPermissions(moduleIndex: number, removedPermission: string) {
        const moduleControl = this.form.get('modules') as FormArray;
        const userPermissions = moduleControl.at(moduleIndex).get('userPermissions') as FormArray;
        userPermissions.controls.forEach((perm) => {
            const dependencies: string = perm.get('dependencies')?.value || '';
            const dependencyList = dependencies.split(',').map(dep => dep.trim());
    
            if (dependencyList.includes(removedPermission)) {
                perm.get('selected')?.setValue(false);
                this.uncheckDependentPermissions(moduleIndex, perm.get('permissions')?.value); // Recursive call
            }
        });
    }
    get form() {
        return this.formGroup;
    }
    
    get modules(): FormArray {
        return this.form.get('modules') as FormArray;
    }

    getUserPermissions(moduleIndex: number): FormArray {
        return (this.modules.at(moduleIndex).get('userPermissions') as FormArray);
    }
    
    get submitData() {
        return {
            name: this.form.get('name')?.value,
            permissions: this.form.value.modules
                .flatMap((module) =>
                    module.userPermissions
                        .filter((perm) => perm.selected) // Get only toggled permissions
                        .map((perm) => perm.name) // Only get the permission names
                ).join(",")
        };
    }
    fill(userPermission: UserPermissionResult) {
        const moduleArray = this.form.get('modules') as FormArray;
        this.form.patchValue({
            name: userPermission.name
        })
        userPermission.permissions.split(",").forEach((module, moduleIndex) => {
            const moduleGroup = moduleArray.at(moduleIndex) as FormGroup;
            if (!moduleGroup) return;    
            const permissionArray = moduleGroup.get('userPermissions') as FormArray;
            permissionArray.controls.forEach((permission) => {
                permission.patchValue({
                    selected: userPermission.permissions.split(",").includes(permission.get("permission").value)
                });
            })
        });
    }
}