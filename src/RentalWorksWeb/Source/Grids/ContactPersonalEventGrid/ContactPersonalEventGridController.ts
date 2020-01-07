class ContactPersonalEventGrid {
    Module: string = 'ContactPersonalEventGrid';
    apiurl: string = 'api/v1/personalevent';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ContactEventId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontactevent`);
                break;
        }
    }
}

var ContactPersonalEventGridController = new ContactPersonalEventGrid();
//----------------------------------------------------------------------------------------------