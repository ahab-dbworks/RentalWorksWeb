class ActivityGrid {
    Module: string = 'ActivityGrid';
    apiurl: string = 'api/v1/activity';


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
//----------------------------------------------------------------------------------------------
var ActivityGridController = new ActivityGrid();