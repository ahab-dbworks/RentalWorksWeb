﻿class TiwPickList extends PickList {

  constructor() {
    super();
    this.id = '744B371E-5478-42F9-9852-E143A1EC5DDA';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getBrowseTemplate(): string {
    return `
    <div data-name="PickList" data-control="FwBrowse" data-type="Browse" id="PickListBrowse" class="fwcontrol fwbrowse" data-orderby="PickListId" data-controller="TiwPickListController" data-hasinactive="false">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="PickListId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-datafield="OrderId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="125px" data-visible="true">
            <div class="field" data-caption="Pick No." data-datafield="PickListNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Pick Date" data-datafield="PickDate" data-browsedatatype="text" data-sort="desc"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Pick Time" data-datafield="PickTime" data-browsedatatype="text" data-sort="desc"></div>
          </div>
          <div class="column" data-width="125px" data-visible="true">
            <div class="field" data-caption="Order No." data-datafield="OrderNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="250px" data-visible="true">
            <div class="field" data-caption="Order Description" data-datafield="OrderDescription" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="250px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="175px" data-visible="true">
            <div class="field" data-caption="Department" data-datafield="Department" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="175px" data-visible="true">
            <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="125px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="WarehouseCode" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="125px" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-browsedatatype="text" data-sort="off"></div>
          </div>
        </div>`;
  }

  getFormTemplate(): string {
    return `
    <div id="picklistform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Pick List" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="TiwPickListController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="PickListId"></div>
          <div id="picklistform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="Pick List"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 700px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Pick List No." data-datafield="PickListNumber" data-enabled="false" style="flex:1 1 125px;"></div>
                          <!--<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderNumber" data-enabled="false" style="flex:1 1 125px;"></div>-->
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="OrderDescription" data-enabled="false" style="flex:2 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="Deal" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div class="fwformcontrol printpicklist" data-type="button" style="flex:0 1 auto;margin-top:24px;max-height:26px;">Print</div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As Of" data-datafield="InputDate" data-enabled="false" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="InputTime" data-enabled="false" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Department" data-datafield="Department" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-enabled="false" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 150px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Due Date/Time">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="DueDate" style="flex:1 1 100px;"></div>
                        </div>                        
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Time" data-datafield="DueTime" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="wideflexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items">
                      <div data-control="FwGrid" data-grid="PickListItemGrid" data-securitycaption="Pick List Item" style="border:1px solid black;min-height:250px;margin:5px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
  }
}

var TiwPickListController = new TiwPickList();