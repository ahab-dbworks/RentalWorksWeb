routes.push({ pattern: /^module\/transferorder$/, action: function (match: RegExpExecArray) { return TransferOrderController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class TransferOrder {
    Module: string = 'TransferOrder';
    apiurl: string = 'api/v1/transferorder';
    caption: string = 'Transfer Order';
    nav: string = 'module/transferorder';
    id: string = 'F089C9A9-554D-40BF-B1FA-015FEDE43591';
    ActiveView: string = 'ALL';
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

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        this.ActiveView = `WarehouseId=${warehouse.warehouseid}`;

        $browse.data('ondatabind', request => {
            request.activeview = this.ActiveView;
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        //warehouse filter
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true);
        $userWarehouse.on('click', e => {
            const $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = `WarehouseId=${warehouse.warehouseid}`;
            FwBrowse.search($browse);
        });
        const $allWarehouses = FwMenu.generateDropDownViewBtn('ALL Warehouses', false);
        $allWarehouses.on('click', e => {
            const $browse = jQuery(e.currentTarget).closest('.fwbrowse');
            this.ActiveView = 'WarehouseId=ALL';
            FwBrowse.search($browse);
        });
        const viewLocation = [];
        viewLocation.push($userWarehouse, $allWarehouses);
        FwMenu.addViewBtn($menuObject, 'Warehouse', viewLocation);
        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        FwFormField.loadItems($form.find('.outtype'), [
            { value: 'DELIVER', text: 'Deliver to Customer' },
            { value: 'SHIP', text: 'Ship to Customer' },
            { value: 'PICK UP', text: 'Customer Pick Up' }
        ], true);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any): JQuery {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="TransferId"] input').val(uniqueids.TransferId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
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
    };
    //----------------------------------------------------------------------------------------------
    events($form: JQuery) {
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
var TransferOrderController = new TransferOrder();