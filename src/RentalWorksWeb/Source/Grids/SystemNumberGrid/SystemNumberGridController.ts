class SystemNumberGrid {
    Module: string = 'SystemNumberGrid';
    apiurl: string = 'api/v1/systemnumber';

    //----------------------------------------------------------------------------------------------
    generateRow = ($control, $generatedtr) => {

        $generatedtr.find('div[data-browsedatafield="IsAssignByUser"]').on('change', 'input.value', e => {
            var isUserAssigned = jQuery(e.currentTarget).prop('checked');
            if (isUserAssigned == true) {
                $generatedtr.find('.userassigned input').prop('disabled', true)
                    .css('background-color', '#F5F5F5');
                $generatedtr.find('.userassigned')
                    .parents('td').css('background-color', "#f5f5f5");
            } else {
                $generatedtr.find('.userassigned input').prop('disabled', false)
                    .css('background-color', '#FFFFFF');
                $generatedtr.find('.userassigned').parents('td').css('background-color', "#FFFFFF");
            }
        });

        FwBrowse.setAfterRenderFieldCallback($control, ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => {
            if ($field.attr('data-browsedatafield') === 'IsAssignByUser') {
                let isUsedAssigned = $field.find('input').prop('checked');
                if (isUsedAssigned) {
                    $tr.find('.userassigned input').prop('disabled', true);
                    $tr.find('.userassigned').parents('td')
                        .css('background-color', "#f5f5f5");
                } else {
                    $tr.find('.userassigned input').prop('disabled', false)
                    $tr.find('.userassigned').parents('td').css('background-color', "#FFFFFF");
                }
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    afterRowEditMode($control, $tr) {
        let $userAssignedField = $tr.find('[data-browsedatafield="IsAssignedByUser"] input'),
            isUserAssigned = $userAssignedField.prop('checked');

        if (isUserAssigned) {
            $tr.find('.userassigned input').prop('disabled', true)
                .css('background-color', '#F5F5F5');
        } else {
            $tr.find('.userassigned input').prop('disabled', false)
        }
    }
    //----------------------------------------------------------------------------------------------
}

var SystemNumberGridController = new SystemNumberGrid();
//----------------------------------------------------------------------------------------------