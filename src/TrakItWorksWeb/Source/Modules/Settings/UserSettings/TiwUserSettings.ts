class TiwUserSettings extends UserSettings {
  constructor() {
    super();
    this.id = '2563927C-8D51-43C4-9243-6F69A52E2657';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getFormTemplate(): string {
    return `
    <div id="usersettingsform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="User Settings" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="UserSettingsController">
      <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-datafield="UserId"></div>
      <div class="formpage">
        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="User Settings">
          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
            <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield browsedefaultrows" data-caption="Default Rows per Page (Browse)" data-datafield="BrowseDefaultRows"  style="float:left;width:25%;">
            </div>
          </div>
          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
            <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield applicationtheme" data-caption="Theme" data-datafield="ApplicationTheme"  style="float:left;width:25%;"></div>
          </div>
        </div>
      </div>
    </div>`;
  }
}
var TiwUserSettingsController = new TiwUserSettings();
