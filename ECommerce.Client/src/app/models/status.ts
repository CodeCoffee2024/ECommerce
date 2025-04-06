export enum EntityStatuses {
    ACTIVE = 'activ',
    INACTIVE = 'disab',
    NONE = ''
}
export class FormatStatus {
    static format(status: EntityStatuses = EntityStatuses.INACTIVE, component = false): string {
        if (component) {
            switch (status) {
                case EntityStatuses.ACTIVE:
                    return "<span class='rounded text-white bg-success p-1'>Active</span>";
                default:
                    return "<span class='rounded text-white bg-secondary p-1'>Disabled</span>";
            }
        }
        switch (status) {
            case EntityStatuses.ACTIVE:
                return "Active";
            default:
                return "Disabled";
        }
    }
}