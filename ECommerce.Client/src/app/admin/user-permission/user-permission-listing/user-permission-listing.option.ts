import { GenericListingOption } from "../../../models/generics/generic-listing-option";

export class UserPermissionListingOption extends GenericListingOption {
    constructor() {
        super();
        this.sortBy = 'CreatedDate';
        this.sortDirection = 'asc';
    }
}