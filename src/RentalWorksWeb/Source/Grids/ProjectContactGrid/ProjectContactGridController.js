var ProjectContactGrid = (function () {
    function ProjectContactGrid() {
        this.Module = 'ProjectContactGrid';
        this.apiurl = 'api/v1/projectcontact';
    }
    ProjectContactGrid.prototype.generateRow = function ($control, $generatedtr) {
        var $form;
        $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="ProjectContactId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.value').val($tr.find('.field[data-browsedatafield="ContactTitleId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.text').val($tr.find('.field[data-browsedatafield="ContactTitle"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Email"] input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="CompanyId"] input').val($form.find('div[data-datafield="DealId"]').attr('data-originalvalue'));
        });
    };
    ;
    ProjectContactGrid.prototype.addLegend = function ($control) {
        var $form;
        $form = $control.closest('.fwform');
        FwBrowse.addLegend($control, 'Project For', '#00c400');
    };
    return ProjectContactGrid;
}());
var ProjectContactGridController = new ProjectContactGrid();
//# sourceMappingURL=ProjectContactGridController.js.map