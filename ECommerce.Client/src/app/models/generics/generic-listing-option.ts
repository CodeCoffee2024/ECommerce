import { UserFragment } from "../user/user";

export abstract class GenericListingOption {
    search: string;
    createdBy: UserFragment;
    modifiedBy: UserFragment;
    page = 1;
    sortBy: string;
    sortDirection: string;
}