export enum ToastType {
    SUCCESS,
    ERROR,
    WARNING
}

export class ToastDTO {
    title: string;
    message: string;
    type: ToastType;
    duration?: number;
    timeLeft = 0;
    get class() {
        switch (this.type) {
            case ToastType.SUCCESS:
                return 'bg-success text-white';
            case ToastType.ERROR:
                return 'bg-danger text-white';
            case ToastType.WARNING:
                return 'bg-warning text-white';
            default:
                return 'bg-warning';
        }
    }
}