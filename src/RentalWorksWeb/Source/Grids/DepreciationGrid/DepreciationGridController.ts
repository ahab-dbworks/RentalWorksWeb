class DepreciationGrid {
    Module: string = 'DepreciationGrid';
    apiurl: string = 'api/v1/depreciation';



    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="DebitGlAccountId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="GlAccountDescription"] input').val($tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
        $generatedtr.find('div[data-browsedatafield="CreditGlAccountId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="CreditGlAccountDescription"] input').val($tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });
    }


    addLegend($control) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, response => {
                for (let key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, ex => {
                FwFunc.showError(ex);
            }, $control);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
}

var DepreciationGridController = new DepreciationGrid();
//----------------------------------------------------------------------------------------------