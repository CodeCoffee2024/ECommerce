import { Component, OnDestroy } from '@angular/core';
import { LoginService } from '../../../login/login.service';
import { GenericComponentListing } from '../../../models/abstractions/generic-component-listing';
import { LoadingService } from '../../services/loading/loading.service';
import { TitleService } from '../../services/title/title.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrl: './base.component.scss'
})
export class BaseComponent extends GenericComponentListing implements OnDestroy {
  exportEntity = '';
  isLoading = false;
  service;
  results;
  listingData;
  protected destroy$ = new Subject<void>();
  constructor(
      private auth: LoginService,
      private titlePage: TitleService,
      private loading: LoadingService
    ) {
    super();
  }
  setGenerics (listingOption, exportEntity, service, listingData, results) {
    this.listingOption = listingOption;
    this.exportEntity = exportEntity;
    this.service = service;
    this.listingData = listingData;
    this.results = results;
  }
  hasAccess(permission) {
    return this.auth.hasAccess(permission);
  }
  export(format: 'pdf' | 'excel') {
    if (format =='excel' ) {
      format = null;
    }
    this.service.export(this.listingOption, format).subscribe({
      next: (result) => {
        this.downloadExportedFile(result, format, this.exportEntity);
      }
    })
  }
  searchChanged(searchValue) {
    this.listingOption.search = searchValue;
    this.refresh();
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  refresh() {
    this.loading.show();
    this.service.getList(this.listingOption).subscribe({
      next: (result) => {
        this.results = result.data.result;
        this.listingData = result.data;
        this.loading.hide();
      }
    })
  }
}

