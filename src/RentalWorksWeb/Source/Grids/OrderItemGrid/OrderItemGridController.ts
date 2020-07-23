class OrderItemGrid {
    Module: string = 'OrderItemGrid';
    apiurl: string = 'api/v1/orderitem';
    //----------------------------------------------------------------------------------------------
    onRowNewMode($control: JQuery, $tr: JQuery) {
        let $form = $control.closest('.fwform');
        const controller = $form.attr('data-controller');
        let $grid = $tr.parents('[data-grid="OrderItemGrid"]');
        let inventoryType;
        //let pickDate;
        //let pickTime;
        //let fromDate;
        //let fromTime;
        //let toDate;
        //let toTime;
        //if (controller !== "TemplateController" && controller !== "PurchaseOrderController") {
        //    pickDate = OrderBaseController.getPickStartStop($form, 'div[data-dateactivitytype="PICK"]');
        //    pickTime = OrderBaseController.getPickStartStop($form, 'div[data-timeactivitytype="PICK"]');
        //    fromDate = OrderBaseController.getPickStartStop($form, 'div[data-dateactivitytype="START"]');
        //    fromTime = OrderBaseController.getPickStartStop($form, 'div[data-timeactivitytype="START"]');
        //    toDate = OrderBaseController.getPickStartStop($form, 'div[data-dateactivitytype="STOP"]');
        //    toTime = OrderBaseController.getPickStartStop($form, 'div[data-timeactivitytype="STOP"]');
        //};
        let pickStartStop: PickStartStop;
        if ((controller === 'QuoteController') || (controller === 'OrderController')) {
            pickStartStop = OrderBaseController.getPickStartStop($form);
        }
        const $td = $tr.find('[data-browsedatafield="InventoryId"]');

        if ($grid.hasClass('R')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'R' });
            inventoryType = 'Rental';
            $td.attr('data-peekForm', 'RentalInventory');
        } else if ($grid.hasClass('S')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'S' });
            inventoryType = 'Sales';
            $td.attr('data-peekForm', 'SalesInventory');
        } else if ($grid.hasClass('M')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'M' });
            inventoryType = 'Misc';
        } else if ($grid.hasClass('L')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'L' });
            inventoryType = 'Labor';
        } else if ($grid.hasClass('RS')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'RS' });
            inventoryType = 'RentalSales';
        } else if ($grid.hasClass('A')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'A' });
            inventoryType = 'Combined';
        } else if ($grid.hasClass('P')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'P' });
            $td.attr('data-peekForm', 'PartsInventory');
            inventoryType = 'Parts';
        } else if ($grid.hasClass('F')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'F' });
            inventoryType = 'LossAndDamage';
        }

        if (controller !== "TemplateController" && controller !== "PurchaseOrderController") {
            let daysPerWeek, discountPercent;
            if (inventoryType !== 'RentalSales') {
                discountPercent = FwFormField.getValueByDataField($form, `${inventoryType}DiscountPercent`);
            }
            if (inventoryType == 'Rental') {
                daysPerWeek = FwFormField.getValueByDataField($form, `RentalDaysPerWeek`);
            };

            FwBrowse.setFieldValue($grid, $tr, 'PickDate', { value: pickStartStop.PickDate });
            FwBrowse.setFieldValue($grid, $tr, 'PickTime', { value: pickStartStop.PickTime });
            FwBrowse.setFieldValue($grid, $tr, 'FromDate', { value: pickStartStop.StartDate });
            FwBrowse.setFieldValue($grid, $tr, 'FromTime', { value: pickStartStop.StartTime });
            FwBrowse.setFieldValue($grid, $tr, 'ToDate', { value: pickStartStop.StopDate });
            FwBrowse.setFieldValue($grid, $tr, 'ToTime', { value: pickStartStop.StopTime });
            if (inventoryType !== 'RentalSales') {
                FwBrowse.setFieldValue($grid, $tr, 'DiscountPercent', { value: discountPercent });
                FwBrowse.setFieldValue($grid, $tr, 'DiscountPercentDisplay', { value: discountPercent });
            }
            if (inventoryType == 'Rental') {
                FwBrowse.setFieldValue($grid, $tr, 'DaysPerWeek', { value: daysPerWeek });
            };
        }

        //Allow searching on description field
        $tr.find('[data-browsedatafield="Description"]').data('changedisplayfield', $validationbrowse => {
            $validationbrowse.find('[data-browsedatafield="ICode"]').attr('data-validationdisplayfield', 'false');
            $validationbrowse.find('[data-browsedatafield="Description"]').attr('data-validationdisplayfield', 'true');
        });
    }
    //----------------------------------------------------------------------------------------------
    addPrintNotes($browse, $tr, $field, controlhtml, $confirmation, $ok) {
        const formmaxlength = (typeof $field.attr('data-formmaxlength') === 'string') ? $field.attr('data-formmaxlength') : '';
        controlhtml.push('<div class="flexrow" style="overflow:hidden;">');
        controlhtml.push('  <div class="flexcolumn">');
        controlhtml.push('    <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield note" data-caption="Notes" data-enabled=""' + ((formmaxlength !== '0') ? 'data-maxlength="' + formmaxlength : '') + '" data-datafield=""></div>');
        controlhtml.push('  </div>');
        controlhtml.push('  <div class="flexcolumn" style="max-width:153px;">');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        controlhtml.push(`      <div style="float:left;width:200px;text-decoration:underline;">Print Notes on</div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer flexrow order quote" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Quote" data-datafield="PrintNoteOnQuote" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer flexrow order transfer-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Order" data-datafield="PrintNoteOnOrder" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer flexrow purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Purchase Order" data-datafield="PrintNoteOnPurchaseOrder" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow transfer-order order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pick List" data-datafield="PrintNoteOnPickList" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Receive Contract" data-datafield="PrintNoteOnReceiveContract" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Return List" data-datafield="PrintNoteOnReturnList" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Vendor Return List" data-datafield="PrintNoteOnVendorReturnList" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Vendor Receive List" data-datafield="PrintNoteOnVendorReceiveList" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Return Contract" data-datafield="PrintNoteOnReturnContract" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order transfer-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Out Contract" data-datafield="PrintNoteOnOutContract" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order transfer-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="In Contract" data-datafield="PrintNoteOnInContract" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
        controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Invoice" data-datafield="PrintNoteOnInvoice" style="float:left;width:100px;"></div>`);
        controlhtml.push('    </div>');
        controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        controlhtml.push(`      <div data-datavalue="CheckAll" data-confirmfield="CheckAllNone" style="float:left;width:120px;text-decoration:underline;color:#4646ff;">Check All</div>`);
        controlhtml.push('    </div>');
        controlhtml.push('  </div>');
        controlhtml.push('</div>');
        $confirmation.find('.fwconfirmationbox').css({ 'width': '515px' });
        FwConfirmation.addControls($confirmation, controlhtml.join('\n'));

        $confirmation.find('.note textarea')
            .css({
                'height': '510px',
                'resize': 'vertical'
            });

        const formController = $browse.closest('.fwform').attr('data-controller');
        if (formController === 'TransferOrderController') {
            $confirmation.find('.transfer-order').show();
            $confirmation.find('div[data-datafield="PrintNoteOnOrder"] label').text('Transfer Order');
            $confirmation.find('div[data-datafield="PrintNoteOnOutContract"] label').text('Manifest');
            $confirmation.find('div[data-datafield="PrintNoteOnInContract"] label').text('Transfer Receipt');
        }
        if (formController === 'PurchaseOrderController') {
            $confirmation.find('.purchase-order').show();
            $confirmation.find('div[data-datafield="PrintNoteOnOrder"] label').text('Purchase Order');
        }

        if (formController === 'OrderController') {
            $confirmation.find('.order:not(.quote)').show();
        }
        if (formController === 'QuoteController') {
            $confirmation.find('.order').show();
        }

        fillInCheckboxesFromRow($confirmation, $tr);
        setTimeout(() => { $confirmation.find('textarea.fwformfield-value').focus(); }, 0)
        // ----------
        $ok.on('mousedown', () => { // saving checkbox values before popup is destroyed on 'ok' click
            const $checkboxes = $confirmation.find('div[data-type="checkbox"]:visible');
            $checkboxes.each((i, e) => {
                const dataField = jQuery(e).attr('data-datafield');
                FwBrowse.setFieldValue($browse, $tr, dataField, { value: FwFormField.getValueByDataField($confirmation, dataField) === 'T' ? true : false });
            });
        });
        // Check All / None
        $confirmation.find('div[data-confirmfield="CheckAllNone"]').on('click', e => {
            const checkAll = $confirmation.find('div[data-confirmfield="CheckAllNone"]').attr('data-datavalue');
            const $checkboxes = $confirmation.find('div[data-type="checkbox"]:visible');
            if (checkAll === 'CheckAll') {
                // select all fields
                $confirmation.find('div[data-confirmfield="CheckAllNone"]').attr('data-datavalue', 'CheckNone');
                $confirmation.find('div[data-confirmfield="CheckAllNone"]').text('Check None');
                $checkboxes.each((i, e) => {
                    const dataField = jQuery(e).attr('data-datafield');
                    FwFormField.setValueByDataField($confirmation, dataField, true);
                });
            } else {
                //unselect all
                $confirmation.find('div[data-confirmfield="CheckAllNone"]').attr('data-datavalue', 'CheckAll');
                $confirmation.find('div[data-confirmfield="CheckAllNone"]').text('Check All');
                $checkboxes.each((i, e) => {
                    const dataField = jQuery(e).attr('data-datafield');
                    FwFormField.setValueByDataField($confirmation, dataField, false);
                });
            }
        });
        // ----------
        function fillInCheckboxesFromRow($confirmation, $tr) {
            const $checkboxes = $confirmation.find('div[data-type="checkbox"]:visible');
            let checkedCount = 0;
            $checkboxes.each((i, e) => {
                const dataField = jQuery(e).attr('data-datafield');
                const val = $tr.find(`.field[data-browsedatafield="${dataField}"] input`).prop('checked');
                val === true ? checkedCount++ : '';
                FwFormField.setValueByDataField($confirmation, dataField, val);
            });

            if (checkedCount === $checkboxes.length) {
                $confirmation.find('div[data-confirmfield="CheckAllNone"]').attr('data-datavalue', 'CheckNone');
                $confirmation.find('div[data-confirmfield="CheckAllNone"]').text('Check None');
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ItemId':
                let inventoryId = $tr.find('.field[data-browsedatafield="InventoryId"] input').val();
                if (inventoryId != '') {
                    request.uniqueids = {
                        InventoryId: inventoryId
                    }
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebarcode`);
                break;
            case 'Description':
            case 'InventoryId':
                const rate = FwBrowse.getValueByDataField($validationbrowse, $tr, 'RecType');
                if (rate !== null) {
                    switch (rate) {
                        case 'R':
                            request.uniqueids = {
                                AvailFor: 'R'
                            };
                            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateicoderental`);
                            break;
                        case 'S':
                            request.uniqueids = {
                                AvailFor: 'S'
                            };
                            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateicodesales`);
                            break;
                        case 'M':
                            request.uniqueids = {
                                AvailFor: 'M'
                            };
                            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateicodemisc`);
                            break;
                        case 'L':
                            request.uniqueids = {
                                AvailFor: 'L'
                            };
                            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateicodelabor`);
                            break;
                        case 'P':
                            request.uniqueids = {
                                AvailFor: 'P'
                            };
                            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateicodeparts`);
                            break;
                    }
                    request.uniqueids.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                    request.uniqueids.LocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
                }
                break;
            case 'UnitId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateunit`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        const controller = $form.attr('data-controller');

        //if ($form.attr('data-controller') === 'OrderController' || $form.attr('data-controller') === 'QuoteController' || $form.attr('data-controller') === 'PurchaseOrderController') {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            // Bold Row
            if ($tr.find('.order-item-bold').text() === 'true') {
                $tr.css('font-weight', "bold");
            }

            // Summarized Row
            if ($tr.find('.order-item-rowsrolledup').text() === 'true') {
                $tr.css('font-style', "italic");
            }

            // Group Header Row
            if ($tr.find('.itemclass').text() === 'GH') {
                $tr.css('font-weight', "bold");
                $tr.css('background-color', "#ffffcc");
                $tr.find('.field:not(.groupheaderline) ').text('');
            }

            // Text Row
            if ($tr.find('.itemclass').text() === 'T') {
                $tr.find('.field:not(.textline) ').text('');
            }

            // Sub-Total Row
            if ($tr.find('.itemclass').text() === 'ST') {
                $tr.css('font-weight', "bold");
                $tr.css('background-color', "#ffff80");
                $tr.find('.field:not(.subtotalline) ').text('');
            }

            //Option to open up Complete/Kit grid to add items
            let itemClass = FwBrowse.getValueByDataField($control, $tr, 'ItemClass');
            const $browsecontextmenu = $tr.find('.browsecontextmenu');
            const classList: any = ['C', 'CI', 'CO', 'K', 'KI', 'KO'];

            $browsecontextmenu.data('contextmenuoptions', $tr => {
                if (classList.includes(itemClass)) {
                    FwContextMenu.addMenuItem($browsecontextmenu, `Update Options`, () => {
                        try {
                            this.renderCompleteKitGridPopup($control, $tr, itemClass);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                //Insert line-item option
                FwContextMenu.addMenuItem($browsecontextmenu, `Insert Line Item`, () => {
                    try {
                        this.insertLineItem($control, $tr);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            });

            const recType = FwBrowse.getValueByDataField($control, $tr, 'RecType');
            if (controller === 'QuoteController' || controller === 'OrderController') {
                const availabilityState = FwBrowse.getValueByDataField($control, $generatedtr, 'AvailabilityState');
                const $availQty = $generatedtr.find('[data-browsedatafield="AvailableQuantity"]')
                $availQty.attr('data-state', availabilityState);
                $availQty.css('cursor', 'pointer');
            }

            if (controller === 'PurchaseOrderController') {
                let peekForm;
                switch (recType) {
                    case 'R':
                        peekForm = 'RentalInventory';
                        break;
                    case 'S':
                        peekForm = 'SalesInventory';
                        break;
                    case 'P':
                        peekForm = 'PartsInventory';
                        break;
                }
                const $td = $tr.find('[data-validationname="GeneralItemValidation"]');
                $td.attr('data-peekForm', peekForm);
            }

            if ($control.parents('[data-control="FwGrid"]').hasClass('A')) { //set peek forms for Items tab
                let peekForm;
                switch (recType) {
                    case 'R':
                    case 'RS':
                        peekForm = 'RentalInventory';
                        break;
                    case 'S':
                        peekForm = 'SalesInventory';
                        break;
                    case 'P':
                        peekForm = 'PartsInventory';
                        break;
                    case 'M':
                        peekForm = 'MiscRate';
                        break;
                    case 'L':
                        peekForm = 'LaborRate';
                        break;
                }
                const $td = $tr.find('[data-validationname="GeneralItemValidation"]');
                $td.attr('data-peekForm', peekForm);
            }

            //Allow searching on description field
            const validDescriptionValidationTypes: any = ['R', 'S', 'P', 'M', 'L'];
            const validTextItemClasses: any = ['M', 'GH', 'T', 'ST'];
            if (validDescriptionValidationTypes.includes(recType)) {
                if (validTextItemClasses.includes(itemClass)) {
                    $tr.find('[data-browsedatafield="Description"]').attr({ 'data-browsedatatype': 'text', 'data-formdatatype': 'text' });
                } else {
                    $tr.find('[data-browsedatafield="Description"]').data('changedisplayfield', $validationbrowse => {
                        $validationbrowse.find('[data-browsedatafield="ICode"]').attr('data-validationdisplayfield', 'false');
                        $validationbrowse.find('[data-browsedatafield="Description"]').attr('data-validationdisplayfield', 'true');
                    });
                }
            }
        });

        FwBrowse.setAfterRenderFieldCallback($control, ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => {
            // Lock Fields
            if ($tr.find('.order-item-lock').text() === 'true') {
                $tr.find('.field-to-lock').parent('td').css({
                    'background-color': '#f5f5f5',
                    //'border': '1.5px inset'
                    'border-top': '2px solid black',
                    'border-bottom': '2px solid black'

                });
                $tr.find('.field-to-lock').attr('data-formreadonly', 'true');
                // disabled grids were rendering with different shade background color
                if ($control.attr('data-enabled') === 'false') {
                    $tr.find('.field-to-lock').css('background-color', 'transparent');
                }
            }

            // Mute Fields
            if ($tr.find('.order-item-mute').text() === 'true') {
                $tr.find('.field-to-mute').css({ 'background-color': '#ffccff' });
                $tr.find('.field-to-mute').parent('td').css({
                    'border-top': '2px solid black',
                    'border-bottom': '2px solid black'
                });
                $tr.find('.field-to-mute').attr('data-formreadonly', 'true');
                // disabled grids were rendering with different shade background color
                if ($control.attr('data-enabled') === 'false') {
                    $tr.find('.field-to-mute').css('background-color', 'transparent');
                }
            }

            //enable editing price on misc items
            const isMiscClass = FwBrowse.getValueByDataField($control, $generatedtr, 'ItemClass');
            if (isMiscClass === 'M') {
                $generatedtr.find('[data-browsedatafield="Price"]').attr('data-formreadonly', 'false');
            }
        });

        if (controller === 'QuoteController' || controller === 'OrderController') {
            //availability calendar popup
            $generatedtr.find('div[data-browsedatafield="AvailableQuantity"]').on('click', e => {
                const inventoryId = FwBrowse.getValueByDataField($control, $generatedtr, 'InventoryId');
                if (inventoryId) {

                    let $popup = jQuery(`
                            <div>
                              <div id="availabilityCalendarPopup" class="flexrow body">
                                <div class="flexcolumn">
                                  <div class="flexrow">
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" data-enabled="false" style="flex:0 1 100px;"></div>
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:0 1 500px;"></div>
                                  </div>
                                  <div class="flexrow inv-data-totals">
                                    <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield warehousefilter" data-caption="Filter By Warehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-displayfield="WarehouseCode" style="max-width:400px; margin-bottom:15px;"></div>
                                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="All Warehouses" data-datafield="AllWarehouses" style="flex:0 1 150px; margin-top:.5em; margin-left: 1em;"></div>
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="Total" data-enabled="false" data-totalfield="Total" style="flex:0 1 85px"></div>
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="In" data-datafield="In" data-enabled="false" data-totalfield="In" style="flex:0 1 85px;"></div>
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="QC  Req'd" data-datafield="QcRequired" data-enabled="false" data-totalfield="QcRequired" style="flex:0 1 85px;"></div>
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="In Container" data-datafield="InContainer" data-enabled="false" data-totalfield="InContainer" style="flex:0 1 85px;"></div>
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="Staged" data-datafield="Staged" data-enabled="false" data-totalfield="Staged" style="flex:0 1 85px;"></div>
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="Out" data-datafield="Out" data-enabled="false" data-totalfield="Out" style="flex:0 1 85px;"></div>
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="In Repair" data-datafield="InRepair" data-enabled="false" data-totalfield="InRepair" style="flex:0 1 85px;"></div>
                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="In Transit" data-datafield="InTransit" data-enabled="false" data-totalfield="InTransit" style="flex:0 1 85px;"></div>
                                  </div>
                                  <div class="flexrow" style="overflow:auto;">
                                    <div data-control="FwScheduler" class="fwcontrol fwscheduler calendar"></div>
                                  </div>
                                  <div class="flexrow schedulerrow" style="display:block;">
                                    <div data-control="FwSchedulerDetailed" class="fwcontrol fwscheduler realscheduler"></div>
                                    <div class="fwbrowse"><div class="legend"></div></div>
                                  </div>
                                </div>
                              </div>
                            </div>
                                        `);
                    FwControl.renderRuntimeControls($popup.find('.fwcontrol'));
                    $popup = FwPopup.renderPopup($popup, { ismodal: true }, 'Availability');
                    FwPopup.showPopup($popup);
                    $form.data('onscreenunload', () => { FwPopup.destroyPopup($popup); });

                    $popup.find('.close-modal').on('click', function (e) {
                        FwPopup.detachPopup($popup);
                    });

                    const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                    const warehouseText = FwFormField.getTextByDataField($form, 'WarehouseId');
                    FwFormField.setValue2($popup.find('.warehousefilter'), warehouseId, warehouseText);
                    // fields on popup
                    const iCode = $generatedtr.find('[data-browsedatafield="InventoryId"]').attr('data-originaltext');
                    FwFormField.setValue2($popup.find('div[data-datafield="ICode"]'), iCode);
                    const description = FwBrowse.getValueByDataField($control, $generatedtr, 'Description');
                    FwFormField.setValue2($popup.find('div[data-datafield="Description"]'), description);


                    const $calendar = $popup.find('.calendar');
                    FwScheduler.renderRuntimeHtml($calendar);
                    FwScheduler.init($calendar);
                    RentalInventoryController.addCalSchedEvents($generatedtr, $calendar, inventoryId);
                    FwScheduler.loadControl($calendar);
                    const fromDate = FwBrowse.getValueByDataField($control, $generatedtr, 'FromDate');
                    let schedDate;
                    if (fromDate) {
                        schedDate = new Date(fromDate);
                        schedDate = new DayPilot.Date(schedDate);
                    } else {
                        schedDate = FwScheduler.getTodaysDate();
                    }
                    FwScheduler.navigate($calendar, schedDate);
                    FwScheduler.refresh($calendar);
                    // sequence of these invocations is important so that events are properly stored on the control ^ v
                    const $scheduler = $popup.find('.realscheduler');
                    FwSchedulerDetailed.renderRuntimeHtml($scheduler);
                    FwSchedulerDetailed.init($scheduler);
                    RentalInventoryController.addCalSchedEvents($generatedtr, $scheduler, inventoryId);
                    FwSchedulerDetailed.loadControl($scheduler);
                    FwSchedulerDetailed.navigate($scheduler, schedDate, 35);
                    FwSchedulerDetailed.refresh($scheduler);

                    try {
                        if ($scheduler.hasClass('legend-loaded') === false) {
                            FwAppData.apiMethod(true, 'GET', 'api/v1/rentalinventory/availabilitylegend', null, FwServices.defaultTimeout,
                                response => {
                                    for (let key in response) {
                                        FwBrowse.addLegend($popup.find('.schedulerrow .fwbrowse'), key, response[key]);
                                    }
                                    $scheduler.addClass('legend-loaded');
                                }, ex => {
                                    FwFunc.showError(ex);
                                }, $scheduler);
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }

                    $popup.on('change', '.warehousefilter', e => {
                        const whFilter = FwFormField.getValue2($popup.find('.warehousefilter'));
                        $generatedtr.data('warehousefilter', whFilter);
                        FwScheduler.refresh($calendar);
                        FwSchedulerDetailed.refresh($scheduler);
                    });

                    //Toggle All Warehouses
                    $popup.on('change', '[data-datafield="AllWarehouses"]', e => {
                        const $this = jQuery(e.currentTarget);
                        const allWh = FwFormField.getValue2($this);
                        if (allWh == 'T') {
                            FwFormField.disable($popup.find('[data-datafield="WarehouseId"]'));
                        } else {
                            FwFormField.enable($popup.find('[data-datafield="WarehouseId"]'));
                        }
                        $generatedtr.data('allwarehousesfilter', allWh);
                        const $calendar = $popup.find('.calendar');
                        const $realScheduler = $popup.find('.realscheduler');
                        FwSchedulerDetailed.refresh($realScheduler);
                        FwScheduler.refresh($calendar);
                    });
                } else {
                    e.stopPropagation();
                    FwNotification.renderNotification('WARNING', 'Save the row first.');
                }
            });
        }
        // when user clears out one ItemId field within the Used Sale grid, we must also clear out the other ItemId field (BarCode & SerialNumber)
        $generatedtr.find('div[data-browsedatafield="ItemId"]').on('change', evt => {
            const val = $generatedtr.find('.field[data-browsedatafield="ItemId"] input')
                .val()
                .trim();
            if (val === '') {
                FwBrowse.setFieldValue($control, $generatedtr, 'ItemId', { value: '', text: '' });
            }
        });

        $generatedtr.find('div[data-browsedatafield="ItemId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="ItemId"] input').val($tr.find('.field[data-browsedatafield="ItemId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedisplayfield="BarCode"] input.text').val($tr.find('.field[data-browsedatafield="BarCode"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedisplayfield="SerialNumber"] input.text').val($tr.find('.field[data-browsedatafield="SerialNumber"]').attr('data-originalvalue'));

            $generatedtr.find('.field[data-browsedatafield="InventoryId"] input').val($tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="InventoryId"] input.text').val($tr.find('.field[data-browsedatafield="ICode"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
        });

        $generatedtr.find('div[data-browsedatafield="Description"]').data('onchange', $tr => {
            const recType = FwBrowse.getValueByDataField($control, $generatedtr, 'RecType');
            let idFieldName;
            if (recType === 'L' || recType === 'M') {
                idFieldName = 'RateId';
            } else {
                idFieldName = 'InventoryId';
            }
            FwBrowse.setFieldValue($control, $generatedtr, 'InventoryId', { value: FwBrowse.getValueByDataField($control, $tr, idFieldName), text: FwBrowse.getValueByDataField($control, $tr, 'ICode') });
            $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange')($tr);
        });

        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            const recType = FwBrowse.getValueByDataField($control, $generatedtr, 'RecType');
            const description = FwBrowse.getValueByDataField($control, $tr, 'Description');
            FwBrowse.setFieldValue($control, $generatedtr, 'Description', { value: description, text: description });
            FwBrowse.setFieldValue($control, $generatedtr, 'QuantityOrdered', { value: '1', text: '1' });
            FwBrowse.setFieldValue($control, $generatedtr, 'ItemId', { value: '', text: '' });

            if ($generatedtr.hasClass("newmode")) {
                const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                $generatedtr.find('.field[data-browsedatafield="SubQuantity"] input').val("0");
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
                const warehouseCode = FwFormField.getValueByDataField($form, 'WarehouseCode');
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);

                if (controller == 'OrderController' || controller == 'QuoteController' || controller == 'PurchaseOrderController') {
                    //if (recType == 'R' || recType == 'S') {
                    let allowFreeFormText = false;;
                    if (recType == 'L' || recType == 'M') {
                        allowFreeFormText = true;
                    } else {
                        allowFreeFormText = FwBrowse.getValueByDataField($control, $tr, 'Classification') == 'M' ? true : false;
                    }
                    if (allowFreeFormText) {
                        $generatedtr.find('[data-browsedatafield="Description"]').attr({ 'data-browsedatatype': 'text', 'data-formdatatype': 'text' });
                        $generatedtr.find('[data-browsedatafield="Description"] input.value').remove();
                        $generatedtr.find('[data-browsedatafield="Description"] input.text').removeClass('text').addClass('value').off('change');
                        $generatedtr.find('[data-browsedatafield="Description"] .btnpeek').hide();
                        $generatedtr.find('[data-browsedatafield="Description"] .btnvalidate').hide();
                        $generatedtr.find('[data-browsedatafield="Description"] .sk-fading-circle validation-loader').hide();
                    }
                    //}
                }
            }

            if (recType != 'RS' && (controller === 'QuoteController' || controller === 'OrderController')) {
                let inventoryId;
                if (recType === 'R' || recType === 'S') {
                    inventoryId = FwBrowse.getValueByDataField($control, $tr, 'InventoryId');
                } else if (recType === 'M' || recType === 'L') {
                    inventoryId = FwBrowse.getValueByDataField($control, $tr, 'RateId');
                }

                let idFieldName = controller === 'OrderController' ? 'Order' : 'Quote';

                const request: any = {
                    OrderId: FwFormField.getValueByDataField($form, `${idFieldName}Id`),
                    InventoryId: inventoryId,
                    RecType: recType
                };
                FwAppData.apiMethod(true, 'POST', `api/v1/${idFieldName}/getcustomrates/`, request, FwServices.defaultTimeout,
                    response => {
                        populateDefaults($tr, recType, response);
                    }, ex => FwFunc.showError(ex), $form);
            } else {
                populateDefaults($tr, recType);
            }
        });

        const populateDefaults = ($tr: any, recType: string, customRatesResponse?: any) => {
            let customRates;
            if (typeof customRatesResponse != 'undefined') {
                if (customRatesResponse.success && customRatesResponse.HasDiscount) {
                    customRates = customRatesResponse.CustomRates;
                }
            }

            let costFieldName;
            let rateFieldName;
            let week2Rate, week3Rate, week4Rate;
            const rateType = FwFormField.getValueByDataField($form, 'RateType');
            switch (recType) {
                case 'P':
                case 'S':
                    costFieldName = 'DefaultCost';
                    rateFieldName = 'Price';
                    break;
                case 'R':
                case 'M':
                case 'L':
                    if (rateType == 'DAILY') {
                        rateFieldName = 'DailyRate';
                        costFieldName = 'DailyCost';
                    } else if (rateType == 'WEEKLY') {
                        rateFieldName = 'WeeklyRate';
                        costFieldName = 'WeeklyCost';
                    } else if (rateType == '3WEEK') {
                        rateFieldName = 'WeeklyRate';
                        week2Rate = FwBrowse.getValueByDataField($control, $tr, 'Week2Rate');
                        week3Rate = FwBrowse.getValueByDataField($control, $tr, 'Week3Rate');
                        week4Rate = FwBrowse.getValueByDataField($control, $tr, 'Week4Rate');
                        costFieldName = 'WeeklyCost';
                    } else if (rateType == 'MONTHLY') {
                        rateFieldName = 'MonthlyRate';
                        costFieldName = 'MonthlyCost';
                    } else {
                        rateFieldName = 'UseDefault';
                        costFieldName = 'UseDefault';
                    }
                    break;
                case 'RS':
                    costFieldName = 'UnitValue';
                    rateFieldName = 'ReplacementCost';
                    break;
            }

            let rate;
            let cost;
            let laborMiscRateType;
            if (recType === 'L' || recType === 'M') {
                laborMiscRateType = FwBrowse.getValueByDataField($control, $tr, 'RateType');
                if (laborMiscRateType == 'SINGLE') {
                    rate = FwBrowse.getValueByDataField($control, $tr, 'Price');
                    cost = FwBrowse.getValueByDataField($control, $tr, 'AverageCost');
                } else {
                    if (rateFieldName == 'UseDefault') {
                        cost = 0;
                        rate = 0;
                    } else {
                        cost = FwBrowse.getValueByDataField($control, $tr, costFieldName);
                        if (typeof customRates != 'undefined') {
                            if (customRates[rateFieldName]) {
                                rate = customRates[rateFieldName];
                            } else {
                                rate = FwBrowse.getValueByDataField($control, $tr, rateFieldName);
                            }
                        } else {
                            rate = FwBrowse.getValueByDataField($control, $tr, rateFieldName);
                        }
                    }
                }
            } else {
                if (recType != 'R') {
                    cost = FwBrowse.getValueByDataField($control, $tr, costFieldName);
                }
                if (recType == 'R' && rateFieldName == 'UseDefault') {
                    rate = FwBrowse.getValueByDataField($control, $tr, 'UnitValue');
                } else {
                    if (typeof customRates != 'undefined') {
                        if (customRates[rateFieldName]) {
                            rate = customRates[rateFieldName];
                        } else {
                            rate = FwBrowse.getValueByDataField($control, $tr, rateFieldName);
                        }
                    } else {
                        rate = FwBrowse.getValueByDataField($control, $tr, rateFieldName);
                    }
                }
            }

            if (recType != 'R') {
                FwBrowse.setFieldValue($control, $generatedtr, 'UnitCost', { value: cost.toString() });
            }
            FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: rate.toString() });
            if ((recType === 'R') && (rateType == '3WEEK')) {
                FwBrowse.setFieldValue($control, $generatedtr, 'Price2', { value: week2Rate });
                FwBrowse.setFieldValue($control, $generatedtr, 'Price3', { value: week3Rate });
                FwBrowse.setFieldValue($control, $generatedtr, 'Price4', { value: week4Rate });
            }

            // Taxable field
            let taxable = FwBrowse.getValueByDataField($control, $tr, 'Taxable') == 'true' ? 'T' : 'F';
            if (taxable === 'T') {
                const nonTaxable = FwFormField.getValueByDataField($form, 'NonTaxable') ?? false;
                if (nonTaxable) {
                    taxable = 'F';
                }
                else {
                    const rentalTax = FwFormField.getValueByDataField($form, 'RentalTaxRate1');
                    const salesTax = FwFormField.getValueByDataField($form, 'SalesTaxRate1');
                    const laborTax = FwFormField.getValueByDataField($form, 'LaborTaxRate1');
                    const $grid = $control.closest('div[data-control="FwGrid"]');
                    if (($grid.hasClass('R')) && (controller === 'PurchaseOrderController') && (!$grid.hasClass('sub'))) { // Rental Inventory grid on PO
                        if (salesTax === '0') {
                            taxable = 'F';
                        }
                    }
                    else if ($grid.hasClass('R')) { // All other Rental grids
                        if (rentalTax === '0') {
                            taxable = 'F';
                        }
                    } else if ($grid.hasClass('L')) { // Labor grid
                        if (laborTax === '0') {
                            taxable = 'F';
                        }
                    } else { // All other grids
                        if (salesTax === '0') {
                            taxable = 'F';
                        }
                    }
                }
            }
            FwBrowse.setFieldValue($control, $generatedtr, 'Taxable', { value: taxable });

            if (typeof customRatesResponse != 'undefined') {
                if (customRatesResponse.success) {
                    let applyDiscount: boolean = true;
                    if (typeof customRates != 'undefined') {
                        if (customRates[rateFieldName]) {
                            applyDiscount = customRatesResponse.ApplyDiscountToCustomRate;
                        }
                    }

                    if (applyDiscount) {
                        const discountPercent = customRatesResponse.DiscountPercent;
                        FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercentDisplay', { value: discountPercent });
                        FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercent', { value: discountPercent });
                    }


                    const fields: any = ['MarginPercent', 'MarkupPercent', 'DaysPerWeek'];
                    for (let i = 0; i < fields.length; i++) {
                        const value = customRatesResponse[fields[i]];
                        if (value != null) {
                            FwBrowse.setFieldValue($control, $generatedtr, fields[i], { value: value });
                        }
                    }
                }
            }

            calculateExtended('Extended');
        };

        $generatedtr.find('div[data-browsedatafield="FromDate"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="ToDate"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="QuantityOrdered"]').on('change', 'input.value', e => {
            calculateExtended('Extended');
            const itemClass = FwBrowse.getValueByDataField($control, $generatedtr, 'ItemClass');
            if (itemClass == 'K' || itemClass == 'C') {
                this.updateCompleteKitAccessoryRows($control, $generatedtr, e, 'QuantityOrdered');
            }
        });

        $generatedtr.find('div[data-browsedatafield="SubQuantity"]').on('change', 'input.value', e => {
            const itemClass = FwBrowse.getValueByDataField($control, $generatedtr, 'ItemClass');
            if (itemClass == 'K' || itemClass == 'C') {
                this.updateCompleteKitAccessoryRows($control, $generatedtr, e, 'SubQuantity');
            }
        });

        $generatedtr.find('div[data-browsedatafield="UnitCost"]').on('change', 'input.value', function ($tr) {
            calculateMarkupMargin('UnitCost');
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="Price"]').on('change', 'input.value', function ($tr) {
            calculateMarkupMargin('Price');
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="Price2"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="Price3"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="Price4"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="DaysPerWeek"]').on('change', 'input.value', e => {
            calculateExtended('Extended');
            const itemClass = FwBrowse.getValueByDataField($control, $generatedtr, 'ItemClass');
            if (itemClass == 'K' || itemClass == 'C') {
                this.updateCompleteKitAccessoryRows($control, $generatedtr, e, 'DaysPerWeek');
            }
        });
        $generatedtr.find('div[data-browsedatafield="DiscountPercentDisplay"]').on('change', 'input.value', e => {
            const $this = jQuery(e.currentTarget);
            const maxDiscount = parseFloat(FwBrowse.getValueByDataField($control, $generatedtr, 'MaxDiscount'));
            let val: any = $this.val();
            val = parseFloat(val);
            if (val > maxDiscount) {
                $this.val(0);
                $this.select();
                FwNotification.renderNotification("WARNING", 'Discount is greater than Max Discount');
            } else {
                calculateExtended('Extended', 'DiscountPercent');
                const itemClass = FwBrowse.getValueByDataField($control, $generatedtr, 'ItemClass');
                if (itemClass == 'K' || itemClass == 'C') {
                    this.updateCompleteKitAccessoryRows($control, $generatedtr, e, 'DiscountPercentDisplay');
                }
            }
        });
        $generatedtr.find('div[data-browsedatafield="UnitExtended"]').on('change', 'input.value', function ($tr) {
            updatePrice('Discount', 'UnitExtended', 'Unit');
        });
        $generatedtr.find('div[data-browsedatafield="WeeklyExtended"]').on('change', 'input.value', function ($tr) {
            updatePrice('Discount', 'WeeklyExtended', 'Weekly');
        });
        $generatedtr.find('div[data-browsedatafield="MonthlyExtended"]').on('change', 'input.value', function ($tr) {
            updatePrice('Discount', 'MonthlyExtended', 'Monthly');
        });
        $generatedtr.find('div[data-browsedatafield="PeriodExtended"]').on('change', 'input.value', function ($tr) {
            updatePrice('Discount', 'PeriodExtended', 'Period');
        });
        $generatedtr.find('div[data-browsedatafield="UnitDiscountAmount"]').on('change', 'input.value', function ($tr) {
            updatePrice('Discount', 'UnitDiscountAmount', 'Unit');
        });
        $generatedtr.find('div[data-browsedatafield="WeeklyDiscountAmount"]').on('change', 'input.value', function ($tr) {
            updatePrice('Discount', 'WeeklyDiscountAmount', 'Weekly');
        });
        $generatedtr.find('div[data-browsedatafield="MonthlyDiscountAmount"]').on('change', 'input.value', function ($tr) {
            updatePrice('Discount', 'MonthlyDiscountAmount', 'Monthly');
        });
        $generatedtr.find('div[data-browsedatafield="PeriodDiscountAmount"]').on('change', 'input.value', function ($tr) {
            updatePrice('Discount', 'PeriodDiscountAmount', 'Period');
        });

        $generatedtr.find('div[data-browsedatafield="MarkupPercent"]').on('change', 'input.value', $tr => {
            calculateMarkupMargin('MarkupPercent');
        });

        $generatedtr.find('div[data-browsedatafield="MarginPercent"]').on('change', 'input.value', $tr => {
            calculateMarkupMargin('MarginPercent');
        });

        if (controller === 'TemplateController') {
            $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                $generatedtr.find('.field[data-browsedatafield="SubQuantity"] input').val("0");
                const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
                const warehouseCode = FwFormField.getValueByDataField($form, 'WarehouseCode');
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);
                const rateType = FwFormField.getValueByDataField($form, 'RateType');
                if ($generatedtr.hasClass("newmode")) {
                    const inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();
                    FwAppData.apiMethod(true, 'GET', `api/v1/pricing/${inventoryId}/${warehouseId}`, null, FwServices.defaultTimeout,
                        response => {
                            switch (rateType) {
                                case 'DAILY':
                                    FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: response[0].DailyRate });
                                    break;
                                case 'WEEKLY':
                                    FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: response[0].WeeklyRate });
                                    break;
                                case 'MONTHLY':
                                    FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: response[0].MonthlyRate });
                                    break;
                            }
                        }, ex => FwFunc.showError(ex), $form);
                }
            });
        }

        function calculateMarkupMargin(columnChanged) {
            let fieldToCalculate: string;
            let markupPercent: number;
            let marginPercent: number;
            let price: number;
            let cost: number;
            let apiurl = "api/v1/orderitem/calculatemarkupmargin?";

            if (columnChanged == "MarkupPercent") {
                fieldToCalculate = "MarginPercent";
            } else if (columnChanged == "MarginPercent") {
                fieldToCalculate = "MarkupPercent";
            } else if (columnChanged == "UnitCost") {
                fieldToCalculate = "UnitCost";
            } else if (columnChanged == "Price") {
                fieldToCalculate = "Price";
            }
            markupPercent = +(FwBrowse.getValueByDataField($control, $generatedtr, 'MarkupPercent').replace('%', '').trim());
            marginPercent = +(FwBrowse.getValueByDataField($control, $generatedtr, 'MarginPercent').replace('%', '').trim());
            price = +FwBrowse.getValueByDataField($control, $generatedtr, 'Price');
            cost = +FwBrowse.getValueByDataField($control, $generatedtr, 'UnitCost');
            apiurl += `FieldToCalculate=${fieldToCalculate}&MarkupPercent=${markupPercent}&MarginPercent=${marginPercent}&Price=${price}&Cost=${cost}`;

            FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout,
                response => {
                    switch (columnChanged) {
                        case 'MarkupPercent':
                            FwBrowse.setFieldValue($control, $generatedtr, 'MarginPercent', { value: response.MarginPercent });
                            FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: response.Price.toString() });
                            calculateExtended('Extended');
                            break;
                        case 'MarginPercent':
                            FwBrowse.setFieldValue($control, $generatedtr, 'MarkupPercent', { value: response.MarkupPercent });
                            FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: response.Price.toString() });
                            calculateExtended('Extended');
                            break;
                        case 'UnitCost':
                            FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: response.Price.toString() });
                            calculateExtended('Extended');
                            break;
                        case 'Price':
                            FwBrowse.setFieldValue($control, $generatedtr, 'MarkupPercent', { value: response.MarkupPercent });
                            FwBrowse.setFieldValue($control, $generatedtr, 'MarginPercent', { value: response.MarginPercent });
                            break;
                    }
                }, ex => FwFunc.showError(ex), null);
        }

        function updatePrice(type, field?, periodType?) {
            let rate: number = +FwBrowse.getValueByDataField($control, $generatedtr, 'Price');
            const extendedVal: number = +FwBrowse.getValueByDataField($control, $generatedtr, `${periodType}Extended`);
            if (rate == 0) {
                if (extendedVal == 0) {
                    FwNotification.renderNotification('WARNING', 'Unable to apply discount because Price and Extended values are 0.');
                    return;
                } else {
                    let discount;
                    const quantity: number = +FwBrowse.getValueByDataField($control, $generatedtr, 'QuantityOrdered');
                    const discountPercent: number = +FwBrowse.getValueByDataField($control, $generatedtr, 'DiscountPercent');
                    const billablePeriods: number = +FwBrowse.getValueByDataField($control, $generatedtr, 'BillablePeriods');
                    const discountAmount: number = +FwBrowse.getValueByDataField($control, $generatedtr, `${periodType}DiscountAmount`);

                    if (discountPercent == 0) {
                        if (field === 'UnitExtended') {
                            rate = extendedVal + discountAmount;
                        } else if (field === 'WeeklyExtended') {
                            if (quantity == 0) {
                                FwNotification.renderNotification('ERROR', 'Quantity must be greater than 0.');
                                return;
                            } else {
                                rate = ((extendedVal + discountAmount) / quantity);
                            }
                        } else if (field === 'PeriodExtended') {
                            if (quantity == 0 || billablePeriods == 0) {
                                FwNotification.renderNotification('ERROR', 'Either Quantity or Billable Periods must be greater than 0.')
                                return;
                            } else {
                                rate = ((extendedVal + discountAmount) / (quantity * billablePeriods));
                            }
                        } else {
                            FwNotification.renderNotification('ERROR', `Unknown field: ${field}`);
                            return;
                        }
                    } else {
                        discount = 1 - (discountPercent / 100);
                        if (discount === 0) {
                            FwNotification.renderNotification('ERROR', `Discount percent must be less than 100.`);
                            return;
                        } else {
                            if (field === 'UnitExtended') {
                                rate = (extendedVal / discount);
                            } else if (field === 'WeeklyExtended') {
                                if (quantity == 0) {
                                    FwNotification.renderNotification('ERROR', 'Quantity must be greater than 0.')
                                    return;
                                } else {
                                    rate = ((extendedVal / quantity) / discount);
                                }
                            } else if (field === 'PeriodExtended') {
                                if (quantity == 0 || billablePeriods == 0) {
                                    FwNotification.renderNotification('ERROR', 'Either Quantity or Billable Periods must be greater than 0.')
                                    return;
                                } else {
                                    rate = ((extendedVal / (quantity * billablePeriods)) / discount);
                                }
                            } else {
                                FwNotification.renderNotification('ERROR', `Unknown field: ${field}`);
                                return;
                            }
                        }
                    }
                    let rateDisplay = Number(rate.toFixed(2)).toLocaleString();
                    const rateLength = rateDisplay.length;
                    const decimalIndex = rateDisplay.indexOf('.');
                    if (rateLength - decimalIndex > rateLength) {
                        rateDisplay = `${rateDisplay}.00`;
                    } else if (rateLength - decimalIndex == 2) {
                        rateDisplay = `${rateDisplay}0`;
                    }
                    const $confirmation = FwConfirmation.renderConfirmation(`Update Rate`, `Update Rate to ${rateDisplay}?`);
                    const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                    const $no = FwConfirmation.addButton($confirmation, 'No', false);

                    $yes.on('click', () => {
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.setFieldValue($control, $generatedtr, 'Price', { value: rate.toString() });
                        calculateExtended(type, field);
                    });

                    $no.on('click', () => {
                        FwConfirmation.destroyConfirmation($confirmation);
                        const originalVal = $generatedtr.find(`[data-browsedatafield="${field}"]`).attr('data-originalvalue');
                        FwBrowse.setFieldValue($control, $generatedtr, field, { value: originalVal });
                    });
                }
            } else {
                calculateExtended(type, field);
            }
        }

        function calculateExtended(type, field?) {
            let rateType, recType, fromDate, toDate, quantity, rate, rate2, rate3, rate4, unitCost, daysPerWeek, discountPercent, weeklyExtended, unitExtended, periodExtended,
                monthlyExtended, unitDiscountAmount, weeklyDiscountAmount, monthlyDiscountAmount, periodDiscountAmount;
            rateType = FwFormField.getValueByDataField($form, 'RateType');
            recType = FwBrowse.getValueByDataField($control, $generatedtr, 'RecType');
            fromDate = FwBrowse.getValueByDataField($control, $generatedtr, 'FromDate');
            toDate = FwBrowse.getValueByDataField($control, $generatedtr, 'ToDate');
            quantity = FwBrowse.getValueByDataField($control, $generatedtr, 'QuantityOrdered');
            rate = FwBrowse.getValueByDataField($control, $generatedtr, 'Price');
            rate2 = FwBrowse.getValueByDataField($control, $generatedtr, 'Price2');
            rate3 = FwBrowse.getValueByDataField($control, $generatedtr, 'Price3');
            rate4 = FwBrowse.getValueByDataField($control, $generatedtr, 'Price4');
            unitCost = FwBrowse.getValueByDataField($control, $generatedtr, 'UnitCost');
            daysPerWeek = FwBrowse.getValueByDataField($control, $generatedtr, 'DaysPerWeek');
            if (field == "DiscountPercent") {
                discountPercent = FwBrowse.getValueByDataField($control, $generatedtr, 'DiscountPercentDisplay');
            }
            else {
                discountPercent = FwBrowse.getValueByDataField($control, $generatedtr, 'DiscountPercent');
            }
            weeklyExtended = FwBrowse.getValueByDataField($control, $generatedtr, 'WeeklyExtended');
            unitExtended = FwBrowse.getValueByDataField($control, $generatedtr, 'UnitExtended');
            periodExtended = FwBrowse.getValueByDataField($control, $generatedtr, 'PeriodExtended');
            //monthlyExtended = $generatedtr.find('.field[data-browsedatafield="MonthlyExtended"] input').val();
            unitDiscountAmount = FwBrowse.getValueByDataField($control, $generatedtr, 'UnitDiscountAmount');
            weeklyDiscountAmount = FwBrowse.getValueByDataField($control, $generatedtr, 'WeeklyDiscountAmount');
            //monthlyDiscountAmount = $generatedtr.find('.field[data-browsedatafield="MonthlyDiscountAmount"] input').val();
            periodDiscountAmount = FwBrowse.getValueByDataField($control, $generatedtr, 'PeriodDiscountAmount');

            let apiurl = "api/v1/orderitem/"

            if (type == "Extended") {
                apiurl += "calculateextended?";
            } else if (type == "Discount") {
                apiurl += "calculatediscountpercent?";
            }
            apiurl += `RateType=${rateType}&RecType=${recType}&FromDate=${fromDate}&ToDate=${toDate}&Quantity=${quantity}&Rate=${rate}&Rate2=${rate2}&Rate3=${rate3}&Rate4=${rate4}&UnitCost=${unitCost}&DaysPerWeek=${daysPerWeek}`;

            if (type == 'Extended') {
                apiurl += `&DiscountPercent=${discountPercent}`;

                FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercent', { value: (response.DiscountPercent || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'PeriodCostExtended', { value: (response.CostExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'UnitExtended', { value: (response.UnitExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'UnitDiscountAmount', { value: (response.UnitDiscountAmount || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'WeeklyExtended', { value: (response.WeeklyExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'WeeklyDiscountAmount', { value: (response.WeeklyDiscountAmount || 0).toString() });
                        //FwBrowse.setFieldValue($control, $generatedtr, 'MonthlyExtended', { value: response.MonthlyExtended });
                        //FwBrowse.setFieldValue($control, $generatedtr, 'MonthlyDiscountAmount', { value: response.MonthlyDiscountAmount });
                        FwBrowse.setFieldValue($control, $generatedtr, 'PeriodExtended', { value: (response.PeriodExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'PeriodDiscountAmount', { value: (response.PeriodDiscountAmount || 0).toString() });
                    }, ex => FwFunc.showError(ex), null);
            }

            if (type == 'Discount') {
                switch (field) {
                    case 'UnitExtended':
                        apiurl += `&UnitExtended=${unitExtended}`;
                        break;
                    case 'WeeklyExtended':
                        apiurl += `&WeeklyExtended=${weeklyExtended}`;
                        break;
                    case 'MonthlyExtended':
                        apiurl += `&MonthlyExtended=${monthlyExtended}`;
                        break;
                    case 'PeriodExtended':
                        apiurl += `&PeriodExtended=${periodExtended}`;
                        break;
                    case 'UnitDiscountAmount':
                        apiurl += `&UnitDiscountAmount=${unitDiscountAmount}`;
                        break;
                    case 'WeeklyDiscountAmount':
                        apiurl += `&WeeklyDiscountAmount=${weeklyDiscountAmount}`;
                        break;
                    case 'MonthlyDiscountAmount':
                        apiurl += `&MonthlyDiscountAmount=${monthlyDiscountAmount}`;
                        break;
                    case 'PeriodDiscountAmount':
                        apiurl += `&PeriodDiscountAmount=${periodDiscountAmount}`;
                        break;
                }
                FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout,
                    response => {
                        FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercent', { value: (response.DiscountPercent || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'DiscountPercentDisplay', { value: (response.DiscountPercent || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'UnitExtended', { value: (response.UnitExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'UnitDiscountAmount', { value: (response.UnitDiscountAmount || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'WeeklyExtended', { value: (response.WeeklyExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'WeeklyDiscountAmount', { value: (response.WeeklyDiscountAmount || 0).toString() });
                        //FwBrowse.setFieldValue($control, $generatedtr, 'MonthlyExtended', { value: response.MonthlyExtended });
                        //FwBrowse.setFieldValue($control, $generatedtr, 'MonthlyDiscountAmount', { value: response.MonthlyDiscountAmount });
                        FwBrowse.setFieldValue($control, $generatedtr, 'PeriodExtended', { value: (response.PeriodExtended || 0).toString() });
                        FwBrowse.setFieldValue($control, $generatedtr, 'PeriodDiscountAmount', { value: (response.PeriodDiscountAmount || 0).toString() });
                    }, ex => FwFunc.showError(ex), null);
            }

            //justin hoffman - moved to API layer
            //const unitCost = +FwBrowse.getValueByDataField($control, $generatedtr, 'UnitCost');
            //const costExtended = unitCost * (+quantity);
            //FwBrowse.setFieldValue($control, $generatedtr, 'PeriodCostExtended', { value: costExtended.toLocaleString(), text: costExtended.toLocaleString() })
        }
    };
    //----------------------------------------------------------------------------------------------
    updateCompleteKitAccessoryRows($grid: JQuery, $tr: JQuery, event: any, field: string) {
        const index = jQuery(event.currentTarget).parents('tr').index();
        const id = FwBrowse.getValueByDataField($grid, $tr, 'OrderItemId');
        const completeKitAccClasses: any = ['KI', 'KO', 'CI', 'CO'];
        const rowCount = FwBrowse.getRowCount($grid);
        for (let i = index + 1; i < rowCount; i++) {
            const $nextRow = FwBrowse.selectRowByIndex($grid, i);
            const nextRowClass = FwBrowse.getValueByDataField($grid, $nextRow, 'ItemClass');
            const parentId = FwBrowse.getValueByDataField($grid, $nextRow, 'ParentId');
            if (completeKitAccClasses.includes(nextRowClass) && id == parentId) {
                let newValue: any;
                if (field == 'DaysPerWeek' || field == 'DiscountPercentDisplay') {
                    const isLocked = FwBrowse.getValueByDataField($grid, $nextRow, 'Locked');
                    //if (isLocked == 'true') {
                    //    return;
                    //} else {
                    //    newValue = jQuery(event.currentTarget).val();
                    //}
                    //justin hoffman 05/19/2020.  If locked, just skip this row and continue to the next row
                    if (isLocked !== 'true') {
                        newValue = jQuery(event.currentTarget).val();
                    }
                } else if (field == 'QuantityOrdered' || field == 'SubQuantity') {
                    const accessoryRatio = parseFloat(FwBrowse.getValueByDataField($grid, $nextRow, 'AccessoryRatio'));
                    const parentValue = Number(jQuery(event.currentTarget).val());
                    newValue = Math.round(parentValue / accessoryRatio).toString();
                }

                if (!$nextRow.hasClass('editmode')) {
                    FwBrowse.setRowEditMode($grid, $nextRow);
                }
                FwBrowse.setFieldValue($grid, $nextRow, field, { value: newValue });

                if (field == 'DiscountPercentDisplay') {
                    FwBrowse.setFieldValue($grid, $tr, 'DiscountPercent', { value: newValue }); //updates Parent Item's value 
                    FwBrowse.setFieldValue($grid, $nextRow, 'DiscountPercent', { value: newValue });
                }
            } else {
                return;
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    afterRowEditMode($grid: JQuery, $tr: JQuery) {
        $tr.find('.divcancelsaverow').on('click', e => {
            const itemClass = FwBrowse.getValueByDataField($grid, $tr, 'ItemClass');
            if (itemClass == 'K' || itemClass == 'C') {
                const index = $tr.index();
                const id = FwBrowse.getValueByDataField($grid, $tr, 'OrderItemId');
                const completeKitAccClasses: any = ['KI', 'KO', 'CI', 'CO'];
                const rowCount = FwBrowse.getRowCount($grid);
                for (let i = index + 1; i < rowCount; i++) {
                    const $nextRow = FwBrowse.selectRowByIndex($grid, i);
                    const nextRowClass = FwBrowse.getValueByDataField($grid, $nextRow, 'ItemClass');
                    const parentId = FwBrowse.getValueByDataField($grid, $nextRow, 'ParentId');
                    if (completeKitAccClasses.includes(nextRowClass) && id == parentId) {
                        FwBrowse.cancelEditMode($grid, $nextRow);
                        $nextRow.find('.browsecontextmenucell').show();
                    } else {
                        break;
                    }
                }
                const rowsInEditMode = $grid.find('.editmode').length;
                const $gridmenu = $grid.find('[data-control="FwMenu"]');
                if (rowsInEditMode == 0) $gridmenu.find('.grid-multi-save').hide();
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    //toggleOrderItemView($form: any, event: any, module) {
    //    // Toggle between Detail and Summary view in all OrderItemGrid
    //    let $element, $orderItemGrid, isSummary, orderId, isSubGrid;
    //    $element = jQuery(event.currentTarget);
    //    isSubGrid = $element.closest('[data-grid="OrderItemGrid"]').attr('data-issubgrid');
    //    orderId = FwFormField.getValueByDataField($form, `${module}Id`);
    //    //const totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]

    //    $orderItemGrid = $element.closest('[data-name="OrderItemGrid"]');

    //    if ($orderItemGrid.data('isSummary') === false) {
    //        isSummary = true;
    //        $orderItemGrid.data('isSummary', true);
    //        $element.children().text('Detail View')
    //    }
    //    else {
    //        isSummary = false;
    //        $orderItemGrid.data('isSummary', false);
    //        $element.children().text('Summary View')
    //    }

    //    $orderItemGrid.data('ondatabind', request => {
    //        request.uniqueids = {
    //            OrderId: orderId,
    //            Summary: isSummary,
    //        }
    //        request.orderby = "RowNumber,RecTypeDisplay"
    //        request.totalfields = totalFields;
    //        if (isSubGrid === "true") {
    //            request.uniqueids.Subs = true;
    //        }
    //    });

    //    FwBrowse.search($orderItemGrid);
    //};
    //----------------------------------------------------------------------------------------------
    colorLegend(event: any) {
        const html = `
                <div id="previewHtml">
                    <div style="margin-bottom:10px; font-weight:bold;">Availability</div>
                    <div class="flexrow" style="margin-bottom:5px"><div data-browsedatafield="QuantityAvailable" data-state="enough"><div class="fieldvalue">32</div></div><div>Enough Available</div></div>
                    <div class="flexrow" style="margin-bottom:5px"><div data-browsedatafield="QuantityAvailable" data-state="negative"><div class="fieldvalue">-7</div></div><div>Inventory Shortage</div></div>
                    <div class="flexrow" style="margin-bottom:5px"><div data-browsedatafield="QuantityAvailable" data-state="low"><div class="fieldvalue">4</div></div><div>Low Availability</div></div>
                    <div style="margin-bottom:10px; margin-top: 1em; font-weight:bold;">Quantity</div>
                    <div class="flexrow" style="margin-bottom:5px"><div data-browsedatafield="QuantityAvailable"><div class="fieldvalue" style="background-color:#ff6f6f; height:.8em; margin-top:1em;"></div></div><div>Quantity modified at</br> Staging/Check-Out</div></div>
                    <div style="margin-bottom:10px; margin-top: 1em; font-weight:bold;">Sub-Quantity</div>
                    <div class="flexrow" style="margin-bottom:5px"><div data-browsedatafield="QuantityAvailable"><div class="fieldvalue" style="background-color:#ffe5b4; height:.8em;"></div></div><div>Sub Purchase Order created</div></div>
                </div>`;

        const $confirmation = FwConfirmation.renderConfirmation(`Color Legend`, html);
        const containerCSS = {
            'box-sizing': 'border-box'
            , 'height': '100%'
            , 'line-height': '100%'
            , 'white-space': 'nowrap'
            , 'text-overflow': 'ellipsis'
            , 'vertical-align': 'middle'
            , 'display': 'flex'
            , 'align-items': 'center'
            , 'font-size': '.8em'
            , 'max-width': '65px'
        }
        $confirmation.find('[data-browsedatafield="QuantityAvailable"]').css(containerCSS);
        const $close = FwConfirmation.addButton($confirmation, 'Close', false);

        $close.on('click', () => {
            FwConfirmation.destroyConfirmation($confirmation);
        });
    }
    //----------------------------------------------------------------------------------------------
    async getSelectedItemIds(event): Promise<any> {
        const items = [];

        const $browse = jQuery(event.currentTarget).closest('.fwbrowse');
        const orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        const orderItemId = $browse.find('.selected [data-browsedatafield="OrderItemId"]').attr('data-originalvalue');
        const $selectedCheckBoxes = $browse.find('tbody .cbselectrow:checked');

        if (orderId != null) {
            let orderItem: any = {};
            orderItem.OrderItemId = orderItemId
            orderItem.OrderId = orderId;
            items.push(orderItem);
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                let orderItem: any = {};
                let orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                orderItem.OrderItemId = orderItemId
                orderItem.OrderId = orderId;
                items.push(orderItem);
            }
        } else {
            FwNotification.renderNotification('WARNING', 'Select a record.')
        }
        return items;
    }
    //----------------------------------------------------------------------------------------------
    async insertHeaderLines(event) {
        const $browse = jQuery(event.currentTarget).closest('.fwbrowse');

        const items = await this.getSelectedItemIds(event);
        if (items.length > 0) {
            await insertHeaderItems(items);
            await jQuery(document).trigger('click');
        }

        function insertHeaderItems(items): void {

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/insertheaders`, items, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        };
    }
    //----------------------------------------------------------------------------------------------   
    async insertTextLines(event) {
        const $browse = jQuery(event.currentTarget).closest('.fwbrowse');

        const items = await this.getSelectedItemIds(event);
        if (items.length > 0) {
            await insertTextItems(items);
            await jQuery(document).trigger('click');
        }

        function insertTextItems(items): void {

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/inserttexts`, items, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        };
    }
    //----------------------------------------------------------------------------------------------
    async insertSubTotalLines(event) {
        const $browse = jQuery(event.currentTarget).closest('.fwbrowse');

        const items = await this.getSelectedItemIds(event);
        if (items.length > 0) {
            await insertSubTotalItems(items);
            await jQuery(document).trigger('click');
        }

        function insertSubTotalItems(items): void {

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/insertsubtotals`, items, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        };
    }
    //----------------------------------------------------------------------------------------------    
    async detailSummaryView(event) {
        const $orderItemGrid: any = jQuery(event.currentTarget).closest('[data-name="OrderItemGrid"]');

        let summary: boolean = $orderItemGrid.data('Summary');
        summary = !summary;
        $orderItemGrid.data('Summary', summary);
        const $element = jQuery(event.currentTarget);
        $element.children().text(summary ? 'Detail View' : 'Summary View');

        const onDataBind = $orderItemGrid.data('ondatabind');
        if (typeof onDataBind == 'function') {
            $orderItemGrid.data('ondatabind', function (request) {
                onDataBind(request);
                request.uniqueids.Summary = summary;
            });
        }
        await FwBrowse.search($orderItemGrid);
        await jQuery(document).trigger('click');
    }
    //----------------------------------------------------------------------------------------------
    copyLineItems(event: any) {
        const $form = jQuery(event.currentTarget).closest('.fwform');
        const module = $form.attr('data-controller').replace('Controller', '');
        const orderId = FwFormField.getValueByDataField($form, `${module}Id`);
        const ids = [];
        const $grid = jQuery(event.currentTarget).closest('.fwbrowse');
        const $selectedCheckBoxes = $grid.find('tbody .cbselectrow:checked');
        if ($selectedCheckBoxes.length) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                const $this = jQuery($selectedCheckBoxes[i]);
                const id = $this.closest('tr').find('div[data-browsedatafield="OrderItemId"]').attr('data-originalvalue');
                ids.push(id);
            };
            const request: any = {};
            request.OrderId = orderId;
            request.OrderItemIds = ids;
            FwAppData.apiMethod(true, 'POST', `api/v1/order/copyorderitems`, request, FwServices.defaultTimeout,
                response => {
                    jQuery(document).trigger('click');
                    FwBrowse.databind($grid);
                },
                ex => FwFunc.showError(ex), $grid);
        } else {
            FwNotification.renderNotification('WARNING', 'Select a record.')
        }
    }
    //----------------------------------------------------------------------------------------------
    SubPOWorksheet(event: any) {
        try {
            const $grid = jQuery(event.currentTarget).parents('[data-control="FwGrid"]');
            const subWorksheetData: any = {};
            if ($grid.hasClass('A')) {
                subWorksheetData.RecType = ''
            } else if ($grid.hasClass('R')) {
                subWorksheetData.RecType = 'R'
            } else if ($grid.hasClass('S')) {
                subWorksheetData.RecType = 'S'
            } else if ($grid.hasClass('M')) {
                subWorksheetData.RecType = 'M'
            } else if ($grid.hasClass('L')) {
                subWorksheetData.RecType = 'L'
            } else if ($grid.hasClass('RS')) {
                subWorksheetData.RecType = 'RS'
            }
            const $form = jQuery(event.currentTarget).closest('.fwform');
            const office = JSON.parse(sessionStorage.getItem('location'));
            subWorksheetData.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            subWorksheetData.RateType = FwFormField.getValueByDataField($form, 'RateType');
            subWorksheetData.CurrencyId = FwFormField.getValueByDataField($form, 'CurrencyId') || office.defaultcurrencyid;;
            subWorksheetData.CurrencyCode = FwFormField.getTextByDataField($form, 'CurrencyId') || office.defaultcurrencycode;
            subWorksheetData.BillingCycleId = FwFormField.getValueByDataField($form, 'BillingCycleId');
            subWorksheetData.BillingCycle = FwFormField.getTextByDataField($form, 'BillingCycleId');
            subWorksheetData.EstimatedStartDate = FwFormField.getValue($form, 'div[data-dateactivitytype="START"]');
            subWorksheetData.EstimatedStopDate = FwFormField.getValue($form, 'div[data-dateactivitytype="STOP"]');
            subWorksheetData.EstimatedStartTime = FwFormField.getValue($form, 'div[data-timeactivitytype="START"]');
            const $subWorksheetForm = SubWorksheetController.openForm('EDIT', subWorksheetData);
            FwModule.openSubModuleTab($form, $subWorksheetForm);
            const $tab = FwTabs.getTabByElement($subWorksheetForm);
            $tab.find('.caption').html('Sub Worksheet');
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    copyTemplate(event: any) {
        const $form = jQuery(event.currentTarget).closest('.fwform');
        const $grid = jQuery(event.currentTarget).closest('[data-name="OrderItemGrid"]');
        let recType, activity;
        recType = jQuery(event.currentTarget).closest('[data-grid="OrderItemGrid"]');
        if (recType.hasClass('R')) {
            recType = 'R';
            activity = 'Rental';
        } else if (recType.hasClass('S')) {
            recType = 'S';
            activity = 'Sales';
        } else if (recType.hasClass('L')) {
            recType = 'L';
            activity = 'Labor';
        } else if (recType.hasClass('M')) {
            recType = 'M';
            activity = 'Miscellaneous';
        } else if (recType.hasClass('P')) {
            recType = 'P';
        } else if (recType.hasClass('A')) {
            recType = '';
        } else if (recType.hasClass('RS')) {
            recType = 'RS'
        }
        let module = $form.attr('data-controller').replace('Controller', '');
        let moduleDisplayName: string = "";
        if (module === 'Quote') {
            moduleDisplayName = "Quote";
        }
        else if (module === 'Order') {
            moduleDisplayName = "Order";
        }
        else if (module === 'PurchaseOrder') {
            moduleDisplayName = "Purchase Order";
        }

        const HTML: Array<string> = [];
        HTML.push(
            `<div class="fwcontrol fwcontainer fwform popup template-popup" data-control="FwContainer" data-type="form">
              <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                <div class="tabpages">
                  <div class="formpage">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section">
                      <div class="formrow">
                        <div class="formcolumn" style="width:100%;margin-top:5px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div class="fwform-section-title" style="margin-bottom:10px;">Copy Template to ${moduleDisplayName}</div>
                            <div data-control="FwGrid" class="container"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formrow add-button">
                        <div class="add-items fwformcontrol" data-type="button" style="float:right;">Add to ${moduleDisplayName}</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`
        );

        const addTemplateBrowse = () => {
            let $browse = FwBrowse.loadBrowseFromTemplate('Template');
            $browse.attr('data-hasmultirowselect', 'true');
            $browse = FwModule.openBrowse($browse);
            $browse.find('.fwbrowse-menu').hide();

            $browse.data('ondatabind', function (request) {
                request.pagesize = 20;
                request.orderby = "Description asc";
            });
            return $browse;
        }
        const $popupHtml = HTML.join('');
        const $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwPopup.showPopup($popup);
        const $templateBrowse = addTemplateBrowse();
        $popup.find('.container').append($templateBrowse);
        const $templatePopup = $popup.find('.template-popup');

        $popup.find('.close-modal').one('click', e => {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });

        $popup.on('click', '.add-items', e => {
            const $selectedCheckBoxes = $popup.find('[data-control="FwGrid"] tbody .cbselectrow:checked');
            const templateIds: Array<string> = [];

            const activities: any = { "Rental": false, "Sales": false, "Miscellaneous": false, "Labor": false, "Facilities": false, };
            delete activities[activity];

            let showPrompt = false;
            const additionalActivities = [];
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                const $this = jQuery($selectedCheckBoxes[i]);
                for (let key in activities) {
                    if (activities[key] !== true) { // prevent iteration over an already true value
                        const hasItem = $this.closest('tr').find(`div[data-browsedatafield="Has${key}Item"]`).attr('data-originalvalue');
                        if (hasItem === 'true') {
                            activities[key] = true;
                            showPrompt = true;
                            additionalActivities.push(key);
                        }
                    }
                }

                const id = $this.closest('tr').find('div[data-browsedatafield="TemplateId"]').attr('data-originalvalue');
                templateIds.push(id);
            };

            const request: any = {
                TemplateIds: templateIds,
                RecType: recType,
                OrderId: FwFormField.getValueByDataField($form, `${module}Id`),
            }
            async function sendTemplate(request) {
                FwAppData.apiMethod(true, 'POST', `api/v1/order/copytemplate`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('.close-modal').click();
                    FwBrowse.databind($grid);
                }, null, $templatePopup);
            }
            if (showPrompt) {
                const $confirmation = FwConfirmation.renderConfirmation(`Multiple Activity Types`, ``);
                $confirmation.find('.fwconfirmationbox').css({
                    'width': '465px',
                });

                FwConfirmation.addControls($confirmation, `<div style="text-align:center;"></div><div style="margin:10px 0 0 0;text-align:center;">You are adding a template from ${activity} but there ${additionalActivities.length > 1 ? 'are' : 'is an'} additional activit${additionalActivities.length > 1 ? 'ies' : 'y'} of ${additionalActivities.join(', ')} in the selected template${$selectedCheckBoxes.length > 1 ? 's' : ''}. Do you wish to just add ${activity} or include all?<div>`);

                const $just = FwConfirmation.addButton($confirmation, `Just ${activity}`, false);
                const $all = FwConfirmation.addButton($confirmation, 'All Activities', false);
                const $cancel = FwConfirmation.addButton($confirmation, 'Cancel', false);

                $just.on('click', e => {
                    FwConfirmation.destroyConfirmation($confirmation);
                    sendTemplate(request);
                });

                $all.on('click', e => {
                    FwConfirmation.destroyConfirmation($confirmation);
                    request.RecType = '';
                    sendTemplate(request);
                });

                $cancel.on('click', e => {
                    FwConfirmation.destroyConfirmation($confirmation);
                });
            } else {
                sendTemplate(request);
            }
        });

        FwBrowse.search($templateBrowse);
    }
    //----------------------------------------------------------------------------------------------
    async muteUnmute(event: any) {
        const $browse = jQuery(event.currentTarget).closest('.fwbrowse');
        const mutedItems = [];
        const $selectedCheckBoxes = $browse.find('tbody .cbselectrow:checked');

        if ($selectedCheckBoxes.length) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                const orderItem: any = {};
                orderItem.OrderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                orderItem.OrderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                orderItem.Description = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Description"]').attr('data-originalvalue');
                orderItem.QuantityOrdered = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="QuantityOrdered"]').attr('data-originalvalue');
                orderItem.RecType = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RecType"]').attr('data-originalvalue');
                orderItem.RowsRolledUp = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RowsRolledUp"]').attr('data-originalvalue');

                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Mute"]').attr('data-originalvalue') === 'true') {
                    orderItem.Mute = false;
                } else {
                    orderItem.Mute = true;
                }
                mutedItems.push(orderItem);
            }
            await muteUnmuteItems(mutedItems);
            await jQuery(document).trigger('click');
        } else {
            FwNotification.renderNotification('WARNING', 'Select a record.')
        }

        function muteUnmuteItems(orders): void {
            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/many`, orders, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.search($browse);
            }, $browse);
        };
    }
    //----------------------------------------------------------------------------------------------
    async boldUnbold(event) {
        const $browse = jQuery(event.currentTarget).closest('.fwbrowse');
        const boldItems = [];
        const $selectedCheckBoxes = $browse.find('tbody .cbselectrow:checked');

        if ($selectedCheckBoxes.length) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                const orderItem: any = {};
                orderItem.OrderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                orderItem.OrderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                orderItem.Description = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Description"]').attr('data-originalvalue');
                orderItem.QuantityOrdered = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="QuantityOrdered"]').attr('data-originalvalue');
                orderItem.RecType = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RecType"]').attr('data-originalvalue');
                orderItem.RowsRolledUp = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RowsRolledUp"]').attr('data-originalvalue');

                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Bold"]').attr('data-originalvalue') === 'true') {
                    orderItem.Bold = false;
                } else {
                    orderItem.Bold = true;
                }
                boldItems.push(orderItem);
            }
            await boldUnboldItem(boldItems);
            await jQuery(document).trigger('click');
        } else {
            FwNotification.renderNotification('WARNING', 'Select a record.')
        }

        function boldUnboldItem(orders): void {

            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/many`, orders, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.search($browse);
            }, $browse);
        };
    }
    //----------------------------------------------------------------------------------------------
    async lockUnlock(event: any) {
        const $browse = jQuery(event.currentTarget).closest('.fwbrowse');
        const lockedItems = [];
        const $selectedCheckBoxes = $browse.find('tbody .cbselectrow:checked');

        if ($selectedCheckBoxes.length) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                const orderItem: any = {};
                orderItem.OrderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                orderItem.OrderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                orderItem.Description = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Description"]').attr('data-originalvalue');
                orderItem.QuantityOrdered = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="QuantityOrdered"]').attr('data-originalvalue');
                orderItem.RecType = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RecType"]').attr('data-originalvalue');
                orderItem.RowsRolledUp = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RowsRolledUp"]').attr('data-originalvalue');

                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Locked"]').attr('data-originalvalue') === 'true') {
                    orderItem.Locked = false;
                } else {
                    orderItem.Locked = true;
                }
                lockedItems.push(orderItem);
            }
            await lockUnlockItem(lockedItems);
            await jQuery(document).trigger('click');
        } else {
            FwNotification.renderNotification('WARNING', 'Select a record.')
        }

        function lockUnlockItem(orders): void {
            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/many`, orders, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.search($browse);
            }, $browse);
        };
    }
    //---------------------------------------------------------------------------------
    async shortagesOnly(event: any) {
        const $orderItemGrid: any = jQuery(event.currentTarget).closest('[data-name="OrderItemGrid"]');
        let shortages: boolean = $orderItemGrid.data('Shortages');
        shortages = !shortages;
        $orderItemGrid.data('Shortages', shortages);
        const $element = jQuery(event.currentTarget);
        $element.children().text(shortages ? 'All Items (not Shortages Only)' : 'Shortages Only');

        const onDataBind = $orderItemGrid.data('ondatabind');
        if (typeof onDataBind == 'function') {
            $orderItemGrid.data('ondatabind', function (request) {
                onDataBind(request);
                request.uniqueids.ShortagesOnly = shortages;
            });
        }

        await FwBrowse.search($orderItemGrid);
        await jQuery(document).trigger('click');
    }
    //---------------------------------------------------------------------------------
    async rollup(event: any) {
        const $orderItemGrid: any = jQuery(event.currentTarget).closest('[data-name="OrderItemGrid"]');
        let rollup: boolean = $orderItemGrid.data('Rollup');
        rollup = !rollup;
        $orderItemGrid.data('Rollup', rollup);
        const $element = jQuery(event.currentTarget);
        $element.children().text(rollup ? 'Show Split Details' : 'Roll-up Quantities');

        const onDataBind = $orderItemGrid.data('ondatabind');
        if (typeof onDataBind == 'function') {
            $orderItemGrid.data('ondatabind', function (request) {
                onDataBind(request);
                request.uniqueids.Rollup = rollup;
            });
        }

        await FwBrowse.search($orderItemGrid);
        await jQuery(document).trigger('click');
    }
    //----------------------------------------------------------------------------------------------
    async restoreSystemSorting(event: any) {
        const $browse = jQuery(event.currentTarget).closest('.fwbrowse');
        const $form = jQuery(event.currentTarget).closest('.fwform');

        await restoreSorting(FwFormField.getValue2($form.find('[data-type="key"]')));
        await jQuery(document).trigger('click');

        function restoreSorting(orderId): void {
            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/cancelmanualsort/${orderId}`, null, FwServices.defaultTimeout,
                response => {
                    FwBrowse.search($browse);
                    $form.data('ismanualsort', false);
                }, ex => FwFunc.showError(ex), $browse);
        };
    }
    //----------------------------------------------------------------------------------------------
    quikSearch(event) {
        const grid = jQuery(event.currentTarget).parents('[data-control="FwGrid"]');
        let gridInventoryType;
        if (grid.hasClass('R')) {
            gridInventoryType = 'Rental';
        }
        if (grid.hasClass('S')) {
            gridInventoryType = 'Sales';
        }
        if (grid.hasClass('L')) {
            gridInventoryType = 'Labor';
        }
        if (grid.hasClass('M')) {
            gridInventoryType = 'Misc';
        }
        if (grid.hasClass('P')) {
            gridInventoryType = 'Parts';
        }

        const $form = jQuery(event.currentTarget).closest('.fwform');
        const controllerName = $form.attr('data-controller');
        if ($form.attr('data-mode') === 'NEW') {
            let isValid = FwModule.validateForm($form);
            if (isValid) {
                let activeTabId = jQuery($form.find('[data-type="tab"].active')).attr('id');
                if (controllerName === "OrderController") {
                    OrderController.saveForm($form, { closetab: false });
                } else if (controllerName === "QuoteController") {
                    QuoteController.saveForm($form, { closetab: false });
                }
                $form.attr('data-opensearch', 'true');
                $form.attr('data-searchtype', gridInventoryType);
                $form.attr('data-activetabid', activeTabId);
            }
            return;
        }

        const search = new SearchInterface();
        switch (controllerName) {
            case 'OrderController':
                const orderId = FwFormField.getValueByDataField($form, 'OrderId');
                if (orderId == '') {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else {
                    search.renderSearchPopup($form, orderId, 'Order', gridInventoryType);
                }
                break;
            case 'QuoteController':
                const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
                if (quoteId == '') {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else {
                    search.renderSearchPopup($form, quoteId, 'Quote', gridInventoryType);
                }
                break;
            case 'PurchaseOrderController':
                const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
                if (purchaseOrderId == '') {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else {
                    search.renderSearchPopup($form, purchaseOrderId, 'PurchaseOrder', gridInventoryType);
                }
                break;
            case 'TemplateController':
                const templateId = FwFormField.getValueByDataField($form, 'TemplateId');
                if (templateId == '') {
                    FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
                } else {
                    search.renderSearchPopup($form, templateId, 'Template', gridInventoryType);
                }
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    insertLineItem($control: JQuery, $tr: JQuery) {
        const itemClass = FwBrowse.getValueByDataField($control, $tr, 'ItemClass');
        const types: any = ['C', 'K', 'I'];
        let primaryItemId;
        if (types.includes(itemClass)) {
            primaryItemId = FwBrowse.getValueByDataField($control, $tr, 'OrderItemId');
        } else {
            primaryItemId = FwBrowse.getValueByDataField($control, $tr, 'ParentId');
        }

        const request: any = {
            OrderId: FwBrowse.getValueByDataField($control, $tr, 'OrderId'),
            PrimaryItemId: primaryItemId,
            BelowOrderItemId: FwBrowse.getValueByDataField($control, $tr, 'OrderItemId')
        };

        FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/insertlineitem`, request, FwServices.defaultTimeout,
            response => {
                const pageNo = parseInt($control.attr('data-pageno'));
                const onDataBind = $control.data('ondatabind');
                if (typeof onDataBind == 'function') {
                    $control.data('ondatabind', request => {
                        onDataBind(request);
                        request.pageno = pageNo;
                    });
                }
                FwBrowse.databind($control);
            }, ex => FwFunc.showError(ex), $control);
    }
    //----------------------------------------------------------------------------------------------
    renderCompleteKitGridPopup($control: JQuery, $tr: JQuery, itemClass: string): void {
        let HTML: Array<string> = [], $popupHtml, $popup;
        let type;

        switch (itemClass) {
            case 'C':
            case 'CI':
            case 'CO':
                type = 'Complete';
                break;
            case 'K':
            case 'KI':
            case 'KO':
                type = 'Kit';
                break;
        }

        HTML.push(
            `<div class="fwcontrol fwcontainer fwform popup" data-control="FwContainer" data-type="form">
                <div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                  <div class="flexpage">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Update ${type} Options">
                        <div class="wideflexrow">
                           <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-datafield="ParentId" style="display:none;"></div>  
                           <div data-control="FwGrid" data-grid="Inventory${type}Grid" data-securitycaption=""></div>
                         </div>
                        <div class="wideflexrow" style="flex-direction:row-reverse;">  
                            <div class="fwformcontrol apply-options" data-type="button" data-enabled="true" style="flex:1 1 150px;margin:16px 10px 0px 5px;text-align:center; max-width:200px;">Apply</div> 
                        </div>
                    </div>
                </div>
            </div>`);
        $popupHtml = HTML.join('');
        $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwControl.renderRuntimeControls($popup.find('.fwcontrol'));
        FwPopup.showPopup($popup);

        let packageId;
        let parentOrderItemId;
        const orderId = FwBrowse.getValueByDataField($control, $tr, 'OrderId');
        const optionTypes: any = ['KI', 'KO', 'CI', 'CO'];
        if (optionTypes.includes(itemClass)) {
            const optionParentId = FwBrowse.getValueByDataField($control, $tr, 'ParentId');
            let $parenttr = $control.find(`[data-browsedatafield="OrderItemId"][data-originalvalue="${optionParentId}"]`).parents('tr');
            packageId = FwBrowse.getValueByDataField($control, $parenttr, 'ParentId');
            parentOrderItemId = FwBrowse.getValueByDataField($control, $parenttr, 'OrderItemId');
        } else {
            //inventoryId = FwBrowse.getValueByDataField($control, $tr, 'InventoryId');
            packageId = FwBrowse.getValueByDataField($control, $tr, 'ParentId');
            parentOrderItemId = FwBrowse.getValueByDataField($control, $tr, 'OrderItemId');
        }

        FwFormField.setValueByDataField($popup.find('.fwform'), 'ParentId', packageId);

        const $completeKitGrid = FwBrowse.renderGrid({
            nameGrid: `Inventory${type}Grid`,
            gridSecurityId: 'ABL0XJQpsQQo',
            moduleSecurityId: 'RFgCJpybXoEb',
            $form: $popup,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    //PackageId:  (itemClass === 'K' || itemClass === 'C') ? inventoryId: parentId,
                    PackageId: packageId,
                    WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid
                };
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.attr('data-hasaudithistory', 'false');
            }
        });

        const fieldsToHide: any = ['ItemTrackedBy', 'IsOption', 'Charge'];
        const $thead = $completeKitGrid.find('thead');
        for (let i = 0; i < fieldsToHide.length; i++) {
            $thead.find(`[data-browsedatafield="${fieldsToHide[i]}"]`).parent('td').hide();
        }
        FwBrowse.setAfterRenderRowCallback($completeKitGrid, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const defaultQuantity = 0;
            const $field = $tr.find('[data-browsedatafield="DefaultQuantity"]');
            $field.attr('data-browsedatatype', 'numericupdown');
            $field.attr('data-allownegative', 'false');
            $field.attr('data-originalvalue', defaultQuantity);
            FwBrowse.setFieldViewMode($completeKitGrid, $tr, $field);

            const isPrimary = FwBrowse.getValueByDataField($completeKitGrid, $tr, 'IsPrimary');
            if (isPrimary == 'true') {
                $field
                    .hide()
                    .parents('td')
                    .css('background-color', 'rgb(245,245,245)');
            }

            $field.on('change', '.value', e => {
                const quantity = jQuery(e.currentTarget).val();
                $field.attr('data-originalvalue', Number(quantity));
            });
        });

        FwBrowse.disableGrid($completeKitGrid);
        FwBrowse.search($completeKitGrid);

        // Close modal
        $popup.find('.close-modal').one('click', e => {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });
        //// Close modal if click outside
        //jQuery(document).on('click', e => {
        //    if (!jQuery(e.target).closest('.popup').length) {
        //        FwPopup.destroyPopup($popup);
        //    }
        //});

        $popup.on('click', '.apply-options', e => {
            const $trs = $completeKitGrid.find(`tbody tr [data-browsedatafield="IsPrimary"][data-originalvalue="false"]`).parents('tr');
            const request: any = {};
            const $items: any = [];
            for (let i = 0; i < $trs.length; i++) {
                const qty = FwBrowse.getValueByDataField($completeKitGrid, jQuery($trs[i]), 'DefaultQuantity')
                if (qty != 0) {
                    const item = {
                        InventoryId: FwBrowse.getValueByDataField($completeKitGrid, jQuery($trs[i]), 'InventoryId'),
                        Quantity: qty
                    }
                    $items.push(item);
                }
            }

            request.OrderId = orderId;
            request.ParentOrderItemId = parentOrderItemId;
            request.Items = $items;
            FwAppData.apiMethod(true, 'POST', "api/v1/orderitem/insertoption", request, FwServices.defaultTimeout,
                response => {
                    $popup.find('.close-modal').click();
                    FwBrowse.databind($control);
                },
                ex => FwFunc.showError(ex), $control);
        });
    }
}



