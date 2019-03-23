﻿abstract class StagingCheckoutBase {
    Module: string;
    caption: string;
    nav: string;
    id: string;
    showAddItemToOrder: boolean;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    contractId: string;
    isPendingItemGridView: boolean;
    Type: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form: JQuery = this.openForm('EDIT');
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?: any) {
        switch (this.Module) {
            case 'StagingCheckout':
                this.Type = 'Order';
                break;
            case 'TransferOut':
                this.Type = 'Transfer';
                break;
            case 'FillContainer':
                this.Type = 'Item';
                break;
        }
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="WarehouseId"]').hide();

        $form.find('.partial-contract').hide();
        $form.find('.pending-item-grid').hide();
        $form.find('.grid-view-radio').hide();

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        this.getSoundUrls();
        this.getOrder($form);
        if (typeof parentmoduleinfo !== 'undefined') {
            //switch (this.Module) {
            //    case 'StagingCheckout':
            //        FwFormField.setValueByDataField($form, 'OrderId', parentmoduleinfo.OrderId, parentmoduleinfo.OrderNumber);
            //        break;
            //    case 'TransferOut':
            //        FwFormField.setValueByDataField($form, 'TransferId', parentmoduleinfo.TransferId, parentmoduleinfo.TransferNumber);
            //        break;
            //    case 'FillContainer':
            //        FwFormField.setValueByDataField($form, 'ItemId', parentmoduleinfo.ItemId, parentmoduleinfo.ItemNumber);
            //        break;
            //}
            FwFormField.setValueByDataField($form, `${this.Type}Id`, parentmoduleinfo[`${this.Type}Id`], parentmoduleinfo[`${this.Type}Number`]);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', parentmoduleinfo.WarehouseId, parentmoduleinfo.Warehouse);
            FwFormField.setValueByDataField($form, 'Description', parentmoduleinfo.description);
            jQuery($form.find('[data-datafield="OrderId"]')).trigger('change');
            $form.attr('data-showsuspendedsessions', 'false');
        }

        $form.find('div[data-datafield="OrderId"] input').focus();
        this.getSuspendedSessions($form);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getSuspendedSessions($form) {
        const showSuspendedSessions = $form.attr('data-showsuspendedsessions');

        if (showSuspendedSessions != "false") {
            FwAppData.apiMethod(true, 'GET', 'api/v1/checkout/suspendedsessionsexist', null, FwServices.defaultTimeout, function onSuccess(response) {
                $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
            }, null, $form);

            $form.on('click', '.suspendedsession', e => {
                let html = `<div>
                              <div style="background-color:white; padding-right:10px; text-align:right;" class="close-modal"><i style="cursor:pointer;" class="material-icons">clear</i></div>
                               <div id="suspendedSessions" style="max-width:1400px; max-height:750px; overflow:auto;"></div>
                            </div>`;


                const officeLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
                const $browse = SuspendedSessionController.openBrowse();
                $browse.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OfficeLocationId: officeLocationId
                        , SessionType: 'OUT'
                        , OrderType: 'O'
                    }
                });

                const $popup = FwPopup.renderPopup(jQuery(html), { ismodal: true });
                FwPopup.showPopup($popup);
                jQuery('#suspendedSessions').append($browse);
                FwBrowse.search($browse);

                $popup.find('.close-modal > i').one('click', function (e) {
                    FwPopup.destroyPopup($popup);
                    jQuery(document).find('.fwpopup').off('click');
                    jQuery(document).off('keydown');
                });

                $browse.on('dblclick', 'tr.viewmode', e => {
                    const $this = jQuery(e.currentTarget);
                    const OrderId = $this.find(`[data-browsedatafield="OrderId"]`).attr('data-originalvalue');
                    const orderNumber = $this.find(`[data-browsedatafield="OrderNumber"]`).attr('data-originalvalue');
                    FwFormField.setValueByDataField($form, 'OrderId', OrderId, orderNumber);
                    FwPopup.destroyPopup($popup);
                    $form.find('[data-datafield="OrderId"] input').change();
                    $form.find('.suspendedsession').hide();
                });
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    getSoundUrls(): void {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    getOrder($form: JQuery): void {
        const maxPageSize = 20;
        const module = this.Module;
        $form.on('change', `[data-datafield="${this.Type}Id"]`, () => {
            try {
                FwFormField.setValueByDataField($form, 'Quantity', '');
                FwFormField.setValueByDataField($form, 'Code', '');
                $form.find('.error-msg:not(.qty)').html('');
                $form.find('.grid-view-radio').show();

                if (FwFormField.getValueByDataField($form, 'IncludeZeroRemaining') === 'T') {
                    $form.find('.option-list').toggle();
                    $form.find('div[data-datafield="IncludeZeroRemaining"] input').prop('checked', false);
                }

                const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
                //if (module == 'StagingCheckout') {
                //    orderId = FwFormField.getValueByDataField($form, 'OrderId');
                //} else if (module == 'TransferOut') {
                //    orderId = FwFormField.getValueByDataField($form, 'TransferId');
                //}
                let apiName;
                switch (module) {
                    case 'StagingCheckout':
                        apiName = 'order';
                        break;
                    case 'TransferOut':
                        apiName = 'transferorder';
                        break;
                    case 'FillContainer':
                        apiName = 'containeritem';
                        break;
                }
                const apiUrl = `api/v1/${apiName}/${orderId}`;
                const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                FwFormField.setValueByDataField($form, 'GridView', 'STAGE');
                FwAppData.apiMethod(true, 'GET', apiUrl, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwFormField.setValueByDataField($form, 'Description', response.Description);
                    FwFormField.setValueByDataField($form, 'Location', response.Location);
                    if (module == 'StagingCheckout') FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
                    // Determine tabs to render
                    FwAppData.apiMethod(true, 'GET', `api/v1/checkout/stagingtabs?OrderId=${orderId}&WarehouseId=${warehouseId}`, null, FwServices.defaultTimeout, function onSuccess(res) {
                        res.QuantityTab === true ? $form.find('.quantity-items-tab').show() : $form.find('.quantity-items-tab').hide();
                        res.HoldingTab === true ? $form.find('.holding-items-tab').show() : $form.find('.holding-items-tab').hide();
                        res.SerialTab === true ? $form.find('.serial-items-tab').show() : $form.find('.serial-items-tab').hide();
                        //res.UsageTab === true ? $form.find('.usage-tab').show() : $form.find('.usage-tab').hide();
                        res.ConsignmentTab === true ? $form.find('.consignment-tab').show() : $form.find('.consignment-tab').hide();
                    }, function onError(ex) {
                        FwFunc.showError(ex)
                    }, $form);
                }, null, $form);
                // ----------
                const $stagedItemGridControl = $form.find('[data-name="StagedItemGrid"]');
                $stagedItemGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId,
                        WarehouseId: warehouseId
                    }
                    request.pagesize = maxPageSize;
                })
                FwBrowse.search($stagedItemGridControl);
                // ----------
                const $stageQuantityItemGridControl = $form.find('[data-name="StageQuantityItemGrid"]');
                $stageQuantityItemGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId
                    }
                    request.pagesize = 20;
                })
                FwBrowse.search($stageQuantityItemGridControl);
                // ----------
                setTimeout(function () {
                    const $contractpeek = $form.find('.outcontract, .incontract');
                    $contractpeek.attr('data-browsedatafield', 'ContractId');

                    const $trs = $form.find('.ordersummarygrid tr.viewmode');
                    for (let i = 0; i <= $trs.length; i++) {
                        const $rectype = jQuery($trs[i]).find('[data-browsedatafield="RecTypeDisplay"]');
                        const recvalue = $rectype.attr('data-originalvalue');
                        const $validationfield = jQuery($trs[i]).find('[data-browsedatafield="InventoryId"]');

                        switch (recvalue) {
                            case 'RENTAL':
                                $validationfield.attr('data-validationname', 'RentalInventoryValidation');
                                break;
                            case 'SALES':
                                $validationfield.attr('data-validationname', 'SalesInventoryValidation');
                                break;
                        }
                    }
                }, 2000);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
            //module == 'StagingCheckout' ? FwFormField.disable($form.find('div[data-datafield="OrderId"]')) : FwFormField.disable($form.find('div[data-datafield="TransferId"]'));
            FwFormField.disable($form.find(`div[data-datafield="${this.Type}Id"]`));
            $form.find('.orderstatus').show();
            $form.find('.createcontract').show();
            $form.find('.original-buttons').show();
            $form.find('[data-datafield="Code"] input').focus();
            $form.find('.suspendedsession').hide();
        });
    };
    //----------------------------------------------------------------------------------------------
    addButtonMenu($form: JQuery): void {
        const $createContract = FwMenu.generateButtonMenuOption('Create Contract');
        $createContract.on('click', e => {
            e.stopPropagation();
            $form.find('.createcontract').click();
        });

        const $createPartialContract = FwMenu.generateButtonMenuOption('Create Partial Contract');
        $createPartialContract.on('click', e => {
            e.stopPropagation();
            this.startPartialCheckoutItems($form, e);
        });

        const menuOptions: Array<string> = [];
        menuOptions.push($createContract, $createPartialContract);

        const $buttonmenu = $form.find('.createcontract[data-type="btnmenu"]');
        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    };
    //----------------------------------------------------------------------------------------------
    startPartialCheckoutItems = ($form: JQuery, event): void => {
        $form.find('.error-msg:not(.qty)').html('');
        const maxPageSize = 20;
        let requestBody: any = {};
        let orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
        requestBody.OrderId = orderId;
        if (orderId != '') {
            $form.find('.orderstatus').hide();
            $form.find('.createcontract').hide();
            $form.find('.original-buttons').hide();
            $form.find('.complete-checkout-contract').show();
            $form.find('[data-caption="Items"]').hide();
            $form.find('.partial-contract').show();
            $form.find('.flexrow').css('max-width', '2200px');
            $form.find('.pending-item-grid').hide();
            $form.find('.staged-item-grid').show();
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/startcheckoutcontract`, requestBody, FwServices.defaultTimeout, response => {
                try {
                    this.contractId = response.ContractId;
                    const $checkedOutItemGridControl = $form.find('[data-name="CheckedOutItemGrid"]');
                    $checkedOutItemGridControl.data('ContractId', this.contractId); // Stores ContractId on grid for dblclick in grid controller
                    $checkedOutItemGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            ContractId: this.contractId
                        }
                        request.orderby = 'OrderBy';
                        request.pagesize = 20;
                    })
                    FwBrowse.search($checkedOutItemGridControl);

                    const $stagedItemGridControl = $form.find('[data-name="StagedItemGrid"]');
                    $stagedItemGridControl.data('ContractId', this.contractId); // Stores ContractId on grid for dblclick in grid controller
                    $stagedItemGridControl.data('ondatabind', function (request) {
                        request.orderby = "ItemOrder";
                        request.uniqueids = {
                            OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                            WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                        };
                        request.pagesize = maxPageSize;
                    })

                    FwBrowse.search($stagedItemGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, null);
        } else {
            event.stopPropagation();
            FwNotification.renderNotification('WARNING', 'Select an Order.')
        }
    };
    //----------------------------------------------------------------------------------------------
    // There are corresponding double click events in the Staged Item Grid controller
    moveStagedItemToOut($form: JQuery): void {
        let barCode, iCode, quantity, orderItemId, vendorId, request: any = {};
        const successSound = new Audio(this.successSoundFileName);
        const errorSound = new Audio(this.errorSoundFileName);
        const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        const $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
        const $selectedCheckBoxes = $stagedItemGrid.find('.cbselectrow:checked');
        const barCodeFieldValue = $form.find('.partial-contract-barcode input').val();
        const quantityFieldValue = $form.find('.partial-contract-quantity input').val();
        const orderId = FwFormField.getValueByDataField($form, 'OrderId');
        const errorMsg = $form.find('.error-msg:not(.qty)');

        if (barCodeFieldValue !== '' && $selectedCheckBoxes.length === 0) {
            request.ContractId = this.contractId;
            request.Code = barCodeFieldValue;
            request.OrderId = orderId
            if (quantityFieldValue !== '') {
                request.Quantity = quantityFieldValue
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/movestageditemtoout`, request, FwServices.defaultTimeout, response => {
                if (response.success === true && response.status != 107) {
                    errorMsg.html('');
                    successSound.play();
                    $form.find('.partial-contract-barcode input').val('');
                    $form.find('.partial-contract-quantity input').val('');
                    $form.find('.partial-contract-barcode input').select();
                    setTimeout(() => {
                        FwBrowse.search($checkedOutItemGrid);
                        FwBrowse.search($stagedItemGrid);
                    }, 500);
                }
                if (response.status === 107) {
                    errorMsg.html('');
                    successSound.play();
                    $form.find('.partial-contract-quantity input').focus();
                }
                if (response.success === false && response.status !== 107) {
                    errorSound.play();
                    errorMsg.html(`<div><span>${response.msg}</span></div>`);
                    $form.find('.partial-contract-barcode input').focus();
                }
            }, null, null);
        } else {
            if ($selectedCheckBoxes.length !== 0) {
                let responseCount = 0;
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    barCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="BarCode"]').attr('data-originalvalue');
                    iCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="ICode"]').attr('data-originalvalue');
                    quantity = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Quantity"]').attr('data-originalvalue');
                    orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                    vendorId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="VendorId"]').attr('data-originalvalue');

                    request.OrderId = orderId
                    request.ContractId = this.contractId;
                    request.Quantity = quantity

                    if (barCode !== '') {
                        request.Code = barCode;
                    } else {
                        request.Code = iCode;
                        request.OrderItemId = orderItemId;
                        request.VendorId = vendorId;
                    }
                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/movestageditemtoout`, request, FwServices.defaultTimeout, response => {
                        responseCount++;
                        if (responseCount === $selectedCheckBoxes.length) {
                            setTimeout(() => {
                                FwBrowse.search($checkedOutItemGrid);
                                FwBrowse.search($stagedItemGrid);
                            }, 0);
                        }
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, null);
                }

                $form.find('.partial-contract-barcode input').val('');
                $form.find('.partial-contract-quantity input').val('');
                $form.find('.partial-contract-barcode input').focus();
            } else {
                FwNotification.renderNotification('WARNING', 'Select rows in Stage Items or use Bar Code input in order to perform this function.');
                $form.find('.partial-contract-barcode input').focus();
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    // There are corresponding double click events in the Checked Out Item Grid controller
    moveOutItemToStaged($form: JQuery): void {
        let $selectedCheckBoxes, $stagedItemGrid, orderId, barCodeFieldValue, barCode, iCode, quantityFieldValue, quantity, orderItemId, vendorId, $checkedOutItemGrid, successSound, errorMsg, errorSound, request: any = {};
        successSound = new Audio(this.successSoundFileName);
        errorSound = new Audio(this.errorSoundFileName);

        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
        $selectedCheckBoxes = $checkedOutItemGrid.find('.cbselectrow:checked');
        barCodeFieldValue = $form.find('.partial-contract-barcode input').val();
        quantityFieldValue = $form.find('.partial-contract-quantity input').val();
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        errorMsg = $form.find('.error-msg:not(.qty)');
        if (barCodeFieldValue !== '' && $selectedCheckBoxes.length === 0) {
            request.ContractId = this.contractId;
            request.Code = barCodeFieldValue;
            request.OrderId = orderId;
            if (quantityFieldValue !== '') {
                request.Quantity = quantityFieldValue
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/moveoutitemtostaged`, request, FwServices.defaultTimeout, response => {
                if (response.success === true && response.status != 107) {
                    errorMsg.html('');
                    successSound.play();
                    $form.find('.partial-contract-barcode input').val('');
                    $form.find('.partial-contract-quantity input').val('');
                    $form.find('.partial-contract-barcode input').select();
                    setTimeout(() => {
                        FwBrowse.search($checkedOutItemGrid);
                        FwBrowse.search($stagedItemGrid);
                    }, 500);
                }
                if (response.status === 107) {
                    errorMsg.html('');
                    successSound.play();
                    $form.find('.partial-contract-quantity input').focus();
                }
                if (response.success === false && response.status !== 107) {
                    errorSound.play();
                    errorMsg.html(`<div><span>${response.msg}</span></div>`);
                    $form.find('.partial-contract-barcode input').focus();
                }
            }, null, null);
        } else {
            if ($selectedCheckBoxes.length !== 0) {
                let responseCount = 0;

                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    barCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="BarCode"]').attr('data-originalvalue');
                    iCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="ICode"]').attr('data-originalvalue');
                    quantity = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Quantity"]').attr('data-originalvalue');
                    orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                    vendorId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="VendorId"]').attr('data-originalvalue');

                    request.OrderId = orderId;
                    request.ContractId = this.contractId;
                    request.Quantity = quantity;

                    if (barCode !== '') {
                        request.Code = barCode;
                    } else {
                        request.Code = iCode;
                        request.OrderItemId = orderItemId;
                        request.VendorId = vendorId;
                    }
                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/moveoutitemtostaged`, request, FwServices.defaultTimeout, response => {
                        responseCount++;

                        if (responseCount === $selectedCheckBoxes.length) {
                            setTimeout(() => {
                                FwBrowse.search($checkedOutItemGrid);
                                FwBrowse.search($stagedItemGrid);
                            }, 0);
                        }
                    }, function onError(response) {
                        FwFunc.showError(response);
                    }, null);
                }

                $form.find('.partial-contract-barcode input').val('');
                $form.find('.partial-contract-quantity input').val('');
                $form.find('.partial-contract-barcode input').focus();
            } else {
                FwNotification.renderNotification('WARNING', 'Select rows in Contract Items or use Bar Code input in order to perform this function.');
                $form.find('.partial-contract-barcode input').focus();
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    completeCheckOutContract($form: JQuery, event): void {
        $form.find('.error-msg:not(.qty)').html('');
        $form.find('.grid-view-radio').hide();

        if (this.contractId) {
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/completecheckoutcontract/${this.contractId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                try {
                    let contractInfo: any = {}, $contractForm;
                    contractInfo.ContractId = response.ContractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    $form.find('.flexrow').css('max-width', '1200px');
                    FwModule.openSubModuleTab($form, $contractForm);
                    $form.find('.clearable').find('input').val(''); // Clears all fields but gridview radio
                    FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
                    $form.find('.partial-contract').hide();
                    $form.find('.complete-checkout-contract').hide();
                    $form.find('[data-caption="Items"]').show();
                    FwFormField.enable($form.find('div[data-datafield="OrderId"]'));
                    // Clear out all grids
                    $form.find('div[data-name="StagedItemGrid"] tr.viewmode').empty();
                    $form.find('div[data-name="CheckOutPendingItemGrid"] tr.viewmode').empty();
                    $form.find('div[data-name="CheckedOutItemGrid"] tr.viewmode').empty();
                    $form.find('div[data-name="StageQuantityItemGrid"] tr.viewmode').empty();
                    $form.find('div[data-datafield="OrderId"]').focus();
                    // Reveal buttons
                    $form.find('.original-buttons').show();
                    $form.find('.orderstatus').show();
                    $form.find('.createcontract').show();
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        } else {
            event.stopPropagation();
            FwNotification.renderNotification('WARNING', 'Check-out items before attemping to perform this function.');
        }
    };
    //----------------------------------------------------------------------------------------------
    createContract($form: JQuery, event): void {
        let orderId, errorMsg, errorSound, request: any = {};
        errorMsg = $form.find('.error-msg:not(.qty)');
        errorMsg.html('');
        $form.find('.grid-view-radio').hide();

        errorSound = new Audio(this.errorSoundFileName);
        orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
        if (orderId != '') {
            request.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', "api/v1/checkout/checkoutallstaged", request, FwServices.defaultTimeout, function onSuccess(response) {
                let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                if (response.success === true) {
                    let contractInfo: any = {}, $contractForm;
                    $form.find('.flexrow').css('max-width', '1200px');
                    contractInfo.ContractId = response.ContractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);
                    $form.find('.clearable').find('input').val(''); // Clears all fields but gridview radio
                    FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
                    FwFormField.enable($form.find('div[data-datafield="OrderId"]'));
                    $form.find('[data-datafield="Code"] input').select();
                    // Clear out all grids
                    $form.find('div[data-name="StagedItemGrid"] tr.viewmode').empty();
                    $form.find('div[data-name="CheckOutPendingItemGrid"] tr.viewmode').empty();
                    $form.find('div[data-name="CheckedOutItemGrid"] tr.viewmode').empty();
                    $form.find('div[data-name="StageQuantityItemGrid"] tr.viewmode').empty();
                    $form.find('.pending-item-grid').hide();
                    $form.find('.staged-item-grid').show();
                }
                if (response.success === false) {
                    errorSound.play();
                    errorMsg.html(`<div><span>${response.msg}</span></div>`);
                }
            }, null, $form);
        } else {
            event.stopPropagation();
            FwNotification.renderNotification('WARNING', 'Select an Order.')
        }
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any): void {
        const type = this.Type;
        // ----------
        const $stagedItemGrid = $form.find('div[data-grid="StagedItemGrid"]');
        const $stagedItemGridControl = FwBrowse.loadGridFromTemplate('StagedItemGrid');
        $stagedItemGridControl.attr('data-tableheight', '735px');

        $stagedItemGrid.empty().append($stagedItemGridControl);

        $stagedItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${type}Id`),
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
            };
            request.pagesize = 20;
        })
        FwBrowse.init($stagedItemGridControl);
        FwBrowse.renderRuntimeHtml($stagedItemGridControl);
        // ----------
        const $checkedOutItemGrid = $form.find('div[data-grid="CheckedOutItemGrid"]');
        const $checkedOutItemGridControl = FwBrowse.loadGridFromTemplate('CheckedOutItemGrid');
        $checkedOutItemGridControl.attr('data-tableheight', '735px');

        $checkedOutItemGrid.empty().append($checkedOutItemGridControl);
        $checkedOutItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkedOutItemGridControl);
        FwBrowse.renderRuntimeHtml($checkedOutItemGridControl);
        // ----------
        const $stageQuantityItemGrid = $form.find('div[data-grid="StageQuantityItemGrid"]');
        const $stageQuantityItemGridControl = FwBrowse.loadGridFromTemplate('StageQuantityItemGrid');
        $stageQuantityItemGrid.empty().append($stageQuantityItemGridControl);
        $stageQuantityItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${type}Id`),
                IncludeZeroRemaining: FwFormField.getValueByDataField($form, 'IncludeZeroRemaining')
            };
            request.pagesize = 20;
            request.orderby = 'ItemOrder';
        });
        FwBrowse.init($stageQuantityItemGridControl);
        FwBrowse.renderRuntimeHtml($stageQuantityItemGridControl);
        // ----------
        const $stageHoldingItemGrid = $form.find('div[data-grid="StageHoldingItemGrid"]');
        const $stageHoldingItemGridControl = FwBrowse.loadGridFromTemplate('StageHoldingItemGrid');
        $stageHoldingItemGrid.empty().append($stageHoldingItemGridControl);
        $stageHoldingItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${type}Id`),
                IncludeZeroRemaining: FwFormField.getValueByDataField($form, 'IncludeZeroRemaining')
            };
            request.pagesize = 20;
            request.orderby = 'ItemOrder';
        });
        FwBrowse.init($stageHoldingItemGridControl);
        FwBrowse.renderRuntimeHtml($stageHoldingItemGridControl);
        // ----------
        const $checkOutPendingItemGrid = $form.find('div[data-grid="CheckOutPendingItemGrid"]');
        const $checkOutPendingItemGridControl = FwBrowse.loadGridFromTemplate('CheckOutPendingItemGrid');
        $checkOutPendingItemGrid.empty().append($checkOutPendingItemGridControl);
        $checkOutPendingItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${type}Id`),
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
            };
            request.pagesize = 20;
            request.orderby = 'ItemOrder';
        });
        FwBrowse.init($checkOutPendingItemGridControl);
        FwBrowse.renderRuntimeHtml($checkOutPendingItemGridControl);
        // ----------
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any): void {
        const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        FwBrowse.search($stagedItemGrid);
        //----------------------------------------------------------------------------------------------
        const $stageHoldingItemGrid = $form.find('[data-name="StageHoldingItemGrid"]');
        FwBrowse.search($stageHoldingItemGrid);
        //----------------------------------------------------------------------------------------------
        const $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
        FwBrowse.search($stageQuantityItemGrid);
        //----------------------------------------------------------------------------------------------
        const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
        FwBrowse.search($checkOutPendingItemGrid);
    };
    //----------------------------------------------------------------------------------------------
    addItemFieldValues($form: any, response: any): void {
        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
    };
    //----------------------------------------------------------------------------------------------
    events = ($form: any) => {
        const errorSound = new Audio(this.errorSoundFileName);
        const successSound = new Audio(this.successSoundFileName);
        const errorMsg = $form.find('.error-msg:not(.qty)');
        const errorMsgQty = $form.find('.error-msg.qty');
        const type = this.Type;

        $form.find('div.quantity-items-tab').on('click', e => {
            //Disable clicking Quantity Items tab w/o an OrderId
            const orderId = FwFormField.getValueByDataField($form, `${type}Id`);
            if (orderId !== '') {
                const $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
                FwBrowse.search($stageQuantityItemGrid);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order.')
            }
        });
        $form.find('div.holding-items-tab').on('click', e => {
            //Disable clicking Quantity Items tab w/o an OrderId
            const orderId = FwFormField.getValueByDataField($form, `${type}Id`);
            if (orderId !== '') {
                const $stageHoldingItemGrid = $form.find('[data-name="StageHoldingItemGrid"]');
                FwBrowse.search($stageHoldingItemGrid);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order.')
            }
        });
        // Refresh grids when navigating to Staging tab
        $form.find('.staging-tab').on('click', e => {
            const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
            const $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');

            FwBrowse.search($stagedItemGrid);
            FwBrowse.search($checkedOutItemGrid);
        });
        // BarCode / I-Code change
        $form.find('[data-datafield="Code"] input').on('keydown', e => {
            if (e.which == 9 || e.which == 13) {
                errorMsg.html('');
                $form.find('div.AddItemToOrder').html('');

                let request: any = {};
                const orderId = FwFormField.getValueByDataField($form, `${type}Id`);
                const code = FwFormField.getValueByDataField($form, 'Code');
                request = {
                    OrderId: orderId,
                    Code: code
                }

                FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
                    if (response.success === true && response.status != 107) {
                        successSound.play();
                        this.addItemFieldValues($form, response);

                        if (this.isPendingItemGridView === false) {
                            const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                            FwBrowse.search($stagedItemGrid);
                        } else {
                            const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
                            FwBrowse.search($checkOutPendingItemGrid);
                        }
                        $form.find('[data-datafield="Code"] input').select();
                    } if (response.status === 107) {
                        successSound.play();
                        this.addItemFieldValues($form, response);
                        FwFormField.setValueByDataField($form, 'Quantity', 0)
                        $form.find('div[data-datafield="Quantity"] input').select();
                    } if (response.ShowAddItemToOrder === true) {
                        errorSound.play();
                        this.showAddItemToOrder = true;
                        this.addItemFieldValues($form, response);
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`)
                    } if (response.ShowAddCompleteToOrder === true) {
                        this.addItemFieldValues($form, response);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StagingCheckoutController.addCompleteToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`)
                    } if (response.ShowUnstage === true) {
                        errorSound.play();
                        this.showAddItemToOrder = true;
                        this.addItemFieldValues($form, response);
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.unstageItem(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Unstage Item</div>`)
                    } if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                        errorSound.play();
                        this.addItemFieldValues($form, response);
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                        $form.find('[data-datafield="Code"] input').select();
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    $form.find('[data-datafield="Code"] input').select();
                }, $form);
            }
        })​;
        //Quantity change
        $form.find('[data-datafield="Quantity"] input').on('keydown', e => {
            if (this.showAddItemToOrder != true) {
                if (e.which == 9 || e.which == 13) {
                    e.preventDefault();
                    errorMsg.html('');
                    $form.find('div.AddItemToOrder').html('');

                    let request: any = {};
                    const orderId = FwFormField.getValueByDataField($form, `${type}Id`);
                    const code = FwFormField.getValueByDataField($form, 'Code');
                    const quantity = +FwFormField.getValueByDataField($form, 'Quantity');
                    request = {
                        OrderId: orderId,
                        Code: code,
                        Quantity: quantity
                    }
                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
                        if (response.success === true) {
                            successSound.play();
                            this.addItemFieldValues($form, response);

                            if (this.isPendingItemGridView === false) {
                                const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                                FwBrowse.search($stagedItemGrid);
                            } else {
                                const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
                                FwBrowse.search($checkOutPendingItemGrid);
                            }
                            FwFormField.setValueByDataField($form, 'Quantity', 0)
                            $form.find('[data-datafield="Code"] input').select();
                        } if (response.ShowAddItemToOrder === true) {
                            errorSound.play();
                            this.addItemFieldValues($form, response);
                            this.showAddItemToOrder = true;
                            errorMsg.html(`<div><span>${response.msg}</span></div>`);
                            $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`)
                        } if (response.ShowAddCompleteToOrder === true) {
                            this.addItemFieldValues($form, response);
                            $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StagingCheckoutController.addCompleteToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`)
                        } if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                            errorSound.play();
                            this.addItemFieldValues($form, response);
                            errorMsg.html(`<div><span>${response.msg}</span></div>`);
                            $form.find('[data-datafield="Code"] input').select();
                        }
                    }, function onError(response) {
                        FwFunc.showError(response);
                        $form.find('[data-datafield="Code"] input').select();
                    }, $form);
                }
            }
        });
        // Order Status
        $form.find('.orderstatus').on('click', e => {
            try {
                const orderInfo: any = {};
                orderInfo.OrderId = FwFormField.getValueByDataField($form, `${type}Id`);
                orderInfo.OrderNumber = FwFormField.getTextByDataField($form, `${type}Id`);
                if (this.Module === 'TransferOut') orderInfo.IsTransferOut = true;
                const mode = 'EDIT';
                const $orderStatusForm = OrderStatusController.openForm(mode, orderInfo);
                FwModule.openSubModuleTab($form, $orderStatusForm);
                jQuery('.tab.submodule.active').find('.caption').html(`${type} Status`);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // Move Staged Item to Out
        $form.find('.right-arrow').on('click', e => {
            this.moveStagedItemToOut($form);
            $form.find('.right-arrow').addClass('arrow-clicked');
            $form.find('.left-arrow').removeClass('arrow-clicked');
        });
        // Move Out Item to Staged
        $form.find('.left-arrow').on('click', e => {
            this.moveOutItemToStaged($form);
            $form.find('.left-arrow').addClass('arrow-clicked');
            $form.find('.right-arrow').removeClass('arrow-clicked');
        });
        // Complete Checkout Contract
        $form.find('.complete-checkout-contract').on('click', e => {
            this.completeCheckOutContract($form, e);
        });
        // Create Contract
        $form.find('.createcontract').on('click', e => {
            this.createContract($form, e);
        });
        //Options button
        $form.find('.options-button').on('click', e => {
            $form.find('.option-list').toggle();
        });
        //IncludeZeroRemaining Checkbox functionality
        $form.find('[data-datafield="IncludeZeroRemaining"] input').on('change', e => {
            const $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
            $stageQuantityItemGrid.data('ondatabind', function (request) {
                const orderId = FwFormField.getValueByDataField($form, `${type}Id`);
                const includeZeroRemaining = FwFormField.getValueByDataField($form, 'IncludeZeroRemaining');
                request.uniqueids = {
                    OrderId: orderId,
                    IncludeZeroRemaining: includeZeroRemaining
                };
                request.pagesize = 20;
                request.orderby = 'ItemOrder';
            });
            FwBrowse.search($stageQuantityItemGrid);
        });
        // Grid view toggle
        $form.find('.grid-view-radio input').on('change', e => {
            const $target = jQuery(e.currentTarget);
            const gridView = $target.val();
            const stagedItemGridContainer = $form.find('.staged-item-grid');
            const checkOutPendingItemGridContainier = $form.find('.pending-item-grid');
            const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
            const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
            const orderId = FwFormField.getValueByDataField($form, `${type}Id`);
            if (orderId !== '') {
                switch (gridView) {
                    case 'STAGE':
                        checkOutPendingItemGridContainier.hide();
                        stagedItemGridContainer.show();
                        FwBrowse.search($stagedItemGrid);
                        this.isPendingItemGridView = false;
                        break;
                    case 'PENDING':
                        stagedItemGridContainer.hide();
                        checkOutPendingItemGridContainier.show();
                        FwBrowse.search($checkOutPendingItemGrid);
                        this.isPendingItemGridView = true;
                        break;
                }
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order before switching views.')
            }
        });
        // Partial Contract Inputs
        $form.find('.partial-contract-inputs input').on('keydown', e => {
            const barCodeFieldValue = $form.find('.partial-contract-barcode input').val();

            if (e.which == 13 && barCodeFieldValue !== '') {
                if ($form.find('.right-arrow').hasClass('arrow-clicked')) {
                    this.moveStagedItemToOut($form);
                } else if ($form.find('.left-arrow').hasClass('arrow-clicked')) {
                    this.moveOutItemToStaged($form);
                } else {
                    this.moveStagedItemToOut($form);
                    $form.find('.right-arrow').addClass('arrow-clicked');
                    $form.find('.left-arrow').removeClass('arrow-clicked');
                }
            }
        });
        // Select None
        $form.find('.selectnone').on('click', e => {
            let request: any = {};
            const orderId = FwFormField.getValueByDataField($form, `${type}Id`);
            request.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/stagequantityitem/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                errorMsgQty.html('');
                if (response.success === false) {
                    errorSound.play();
                    errorMsgQty.html(`<div><span>${response.msg}</span></div>`);
                } else {
                    successSound.play();
                    const $stageQuantityItemGrid = $form.find('div[data-name="StageQuantityItemGrid"]');
                    FwBrowse.search($stageQuantityItemGrid);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
        // Select All
        $form.find('.selectall').on('click', e => {

            let request: any = {};
            const orderId = FwFormField.getValueByDataField($form, `${type}Id`);
            request.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/stagequantityitem/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                errorMsgQty.html('');
                if (response.success === false) {
                    errorSound.play();
                    errorMsgQty.html(`<div><span>${response.msg}</span></div>`);
                } else {
                    successSound.play();
                    const $stageQuantityItemGrid = $form.find('div[data-name="StageQuantityItemGrid"]');
                    FwBrowse.search($stageQuantityItemGrid);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        switch (validationName) {
            case 'OrderValidation':
                request.miscfields = {
                    Staging: true,
                    StagingWarehouseId: warehouse.warehouseid
                };
                break;
        };
    }
    //----------------------------------------------------------------------------------------------
    addItemToOrder(element: any): void {
        this.showAddItemToOrder = false;
        const $element = jQuery(element);
        const $form = jQuery($element).closest('.fwform');

        let request: any = {};
        const orderId = FwFormField.getValueByDataField($form, 'OrderId');
        const code = FwFormField.getValueByDataField($form, 'Code');
        const quantity = +FwFormField.getValueByDataField($form, 'Quantity');
        if (quantity != 0) {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true,
                Quantity: quantity
            }
        } else {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true
            }
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
            try {
                if (this.isPendingItemGridView === false) {
                    const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                    FwBrowse.search($stagedItemGrid);
                } else {
                    const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
                    FwBrowse.search($checkOutPendingItemGrid);
                }
                $form.find('.error-msg:not(.qty)').html('');
                $form.find('div.AddItemToOrder').html('');
                const successSound = new Audio(this.successSoundFileName);
                successSound.play();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
        $form.find('[data-datafield="Code"] input').select();
    }
    //----------------------------------------------------------------------------------------------
    unstageItem(element: any): void {
        this.showAddItemToOrder = false;
        const $element = jQuery(element);
        const $form = jQuery($element).closest('.fwform');

        let request: any = {};
        const orderId = FwFormField.getValueByDataField($form, 'OrderId');
        const code = FwFormField.getValueByDataField($form, 'Code');
        request = {
            OrderId: orderId,
            Code: code,
            UnstageItem: true,
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
            try {
                if (this.isPendingItemGridView === false) {
                    const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                    FwBrowse.search($stagedItemGrid);
                } else {
                    const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
                    FwBrowse.search($checkOutPendingItemGrid);
                }
                $form.find('.error-msg:not(.qty)').html('');
                $form.find('div.AddItemToOrder').html('');
                const successSound = new Audio(this.successSoundFileName);
                successSound.play();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
        $form.find('[data-datafield="Code"] input').select();
    }
    //----------------------------------------------------------------------------------------------
    addCompleteToOrder(element: any): void {
        this.showAddItemToOrder = false;
        const $element = jQuery(element);
        const $form = jQuery($element).closest('.fwform');

        let request: any = {};
        const orderId = FwFormField.getValueByDataField($form, 'OrderId');
        const code = FwFormField.getValueByDataField($form, 'Code');
        const quantity = +FwFormField.getValueByDataField($form, 'Quantity');
        if (quantity != 0) {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true,
                Quantity: quantity
            }
        } else {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true
            }
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
            try {
                if (this.isPendingItemGridView === false) {
                    const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                    FwBrowse.search($stagedItemGrid);
                } else {
                    const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
                    FwBrowse.search($checkOutPendingItemGrid);
                }
                $form.find('.error-msg:not(.qty)').html('');
                $form.find('div.AddItemToOrder').html('');
                const successSound = new Audio(this.successSoundFileName);
                successSound.play();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
        $form.find('[data-datafield="Code"] input').select();
    }
    //----------------------------------------------------------------------------------------------
    addLegend($form: any, $grid): void {
        FwBrowse.addLegend($grid, 'Complete', '#8888ff');
        FwBrowse.addLegend($grid, 'Kit', '#03d337');
        FwBrowse.addLegend($grid, 'Exchange', '#a0cdb4');
        FwBrowse.addLegend($grid, 'Sub Vendor', '#ffb18c');
        FwBrowse.addLegend($grid, 'Consignor', '#8080ff');
        FwBrowse.addLegend($grid, 'Truck', '#ffff00');
        FwBrowse.addLegend($grid, 'Suspended', '#0000a0');
        FwBrowse.addLegend($grid, 'Lost', '#ff8080');
        FwBrowse.addLegend($grid, 'Sales', '#ff0080');
        FwBrowse.addLegend($grid, 'Not Yet Staged or Still Out', '#ff0000');
        FwBrowse.addLegend($grid, 'Too Many Staged', '#00ff80');
    };
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="stagingcheckoutform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="${this.caption}" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="${this.Module}Controller">
          <div id="checkoutform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="stagingtab" class="tab staging-tab" data-tabpageid="stagingtabpage" data-caption="${this.caption}"></div>
              <div data-type="tab" id="quantityitemtab" class="tab quantity-items-tab" data-tabpageid="quantityitemtabpage" data-caption="Quantity Items" style="display:none;"></div>
              <div data-type="tab" id="holdingitemtab" class="tab holding-items-tab" data-tabpageid="holdingitemtabpage" data-caption="Holding" style="display:none;"></div>
              <div data-type="tab" id="serialitemtab" class="tab serial-items-tab" data-tabpageid="serialitemtabpage" data-caption="Serial Items" style="display:none;"></div>
              <div data-type="tab" id="usagetab" class="tab usage-tab" data-tabpageid="usagetabpage" data-caption="Usage" style="display:none;"></div>
              <div data-type="tab" id="consignmenttab" class="tab consignment-tab" data-tabpageid="consignmenttabpage" data-caption="Consignment" style="display:none;"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="stagingtabpage" class="tabpage" data-tabid="stagingtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="${this.caption}">
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:1 1 850px;">
                          <div class="flexrow">
                            ${this.Module == 'StagingCheckout' ?
                '<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-formbeforevalidate="beforeValidate" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>'
                : '<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Transfer No." data-datafield="TransferId" data-displayfield="TransferNumber" data-validationname="TransferOrderValidation" style="flex:0 1 175px;"></div>'}
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:1 1 250px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:1 1 300px;"></div>
                            ${this.Module == 'StagingCheckout' ?
                '<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-enabled="false" style="flex:1 1 300px;"></div>' : ''}
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Location" data-datafield="Location" data-enabled="false" style="flex:1 1 300px;"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-visible="false" data-enabled="false" style="flex:1 1 175px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer" data-control="FwContainer" data-type="section" data-caption="Items">
                        <div class="flexrow">
                          <div class="flexcolumn" style="flex:1 1 300px;">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Bar Code / I-Code" data-datafield="Code" style="flex:0 1 320px;"></div>
                              <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield quantity clearable" data-caption="Quantity" data-datafield="Quantity" style="flex:0 1 100px;"></div>
                            </div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 850px;">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="I-Code" data-enabled="false" data-datafield="ICode" style="flex:1 1 300px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Description" data-enabled="false" data-datafield="InventoryDescription" style="flex:1 1 400px;"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Ordered" data-enabled="false" data-datafield="QuantityOrdered" style="flex:0 1 100px;"></div>
                              <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Sub" data-enabled="false" data-datafield="QuantitySub" style="flex:0 1 100px;"></div>
                              <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Out" data-enabled="false" data-datafield="QuantityOut" style="flex:0 1 100px;"></div>
                              <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Staged" data-enabled="false" data-datafield="QuantityStaged" style="flex:0 1 100px;"></div>
                              <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Remaining" data-enabled="false" data-datafield="QuantityRemaining" style="flex:0 1 100px;"></div>
                              <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield grid-view-radio" data-caption="" data-datafield="GridView" style="flex:1 1 250px;">
                                <div data-value="STAGE" class="staged-view" data-caption="View Staged" style="margin-top:15px;"></div>
                                <div data-value="PENDING" class="pending-item-view" data-caption="View Pending Items" style="margin-top:-4px;"></div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow error-msg"></div>
                      <div class="formrow AddItemToOrder"></div>
                      <div class="flexrow">
                        <div class="flexcolumn summaryview">
                          <div class="flexrow staged-item-grid">
                            <div data-control="FwGrid" data-grid="StagedItemGrid" data-securitycaption="Staged Items"></div>
                          </div>
                          <div class="flexrow pending-item-grid">
                            <div class="pending-item-grid" data-control="FwGrid" data-grid="CheckOutPendingItemGrid" data-securitycaption=""></div>
                          </div>
                          <div class="flexrow original-buttons" style="display:flex;justify-content:space-between;">
                            <div class="orderstatus fwformcontrol" data-type="button" style="flex:0 1 132px; margin-left:8px; text-align:center;">${this.Module == 'StagingCheckout' ? 'Order Status' : 'Transfer Status'}</div>
                            <div class="createcontract" data-type="btnmenu" style="flex:0 1 200px;margin-right:7px;" data-caption="${this.Module == 'StagingCheckout' ? 'Create Contract' : 'Create Manifest'}"></div>
                          </div>
                        </div>
                        <div class="flexcolumn partial-contract" style="max-width:125px;justify-content:center;">
                          <button type="submit" class="dbl-angle right-arrow"><img src="theme/images/icons/integration/dbl-angle-right.svg" alt="Add" /></button>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield partial-contract-inputs partial-contract-barcode clearable" data-caption="Bar Code / I-Code" data-datafield="" style="margin-top:30px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield partial-contract-inputs partial-contract-quantity clearable" data-caption="Quantity" data-datafield="" style="max-width:72px;"></div>
                          <button type="submit" class="dbl-angle left-arrow" style="margin-top:40px;"><img src="theme/images/icons/integration/dbl-angle-left.svg" alt="Remove" /></button>
                        </div>
                        <div class="flexcolumn partial-contract">
                          <div class="flexrow">
                            <div data-control="FwGrid" data-grid="CheckedOutItemGrid" data-securitycaption="Contract Items"></div>
                          </div>
                          <div class="flexrow" style="align-items:flex-end;">
                            <div class="fwformcontrol complete-checkout-contract" data-type="button" style="max-width:140px;">${this.Module == 'StagingCheckout' ? 'Create Contract' : 'Create Manifest'}</div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--QUANTITY ITEM  PAGE-->
              <div data-type="tabpage" id="quantityitemtabpage" class="tabpage" data-tabid="quantityitemtab">
                <div class="flexpage">
                  <div class="flexrow error-msg qty"></div>
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="StageQuantityItemGrid" data-securitycaption=""></div>
                  </div>
                  <div class="formrow">
                    <div class="fwformcontrol options-button" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                    <div class="fwformcontrol selectall" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                    <div class="fwformcontrol selectnone" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                  </div>
                  <div class="formrow option-list" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show items with zero Remaining" data-datafield="IncludeZeroRemaining"></div>
                  </div>
                </div>
              </div>
              <!--HOLDING PAGE-->
              <div data-type="tabpage" id="holdingitemtabpage" class="tabpage" data-tabid="holdingitemtab">
                <div class="flexpage">
                  <div class="flexrow error-msg qty"></div>
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="StageHoldingItemGrid" data-securitycaption=""></div>
                  </div>
                  <div class="formrow">
                    <div class="fwformcontrol options-button-holding" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                    <div class="fwformcontrol selectall-holding" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                    <div class="fwformcontrol selectnone-holding" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                  </div>
                  <div class="formrow option-list-holding" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show ..." data-datafield=""></div>
                  </div>
                </div>
              </div>
              <!--SERIAL ITEM PAGE-->
              <div data-type="tabpage" id="serialitemtabpage" class="tabpage" data-tabid="serialitemtab">
                <div class="flexpage">
                  <!--<div class="flexrow error-msg serial"></div>-->
                  <div class="flexrow">
                    <!--<div data-control="FwGrid" data-grid="StageQuantityItemGrid" data-securitycaption=""></div>-->
                  </div>
                  <div class="formrow">
                    <div class="fwformcontrol options-button" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                    <div class="fwformcontrol selectall" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                    <div class="fwformcontrol selectnone" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                  </div>
                  <div class="formrow option-list" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show items with zero Remaining" data-datafield=""></div>
                  </div>
                </div>
              </div>
              <!--USAGE PAGE-->
              <div data-type="tabpage" id="usagetabpage" class="tabpage" data-tabid="usagetab">
                <div class="flexpage">
                  <!--<div class="flexrow error-msg usage"></div>-->
                  <div class="flexrow">
                    <!--<div data-control="FwGrid" data-grid="StageQuantityItemGrid" data-securitycaption=""></div>-->
                  </div>
                  <div class="formrow">
                    <div class="fwformcontrol options-button" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                    <div class="fwformcontrol selectall" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                    <div class="fwformcontrol selectnone" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                  </div>
                  <div class="formrow option-list" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show items with zero Remaining" data-datafield=""></div>
                  </div>
                </div>
              </div>
              <!--CONSIGNMENT PAGE-->
              <div data-type="tabpage" id="consignmenttabpage" class="tabpage" data-tabid="consignmenttab">
                <div class="flexpage">
                  <!--<div class="flexrow error-msg consign"></div>-->
                  <div class="flexrow">
                    <!--<div data-control="FwGrid" data-grid="StageQuantityItemGrid" data-securitycaption=""></div>-->
                  </div>
                  <div class="formrow">
                    <div class="fwformcontrol options-button" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                    <div class="fwformcontrol selectall" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                    <div class="fwformcontrol selectnone" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                  </div>
                  <div class="formrow option-list" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show items with zero Remaining" data-datafield=""></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
}