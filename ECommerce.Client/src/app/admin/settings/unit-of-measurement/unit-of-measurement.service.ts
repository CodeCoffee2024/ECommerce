import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GenericListingResult } from "../../../models/generics/generic-listing-result";
import { ApiResult, NullApiResult } from "../../../models/result.model";
import { UnitOfMeasurementFragment, UnitOfMeasurementResult } from "../../../models/settings/unit-of-measurement/unit-of-measurement";
import { UnitOfMeasurementListingFragmentResponse, UnitOfMeasurementListingResponse } from "../../../models/settings/unit-of-measurement/unit-of-measurement-listing-response";
import { GenericService } from "../../../shared/services/generic/generic.service";

@Injectable({
    providedIn: 'root'
})
export class UnitOfMeasurementService extends GenericService {
    private controller = 'UnitOfMeasurement';
    private exportController = 'Export';
    constructor(private http2:HttpClient) {
        super(http2);
    }
    getList(listingOption): Observable<ApiResult<GenericListingResult<UnitOfMeasurementListingResponse[]>>> {
        const queryParams = this.setQueryParameters(listingOption);
        return this.get<ApiResult<GenericListingResult<UnitOfMeasurementListingResponse[]>>>(`${this.controller}/GetUnitOfMeasurements?`+queryParams, null, true);
    }
    getActiveList(listingOption): Observable<ApiResult<GenericListingResult<UnitOfMeasurementListingFragmentResponse[]>>> {
        const queryParams = this.setQueryParameters(listingOption);
        return this.get<ApiResult<GenericListingResult<UnitOfMeasurementListingFragmentResponse[]>>>(`${this.controller}/GetUnitOfMeasurements?`+queryParams, null, true);
    }
    getStatuses(): Observable<ApiResult<[]>> {
        return this.get<ApiResult<[]>>(`${this.controller}/GetStatuses?`, null, true);
    }
    show(id): Observable<ApiResult<UnitOfMeasurementResult>> {
        return this.get<ApiResult<UnitOfMeasurementResult>>(`${this.controller}/`+id, null, true);
    }
    getDropdown(listingOption): Observable<ApiResult<GenericListingResult<UnitOfMeasurementFragment[]>>> {
        const queryParams = this.setQueryParameters(listingOption);
        return this.get<ApiResult<GenericListingResult<UnitOfMeasurementFragment[]>>>(`${this.controller}/Dropdown?`+queryParams, null, true);
    }
    create(payload): Observable<ApiResult<UnitOfMeasurementResult>> {
        return this.post(this.controller, payload);
    }
    update(payload, id): Observable<ApiResult<UnitOfMeasurementResult>> {
        return this.put(this.controller+"/"+id, payload);
    }
    export(listingOption, exportType = 'excel'): Observable<Blob> {
        const queryParams = this.setQueryParameters(listingOption);
        return this.getFile(`${this.exportController}/unit-of-measurement/${exportType}?` + queryParams, undefined, true);
    }
    enable(id: string):Observable<NullApiResult> {
        return this.get(this.controller+"/enable/"+id);
    }
    disable(id: string):Observable<NullApiResult> {
        return this.get(this.controller+"/disable/"+id);
    }
    deleteUnitOfMeasurement(id: string):Observable<NullApiResult> {
        return this.delete(this.controller+"/"+id);
    }
}
