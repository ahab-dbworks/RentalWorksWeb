routes.push({ pattern: /^module\/InventoryQuantityHistory/, action: function (match: RegExpExecArray) { return InventoryQuantityHistoryController.getModuleScreen(); } });

class InventoryQuantityHistory {
    Module: string = 'InventoryQuantityHistory';
    apiurl: string = 'api/v1/InventoryQuantityHistory';
    caption: string = Constants.Modules.Inventory.children.InventoryQuantityHistory.caption;
    nav: string = Constants.Modules.Inventory.children.InventoryQuantityHistory.nav;
    id: string = Constants.Modules.Inventory.children.InventoryQuantityHistory.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //-----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        FwMenu.addVerticleSeparator(options.$menu);
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $allWarehouses = FwMenu.generateDropDownViewBtn('ALL', false, "ALL");
        const $userWarehouse = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);
        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }
        let viewWarehouse = [];
        viewWarehouse.push($allWarehouses, $userWarehouse);
        FwMenu.addViewBtn(options.$menu, 'Warehouse', viewWarehouse, true, "WarehouseId");

        const $allTypes = FwMenu.generateDropDownViewBtn('ALL', true, 'all');
        const $qty = FwMenu.generateDropDownViewBtn('Quantity', false, 'qty');
        const $in = FwMenu.generateDropDownViewBtn('In', false, 'qtyin');
        const $staged = FwMenu.generateDropDownViewBtn('Staged', false, 'qtystaged');
        const $out = FwMenu.generateDropDownViewBtn('Out', false, 'qtyout');
        const $inTransit = FwMenu.generateDropDownViewBtn('In Transit', false, 'qtytransit');
        const $inContainer = FwMenu.generateDropDownViewBtn('In Container', false, 'qtyincontainer');
        const $inRepair = FwMenu.generateDropDownViewBtn('In Repair', false, 'qtyinrepair');
        const $onTruck = FwMenu.generateDropDownViewBtn('On Truck', false, 'qtyintransit');

        FwMenu.addVerticleSeparator(options.$menu);

        let qtyType = [];
        qtyType.push($allTypes, $qty, $in, $staged, $out, $inTransit, $inContainer, $inRepair, $onTruck);
        FwMenu.addViewBtn(options.$menu, 'Quantity Type', qtyType, true, "QuantityType");
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = () => {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        return $browse;
    }
}
//----------------------------------------------------------------------------------------------
var InventoryQuantityHistoryController = new InventoryQuantityHistory();