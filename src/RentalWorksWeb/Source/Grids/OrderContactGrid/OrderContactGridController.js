var OrderContactGrid = (function () {
    function OrderContactGrid() {
        this.Module = 'OrderContactGrid';
        this.apiurl = 'api/v1/ordercontact';
    }
    OrderContactGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="ContactId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.value').val($tr.find('.field[data-browsedatafield="ContactTitleId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.text').val($tr.find('.field[data-browsedatafield="ContactTitle"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Email"] input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        });
    };
    ;
    return OrderContactGrid;
}());
var OrderContactGridController = new OrderContactGrid();
//# sourceMappingURL=OrderContactGridController.js.map