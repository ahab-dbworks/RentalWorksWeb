routes.push({ pattern: /^module\/transferreceipt$/, action: function (match: RegExpExecArray) { return TransferReceiptController.getModuleScreen(); } });

class TransferReceipt extends Contract {
    Module: string = 'TransferReceipt';
    apiurl: string = 'api/v1/manifest';
    caption: string = Constants.Modules.Home.TransferReceipt.caption;
	nav: string = Constants.Modules.Home.TransferReceipt.nav;
	id: string = Constants.Modules.Home.TransferReceipt.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
      <div data-name="TransferReceipt" data-control="FwBrowse" data-type="Browse" id="ManifestBrowse" class="fwcontrol fwbrowse" data-controller="TransferReceiptController" data-hasinactive="false">
        <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="true" data-datafield="ManifestId" data-datatype="key"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Receipt No." data-datafield="ManifestNumber" data-datatype="text" data-sort="off" data-searchfieldoperators="startswith"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Date" data-datafield="ManifestDate" data-datatype="date" data-sortsequence="1" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Time" data-datafield="ManifestTime" data-datatype="text" data-sortsequence="2" data-sort="desc"></div>
        </div>
        <div class="column" data-width="auto" data-visible="true">
          <div class="field" data-caption="Received By" data-datafield="NameFirstMiddleLast" data-datatype="text" data-sort="off"></div>
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
      <div id="transferreceiptform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Transfer Receipt" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="TransferReceiptController">
        <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="ManifestId"></div>
        <div id="transferreceiptform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
          <div class="tabs">
            <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
            <div data-type="tab" id="rentaltab" class="tab" data-tabpageid="rentaltabpage" data-caption="Rental Detail"></div>
            <div data-type="tab" id="salestab" class="tab" data-tabpageid="salestabpage" data-caption="Sales Detail"></div>
          </div>
          <div class="tabpages">
            <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Transfer Receipt">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Receipt No." data-datafield="ManifestNumber" style="flex:1 1 0" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="ManifestDate" style="flex:1 1 0" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Time" data-datafield="ManifestTime" style="flex:1 1 0" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="OfficeLocationValidation" data-displayfield="Location" class="fwcontrol fwformfield" data-caption="Office" data-datafield="LocationId" style="flex:1 1 50px" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="WarehouseValidation" data-displayfield="Warehouse" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" style="flex:1 1 50px" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="ContractType" style="flex:1 1 0" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="DepartmentValidation" data-displayfield="Department" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" style="flex:1 1 50px" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="Sales" style="float:left;width:250px; display:none"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="Rental" style="float:left;width:250px; display:none"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Exchange" data-datafield="Exchange" style="float:left;width:250px; display:none"></div>
                    <div class="print fwformcontrol" data-type="button" style="max-width:45px;margin:15px 0 0 10px;">Print</div>
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
FwApplicationTree.clickEvents[Constants.Modules.Home.TransferReceipt.form.menuItems.Print.id] = function (e) {
    var $form;
    try {
        $form = jQuery(this).closest('.fwform');
        $form.find('.print').trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------

var TransferReceiptController = new TransferReceipt();