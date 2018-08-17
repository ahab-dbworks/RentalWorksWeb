﻿routes.push({ pattern: /^module\/checkout$/, action: function (match: RegExpExecArray) { return StagingCheckoutController.getModuleScreen(); } });

class StagingCheckout {
    Module: string = 'StagingCheckout';
    showAddItemToOrder: boolean = false;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    contractId: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {}, self = this;
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Staging / Check-Out', false, 'FORM', true);
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
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-StagingCheckoutForm').html());
        $form = FwModule.openForm($form, mode);

        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        $form.find('[data-datafield="WarehouseId"]').hide();
        $form.find('.orderstatus').hide();
        $form.find('.createcontract').hide();
        $form.find('.partial-contract').hide();

        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        this.getSoundUrls($form);
        this.getOrder($form);
        if (typeof parentmoduleinfo !== 'undefined') {
            $form.find('div[data-datafield="OrderId"] input.fwformfield-value').val(parentmoduleinfo.OrderId);
            $form.find('div[data-datafield="OrderId"] input.fwformfield-text').val(parentmoduleinfo.OrderNumber);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', parentmoduleinfo.WarehouseId, parentmoduleinfo.Warehouse);
            FwFormField.setValueByDataField($form, 'Description', parentmoduleinfo.description);
            jQuery($form.find('[data-datafield="OrderId"]')).trigger('change');
        }

        //$form.find('.rentalview').hide();
        //$form.find('.salesview').hide();

