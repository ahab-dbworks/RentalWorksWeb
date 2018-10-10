class AuditHistoryGrid {
    Module: string = 'AuditHistoryGrid';
    apiurl: string = 'api/v1/webauditjson';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let $oldElement = $tr.find('[data-browsedatafield="Json"]');
            let changes = JSON.parse($oldElement.attr('data-originalvalue'));
            let html: any = [];

            for (let change in changes) {
                html.push(`<ul style="font-size:14px;">
                                <span style="font-weight:bold; float:left; width:250px;">${change}:</span>
                                <span style="white-space:pre;">${changes[change] === "" ? "&#160;" : changes[change]}</span>
                          </ul>`);
            }
            jQuery($oldElement).replaceWith(html.join(''));

        });
    }
}

var AuditHistoryGridController = new AuditHistoryGrid();
//----------------------------------------------------------------------------------------------