class CompanyContactGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CompanyContactGrid';
        this.apiurl = 'api/v1/companycontact';
    }

    loadRelatedValidationFields(validationName, $valuefield, $tr) {
        var $form;
        $form = $valuefield.closest('.fwform');

        if (validationName === 'ContactValidation') {
        $form.find('.editrow .email input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        }

    };

}
(<any>window).CompanyContactGridController = new CompanyContactGrid();
//----------------------------------------------------------------------------------------------