class InventorySummaryRetiredHistoryGrid {
    Module: string = 'InventorySummaryRetiredHistoryGrid';
    apiurl: string = 'api/v1/inventorysummaryretiredhistory';
    generateRow($control, $generatedtr) {

        $generatedtr.on('click', '[data-browsedatafield="RetiredDate"] .peekBtn', function (e: JQuery.Event) {
            try {
                let $this = jQuery(this);
                let $td = $this.parent();
                $this.css('visibility', 'hidden');
                $td.find('.validation-loader').show();
                setTimeout(function () {
                    const retiredId = FwBrowse.getValueByDataField($control, $generatedtr, 'RetiredId');
                    FwValidation.validationPeek($control, 'RetiredHistory', retiredId, 'RetiredId', null, 'Retire History');
                    $this.css('visibility', 'visible');
                    $td.find('.validation-loader').hide();
                })

            } catch (ex) {
                FwFunc.showError(ex)
            }
            e.stopPropagation();
        });
        // creating a validation peek for RetiredDate so it can open into Retire History form
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const $retiredDateField = $tr.find('[data-browsedatafield="RetiredDate"]');
            const validationIcon = `<div class="peekBtn" style="cursor:pointer;"><i class="material-icons">more_horiz</i></div>`
            $retiredDateField.append(validationIcon);

        });
    }
}

var InventorySummaryRetiredHistoryGridController = new InventorySummaryRetiredHistoryGrid();
//----------------------------------------------------------------------------------------------