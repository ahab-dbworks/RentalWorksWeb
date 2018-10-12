class AuditHistoryGrid {
    Module: string = 'AuditHistoryGrid';
    apiurl: string = 'api/v1/webauditjson';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let $oldElement = $tr.find('[data-browsedatafield="Json"]');
            let changes = JSON.parse($oldElement.attr('data-originalvalue'));
            let html: any = [];

            html.push(`<ul style="font-size:14px; width:100%; display:flex;">
                                <span style="font-weight:bold; float:left; flex:0 0 250px; text-decoration:underline; padding-right:1em;">Field Name</span>
                                <span style="font-weight:bold; float:left; flex:1 0 250px; text-decoration:underline; padding-right:1em; width:250px">New Value</span>
                                <span style="font-weight:bold; float:left; flex:1 0 250px; text-decoration:underline; width:250px; word-wrap:break-word">Old Value</span>
                          </ul>`);

            for (let i = 0; i < changes.length; i++) {
                html.push(`<ul style="font-size:14px; width:100%; display:flex;">
                                <span style="font-weight:bold; float:left; flex:0 0 250px; padding-right:1em;">${changes[i].FieldName}:</span>
                                <span style="flex:1 0 250px; padding-right:1em; width:250px; word-wrap:break-word">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}</span>
                                <span style="flex:1 0 250px; width:250px; word-wrap:break-word">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}</span>
                          </ul>`);
            }
            jQuery($oldElement).replaceWith(html.join(''));

        });
    }
}

var AuditHistoryGridController = new AuditHistoryGrid();
//----------------------------------------------------------------------------------------------