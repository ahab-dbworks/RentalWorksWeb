routes.push({ pattern: /^module\/asset$/, action: function (match: RegExpExecArray) { return AssetController.getModuleScreen(); } });

class Asset {
    Module: string = 'Asset';
    apiurl: string = 'api/v1/item';
    caption: string = 'Asset';
    nav: string = 'module/asset';
    id: string = 'E1366299-0008-429C-93CC-B8ED8969B180';
    nameItemAttributeValueGrid: string = 'ItemAttributeValueGrid';
    nameItemQcGrid: string = 'ItemQcGrid';
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

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
        //let $browse = jQuery(this.getBrowseTemplate());
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
    addBrowseMenuItems($menuObject: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);

        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($userWarehouse, $all);
        FwMenu.addViewBtn($menuObject, 'Warehouse', viewSubitems, true, "WarehouseId");

        //Tracked By Filter
        const $trackAll = FwMenu.generateDropDownViewBtn('ALL', true, "ALL");
        const $trackBarcode = FwMenu.generateDropDownViewBtn('Bar Code', false, "BARCODE");
        const $trackSerialNumber = FwMenu.generateDropDownViewBtn('Serial Number', false, "SERIALNO");
        const $trackRFID = FwMenu.generateDropDownViewBtn('RFID', false, "RFID");

        let viewTrack: Array<JQuery> = [];
        viewTrack.push($trackAll, $trackBarcode, $trackSerialNumber, $trackRFID);
        FwMenu.addViewBtn($menuObject, 'Tracked By', viewTrack, true, "TrackedBy");
        return $menuObject;
    };
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        //let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        
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
        var $itemAttributeValueGrid: JQuery = $form.find('div[data-grid="' + this.nameItemAttributeValueGrid + '"]');
        var $itemAttributeValueGridControl: JQuery = jQuery(jQuery('#tmpl-grids-' + this.nameItemAttributeValueGrid + 'Browse').html());
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
        // ----------
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
        // ----------
    };
    //---------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        //const availFor = FwFormField.getValueByDataField($form, 'AvailFor');
        //switch (availFor) {
        //    case 'S':
        //        $form.find('[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
        //        break;
        //    case 'P':
        //        $form.find('[data-datafield="InventoryId"]').attr('data-validationname', 'PartsInventoryValidation');
        //        break;
        //    case 'R':
        //    default:
        //        break;
        //}

        var $itemAttributeValueGrid: JQuery = $form.find('[data-name="' + this.nameItemAttributeValueGrid + '"]');
        FwBrowse.search($itemAttributeValueGrid);

        var $itemQcGrid: JQuery = $form.find('[data-name="' + this.nameItemQcGrid + '"]');
        FwBrowse.search($itemQcGrid);

        var status: string = FwFormField.getValueByDataField($form, 'InventoryStatus');
        if (status === "IN") {
            FwFormField.enable($form.find('.ifin'));
        }

        //let isContainer = FwFormField.getValueByDataField($form, 'IsContainer');
        //if (isContainer == 'true') {
        //    $form.find('.containertab').show();
        //}
    };
    //---------------------------------------------------------------------------------------------
}

var AssetController = new Asset();
