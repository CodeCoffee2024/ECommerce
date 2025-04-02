import { BehaviorSubject } from "rxjs";
import { GenericSort } from "../generics/generic-sort";

export abstract class GenericComponentListing {
    listingOption;
    refreshBySort = new BehaviorSubject<{value, sortBy, sortDirection}>({value: false, sortBy: '', sortDirection: ''});
    sortEvent(event: GenericSort) {
        this.refreshBySort.next({value: true, sortBy: event.name, sortDirection: event.sortDirection});
    };
    turnOffSortEvent(){
        this.refreshBySort.next({value: false, sortBy: '', sortDirection: ''});
    }
    downloadExportedFile(result, exportType: string, fileName: string) {
        const fileType =
        exportType === 'pdf'
            ? 'application/pdf'
            : 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
        const fileExtension = exportType === 'pdf' ? 'pdf' : 'xlsx';

        const blobFile = new Blob([result], { type: fileType });
        const url = window.URL.createObjectURL(blobFile);
        const a = document.createElement('a');
        a.href = url;
        a.download = `${fileName}.${fileExtension}`;
        a.click();
        window.URL.revokeObjectURL(url);
    }
}