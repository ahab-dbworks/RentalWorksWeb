routes.push({ pattern: /^module\/manifest$/, action: function (match: RegExpExecArray) { return ManifestController.getModuleScreen(); } });

class Manifest extends ContractBase {
    Module:             string = 'Manifest';
    apiurl:             string = 'api/v1/transfermanifest';
    caption:            string = Constants.Modules.Transfers.children.Manifest.caption;
    nav:                string = Constants.Modules.Transfers.children.Manifest.nav;
    id:                 string = Constants.Modules.Transfers.children.Manifest.id;
    ActiveViewFields:   any    = {};
    ActiveViewFieldsId: string;
    uniqueIdFieldName = "ManifestId";
    numberFieldName   = "ManifestNumber";
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasInactive = false;
        options.hasNew = false;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);
        super.afterAddBrowseMenuItems(options);
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Transfer Manifest', '', (e: JQuery.ClickEvent) => {
            try {
                this.printContract(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
      <div data-name="Manifest" data-control="FwBrowse" data-type="Browse" id="ManifestBrowse" class="fwcontrol fwbrowse" data-controller="ManifestController">
        <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="true" data-datafield="ManifestId" data-datatype="key"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Manifest No." data-datafield="ManifestNumber" data-datatype="text" data-sort="off" data-searchfieldoperators="startswith"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Date" data-datafield="ManifestDate" data-datatype="date" data-sortsequence="1" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Time" data-datafield="ManifestTime" data-datatype="text" data-sortsequence="2" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Transferred By" data-datafield="NameFirstMiddleLast" data-datatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Transfer No." data-datafield="TransferNumber" data-datatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Description" data-datafield="Description" data-datatype="text" data-sort="off"></div>
        </div>
         <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Type" data-datafield="ContractType" data-datatype="text" data-sort="off"></div>
        </div>
      </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
      <div id="manifestform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Manifest" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ManifestController">
        <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="ManifestId"></div>
        <div id="manifestform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
          <div class="tabs">
            <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
            <div data-type="tab" id="rentaltab" class="tab" data-tabpageid="rentaltabpage" data-caption="Rental Detail"></div>
            <div data-type="tab" id="salestab" class="tab" data-tabpageid="salestabpage" data-caption="Sales Detail"></div>
          </div>
          <div class="tabpages">
            <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Manifest">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Manifest No." data-datafield="ManifestNumber" style="flex:1 1 0" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="ManifestDate" style="flex:1 1 0" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Time" data-datafield="ManifestTime" style="flex:1 1 0" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="OfficeLocationValidation" data-displayfield="Location" class="fwcontrol fwformfield" data-caption="Office" data-datafield="LocationId" style="flex:1 1 50px" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="WarehouseValidation" data-displayfield="Warehouse" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" style="flex:1 1 50px" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="ContractType" style="flex:1 1 0" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="DepartmentValidation" data-displayfield="Department" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" style="flex:1 1 50px" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="Sales" style="float:left;width:250px; display:none"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="Rental" style="float:left;width:250px; display:none"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exchange" data-datafield="Exchange" style="float:left;width:250px; display:none"></div>
                  </div>
                </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Summary">
                  <div class="flexrow summary" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractSummaryGrid" data-securitycaption="Contract Summary"></div>
                  </div>
                  <div class="flexrow exchange" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractExchangeItemGrid" data-securitycaption="Contract Exchange Item"></div>
                  </div>
                </div>
              </div>
            </div>
            <div data-type="tabpage" id="rentaltabpage" class="tabpage" data-tabid="rentaltab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                  <div class="flexrow rentaldetailgrid" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractDetailGrid" data-securitycaption="Rental Detail"></div>
                  </div>
                </div>
              </div>
            </div>
            <div data-type="tabpage" id="salestabpage" class="tabpage" data-tabid="salestab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Items">
                  <div class="flexrow salesdetailgrid" style="max-width:1800px;">
                    <div data-control="FwGrid" data-grid="ContractDetailGrid" data-securitycaption="Sales Detail"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------

var ManifestController = new Manifest();