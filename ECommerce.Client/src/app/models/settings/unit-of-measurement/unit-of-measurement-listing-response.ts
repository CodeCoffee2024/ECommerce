import { AuditableResponse } from "../../abstractions/auditable-response";
import { UnitOfMeasurementTypeFragment } from "../unit-of-measurement-type/unit-of-measurement-type";
import { UnitOfMeasurementStatuses } from "./unit-of-measurement";

export class UnitOfMeasurementListingResponse extends AuditableResponse {
    id: string;
    name: string;
    unitOfMeasurementType: UnitOfMeasurementTypeFragment;
    status: UnitOfMeasurementStatuses;
}
export class UnitOfMeasurementListingFragmentResponse extends AuditableResponse {
    id: string;
    name: string;
}