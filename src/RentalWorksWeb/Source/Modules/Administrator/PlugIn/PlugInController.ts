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
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        (async () => {
            const isHubSpotConnected = await FwAjax.callWebApi<any, any>({
                httpMethod: 'POST',
                url: `${applicationConfig.apiurl}api/v1/hubspotplugin/hashubspotrefreshtoken`,
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
                    url: `${applicationConfig.apiurl}api/v1/hubspotplugin/hashubspotrefreshtoken`,
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
    //---------------------------------------------------------------------------------
    getFormTemplate(): string {
        let html: string | string[] = [];
        html.push(
            `<div class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Plug-In" data-hasaudit="false" data-controller="PlugInController">
  <div class="plugins">`);

        if (sessionStorage.clientCode === 'VISTEK') {
            html.push(
                `<div class="plugin vistekprocesscreditcard">
      <div class="plugin-title">
        <div class="title">Vistek Credit Card Plugin</div>
        <div class="synop">Process Credit Card Payments through Vistek's Payment Capture SOAP Service.</div>
      </div>
      <div class="plugin-settings">
        <div class="setting">
          <div class="setting-caption">Use Emulated Service (Development):</div>
          <div class="setting-control">
            <div data-control="FwFormField" data-type="toggleswitch" class="fwcontrol fwformfield" data-caption="" data-datafield="UseMockVistekPaymentCapture"></div>
          </div>
        </div>
        <div class="setting">
          <div class="setting-caption">Payment Capture Service URL:</div>
          <div class="setting-control">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="VistekPaymentCaptureServiceUrl"></div>
          </div>
        </div>
      </div>
    </div>`);
        }

        if (!true) {
        html.push(
            `<div class="plugin azuread">
      <div class="plugin-title">
        <div class="title">Azure Active Directory</div>
        <div class="synop">Connect to Microsoft Azure Active Directory to authenticate RentalWorks access against your AD accounts.</div>
      </div>
      <div class="plugin-settings">
        <div class="setting">
          <div class="setting-caption">Enabled:</div>
          <div class="setting-control">
            <div data-control="FwFormField" data-type="toggleswitch" class="fwcontrol fwformfield" data-caption="" data-datafield="TenantId"></div>
          </div>
        </div>
        <div class="setting">
          <div class="setting-caption">Tenant ID:</div>
          <div class="setting-control">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="TenantId"></div>
          </div>
        </div>
        <div class="setting">
          <div class="setting-caption">Client ID:</div>
          <div class="setting-control">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="ClientId"></div>
          </div>
        </div>
        <div class="setting">
          <div class="setting-caption">Client Secret:</div>
          <div class="setting-control">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="ClientSecret"></div>
          </div>
        </div>
      </div>
    </div>`);

        html.push(
            `<div class="plugin hubspot">
      <div class="plugin-title">
        <div class="title">HubSpot</div>
        <div class="synop">Enables syncing of contacts between RentalWorks and Hubspot</div>
      </div>
      <div class="plugin-settings">
        <div class="setting">
          <div class="setting-caption">Connect to Hubspot:</div>
          <div class="setting-control">
            <div class="plugin-button hubspot-btn"></div>
          </div>
        </div>
        <div class="setting">
          <div class="setting-caption">Manually sync contacts with Hubspot:</div>
          <div class="setting-control">
            <div class="plugin-button sync-contacts-btn">Sync Contacts</div>
          </div>
        </div>
      </div>
    </div>`);

        html.push(
            `<div class="plugin okta">
      <div class="plugin-title">
        <div class="title">OKTA</div>
      </div>
      <div class="plugin-settings">

      </div>
    </div>`);

        html.push(
            `<div class="plugin quickbooksonline">
      <div class="plugin-title">
        <div class="title">QuickBooks Online</div>
        <div class="synop">Enables syncing of invoices, credit memos, and reciepits between RentalWorks and QuickBooks Online.</div>
      </div>
      <div class="plugin-settings">
        <div class="setting">
          <div class="setting-caption">Connect to QuickBooks Online:</div>
          <div class="setting-control">
            <div class="plugin-button">Connect</div>
          </div>
        </div>
        <div class="setting">
          <div class="setting-caption">Refresh the connection token:</div>
          <div class="setting-control">
            <div class="plugin-button">Refresh Token</div>
          </div>
        </div>
        <div class="setting">
          <div class="setting-caption">Revoke the connection token:</div>
          <div class="setting-control">
            <div class="plugin-button">Revoke Token</div>
          </div>
        </div>
        <div class="setting">
          <div class="setting-caption">Last connected:</div>
          <div class="setting-control">
            
          </div>
        </div>
        <div class="setting">
          <div class="setting-caption">Expires in:</div>
          <div class="setting-control">
            
          </div>
        </div>
      </div>
    </div>`);
    }

html.push(
  `</div>
</div>`);

        html = html.join('\n');
        return html;
    }
    //---------------------------------------------------------------------------------
}
//-------------------------------------------------------------------------------------
var PlugInController = new PlugIn();