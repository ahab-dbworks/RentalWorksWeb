﻿class ManualGlTransactionsGrid {
    Module: string = 'ManualGlTransactionsGrid';
    apiurl: string = 'api/v1/glmanual';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        const $form = $control.closest('.fwform');
        const invoiceDate = FwFormField.getValueByDataField($form, 'InvoiceDate');
        FwBrowse.setFieldValue($control, $tr, 'GlDate', { value: invoiceDate });
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="DebitGlAccountId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="DebitGlAccountDescription"]').text($tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        $generatedtr.find('div[data-browsedatafield="CreditGlAccountId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="CreditGlAccountDescription"]').text($tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
    }
}
//----------------------------------------------------------------------------------------------
var ManualGlTransactionsGridController = new ManualGlTransactionsGrid();