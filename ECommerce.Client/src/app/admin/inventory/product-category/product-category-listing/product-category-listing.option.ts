import { GenericListingOption } from "../../../../models/generics/generic-listing-option";
import { EntityStatuses } from "../../../../models/status";

export class ProductCategoryListingOption extends GenericListingOption {
    exclude: string;
    status: EntityStatuses = EntityStatuses.NONE;
    constructor() {
        super();
        this.sortBy = 'CreatedDate';
        this.sortDirection = 'asc';
    }
}