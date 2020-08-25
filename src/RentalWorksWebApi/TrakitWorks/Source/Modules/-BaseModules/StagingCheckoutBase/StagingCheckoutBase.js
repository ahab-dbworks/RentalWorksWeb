var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class StagingCheckoutBase {
    constructor() {
        this.isPendingItemGridView = false;
        this.startPartialCheckoutItems = ($form, event) => {
            $form.find('.error-msg:not(.qty)').html('');
            const requestBody = {};
            const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
            requestBody.OrderId = orderId;
            if (orderId != '') {
                this.partialContractGridVisibility($form);
                if (this.contractId == '' || this.contractId == undefined) {
                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/startcheckoutcontract`, requestBody, FwServices.defaultTimeout, response => {
                        try {
                            this.contractId = response.ContractId;
                            $form.find('.suspendedsession').hide();
                            $form.find('div.caption:contains(Cancel Staging / Check-Out)').parent().attr('data-enabled', 'true');
                            this.renderPartialCheckoutGrids($form);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }, null, null);
                }
                else {
                    $form.find('.suspendedsession').hide();
                    $form.find('div.caption:contains(Cancel Staging / Check-Out)').parent().attr('data-enabled', 'true');
                    this.renderPartialCheckoutGrids($form);
                }
            }
            else {
                event.stopPropagation();
                if (this.Type != undefined && this.Type === 'ContainerItem') {
                    FwNotification.renderNotification('WARNING', 'Select a Container.');
                }
                else {
                    FwNotification.renderNotification('WARNING', 'Select an Order.');
                }
            }
        };
    }
    getModuleScreen() {
        const screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $form = this.openForm('EDIT');
        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () { };
        return screen;
    }
    ;
    loadForm(uniqueids) {
        let $form = this.openForm('EDIT');
        $form.find(`div.fwformfield[data-datafield="${this.Type}Id"] input`).val(uniqueids[`${this.Type}Id`]);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    ;
    openForm(mode, parentmoduleinfo) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');
        $form.find('[data-type="RefreshMenuBarButton"]').remove();
        $form.find('.partial-contract').hide();
        $form.find('.pending-item-grid').hide();
        $form.find('div[data-datafield="GridView"]').hide();
        $form.find('[data-datafield="WarehouseId"]').hide();
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
        $form.find('div.caption:contains(Cancel Staging / Check-Out)').parent().attr('data-enabled', 'false');
        this.getOrder($form);
        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, `${this.Type}Id`, parentmoduleinfo[`${this.Type}Id`], parentmoduleinfo[`${this.Type}Number`]);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', parentmoduleinfo.WarehouseId, parentmoduleinfo.Warehouse);
            FwFormField.setValueByDataField($form, 'Description', parentmoduleinfo.description);
            $form.find(`[data-datafield="${this.Type}Id"]`).change();
            $form.attr('data-showsuspendedsessions', 'false');
        }
        $form.find(`div[data-datafield="${this.Type}Id"] input`).focus();
        this.getSuspendedSessions($form);
        this.events($form);
        return $form;
    }
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'StagedItemGrid',
            gridSecurityId: '40bj9sn7JHqai',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 20,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
                FwMenu.addSubMenuItem(options.$groupActions, 'Unstage Selected Items', '', (e) => {
                    try {
                        if ($form.attr('data-controller') === 'TransferOutController') {
                            window.TransferOutController.unstageItems($form, e);
                        }
                        else {
                            StagingCheckoutController.unstageItems($form, e);
                        }
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                };
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'CheckedOutItemGrid',
            gridSecurityId: 'HXSEu4U0vSir',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId')
                };
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'StageQuantityItemGrid',
            gridSecurityId: '0m0QMviBYWVYm',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 20,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
                FwMenu.addSubMenuItem(options.$groupActions, 'Unstage Selected Items', '', (e) => {
                    try {
                        if ($form.attr('data-controller') === 'TransferOutController') {
                            window.TransferOutController.unstageItems($form, e);
                        }
                        else {
                            StagingCheckoutController.unstageItems($form, e);
                        }
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                    IncludeZeroRemaining: FwFormField.getValueByDataField($form, 'IncludeZeroRemaining')
                };
                request.orderby = 'ItemOrder';
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'StageHoldingItemGrid',
            gridSecurityId: 'i7EMskpGXvByc',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 20,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                    IncludeZeroRemaining: FwFormField.getValueByDataField($form, 'IncludeZeroRemaining')
                };
                request.orderby = 'ItemOrder';
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'CheckOutPendingItemGrid',
            gridSecurityId: 'GO96A3pk0UE',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 20,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                };
                request.orderby = 'ItemOrder';
            }
        });
    }
    ;
    afterLoad($form) {
        const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        FwBrowse.search($stagedItemGrid);
        const $stageHoldingItemGrid = $form.find('[data-name="StageHoldingItemGrid"]');
        FwBrowse.search($stageHoldingItemGrid);
        const $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
        FwBrowse.search($stageQuantityItemGrid);
        const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
        FwBrowse.search($checkOutPendingItemGrid);
    }
    ;
    getSuspendedSessions($form) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const showSuspendedSessions = $form.attr('data-showsuspendedsessions');
        if (showSuspendedSessions != "false") {
            let apiUrl;
            let sessionType;
            switch (this.Module) {
                case 'StagingCheckout':
                    apiUrl = `api/v1/checkout/suspendedsessionsexist?warehouseId=${warehouse.warehouseid}`;
                    sessionType = 'OUT';
                    break;
                case 'TransferOut':
                    apiUrl = `api/v1/checkout/transfersuspendedsessionsexist?warehouseId=${warehouse.warehouseid}`;
                    sessionType = 'MANIFEST';
                    break;
                case 'FillContainer':
                    apiUrl = `api/v1/checkout/containersuspendedsessionsexist?warehouseId=${warehouse.warehouseid}`;
                    sessionType = 'FILL';
            }
            FwAppData.apiMethod(true, 'GET', apiUrl, null, FwServices.defaultTimeout, response => {
                if (response) {
                    $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
                }
            }, ex => FwFunc.showError(ex), $form);
            $form.on('click', '.suspendedsession', e => {
                SuspendedSessionController.sessionType = sessionType;
                const $browse = SuspendedSessionController.openBrowse();
                const $popup = FwPopup.renderPopup($browse, { ismodal: true }, 'Suspended Sessions');
                FwPopup.showPopup($popup);
                $browse.data('ondatabind', request => {
                    request.uniqueids = {
                        SessionType: sessionType,
                        WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid
                    };
                });
                FwBrowse.search($browse);
                $browse.on('dblclick', 'tr.viewmode', e => {
                    const $this = jQuery(e.currentTarget);
                    const id = $this.find(`[data-browsedatafield="${this.Type}Id"]`).attr('data-originalvalue');
                    const number = $this.find(`[data-browsedatafield="${this.Type}Number"]`).attr('data-originalvalue');
                    const contractId = $this.find(`[data-browsedatafield="ContractId"]`).attr('data-originalvalue');
                    this.contractId = contractId;
                    $form.find('div.caption:contains(Cancel Staging / Check-Out)').parent().attr('data-enabled', 'true');
                    if (this.Module == 'FillContainer') {
                        const orderId = $this.find(`[data-browsedatafield="OrderId"]`).attr('data-originalvalue');
                        FwFormField.setValueByDataField($form, 'OrderId', orderId);
                    }
                    FwFormField.setValueByDataField($form, `${this.Type}Id`, id, number, true);
                    FwPopup.destroyPopup($popup);
                    $form.find('.suspendedsession').hide();
                    this.partialContractGridVisibility($form);
                    this.renderPartialCheckoutGrids($form);
                });
            });
        }
    }
    getOrder($form) {
        const maxPageSize = 20;
        const module = this.Module;
        $form.on('change', `[data-datafield="${this.Type}Id"]`, e => {
            try {
                FwFormField.setValueByDataField($form, 'Quantity', '');
                FwFormField.setValueByDataField($form, 'Code', '');
                $form.find('.error-msg:not(.qty)').html('');
                $form.find('div[data-datafield="GridView"]').show();
                if (FwFormField.getValueByDataField($form, 'IncludeZeroRemaining') === 'T') {
                    $form.find('.option-list').toggle();
                    $form.find('div[data-datafield="IncludeZeroRemaining"] input').prop('checked', false);
                }
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
                const orderId = FwFormField.getValueByDataField($form, `${this.Type == 'ContainerItem' ? 'Order' : this.Type}Id`);
                const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                FwFormField.setValueByDataField($form, 'GridView', 'STAGE');
                const apiUrl = `api/v1/${apiName}/${orderId}`;
                FwAppData.apiMethod(true, 'GET', apiUrl, null, FwServices.defaultTimeout, response => {
                    FwFormField.setValueByDataField($form, 'Description', response.Description);
                    FwFormField.setValueByDataField($form, 'Location', response.Location);
                    if (module == 'StagingCheckout')
                        FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
                    if (module == 'FillContainer')
                        FwFormField.setValueByDataField($form, 'BarCode', response.BarCode);
                    FwAppData.apiMethod(true, 'GET', `api/v1/checkout/stagingtabs?OrderId=${orderId}&WarehouseId=${warehouseId}`, null, FwServices.defaultTimeout, res => {
                        res.QuantityTab === true ? $form.find('.quantity-items-tab').show() : $form.find('.quantity-items-tab').hide();
                        res.HoldingTab === true ? $form.find('.holding-items-tab').show() : $form.find('.holding-items-tab').hide();
                        res.SerialTab === true ? $form.find('.serial-items-tab').show() : $form.find('.serial-items-tab').hide();
                        res.ConsignmentTab === true ? $form.find('.consignment-tab').show() : $form.find('.consignment-tab').hide();
                    }, ex => {
                        FwFunc.showError(ex);
                    }, $form);
                }, null, $form);
                const $stagedItemGridControl = $form.find('[data-name="StagedItemGrid"]');
                $stagedItemGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId,
                        WarehouseId: warehouseId
                    };
                    request.pagesize = maxPageSize;
                });
                FwBrowse.search($stagedItemGridControl);
                const $stageQuantityItemGridControl = $form.find('[data-name="StageQuantityItemGrid"]');
                $stageQuantityItemGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId
                    };
                    request.pagesize = 20;
                });
                FwBrowse.search($stageQuantityItemGridControl);
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
            FwFormField.disable($form.find(`div[data-datafield="${this.Type == 'ContainerItem' ? 'BarCode' : this.Type + 'Id'}"]`));
            if (this.contractId == '') {
                $form.find('.orderstatus').show();
                $form.find('.createcontract').show();
                $form.find('.original-buttons').show();
            }
            $form.find('[data-datafield="Code"] input').focus();
            $form.find('.suspendedsession').hide();
        });
    }
    ;
    addButtonMenu($form) {
        let caption;
        let partialCaption;
        switch (this.Module) {
            case 'StagingCheckout':
                caption = 'Create Contract';
                partialCaption = 'Create Partial Contract';
                break;
            case 'TransferOut':
                caption = 'Create Manifest';
                partialCaption = 'Create Partial Manifest';
                break;
            case 'FillContainer':
                caption = 'Fill Container';
                partialCaption = 'Fill Partial Container';
                break;
        }
        const $createContract = FwMenu.generateButtonMenuOption(caption);
        $createContract.on('click', e => {
            e.stopPropagation();
            $form.find('.createcontract').click();
        });
        const $createPartialContract = FwMenu.generateButtonMenuOption(partialCaption);
        $createPartialContract.on('click', e => {
            e.stopPropagation();
            this.startPartialCheckoutItems($form, e);
        });
        const menuOptions = [];
        menuOptions.push($createContract, $createPartialContract);
        const $buttonmenu = $form.find('.createcontract[data-type="btnmenu"]');
        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    }
    ;
    partialContractGridVisibility($form) {
        $form.find('.orderstatus').hide();
        $form.find('.createcontract').hide();
        $form.find('.original-buttons').hide();
        $form.find('.complete-checkout-contract').show();
        $form.find('.abort-checkout-contract').show();
        $form.find('[data-caption="Items"]').hide();
        $form.find('.partial-contract').show();
        $form.find('.flexrow').css('max-width', '2200px');
        $form.find('.pending-item-grid').hide();
        $form.find('.staged-item-grid').show();
    }
    renderPartialCheckoutGrids($form) {
        const maxPageSize = 20;
        const $checkedOutItemGridControl = $form.find('[data-name="CheckedOutItemGrid"]');
        $checkedOutItemGridControl.data('ContractId', this.contractId);
        $checkedOutItemGridControl.attr('data-tableheight', '735px');
        $checkedOutItemGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ContractId: this.contractId
            };
            request.orderby = 'OrderBy';
            request.pagesize = maxPageSize;
        });
        FwBrowse.search($checkedOutItemGridControl);
        const $stagedItemGridControl = $form.find('[data-name="StagedItemGrid"]');
        $stagedItemGridControl.data('ContractId', this.contractId);
        $stagedItemGridControl.attr('data-tableheight', '735px');
        $stagedItemGridControl.data('ondatabind', request => {
            request.orderby = "ItemOrder";
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Type == 'ContainerItem' ? 'Order' : this.Type}Id`),
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
            };
            request.pagesize = maxPageSize;
        });
        FwBrowse.search($stagedItemGridControl);
    }
    abortPartialContract($form) {
        $form.find('.orderstatus').show();
        $form.find('.createcontract').show();
        $form.find('.original-buttons').show();
        $form.find('.complete-checkout-contract').hide();
        $form.find('.abort-checkout-contract').hide();
        $form.find('[data-caption="Items"]').show();
        $form.find('.partial-contract').hide();
        $form.find('.flexrow').css('max-width', '1200px');
        $form.find('.pending-item-grid').hide();
        $form.find('.staged-item-grid').show();
        const $stagedItemGridControl = $form.find('[data-name="StagedItemGrid"]');
        $stagedItemGridControl.data('ContractId', this.contractId);
        $stagedItemGridControl.attr('data-tableheight', '735px');
        $stagedItemGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, `${this.Type}Id`),
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
            };
            request.pagesize = 20;
        });
        FwBrowse.search($stagedItemGridControl);
        $form.find('div[data-datafield="GridView"] input').change();
        $form.find('[data-datafield="Code"] input').focus();
    }
    unstageSelectedItems($form, $selectedCheckBoxes) {
        return __awaiter(this, void 0, void 0, function* () {
            function delay(ms) {
                return new Promise(resolve => setTimeout(resolve, ms));
            }
            let responseCount = 0;
            const errorMessages = [];
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
                const orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                const vendorId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="VendorId"]').attr('data-originalvalue');
                const barCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="BarCode"]').attr('data-originalvalue');
                const iCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="ICode"]').attr('data-originalvalue');
                const quantity = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Quantity"]').attr('data-originalvalue');
                const quantityStaged = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="QuantityStaged"]').attr('data-originalvalue');
                const request = {
                    OrderId: orderId,
                    OrderItemId: orderItemId,
                    Code: barCode ? barCode : iCode,
                    Quantity: quantity ? quantity : quantityStaged,
                    VendorId: vendorId
                };
                FwAppData.apiMethod(true, 'POST', `api/v1/checkout/unstageitem`, request, FwServices.defaultTimeout, response => {
                    responseCount++;
                    if (!response.success) {
                        errorMessages.push(response.msg);
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                }, $form);
            }
            while (responseCount < $selectedCheckBoxes.length) {
                yield delay(1000);
            }
            return errorMessages;
        });
    }
    unstageItems($form, event) {
        return __awaiter(this, void 0, void 0, function* () {
            const $element = jQuery(event.currentTarget);
            const $grid = jQuery($element.closest('[data-type="Grid"]'));
            const $selectedCheckBoxes = $grid.find('tbody .cbselectrow:checked');
            const errorMsg = $form.find('.error-msg:not(.qty)');
            if ($selectedCheckBoxes.length !== 0) {
                yield this.unstageSelectedItems($form, $selectedCheckBoxes).then(errorMessages => {
                    FwBrowse.search($grid);
                    const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
                    const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                    FwAppData.apiMethod(true, 'GET', `api/v1/checkout/stagingtabs?OrderId=${orderId}&WarehouseId=${warehouseId}`, null, FwServices.defaultTimeout, res => {
                        res.QuantityTab === true ? $form.find('.quantity-items-tab').show() : $form.find('.quantity-items-tab').hide();
                        res.HoldingTab === true ? $form.find('.holding-items-tab').show() : $form.find('.holding-items-tab').hide();
                        res.SerialTab === true ? $form.find('.serial-items-tab').show() : $form.find('.serial-items-tab').hide();
                        res.ConsignmentTab === true ? $form.find('.consignment-tab').show() : $form.find('.consignment-tab').hide();
                    }, ex => {
                        FwFunc.showError(ex);
                    }, $form);
                    if (errorMessages.length == 0) {
                        errorMsg.html('');
                        FwFunc.playSuccessSound();
                    }
                    else {
                        FwFunc.playErrorSound();
                        errorMsg.html(`<div><span>${errorMessages.join('<br>')}</span></div>`);
                    }
                });
            }
            else {
                FwNotification.renderNotification('WARNING', 'Select rows to unstage in order to perform this function.');
            }
        });
    }
    moveStagedItemToOut($form) {
        const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        const $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
        const $selectedCheckBoxes = $checkedOutItemGrid.find('tbody .cbselectrow:checked');
        const barCodeFieldValue = $form.find('.partial-contract-barcode input').val();
        const quantityFieldValue = $form.find('.partial-contract-quantity input').val();
        const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
        const errorMsg = $form.find('.error-msg:not(.qty)');
        const request = {};
        if (barCodeFieldValue !== '' && $selectedCheckBoxes.length === 0) {
            request.ContractId = this.contractId;
            request.Code = barCodeFieldValue;
            request.OrderId = orderId;
            if (quantityFieldValue !== '') {
                request.Quantity = quantityFieldValue;
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/movestageditemtoout`, request, FwServices.defaultTimeout, response => {
                if (response.success === true && response.status != 107) {
                    errorMsg.html('');
                    FwFunc.playSuccessSound();
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
                    FwFunc.playSuccessSound();
                    $form.find('.partial-contract-quantity input').focus();
                }
                if (response.success === false && response.status !== 107) {
                    FwFunc.playErrorSound();
                    errorMsg.html(`<div><span>${response.msg}</span></div>`);
                    $form.find('.partial-contract-barcode input').focus();
                }
            }, null, null);
        }
        else {
            if ($selectedCheckBoxes.length !== 0) {
                let responseCount = 0;
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    const barCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="BarCode"]').attr('data-originalvalue');
                    const iCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="ICode"]').attr('data-originalvalue');
                    const quantity = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Quantity"]').attr('data-originalvalue');
                    const orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                    const vendorId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="VendorId"]').attr('data-originalvalue');
                    request.OrderId = orderId;
                    request.ContractId = this.contractId;
                    request.Quantity = quantity;
                    if (barCode !== '') {
                        request.Code = barCode;
                    }
                    else {
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
            }
            else {
                FwNotification.renderNotification('WARNING', 'Select rows in Stage Items or use Bar Code input in order to perform this function.');
                $form.find('.partial-contract-barcode input').focus();
            }
        }
    }
    ;
    moveOutItemToStaged($form) {
        const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
        const $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
        const $selectedCheckBoxes = $checkedOutItemGrid.find('.cbselectrow:checked');
        const barCodeFieldValue = $form.find('.partial-contract-barcode input').val();
        const quantityFieldValue = $form.find('.partial-contract-quantity input').val();
        const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
        const errorMsg = $form.find('.error-msg:not(.qty)');
        const request = {};
        if (barCodeFieldValue !== '' && $selectedCheckBoxes.length === 0) {
            request.ContractId = this.contractId;
            request.Code = barCodeFieldValue;
            request.OrderId = orderId;
            if (quantityFieldValue !== '') {
                request.Quantity = quantityFieldValue;
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/moveoutitemtostaged`, request, FwServices.defaultTimeout, response => {
                if (response.success === true && response.status != 107) {
                    errorMsg.html('');
                    FwFunc.playSuccessSound();
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
                    FwFunc.playSuccessSound();
                    $form.find('.partial-contract-quantity input').focus();
                }
                if (response.success === false && response.status !== 107) {
                    FwFunc.playErrorSound();
                    errorMsg.html(`<div><span>${response.msg}</span></div>`);
                    $form.find('.partial-contract-barcode input').focus();
                }
            }, null, null);
        }
        else {
            if ($selectedCheckBoxes.length !== 0) {
                let responseCount = 0;
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    const barCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="BarCode"]').attr('data-originalvalue');
                    const iCode = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="ICode"]').attr('data-originalvalue');
                    const quantity = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Quantity"]').attr('data-originalvalue');
                    const orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
                    const vendorId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="VendorId"]').attr('data-originalvalue');
                    request.OrderId = orderId;
                    request.ContractId = this.contractId;
                    request.Quantity = quantity;
                    if (barCode !== '') {
                        request.Code = barCode;
                    }
                    else {
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
            }
            else {
                FwNotification.renderNotification('WARNING', 'Select rows in Contract Items or use Bar Code input in order to perform this function.');
                $form.find('.partial-contract-barcode input').focus();
            }
        }
    }
    ;
    completeCheckOutContract($form, event) {
        $form.find('.error-msg:not(.qty)').html('');
        $form.find('div[data-datafield="GridView"]').hide();
        if (this.contractId) {
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/completecheckoutcontract/${this.contractId}`, null, FwServices.defaultTimeout, response => {
                try {
                    const contractInfo = {};
                    contractInfo.ContractId = response.ContractId;
                    const $contractForm = ContractController.loadForm(contractInfo);
                    $form.find('.flexrow').css('max-width', '1200px');
                    FwModule.openSubModuleTab($form, $contractForm);
                    this.resetForm($form);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        }
        else {
            event.stopPropagation();
            FwNotification.renderNotification('WARNING', 'Check-out items before attemping to perform this function.');
        }
    }
    ;
    createContract($form, event) {
        const type = this.Type;
        const errorMsg = $form.find('.error-msg:not(.qty)');
        errorMsg.html('');
        const orderId = FwFormField.getValueByDataField($form, `${type}Id`);
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const request = {};
        setTimeout(() => {
            if (this.Module === 'StagingCheckout' && warehouse.promptforcheckoutexceptions == true) {
                const $grid = $form.find('[data-name="CheckOutPendingItemGrid"]');
                FwBrowse.search($grid).then(() => {
                    if ($grid.find('tbody tr').length > 0) {
                        FwFormField.setValueByDataField($form, 'GridView', 'PENDING');
                        $form.find('div[data-datafield="GridView"] input').change();
                        const $confirmation = FwConfirmation.renderConfirmation(`Confirm?`, '');
                        const html = `<div class="flexrow">Pending items exist. Continue with Contract?</div>`;
                        FwConfirmation.addControls($confirmation, html);
                        const $yes = FwConfirmation.addButton($confirmation, 'Create Contract', false);
                        FwConfirmation.addButton($confirmation, 'Cancel');
                        $yes.focus();
                        $yes.on('click', e => {
                            checkout();
                            FwConfirmation.destroyConfirmation($confirmation);
                        });
                    }
                    else {
                        checkout();
                    }
                });
            }
            else {
                checkout();
            }
        }, 1000);
        function checkout() {
            if (orderId != '') {
                request.OrderId = orderId;
                FwAppData.apiMethod(true, 'POST', "api/v1/checkout/checkoutallstaged", request, FwServices.defaultTimeout, response => {
                    if (response.success === true) {
                        $form.find('div[data-datafield="GridView"]').hide();
                        const contractInfo = {};
                        $form.find('.flexrow').css('max-width', '1200px');
                        contractInfo.ContractId = response.ContractId;
                        const $contractForm = ContractController.loadForm(contractInfo);
                        FwModule.openSubModuleTab($form, $contractForm);
                        $form.find('.clearable').find('input').val('');
                        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
                        type === 'ContainerItem' ? FwFormField.enable($form.find(`div[data-datafield="BarCode"]`)) : FwFormField.enable($form.find(`div[data-datafield="${type}Id"]`));
                        $form.find('[data-datafield="Code"] input').select();
                        $form.find('div[data-name="StagedItemGrid"] tr.viewmode').empty();
                        $form.find('div[data-name="CheckOutPendingItemGrid"] tr.viewmode').empty();
                        $form.find('div[data-name="CheckedOutItemGrid"] tr.viewmode').empty();
                        $form.find('div[data-name="StageQuantityItemGrid"] tr.viewmode').empty();
                        $form.find('.pending-item-grid').hide();
                        $form.find('.staged-item-grid').show();
                    }
                    else if (response.success === false) {
                        FwFunc.playErrorSound();
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                    }
                }, null, $form);
            }
            else {
                event.stopPropagation();
                if (type != undefined && type === 'ContainerItem') {
                    FwNotification.renderNotification('WARNING', 'Select a Container.');
                }
                else {
                    FwNotification.renderNotification('WARNING', 'Select an Order.');
                }
            }
        }
    }
    ;
    addItemFieldValues($form, response) {
        FwFormField.setValueByDataField($form, 'ICode', response.InventoryStatus.ICode);
        FwFormField.setValueByDataField($form, 'InventoryDescription', response.InventoryStatus.Description);
        FwFormField.setValueByDataField($form, 'QuantityOrdered', response.InventoryStatus.QuantityOrdered);
        FwFormField.setValueByDataField($form, 'QuantitySub', response.InventoryStatus.QuantitySub);
        FwFormField.setValueByDataField($form, 'QuantityOut', response.InventoryStatus.QuantityOut);
        FwFormField.setValueByDataField($form, 'QuantityStaged', response.InventoryStatus.QuantityStaged);
        FwFormField.setValueByDataField($form, 'QuantityRemaining', response.InventoryStatus.QuantityRemaining);
    }
    ;
    refreshGridForScanning($form) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
        if (orderId && warehouse) {
            const gridView = FwFormField.getValueByDataField($form, 'GridView');
            if (gridView === 'STAGE') {
                const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                FwBrowse.search($stagedItemGrid);
            }
            else {
                const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
                FwBrowse.search($checkOutPendingItemGrid);
            }
        }
    }
    ;
    events($form) {
        const errorMsg = $form.find('.error-msg:not(.qty)');
        const errorMsgQty = $form.find('.error-msg.qty');
        const debouncedRefreshGrid = FwFunc.debounce(function () {
            const gridView = FwFormField.getValueByDataField($form, 'GridView');
            if (gridView === 'STAGE') {
                const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                FwBrowse.search($stagedItemGrid);
            }
            else {
                const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
                FwBrowse.search($checkOutPendingItemGrid);
            }
        }, 2000, false);
        $form.find('div.quantity-items-tab').on('click', e => {
            const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
            if (orderId !== '') {
                const $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
                FwBrowse.search($stageQuantityItemGrid);
            }
            else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order.');
            }
        });
        $form.find('div.holding-items-tab').on('click', e => {
            const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
            if (orderId !== '') {
                const $stageHoldingItemGrid = $form.find('[data-name="StageHoldingItemGrid"]');
                FwBrowse.search($stageHoldingItemGrid);
            }
            else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order.');
            }
        });
        $form.find('.staging-tab').on('click', e => {
            const gridView = FwFormField.getValueByDataField($form, 'GridView');
            let $grid;
            gridView == 'STAGE' ? $grid = $form.find('[data-name="StagedItemGrid"]') : $grid = $form.find('[data-name="CheckOutPendingItemGrid"]');
            FwBrowse.search($grid);
            const $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
            FwBrowse.search($checkedOutItemGrid);
        });
        $form.find('[data-datafield="Code"] input').on('keydown', e => {
            if (e.which == 9 || e.which == 13) {
                errorMsg.html('');
                $form.find('div.AddItemToOrder').html('');
                const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
                const code = FwFormField.getValueByDataField($form, 'Code');
                const request = {
                    OrderId: orderId,
                    Code: code,
                    WarehouseId: warehouse.warehouseid
                };
                FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
                    if (response.success === true && response.status != 107) {
                        FwFunc.playSuccessSound();
                        this.addItemFieldValues($form, response);
                        debouncedRefreshGrid();
                        $form.find('[data-datafield="Code"] input').select();
                    }
                    if (response.status === 107) {
                        FwFunc.playSuccessSound();
                        this.addItemFieldValues($form, response);
                        FwFormField.setValueByDataField($form, 'Quantity', 0);
                        $form.find('div[data-datafield="Quantity"] input').select();
                    }
                    if (response.ShowAddItemToOrder === true) {
                        FwFunc.playErrorSound();
                        this.showAddItemToOrder = true;
                        this.addItemFieldValues($form, response);
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="${this.Module}Controller.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`);
                    }
                    if (response.ShowAddCompleteToOrder === true) {
                        this.addItemFieldValues($form, response);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="${this.Module}Controller.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol add-complete" onclick="${this.Module}Controller.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`);
                    }
                    if (response.ShowUnstage === true) {
                        FwFunc.playErrorSound();
                        this.showAddItemToOrder = true;
                        this.addItemFieldValues($form, response);
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                        $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="${this.Module}Controller.unstageItem(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Unstage Item</div>`);
                    }
                    if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                        FwFunc.playErrorSound();
                        this.addItemFieldValues($form, response);
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                        $form.find('[data-datafield="Code"] input').select();
                    }
                }, response => {
                    FwFunc.showError(response);
                    $form.find('[data-datafield="Code"] input').select();
                }, $form);
            }
        });
        $form.find('[data-datafield="Quantity"] input').on('keydown', e => {
            if (this.showAddItemToOrder != true) {
                if (e.which == 9 || e.which == 13) {
                    e.preventDefault();
                    errorMsg.html('');
                    $form.find('div.AddItemToOrder').html('');
                    const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                    const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
                    const code = FwFormField.getValueByDataField($form, 'Code');
                    const quantity = +FwFormField.getValueByDataField($form, 'Quantity');
                    const request = {
                        OrderId: orderId,
                        Code: code,
                        WarehouseId: warehouse.warehouseid,
                        Quantity: quantity
                    };
                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
                        if (response.success === true) {
                            FwFunc.playSuccessSound();
                            this.addItemFieldValues($form, response);
                            debouncedRefreshGrid();
                            FwFormField.setValueByDataField($form, 'Quantity', 0);
                            $form.find('[data-datafield="Code"] input').select();
                        }
                        if (response.ShowAddItemToOrder === true) {
                            FwFunc.playErrorSound();
                            this.addItemFieldValues($form, response);
                            this.showAddItemToOrder = true;
                            errorMsg.html(`<div><span>${response.msg}</span></div>`);
                            $form.find('div.AddItemToOrder').html(`<div class="formrow fwformcontrol" onclick="${this.Module}Controller.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`);
                        }
                        if (response.ShowAddCompleteToOrder === true) {
                            this.addItemFieldValues($form, response);
                            $form.find('div.AddItemToOrder').html(`<div class="formrow"><div class="fwformcontrol" onclick="${this.Module}Controller.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol add-complete" onclick="${this.Module}Controller.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`);
                        }
                        if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                            FwFunc.playErrorSound();
                            this.addItemFieldValues($form, response);
                            errorMsg.html(`<div><span>${response.msg}</span></div>`);
                            $form.find('[data-datafield="Code"] input').select();
                        }
                    }, response => {
                        FwFunc.showError(response);
                        $form.find('[data-datafield="Code"] input').select();
                    }, $form);
                }
            }
        });
        $form.find('.orderstatus').on('click', e => {
            try {
                const orderInfo = {};
                orderInfo.OrderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
                orderInfo.OrderNumber = FwFormField.getTextByDataField($form, `${this.Type}Id`);
                const $orderStatusForm = window[`${this.Type === 'ContainerItem' ? 'Container' : this.Type}StatusController`].openForm('EDIT', orderInfo);
                FwModule.openSubModuleTab($form, $orderStatusForm);
                const $tabPage = FwTabs.getTabPageByElement($orderStatusForm);
                const $tab = FwTabs.getTabByElement(jQuery($tabPage));
                $tab.find('.caption').html(`${this.Type === 'ContainerItem' ? 'Container' : this.Type} Status`);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $form.find('.right-arrow').on('click', e => {
            this.moveStagedItemToOut($form);
            $form.find('.right-arrow').addClass('arrow-clicked');
            $form.find('.left-arrow').removeClass('arrow-clicked');
        });
        $form.find('.left-arrow').on('click', e => {
            this.moveOutItemToStaged($form);
            $form.find('.left-arrow').addClass('arrow-clicked');
            $form.find('.right-arrow').removeClass('arrow-clicked');
        });
        $form.find('.complete-checkout-contract').on('click', e => {
            this.completeCheckOutContract($form, e);
        });
        $form.find('.createcontract').on('click', e => {
            this.createContract($form, e);
        });
        $form.find('.options-button').on('click', e => {
            $form.find('.option-list').toggle();
        });
        $form.find('.abort-checkout-contract').on('click', e => {
            this.abortPartialContract($form);
        });
        $form.find('[data-datafield="IncludeZeroRemaining"] input').on('change', e => {
            const $stageQuantityItemGrid = $form.find('[data-name="StageQuantityItemGrid"]');
            $stageQuantityItemGrid.data('ondatabind', request => {
                const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
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
        $form.find('div[data-datafield="GridView"]').on('change', e => {
            const $target = jQuery(e.currentTarget);
            const gridView = FwFormField.getValueByDataField($form, 'GridView');
            const stagedItemGridContainer = $form.find('.staged-item-grid');
            const checkOutPendingItemGridContainier = $form.find('.pending-item-grid');
            const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
            const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
            const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
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
            }
            else {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select an Order before switching views.');
            }
        });
        $form.find('.partial-contract-inputs input').on('keydown', e => {
            const barCodeFieldValue = $form.find('.partial-contract-barcode input').val();
            if (e.which == 13 && barCodeFieldValue !== '') {
                if ($form.find('.right-arrow').hasClass('arrow-clicked')) {
                    this.moveStagedItemToOut($form);
                }
                else if ($form.find('.left-arrow').hasClass('arrow-clicked')) {
                    this.moveOutItemToStaged($form);
                }
                else {
                    this.moveStagedItemToOut($form);
                    $form.find('.right-arrow').addClass('arrow-clicked');
                    $form.find('.left-arrow').removeClass('arrow-clicked');
                }
            }
        });
        $form.find('.selectnone').on('click', e => {
            const $confirmation = FwConfirmation.renderConfirmation(`Unstage All`, `Unstage ALL Quantity items staged on this Order?`);
            const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
            FwConfirmation.addButton($confirmation, 'No', true);
            $yes.on('click', () => {
                FwConfirmation.destroyConfirmation($confirmation);
                let request = {};
                const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
                request.OrderId = orderId;
                FwAppData.apiMethod(true, 'POST', `api/v1/stagequantityitem/selectnone`, request, FwServices.defaultTimeout, response => {
                    errorMsgQty.html('');
                    if (response.success === false) {
                        FwFunc.playErrorSound();
                        errorMsgQty.html(`<div><span>${response.msg}</span></div>`);
                    }
                    else {
                        FwFunc.playSuccessSound();
                        const $stageQuantityItemGrid = $form.find('div[data-name="StageQuantityItemGrid"]');
                        FwBrowse.search($stageQuantityItemGrid);
                    }
                }, response => {
                    FwFunc.showError(response);
                }, $form);
            });
        });
        $form.find('.selectall').on('click', e => {
            let request = {};
            $form.find('.option-list').show();
            FwFormField.setValueByDataField($form, 'IncludeZeroRemaining', true);
            const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
            request.OrderId = orderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/stagequantityitem/selectall`, request, FwServices.defaultTimeout, response => {
                errorMsgQty.html('');
                if (response.success === false) {
                    FwFunc.playErrorSound();
                    errorMsgQty.html(`<div><span>${response.msg}</span></div>`);
                }
                else {
                    FwFunc.playSuccessSound();
                    const $stageQuantityItemGrid = $form.find('div[data-name="StageQuantityItemGrid"]');
                    FwBrowse.search($stageQuantityItemGrid);
                }
            }, response => {
                FwFunc.showError(response);
            }, $form);
        });
    }
    ;
    beforeValidate(datafield, request, $validationbrowse, $form, $tr) {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        switch (validationName) {
            case 'OrderValidation':
                request.miscfields = {
                    Staging: true,
                    StagingWarehouseId: warehouse.warehouseid
                };
                break;
            case 'TransferOrderValidation':
                request.miscfields = {
                    TransferOut: true,
                    TransferOutWarehouseId: warehouse.warehouseid
                };
                break;
            case 'ContainerItemValidation':
                request.uniqueids = {
                    WarehouseId: warehouse.warehouseid
                };
                break;
            case 'ContainerValidation':
                const inventoryId = FwFormField.getValueByDataField($validationbrowse, 'InventoryId');
                request.uniqueids = {
                    ScannableInventoryId: inventoryId
                };
                break;
        }
        ;
    }
    addItemToOrder(element) {
        this.showAddItemToOrder = false;
        const $element = jQuery(element);
        const $form = jQuery($element).closest('.fwform');
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
        const code = FwFormField.getValueByDataField($form, 'Code');
        const quantity = +FwFormField.getValueByDataField($form, 'Quantity');
        let request = {};
        if ($element.hasClass('add-complete')) {
            if (quantity != 0) {
                request = {
                    OrderId: orderId,
                    Code: code,
                    Warehouse: warehouse.warehouseid,
                    AddCompleteToOrder: true,
                    Quantity: quantity
                };
            }
            else {
                request = {
                    OrderId: orderId,
                    Code: code,
                    Warehouse: warehouse.warehouseid,
                    AddCompleteToOrder: true
                };
            }
        }
        else {
            if (quantity != 0) {
                request = {
                    OrderId: orderId,
                    Code: code,
                    Warehouse: warehouse.warehouseid,
                    AddItemToOrder: true,
                    Quantity: quantity
                };
            }
            else {
                request = {
                    OrderId: orderId,
                    Code: code,
                    Warehouse: warehouse.warehouseid,
                    AddItemToOrder: true
                };
            }
        }
        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
            try {
                const gridView = FwFormField.getValueByDataField($form, 'GridView');
                if (gridView === 'STAGE') {
                    const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                    FwBrowse.search($stagedItemGrid);
                }
                else {
                    const $checkOutPendingItemGrid = $form.find('[data-name="CheckOutPendingItemGrid"]');
                    FwBrowse.search($checkOutPendingItemGrid);
                }
                $form.find('.error-msg:not(.qty)').html('');
                $form.find('div.AddItemToOrder').html('');
                FwFunc.playSuccessSound();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
        $form.find('[data-datafield="Code"] input').select();
    }
    unstageItem(element) {
        this.showAddItemToOrder = false;
        const $element = jQuery(element);
        const $form = jQuery($element).closest('.fwform');
        let request = {};
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const orderId = FwFormField.getValueByDataField($form, `${this.Type}Id`);
        const code = FwFormField.getValueByDataField($form, 'Code');
        request = {
            OrderId: orderId,
            Code: code,
            Warehouse: warehouse.warehouseid,
            UnstageItem: true
        };
        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, request, FwServices.defaultTimeout, response => {
            try {
                this.refreshGridForScanning($form);
                $form.find('.error-msg:not(.qty)').html('');
                $form.find('div.AddItemToOrder').html('');
                FwFunc.playSuccessSound();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
        $form.find('[data-datafield="Code"] input').select();
    }
    resetForm($form) {
        $form.find('.clearable').find('input').val('');
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
        $form.find('.partial-contract').hide();
        $form.find('.complete-checkout-contract').hide();
        $form.find('.abort-checkout-contract').hide();
        $form.find('[data-caption="Items"]').show();
        FwFormField.enable($form.find(`div[data-datafield="${this.Type}Id"]`));
        $form.find('div[data-name="StagedItemGrid"] tr.viewmode').empty();
        $form.find('div[data-name="CheckOutPendingItemGrid"] tr.viewmode').empty();
        $form.find('div[data-name="CheckedOutItemGrid"] tr.viewmode').empty();
        $form.find('div[data-name="StageQuantityItemGrid"] tr.viewmode').empty();
        $form.find(`div[data-datafield="${this.Type}Id"]`).focus();
        $form.find('.original-buttons').show();
        $form.find('.orderstatus').show();
        $form.find('.createcontract').show();
        this.contractId = '';
        $form.find('div.caption:contains(Cancel Staging / Check-Out)').parent().attr('data-enabled', 'false');
        $form.find('.suspendedsession').show();
    }
    addLegend($form, $grid) {
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
    }
    getFormTemplate() {
        let tabCaption;
        let typeHTML;
        let statusBtnCaption;
        let createBtnCaption;
        switch (this.Module) {
            case 'StagingCheckout':
                tabCaption = 'Staging';
                typeHTML = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>`;
                statusBtnCaption = 'Order Status';
                createBtnCaption = 'Create Contract';
                break;
            case 'TransferOut':
                tabCaption = this.caption;
                typeHTML = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Transfer No." data-datafield="TransferId" data-displayfield="TransferNumber" data-validationname="TransferOrderValidation" style="flex:0 1 175px;"></div>`;
                statusBtnCaption = 'Transfer Status';
                createBtnCaption = 'Create Manifest';
                break;
            case 'FillContainer':
                tabCaption = this.caption;
                typeHTML = `<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Container Item" data-datafield="BarCode" style="flex:0 1 175px;"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-datafield="ContainerItemId" data-displayfield="BarCode" data-validationname="ContainerItemValidation" style="display:none;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Order Id" data-datafield="OrderId" style="display:none;"></div>`;
                statusBtnCaption = 'Container Status';
                createBtnCaption = 'Fill Container';
                break;
        }
        return `
        <div id="stagingcheckoutform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="${this.caption}" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="${this.Module}Controller">
          <div id="checkoutform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="stagingtab" class="tab staging-tab" data-tabpageid="stagingtabpage" data-caption="${tabCaption}"></div>
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
                            ${typeHTML}
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
                              <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="GridView" style="flex:1 1 250px;">
                                <div data-value="STAGE" data-caption="View Staged" style="margin-top:15px;"></div>
                                <div data-value="PENDING" data-caption="View Pending Items" style="margin-top:-4px;"></div>
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
                            <div class="orderstatus fwformcontrol" data-type="button" style="flex:0 1 145px; margin-left:8px; text-align:center;">${statusBtnCaption}</div>
                            <div class="createcontract" data-type="btnmenu" style="flex:0 1 201px;margin-right:7px;" data-caption="${createBtnCaption}"></div>
                          </div>
                          <div class="fwformcontrol abort-checkout-contract" data-type="button" style="max-width:157px;display:none;"><< Back to Staging</div>
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
                          <div class="flexrow" style="align-items:space-between;">
                            <div class="fwformcontrol complete-checkout-contract" data-type="button" style="max-width:140px;">${createBtnCaption}</div>
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
                  <div class="formrow add-item-qty"></div>
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="StageQuantityItemGrid" data-securitycaption=""></div>
                  </div>
                  <div class="formrow">
                    <div class="fwformcontrol options-button" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                    <div class="fwformcontrol selectall" data-type="button" style="float:left; margin-left:10px;">Stage All</div>
                    <div class="fwformcontrol selectnone" data-type="button" style="float:left; margin-left:10px;">Unstage All</div>
                  </div>
                  <div class="formrow option-list" style="display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show items with zero Remaining" data-datafield="IncludeZeroRemaining"></div>
                  </div>
                </div>
              </div>
              <!--HOLDING PAGE-->
              <div data-type="tabpage" id="holdingitemtabpage" class="tabpage" data-tabid="holdingitemtab">
                <div class="flexpage">
                  <div class="flexrow error-msg holding"></div>
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
//# sourceMappingURL=StagingCheckoutBase.js.map