import { Injectable } from '@angular/core';
import { GenericService } from '../../../shared/services/generic/generic.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, NullApiResult } from '../../../models/result.model';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';
import { UnitOfMeasurementTypeFragment, UnitOfMeasurementTypeResult } from '../../../models/settings/unit-of-measurement-type/unit-of-measurement-type';
import { UnitOfMeasurementTypeListingResponse } from '../../../models/settings/unit-of-measurement-type/unit-of-measurement-type-listing-response';

@Injectable({
  providedIn: 'root'
})
export class UnitOfMeasurementTypeService extends GenericService {
  private controller = 'UnitOfMeasurementType';
  private exportController = 'Export';
  constructor(private http2:HttpClient) {
    super(http2);
  }
  getList(listingOption): Observable<ApiResult<GenericListingResult<UnitOfMeasurementTypeListingResponse[]>>> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.get<ApiResult<GenericListingResult<UnitOfMeasurementTypeListingResponse[]>>>(`${this.controller}/GetUnitOfMeasurementTypes?`+queryParams, null, true);
  }
  getStatuses(): Observable<ApiResult<[]>> {
    return this.get<ApiResult<[]>>(`${this.controller}/GetStatuses?`, null, true);
  }
  getDropdown(listingOption): Observable<ApiResult<GenericListingResult<UnitOfMeasurementTypeFragment[]>>> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.get<ApiResult<GenericListingResult<UnitOfMeasurementTypeFragment[]>>>(`${this.controller}/Dropdown?`+queryParams, null, true);
  }
  show(id): Observable<ApiResult<UnitOfMeasurementTypeResult>> {
    return this.get<ApiResult<UnitOfMeasurementTypeResult>>(`${this.controller}/`+id, null, true);
  }
  create(payload): Observable<ApiResult<UnitOfMeasurementTypeResult>> {
    return this.post(this.controller, payload);
  }
  update(payload, id): Observable<ApiResult<UnitOfMeasurementTypeResult>> {
    return this.put(this.controller+"/"+id, payload);
  }
  export(listingOption, exportType = 'excel'): Observable<Blob> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.getFile(`${this.exportController}/unit-of-measurement-type/${exportType}?` + queryParams, undefined, true);
  }
  enable(id: string):Observable<NullApiResult> {
    return this.get(this.controller+"/enable/"+id);
  }
  disable(id: string):Observable<NullApiResult> {
    return this.get(this.controller+"/disable/"+id);
  }
  deleteUnitOfMeasurementType(id: string):Observable<NullApiResult> {
    return this.delete(this.controller+"/"+id);
  }
}
