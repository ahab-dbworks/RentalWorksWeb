class CompanyTaxOptionGrid {
    Module: string = 'CompanyTaxOptionGrid';
    apiurl: string = 'api/v1/companytaxoption';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'TaxOptionId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetaxoption`);
                break;
        }
    }
}

var CompanyTaxOptionGridController = new CompanyTaxOptionGrid();
//----------------------------------------------------------------------------------------------