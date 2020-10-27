routes.push({ pattern: /^module\/plugin$/, action: function (match: RegExpExecArray) { return PlugInController.getModuleScreen(); } });

class PlugIn implements IModule {
    Module: string = 'PlugIn';
    apiurl: string = 'api/v1/plugin';
    caption: string = Constants.Modules.Administrator.children.PlugIn.caption;
    nav: string = Constants.Modules.Administrator.children.PlugIn.nav;
    id: string = Constants.Modules.Administrator.children.PlugIn.id;
    //---------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: IModuleScreen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);

        let $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };

        return screen;
    }
    //---------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        (async () => {
            const isHubSpotConnected = await FwAjax.callWebApi<any, any>({
                httpMethod: 'POST',
                url: `${applicationConfig.apiurl}api/v1/plugin/hashubspotrefreshtoken`,
            }); 
            this.events($form, isHubSpotConnected.hasRefreshToken);
        })()

        //for (var plugin of $form.find('.plugin')) {
        //    var $plugin = jQuery(plugin);
        //    $plugin.find('.status').addClass('disabled').html('Disabled');
        //}

        return $form;
    }
    //---------------------------------------------------------------------------------
    events($form: JQuery, ishubspotconnected: boolean) {
        let installUrl = "https://app.hubspot.com/oauth/authorize?client_id=7fd2d81a-ae52-4a60-af93-caa4d1fd5848&redirect_uri=http://localhost:57949/webdev&scope=contacts";
        let installBtn = $form.find('.hubspot-btn');
        let syncBtn = $form.find('.sync-contacts-btn');

        ishubspotconnected ? installBtn.text('Disconnect') && installBtn.css('background-color', 'red') : installBtn.text('Connect');

        installBtn.on('click', async () => {
            if (installBtn.text() === 'Connect') {
                window.location.href = installUrl;
            } else {
                const deleteHubSpotTokens = await FwAjax.callWebApi<any, any>({
                    httpMethod: 'POST',
                    url: `${applicationConfig.apiurl}api/v1/plugin/deletehubspottokens`,
                });
                if (deleteHubSpotTokens.message = "Success") {
                    installBtn.text('Connect').css('background-color', '#1976d2');
                    installBtn.trigger('change');
                }
            }
        });
        syncBtn.on('click', async () => {
                const isHubSpotConnected = await FwAjax.callWebApi<any, any>({
                    httpMethod: 'POST',
                    url: `${applicationConfig.apiurl}api/v1/plugin/hashubspotrefreshtoken`,
                });
            if (isHubSpotConnected) {
                const syncContacts = await FwAjax.callWebApi<any, any>({
                    httpMethod: 'POST',
                    url: `${applicationConfig.apiurl}api/v1/hubspot/getcontactsepoch`,
                    data: {
                        webusersid: sessionStorage.getItem('webusersid')
                    },
                    $elementToBlock: $form
                });
                if (syncContacts.Result.StatusCode === 200) {
                    FwFunc.showMessage('Sync Has Successfully Completed');
                } else {
                    FwFunc.showMessage('Error, Sync Was Unsuccessful!');
                }
            } else {
                FwFunc.showMessage('HubSpot Must Be Connected Before Syncing!')
            }
        });
    }
    //---------------------------------------------------------------------------------
    afterLoad($form) {
        
    }
    //---------------------------------------------------------------------------------
    //loadForm() {
    //    let $form = this.openForm('EDIT');
    //    FwModule.loadForm(this.Module, $form);

    //    return $form;
    //}

}
//-------------------------------------------------------------------------------------
var PlugInController = new PlugIn();