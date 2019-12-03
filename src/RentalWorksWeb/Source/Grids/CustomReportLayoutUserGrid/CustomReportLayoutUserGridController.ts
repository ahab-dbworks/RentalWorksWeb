class CustomReportLayoutUserGrid {
    Module: string = 'CustomReportLayoutUserGrid';
    apiurl: string = 'api/v1/customreportlayoutuser';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'UserId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateuser`);
                break;
        }
    }
}

var CustomReportLayoutUserGridController = new CustomReportLayoutUserGrid();
//----------------------------------------------------------------------------------------------