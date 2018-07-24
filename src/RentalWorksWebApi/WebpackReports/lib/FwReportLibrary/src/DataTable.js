export class DataTable {
    static toObjectList(dt) {
        let objects = [];
        for (let rowno = 0; rowno < dt.Rows.length; rowno++) {
            let row = dt.Rows[rowno];
            let object = {};
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
}
//# sourceMappingURL=DataTable.js.map