class AuditHistoryGrid {
    Module: string = 'AuditHistoryGrid';
    apiurl: string = 'api/v1/webauditjson';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let $oldElement = $tr.find('[data-browsedatafield="Json"]');
            let changes = JSON.parse($oldElement.attr('data-originalvalue'));
            let html: any = [];

            html.push(`<ul style="font-size:14px; width:100%; display:flex;">
                                <span style="font-weight:bold; float:left; width:200px; text-decoration:underline; padding-right:1em;">Field Name</span>
                                <span style="font-weight:bold; float:left; width:200px; text-decoration:underline; padding-right:1em;">Old Value</span>
                                <span style="font-weight:bold; float:left; width:200px; text-decoration:underline;">New Value</span>
                          </ul>`);

            for (let i = 0; i < changes.length; i++) {
                html.push(`<ul style="font-size:14px; width:100%; display:flex;">
                                <span style="font-weight:bold; float:left; width:200px; padding-right:1em;">${changes[i].FieldName}:</span>
                                <span style="width:200px; word-wrap:break-word; padding-right:1em;">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}</span>
                                <span style="width:200px; word-wrap:break-word">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}</span>
                          </ul>`);
            }
            jQuery($oldElement).replaceWith(html.join(''));

        });
    }
}

var AuditHistoryGridController = new AuditHistoryGrid();
//----------------------------------------------------------------------------------------------