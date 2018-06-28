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
                $generatedtr.find('.limit input').prop('disabled', true)
                    .css('background-color', '#F5F5F5');
                $generatedtr.find('.limit')
                    .parents('td').css('background-color', "#f5f5f5");
            }
            else {
                $generatedtr.find('.limit input').prop('disabled', false)
                    .css('background-color', '#FFFFFF');
                $generatedtr.find('.limit').parents('td').css('background-color', "#FFFFFF");
            }
        });
        FwBrowse.setAfterRenderFieldCallback($control, function ($tr, $td, $field, dt, rowIndex, colIndex) {
            if ($field.attr('data-browsedatafield') === 'HasLimit') {
                var limitChecked = $field.find('input').prop('checked');
                if (!limitChecked) {
                    $tr.find('.limit input').prop('disabled', true);
                    $tr.find('.limit').parents('td')
                        .css('background-color', "#f5f5f5");
                }
                else {
                    $tr.find('.limit input').prop('disabled', false);
                    $tr.find('.limit').parents('td').css('background-color', "#FFFFFF");
                }
            }
        });
    };
    POApproverGrid.prototype.afterRowEditMode = function ($control, $tr) {
        var $limitField = $tr.find('[data-browsedatafield="HasLimit"] input'), hasLimit = $limitField.prop('checked');
        if (!hasLimit) {
            $tr.find('.limit input').prop('disabled', true)
                .css('background-color', '#F5F5F5');
        }
        else {
            $tr.find('.limit input').prop('disabled', false);
        }
    };
    POApproverGrid.prototype.onRowNewMode = function ($control, $tr) {
        $tr.find('.limit input').prop('disabled', true)
            .css('background-color', "#f5f5f5");
    };
    return POApproverGrid;
}());
var POApproverGridController = new POApproverGrid();
//# sourceMappingURL=POApproverGridController.js.map