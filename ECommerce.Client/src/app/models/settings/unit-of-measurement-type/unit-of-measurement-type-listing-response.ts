import { AuditableResponse } from "../../abstractions/auditable-response";
import { UnitOfMeasurementTypeStatuses } from "./unit-of-measurement-type";

export class UnitOfMeasurementTypeListingReponse extends AuditableResponse {
    id: string;
    name: string;
    hasDecimal: string;
    status: UnitOfMeasurementTypeStatuses;
}