//----------------------------------------------------------------------------------------------
////Refresh Availability
//FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.RefreshAvailability.id] = function (e) {
//    const $orderItemGrid = jQuery(event.currentTarget).closest('[data-name="OrderItemGrid"]');
//    let recType;
//    recType = jQuery(event.currentTarget).closest('[data-grid="OrderItemGrid"]');
//    if (recType.hasClass('R')) {
//        recType = 'R';
//    } else if (recType.hasClass('S')) {
//        recType = 'S';
//    } else if (recType.hasClass('L')) {
//        recType = 'L';
//    } else if (recType.hasClass('M')) {
//        recType = 'M';
//    } else if (recType.hasClass('P')) {
//        recType = 'P';
//    } else if (recType.hasClass('A')) {
//        recType = '';
//    } else if (recType.hasClass('RS')) {
//        recType = 'RS'
//    }

//    const pageNumber = $orderItemGrid.attr('data-pageno');
//    const onDataBind = $orderItemGrid.data('ondatabind');
//    if (typeof onDataBind == 'function') {
//        $orderItemGrid.data('ondatabind', function (request) {
//            onDataBind(request);
//            request.uniqueids.RefreshAvailability = true;
//            request.pageno = parseInt(pageNumber);
//        });
//    }

//    FwBrowse.search($orderItemGrid);
//    $orderItemGrid.attr('data-pageno', pageNumber);
//    //resets ondatabind
//    $orderItemGrid.data('ondatabind', onDataBind);

