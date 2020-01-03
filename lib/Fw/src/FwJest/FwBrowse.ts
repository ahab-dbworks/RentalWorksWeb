export class FwBrowseRequest {
    miscfields: any = {};
    module: string = '';
    options: any = {};
    orderby: string = '';
    orderbydirection: string = '';
    top: number = 15;
    pageno: number = 0;
    pagesize: number = 0;
    searchfieldoperators: Array<string> = [];
    searchfields: Array<string> = [];
    searchfieldvalues: Array<string> = [];
    searchfieldtypes: Array<string> = [];
    searchseparators: Array<string> = [];
    searchcondition: Array<string> = [];
    searchconjunctions: Array<string> = [];
    uniqueids: any = {};
    boundids: any = {};
    filterfields: any = {};
    activeview: string = '';
}

export class FwJsonDataTable {
    ColumnIndex: any;
    Columns: Array<FwJsonDataTableColumn>;
    Rows: Array<Array<any>>;
    PageNo:	number
    PageSize:	number;
    TotalPages:	number;
    TotalRows:	number;
    ColumnNameByIndex: any;

    static toObjectList<T>(dt: FwJsonDataTable): Array<T> {
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

export class FwJsonDataTableColumn {
    "Name": string;
    "DataField": string;
    "DataType": string;
    "IsUniqueId": boolean;
    "IsVisible": boolean;
}