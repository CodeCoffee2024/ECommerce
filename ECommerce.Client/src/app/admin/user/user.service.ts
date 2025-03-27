import { Injectable } from '@angular/core';
import { GenericService } from '../../shared/services/generic/generic.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, NullApiResult } from '../../models/result.model';
import { GenericListingResult } from '../../models/generics/generic-listing-result';
import { UserListingReponse } from '../../models/user/user-listing-response';
import { UserResult } from '../../models/user/user';

@Injectable({
  providedIn: 'root'
})
export class UserService extends GenericService {
  private controller = 'User';
  private exportController = 'Export';
  constructor(private http2:HttpClient) {
    super(http2);
  }
  getList(listingOption): Observable<ApiResult<GenericListingResult<UserListingReponse[]>>> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.get<ApiResult<GenericListingResult<UserListingReponse[]>>>(`${this.controller}/GetUsers?`+queryParams, null, true);
  }
  show(id): Observable<ApiResult<UserResult>> {
    return this.get<ApiResult<UserResult>>(`${this.controller}/`+id, null, true);
  }
  create(payload): Observable<ApiResult<UserResult>> {
    return this.post(this.controller, payload);
  }
  update(payload, id): Observable<ApiResult<UserResult>> {
    return this.put(this.controller+"/"+id, payload);
  }
  export(listingOption, exportType = 'excel'): Observable<Blob> {
    const queryParams = this.setQueryParameters(listingOption);
    return this.getFile(`${this.exportController}/user/${exportType}?` + queryParams, undefined, true);
  }
  deleteUser(id: string):Observable<NullApiResult> {
    return this.delete(this.controller+"/"+id);
  }
}