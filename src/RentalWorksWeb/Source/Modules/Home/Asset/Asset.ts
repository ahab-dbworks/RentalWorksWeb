class RwAsset {
    Module: string;
    apiurl: string;
    caption: string;
    nameItemAttributeValueGrid: string;
    nameItemQcGrid: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Asset';
        this.apiurl = 'api/v1/item';
        this.caption = 'Asset';
        this.nameItemAttributeValueGrid = 'ItemAttributeValueGrid';
        this.nameItemQcGrid = 'ItemQcGrid';
        this.ActiveView = 'ALL';
    }

    getModuleScreen() {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var self = this;
        var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
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
    }

    addBrowseMenuItems($menuObject: any) {
        var self = this;
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        var $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false);
        var $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true);

        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'WarehouseId=ALL';
            FwBrowse.search($browse);
        });
        $userWarehouse.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;
            FwBrowse.search($browse);
        });
      

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($userWarehouse);
        viewSubitems.push($all);

        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        //Tracked By Filter
        var $trackAll = FwMenu.generateDropDownViewBtn('ALL', true);
        var $trackBarcode = FwMenu.generateDropDownViewBtn('Barcode', false);
        var $trackSerialNumber = FwMenu.generateDropDownViewBtn('Serial Number', false);
        var $trackRFID = FwMenu.generateDropDownViewBtn('RFID', false);

        $trackAll.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'TrackedBy=ALL';
            FwBrowse.search($browse);
        });
        $trackBarcode.on('click', function () {
            var $browse, barcode, sortByBarcode;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'TrackedBy=BARCODE';
            FwBrowse.search($browse);
        });
        $trackSerialNumber.on('click', function () {
            var $browse, serialNumber, sortBySerial;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'TrackedBy=SERIALNUMBER';
            FwBrowse.search($browse);
        });
        $trackRFID.on('click', function () {
            var $browse, rfid, sortByRFID;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'TrackedBy=RFID';
            FwBrowse.search($browse);
        });
        var viewTrack = [];
        viewTrack.push($trackAll);
        viewTrack.push($trackBarcode);
        viewTrack.push($trackSerialNumber);
        viewTrack.push($trackRFID);
        var $trackByView;
        $trackByView = FwMenu.addViewBtn($menuObject, 'Track By', viewTrack);
        return $menuObject;
    };

    openForm(mode: string) {
        var $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ItemId', uniqueids.ItemId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: JQuery, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath});
    }

    loadAudit($form: JQuery) {
        var uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: JQuery) {
        var $itemAttributeValueGrid : JQuery = $form.find('div[data-grid="' + this.nameItemAttributeValueGrid + '"]');
        var $itemAttributeValueGridControl: JQuery = FwBrowse.loadGridFromTemplate(this.nameItemAttributeValueGrid);
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

        var $itemQcGrid: JQuery = $form.find('div[data-grid="' + this.nameItemQcGrid + '"]');
        var $itemQcGridControl: JQuery = jQuery(jQuery('#tmpl-grids-' + this.nameItemQcGrid + 'Browse').html());
        $itemQcGrid.empty().append($itemQcGridControl);
        $itemQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: FwFormField.getValueByDataField($form, 'ItemId')
            };
        })
        FwBrowse.init($itemQcGridControl);
        FwBrowse.renderRuntimeHtml($itemQcGridControl);
    
    }

    afterLoad($form: JQuery) {
        var $itemAttributeValueGrid: JQuery = $form.find('[data-name="' + this.nameItemAttributeValueGrid + '"]');
        FwBrowse.search($itemAttributeValueGrid);

        var $itemQcGrid: JQuery = $form.find('[data-name="' + this.nameItemQcGrid + '"]');
        FwBrowse.search($itemQcGrid);

        var status: string = FwFormField.getValueByDataField($form, 'StatusType');
        if (status === "IN") {
            FwFormField.enable($form.find('.ifin'));
        }
    }
}

var AssetController = new RwAsset();