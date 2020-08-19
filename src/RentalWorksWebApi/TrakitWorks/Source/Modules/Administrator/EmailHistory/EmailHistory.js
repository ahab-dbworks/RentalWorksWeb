class EmailHistory {
    constructor() {
        this.Module = 'EmailHistory';
        this.apiurl = 'api/v1/emailhistory';
        this.caption = Constants.Modules.Administrator.children.EmailHistory.caption;
        this.nav = Constants.Modules.Administrator.children.EmailHistory.nav;
        this.id = Constants.Modules.Administrator.children.EmailHistory.id;
        this.ActiveViewFields = {};
    }
    addBrowseMenuItems(options) {
        options.hasNew = false;
        options.hasEdit = false;
        options.hasDelete = false;
        options.hasInactive = false;
        FwMenu.addBrowseMenuButtons(options);
        let userid = JSON.parse(sessionStorage.getItem('userid'));
        const $me = FwMenu.generateDropDownViewBtn('Me', true, userid.webusersid);
        const $all = FwMenu.generateDropDownViewBtn('All Users', false, "ALL");
        FwMenu.addVerticleSeparator(options.$menu);
        if (typeof this.ActiveViewFields["FromUserId"] == 'undefined') {
            this.ActiveViewFields.FromUserId = [userid.webusersid];
        }
        let sentBy = [];
        sentBy.push($me, $all);
        FwMenu.addViewBtn(options.$menu, 'Sent By', sentBy, true, "FromUserId");
    }
    addFormMenuItems(options) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    getModuleScreen() {
        const screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    }
    openBrowse() {
        let self = this;
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });
        return $browse;
    }
    openForm(mode) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    loadForm(uniqueids) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="EmailHistoryId"] input').val(uniqueids.EmailHistoryId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
    }
    beforeValidateWarehouse($browse, $form, request) {
        request.uniqueids = {};
        const locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
        if (locationId) {
            request.uniqueids.LocationId = locationId;
        }
    }
    getBrowseTemplate() {
        return `
        <div data-name="EmailHistory" data-control="FwBrowse" data-type="Browse" id="EmailHistoryBrowse" class="fwcontrol fwbrowse" data-orderby="Name" data-controller="EmailHistoryController">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="EmailHistoryId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="false" data-datafield="ReportId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="false" data-datafield="FromUserId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="200px" data-visible="true">
            <div class="field" data-caption="Sent" data-isuniqueid="false" data-datafield="EmailDate" data-browsedatatype="text" data-sort="asc"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="From" data-isuniqueid="false" data-datafield="FromUser" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="E-mail To" data-isuniqueid="false" data-datafield="EmailTo" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Subject" data-isuniqueid="false" data-datafield="EmailSubject" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Report" data-isuniqueid="false" data-datafield="Title" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Status" data-isuniqueid="false" data-datafield="Status" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>
        `;
    }
    getFormTemplate() {
        return `
        <div id="emailhistoryform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Email History" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="EmailHistoryController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="EmailHistoryId"></div>
          <div id="emailhistoryform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="emailhistorytab" class="tab" data-tabpageid="emailhistorytabpage" data-caption="Email History"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="emailhistorytabpage" class="tabpage" data-tabid="emailhistorytab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 825px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Email History">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email Date" data-datafield="EmailDate" style="flex:0 1 125px;" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" style="flex:0 1 125px;" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="From" data-datafield="FromUser" style="flex:1 1 125px;" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email To" data-datafield="EmailTo" style="flex:1 1 125px;" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email Subject" data-datafield="EmailSubject" style="flex:1 1 120px;" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Report" data-datafield="Title" style="flex:1 1 25px;" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Email Text" data-datafield="EmailText" style="flex:1 1 225px;" data-enabled="false"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        `;
    }
}
var EmailHistoryController = new EmailHistory();
//# sourceMappingURL=EmailHistory.js.map