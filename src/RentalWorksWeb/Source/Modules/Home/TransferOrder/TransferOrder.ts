routes.push({ pattern: /^module\/transferorder$/, action: function (match: RegExpExecArray) { return TransferOrderController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class TransferOrder {
    Module: string = 'TransferOrder';
    apiurl: string = 'api/v1/transferorder';
    caption: string = 'Transfer Order';
    nav: string = 'module/transferorder';
    id: string = 'F089C9A9-554D-40BF-B1FA-015FEDE43591';
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const self = this;
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        //warehouse filter
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);
        const $allWarehouses = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");

        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }

        let viewLocation: Array<JQuery> = [];
        viewLocation.push($userWarehouse, $allWarehouses);
        FwMenu.addViewBtn($menuObject, 'Warehouse', viewLocation, true, "WarehouseId");

        const location = JSON.parse(sessionStorage.getItem('location'));
        const $userLoc: JQuery = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);
        const $allLoc = FwMenu.generateDropDownViewBtn('ALL', false, "ALL");

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        let view: Array<JQuery> = [];
        view.push($userLoc, $allLoc);
        FwMenu.addViewBtn($menuObject, 'Location', view, true, "LocationId");

        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        FwFormField.loadItems($form.find('.outtype'), [
            { value: 'DELIVER', text: 'Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Pick Up' }
        ], true);

        if (mode === 'NEW') {
            const department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid, department.department);

            const userId = sessionStorage.getItem('usersid');
            const userName = sessionStorage.getItem('name');
            FwFormField.setValueByDataField($form, 'AgentId', userId, userName);

            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'ShipDate', today);

            $form.find('[data-datafield="Rental"] input').prop('checked', true);

            const location = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValueByDataField($form, 'OfficeLocationId', location.locationid, location.location);

            $form.find('.manifestSubModule').append(this.openManifestBrowse($form));
            $form.find('.receiptSubModule').append(this.openReceiptBrowse($form));
            $form.find('.picklistSubModule').append(this.openPickListBrowse($form));
        }

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any): JQuery {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="TransferId"] input').val(uniqueids.TransferId);
        FwModule.loadForm(this.Module, $form);

        $form.find('.manifestSubModule').append(this.openManifestBrowse($form));
        $form.find('.receiptSubModule').append(this.openReceiptBrowse($form));
        $form.find('.picklistSubModule').append(this.openPickListBrowse($form));

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openPickListBrowse($form) {
        const $browse = PickListController.openBrowse();
        $browse.data('ondatabind', request => {
            request.activeviewfields = PickListController.ActiveViewFields;
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TransferId')
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openManifestBrowse($form) {
        const $browse = ManifestController.openBrowse();
        $browse.data('ondatabind', request => {
            request.activeviewfields = ManifestController.ActiveViewFields;
            request.uniqueids = {
                ContractType: "MANIFEST"
                , TransferId: FwFormField.getValueByDataField($form, 'TransferId')
            };
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openReceiptBrowse($form) {
        const $browse = TransferReceiptController.openBrowse();
        $browse.data('ondatabind', request => {
            request.activeviewfields = TransferReceiptController.ActiveViewFields;
            request.uniqueids = {
                ContractType: "RECEIPT"
                , TransferId: FwFormField.getValueByDataField($form, 'TransferId')
            };
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        // ----------
        const $picklistGrid = $form.find('div[data-grid="OrderPickListGrid"]');
        const $picklistGridControl = FwBrowse.loadGridFromTemplate('OrderPickListGrid');
        $picklistGrid.empty().append($picklistGridControl);
        $picklistGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TransferId')
            };
        });
        FwBrowse.init($picklistGridControl);
        FwBrowse.renderRuntimeHtml($picklistGridControl);
        // ----------
        const $orderItemRentalGrid = $form.find('.rentalItemGrid div[data-grid="TransferOrderItemGrid"]');
        const $orderItemRentalGridControl = FwBrowse.loadGridFromTemplate('TransferOrderItemGrid');
        $orderItemRentalGrid.empty().append($orderItemRentalGridControl);
        $orderItemRentalGrid.addClass('R');
        $orderItemRentalGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TransferId')
                , Rectype: 'R'
            };
        });
        FwBrowse.init($orderItemRentalGridControl);
        FwBrowse.renderRuntimeHtml($orderItemRentalGridControl);
        // ----------
        const $orderItemSalesGrid = $form.find('.salesItemGrid div[data-grid="TransferOrderItemGrid"]');
        const $orderItemSalesGridControl = FwBrowse.loadGridFromTemplate('TransferOrderItemGrid');
        $orderItemSalesGrid.empty().append($orderItemSalesGridControl);
        $orderItemSalesGrid.addClass('S');
        $orderItemSalesGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TransferId')
                , Rectype: 'S'
            };
        });
        FwBrowse.init($orderItemSalesGridControl);
        FwBrowse.renderRuntimeHtml($orderItemSalesGridControl);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        $form.find('.submodule').show();

        const $orderItemRentalGrid = $form.find('.rentalItemGrid [data-name="TransferOrderItemGrid"]');
        FwBrowse.search($orderItemRentalGrid);

        const $orderItemSalesGrid = $form.find('.salesItemGrid [data-name="TransferOrderItemGrid"]');
        FwBrowse.search($orderItemSalesGrid);

        const isRental = FwFormField.getValueByDataField($form, 'Rental');
        const rentalTab = $form.find('.rentalTab');
        isRental ? rentalTab.show() : rentalTab.hide();

        const isSales = FwFormField.getValueByDataField($form, 'Sales');
        const salesTab = $form.find('.salesTab');
        isSales ? salesTab.show() : salesTab.hide();

        //Load grids and browses upon clicking their tab
        $form.on('click', '[data-type="tab"]', e => {
            const tabname = jQuery(e.currentTarget).attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    const $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    events($form: JQuery) {
        $form.on('change', '[data-datafield="Sales"] input', e => {
            const $this = jQuery(e.currentTarget);
            const salesTab = $form.find('.salesTab');
            $this.prop('checked') === true ? salesTab.show() : salesTab.hide();
        });
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $form, request) {
        const validationName = request.module;
        const warehouse = FwFormField.getValueByDataField($form, 'FromWarehouseId');

        switch (validationName) {
            case 'UserValidation':
                request.uniqueids = {
                    WarehouseId: warehouse
                };
                break;
        };
    }
};

//-----------------------------------------------------------------------------------------------------
//Open Search Interface
FwApplicationTree.clickEvents['{16CD0101-28D7-49E2-A3ED-43C03152FEE6}'] = function (event) {
    let search, $form, transferId;
    $form = jQuery(this).closest('.fwform');
    transferId = FwFormField.getValueByDataField($form, 'TransferId');

    if ($form.attr('data-mode') === 'NEW') {
        TransferOrderController.saveForm($form, { closetab: false });
        return;
    }

    if (transferId == "") {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    } else {
        search = new SearchInterface();
        search.renderSearchPopup($form, transferId, 'Transfer');
    }
};
//----------------------------------------------------------------------------------------------
//Transfer Status
FwApplicationTree.clickEvents['{A256288F-238F-4594-8A6A-3B70613925DA}'] = e => {
    try {
        const $form = jQuery(e.currentTarget).closest('.fwform');
        const mode = 'EDIT';
        const orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'TransferId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'TransferNumber');
        orderInfo.IsTransfer = true;
        const $orderStatusForm = OrderStatusController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $orderStatusForm);
        jQuery('.tab.submodule.active').find('.caption').html('Transfer Status');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//Transfer Out
FwApplicationTree.clickEvents['{D0AB3734-7F96-46A6-8297-331110A4854F}'] = e => {
    try {
        const $form = jQuery(e.currentTarget).closest('.fwform');
        const mode = 'EDIT';
        const orderInfo: any = {};
        orderInfo.TransferId = FwFormField.getValueByDataField($form, 'TransferId');
        orderInfo.TransferNumber = FwFormField.getValueByDataField($form, 'TransferNumber');
        const $stagingCheckoutForm = TransferOutController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $stagingCheckoutForm);
        jQuery('.tab.submodule.active').find('.caption').html('Transfer Out');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
//Transfer In
FwApplicationTree.clickEvents['{E362D71D-7597-4752-8BDD-72EE0CB7B2C4}'] = e => {
    try {
        const $form = jQuery(e.currentTarget).closest('.fwform');
        const mode = 'EDIT';
        const orderInfo: any = {};
        orderInfo.TransferId = FwFormField.getValueByDataField($form, 'TransferId');
        orderInfo.TransferNumber = FwFormField.getValueByDataField($form, 'TransferNumber');
        const $checkinForm = TransferInController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $checkinForm);
        jQuery('.tab.submodule.active').find('.caption').html('Transfer In');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//Confirm Transfer
//-----------------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{A35F0AAD-81B5-4A0C-8970-D448A67D5A82}'] = e => {
    const $form = jQuery(e.currentTarget).closest('.fwform');
    const transferNumber = FwFormField.getValueByDataField($form, `TransferNumber`);
    const $confirmation = FwConfirmation.renderConfirmation('Confirm Transfer', '');
    $confirmation.find('.fwconfirmationbox').css('width', '450px');
    const html = `<div class="fwform" data-controller="none" style="background-color: transparent;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div>Confirm Transfer Number ${transferNumber}?</div>
                    </div>
                  </div>`;
    FwConfirmation.addControls($confirmation, html);
    const $yes = FwConfirmation.addButton($confirmation, 'Confirm Transfer', false);
    FwConfirmation.addButton($confirmation, 'Cancel');

    $yes.on('click', e => {
        const transferId = FwFormField.getValueByDataField($form, 'TransferId');
        FwAppData.apiMethod(true, 'POST', `api/v1/transferorder/confirm/${transferId}`, null, FwServices.defaultTimeout, response => {
            FwNotification.renderNotification('SUCCESS', 'Transfer Order Successfully Confirmed.');
            FwConfirmation.destroyConfirmation($confirmation);
            FwModule.refreshForm($form, TransferOrderController);
        }, null, $confirmation);
    });
};
//-----------------------------------------------------------------------------------------------------
//Create Pick List
//-----------------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{5CA07E25-A93E-4FA0-9206-B3F556684B0C}'] = e => {
    try {
        const $form = jQuery(e.currentTarget).closest('.fwform');
        const mode = 'EDIT';
        const orderInfo: any = {};
        orderInfo.IsTransfer = true;
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'TransferId');
        const $pickListForm = CreatePickListController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $pickListForm);
        jQuery('.tab.submodule.active').find('.caption').html('New Pick List');
        const $pickListUtilityGrid = $pickListForm.find('[data-name="PickListUtilityGrid"]');
        FwBrowse.search($pickListUtilityGrid);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//-----------------------------------------------------------------------------------------------------
var TransferOrderController = new TransferOrder();