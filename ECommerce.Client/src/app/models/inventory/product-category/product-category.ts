
export enum ProductCategoryPermission {
    UserEnableToModifyProductCategory = "UserEnableToModifyProductCategory",
    UserEnableToViewProductCategory = "UserEnableToViewProductCategory",
    UserEnableToDeleteProductCategory = "UserEnableToDeleteProductCategory",
}
export class ProductCategoryDTO {
    name: string;
    status: ProductCategoryStatuses;
}
export class ProductCategoryFragment {
    name: string;
    id: string;
}
export class ProductCategoryResult {
    id: string;
    name: string;
    hasDecimal: string;
    canDisable: boolean;
    canUpdate: boolean;
    canDelete: boolean;
    canEnable: boolean;
    isSubCategory: boolean;
    parentProductCategoryId: boolean;
    parentProductCategory: ProductCategoryFragment;
    status: ProductCategoryStatuses;
}
export enum ProductCategoryStatuses {
    ACTIVE = 'activ',
    INACTIVE = 'inact'
}

export class FormatProductCategoryStatus {
    static format(status: ProductCategoryStatuses = ProductCategoryStatuses.INACTIVE, component = false): string {
        if (component) {
            switch (status) {
                case ProductCategoryStatuses.ACTIVE:
                    return "<span class='rounded bg-success text-white p-1'>Active</span>";
                default:
                    return "<span class='rounded text-white bg-secondary p-1'>Disabled</span>";
            }
        }
        switch (status) {
            case ProductCategoryStatuses.ACTIVE:
                return "Active";
            default:
                return "Disabled";
        }
    }
}