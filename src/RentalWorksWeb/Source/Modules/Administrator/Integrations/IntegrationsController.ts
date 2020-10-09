routes.push({ pattern: /^module\/integrations$/, action: function (match: RegExpExecArray) { return IntegrationsController.getModuleScreen(); } });

class Integrations implements IModule {
    Module: string = 'Integrations';
    apiurl: string = 'api/v1/integrations';
    caption: string = Constants.Modules.Administrator.children.Integrations.caption;
    nav: string = Constants.Modules.Administrator.children.Integrations.nav;
    id: string = Constants.Modules.Administrator.children.Integrations.id;
    //---------------------------------------------------------------------------------
    getModuleScreen() {
        let $form;
        const screen: IModuleScreen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $form = this.openForm('EDIT');

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
                url: `${applicationConfig.apiurl}api/v1/integrations/hashubspotrefreshtoken`,
            }); 
            this.events($form, isHubSpotConnected.hasRefreshToken);
            const getContacts = await FwAjax.callWebApi<any, any>({
                httpMethod: 'POST',
                url: `${applicationConfig.apiurl}api/v1/hubspot/getcontactsepoch`,
                data: {
                    accessToken: 'CLrMmOrOLhICAQEYz8GHBCCr5dcFKIKADjIZAKW9C0Zht3IbAGRHIgEFUHRt7C8MWO0iWjoaAAoCQQAADIADAAgAAAABAAAAAAAAABjAABNCGQClvQtGwFLeovRTX8dQYybTHl0f_9H2wto'
                }
            });
            console.log(getContacts);
        })()
        
        return $form;
    }
    //---------------------------------------------------------------------------------
    events($form: JQuery, ishubspotconnected: boolean) {
        let installUrl = "https://app.hubspot.com/oauth/authorize?client_id=7fd2d81a-ae52-4a60-af93-caa4d1fd5848&redirect_uri=http://localhost:57949/webdev&scope=contacts";
        let installBtn = $form.find('.hubspot-btn');

        ishubspotconnected ? installBtn.text('Disconnect') && installBtn.css('background-color', 'red') : installBtn.text('Connect');

        installBtn.on('click', async () => {
            if (installBtn.text() === 'Connect') {
                window.location.href = installUrl;
            } else {
                const deleteHubSpotTokens = await FwAjax.callWebApi<any, any>({
                    httpMethod: 'POST',
                    url: `${applicationConfig.apiurl}api/v1/integrations/deletehubspottokens`,
                });
                if (deleteHubSpotTokens.message = "Success") {
                    installBtn.text('Connect').css('background-color', '#1976d2');
                    installBtn.trigger('change');
                }
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
var IntegrationsController = new Integrations();