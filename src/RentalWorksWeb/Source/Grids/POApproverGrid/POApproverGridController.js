var POApproverGrid = (function () {
    function POApproverGrid() {
        this.Module = 'POApproverGrid';
        this.apiurl = 'api/v1/poapprover';
    }
    POApproverGrid.prototype.generateRow = function ($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="HasLimit"] input').data('onchange', function ($tr) {
            var hasLimit = $tr.find('div[data-browsedatafield="HasLimit"]').prop('checked');
            alert(hasLimit);
            if (hasLimit == 'false') {
                FwFormField.disable($tr.find('.limit'));
                console.log("in false");
            }
        });
    };
    return POApproverGrid;
}());
var POApproverGridController = new POApproverGrid();
//# sourceMappingURL=POApproverGridController.js.map