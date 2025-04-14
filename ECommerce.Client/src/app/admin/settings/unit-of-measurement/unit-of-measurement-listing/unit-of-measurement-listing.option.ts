import { GenericListingOption } from "../../../../models/generics/generic-listing-option";
import { EntityStatuses } from "../../../../models/status";

export class UnitOfMeasurementListingOption extends GenericListingOption {
    exclude: string;
    status: EntityStatuses = EntityStatuses.NONE;
    unitOfMeasurementTypes = '';
    constructor() {
        super();
        this.sortBy = 'CreatedDate';
        this.sortDirection = 'asc';
    }
}