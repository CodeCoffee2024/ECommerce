import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { GenericActivityLogListingResult } from '../../../models/generics/generc-activity-log-listing-result';
import { GenericListingResult } from '../../../models/generics/generic-listing-result';
import { ActivityLogService } from '../../services/ActivityLog/activity-log.service';
import { LoadingService } from '../../services/loading/loading.service';

@Component({
  selector: 'app-activity-log-list',
  templateUrl: './activity-log-list.component.html',
  styleUrl: './activity-log-list.component.scss'
})
export class ActivityLogListComponent {
  @Input() listingData: GenericListingResult<GenericActivityLogListingResult[]>;
  @Input() id: string;
  @Input() entity: string;
  @Input() controller: string;
  @Input() route: string;
  constructor(private router: Router,
    private activityLogService: ActivityLogService,
    private loadingService: LoadingService
  ) {
    
  }
  goTo(page: number) {
    this.loadingService.show();
    this.activityLogService.getLogs(this.controller, this.id, page).subscribe({
      next:(result) => {
        this.listingData = result.data;
        this.loadingService.hide();
      }
    })
  }
  get lists() {
    return this.listingData.result;
  }
  go (id) {
    this.router.navigate(['/admin/'+this.route+'/activity-log/'+this.id+'/'+id]);
  }
}
