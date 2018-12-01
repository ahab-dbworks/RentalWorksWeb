//routes.push({ pattern: /^module\/checkout$/, action: function (match: RegExpExecArray) { return StagingCheckoutController.getModuleScreen(); } });

class StagingCheckout {
    Module: string = 'StagingCheckout';
    caption: string = 'Staging / Check-Out';
    nav: string = 'module/checkout';
    id: string = 'C3B5EEC9-3654-4660-AD28-20DE8FF9044D';
    showAddItemToOrder: boolean = false;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    contractId: string;
    isPendingItemGridView: boolean = false;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
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
        //var $form;

        //$form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        $form.find('[data-datafield="WarehouseId"]').hide();

        $form.find('.partial-contract').hide();
        $form.find('.pending-item-grid').hide();
        $form.find('.grid-view-radio').hide();

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
                $form.find('.grid-view-radio').show();

                if (FwFormField.getValueByDataField($form, 'IncludeZeroRemaining') === 'T') {
                    $form.find('.option-list').toggle();
                    $form.find('div[data-datafield="IncludeZeroRemaining"] input').prop('checked', false);
                }
                FwFormField.setValueByDataField($form, 'GridView', 'STAGE');
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
            this.startPartialCheckoutItems($form, e);
        });

        menuOptions.push($createContract, $createPartialContract);

        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    };
    //----------------------------------------------------------------------------------------------
    startPartialCheckoutItems = ($form: JQuery, event): void => {
        $form.find('.error-msg').html('');
        const MAX_PAGE_SIZE = 9999;
        let requestBody: any = {}, $checkedOutItemGridControl, $stagedItemGridControl: any;
        let orderId = FwFormField.getValueByDataField($form, 'OrderId');
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
        } else {
            event.stopPropagation();
            FwNotification.renderNotification('WARNING', 'Select an Order.')
        }
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
    completeCheckOutContract($form: JQuery, event): void {
        $form.find('.error-msg').html('');
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
        $form.find('.error-msg').html('');
        $form.find('.grid-view-radio').hide();

        let orderId, errorSound, request: any = {};
        errorSound = new Audio(this.errorSoundFileName);
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
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
                    $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                }
            }, null, $form);
        } else {
            event.stopPropagation();
            FwNotification.renderNotification('WARNING', 'Select an Order.')
        }
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any): void {
        let $stagedItemGrid: any, $stagedItemGridControl: any;
        let $checkedOutItemGrid: any, $checkedOutItemGridControl: any;
        let $stageQuantityItemGrid: any, $stageQuantityItemGridControl: any;
        let $checkOutPendingItemGrid: any, $checkOutPendingItemGridControl: any;

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
        $checkOutPendingItemGrid = $form.find('div[data-grid="CheckOutPendingItemGrid"]');
        $checkOutPendingItemGridControl = jQuery(jQuery('#tmpl-grids-CheckOutPendingItemGridBrowse').html());
        $checkOutPendingItemGrid.empty().append($checkOutPendingItemGridControl);
        $checkOutPendingItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
            };
            //request.pagesize = 10;
            request.pagesize = 999;
            request.orderby = 'ItemOrder';
        });
        FwBrowse.init($checkOutPendingItemGridControl);
        FwBrowse.renderRuntimeHtml($checkOutPendingItemGridControl);
        //----------------------------------------------------------------------------------------------
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any): void {
        let $stagedItemGrid, $stageQuantityItemGrid, $checkOutPendingItemGrid;
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        FwBrowse.search($stagedItemGrid);
        //----------------------------------------------------------------------------------------------
        $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
        FwBrowse.search($stageQuantityItemGrid);
        //----------------------------------------------------------------------------------------------
        $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
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
        let errorSound, successSound;
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);

        $form.find('div.quantity-items-tab').on('click', e => {
            //Disable clicking Quantity Items tab w/o an OrderId
            let orderId = FwFormField.getValueByDataField($form, 'OrderId');
            if (orderId !== '') {
                let $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
                FwBrowse.search($stageQuantityItemGrid);
            } else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order.')
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
                let code, orderId, $stagedItemGrid, $checkOutPendingItemGrid, request: any = {};

                $form.find('.error-msg').html('');
                $form.find('div.AddItemToOrder').html('');
                orderId = FwFormField.getValueByDataField($form, 'OrderId');
                code = FwFormField.getValueByDataField($form, 'Code');
                $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');

                request = {
                    OrderId: orderId,
                    Code: code
                }

                FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
                    if (response.success === true && response.status != 107) {
                        successSound.play();
                        this.addItemFieldValues($form, response);

                        if (this.isPendingItemGridView === false) {
                            FwBrowse.search($stagedItemGrid);
                        } else {
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
                        $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`)
                    } if (response.ShowAddCompleteToOrder === true) {
                        this.addItemFieldValues($form, response);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StagingCheckoutController.addCompleteToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`)
                    } if (response.ShowUnstage === true) {
                        errorSound.play();
                        this.showAddItemToOrder = true;
                        this.addItemFieldValues($form, response);
                        $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.unstageItem(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Unstage Item</div>`)
                } if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                        errorSound.play();
                    this.addItemFieldValues($form, response);
                        $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
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
                    let code, orderId, quantity, $stagedItemGrid, $checkOutPendingItemGrid;

                    $form.find('.error-msg').html('');
                    $form.find('div.AddItemToOrder').html('');
                    orderId = FwFormField.getValueByDataField($form, 'OrderId');
                    code = FwFormField.getValueByDataField($form, 'Code');
                    quantity = +FwFormField.getValueByDataField($form, 'Quantity');
                    $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                    $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');

                    let request: any = {};
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
                                FwBrowse.search($stagedItemGrid);
                            } else {
                                FwBrowse.search($checkOutPendingItemGrid);
                            }
                            FwFormField.setValueByDataField($form, 'Quantity', 0)
                            $form.find('[data-datafield="Code"] input').select();
                        } if (response.ShowAddItemToOrder === true) {
                            errorSound.play();
                            this.addItemFieldValues($form, response);
                            this.showAddItemToOrder = true;
                            $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                            $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`)
                        } if (response.ShowAddCompleteToOrder === true) {
                            this.addItemFieldValues($form, response);
                            $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StagingCheckoutController.addCompleteToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`)
                        } if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                            errorSound.play();
                            this.addItemFieldValues($form, response);
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
        // Grid view toggle
        $form.find('.grid-view-radio input').on('change', e => {
            let $target = jQuery(e.currentTarget),
                gridView = $target.val(),
                stagedItemGridContainer = $form.find('.staged-item-grid'),
                checkOutPendingItemGridContainier = $form.find('.pending-item-grid'),
                $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]'),
                $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]'),
                orderId = FwFormField.getValueByDataField($form, 'OrderId');
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
            let barCodeFieldValue = $form.find('.partial-contract-barcode input').val();

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
            const $stageQuantityItemGrid = $form.find('div[data-name="StageQuantityItemGrid"]');
            const orderId = FwFormField.getValueByDataField($form, 'OrderId');

            request.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/stagequantityitem/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                $form.find('.error-msg-qty').html('');
                if (response.success === false) {
                    errorSound.play();
                    $form.find('div.error-msg-qty').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                } else {
                    successSound.play();
                    FwBrowse.search($stageQuantityItemGrid);
                }
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
                $form.find('.error-msg-qty').html('');
                if (response.success === false) {
                    errorSound.play();
                    $form.find('div.error-msg-qty').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                } else {
                    successSound.play();
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
        let code, $form, $element, orderId, quantity, $stagedItemGrid, $checkOutPendingItemGrid, successSound, request: any = {};
        $element = jQuery(element);
        $form = jQuery($element).closest('.fwform');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        code = FwFormField.getValueByDataField($form, 'Code');
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
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

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
            try {
                if (this.isPendingItemGridView === false) {
                    FwBrowse.search($stagedItemGrid);
                } else {
                    FwBrowse.search($checkOutPendingItemGrid);
                }
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
    unstageItem(element: any): void {
        this.showAddItemToOrder = false;
        let code, $form, $element, orderId, $stagedItemGrid, $checkOutPendingItemGrid, successSound, request: any = {};
        $element = jQuery(element);
        $form = jQuery($element).closest('.fwform');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        code = FwFormField.getValueByDataField($form, 'Code');
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
        successSound = new Audio(this.successSoundFileName);

        request = {
            OrderId: orderId,
            Code: code,
            UnstageItem: true,
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
            try {
                if (this.isPendingItemGridView === false) {
                    FwBrowse.search($stagedItemGrid);
                } else {
                    FwBrowse.search($checkOutPendingItemGrid);
                }
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
        let code, $form, $element, orderId, quantity, $stagedItemGrid, $checkOutPendingItemGrid, successSound, request: any = {};
        $element = jQuery(element);
        $form = jQuery($element).closest('.fwform');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        code = FwFormField.getValueByDataField($form, 'Code');
        $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
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

        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
            try {
                if (this.isPendingItemGridView === false) {
                    FwBrowse.search($stagedItemGrid);
                } else {
                    FwBrowse.search($checkOutPendingItemGrid);
                }
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
    getFormTemplate(): string {
        return `
        <div id="stagingcheckoutform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Staging / Check-Out" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="StagingCheckoutController">
          <div id="dealform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="stagingtab" class="tab staging-tab" data-tabpageid="stagingtabpage" data-caption="Staging"></div>
              <div data-type="tab" id="quantityitemtab" class="tab quantity-items-tab" data-tabpageid="quantityitemtabpage" data-caption="Quantity Items"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="stagingtabpage" class="tabpage" data-tabid="stagingtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Staging / Check-Out">
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:1 1 850px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-formbeforevalidate="beforeValidate" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:1 1 250px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:1 1 300px;"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-enabled="false" style="flex:1 1 300px;"></div>
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
                            <div class="orderstatus fwformcontrol" data-type="button" style="flex:0 1 109px; margin-left:8px;">Order Status</div>
                            <div class="createcontract" data-type="btnmenu" style="flex:0 1 200px;margin-right:7px;" data-caption="Create Contract"></div>
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
                            <div class="fwformcontrol complete-checkout-contract" data-type="button" style="max-width:140px;">Create Contract</div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--Quantity Items Page-->
              <div data-type="tabpage" id="quantityitemtabpage" class="tabpage" data-tabid="quantityitemtab">
                <div class="flexpage">
                  <div class="flexrow error-msg-qty"></div>
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
            </div>
          </div>
        </div>
        `;
    }
}
var StagingCheckoutController = new StagingCheckout();