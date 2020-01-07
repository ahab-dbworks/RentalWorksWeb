class OrderTypeCoverLetterGrid {
    Module: string = 'OrderTypeCoverLetter';
    apiurl: string = 'api/v1/ordertypelocation';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'CoverLetterId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecoverletter`);
                break;
        }
    }
}

var OrderTypeCoverLetterGridController = new OrderTypeCoverLetterGrid();
//----------------------------------------------------------------------------------------------