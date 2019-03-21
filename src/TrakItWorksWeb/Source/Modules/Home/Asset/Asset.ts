routes.push({ pattern: /^module\/asset/, action: function (match: RegExpExecArray) { return AssetController.getModuleScreen(); } });

class Asset {
    Module: string = 'Asset';
    apiurl: string = 'api/v1/item';
    caption: string = 'Asset';
    nav: string = 'module/item';
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
        // var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        let $browse = jQuery(this.getBrowseTemplate());
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
        // var $form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
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
        const availFor = FwFormField.getValueByDataField($form, 'AvailFor');
        switch (availFor) {
            case 'S':
                $form.find('[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
                break;
            case 'P':
                $form.find('[data-datafield="InventoryId"]').attr('data-validationname', 'PartsInventoryValidation');
                break;
            case 'R':
            default:
                break;
        }

        var $itemAttributeValueGrid: JQuery = $form.find('[data-name="' + this.nameItemAttributeValueGrid + '"]');
        FwBrowse.search($itemAttributeValueGrid);

        var $itemQcGrid: JQuery = $form.find('[data-name="' + this.nameItemQcGrid + '"]');
        FwBrowse.search($itemQcGrid);

        var status: string = FwFormField.getValueByDataField($form, 'InventoryStatus');
        if (status === "IN") {
            FwFormField.enable($form.find('.ifin'));
        }

        let isContainer = FwFormField.getValueByDataField($form, 'IsContainer');
        if (isContainer == 'true') {
            $form.find('.containertab').show();
        }
    };
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Asset" data-control="FwBrowse" data-type="Browse" id="AssetBrowse" class="fwcontrol fwbrowse" data-orderby="StatusDate" data-controller="AssetController" data-hasinactive="true">
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="ItemId" data-browsedatatype="key"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Barcode No." data-datafield="BarCode" data-browsedatatype="text" data-sort="asc"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Serial No." data-datafield="SerialNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="150px" data-visible="true">
            <div class="field" data-caption="I-Code" data-datafield="ICode" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="450px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="125px" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="InventoryStatus" data-cellcolor="Color" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="As Of" data-datafield="StatusDate" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Location" data-datafield="CurrentLocation" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-datafield="Inactive" data-browsedatatype="text" data-visible="false"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>`;
    };
    //---------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="assetform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Asset" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="AssetController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="ItemId"></div>
          <div id="assetform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="assettab" class="tab" data-tabpageid="assettabpage" data-caption="Asset"></div>
              <div data-type="tab" id="locationtab" class="tab" data-tabpageid="locationtabpage" data-caption="Location"></div>
              <div data-type="tab" id="manufacturertab" class="tab" data-tabpageid="manufacturertabpage" data-caption="Manufacturer"></div>
              <div data-type="tab" id="attributetab" class="tab" data-tabpageid="attributetabpage" data-caption="Attribute"></div>
              <div data-type="tab" id="qctab" class="tab" data-tabpageid="qctabpage" data-caption="Quality Control"></div>
            </div>
            <div class="tabpages">

              <!-- ##### ASSET tab ##### -->
              <div data-type="tabpage" id="assettabpage" class="tabpage" data-tabid="assettab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 700px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Asset">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" data-enabled="false" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:2 1 500px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ifin" data-caption="Bar Code No." data-datafield="BarCode" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ifin" data-caption="Serial No." data-datafield="SerialNumber" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ifin" data-caption="RFID" data-datafield="RfId" data-enabled="false" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-enabled="false" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ownership" data-datafield="Ownership" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 150px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="InventoryStatus" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusDate" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!--
                  <div class="flexrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Image" style="width:875px;">
                        <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="" style="float:left;width:700px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Number" data-datafield="" style="float:left;width:125px;"></div>
                          </div>
                        <div class="flexrow">
                              <div class="fwcontrol fwappimage" data-control="FwAppImage" data-type="" data-uniqueid1field="inventory.itemid" data-description="" data-rectype="F" style="min-height:450px;"></div>
                          </div>
                      </div>
                  </div>
                  -->
                </div>
              </div>

              <!-- ##### LOCATION tab ##### -->
              <div data-type="tabpage" id="locationtabpage" class="tabpage" data-tabid="locationtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Last Customer / Deal / Order">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CurrentCustomer" data-enabled="false" style="flex:1 1 375px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="CurrentDeal" data-enabled="false" style="flex:1 1 375px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="CurrentOrderNumber" data-enabled="false" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Order Date" data-datafield="CurrentOrderDate" data-enabled="false" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contract No." data-datafield="CurrentContractNumber" data-enabled="false" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Contract Date" data-datafield="CurrentContractDate" data-enabled="false" style="flex:1 1 125px;"></div>
                      </div>                  
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Pick Date / Time" data-datafield="CurrentOrderPickDate" data-enabled="false" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Start Date / Time" data-datafield="CurrentOrderFromDate" data-enabled="false" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Stop Date / Time" data-datafield="CurrentOrderToDate" data-enabled="false" style="flex:1 1 150px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 400px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location / Condition">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Aisle" data-datafield="AisleLocation" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Shelf" data-datafield="ShelfLocation" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Condition" data-datafield="ConditionId" data-displayfield="Condition" data-validationname="InventoryConditionValidation" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 125px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Usage">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Hours" data-datafield="AssetHours" style="flex:1 1 75px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- 
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inspection" style="flex:1 1 525px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Inspection No." data-datafield="InspectionNo" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="InspectionVendorId" data-displayfield="InspectionVendor" data-validationname="VendorValidation" style="flex:1 1 375px;"></div>
                    </div>
                  </div>
                  -->
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Last Physical Inventory" style="flex:1 1 525px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="PhysicalDate" data-enabled="false" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="PhysicalBy" data-enabled="false" style="flex:1 1 375px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- ##### MANUFACTURER tab ##### -->
              <div data-type="tabpage" id="manufacturertabpage" class="tabpage" data-tabid="manufacturertab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 500px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Manufacturer">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Name" data-datafield="ManufacturerId" data-displayfield="Manufacturer" data-validationname="VendorValidation" style="flex:1 1 375px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warranty Period" data-datafield="WarrantyPeriod" style="flex:1 1 100px;"></div>
                          <p style="float:left;margin-top:25px;margin-bottom:-25px;font-size:10pt;">Days</p>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Model" data-datafield="ManufacturerModelNumber" style="flex:0 1 150px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country of Origin" data-datafield="CountryOfOriginId" data-displayfield="CountryOfOrigin" data-validationname="CountryValidation" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Warranty Expiration" data-datafield="WarrantyExpiration" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Part No." data-datafield="ManufacturerPartNumber" style="flex:1 1 375px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Mfg. Date" data-datafield="ManufactureDate" style="flex:1 1 125px;"></div>
                        </div>
                      </div>  
                    </div>
                    <div class="flexcolumn" style="flex:1 1 325px;">
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item Description">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="ItemDescription" style="flex:1 1 500px;margin-top:-15px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shelf Life" style="flex:0 1 175px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Expiration Date" data-datafield="ShelfLifeExpirationDate" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- ##### ATTRIBUTE tab ##### -->
              <div data-type="tabpage" id="attributetabpage" class="tabpage" data-tabid="attributetab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Attributes">
                      <div class="flexrow">##### ADD ATTRIBUTES GRID HERE #####
                        <div data-control="FwGrid" data-grid="ItemAttributeValueGrid" data-securitycaption="Item Attribute Value"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- ##### QC tab ##### -->
              <div data-type="tabpage" id="qctabpage" class="tabpage" data-tabid="qctab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quality Control">
                      <div class="flexrow">##### ADD QC GRID HERE #####
                        <div data-control="FwGrid" data-grid="ItemQcGrid" data-securitycaption="Item QC"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
    //---------------------------------------------------------------------------------------------
}

var AssetController = new Asset();
