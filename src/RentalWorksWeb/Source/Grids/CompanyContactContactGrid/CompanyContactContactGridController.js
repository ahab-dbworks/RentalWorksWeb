var CompanyContactContactGrid = /** @class */ (function () {
    function CompanyContactContactGrid() {
        this.Module = 'CompanyContactContactGrid';
        this.apiurl = 'api/v1/companycontact';
        this.ActiveView = 'ALL';
    }
    CompanyContactContactGrid.prototype.loadRelatedValidationFields = function (validationName, $valuefield, $tr) {
        var $form;
        $form = $valuefield.closest('.fwform');
        if (validationName === 'ContactValidation') {
            $form.find('.editrow .email input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        }
        if (validationName === 'CompanyValidation') {
            $form.find('.editrow .type input').val($tr.find('.field[data-browsedatafield="CompanyType"]').attr('data-originalvalue'));
        }
    };
    ;
    return CompanyContactContactGrid;
}());
window.CompanyContactContactGridController = new CompanyContactContactGrid();
//---------------------------------------------------------------------------------------------- 
//# sourceMappingURL=CompanyContactContactGridController.js.map