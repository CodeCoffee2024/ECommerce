
export enum UnitOfMeasurementTypePermission {
    UserEnableToModifyUnitOfMeasurementType = "UserEnableToModifyUnitOfMeasurementType",
    UserEnableToViewUnitOfMeasurementType = "UserEnableToViewUnitOfMeasurementType",
    UserEnableToDeleteUnitOfMeasurementType = "UserEnableToDeleteUnitOfMeasurementType",
}
export class UnitOfMeasurementTypeDTO {
    name: string;
    status: UnitOfMeasurementTypeStatuses;
}
export class UnitOfMeasurementTypeFragment {
    name: string;
    id: string;
}
export class UnitOfMeasurementTypeResult {
    id: string;
    name: string;
    hasDecimal: string;
    canDisable: boolean;
    canUpdate: boolean;
    canDelete: boolean;
    canEnable: boolean;
    status: UnitOfMeasurementTypeStatuses;
}
export enum UnitOfMeasurementTypeStatuses {
    ACTIVE = 'activ',
    INACTIVE = 'inact'
}

export class FormatUnitOfMeasurementTypeStatus {
    static format(status: UnitOfMeasurementTypeStatuses = UnitOfMeasurementTypeStatuses.INACTIVE, component = false): string {
        if (component) {
            switch (status) {
                case UnitOfMeasurementTypeStatuses.ACTIVE:
                    return "<span class='rounded bg-success text-white p-1'>Active</span>";
                default:
                    return "<span class='rounded text-white bg-secondary p-1'>Disabled</span>";
            }
        }
        switch (status) {
            case UnitOfMeasurementTypeStatuses.ACTIVE:
                return "Active";
            default:
                return "Disabled";
        }
    }
}