export class DataTable {
    ColumnIndex: any;
    Columns: Array<DataTableColumn>;
    Rows: Array<Array<any>>;
    PageNo: number
    PageSize: number;
    TotalPages: number;
    TotalRows: number;
    ColumnNameByIndex: any;

    static toObjectList<T>(dt: DataTable): Array<T> {
        let objects = [];
        for (let rowno = 0; rowno < dt.Rows.length; rowno++) {
            let row = dt.Rows[rowno];
            let object: any = {};
            for (let colno = 0; colno < dt.Columns.length; colno++) {
                let column = dt.Columns[colno];
                object[dt.Columns[colno].DataField] = row[colno];
            }
            objects.push(object);
        }
        return objects;
    }
}

export class DataTableColumn {
    "Name": string;
    "DataField": string;
    "DataType": string;
    "IsUniqueId": boolean;
    "IsVisible": boolean;
}

export class BrowseRequest {
    miscfields: any = {};
    module: string = '';
    options: any = {};
    orderby: string = '';
    orderbydirection: string = '';
    top: number = 0;
    pageno: number = 0;
    pagesize: number = 0;
    searchfieldoperators: Array<string> = [];
    searchfields: Array<string> = [];
    searchfieldvalues: Array<string> = [];
    searchfieldtypes: Array<string> = [];
    uniqueids: any = {};
    boundids: any = {};
    filterfields: any = {};
    activeview: string = '';
}

