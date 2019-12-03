class CustomReportLayoutGroupGrid {
    Module: string = 'CustomReportLayoutGroupGrid';
    apiurl: string = 'api/v1/customreportlayoutgroup';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'GroupId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validategroupname`);
                break;
        }
    }
}

var CustomReportLayoutGroupGridController = new CustomReportLayoutGroupGrid();
//----------------------------------------------------------------------------------------------