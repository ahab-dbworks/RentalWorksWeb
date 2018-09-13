routes.push({ pattern: /^module\/checkout$/, action: function (match: RegExpExecArray) { return StagingCheckoutController.getModuleScreen(); } });

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
                }, null, $form);

                //----------------------------------------------------------------------------------------------
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

                var $stageQuantityItemGridControl: any;
                $stageQuantityItemGridControl = $form.find('[data-name="StageQuantityItemGrid"]');
                $stageQuantityItemGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId
                    }
                    request.pagesize = 10;
                })
                FwBrowse.search($stageQuantityItemGridControl);
                //----------------------------------------------------------------------------------------------
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
            FwFormField.disable($form.find('div[data-datafield="OrderId"]'));
            $form.find('.orderstatus').show();
            $form.find('.createcontract').show();
            $form.find('.original-buttons').show();
            $form.find('[data-datafield="Code"] input').focus();
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
        $form.find('.error-msg').html('');
        const MAX_PAGE_SIZE = 9999;
        let requestBody: any = {}, $checkedOutItemGridControl, $stagedItemGridControl: any;
        requestBody.OrderId = FwFormField.getValueByDataField($form, 'OrderId');

        $form.find('.orderstatus').hide();
        $form.find('.createcontract').hide();
        $form.find('.original-buttons').hide();
        $form.find('.complete-checkout-contract').show();
        $form.find('[data-caption="Items"]').hide();
        $form.find('.partial-contract').show();
        $form.find('.flexrow').css('max-width', '2200px');

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/startcheckoutcontract`, requestBody, FwServices.defaultTimeout, response => {
            try {
                this.contractId = response.ContractId;
                $checkedOutItemGridControl = $form.find('[data-name="CheckedOutItemGrid"]');
                $checkedOutItemGridControl.data('ContractId', this.contractId); // Stores ContractId on grid for dblclick in grid controller
                $checkedOutItemGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        ContractId: this.contractId
                    }
                    request.orderby = 'OrderBy';
                    request.pagesize = 10;
                })
                FwBrowse.search($checkedOutItemGridControl);
             
                $stagedItemGridControl = $form.find('[data-name="StagedItemGrid"]');
                $stagedItemGridControl.data('ContractId', this.contractId); // Stores ContractId on grid for dblclick in grid controller
                $stagedItemGridControl.data('ondatabind', function (request) {
                    request.orderby = "ItemOrder";
                    request.uniqueids = {
                        OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                        WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                    };
                    request.pagesize = MAX_PAGE_SIZE;
                })

                FwBrowse.search($stagedItemGridControl);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, null);
    };
    //----------------------------------------------------------------------------------------------
    // There are corresponding double click events in the Staged Item Grid controller 
    moveStagedItemToOut($form: JQuery): void {
        let $selectedCheckBoxes, $stagedItemGrid, orderId, barCodeFieldValue, quantityFieldValue, barCode, iCode, quantity, orderItemId, vendorId, $checkedOutItemGrid, successSound, errorSound, request: any = {};
        successSound = new Audio(this.successSoundFileName);
        errorSound = new Audio(this.errorSoundFileName);
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
        $selectedCheckBoxes = $stagedItemGrid.find('.cbselectrow:checked');
        barCodeFieldValue = $form.find('.partial-contract-barcode input').val();
        quantityFieldValue = $form.find('.partial-contract-quantity input').val();
        orderId = FwFormField.getValueByDataField($form, 'OrderId');

        if (barCodeFieldValue !== '' && $selectedCheckBoxes.length === 0) {
            request.ContractId = this.contractId;
            request.Code = barCodeFieldValue;
            request.OrderId = orderId
            if (quantityFieldValue !== '') {
                request.Quantity = quantityFieldValue
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/movestageditemtoout`, request, FwServices.defaultTimeout, response => {
                if (response.success === true && response.status != 107) {
                    $form.find('.error-msg').html('');
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
                    $form.find('.error-msg').html('');
                    successSound.play();
                    $form.find('.partial-contract-quantity input').focus();
                }
                if (response.success === false && response.status !== 107) {
                    errorSound.play();
                    $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
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
        let $selectedCheckBoxes, $stagedItemGrid, orderId, barCodeFieldValue, barCode, iCode, quantityFieldValue, quantity, orderItemId, vendorId, $checkedOutItemGrid, successSound, errorSound, request: any = {};
        successSound = new Audio(this.successSoundFileName);
        errorSound = new Audio(this.errorSoundFileName);

        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
        $selectedCheckBoxes = $checkedOutItemGrid.find('.cbselectrow:checked');
        barCodeFieldValue = $form.find('.partial-contract-barcode input').val();
        quantityFieldValue = $form.find('.partial-contract-quantity input').val();
        orderId = FwFormField.getValueByDataField($form, 'OrderId');

        if (barCodeFieldValue !== '' && $selectedCheckBoxes.length === 0) {
            request.ContractId = this.contractId;
            request.Code = barCodeFieldValue;
            request.OrderId = orderId;
            if (quantityFieldValue !== '') {
                request.Quantity = quantityFieldValue
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/moveoutitemtostaged`, request, FwServices.defaultTimeout, response => {
                if (response.success === true && response.status != 107) {
                    $form.find('.error-msg').html('');
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
                    $form.find('.error-msg').html('');
                    successSound.play();
                    $form.find('.partial-contract-quantity input').focus();
                }
                if (response.success === false && response.status !== 107) {
                    errorSound.play();
                    $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
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
    completeCheckOutContract($form: JQuery): void {
        let $stagedItemGrid;
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        $form.find('.error-msg').html('');

        if (this.contractId) {
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/completecheckoutcontract/${this.contractId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                try {
                    let contractInfo: any = {}, $contractForm;
                    contractInfo.ContractId = response.ContractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);
                    $form.find('.fwformfield').find('input').val('');
                    FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
                    $form.find('.flexrow').css('max-width', '1400px');
                    $form.find('.orderstatus').hide();
                    $form.find('.createcontract').hide();
                    $form.find('.partial-contract').hide();
                    $form.find('.complete-checkout-contract').hide();
                    $form.find('[data-caption="Items"]').show();
                    FwFormField.enable($form.find('div[data-datafield="OrderId"]'));
                    $form.find('div[data-name="StagedItemGrid"] tr.viewmode').empty();
                    $form.find('div[data-name="CheckedOutItemGrid"] tr.viewmode').empty();
                    $form.find('div[data-datafield="OrderId"]').focus();

                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        } else {
            FwNotification.renderNotification('WARNING', 'Check-out items before attemping to perform this function.');
        }
    };
    //----------------------------------------------------------------------------------------------
    createContract($form: JQuery): void {
        $form.find('.error-msg').html('');
        let orderId, $stagedItemGrid, errorSound, request: any = {};
        errorSound = new Audio(this.errorSoundFileName);
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        request.OrderId = orderId;
        FwAppData.apiMethod(true, 'POST', "api/v1/checkout/checkoutallstaged", request, FwServices.defaultTimeout, function onSuccess(response) {
            let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            if (response.success === true) {
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
            if (response.success === false) {
                errorSound.play();
                $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
            }
        }, null, $form);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any): void {
        let $stagedItemGrid: any, $stagedItemGridControl: any;
        let $checkedOutItemGrid: any, $checkedOutItemGridControl: any;
        let $stageQuantityItemGrid: any, $stageQuantityItemGridControl: any;
        let $stagingExceptionGrid: any, $stagingExceptionGridControl: any;

        //----------------------------------------------------------------------------------------------
        $stagedItemGrid = $form.find('div[data-grid="StagedItemGrid"]');
        $stagedItemGridControl = jQuery(jQuery('#tmpl-grids-StagedItemGridBrowse').html());
        $stagedItemGrid.empty().append($stagedItemGridControl);

        $stagedItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
            };
            request.pagesize = 250;
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
        $stageQuantityItemGrid = $form.find('div[data-grid="StageQuantityItemGrid"]');
        $stageQuantityItemGridControl = jQuery(jQuery('#tmpl-grids-StageQuantityItemGridBrowse').html());
        $stageQuantityItemGrid.empty().append($stageQuantityItemGridControl);
        $stageQuantityItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                IncludeZeroRemaining: FwFormField.getValueByDataField($form, 'IncludeZeroRemaining')
            };
            request.pagesize = 10;
            request.orderby = 'ItemOrder';
        });
        FwBrowse.init($stageQuantityItemGridControl);
        FwBrowse.renderRuntimeHtml($stageQuantityItemGridControl);
        //----------------------------------------------------------------------------------------------
        $stagingExceptionGrid = $form.find('div[data-grid="StagingExceptionGrid"]');
        $stagingExceptionGridControl = jQuery(jQuery('#tmpl-grids-StagingExceptionGridBrowse').html());
        $stagingExceptionGrid.empty().append($stagingExceptionGridControl);
        $stagingExceptionGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
            };
            //request.pagesize = 10;
            request.pagesize = 999;
            request.orderby = 'ItemOrder';
        });
        FwBrowse.init($stagingExceptionGridControl);
        FwBrowse.renderRuntimeHtml($stagingExceptionGridControl);
        //----------------------------------------------------------------------------------------------
        //this.addLegend($form, $stagedItemGrid);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any): void {
        let $stagedItemGrid, $stageQuantityItemGrid, $stagingExceptionGrid;
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        FwBrowse.search($stagedItemGrid);
        //----------------------------------------------------------------------------------------------
        $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
        FwBrowse.search($stageQuantityItemGrid);
        //----------------------------------------------------------------------------------------------
        $stagingExceptionGrid = $form.find('[data-name="StagingExceptionGrid"]');
        FwBrowse.search($stagingExceptionGrid);
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

        //Refresh grids on tab click
        $form.find('div.exceptions-tab').on('click', e => {
            //Disable clicking Exception tab w/o an OrderId
            let orderId = FwFormField.getValueByDataField($form, 'OrderId');
            if (orderId !== '') {
                let $stagingExceptionGrid = $form.find('[data-name="StagingExceptionGrid"]');
                FwBrowse.search($stagingExceptionGrid);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order first.')
            }
        });
        $form.find('div.quantity-items-tab').on('click', e => {
            //Disable clicking Quantity Items tab w/o an OrderId
            let orderId = FwFormField.getValueByDataField($form, 'OrderId');
            if (orderId !== '') {
                let $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
                FwBrowse.search($stageQuantityItemGrid);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order first.')
            }
        });
        // Refresh grids when navigating to Staging tab
        $form.find('.staging-tab').on('click', e => {
            let $stagedItemGrid, $checkedOutItemGrid;
            $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
            $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');

            FwBrowse.search($checkedOutItemGrid);
            FwBrowse.search($stagedItemGrid);

        });
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
                    Code: code
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
            this.completeCheckOutContract($form);
        });
        // Create Contract
        $form.find('.createcontract').on('click', e => {
            this.createContract($form);
        });
        //Options button
        $form.find('.options-button').on('click', e => {
            $form.find('.option-list').toggle();
        });
        //IncludeZeroRemaining Checkbox functionality
        $form.find('[data-datafield="IncludeZeroRemaining"] input').on('change', e => {
            let $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
            let orderId = FwFormField.getValueByDataField($form, 'OrderId');
            let includeZeroRemaining = FwFormField.getValueByDataField($form, 'IncludeZeroRemaining');
            $stageQuantityItemGrid.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId,
                    IncludeZeroRemaining: includeZeroRemaining
                };
                request.pagesize = 10;
                request.orderby = 'ItemOrder';
            });
            FwBrowse.search($stageQuantityItemGrid);
        });
        // Partial Contract Inputs
        $form.find('.partial-contract-inputs input').on('keydown', e => {
            let barCodeFieldValue = $form.find('.partial-contract-barcode input').val();
            let quantityFieldValue = $form.find('.partial-contract-quantity input').val();

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
            let request: any = {}, quantity;
            const $stageQuantityItemGrid = $form.find('div[data-name="StageQuantityItemGrid"]');
            const orderId = FwFormField.getValueByDataField($form, 'OrderId');

            request.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/stagequantityitem/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($stageQuantityItemGrid);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
        // Select All
        $form.find('.selectall').on('click', e => {
            let request: any = {};
            const $stageQuantityItemGrid = $form.find('div[data-name="StageQuantityItemGrid"]');
            const orderId = FwFormField.getValueByDataField($form, 'OrderId');

            request.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/stagequantityitem/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($stageQuantityItemGrid);
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
                Quantity: quantity
            }
        } else {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true
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
                Quantity: quantity
            }
        } else {
            request = {
                OrderId: orderId,
                Code: code,
                AddItemToOrder: true
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