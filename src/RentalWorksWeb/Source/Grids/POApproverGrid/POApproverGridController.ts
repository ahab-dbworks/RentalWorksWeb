class POApproverGrid {
    Module: string = 'POApproverGrid';
    apiurl: string = 'api/v1/poapprover';

    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');

        $generatedtr.find('div[data-browsedatafield="HasLimit"]').on('change', 'input.value', e => {
            var hasLimit = jQuery(e.currentTarget).prop('checked');
            if (hasLimit == false) {
                $generatedtr.find('.limit input').css('background-color', '#f5f5f5');
            } else {
                $generatedtr.find('.limit input').css('background-color', '#FFFFFF');
            }
        });

        FwBrowse.setAfterRenderFieldCallback($control, ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => {

            if ($field.attr('data-browsedatafield') === 'HasLimit') {
                let limitChecked = $field.find('input').prop('checked');
                if (!limitChecked) {
                    $tr.find('.limit').css('background-color', '#f5f5f5');
                } else {
                    $tr.find('.limit').css('background-color', '#FFFFFF');
                }
            }
        });

    }

}

var POApproverGridController = new POApproverGrid();
//----------------------------------------------------------------------------------------------