class QuikActivityGrid {
    Module: string = 'QuikActivityGrid';
    apiurl: string = 'api/v1/quikactivity';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const orderTypeController = FwBrowse.getValueByDataField($control, $tr, 'OrderTypeController')
            $tr.find('[data-browsedisplayfield="OrderNumber"]').attr('data-validationname', `${orderTypeController}Validation`);
        });
    };

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ActivityStatusId':
                const activityType = FwBrowse.getValueByDataField($gridbrowse, $tr, 'ActivityTypeId');
                request.uniqueids = {
                    ActivityTypeId: activityType
                };
                break;
        }
    }
}

var QuikActivityGridController = new QuikActivityGrid();
//----------------------------------------------------------------------------------------------