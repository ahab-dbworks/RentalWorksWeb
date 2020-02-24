﻿routes.push({ pattern: /^module\/migrateorders/, action: function (match: RegExpExecArray) { return MigrateOrdersController.getModuleScreen(); } });

class MigrateOrders {
    Module: string = 'MigrateOrders';
    apiurl: string = 'api/v1/migrate';
    caption: string = Constants.Modules.Utilities.children.MigrateOrders.caption;
    nav: string = Constants.Modules.Utilities.children.MigrateOrders.nav;
    id: string = Constants.Modules.Utilities.children.MigrateOrders.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        var $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables "modified" asterisk
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        const department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        const rateType = JSON.parse(sessionStorage.getItem('defaultlocation')).ratetype;
        FwFormField.setValueByDataField($form, 'RateType', rateType, rateType);
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'FromDate', today);
        FwFormField.setValueByDataField($form, 'ToDate', today);
        FwFormField.setValueByDataField($form, 'CopyLineItemNotes', true);
        FwFormField.setValueByDataField($form, 'CopyRentalRates', true);
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);

        //add order browse
        const $orderBrowse = this.addOrderBrowse($form);
        $form.find('.orderBrowse').append($orderBrowse);

        $form.find('[data-datafield="DealId"], [data-datafield="DepartmentId"]').data('onchange', e => {
            const dealId = FwFormField.getValueByDataField($form, 'DealId');
            const departmentId = FwFormField.getValueByDataField($form, 'DepartmentId');
            const onDataBind = $orderBrowse.data('ondatabind');
            if (typeof onDataBind == 'function') {
                $orderBrowse.data('ondatabind', request => {
                    onDataBind(request);
                    request.miscfields.DealId = dealId;
                    request.miscfields.DepartmentId = departmentId;
                    request.miscfields.Migrate = true;
                });
            }
            FwBrowse.search($orderBrowse);
        });

        //toggle destination fields
        $form.find('[data-datafield="CreateNewOrder"], [data-datafield="MigrateToExistingOrder"]').on('change', e => {
            const datafield = jQuery(e.currentTarget).attr('data-datafield');
            let datafield2;
            if (datafield === 'CreateNewOrder') {
                datafield2 = 'MigrateToExistingOrder';
            } else {
                datafield2 = 'CreateNewOrder';
            }
            const isChecked = FwFormField.getValueByDataField($form, datafield);
            if (isChecked) {
                FwFormField.setValueByDataField($form, datafield2, false);
                FwFormField.disable($form.find(`.${datafield2}`));
                FwFormField.enable($form.find(`.${datafield}`));
            } else {
                FwFormField.setValueByDataField($form, datafield2, true);
                FwFormField.enable($form.find(`.${datafield2}`));
                FwFormField.disable($form.find(`.${datafield}`));
            }

            const isCreateNewOrder = FwFormField.getValueByDataField($form, 'CreateNewOrder');
            if (isCreateNewOrder) {
                $form.find('[data-datafield="PendingPO"]').change();
            }
        });
        FwFormField.setValueByDataField($form, 'CreateNewOrder', true, undefined, true);

        //toggle billing stop date
        $form.find('[data-datafield="UpdateBillingStopDate"]').on('change', e => {
            const isChecked = FwFormField.getValueByDataField($form, 'UpdateBillingStopDate');
            if (isChecked) {
                FwFormField.enable($form.find('[data-datafield="MigrateBillingStopDate"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="MigrateBillingStopDate"]'));
            }
        });

        //Toggle PO fields
        $form.find('[data-datafield="PendingPO"]').on('change', e => {
            const isChecked = FwFormField.getValueByDataField($form, 'PendingPO');
            if (isChecked) {
                FwFormField.disable($form.find('[data-datafield="FlatPO"], [data-datafield="PurchaseOrderNumber"], [data-datafield="PurchaseOrderAmount"]'));
            } else {
                FwFormField.enable($form.find('[data-datafield="FlatPO"], [data-datafield="PurchaseOrderNumber"], [data-datafield="PurchaseOrderAmount"]'));
            }
        });
        FwFormField.setValueByDataField($form, 'PendingPO', true, undefined, true);

        //default description and deal values
        $form.find('[data-datafield="OrderId"]').data('onchange', $tr => {
            const description = $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue');
            const dealId = $tr.find('[data-browsedatafield="DealId"]').attr('data-originalvalue');
            const deal = $tr.find('[data-browsedatafield="Deal"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'Description', description);
            FwFormField.setValueByDataField($form, 'MigrateDealId', dealId, deal);
        });

        //render grid upon clicking Items tab
        $form.find('[data-type="tab"][data-caption="Items"]').on('click', e => {
            this.renderMigrateItemGrid($form);  //starts session
        });


        const $migrateItemGrid = $form.find('div[data-grid="MigrateItemGrid"]');
        // Select All
        $form.find('.select-all').on('click', e => {
            const $migrateItemGridControl = $form.find('[data-name="MigrateItemGrid"]');
            const request: any = {};
            request.SessionId = $migrateItemGrid.data('sessionId');
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/selectall`, request, FwServices.defaultTimeout,
                response => {
                    $form.find('.error-msg').html('');
                    if (response.success === false) {
                        FwFunc.playErrorSound();
                        $form.find('div.error-msg').html(`<div><span>${response.msg}</span></div>`);
                    } else {
                        FwFunc.playSuccessSound();
                        FwBrowse.search($migrateItemGridControl);
                    }
                },
                ex => FwFunc.showError(ex), $migrateItemGrid);
        });
        // Select None
        $form.find('.select-none').on('click', e => {
            const $migrateItemGridControl = $form.find('[data-name="MigrateItemGrid"]');
            const request: any = {};
            request.SessionId = $migrateItemGrid.data('sessionId');
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/selectnone`, request, FwServices.defaultTimeout,
                response => {
                    $form.find('.error-msg').html('');
                    if (response.success === false) {
                        FwFunc.playErrorSound();
                        $form.find('div.error-msg').html(`<div><span>${response.msg}</span></div>`);
                    } else {
                        FwFunc.playSuccessSound();
                    }
                    FwBrowse.search($migrateItemGridControl);
                }, ex => FwFunc.showError(ex), $migrateItemGrid);
        });

        //finalize migration
        $form.find('.finalize-migration').on('click', e => {
            let request: any = {};
            request = {
                SessionId: $migrateItemGrid.data('sessionId')
                , MigrateToNewOrder: FwFormField.getValueByDataField($form, 'CreateNewOrder')
                , NewOrderOfficeLocationId: FwFormField.getValueByDataField($form, 'OfficeLocationId')
                , NewOrderWarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                , NewOrderDealId: FwFormField.getValueByDataField($form, 'CreateNewDealId')
                , NewOrderDescription: FwFormField.getValueByDataField($form, 'NewOrderDescription')
                , NewOrderRateType: FwFormField.getValueByDataField($form, 'RateType')
                , NewOrderFromDate: FwFormField.getValueByDataField($form, 'FromDate')
                , NewOrderFromTime: FwFormField.getValueByDataField($form, 'FromTime')
                , NewOrderToDate: FwFormField.getValueByDataField($form, 'ToDate')
                , NewOrderToTime: FwFormField.getValueByDataField($form, 'ToTime')
                , NewOrderBillingStopDate: FwFormField.getValueByDataField($form, 'BillingStopDate')
                , NewOrderPendingPO: FwFormField.getValueByDataField($form, 'PendingPO')
                , NewOrderFlatPO: FwFormField.getValueByDataField($form, 'FlatPO')
                , NewOrderPurchaseOrderNumber: FwFormField.getValueByDataField($form, 'PurchaseOrderNumber')
                , NewOrderPurchaseOrderAmount: FwFormField.getValueByDataField($form, 'PurchaseOrderAmount')
                , MigrateToExistingOrder: FwFormField.getValueByDataField($form, 'MigrateToExistingOrder')
                , ExistingOrderId: FwFormField.getValueByDataField($form, 'OrderId')
                , InventoryFulfillIncrement: FwFormField.getValueByDataField($form, 'InventoryFulfillIncrement')
                , InventoryCheckedOrStaged: FwFormField.getValueByDataField($form, 'InventoryCheckedOrStaged')
                , CopyLineItemNotes: FwFormField.getValueByDataField($form, 'CopyLineItemNotes')
                , CopyOrderNotes: FwFormField.getValueByDataField($form, 'CopyOrderNotes')
                , CopyRentalRates: FwFormField.getValueByDataField($form, 'CopyRentalRates')
                , UpdateBillingStopDate: FwFormField.getValueByDataField($form, 'UpdateBillingStopDate')
                , BillingStopDate: FwFormField.getValueByDataField($form, 'MigrateBillingStopDate')
            }
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/completesession`, request, FwServices.defaultTimeout,
                response => {
                    $form.find('.error-msg').html('');
                    if (response.success === false) {
                        FwFunc.playErrorSound();
                        $form.find('div.error-msg').html(`<div><span>${response.msg}</span></div>`);
                    } else {
                        FwFunc.playSuccessSound();
                        if (response.Contracts.length > 0) {
                            for (let i = 0; i < response.Contracts.length; i++) {
                                const contractId = response.Contracts[i].ContractId;
                                const contractInfo: any = {};
                                contractInfo.ContractId = contractId;
                                const $contractForm = ContractController.loadForm(contractInfo);
                                FwModule.openSubModuleTab($form, $contractForm);
                            }
                        }
                        $form.empty().append(MigrateOrdersController.openForm('NEW')); //reset migrate orders tab
                    }
                }, ex => FwFunc.showError(ex), $migrateItemGrid);
        });
    }
    //----------------------------------------------------------------------------------------------
    renderMigrateItemGrid($form) {
        const $migrateItemGrid = $form.find('div[data-grid="MigrateItemGrid"]');
        const $migrateItemGridControl = FwBrowse.loadGridFromTemplate('MigrateItemGrid');
        $migrateItemGrid.empty().append($migrateItemGridControl);
        FwBrowse.init($migrateItemGridControl);
        FwBrowse.renderRuntimeHtml($migrateItemGridControl);

        const $orderBrowse = $form.find('.orderBrowse .fwbrowse');
        const $selectedCheckboxes = $orderBrowse.find('tbody .cbselectrow:checked');

        let orderIds: any = [];
        let sessionId;
        if ($selectedCheckboxes.length > 0) {
            for (let i = 0; i < $selectedCheckboxes.length; i++) {
                const orderId = $selectedCheckboxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                if (orderId !== '') {
                    orderIds.push(orderId);
                }
            }
            orderIds = orderIds.join(',');

            const request: any = {};
            request.OrderIds = orderIds;
            request.DealId = FwFormField.getValueByDataField($form, 'DealId');
            request.DepartmentId = JSON.parse(sessionStorage.getItem('department')).departmentid;

            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/startsession`, request, FwServices.defaultTimeout,
                response => {
                    sessionId = response.SessionId;
                    if (sessionId) {
                        $migrateItemGrid.data('sessionId', sessionId);
                        $migrateItemGridControl.data('ondatabind', request => {
                            request.uniqueids = {
                                SessionId: sessionId
                            };
                        });
                        FwBrowse.search($migrateItemGridControl);
                    }
                }, ex => FwFunc.showError(ex), $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    addOrderBrowse($form) {
        let $browse = jQuery(OrderController.getBrowseTemplate());
        $browse.attr('data-hasmultirowselect', 'true');
        $browse.attr('data-type', 'Browse');
        $browse.attr('data-showsearch', 'false');
        $browse.attr('data-pagesize', 1000);
        $browse = FwModule.openBrowse($browse);
        $browse.find('.fwbrowse-menu').hide();

        const userLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
        $browse.data('ondatabind', request => {
            request.ActiveViewFields = { Status: ["ALL"], LocationId: [userLocationId] };
            request.orderby = 'OrderDate desc';
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
        switch (datafield) {
            case 'CreateNewDealId':
            case 'DealId':
                const shareDeals = JSON.parse(sessionStorage.getItem('controldefaults')).sharedealsacrossofficelocations;
                if (!shareDeals) {
                    request.uniqueids = {
                        LocationId: officeLocationId
                    }
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'CreateNewDealId':
                request.uniqueids = {
                    LocationId: officeLocationId
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecreatenewdeal`);
                break;
            case 'RateType':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateratetype`);
                break;
        }
    }
}
var MigrateOrdersController = new MigrateOrders();