//    jQuery(document).trigger('click');
//}

//----------------------------------------------------------------------------------------------
// Manual Sorting
//FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.ManualSorting.id] = e => {
//    const $grid = jQuery(e.currentTarget).closest('.fwbrowse');
//    const $form = jQuery(e.currentTarget).closest('.fwform');
//    const module = $form.attr('data-controller').replace('Controller', '');
//    const orderId = FwFormField.getValueByDataField($form, `${module}Id`);

//    //hides the paging controls in manual sorting mode
//    const $pagingControls = $grid.find('.pager').children().children();
//    $pagingControls.hide();

//    //show all rows
//    const onDataBind = $grid.data('ondatabind');
//    if (typeof onDataBind == 'function') {
//        $grid.data('ondatabind', request => {
//            onDataBind(request);
//            request.pagesize = 9999;
//        });
//    }

//    $grid.data('afterdatabindcallback', () => {
//        //add sortable handle
//        const $tdselectrow = $grid.find('tbody td.tdselectrow');
//        $tdselectrow.find('div.divselectrow').hide();
//        if ($tdselectrow.find('.drag-handle').length === 0) {
//            $tdselectrow
//                .append('<i style="vertical-align:-webkit-baseline-middle; cursor:grab;" class="material-icons drag-handle">drag_handle</i>')
//                .css('text-align', 'center');
//        } else {
//            $tdselectrow.find('.drag-handle').show();
//        }

