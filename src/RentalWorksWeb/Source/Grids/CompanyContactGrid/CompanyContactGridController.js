var CompanyContactGrid = (function () {
    function CompanyContactGrid() {
        this.Module = 'CompanyContactGrid';
        this.apiurl = 'api/v1/companycontact';
    }
    CompanyContactGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="ContactId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Email"] input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        });
    };
    ;
    return CompanyContactGrid;
}());
window.CompanyContactGridController = new CompanyContactGrid();
//# sourceMappingURL=CompanyContactGridController.js.map