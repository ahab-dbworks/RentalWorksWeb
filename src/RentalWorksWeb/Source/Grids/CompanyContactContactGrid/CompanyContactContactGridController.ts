class CompanyContactContactGrid {
    Module: string;
    apiurl: string;
    ActiveView: string;

    constructor() {
        this.Module = 'CompanyContactContactGrid';
        this.apiurl = 'api/v1/companycontact';
        this.ActiveView = 'ALL';
    }

    loadRelatedValidationFields(validationName, $valuefield, $tr) {
        var $form;
        $form = $valuefield.closest('.fwform');

        if (validationName === 'ContactValidation') {
            $form.find('.editrow .email input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        }

        if (validationName === 'CompanyValidation') {
            $form.find('.editrow .type input').val($tr.find('.field[data-browsedatafield="CompanyType"]').attr('data-originalvalue'));
        }

    }; 
}

(<any>window).CompanyContactContactGridController = new CompanyContactContactGrid();
//----------------------------------------------------------------------------------------------