//        //adds button to apply changes in sorting
//        const $applyChangesBtn = jQuery('<div data-type="button" class="fwformcontrol sorting"><i class="material-icons" style="position:relative; top:5px;">&#xE161;</i>Apply</div>');
//        const $gridMenu = $grid.find('[data-control="FwMenu"]');
//        $applyChangesBtn.on('click', e => {
//            try {
//                const $trs = $grid.find('tbody  tr');
//                const orderItemIds: any = [];
//                let startAtIndex = '';
//                const isFirstPage = $grid.attr('data-pageno') === "1";
//                for (let i = 0; i < $trs.length; i++) {
//                    const $tr = jQuery($trs[i]);
//                    const id = $tr.find('[data-browsedatafield="OrderItemId"]').attr('data-originalvalue');
//                    //get index of first row if not on first page of the grid
//                    if (i === 0 && !isFirstPage) {
//                        startAtIndex = $tr.find('[data-browsedatafield="RowNumber"]').attr('data-originalvalue');
//                    }
//                    orderItemIds.push(id);
//                }

//                const request: any = {};
//                request.OrderId = orderId;
//                request.OrderItemIds = orderItemIds;
//                //request.pageno = parseInt($grid.attr('data-pageno'));
//                if (startAtIndex != '') request.StartAtIndex = startAtIndex;
//                FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/sort`, request, FwServices.defaultTimeout,
//                    response => {
//                        FwBrowse.search($grid);
//                        $pagingControls.show();
//                        $gridMenu.find('.sorting').hide();
//                        $tdselectrow.find('.drag-handle').hide();
//                        $tdselectrow.find('div.divselectrow').show();
//                        $gridMenu.find('.buttonbar').show();
//                    },
//                    ex => FwFunc.showError(ex), $grid);
//            } catch (ex) {
//                FwFunc.showError(ex);
//            }
//        });

