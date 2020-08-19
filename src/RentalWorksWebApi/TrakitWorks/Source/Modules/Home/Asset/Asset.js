routes.push({ pattern: /^module\/asset$/, action: function (match) { return AssetController.getModuleScreen(); } });
class Asset {
    constructor() {
        this.Module = 'Asset';
        this.apiurl = 'api/v1/item';
        this.caption = Constants.Modules.Home.Asset.caption;
        this.nav = Constants.Modules.Home.Asset.nav;
        this.id = Constants.Modules.Home.Asset.id;
        this.ActiveViewFields = {};
    }
    getModuleScreen() {
        var screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        var $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    }
    ;
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        const self = this;
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });
        FwAppData.apiMethod(true, 'GET', "api/v1/inventorystatus", null, FwServices.defaultTimeout, function onSuccess(response) {
            for (let i = 0; i < response.length; i++) {
                FwBrowse.addLegend($browse, response[i].InventoryStatus, response[i].Color);
            }
        }, null, $browse);
        return $browse;
    }
    ;
    addBrowseMenuItems($menuObject) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $all = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");
        const $userWarehouse = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);
        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }
        let viewSubitems = [];
        viewSubitems.push($userWarehouse, $all);
        FwMenu.addViewBtn($menuObject, 'Warehouse', viewSubitems, true, "WarehouseId");
        const $trackAll = FwMenu.generateDropDownViewBtn('ALL', true, "ALL");
        const $trackBarcode = FwMenu.generateDropDownViewBtn('Bar Code', false, "BARCODE");
        const $trackSerialNumber = FwMenu.generateDropDownViewBtn('Serial Number', false, "SERIALNO");
        const $trackRFID = FwMenu.generateDropDownViewBtn('RFID', false, "RFID");
        let viewTrack = [];
        viewTrack.push($trackAll, $trackBarcode, $trackSerialNumber, $trackRFID);
        FwMenu.addViewBtn($menuObject, 'Tracked By', viewTrack, true, "TrackedBy");
        return $menuObject;
    }
    ;
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    ;
    loadForm(uniqueids) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ItemId', uniqueids.ItemId);
        FwModule.loadForm(this.Module, $form);
        let $submoduleRepairOrderBrowse = this.openRepairOrderBrowse($form);
        $form.find('.repairOrderSubModule').append($submoduleRepairOrderBrowse);
        return $form;
    }
    ;
    openRepairOrderBrowse($form) {
        let itemId = FwFormField.getValueByDataField($form, 'ItemId');
        let $browse;
        $browse = RepairController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = RepairController.ActiveViewFields;
            request.uniqueids = {
                ItemId: itemId
            };
        });
        jQuery($browse).find('.ddviewbtn-caption:contains("Show:")').siblings('.ddviewbtn-select').find('.ddviewbtn-dropdown-btn:contains("All")').click();
        return $browse;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    ;
    loadAudit($form) {
        var uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
        FwModule.loadAudit($form, uniqueid);
    }
    ;
    renderGrids($form) {
        var $itemAttributeValueGrid = $form.find('div[data-grid="ItemAttributeValueGrid"]');
        var $itemAttributeValueGridControl = jQuery(jQuery('#tmpl-grids-ItemAttributeValueGridBrowse').html());
        $itemAttributeValueGrid.empty().append($itemAttributeValueGridControl);
        $itemAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: FwFormField.getValueByDataField($form, 'ItemId')
            };
        });
        $itemAttributeValueGridControl.data('beforesave', function (request) {
            request.ItemId = FwFormField.getValueByDataField($form, 'ItemId');
        });
        FwBrowse.init($itemAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($itemAttributeValueGridControl);
        var $itemQcGrid = $form.find('div[data-grid="ItemQcGrid"]');
        var $itemQcGridControl = jQuery(jQuery('#tmpl-grids-ItemQcGridBrowse').html());
        $itemQcGrid.empty().append($itemQcGridControl);
        $itemQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: FwFormField.getValueByDataField($form, 'ItemId')
            };
        });
        FwBrowse.init($itemQcGridControl);
        FwBrowse.renderRuntimeHtml($itemQcGridControl);
    }
    ;
    afterLoad($form) {
        var $itemAttributeValueGrid = $form.find('[data-name="ItemAttributeValueGrid"]');
        FwBrowse.search($itemAttributeValueGrid);
        var $itemQcGrid = $form.find('[data-name="ItemQcGrid"]');
        FwBrowse.search($itemQcGrid);
        if (FwFormField.getValueByDataField($form, 'InventoryStatus') === "IN") {
            FwFormField.enable($form.find('[data-datafield="BarCode"]'));
            FwFormField.enable($form.find('[data-datafield="SerialNumber"]'));
            FwFormField.enable($form.find('[data-datafield="RfId"]'));
        }
    }
    ;
}
var AssetController = new Asset();
//# sourceMappingURL=Asset.js.map