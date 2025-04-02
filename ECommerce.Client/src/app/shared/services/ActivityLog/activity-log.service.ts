import { Injectable } from '@angular/core';
import { GenericService } from '../generic/generic.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult } from '../../../models/result.model';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';
import { GenericActivityLogListingResult } from '../../../models/generics/generc-activity-log-listing-result';
import { GenericActivityLogResult } from '../../../models/generics/generic-activity-log-result';

@Injectable({
  providedIn: 'root'
})
export class ActivityLogService extends GenericService {
  constructor(private http2:HttpClient) {
    super(http2);
  }
  getLogs(controller, id, page = 1): Observable<ApiResult<GenericListingResult<GenericActivityLogListingResult[]>>> {
    return this.get<ApiResult<GenericListingResult<GenericActivityLogListingResult[]>>>(`${controller}/GetActivityLogs?page=${page}&Id=`+id, null, true);
  }
  getLog(controller, id): Observable<ApiResult<GenericActivityLogResult>> {
    return this.get<ApiResult<GenericActivityLogResult>>(`${controller}/GetActivityLog/${id}`, null, true);
  }
}
