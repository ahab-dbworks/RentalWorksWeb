routes.push({ pattern: /^module\/update$/, action: function (match: RegExpExecArray) { return SystemUpdateController.getModuleScreen(); } });

class SystemUpdate {
    Module: string = 'SystemUpdate';
    apiurl: string = 'api/v1/update';
    caption: string = Constants.Modules.Administrator.children.SystemUpdate.caption;
    nav: string = Constants.Modules.Administrator.children.SystemUpdate.nav;
    id: string = Constants.Modules.Administrator.children.SystemUpdate.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
            this.afterLoad($form);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        FwFormField.loadItems($form.find('div[data-datafield="ToVersion"]'), [
            { value: '2019.1.2.94', text: '2019.1.2.94' },
            { value: '2019.1.2.95', text: '2019.1.2.95' },
            { value: '2019.1.2.96', text: '2019.1.2.96' },
            { value: '2019.1.2.97', text: '2019.1.2.97' },
        ]);

        $form.find('div[data-control="FwTabs"] .tabs').hide();
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('div[data-type="button"].update-now').on('click', e => {
            const toVersion = FwFormField.getValueByDataField($form, 'ToVersion');
            if (toVersion !== '') {
                const request: any = {
                    ToVersion: toVersion,
                };
                FwAppData.apiMethod(true, 'POST', `api/v1/update/applyupdate`, request, FwServices.defaultTimeout, response => {
                   
                }, function onError(response) {
                    FwFunc.showError(response);
                }, null);
            } else {
                FwNotification.renderNotification('WARNING', 'Select a version in order to update RentalWorks.')
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {

    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        // ----------
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
            <div id="systemupdateform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="System Update" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="SystemUpdateController">
              <div id="systemupdateform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabpages">
                  <div class="flexrow">
                    <div class="flexcolumn" style="max-width:135px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Current Version" data-datafield="CurrentVersion" data-enabled="false" style="flex: 0 1 135px;">
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Update to Version" data-datafield="ToVersion" style="flex: 0 1 135px;">
                      </div>
                      <div class="flexrow">
                        <div class="update-now fwformcontrol" data-type="button" style="float:right;margin-top:15px;">Update Now</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var SystemUpdateController = new SystemUpdate();