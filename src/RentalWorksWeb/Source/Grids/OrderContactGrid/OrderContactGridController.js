var OrderContactGrid = (function () {
    function OrderContactGrid() {
        this.Module = 'OrderContactGrid';
        this.apiurl = 'api/v1/ordercontact';
    }
    OrderContactGrid.prototype.generateRow = function ($control, $generatedtr) {
        var $form;
        $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="ContactId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.value').val($tr.find('.field[data-browsedatafield="ContactTitleId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.text').val($tr.find('.field[data-browsedatafield="ContactTitle"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Email"] input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="CompanyId"] input').val($form.find('div[data-datafield="DealId"]').attr('data-originalvalue'));
        });
    };
    ;
    OrderContactGrid.prototype.addLegend = function ($control) {
        var $form;
        $form = $control.closest('.fwform');
        if ($form.attr('data-controller') === 'QuoteController') {
            FwBrowse.addLegend($control, 'Quoted For', '#00c400');
        }
        else {
            FwBrowse.addLegend($control, 'Ordered By', '#00c400');
        }
    };
    return OrderContactGrid;
}());
var OrderContactGridController = new OrderContactGrid();
//# sourceMappingURL=OrderContactGridController.js.map