        //$form.find('div[data-datafield="TaxOptionId"]').data('onchange', function ($tr) {
        //    FwFormField.setValue($form, 'div[data-datafield=""]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
        //});
        $form.find('div[data-datafield="OrderId"] input').focus();
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getSoundUrls = ($form): void => {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    getOrder($form: JQuery): void {
        const order = $form.find('[data-datafield="OrderId"]');
        const maxPageSize = 9999;

        order.on('change', function () {
            try {
                let orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();
                let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                FwFormField.setValueByDataField($form, 'Quantity', '');
                FwFormField.setValueByDataField($form, 'Code', '');
                $form.find('.error-msg').html('');
                FwAppData.apiMethod(true, 'GET', "api/v1/order/" + orderId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwFormField.setValueByDataField($form, 'Description', response.Description);
                    FwFormField.setValueByDataField($form, 'Location', response.Location);
                    FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
                    //FwFormField.setValueByDataField($form, 'WarehouseId', response.WarehouseId);
                    //FwFormField.setValueByDataField($form, 'Status', response.Status);
                    //FwFormField.setValueByDataField($form, 'EstimatedStartDate', response.EstimatedStartDate);
                    //FwFormField.setValueByDataField($form, 'EstimatedStartTime', response.EstimatedStartTime);
                    //FwFormField.setValueByDataField($form, 'EstimatedStopDate', response.EstimatedStopDate);
                    //FwFormField.setValueByDataField($form, 'EstimatedStopTime', response.EstimatedStopTime);

                    //var rental = response.Rental;
                    //var sales = response.Sales;
                    //if (rental === false && sales === false) {
                    //    $form.find('div[data-value="Details"]').hide();
                    //} else {
                    //    $form.find('div[data-value="Details"]').show();
                    //}

                    //if (rental === true) {
                    //    $form.find('.rentalview').show();
                    //} else {
                    //    $form.find('.rentalview').hide();
                    //}

                    //if (sales === true) {
                    //    $form.find('.salesview').show();
                    //} else {
                    //    $form.find('.salesview').hide();
                    //}

                    //$form.find('.details').hide();
                }, null, $form);

                var $stagedItemGridControl: any;
                $stagedItemGridControl = $form.find('[data-name="StagedItemGrid"]');
                $stagedItemGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId,
                        WarehouseId: warehouseId
                    }
                    request.pagesize = maxPageSize;
                })
                FwBrowse.search($stagedItemGridControl);

                //var $orderStatusRentalDetailGridControl: any;
                //$orderStatusRentalDetailGridControl = $form.find('[data-name="OrderStatusRentalDetailGrid"]');
                //$orderStatusRentalDetailGridControl.data('ondatabind', function (request) {
                //    request.uniqueids = {
                //        OrderId: orderId,
                //        RecType: "R"
                //    }
                //    request.pagesize = maxPageSize;
                //})
                //FwBrowse.search($orderStatusRentalDetailGridControl);

                //var $orderStatusSalesDetailGridControl: any;
                //$orderStatusSalesDetailGridControl = $form.find('[data-name="OrderStatusSalesDetailGrid"]');
                //$orderStatusSalesDetailGridControl.data('ondatabind', function (request) {
                //    request.uniqueids = {
                //        OrderId: orderId,
                //        RecType: "S"
                //    }
                //    request.pagesize = maxPageSize;
                //})
                //FwBrowse.search($orderStatusSalesDetailGridControl);

                setTimeout(function () {
                    var $trs = $form.find('.ordersummarygrid tr.viewmode');

                    var $contractpeek = $form.find('.outcontract, .incontract');
                    $contractpeek.attr('data-browsedatafield', 'ContractId');

                    for (var i = 0; i <= $trs.length; i++) {
                        var $rectype = jQuery($trs[i]).find('[data-browsedatafield="RecTypeDisplay"]');
                        var recvalue = $rectype.attr('data-originalvalue');
                        var $validationfield = jQuery($trs[i]).find('[data-browsedatafield="InventoryId"]');

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
            //FwFormField.disable($form.find('div[data-datafield="OrderId"]'));
            $form.find('.orderstatus').show();
            $form.find('.createcontract').show();
        });
    };
    //----------------------------------------------------------------------------------------------
    addButtonMenu($form: JQuery): void {
        let $createPartialContract, $createContract, $buttonmenu, menuOptions: Array<string> = [];
        $buttonmenu = $form.find('.createcontract[data-type="btnmenu"]');
        $createContract = FwMenu.generateButtonMenuOption('Create Contract');
        $createPartialContract = FwMenu.generateButtonMenuOption('Create Partial Contract');

        $createContract.on('click', e => {
            e.stopPropagation();
            $form.find('.createcontract').click();
        });

        $createPartialContract.on('click', e => {
            e.stopPropagation();
            this.startPartialCheckoutItems($form);
        });

        menuOptions.push($createContract, $createPartialContract);

        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    };
    //----------------------------------------------------------------------------------------------
    startPartialCheckoutItems = ($form: JQuery): void => {
        let contractId, request: any = {};
        $form.find('.orderstatus').hide();
        $form.find('.createcontract').hide();
        $form.find('.partial-contract-hide').hide();
        $form.find('.partial-contract').show();
        $form.find('.flexrow').css('max-width', '2200px');

        request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/startcheckoutcontract`, request, FwServices.defaultTimeout, response => {
            try {
                this.contractId = response.ContractId;
                var $checkedOutItemGridControl: any;
                $checkedOutItemGridControl = $form.find('[data-name="CheckedOutItemGrid"]');
                $checkedOutItemGridControl.data('ondatabind', function (request) {
                    request.orderby = 'OrderBy';
                    request.pagesize = 10;
                    request.uniqueids = {
                        ContractId: contractId,
                    }
                })
                FwBrowse.search($checkedOutItemGridControl);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, null);
    };
    //----------------------------------------------------------------------------------------------
    moveStagedItemToOut($form: JQuery): void {
        let $selectedCheckBoxes, $stagedItemGrid, orderId, barCodeFieldValue, quantityFieldValue, barCode, iCode, quantity, orderItemId, $checkedOutItemGrid, request: any = {};

        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
        $selectedCheckBoxes = $stagedItemGrid.find('.cbselectrow:checked');
        barCodeFieldValue = $form.find('.partial-contract-barcode input').val();
        quantityFieldValue = $form.find('.partial-contract-quantity input').val();
        orderId = FwFormField.getValueByDataField($form, 'OrderId');

        if (barCodeFieldValue !== '' && quantityFieldValue !== '') {
            request.ContractId = this.contractId;
            request.Quantity = quantityFieldValue
            request.Code = barCodeFieldValue;
            request.OrderId = orderId
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/movestageditemtoout`, request, FwServices.defaultTimeout, response => {
                // need error handling here. API is 200 regardless of what i send
            }, null, null);
        } else {
            if ($selectedCheckBoxes.length !== 0) {
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    barCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="BarCode"]').attr('data-originalvalue');
                    iCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="ICode"]').attr('data-originalvalue');
                    quantity = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Quantity"]').attr('data-originalvalue');
                    orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');

