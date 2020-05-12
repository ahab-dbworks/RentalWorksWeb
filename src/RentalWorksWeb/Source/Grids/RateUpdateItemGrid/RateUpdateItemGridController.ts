class RateUpdateItemGrid {
    Module: string = 'RateUpdateItemGrid';
    apiurl: string = 'api/v1/rateupdateitem';
    AuditKeyFields: Array<string> = ['InventoryId', 'WarehouseId'];

    addLegend($control: any) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $control);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderFieldCallback($control, ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => {
            if ($field.attr('data-caption') == 'New') {
                if ($field.attr('data-originalvalue') != '0') {
                    $field.css({
                        'background-color': '#9aeabe'
                    });
                }
            }
        });

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const availFor = FwBrowse.getValueByDataField($control, $generatedtr, 'AvailableFor');
            let inventoryPeekForm;
            let categoryPeekForm;
            switch (availFor) {
                case 'R':
                    inventoryPeekForm = 'RentalInventory';
                    categoryPeekForm = 'RentalCategory';
                    break;
                case 'S':
                    inventoryPeekForm = 'SalesInventory';
                    categoryPeekForm = 'SalesCategory';
                    break;
                case 'P':
                    inventoryPeekForm = 'PartsInventory';
                    categoryPeekForm = 'PartsCategory';
                    break;
                case 'M':
                    inventoryPeekForm = 'MiscRate';
                    categoryPeekForm = 'MiscCategory';
                    break;
                case 'L':
                    inventoryPeekForm = 'LaborRate';
                    categoryPeekForm = 'LaborCategory';
                    break;
            }
            const $inventoryTd = $generatedtr.find('[data-validationname="GeneralItemValidation"]');
            $inventoryTd.attr('data-peekForm', inventoryPeekForm);

            const $categoryTd = $generatedtr.find('[data-validationname="CategoryValidation"]');
            $categoryTd.attr('data-peekForm', categoryPeekForm);
        });
    }
    //----------------------------------------------------------------------------------------------
    applyPercentageChanges($form: JQuery) {
        const $grid = $form.find('[data-name="RateUpdateItemGrid"]');
        const $selectedCheckBoxes = $grid.find('tbody .cbselectrow:checked');
        const enableThreeWeekPricing = JSON.parse(sessionStorage.getItem('controldefaults')).enable3weekpricing;
        if ($selectedCheckBoxes.length) {
            const type = FwFormField.getValueByDataField($form, 'AvailableFor');
            let ratetypes;
            switch (type) {
                case 'R':
                    if (!enableThreeWeekPricing) {
                        ratetypes = ['Daily Rate', 'Weekly Rate', 'Monthly Rate', 'Replacement Cost', 'Unit Value'];
                    } else {
                        ratetypes = ['Daily Rate', 'Weekly Rate', 'Week 2 Rate', 'Week 3 Rate', 'Week 4 Rate', 'Monthly Rate', 'Replacement Cost', 'Unit Value'];
                    }
                    break;
                case 'S':
                case 'P':
                    ratetypes = ['Retail', 'Price', 'Default Cost'];
                    break;
                case 'M':
                case 'L':
                    ratetypes = ['Hourly Rate', 'Daily Rate', 'Weekly Rate', 'Monthly Rate', 'Hourly Cost', 'Daily Cost', 'Weekly Cost', 'Monthly Cost'];
                    break;
            }

            const pageSize = $grid.data('pagesize');
            const totalPages = $grid.data('totalpages');
            const totalRows = $grid.data('totalRowCount');

            let $confirmation, $ok;
            $confirmation = FwConfirmation.renderConfirmation('Apply Percentage Changes', '');
            const html: Array<string> = [];;
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Changes to Apply" style="margin-bottom:1em;">')
            html.push('        <div class="flexrow">');
            html.push('                 <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="ChangeType" style="margin:.5em; 1em;">');
            html.push(`                      <div data-value="INCREASE" data-caption="INCREASE all 'New' values"></div>`);
            html.push(`                      <div data-value="DECREASE" data-caption="DECREASE all 'New' values"></div>`);
            html.push('                 </div>');
            html.push(`                 <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="by % of 'Current' values" data-datafield="Percent" style="max-width:150px; margin:0 1em;"></div>`);
            html.push('         </div>');
            html.push('    </div>');
            html.push('    <div class="fwcontrol fwcontainer fwform-section fields-to-update" data-control="FwContainer" data-type="section" data-caption="Apply to these Rates/Costs">')
            for (let i = 0; i < ratetypes.length; i++) {
                html.push(`<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="${ratetypes[i]}" data-datafield="${ratetypes[i].replace(/ /g, '')}"></div>`);
            }
            html.push('    </div>');
            if ($selectedCheckBoxes.length === pageSize && totalPages > 1) {
                html.push(`    <div class="warning" style="margin:1em; color:red;">Warning: only the first ${$selectedCheckBoxes.length} item(s) of ${totalRows} have been selected.</div>`);
            }
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));
            $ok = FwConfirmation.addButton($confirmation, `APPLY CHANGES TO ${$selectedCheckBoxes.length} ITEM(S)`, false);
            FwConfirmation.addButton($confirmation, 'CANCEL');
            $ok.on('click', e => {
                const changeType = FwFormField.getValueByDataField($confirmation, 'ChangeType');
                const changeAmount = FwFormField.getValueByDataField($confirmation, 'Percent');
                const $fields = $confirmation.find('.fields-to-update [data-datafield] input:checked').closest('[data-datafield]');

                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    const $row = jQuery($selectedCheckBoxes[i]).closest('tr');
                    FwBrowse.setRowEditMode($grid, $row);
                    for (let j = 0; j < $fields.length; j++) {
                        const fieldName = jQuery($fields[j]).data('datafield');
                        const currentValue = parseFloat(FwBrowse.getValueByDataField($grid, $row, fieldName));
                        let newValue;
                        if (changeType === 'INCREASE') {
                            newValue = currentValue + (currentValue * (changeAmount / 100));
                        } else {
                            newValue = currentValue - (currentValue * (changeAmount / 100));
                        }
                        FwBrowse.setFieldValue($grid, $row, `New${fieldName}`, { value: newValue.toString() });
                    }
                }
                FwConfirmation.destroyConfirmation($confirmation);
                $grid.find('.grid-multi-save').click();
            });
        } else {
            FwNotification.renderNotification('WARNING', 'Select rows in order to apply percentage changes.')
        }
       
    }
    //----------------------------------------------------------------------------------------------
}

var RateUpdateItemGridController = new RateUpdateItemGrid();
//----------------------------------------------------------------------------------------------