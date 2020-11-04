class CompanyContactGrid {
    Module: string = 'CompanyContactGrid';
    apiurl: string = 'api/v1/companycontact';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="ContactId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.value').val($tr.find('.field[data-browsedatafield="ContactTitleId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.text').val($tr.find('.field[data-browsedatafield="ContactTitle"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="MobilePhone"] input').val($tr.find('.field[data-browsedatafield="MobilePhone"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Email"] input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        });
    };
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ContactId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontact`);
                break;
            case 'ContactTitleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontacttitle`);
                break;
        }
    }
}

var CompanyContactGridController = new CompanyContactGrid();
//----------------------------------------------------------------------------------------------