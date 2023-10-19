import { FilterMetadata, SortMeta } from "primeng/api";

export class PagedInputDTO {
    first?: number;
    last?: number;
    rows?: number | null;
    sortField?: string;
    sortOrder?: number;
    multiSortMeta?: SortMeta[];
    globalFilter?: any;
    filters?: {
        [s: string]: FilterMetadata;
    };
    fieldsFilters: EntityFilters[] = [];
    forceUpdate?: () => void;

}
export class EntityFilters {
    fieldName!: string | undefined;;
    fields!: FieldsFilter | undefined;;
}


export class FieldsFilter {
    value!: string | undefined;
    matchMode!: string | undefined;;
}