﻿class CompanyContactGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CompanyContactGrid';
        this.apiurl = 'api/v1/companycontact';
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="ContactId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));            
            $generatedtr.find('.field[data-browsedatafield="Email"] input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        });
    };
}

var CompanyContactGridController = new CompanyContactGrid();
//----------------------------------------------------------------------------------------------