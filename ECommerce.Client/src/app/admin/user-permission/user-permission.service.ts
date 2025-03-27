import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GenericListingResult } from '../../models/generics/generic-listing-result';
import { ApiResult, NullApiResult } from '../../models/result.model';
import { UserPermissionListingReponse } from '../../models/user-permission/user-permission-listing-response';
import { GenericService } from '../../shared/services/generic/generic.service';
import { ModuleDTO } from '../../models/module/module';
import { UserPermissionResult } from '../../models/user-permission/user-permission';

@Injectable({
  providedIn: 'root'
})
export class UserPermissionService extends GenericService {
  private controller = 'UserPermission';
  private exportController = 'Export';
  constructor(private http2:HttpClient) {
    super(http2);
  }
  getList(listingOption): Observable<ApiResult<GenericListingResult<UserPermissionListingReponse[]>>> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.get<ApiResult<GenericListingResult<UserPermissionListingReponse[]>>>(`${this.controller}/GetUserPermissions?`+queryParams, null, true);
  }
  dropdown(listingOption): Observable<ApiResult<GenericListingResult<UserPermissionListingReponse[]>>> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.get<ApiResult<GenericListingResult<UserPermissionListingReponse[]>>>(`${this.controller}/Dropdown?`+queryParams, null, true);
  }
  getPermissions(): Observable<ApiResult<ModuleDTO[]>> {
    return this.get<ApiResult<ModuleDTO[]>>(`${this.controller}/GetAllUserPermissions?`, null, true);
  }
  show(id): Observable<ApiResult<UserPermissionResult>> {
    return this.get<ApiResult<UserPermissionResult>>(`${this.controller}/`+id, null, true);
  }
  create(payload): Observable<ApiResult<UserPermissionResult>> {
    return this.post(this.controller, payload);
  }
  update(payload, id): Observable<ApiResult<UserPermissionResult>> {
    return this.put(this.controller+"/"+id, payload);
  }
  export(listingOption, exportType = 'excel'): Observable<Blob> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.getFile(`${this.exportController}/user-permission/${exportType}?` + queryParams, undefined, true);
  }
  deletePermission(id: string):Observable<NullApiResult> {
    return this.delete(this.controller+"/"+id);
  }
}
