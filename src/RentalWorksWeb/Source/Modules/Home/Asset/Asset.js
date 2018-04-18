routes.push({ pattern: /^module\/asset$/, action: function (match) { return AssetController.getModuleScreen(); } });
var RwAsset = (function () {
    function RwAsset() {
        this.Module = 'Asset';
        this.apiurl = 'api/v1/item';
        this.caption = 'Asset';
        this.nameItemAttributeValueGrid = 'ItemAttributeValueGrid';
        this.nameItemQcGrid = 'ItemQcGrid';
        this.ActiveView = 'ALL';
    }
    RwAsset.prototype.getModuleScreen = function () {
        var self = this;
        var screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    RwAsset.prototype.openBrowse = function () {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;
        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwAppData.apiMethod(true, 'GET', "api/v1/inventorystatus", null, FwServices.defaultTimeout, function onSuccess(response) {
            for (var i = 0; i < response.length; i++) {
                FwBrowse.addLegend($browse, response[i].InventoryStatus, response[i].Color);
            }
        }, null, $browse);
        return $browse;
    };
    RwAsset.prototype.addBrowseMenuItems = function ($menuObject) {
        var self = this;
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        var $all = FwMenu.generateDropDownViewBtn('ALL Warehouses', false);
        var $userWarehouse = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true);
        var view = [];
        view[0] = 'WarehouseId=' + warehouse.warehouseid;
        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'WarehouseId=ALL';
            view[0] = self.ActiveView;
            if (view.length > 1) {
                self.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $userWarehouse.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;
            view[0] = self.ActiveView;
            if (view.length > 1) {
                self.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        var viewSubitems = [];
        viewSubitems.push($userWarehouse);
        viewSubitems.push($all);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);
        var $trackAll = FwMenu.generateDropDownViewBtn('ALL', true);
        var $trackBarcode = FwMenu.generateDropDownViewBtn('Barcode', false);
        var $trackSerialNumber = FwMenu.generateDropDownViewBtn('Serial Number', false);
        var $trackRFID = FwMenu.generateDropDownViewBtn('RFID', false);
        $trackAll.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'TrackedBy=ALL';
            view[1] = self.ActiveView;
            if (view.length > 1) {
                self.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $trackBarcode.on('click', function () {
            var $browse, barcode, sortByBarcode;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'TrackedBy=BARCODE';
            view[1] = self.ActiveView;
            if (view.length > 1) {
                self.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $trackSerialNumber.on('click', function () {
            var $browse, serialNumber, sortBySerial;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'TrackedBy=SERIALNO';
            view[1] = self.ActiveView;
            if (view.length > 1) {
                self.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        $trackRFID.on('click', function () {
            var $browse, rfid, sortByRFID;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'TrackedBy=RFID';
            view[1] = self.ActiveView;
            if (view.length > 1) {
                self.ActiveView = view.join(', ');
            }
            FwBrowse.search($browse);
        });
        var viewTrack = [];
        viewTrack.push($trackAll);
        viewTrack.push($trackBarcode);
        viewTrack.push($trackSerialNumber);
        viewTrack.push($trackRFID);
        var $trackByView;
        $trackByView = FwMenu.addViewBtn($menuObject, 'Tracked By', viewTrack);
        return $menuObject;
    };
    ;
    RwAsset.prototype.openForm = function (mode) {
        var $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    RwAsset.prototype.loadForm = function (uniqueids) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ItemId', uniqueids.ItemId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    RwAsset.prototype.saveForm = function ($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    RwAsset.prototype.loadAudit = function ($form) {
        var uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
        FwModule.loadAudit($form, uniqueid);
    };
    RwAsset.prototype.renderGrids = function ($form) {
        var $itemAttributeValueGrid = $form.find('div[data-grid="' + this.nameItemAttributeValueGrid + '"]');
        var $itemAttributeValueGridControl = FwBrowse.loadGridFromTemplate(this.nameItemAttributeValueGrid);
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
        var $itemQcGrid = $form.find('div[data-grid="' + this.nameItemQcGrid + '"]');
        var $itemQcGridControl = jQuery(jQuery('#tmpl-grids-' + this.nameItemQcGrid + 'Browse').html());
        $itemQcGrid.empty().append($itemQcGridControl);
        $itemQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: FwFormField.getValueByDataField($form, 'ItemId')
            };
        });
        FwBrowse.init($itemQcGridControl);
        FwBrowse.renderRuntimeHtml($itemQcGridControl);
    };
    RwAsset.prototype.afterLoad = function ($form) {
        var $itemAttributeValueGrid = $form.find('[data-name="' + this.nameItemAttributeValueGrid + '"]');
        FwBrowse.search($itemAttributeValueGrid);
        var $itemQcGrid = $form.find('[data-name="' + this.nameItemQcGrid + '"]');
        FwBrowse.search($itemQcGrid);
        var status = FwFormField.getValueByDataField($form, 'InventoryStatus');
        if (status === "IN") {
            FwFormField.enable($form.find('.ifin'));
        }
    };
    return RwAsset;
}());
var AssetController = new RwAsset();
//# sourceMappingURL=Asset.js.map