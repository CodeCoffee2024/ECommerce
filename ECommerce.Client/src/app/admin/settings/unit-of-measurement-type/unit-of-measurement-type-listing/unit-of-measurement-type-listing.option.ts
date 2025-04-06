import { GenericListingOption } from "../../../../models/generics/generic-listing-option";
import { EntityStatuses } from "../../../../models/status";

export class UnitOfMeasurementTypeListingOption extends GenericListingOption {
    exclude: string;
    status: EntityStatuses = EntityStatuses.NONE;
    hasDecimal = '';
    constructor() {
        super();
        this.sortBy = 'CreatedDate';
        this.sortDirection = 'asc';
    }
}