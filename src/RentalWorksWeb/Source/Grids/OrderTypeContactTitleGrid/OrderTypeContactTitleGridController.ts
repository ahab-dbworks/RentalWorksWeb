class OrderTypeContactTitleGrid {
    Module: string = 'OrderTypeContactTitleGrid';
    apiurl: string = 'api/v1/ordertypecontacttitle';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ContactTitleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontacttitle`);
                break;
        }
    }
}

var OrderTypeContactTitleGridController = new OrderTypeContactTitleGrid();
//----------------------------------------------------------------------------------------------