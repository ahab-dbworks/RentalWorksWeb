class ContactCompanyGrid {
    constructor() {
        this.Module = 'ContactCompanyGrid';
        this.apiurl = 'api/v1/companycontact';
        this.ActiveView = 'ALL';
    }
    generateRow($control, $generatedtr) {
        var $form;
        $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="CompanyId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="CompanyType"]').text($tr.find('.field[data-browsedatafield="CompanyType"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.value').val($form.find('div[data-datafield="ContactTitleId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.text').val($form.find('div[data-datafield="ContactTitleId"] input.fwformfield-text').val());
            $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($form.find('div[data-datafield="OfficePhone"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Email"] input').val($form.find('div[data-datafield="Email"]').attr('data-originalvalue'));
        });
    }
    ;
}
var ContactCompanyGridController = new ContactCompanyGrid();
//# sourceMappingURL=ContactCompanyGridController.js.map