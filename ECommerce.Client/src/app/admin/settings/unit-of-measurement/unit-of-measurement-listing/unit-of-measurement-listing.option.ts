import { GenericListingOption } from "../../../../models/generics/generic-listing-option";
import { EntityStatuses } from "../../../../models/status";

export class UnitOfMeasurementListingOption extends GenericListingOption {
    exclude = '';
    status: EntityStatuses = EntityStatuses.NONE;
    unitOfMeasurementType = '';
    constructor() {
        super();
        this.sortBy = 'CreatedDate';
        this.sortDirection = 'asc';
    }
}