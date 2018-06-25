var POApproverGrid = (function () {
    function POApproverGrid() {
        this.Module = 'POApproverGrid';
        this.apiurl = 'api/v1/poapprover';
    }
    POApproverGrid.prototype.generateRow = function ($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="HasLimit"]').on('change', 'input.value', function (e) {
            var hasLimit = jQuery(e.currentTarget).prop('checked');
            if (hasLimit == false) {
                $generatedtr.find('.limit input').css('background-color', '#f5f5f5');
            }
            else {
                $generatedtr.find('.limit input').css('background-color', '#FFFFFF');
            }
        });
        FwBrowse.setAfterRenderFieldCallback($control, function ($tr, $td, $field, dt, rowIndex, colIndex) {
            if ($field.attr('data-browsedatafield') === 'HasLimit') {
                var limitChecked = $field.find('input').prop('checked');
                if (!limitChecked) {
                    $tr.find('.limit').css('background-color', '#f5f5f5');
                }
                else {
                    $tr.find('.limit').css('background-color', '#FFFFFF');
                }
            }
        });
    };
    return POApproverGrid;
}());
var POApproverGridController = new POApproverGrid();
//# sourceMappingURL=POApproverGridController.js.map