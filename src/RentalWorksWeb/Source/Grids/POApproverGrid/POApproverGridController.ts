class POApproverGrid {
    Module: string = 'POApproverGrid';
    apiurl: string = 'api/v1/poapprover';

    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');

        $generatedtr.find('div[data-browsedatafield="HasLimit"]').on('change', 'input.value', e => {
            var hasLimit = jQuery(e.currentTarget).prop('checked');
            if (hasLimit == false) {
                $generatedtr.find('.limit input').prop('disabled', true)
                    .css('background-color', '#F5F5F5');
                $generatedtr.find('.limit')
                    .parents('td').css('background-color', "#f5f5f5");
            } else {
                $generatedtr.find('.limit input').prop('disabled', false)
                    .css('background-color', '#FFFFFF');
                $generatedtr.find('.limit').parents('td').css('background-color', "#FFFFFF");
            }
        });

        FwBrowse.setAfterRenderFieldCallback($control, ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => {
            if ($field.attr('data-browsedatafield') === 'HasLimit') {
                let limitChecked = $field.find('input').prop('checked');
                if (!limitChecked) {
                    $tr.find('.limit input').prop('disabled', true);
                    $tr.find('.limit').parents('td')
                        .css('background-color', "#f5f5f5");
                } else {
                    $tr.find('.limit input').prop('disabled', false)
                    $tr.find('.limit').parents('td').css('background-color', "#FFFFFF");
                }
            }
        });
    }

    afterRowEditMode($control, $tr) {
        let $limitField = $tr.find('[data-browsedatafield="HasLimit"] input'),
            hasLimit = $limitField.prop('checked');

        if (!hasLimit) {
            $tr.find('.limit input').prop('disabled', true)
                .css('background-color', '#F5F5F5');
        } else {
            $tr.find('.limit input').prop('disabled', false)
        }
    }

    onRowNewMode($control: JQuery, $tr: JQuery) {
        $tr.find('.limit input').prop('disabled', true)
            .css('background-color', "#f5f5f5");
    }
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'UsersId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateuser`);
                break;
            case 'AppRoleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterole`);
                break;
        }
    }
}

var POApproverGridController = new POApproverGrid();
//----------------------------------------------------------------------------------------------