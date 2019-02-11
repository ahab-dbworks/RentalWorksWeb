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
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
    };
    //----------------------------------------------------------------------------------------------
    events($form: JQuery) {
    };
};
//----------------------------------------------------------------------------------------------
var TransferOrderController = new TransferOrder();