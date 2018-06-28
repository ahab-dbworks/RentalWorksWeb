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
        var $browse = jQuery(this.getBrowseTemplate());
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
        $view = FwMenu.addViewBtn($menuObject, 'Warehouse', viewSubitems);
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
        var $form = jQuery(this.getFormTemplate());
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
    RwAsset.prototype.getBrowseTemplate = function () {
        return "\n        <div data-name=\"Asset\" data-control=\"FwBrowse\" data-type=\"Browse\" id=\"AssetBrowse\" class=\"fwcontrol fwbrowse\" data-orderby=\"StatusDate\" data-controller=\"AssetController\" data-hasinactive=\"true\">\n            <div class=\"column flexcolumn\" data-width=\"0\" data-visible=\"false\">\n                <div class=\"field\" data-isuniqueid=\"true\" data-datafield=\"ItemId\" data-browsedatatype=\"key\" ></div>\n            </div>\n            <div class=\"column flexcolumn\" max-width=\"250px\" data-visible=\"true\">\n                <div class=\"field\" data-caption=\"Barcode No.\" data-datafield=\"BarCode\" data-browsedatatype=\"text\" data-sort=\"asc\"></div>\n            </div>\n            <div class=\"column flexcolumn\" max-width=\"250px\" data-visible=\"true\">\n                <div class=\"field\" data-caption=\"Serial No.\" data-datafield=\"SerialNumber\" data-browsedatatype=\"text\" data-sort=\"off\"></div>\n            </div>\n            <div class=\"column flexcolumn\" max-width=\"150px\" data-visible=\"true\">\n                <div class=\"field\" data-caption=\"I-Code\" data-datafield=\"ICode\" data-browsedatatype=\"text\" data-sort=\"off\"></div>\n            </div>\n            <div class=\"column flexcolumn\" max-width=\"450px\" data-visible=\"true\">\n                <div class=\"field\" data-caption=\"Description\" data-datafield=\"Description\" data-browsedatatype=\"text\" data-sort=\"off\"></div>\n            </div>\n            <div class=\"column flexcolumn\" max-width=\"125px\" data-visible=\"true\">\n                <div class=\"field\" data-caption=\"Status\" data-datafield=\"InventoryStatus\" data-cellcolor=\"Color\" data-browsedatatype=\"text\" data-sort=\"off\"></div>\n            </div>\n            <div class=\"column flexcolumn\" max-width=\"100px\" data-visible=\"true\">\n                <div class=\"field\" data-caption=\"As Of\" data-datafield=\"StatusDate\" data-browsedatatype=\"text\" data-sort=\"off\"></div>\n            </div>\n            <div class=\"column flexcolumn\" max-width=\"250px\" data-visible=\"true\">\n                <div class=\"field\" data-caption=\"Location\" data-datafield=\"CurrentLocation\" data-browsedatatype=\"text\" data-sort=\"off\"></div>\n            </div>\n            <div class=\"column flexcolumn\" data-width=\"0\" data-visible=\"false\">\n                <div class=\"field\" data-datafield=\"Inactive\" data-browsedatatype=\"text\"  data-visible=\"false\"></div>\n            </div>\n            <div class=\"column spacer\" data-width=\"auto\" data-visible=\"true\"></div>\n        </div>";
    };
    RwAsset.prototype.getFormTemplate = function () {
        return "\n        <div id=\"assetform\" class=\"fwcontrol fwcontainer fwform\" data-control=\"FwContainer\" data-type=\"form\" data-version=\"1\" data-caption=\"Asset\" data-rendermode=\"template\" data-mode=\"\" data-hasaudit=\"false\" data-controller=\"AssetController\">\n          <div data-control=\"FwFormField\" data-type=\"key\" class=\"fwcontrol fwformfield\" data-isuniqueid=\"true\" data-saveorder=\"1\" data-caption=\"\" data-datafield=\"ItemId\"></div>\n          <div id=\"assetform-tabcontrol\" class=\"fwcontrol fwtabs\" data-control=\"FwTabs\" data-type=\"\">\n            <div class=\"tabs\">\n              <div data-type=\"tab\" id=\"assettab\" class=\"tab\" data-tabpageid=\"assettabpage\" data-caption=\"General\"></div>\n              <div data-type=\"tab\" id=\"locationtab\" class=\"tab\" data-tabpageid=\"locationtabpage\" data-caption=\"Location\"></div>\n              <div data-type=\"tab\" id=\"manufacturertab\" class=\"tab\" data-tabpageid=\"manufacturertabpage\" data-caption=\"Manufacturer\"></div>\n              <div data-type=\"tab\" id=\"attributetab\" class=\"tab\" data-tabpageid=\"attributetabpage\" data-caption=\"Attribute\"></div>\n              <div data-type=\"tab\" id=\"qctab\" class=\"tab\" data-tabpageid=\"qctabpage\" data-caption=\"Quality Control\"></div>\n              <div data-type=\"tab\" id=\"notestab\" class=\"tab\" data-tabpageid=\"notestabpage\" data-caption=\"Notes\"></div>\n            </div>\n            <div class=\"tabpages\">\n\n              <!-- General tab -->\n              <div data-type=\"tabpage\" id=\"assettabpage\" class=\"tabpage\" data-tabid=\"assettab\">\n                <div class=\"formpage\">\n                  <div class=\"formrow\">\n                    <div class=\"formcolumn\" style=\"width:675px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Asset\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"I-Code\" data-datafield=\"ICode\" data-enabled=\"false\" style=\"float:left;width:125px;\"></div>\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Description\" data-datafield=\"Description\" data-enabled=\"false\" style=\"float:left;width:500px;\"></div>\n                        </div>\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield ifin\" data-caption=\"Bar Code No.\" data-datafield=\"BarCode\" data-enabled=\"false\" style=\"float:left;width:200px;\"></div>\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield ifin\" data-caption=\"Serial No.\" data-datafield=\"SerialNumber\" data-enabled=\"false\" style=\"float:left;width:200px;\"></div>\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield ifin\" data-caption=\"RFID\" data-datafield=\"RfId\" data-enabled=\"false\" style=\"float:left;width:200px;\"></div>\n                        </div>\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Warehouse\" data-datafield=\"Warehouse\" data-enabled=\"false\" style=\"float:left;width:250px;\"></div>\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Ownership\" data-datafield=\"Ownership\" data-enabled=\"false\" style=\"float:left;width:250px;\"></div>\n                        </div>\n                      </div>\n                    </div>\n                    <div class=\"formcolumn\" style=\"width:225px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Status\" style=\"padding-left:1px;\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Status\" data-datafield=\"InventoryStatus\" data-enabled=\"false\" style=\"width:175px;\"></div>\n                        </div>\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Status Date\" data-datafield=\"StatusDate\" data-enabled=\"false\" style=\"float:left;width:175px;\"></div>\n                        </div>\n                      </div>\n                    </div>\n                  </div>\n                  <!--<div class=\"formrow\">\n                      <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Image\" style=\"width:875px;\">\n                          <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                              <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Description\" data-datafield=\"\" style=\"float:left;width:700px;\"></div>\n                              <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Number\" data-datafield=\"\" style=\"float:left;width:125px;\"></div>\n                          </div>\n                          <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                              <div class=\"fwcontrol fwappimage\" data-control=\"FwAppImage\" data-type=\"\" data-uniqueid1field=\"inventory.itemid\" data-description=\"\" data-rectype=\"F\" style=\"min-height:450px;\"></div>\n                          </div>\n                      </div>\n                  </div>-->\n                </div>\n              </div>\n\n              <!-- Location tab -->\n              <div data-type=\"tabpage\" id=\"locationtabpage\" class=\"tabpage\" data-tabid=\"locationtab\">\n                <div class=\"formpage\">\n                  <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Current Customer / Deal / Order\" style=\"width:825px;\">\n                    <div class=\"formrow\">\n                      <div class=\"formcolumn\" style=\"width:400px;\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Customer\" data-datafield=\"CurrentCustomer\" data-enabled=\"false\" style=\"width:375px;\"></div>\n                        </div>\n                      </div>\n                      <div class=\"formcolumn\" style=\"width:400px;\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Deal\" data-datafield=\"CurrentDeal\" data-enabled=\"false\" style=\"width:375px;\"></div>\n\n                        </div>\n                      </div>\n                    </div>\n                    <div class=\"formrow\">\n                      <div class=\"formcolumn\" style=\"width:400px;\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Order No.\" data-datafield=\"CurrentOrderNumber\" data-enabled=\"false\" style=\"float:left;width:150px;\"></div>\n                          <div data-control=\"FwFormField\" data-type=\"date\" class=\"fwcontrol fwformfield\" data-caption=\"Order Date\" data-datafield=\"CurrentOrderDate\" data-enabled=\"false\" style=\"float:left;width:125px;\"></div>\n                        </div>\n                      </div>\n                    </div>\n                    <div class=\"formrow\">\n                      <div class=\"formcolumn\" style=\"width:600px\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\" style=\"margin-top:10px;\">\n                          <div data-control=\"FwFormField\" data-type=\"date\" class=\"fwcontrol fwformfield\" data-caption=\"Pick Date / Time\" data-datafield=\"CurrentOrderPickDate\" data-enabled=\"false\" style=\"float:left;width:150px;\"></div>\n                          <div data-control=\"FwFormField\" data-type=\"date\" class=\"fwcontrol fwformfield\" data-caption=\"Est. Start Date / Time\" data-datafield=\"CurrentOrderFromDate\" data-enabled=\"false\" style=\"float:left;width:150px;\"></div>\n                          <div data-control=\"FwFormField\" data-type=\"date\" class=\"fwcontrol fwformfield\" data-caption=\"Est. Stop Date / Time\" data-datafield=\"CurrentOrderToDate\" data-enabled=\"false\" style=\"float:left;width:150px;\"></div>\n\n                        </div>\n                      </div>\n\n                    </div>\n                  </div>\n                  <div class=\"formrow\">\n                    <div class=\"formcolumn\" style=\"width:400px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Location / Condition\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Aisle\" data-datafield=\"AisleLocation\" style=\"float:left;width:75px;\"></div>\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Shelf\" data-datafield=\"ShelfLocation\" style=\"float:left;width:75px;\"></div>\n                          <div data-control=\"FwFormField\" data-type=\"validation\" class=\"fwcontrol fwformfield\" data-caption=\"Condition\" data-datafield=\"ConditionId\" data-displayfield=\"Condition\" data-validationname=\"InventoryConditionValidation\" style=\"float:left;width:225px;\"></div>\n                        </div>\n                      </div>\n                    </div>\n                    <div class=\"formcolumn\" style=\"width:124px;padding-left:1px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Usage\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Hours\" data-datafield=\"AssetHours\" style=\"float:left;width:75px;\"></div>\n                        </div>\n                      </div>\n                    </div>\n                  </div>\n                  <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Inspection\" style=\"width:525px;\">\n                    <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                      <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Inspection No.\" data-datafield=\"InspectionNo\" style=\"float:left;width:125px;\"></div>\n                      <div data-control=\"FwFormField\" data-type=\"validation\" class=\"fwcontrol fwformfield\" data-caption=\"Vendor\" data-datafield=\"InspectionVendorId\" data-displayfield=\"InspectionVendor\" data-validationname=\"VendorValidation\" style=\"float:left;width:375px;\"></div>\n\n                    </div>\n                  </div>\n                  <div class=\"formrow\">\n                    <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Last Physical Inventory\" style=\"width:525px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                        <div data-control=\"FwFormField\" data-type=\"date\" class=\"fwcontrol fwformfield\" data-caption=\"Date\" data-datafield=\"PhysicalDate\" data-enabled=\"false\" style=\"float:left;width:125px;\"></div>\n                        <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"By\" data-datafield=\"PhysicalBy\" data-enabled=\"false\" style=\"float:left;width:375px;\"></div>\n                      </div>\n                    </div>\n                  </div>\n                </div>\n              </div>\n\n\n              <!-- Manufacturer tab -->\n              <div data-type=\"tabpage\" id=\"manufacturertabpage\" class=\"tabpage\" data-tabid=\"manufacturertab\">\n                <div class=\"formpage\">\n                  <div class=\"formrow\">\n                    <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Manufacturer\" style=\"width:525px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                        <div data-control=\"FwFormField\" data-type=\"validation\" class=\"fwcontrol fwformfield\" data-caption=\"Name\" data-datafield=\"ManufacturerId\" data-displayfield=\"Manufacturer\" data-validationname=\"VendorValidation\" style=\"float:left;width:375px;\"></div>\n                        <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Warranty Period\" data-datafield=\"WarrantyPeriod\" style=\"float:left;width:100px;\"></div>\n                        <p style=\"float:left;margin-top:22px;margin-bottom:-22px;font-size:10pt;\">Days</p>\n                      </div>\n                      <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                        <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Model\" data-datafield=\"ManufacturerModelNumber\" style=\"float:left;width:150px;\"></div>\n                        <div data-control=\"FwFormField\" data-type=\"validation\" class=\"fwcontrol fwformfield\" data-caption=\"Country of Origin\" data-datafield=\"CountryOfOriginId\" data-displayfield=\"CountryOfOrigin\" data-validationname=\"CountryValidation\" style=\"float:left;width:225px;\"></div>\n                        <div data-control=\"FwFormField\" data-type=\"date\" class=\"fwcontrol fwformfield\" data-caption=\"Warranty Expiration\" data-datafield=\"WarrantyExpiration\" style=\"float:left;width:125px;\"></div>\n                      </div>\n                      <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                        <div data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield\" data-caption=\"Part No.\" data-datafield=\"ManufacturerPartNumber\" style=\"float:left;width:375px;\"></div>\n                        <div data-control=\"FwFormField\" data-type=\"date\" class=\"fwcontrol fwformfield\" data-caption=\"Mfg. Date\" data-datafield=\"ManufactureDate\" style=\"float:left;width:125px;\"></div>\n                      </div>\n                    </div>\n                  </div>\n                  <div class=\"formrow\">\n                    <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Item Description\" style=\"width:525px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                        <div data-control=\"FwFormField\" data-type=\"textarea\" class=\"fwcontrol fwformfield\" data-caption=\"\" data-datafield=\"ItemDescription\" style=\"float:left;width:500px;margin-top:-15px;\"></div>\n                      </div>\n                    </div>\n                  </div>\n                  <div class=\"formrow\">\n                    <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Shelf Life\" style=\"width:150px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                        <div data-control=\"FwFormField\" data-type=\"date\" class=\"fwcontrol fwformfield\" data-caption=\"Expiration Date\" data-datafield=\"ShelfLifeExpirationDate\" style=\"float:left;width:125px;\"></div>\n                      </div>\n                    </div>\n                  </div>\n                </div>\n              </div>\n\n              <!-- Attribute tab -->\n              <div data-type=\"tabpage\" id=\"attributetabpage\" class=\"tabpage\" data-tabid=\"attributetab\">\n                <div class=\"formpage\">\n                  <div class=\"formrow\">\n                    <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Attributes\" style=\"width:500px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                        <div data-control=\"FwGrid\" data-grid=\"ItemAttributeValueGrid\" data-securitycaption=\"Item Attribute Value\" style=\"min-width:250px;max-width:500px;\"></div>\n                      </div>\n                    </div>\n                  </div>\n                </div>\n              </div>\n\n              <!-- QC tab -->\n              <div data-type=\"tabpage\" id=\"qctabpage\" class=\"tabpage\" data-tabid=\"qctab\">\n                <div class=\"formpage\">\n                  <div class=\"formrow\" style=\"width:100%;\">\n                    <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Quality Control\" style=\"width:1300px;\">\n                      <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                        <div data-control=\"FwGrid\" data-grid=\"ItemQcGrid\" data-securitycaption=\"Item QC\"></div>\n                      </div>\n                    </div>\n                  </div>\n                </div>\n              </div>\n\n              <!-- Notes tab -->\n              <div data-type=\"tabpage\" id=\"notestabpage\" class=\"tabpage\" data-tabid=\"notestab\">\n                <div class=\"flexpage\">\n                  <div class=\"flexrow\">\n                    <div class=\"flexcolumn\">\n                      <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Notes\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwFormField\" data-type=\"textarea\" class=\"fwcontrol fwformfield\" data-caption=\"\" data-datafield=\"ItemNotes\"></div>\n                        </div>\n                      </div>\n                    </div>\n                    <div class=\"flexcolumn\">\n                      <div class=\"fwcontrol fwcontainer fwform-section\" data-control=\"FwContainer\" data-type=\"section\" data-caption=\"Photo\">\n                        <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                          <div data-control=\"FwAppImage\" data-type=\"\" class=\"fwcontrol fwappimage contactphoto\" data-caption=\"Photo\" data-uniqueid1field=\"ItemId\" data-description=\"\" data-rectype=\"F\"></div>\n                        </div>\n                      </div>\n                    </div>\n                  </div>\n                </div>\n              </div>\n            </div>\n          </div>\n        </div>";
    };
    return RwAsset;
}());
var AssetController = new RwAsset();
//# sourceMappingURL=Asset.js.map