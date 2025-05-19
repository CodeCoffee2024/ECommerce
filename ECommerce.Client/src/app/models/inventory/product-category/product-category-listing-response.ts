import { AuditableResponse } from "../../abstractions/auditable-response";
import { ProductCategoryFragment, ProductCategoryStatuses } from "./product-category";

export class ProductCategoryListingResponse extends AuditableResponse {
    id: string;
    name: string;
    parentProductCategory: ProductCategoryFragment;
    status: ProductCategoryStatuses;
}