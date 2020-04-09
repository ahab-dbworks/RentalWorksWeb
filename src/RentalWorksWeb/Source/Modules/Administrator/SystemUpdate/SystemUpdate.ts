﻿routes.push({ pattern: /^module\/update$/, action: function (match: RegExpExecArray) { return SystemUpdateController.getModuleScreen(); } });

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

        FwFormField.loadItems($form.find('div[data-datafield="UpdateVersion"]'), [
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
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="System Update" style="max-width:200px">
                    <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Current Version" data-datafield="CurrentVersion" data-enabled="false" style="flex: 0 1 200px;">
                    </div>
                    <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Update to Version" data-datafield="UpdateVersion" style="flex: 0 1 200px;">
                    </div>
                  </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var SystemUpdateController = new SystemUpdate();