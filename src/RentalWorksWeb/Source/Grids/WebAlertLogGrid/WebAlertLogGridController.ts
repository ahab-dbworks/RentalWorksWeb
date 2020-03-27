class WebAlertLogGrid {
    Module: string = 'WebAlertLog';
    apiurl: string = 'api/v1/webalertlog';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let $alertBodyField = $tr.find('[data-browsedatafield="AlertBody"]');
            const val = FwBrowse.getValueByDataField($control, $tr, 'AlertBody');

            const $contentIcon = jQuery('<i class="material-icons" style="cursor:pointer;color:#0D47A1;">insert_drive_file</i>');
            $alertBodyField.empty().append($contentIcon);

            $contentIcon.on('click', e => {
                e.stopPropagation();
                const $confirmation = FwConfirmation.renderConfirmation('Alert Body', '');
                FwConfirmation.addButton($confirmation, 'Close', true);
                FwConfirmation.addControls($confirmation, val);
            });
        });
    }
}

var WebAlertLogGridController = new WebAlertLogGrid();
//----------------------------------------------------------------------------------------------