//        //cancel sorting button
//        const $cancelBtn = jQuery('<div data-type="button" class="fwformcontrol sorting" style="margin-left:10px;">Cancel</div>');
//        $cancelBtn.on('click', e => {
//            FwBrowse.search($grid); //refresh grid to reset to original sorting order
//            $pagingControls.show();
//            $gridMenu.find('.sorting').hide();
//            $tdselectrow.find('.drag-handle').hide();
//            $tdselectrow.find('div.divselectrow').show();
//            $gridMenu.find('.buttonbar').show();
//        });

//        //toggle displayed buttons
//        $gridMenu.find('.buttonbar').hide();
//        if ($gridMenu.find('.sorting').length < 1) {
//            $gridMenu.append($applyChangesBtn, $cancelBtn);
//        } else {
//            $gridMenu.find('.sorting').show();
//        }

//        //initialize Sortable
//        Sortable.create($grid.find('tbody').get(0), {
//            handle: 'i.drag-handle'
//        });

//        $grid.data('afterdatabindcallback', null);
//    });

//    FwBrowse.search($grid);
//    //resets ondatabind if we want the grid to go back to original pagesize (move to apply/cancel events)
//    //$grid.data('ondatabind', onDataBind);

//    //closes menu
//    jQuery(document).trigger('click');
//};

//---------------------------------------------------------------------------------
//FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.AddLossAndDamageItems.id] = function (event: JQuery.ClickEvent) {
//    try {
//        const $form = jQuery(this).closest('.fwform');
//        if ($form.attr('data-mode') !== 'NEW') {
//            OrderController.addLossDamage($form, event);
//        } else {
//            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//        }
//    }
//    catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
////---------------------------------------------------------------------------------
//FwApplicationTree.clickEvents[Constants.Grids.OrderItemGrid.menuItems.RetireLossAndDamageItems.id] = function (event: JQuery.ClickEvent) {
//    try {
//        const $form = jQuery(this).closest('.fwform');
//        if ($form.attr('data-mode') !== 'NEW') {
//            let $form = jQuery(this).closest('.fwform');
//            OrderController.retireLossDamage($form);
//        } else {
//            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//        }
//    }
//    catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//---------------------------------------------------------------------------------
var OrderItemGridController = new OrderItemGrid();