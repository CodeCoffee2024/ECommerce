export enum ConfirmationDialogType {
    SUCCESS,
    ERROR,
    WARNING
}

export class ConfirmationDialogDTO {
    title: string;
    message: string;
    type: ConfirmationDialogType;
    constructor(title = '', message= '', type= ConfirmationDialogType.SUCCESS) {
        this.title = title;
        this.message = message;
        this.type = type;
    }
    get class() {
        switch (this.type) {
            case ConfirmationDialogType.SUCCESS:
                return 'btn btn-success';
            case ConfirmationDialogType.ERROR:
                return 'btn btn-danger';
            case ConfirmationDialogType.WARNING:
                return 'btn btn-warning';
            default:
                return 'bg-warning';
        }
    }
}

export function DeleteConfirmationDialog(entityName: string): ConfirmationDialogDTO {
    return new ConfirmationDialogDTO(
        "Delete Confirmation",
        `Are you sure you want to delete this ${entityName}?`,
        ConfirmationDialogType.WARNING
    );
}