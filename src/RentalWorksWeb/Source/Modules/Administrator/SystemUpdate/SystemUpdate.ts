﻿routes.push({ pattern: /^module\/update$/, action: function (match: RegExpExecArray) { return SystemUpdateController.getModuleScreen(); } });

class SystemUpdate {
    Module: string = 'SystemUpdate';
    apiurl: string = 'api/v1/systemupdate';
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
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.events($form);
        this.getCurrentVersions($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('div[data-type="button"].update-now').on('click', e => {
            const toVersion = FwFormField.getValueByDataField($form, 'ToVersion');
            if (toVersion !== '') {
                const currentVersion = FwFormField.getValueByDataField($form, 'CurrentVersion');
                const request: any = {
                    CurrentVersion: currentVersion,
                    ToVersion: toVersion,
                };

                const app = jQuery('#application');
                FwFormField.disable($form.find('.update-now'));
                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/applyupdate`, request, FwServices.defaultTimeout, response => {
                    $form.find('.flexrow.msg').html('');
                    if (response.msg) {
                        FwFunc.playErrorSound();
                        $form.find('.error-msg').html(`<div style="margin:0 0 0 0;"><span>${response.msg}</span></div>`);
                        FwFormField.enable($form.find('.update-now'));
                    }
                }, errResponse => {
                    this.showProgressBar($form);
                }, app);
            } else {
                FwNotification.renderNotification('WARNING', 'Select a version in order to update RentalWorks.')
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            const tabname = jQuery(e.currentTarget).attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    const $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'SystemUpdateHistoryGrid',
            gridSecurityId: 'M9KMnPVOQgT43',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    //SystemUpdateHistoryId: FwFormField.getValueByDataField($form, 'SystemUpdateHistoryId')
                };
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getCurrentVersions($form: JQuery): void {
        FwFormField.setValueByDataField($form, 'CurrentVersion', sessionStorage.getItem('serverVersion'));
        const request: any = {
            CurrentVersion: sessionStorage.getItem('serverVersion'),
            OnlyIncludeNewerVersions: false,
        };
        FwAppData.apiMethod(true, 'POST', `${this.apiurl}/availableversions`, request, FwServices.defaultTimeout, response => {
            if (response.Versions) {
                FwFormField.loadItems($form.find('div[data-datafield="ToVersion"]'), response.Versions);
            } else {
                FwNotification.renderNotification('WARNING', 'There was a problem retrieving available versions.')
            }
        }, function onError(response) {
            FwFunc.showError(response);
        }, $form);
    }
    //----------------------------------------------------------------------------------------------
    showProgressBar($appendToElement: JQuery): JQuery {
        //$form.find('.success-msg').html(`<div style="margin:0 0 0 0;"><span>Version Update Initiated. You will be logged out of RentalWorks when complete.</span></div>`);
        const fullurl = `${applicationConfig.apiurl}api/v1/deal/emptyobject`;
        const ajaxOptions: JQuery.AjaxSettings<any> = {
            method: 'GET',
            url: fullurl,
            contentType: 'application/json',
            dataType: 'json',
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('apiToken')}`
            },
            context: {
                requestid: FwAppData.generateUUID()
            },
            statusCode: {
                401: function () {
                    //proceed if a 401 (unauthorized) response is received.  Will get here when upgrading or downgrading and service is back online
                    window.clearInterval(handle);
                    handle = 0;
                    program.getModule('logoff');
                }
            }
        };

        const html: Array<string> = [];
        html.push(`<progress max="100" value="100"><span class="progress_span">0</span></progress>`);
        html.push(`<div class="progress_bar_text"></div>`);
        html.push(`<div class="progress_bar_caption">Initiating update...</div>`);

        const $moduleoverlay = jQuery(`<div class="progress_bar">`);
        $moduleoverlay.html(html.join(''));
        $appendToElement.css('position', 'relative').append($moduleoverlay);

        let currentStep: number = 0;
        let caption: string;
        let handle: number = window.setInterval(() => {
            if ($moduleoverlay) {
                caption = 'Update in progress.  Please Standby...';
                currentStep += 0.5;
                $moduleoverlay.find('progress').val(currentStep);
                $moduleoverlay.find('progress').attr('max', 200);
                $moduleoverlay.find('.progress_bar_caption').text(caption);
            }
            jQuery.ajax(ajaxOptions)
                .done(response => {
                    //proceed if a valid response is received.  Will only get here when re-installing the same version as currentVersion
                    window.clearInterval(handle);
                    handle = 0;
                    program.getModule('logoff');
                });
        }, 500);

        return $moduleoverlay;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
            <div id="systemupdateform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="System Update" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="SystemUpdateController">
              <div id="systemupdateform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabs">
                  <div data-type="tab" id="generaltab" class="generaltab tab" data-tabpageid="generaltabpage" data-caption="System Update"></div>
                  <div data-type="tab" id="builddoctab" class="builddoctab tab" data-tabpageid="builddoctabpage" data-caption="Build Documents"></div>
                  <div data-type="tab" id="updatehistorytab" class="updatehistorytab tab" data-tabpageid="updatehistorytabpage" data-caption="Update History"></div>
                </div>
                <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                  <div class="flexrow">
                    <div class="flexcolumn" style="margin: 0 0 0 8px;">
                    <div class="flexcolumn" style="max-width:135px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Current Version" data-datafield="CurrentVersion" data-enabled="false" style="flex: 0 1 135px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Update to Version" data-datafield="ToVersion" style="flex: 0 1 135px;"></div>
                      </div>
                      <div class="flexrow">
                        <div class="update-now fwformcontrol" data-type="button" style="float:right;margin-top:15px;">Update Now</div>
                      </div>
                    </div>
                    <div class="flexrow msg error-msg" style="margin:4px 0 0 0;"></div>
                    <div class="flexrow msg success-msg" style="margin:4px 0 0 0;"></div>
                  </div>
                </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var SystemUpdateController = new SystemUpdate();