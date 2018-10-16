class TiwAsset extends RwAsset {
    //browseModel: any = {};

    //getBrowseTemplate(): void {
    //    //let template = super.getBrowseTemplate();
    //    //return template;
    //}

    getFormTemplate(): string {
        return `
        <div id="assetform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Asset" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="AssetController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="ItemId"></div>
          <div id="assetform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="assettab" class="tab" data-tabpageid="assettabpage" data-caption="General"></div>
              <div data-type="tab" id="locationtab" class="tab" data-tabpageid="locationtabpage" data-caption="Location"></div>
              <div data-type="tab" id="manufacturertab" class="tab" data-tabpageid="manufacturertabpage" data-caption="Manufacturer"></div>
              <div data-type="tab" id="attributetab" class="tab" data-tabpageid="attributetabpage" data-caption="Attribute"></div>
              <div data-type="tab" id="qctab" class="tab" data-tabpageid="qctabpage" data-caption="Quality Control"></div>
            </div>
            <div class="tabpages">

              <!-- General tab -->
              <div data-type="tabpage" id="assettabpage" class="tabpage" data-tabid="assettab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="formcolumn" style="width:675px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Asset">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" data-enabled="false" style="float:left;width:125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="float:left;width:500px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ifin" data-caption="Bar Code No." data-datafield="BarCode" data-enabled="false" style="float:left;width:200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ifin" data-caption="Serial No." data-datafield="SerialNumber" data-enabled="false" style="float:left;width:200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ifin" data-caption="RFID" data-datafield="RfId" data-enabled="false" style="float:left;width:200px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-enabled="false" style="float:left;width:250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ownership" data-datafield="Ownership" data-enabled="false" style="float:left;width:250px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:225px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status" style="padding-left:1px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="InventoryStatus" data-enabled="false" style="width:175px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusDate" data-enabled="false" style="float:left;width:175px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!--<div class="formrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Image" style="width:875px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="" style="float:left;width:700px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Number" data-datafield="" style="float:left;width:125px;"></div>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                              <div class="fwcontrol fwappimage" data-control="FwAppImage" data-type="" data-uniqueid1field="inventory.itemid" data-description="" data-rectype="F" style="min-height:450px;"></div>
                          </div>
                      </div>
                  </div>-->
                </div>
              </div>

              <!-- Location tab -->
              <div data-type="tabpage" id="locationtabpage" class="tabpage" data-tabid="locationtab">
                <div class="formpage">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Current Customer / Deal / Order" style="width:825px;">
                    <div class="formrow">
                      <div class="formcolumn" style="width:400px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CurrentCustomer" data-enabled="false" style="width:375px;"></div>
                        </div>
                      </div>
                      <div class="formcolumn" style="width:400px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="CurrentDeal" data-enabled="false" style="width:375px;"></div>

                        </div>
                      </div>
                    </div>
                    <div class="formrow">
                      <div class="formcolumn" style="width:400px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="CurrentOrderNumber" data-enabled="false" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Order Date" data-datafield="CurrentOrderDate" data-enabled="false" style="float:left;width:125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formrow">
                      <div class="formcolumn" style="width:600px">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="margin-top:10px;">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Pick Date / Time" data-datafield="CurrentOrderPickDate" data-enabled="false" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Start Date / Time" data-datafield="CurrentOrderFromDate" data-enabled="false" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Stop Date / Time" data-datafield="CurrentOrderToDate" data-enabled="false" style="float:left;width:150px;"></div>

                        </div>
                      </div>

                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:400px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location / Condition">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Aisle" data-datafield="AisleLocation" style="float:left;width:75px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Shelf" data-datafield="ShelfLocation" style="float:left;width:75px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Condition" data-datafield="ConditionId" data-displayfield="Condition" data-validationname="InventoryConditionValidation" style="float:left;width:225px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:124px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Usage">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Hours" data-datafield="AssetHours" style="float:left;width:75px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inspection" style="width:525px;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Inspection No." data-datafield="InspectionNo" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="InspectionVendorId" data-displayfield="InspectionVendor" data-validationname="VendorValidation" style="float:left;width:375px;"></div>

                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Last Physical Inventory" style="width:525px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="PhysicalDate" data-enabled="false" style="float:left;width:125px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="PhysicalBy" data-enabled="false" style="float:left;width:375px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>


              <!-- Manufacturer tab -->
              <div data-type="tabpage" id="manufacturertabpage" class="tabpage" data-tabid="manufacturertab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Manufacturer" style="width:525px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Name" data-datafield="ManufacturerId" data-displayfield="Manufacturer" data-validationname="VendorValidation" style="float:left;width:375px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warranty Period" data-datafield="WarrantyPeriod" style="float:left;width:100px;"></div>
                        <p style="float:left;margin-top:22px;margin-bottom:-22px;font-size:10pt;">Days</p>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Model" data-datafield="ManufacturerModelNumber" style="float:left;width:150px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country of Origin" data-datafield="CountryOfOriginId" data-displayfield="CountryOfOrigin" data-validationname="CountryValidation" style="float:left;width:225px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Warranty Expiration" data-datafield="WarrantyExpiration" style="float:left;width:125px;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Part No." data-datafield="ManufacturerPartNumber" style="float:left;width:375px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Mfg. Date" data-datafield="ManufactureDate" style="float:left;width:125px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item Description" style="width:525px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="ItemDescription" style="float:left;width:500px;margin-top:-15px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shelf Life" style="width:150px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Expiration Date" data-datafield="ShelfLifeExpirationDate" style="float:left;width:125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- Attribute tab -->
              <div data-type="tabpage" id="attributetabpage" class="tabpage" data-tabid="attributetab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Attributes" style="width:500px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="ItemAttributeValueGrid" data-securitycaption="Item Attribute Value" style="min-width:250px;max-width:500px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- QC tab -->
              <div data-type="tabpage" id="qctabpage" class="tabpage" data-tabid="qctab">
                <div class="formpage">
                  <div class="formrow" style="width:100%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quality Control" style="width:1300px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
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

}

var TiwAssetController = new TiwAsset();