class ManualGlTransactionsGrid {
    Module: string = 'ManualGlTransactionsGrid';
    apiurl: string = 'api/v1/glmanual';

    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
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