                    request.OrderId = orderId
                    request.ContractId = this.contractId;
                    request.Quantity = quantity

                    if (barCode !== '') {
                        request.Code = barCode;
                    } else {
                        request.Code = iCode;
                        request.OrderItemId = orderItemId;
                    }
                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/movestageditemtoout`, request, FwServices.defaultTimeout, response => {
// need error handling
                    }, null, null);
                }
            } else {
                FwNotification.renderNotification('WARNING', 'Please select one or more rows in Stage Items or use Bar Code input in order to perform this function.');
            }
console.log('request: ', request);
        }
        $form.find('.partial-contract-barcode input').val('');
        $form.find('.partial-contract-quantity input').val('');
        FwBrowse.search($checkedOutItemGrid);
        FwBrowse.search($stagedItemGrid);
    };
    //----------------------------------------------------------------------------------------------
    moveOutItemToStaged($form: JQuery): void {
        let $selectedCheckBoxes, $stagedItemGrid, orderId, barCodeFieldValue, barCode, iCode, quantityFieldValue, quantity, orderItemId, $checkedOutItemGrid, request: any = {};

        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
        $selectedCheckBoxes = $checkedOutItemGrid.find('.cbselectrow:checked');
        barCodeFieldValue = $form.find('.partial-contract-barcode').val();
        quantityFieldValue = $form.find('.partial-contract-quantity').val();
        orderId = FwFormField.getValueByDataField($form, 'OrderId');

        if (barCodeFieldValue !== '' && quantityFieldValue !== '') {
            request.ContractId = this.contractId;
            request.Quantity = quantityFieldValue
            request.Code = barCodeFieldValue;
            request.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/moveoutitemtostaged`, request, FwServices.defaultTimeout, response => {
                // need error handling here. API is 200 regardless of what i send
            }, null, null);
        } else {
            if ($selectedCheckBoxes.length !== 0) {
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    barCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="BarCode"]').attr('data-originalvalue');
                    iCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="ICode"]').attr('data-originalvalue');
                    quantity = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Quantity"]').attr('data-originalvalue');
                    orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');

                    request.OrderId = orderId;
                    request.ContractId = this.contractId;
                    request.Quantity = quantity;

                    if (barCode !== '') {
                        request.Code = barCode;
                    } else {
                        request.Code = iCode;
                        request.OrderItemId = orderItemId;
                    }
                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/moveoutitemtostaged`, request, FwServices.defaultTimeout, response => {
                        // need error handling
                    }, null, null);
                }
            } else {
                FwNotification.renderNotification('WARNING', 'Please select one or more rows in Contract Items or use Bar Code input in order to perform this function.');
            }
console.log('request: ', request);
        }
        $form.find('.partial-contract-barcode input').val('');
        $form.find('.partial-contract-quantity input').val('');
        FwBrowse.search($checkedOutItemGrid);
        FwBrowse.search($stagedItemGrid);
    };
    //----------------------------------------------------------------------------------------------
    completeCheckOutContract($form: JQuery): void {
        let $stagedItemGrid;
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');

        if (this.contractId) {
            FwAppData.apiMethod(true, 'POST', `pi/v1/checkout/completecheckoutcontract/${this.contractId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                try {
                    let contractInfo: any = {}, $contractForm;
                    contractInfo.ContractId = response.ContractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);
                    $form.find('.fwformfield').find('input').val('');
                    FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
                    $form.find('.orderstatus').hide();
                    $form.find('.createcontract').hide();
                    FwFormField.enable($form.find('div[data-datafield="OrderId"]'));
                    $form.find('[data-datafield="Code"] input').select();
                    $form.find('div[data-name="StagedItemGrid"] tr.viewmode').empty();
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, null);
        } else {
            FwNotification.renderNotification('WARNING', 'Please check-out items before attemping to perform this function.');
        }
    };
    //----------------------------------------------------------------------------------------------
    createContract($form: JQuery): void {
        let orderId, $stagedItemGrid, request: any = {};
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
// error handling needed. what if no items?
        request.OrderId = orderId;
        FwAppData.apiMethod(true, 'POST', "api/v1/checkout/checkoutallstaged", request, FwServices.defaultTimeout, function onSuccess(response) {
            let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            try {
                let contractInfo: any = {}, $contractForm;
                contractInfo.ContractId = response.ContractId;
                $contractForm = ContractController.loadForm(contractInfo);
                FwModule.openSubModuleTab($form, $contractForm);
                $form.find('.fwformfield').find('input').val('');
                FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
                $form.find('.orderstatus').hide();
                $form.find('.createcontract').hide();
                FwFormField.enable($form.find('div[data-datafield="OrderId"]'));
                $form.find('[data-datafield="Code"] input').select();
                $form.find('div[data-name="StagedItemGrid"] tr.viewmode').empty();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, null);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any): void {
        let $stagedItemGrid: any, $stagedItemGridControl: any;
        let $checkedOutItemGrid: any, $checkedOutItemGridControl: any;
        let orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();
        let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        let maxPageSize = 250;
        //----------------------------------------------------------------------------------------------
        $stagedItemGrid = $form.find('div[data-grid="StagedItemGrid"]');
        $stagedItemGridControl = jQuery(jQuery('#tmpl-grids-StagedItemGridBrowse').html());
        $stagedItemGrid.empty().append($stagedItemGridControl);
        $stagedItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: orderId,
                WarehouseId: warehouseId
            };
            request.pagesize = maxPageSize;
        })
        FwBrowse.init($stagedItemGridControl);
        FwBrowse.renderRuntimeHtml($stagedItemGridControl);
        //----------------------------------------------------------------------------------------------
        $checkedOutItemGrid = $form.find('div[data-grid="CheckedOutItemGrid"]');
        $checkedOutItemGridControl = jQuery(jQuery('#tmpl-grids-CheckedOutItemGridBrowse').html());
        $checkedOutItemGrid.empty().append($checkedOutItemGridControl);
        $checkedOutItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($checkedOutItemGridControl);
        FwBrowse.renderRuntimeHtml($checkedOutItemGridControl);
        //----------------------------------------------------------------------------------------------
        //this.addLegend($form, $stagedItemGrid);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any): void {
        let $stagedItemGrid;
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        FwBrowse.search($stagedItemGrid);
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
        let errorSound, successSound, self = this;
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);

        // BarCode / I-Code change
        $form.find('[data-datafield="Code"] input').on('keydown', e => {
            if (e.which == 9 || e.which == 13) {
                let code, orderId, $stagedItemGrid, request: any = {};

                $form.find('.error-msg').html('');
                $form.find('div.AddItemToOrder').html('');
                orderId = FwFormField.getValueByDataField($form, 'OrderId');
                code = FwFormField.getValueByDataField($form, 'Code');
                $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');

                request = {
                    OrderId: orderId,
                    Code: code,
                }

                FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true && response.status != 107) {
                        successSound.play();
                        self.addItemFieldValues($form, response);
                        FwBrowse.search($stagedItemGrid);
                        $form.find('[data-datafield="Code"] input').select();
                    } if (response.status === 107) {
                        successSound.play();
                        self.addItemFieldValues($form, response);
                        FwFormField.setValueByDataField($form, 'Quantity', 0)
                        $form.find('div[data-datafield="Quantity"] input').select();
                    } if (response.ShowAddItemToOrder === true) {
                        errorSound.play();
                        self.showAddItemToOrder = true;
                        self.addItemFieldValues($form, response);
                        $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`)
                    } if (response.ShowAddCompleteToOrder === true) {
                        self.addItemFieldValues($form, response);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StagingCheckoutController.addCompleteToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`)
                    } if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                        errorSound.play();
                        self.addItemFieldValues($form, response);
                        $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                        $form.find('[data-datafield="Code"] input').select();
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    $form.find('[data-datafield="Code"] input').select();
                }, $form);
            }
        })​;​

        //Quantity change
        $form.find('[data-datafield="Quantity"] input').on('keydown', e => {
            if (self.showAddItemToOrder != true) {
                if (e.which == 9 || e.which == 13) {
                    e.preventDefault();
                    let code, orderId, quantity, $stagedItemGrid;

                    $form.find('.error-msg').html('');
                    $form.find('div.AddItemToOrder').html('');
                    orderId = FwFormField.getValueByDataField($form, 'OrderId');
                    code = FwFormField.getValueByDataField($form, 'Code');
                    quantity = +FwFormField.getValueByDataField($form, 'Quantity');
                    $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');

                    let request: any = {};
                    request = {
                        OrderId: orderId,
                        Code: code,
                        Quantity: quantity
                    }
                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        if (response.success === true) {
                            successSound.play();
                            self.addItemFieldValues($form, response);
                            FwBrowse.search($stagedItemGrid);
                            FwFormField.setValueByDataField($form, 'Quantity', 0)
                            $form.find('[data-datafield="Code"] input').select();
                        } if (response.ShowAddItemToOrder === true) {
                            errorSound.play();
                            self.addItemFieldValues($form, response);
                            self.showAddItemToOrder = true;
                            $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                            $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`)
                        } if (response.ShowAddCompleteToOrder === true) {
                            self.addItemFieldValues($form, response);
                            $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StagingCheckoutController.addCompleteToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`)
                        } if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                            errorSound.play();
                            self.addItemFieldValues($form, response);
                            $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
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
            let $orderStatusForm;
            try {
                var mode = 'EDIT';
                var orderInfo: any = {};
                orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
                orderInfo.OrderNumber = FwFormField.getTextByDataField($form, 'OrderId');
                $orderStatusForm = OrderStatusController.openForm(mode, orderInfo);
                FwModule.openSubModuleTab($form, $orderStatusForm);
                jQuery('.tab.submodule.active').find('.caption').html('Order Status');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // Move Staged Item to Out
        $form.find('.right-arrow').on('click', e => {
            this.moveStagedItemToOut($form);
        });
        // Move Out Item to Staged
        $form.find('.left-arrow').on('click', e => {
            this.moveOutItemToStaged($form);
        });
        // Complete Checkout Contract
        $form.find('.complete-checkout-contract').on('click', e => {
            this.completeCheckOutContract($form);
        });
        // Create Contract
        $form.find('.createcontract').on('click', e => {
            this.createContract($form);
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
                    StagingWarehouseId: warehouse.warehouseid,
                };
                break;
        };
    }
    //----------------------------------------------------------------------------------------------
    addItemToOrder(element: any): void {
        this.showAddItemToOrder = false;
        let code, $form, $element, orderId, quantity, $stagedItemGrid, successSound, request: any = {};
        $element = jQuery(element);
        $form = jQuery($element).closest('.fwform'); 
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        code = FwFormField.getValueByDataField($form, 'Code');
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        quantity = +FwFormField.getValueByDataField($form, 'Quantity');
        successSound = new Audio(this.successSoundFileName);

        if (quantity != 0) {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true,
                Quantity: quantity,
            }
        } else {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true,
            }
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                FwBrowse.search($stagedItemGrid);
                $form.find('.error-msg').html('');
                $form.find('div.AddItemToOrder').html('');
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
        let code, $form, $element, orderId, quantity, $stagedItemGrid, successSound, request: any = {};
        $element = jQuery(element);
        $form = jQuery($element).closest('.fwform');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        code = FwFormField.getValueByDataField($form, 'Code');
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        successSound = new Audio(this.successSoundFileName);

        if (quantity != 0) {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true,
                Quantity: quantity,
            }
        } else {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true,
            }
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                FwBrowse.search($stagedItemGrid);
                $form.find('.error-msg').html('');
                $form.find('div.AddItemToOrder').html('');
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
}
var StagingCheckoutController = new StagingCheckout();