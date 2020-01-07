class OrderTypeTermsAndConditionsGrid {
    Module: string = 'OrderTypeTermsAndConditions';
    apiurl: string = 'api/v1/ordertypelocation';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'TermsConditionsId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetermsconditions`);
                break;
        }
    }
}

var OrderTypeTermsAndConditionsGridController = new OrderTypeTermsAndConditionsGrid();
//----------------------------------------------------------------------------------------------