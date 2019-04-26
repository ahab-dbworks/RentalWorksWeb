interface FwJsonDataTable {
    ColumnIndex: { [columnName: string]: number };
    Columns: FwJsonDataTableColumn[];
    Rows: any[][];
    PageNo: number;
    PageSize: number;
    TotalPages: number;
    TotalRows: number;
    ColumnNameByIndex: { [columnnIndex: number]: string };
}

interface FwJsonDataTableColumn {
    Name: string;
    DataField: string;
    DataType: 'Text' | 'Date' | 'Time' | 'DateTime' | 'DateTimeOffset' | 'Decimal' | 'Boolean' | 'CurrencyString' | 'CurrencyStringNoDollarSign' | 'CurrencyStringNoDollarSignNoDecimalPlaces' | 'PhoneUS' | 'ZipcodeUS' | 'Percentage' | 'OleToHtmlColor' | 'Integer' | 'JpgDataUrl' | 'UTCDateTime';
    IsUniqueId: boolean;
    IsVisible: boolean;
}