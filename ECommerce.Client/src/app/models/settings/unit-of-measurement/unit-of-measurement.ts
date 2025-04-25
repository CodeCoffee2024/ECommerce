import { UnitOfMeasurementConversionFragment } from "../unit-of-measurement-conversion/unit-of-measurement-conversion";
import { UnitOfMeasurementTypeFragment } from "../unit-of-measurement-type/unit-of-measurement-type";

export enum UnitOfMeasurementPermission {
    UserEnableToModifyUnitOfMeasurement = "UserEnableToModifyUnitOfMeasurement",
    UserEnableToViewUnitOfMeasurement = "UserEnableToViewUnitOfMeasurement",
    UserEnableToDeleteUnitOfMeasurement = "UserEnableToDeleteUnitOfMeasurement",
}
export class UnitOfMeasurementDTO {
    name: string;
    status: UnitOfMeasurementStatuses;
}
export class UnitOfMeasurementResult {
    id: string;
    name: string;
    abbreviation: string;
    unitOfMeasurementType: UnitOfMeasurementTypeFragment;
    conversionsFrom: UnitOfMeasurementConversionFragment[];
    conversionsTo: UnitOfMeasurementConversionFragment[];
    canDisable: boolean;
    canUpdate: boolean;
    canDelete: boolean;
    canEnable: boolean;
    status: UnitOfMeasurementStatuses;
}
export enum UnitOfMeasurementStatuses {
    ACTIVE = 'activ',
    INACTIVE = 'inact'
}

export class UnitOfMeasurementFragment {
    abbreviation: string;
    name: string;
    id: string;
}
export class FormatUnitOfMeasurementStatus {
    static format(status: UnitOfMeasurementStatuses = UnitOfMeasurementStatuses.INACTIVE, component = false): string {
        if (component) {
            switch (status) {
                case UnitOfMeasurementStatuses.ACTIVE:
                    return "<span class='rounded bg-success text-white p-1'>Active</span>";
                default:
                    return "<span class='rounded text-white bg-secondary p-1'>Disabled</span>";
            }
        }
        switch (status) {
            case UnitOfMeasurementStatuses.ACTIVE:
                return "Active";
            default:
                return "Disabled";
        }
    }
}