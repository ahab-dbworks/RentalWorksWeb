class TiwContract extends Contract {

  constructor() {
    super();
    this.id = 'F6D42CC1-FAC6-49A9-9BF2-F370FE408F7B';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  //##################################################
  getBrowseTemplate(): string {
    return `
    <div data-name="Contract" data-control="FwBrowse" data-type="Browse" id="ContractBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="TiwContractController" data-hasinactive="false">
        <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="true" data-datafield="ContractId" data-browsedatatype="key"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Contract No." data-datafield="ContractNumber" data-browsedatatype="text" data-sort="off" data-searchfieldoperators="startswith"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Type" data-datafield="ContractType" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Date" data-datafield="ContractDate" data-browsedatatype="date" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Time" data-datafield="ContractTime" data-browsedatatype="text" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Department" data-datafield="Department" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Vendor" data-datafield="Vendor" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Order Description" data-datafield="OrderDescription" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="PO No." data-datafield="PurchaseOrderNumber" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Billing Start/Stop" data-datafield="BillingDate" data-browsedatatype="date" data-sort="off"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Warehouse" data-datafield="Warehouse" data-browsedatatype="text" data-sort="off"></div>
        </div>
         <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Location" data-datafield="Location" data-browsedatatype="text" data-sort="off"></div>
        </div>
        <div class="column spacer" data-width="auto" data-visible="true"></div>
      </div>`;
  }
  //##################################################
  getFormTemplate(): string {
    return `
      <div id="contractform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Contract" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="TiwContractController">
        <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="ContractId"></div>
        <div id="contractform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
          <div class="tabs">
            <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="Summary"></div>
            <div data-type="tab" id="rentaltab" class="tab" data-tabpageid="rentaltabpage" data-caption="Detail"></div>
          </div>
          <div class="tabpages">
            <!-- ##### CONTRACT tab ##### -->
            <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contract">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contract Number" data-datafield="ContractNumber" style="flex:1 1 125px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="ContractDate" style="flex:1 1 125px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Time" data-datafield="ContractTime" style="flex:1 1 75px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="ContractType" style="flex:1 1 125px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="Sales" style="flex:1 1 125px;display:none;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="Rental" style="flex:1 1 125px;display:none;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Exchange" data-datafield="Exchange" style="flex:1 1 125px;display:none;"></div>
                      <div class="print fwformcontrol" data-type="button" style="flex:0 1 50px;margin:15px 0 0 10px;">Print</div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="OfficeLocationValidation" data-displayfield="Location" class="fwcontrol fwformfield" data-caption="Office" data-datafield="LocationId" style="flex:1 1 125px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="DepartmentValidation" data-displayfield="Department" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" style="flex:1 1 125px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="WarehouseValidation" data-displayfield="Warehouse" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" style="flex:1 1 125px;" data-enabled="false"></div>
                    </div>
                  </div>
                </div>
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Summary">
                    <div class="wideflexrow summary" style="border:1px solid black;min-height:250px;margin:10px;">
                      <div data-control="FwGrid" data-grid="ContractSummaryGrid" data-securitycaption="Contract Summary" style="border:1px solid black;min-height:250px;margin:10px;"></div>
                    </div>
                    <div class="flexrow exchange" style="border:1px solid black;min-height:250px;margin:10px;">
                      <div data-control="FwGrid" data-grid="ContractExchangeItemGrid" data-securitycaption="Contract Exchange Item" style="border:1px solid black;min-height:250px;margin:10px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- ##### DETAIL tab ##### -->
            <div data-type="tabpage" id="rentaltabpage" class="tabpage" data-tabid="rentaltab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                    <div class="flexrow rentaldetailgrid" style="border:1px solid black;min-height:250px;margin:10px;">
                      <div data-control="FwGrid" data-grid="ContractDetailGrid" data-securitycaption="Rental Detail" style="border:1px solid black;min-height:250px;margin:10px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>`;
  }
  //##################################################
}
var TiwContractController = new TiwContract();
