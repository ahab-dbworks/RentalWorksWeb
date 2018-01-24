var CompanyContactGrid = /** @class */ (function () {
    function CompanyContactGrid() {
        this.Module = 'CompanyContactGrid';
        this.apiurl = 'api/v1/companycontact';
    }
    CompanyContactGrid.prototype.loadRelatedValidationFields = function (validationName, $valuefield, $tr) {
        var $form;
        $form = $valuefield.closest('.fwform');
        if (validationName === 'ContactValidation') {
            $form.find('.editrow .email input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        }
    };
    ;
    return CompanyContactGrid;
}());
window.CompanyContactGridController = new CompanyContactGrid();
//---------------------------------------------------------------------------------------------- 
//# sourceMappingURL=CompanyContactGridController.js.map