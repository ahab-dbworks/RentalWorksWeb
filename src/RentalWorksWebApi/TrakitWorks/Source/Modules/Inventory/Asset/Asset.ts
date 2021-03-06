routes.push({ pattern: /^module\/asset$/, action: function (match: RegExpExecArray) { return AssetController.getModuleScreen(); } });

class Asset {
    Module:             string = 'Asset';
    apiurl:             string = 'api/v1/item';
    caption: string = Constants.Modules.Inventory.children.Asset.caption;
	nav: string = Constants.Modules.Inventory.children.Asset.nav;
	id: string = Constants.Modules.Inventory.children.Asset.id;
    ActiveViewFields:   any    = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);

        var $browse: JQuery = this.openBrowse();

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
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
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
    };
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);

        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($userWarehouse, $all);
        FwMenu.addViewBtn(options.$menu, 'Warehouse', viewSubitems, true, "WarehouseId");

        //Tracked By Filter
        const $trackAll = FwMenu.generateDropDownViewBtn('ALL', true, "ALL");
        const $trackBarcode = FwMenu.generateDropDownViewBtn('Bar Code', false, "BARCODE");
        const $trackSerialNumber = FwMenu.generateDropDownViewBtn('Serial Number', false, "SERIALNO");
        const $trackRFID = FwMenu.generateDropDownViewBtn('RFID', false, "RFID");

        let viewTrack: Array<JQuery> = [];
        viewTrack.push($trackAll, $trackBarcode, $trackSerialNumber, $trackRFID);
        FwMenu.addViewBtn(options.$menu, 'Tracked By', viewTrack, true, "TrackedBy");
        return options;
    };
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form     = FwModule.openForm($form, mode);

        return $form;
    };
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ItemId', uniqueids.ItemId);
        FwModule.loadForm(this.Module, $form);

        let $submoduleRepairOrderBrowse = this.openRepairOrderBrowse($form);
        $form.find('.repairOrderSubModule').append($submoduleRepairOrderBrowse);
        return $form;
    };
    //---------------------------------------------------------------------------------------------
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
        //$browse.attr('data-activeinactiveview', 'all');
        jQuery($browse).find('.ddviewbtn-caption:contains("Show:")').siblings('.ddviewbtn-select').find('.ddviewbtn-dropdown-btn:contains("All")').click();
        return $browse;
    }
   //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //---------------------------------------------------------------------------------------------
    loadAudit($form: JQuery) {
        var uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
        FwModule.loadAudit($form, uniqueid);
    };
    //---------------------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
        var $itemAttributeValueGrid: JQuery = $form.find('div[data-grid="ItemAttributeValueGrid"]');
        var $itemAttributeValueGridControl: JQuery = jQuery(jQuery('#tmpl-grids-ItemAttributeValueGridBrowse').html());
        $itemAttributeValueGrid.empty().append($itemAttributeValueGridControl);
        $itemAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: FwFormField.getValueByDataField($form, 'ItemId')
            };
        });
        $itemAttributeValueGridControl.data('beforesave', function (request) {
            request.ItemId = FwFormField.getValueByDataField($form, 'ItemId');
        })
        FwBrowse.init($itemAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($itemAttributeValueGridControl);

        var $itemQcGrid: JQuery = $form.find('div[data-grid="ItemQcGrid"]');
        var $itemQcGridControl: JQuery = jQuery(jQuery('#tmpl-grids-ItemQcGridBrowse').html());
        $itemQcGrid.empty().append($itemQcGridControl);
        $itemQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: FwFormField.getValueByDataField($form, 'ItemId')
            };
        })
        FwBrowse.init($itemQcGridControl);
        FwBrowse.renderRuntimeHtml($itemQcGridControl);
    };
    //---------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        var $itemAttributeValueGrid: JQuery = $form.find('[data-name="ItemAttributeValueGrid"]');
        FwBrowse.search($itemAttributeValueGrid);

        var $itemQcGrid: JQuery = $form.find('[data-name="ItemQcGrid"]');
        FwBrowse.search($itemQcGrid);

        if (FwFormField.getValueByDataField($form, 'InventoryStatus') === "IN") {
            FwFormField.enable($form.find('[data-datafield="BarCode"]'));
            FwFormField.enable($form.find('[data-datafield="SerialNumber"]'));
            FwFormField.enable($form.find('[data-datafield="RfId"]'));
        }
    };
    //---------------------------------------------------------------------------------------------
}

var AssetController = new Asset();
