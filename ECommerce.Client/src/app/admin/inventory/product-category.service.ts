import { Injectable } from '@angular/core';
import { GenericService } from '../../shared/services/generic/generic.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, NullApiResult } from '../../models/result.model';
import { GenericListingResult } from '../../models/generics/generic-listing-result';
import { ProductCategoryListingResponse } from '../../models/inventory/product-category/product-category-listing-response';
import { ProductCategoryFragment, ProductCategoryResult } from '../../models/inventory/product-category/product-category';

@Injectable({
  providedIn: 'root'
})
export class ProductCategoryService  extends GenericService {
  private controller = 'ProductCategory';
  private exportController = 'Export';
  constructor(private http2:HttpClient) {
    super(http2);
  }
  getList(listingOption): Observable<ApiResult<GenericListingResult<ProductCategoryListingResponse[]>>> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.get<ApiResult<GenericListingResult<ProductCategoryListingResponse[]>>>(`${this.controller}/GetProductCategories?`+queryParams, null, true);
  }
  getStatuses(): Observable<ApiResult<[]>> {
    return this.get<ApiResult<[]>>(`${this.controller}/GetStatuses?`, null, true);
  }
  getDropdown(listingOption): Observable<ApiResult<GenericListingResult<ProductCategoryFragment[]>>> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.get<ApiResult<GenericListingResult<ProductCategoryFragment[]>>>(`${this.controller}/Dropdown?`+queryParams, null, true);
  }
  show(id): Observable<ApiResult<ProductCategoryResult>> {
    return this.get<ApiResult<ProductCategoryResult>>(`${this.controller}/`+id, null, true);
  }
  create(payload): Observable<ApiResult<ProductCategoryResult>> {
    return this.post(this.controller, payload);
  }
  update(payload, id): Observable<ApiResult<ProductCategoryResult>> {
    return this.put(this.controller+"/"+id, payload);
  }
  export(listingOption, exportType = 'excel'): Observable<Blob> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.getFile(`${this.exportController}/product-category/${exportType}?` + queryParams, undefined, true);
  }
  enable(id: string):Observable<NullApiResult> {
    return this.get(this.controller+"/enable/"+id);
  }
  disable(id: string):Observable<NullApiResult> {
    return this.get(this.controller+"/disable/"+id);
  }
  deleteProductCategory(id: string):Observable<NullApiResult> {
    return this.delete(this.controller+"/"+id);
  }
}
