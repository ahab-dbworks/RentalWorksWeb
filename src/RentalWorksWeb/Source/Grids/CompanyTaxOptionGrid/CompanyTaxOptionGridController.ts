class CompanyTaxOptionGrid {
    Module: string = 'CompanyTaxOptionGrid';
    apiurl: string = 'api/v1/companytaxoption';

    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');

        $generatedtr.find('div[data-browsedatafield="TaxOptionId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="RentalTaxRate1"]').text($tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="SalesTaxRate1"]').text($tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="LaborTaxRate1"]').text($tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });
    }

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