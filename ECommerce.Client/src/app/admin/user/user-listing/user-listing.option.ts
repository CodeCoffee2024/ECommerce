import { GenericListingOption } from "../../../models/generics/generic-listing-option";

export class UserListingOption extends GenericListingOption {
    userPermissions: string;
    constructor() {
        super();
        this.sortBy = 'CreatedDate';
        this.sortDirection = 'asc';
    }
}