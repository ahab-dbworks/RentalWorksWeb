﻿class TransferOrderItemGrid {
    Module: string = 'TransferOrderItemGrid';
    apiurl: string = 'api/v1/orderitem';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        //Allow searching on description field
        $tr.find('[data-browsedatafield="Description"]').data('changedisplayfield', $validationbrowse => {
            $validationbrowse.find('[data-browsedatafield="ICode"]').attr('data-validationdisplayfield', 'false');
            $validationbrowse.find('[data-browsedatafield="Description"]').attr('data-validationdisplayfield', 'true');
        });
    }

    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');

        $generatedtr.find('div[data-browsedatafield="QuantityOrdered"]').on('change', 'input.value', e => {
            const itemClass = FwBrowse.getValueByDataField($control, $generatedtr, 'ItemClass');
            if (itemClass == 'K' || itemClass == 'C') {
                this.updateCompleteKitAccessoryRows($control, $generatedtr, e, 'QuantityOrdered');
            }
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
            const fromWarehouseId = FwFormField.getValueByDataField($form, 'FromWarehouseId');
            const toWarehouseId = FwFormField.getValueByDataField($form, 'ToWarehouseId');
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
            $generatedtr.find('.field[data-browsedatafield="WarehouseId"]').text(FwFormField.getTextByDataField($form, 'FromWarehouseId'));
            $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"]').text(FwFormField.getTextByDataField($form, 'ToWarehouseId'));

            //adds warehouse values
            const beforeSave = $control.data('beforesave');
            if (typeof beforeSave == 'function') {
                $control.data('beforesave', request => {
                    beforeSave(request);
                    request.WarehouseId = fromWarehouseId;
                    request.ReturnToWarehouseId = toWarehouseId;
                });
            };

            if ($generatedtr.hasClass("newmode")) {
                const classification = FwBrowse.getValueByDataField($control, $tr, 'Classification');
                if (classification == 'M') {
                    $generatedtr.find('[data-browsedatafield="Description"]').attr({ 'data-browsedatatype': 'text', 'data-formdatatype': 'text' });
                    $generatedtr.find('[data-browsedatafield="Description"] input.value').remove();
                    $generatedtr.find('[data-browsedatafield="Description"] input.text').removeClass('text').addClass('value').off('change');
                    $generatedtr.find('[data-browsedatafield="Description"] .btnpeek').hide();
                    $generatedtr.find('[data-browsedatafield="Description"] .btnvalidate').hide();
                    $generatedtr.find('[data-browsedatafield="Description"] .sk-fading-circle validation-loader').hide();
                }
            }
        });

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const availabilityState = FwBrowse.getValueByDataField($control, $generatedtr, 'AvailabilityState');
            const availabilityStateAllWarehouses = FwBrowse.getValueByDataField($control, $generatedtr, 'AvailabilityStateAllWarehouses');
            const $availQty = $generatedtr.find('[data-browsedatafield="AvailableQuantity"]')
            const $availQtyAllWarehouses = $generatedtr.find('[data-browsedatafield="AvailableQuantityAllWarehouses"]')
            $availQty.attr('data-state', availabilityState);
            $availQtyAllWarehouses.attr('data-state', availabilityStateAllWarehouses);
            $availQty.css('cursor', 'pointer');

            //Option to open up Complete/Kit grid to add items
            const itemClass = FwBrowse.getValueByDataField($control, $tr, 'ItemClass');
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
            const $td = $tr.find('[data-browsedatafield="InventoryId"]');
            let peekForm;
            switch (recType) {
                case 'R':
                    peekForm = 'RentalInventory';
                    break;
                case 'S':
                    peekForm = 'SalesInventory';
                    break;
            }
            $td.attr('data-peekForm', peekForm);

            //Allow searching on description field
            const validTextItemClasses: any = ['M', 'GH', 'T', 'ST'];
            if (validTextItemClasses.includes(itemClass)) {
                $tr.find('[data-browsedatafield="Description"]').attr({ 'data-browsedatatype': 'text', 'data-formdatatype': 'text' });
            } else {
                $tr.find('[data-browsedatafield="Description"]').data('changedisplayfield', $validationbrowse => {
                    $validationbrowse.find('[data-browsedatafield="ICode"]').attr('data-validationdisplayfield', 'false');
                    $validationbrowse.find('[data-browsedatafield="Description"]').attr('data-validationdisplayfield', 'true');
                });
            }
        });

        //availability calendar popup
        $generatedtr.find('div[data-browsedatafield="AvailableQuantity"]').on('click', e => {
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

            const $calendar = $popup.find('.calendar');
            const $scheduler = $popup.find('.realscheduler');

            const iCode = $generatedtr.find('[data-browsedatafield="InventoryId"]').attr('data-originaltext');
            FwFormField.setValue2($popup.find('div[data-datafield="ICode"]'), iCode);
            const description = FwBrowse.getValueByDataField($control, $generatedtr, 'Description');
            FwFormField.setValue2($popup.find('div[data-datafield="Description"]'), description);
            const warehouseId = FwBrowse.getValueByDataField($control, $generatedtr, 'WarehouseId');
            const warehouseText = $generatedtr.find('[data-browsedatafield="WarehouseId"]').attr('data-originaltext');
            FwFormField.setValue2($popup.find('.warehousefilter'), warehouseId, warehouseText);

            FwScheduler.renderRuntimeHtml($calendar);
            FwScheduler.init($calendar);
            FwScheduler.loadControl($calendar);
            const inventoryId = FwBrowse.getValueByDataField($control, $generatedtr, 'InventoryId');
            RentalInventoryController.addCalSchedEvents($generatedtr, $calendar, inventoryId);
            const schddate = FwScheduler.getTodaysDate();
            FwScheduler.navigate($calendar, schddate);
            FwScheduler.refresh($calendar);

            FwSchedulerDetailed.renderRuntimeHtml($scheduler);
            FwSchedulerDetailed.init($scheduler);
            FwSchedulerDetailed.loadControl($scheduler);
            RentalInventoryController.addCalSchedEvents($generatedtr, $scheduler, inventoryId);
            FwSchedulerDetailed.navigate($scheduler, schddate, 35);
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
        });
    }

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'Description':
            case 'InventoryId':
                const availFor = $form.find('.active [data-grid="TransferOrderItemGrid"]');
                if (availFor.hasClass('R')) {
                    request.uniqueids = {
                        AvailFor: 'R'
                    };
                } else if (availFor.hasClass('S')) {
                    request.uniqueids = {
                        AvailFor: 'S'
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateicodetransfer`);
        }
    }
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
                //if (field == 'DaysPerWeek' || field == 'DiscountPercentDisplay') {
                //    const isLocked = FwBrowse.getValueByDataField($grid, $nextRow, 'Locked');
                //    if (isLocked == 'true') {
                //        return;
                //    } else {
                //        newValue = jQuery(event.currentTarget).val();
                //    }
                //} else

                if (field == 'QuantityOrdered') {
                    const accessoryRatio = parseInt(FwBrowse.getValueByDataField($grid, $nextRow, 'AccessoryRatio'));
                    const parentValue = Number(jQuery(event.currentTarget).val());
                    newValue = Math.round(parentValue / accessoryRatio).toString();
                }

                FwBrowse.setRowEditMode($grid, $nextRow);
                FwBrowse.setFieldValue($grid, $nextRow, field, { value: newValue });
            } else {
                return;
            }
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
                    $items.unshift(item);
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
                ex => FwFunc.showError(ex), $popup);
        });
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
}

//-----------------------------------------------------------------------------------------------------
//QuikSearch
FwApplicationTree.clickEvents[Constants.Grids.TransferOrderItemGrid.menuItems.QuikSearch.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(event.currentTarget).closest('.fwform');

        if ($form.attr('data-mode') === 'NEW') {
            TransferOrderController.saveForm($form, { closetab: false });
            return;
        }

        const transferId = FwFormField.getValueByDataField($form, 'TransferId');
        if (transferId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        } else {
            let search = new SearchInterface();
            search.renderSearchPopup($form, transferId, 'Transfer');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
////Refresh Availability
//FwApplicationTree.clickEvents[Constants.Grids.TransferOrderItemGrid.menuItems.RefreshAvailability.id] = function (e: JQuery.ClickEvent) {
//    const $transferOrderItemGrid = jQuery(this).closest('[data-name="TransferOrderItemGrid"]');
//    let recType;
//    recType = jQuery(this).closest('[data-grid="TransferOrderItemGrid"]');
//    if (recType.hasClass('R')) {
//        recType = 'R';
//    } else if (recType.hasClass('S')) {
//        recType = 'S';
//    }
//    //else if (recType.hasClass('L')) {
//    //    recType = 'L';
//    //} else if (recType.hasClass('M')) {
//    //    recType = 'M';
//    //} else if (recType.hasClass('P')) {
//    //    recType = 'P';
//    //} else if (recType.hasClass('A')) {
//    //    recType = '';
//    //} else if (recType.hasClass('RS')) {
//    //    recType = 'RS'
//    //}

//    const pageNumber = $transferOrderItemGrid.attr('data-pageno');
//    const onDataBind = $transferOrderItemGrid.data('ondatabind');
//    if (typeof onDataBind == 'function') {
//        $transferOrderItemGrid.data('ondatabind', function (request) {
//            onDataBind(request);
//            request.uniqueids.RefreshAvailability = true;
//            request.pageno = parseInt(pageNumber);
//        });
//    }

//    FwBrowse.search($transferOrderItemGrid);
//    $transferOrderItemGrid.attr('data-pageno', pageNumber);
//    //resets ondatabind
//    $transferOrderItemGrid.data('ondatabind', onDataBind);

//    jQuery(document).trigger('click');
//}
////----------------------------------------------------------------------------------------------
//Copy Template
FwApplicationTree.clickEvents[Constants.Grids.TransferOrderItemGrid.menuItems.CopyTemplate.id] = function (e: JQuery.ClickEvent) {
    const $form = jQuery(this).closest('.fwform');
    const $grid = jQuery(this).closest('[data-name="TransferOrderItemGrid"]');
    let recType;
    recType = jQuery(this).closest('[data-grid="TransferOrderItemGrid"]');
    if (recType.hasClass('R')) {
        recType = 'R';
    } else if (recType.hasClass('S')) {
        recType = 'S';
    } else if (recType.hasClass('L')) {
        recType = 'L';
    } else if (recType.hasClass('M')) {
        recType = 'M';
    } else if (recType.hasClass('P')) {
        recType = 'P';
    } else if (recType.hasClass('A')) {
        recType = '';
    } else if (recType.hasClass('RS')) {
        recType = 'RS'
    }
    let module = $form.attr('data-controller').replace('Controller', '');
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
                            <div class="fwform-section-title" style="margin-bottom:10px;">Copy Template to Transfer</div>
                            <div data-control="FwGrid" class="container"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formrow add-button">
                        <div class="select-items fwformcontrol" data-type="button" style="float:right;">Add to Transfer</div>
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

    $popup.on('click', '.select-items', e => {
        const $selectedCheckBoxes = $popup.find('[data-control="FwGrid"] tbody .cbselectrow:checked');
        let templateIds: Array<string> = [];
        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let $this = jQuery($selectedCheckBoxes[i]);
            let id;
            id = $this.closest('tr').find('div[data-browsedatafield="TemplateId"]').attr('data-originalvalue');
            templateIds.push(id);
        };

        let request: any = {};
        request = {
            TemplateIds: templateIds
            , RecType: recType
            , OrderId: FwFormField.getValueByDataField($form, `TransferId`)
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/order/copytemplate`, request, FwServices.defaultTimeout, function onSuccess(response) {
            $popup.find('.close-modal').click();
            FwBrowse.search($grid);
        }, null, $templatePopup);

    });

    FwBrowse.search($templateBrowse);
};

//----------------------------------------------------------------------------------------------
var TransferOrderItemGridController = new TransferOrderItemGrid();