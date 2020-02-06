# How To Add Okta To a Project

1. update the FW in the project
2. Add the following code/settings to your applicationConfig.js in the main directory, substituting the parameters where necessary:

```
applicationConfig.isOktaLogin = false; //change this to true or false to toggle okta on or off.
applicationConfig.oktaApiUrl = 'https://YOUR_DEV_ORG_URL.oktapreview.com (this needs to be your okta "Org Url")/api/v1/sessions/'
applicationConfig.oktaSignIn = new OktaSignIn({
    baseUrl: "https://YOUR_DEV_ORG_URL.oktapreview.com",
    clientId: "YOUR CLIENT ID",  --> this comes from your application's client id.
    redirectUri: "(should be http://localhost/gateworksweb/ or http://localhost/rentalworksweb/ etc... this cannot have a hash fragment and must match the redirect uri set in the client on oktas dev console!",
    authParams: {
        issuer: "https://YOUR_DEV_ORG_URL.oktapreview.com ( --> This needs to be your okta "Org Url") /oauth2/default",
        responseType: ['token', 'id_token'],
        display: 'page'
    }
});
```

3. Add the CDN for the JS and CSS files to the main index file in the main directory (make sure its loaded before applicationConfig file/check Okta for latest links):

```
<script src="https://global.oktacdn.com/okta-signin-widget/3.2.0/js/okta-sign-in.min.js" type="text/javascript"></script>
<link href="https://global.oktacdn.com/okta-signin-widget/3.2.0/css/okta-sign-in.min.css" type="text/css" rel="stylesheet" />
```

4. Add OktaLogin.ts to extend FwOktaLoginBase with proper sessionStorage settings to get a session from the db (ex from GWW):


```
class OktaLogin extends FwOktaLoginBase {
        OktaIntegrationSettings(responseSessionInfo: any): void {
            sessionStorage.setItem('applicationtree', JSON.stringify(responseSessionInfo.SecurityTree));
            AppSession.ApplicationTheme(responseSessionInfo.ApplicationTheme);
            AppSession.BrowseDefaultRows(responseSessionInfo.BrowseDefaultRows);
            AppSession.Campus(responseSessionInfo.Campus);
            AppSession.Deal(responseSessionInfo.Deal);
            AppSession.ParkingCoordinatorDeals(responseSessionInfo.ParkingCoordinatorDeals);
            AppSession.PersonId(responseSessionInfo.PersonId);
            AppSession.UserName(responseSessionInfo.FullName);
            AppSession.UserType(responseSessionInfo.UserType);
            program.navigate('home');
        }
    }
var OktaLoginInstance = new OktaLogin()
```

5. Add Okta api Key in appsettings.json inside the "JwtIssuerOptions" Object:

```
"JwtIssuerOptions": {
      // Secret key used to sign security tokens for accessing the api. If you leave it blank it will throw an exception on startup and in the exception message there will be a random key that you can use.
      "SecretKey": "xxxxxxxxxxx",
      "OktaKey": "xxxxxxxxxx",
```

6. Add okta login screen conditional logic to Base.ts, in Base.ts find a method called "getLoginScreen()", add "isOktaLogin" to the viewmodel object -

```
getLoginScreen() {
        var viewModel = {
            captionPanelLogin:       'GateWorks Login',
            captionEmail:            Languages.translate('E-mail / Username'),
            valueEmail:              (AppLocal.UserName() ? AppLocal.UserName() : ''),
            captionPassword:         Languages.translate('Password'),
            valuePassword:           '',
            captionBtnLogin:         Languages.translate('Sign In'),
            captionBtnCancel:        Languages.translate('Cancel'),
            captionPasswordRecovery: Languages.translate('Recover Password'),
            captionAbout:            Languages.translate('About'),
            captionSupport:          Languages.translate('Support'),
            valueYear:               new Date().getFullYear(),
            valueVersion:            applicationConfig.version,
            isOktaLogin:             applicationConfig.isOktaLogin
        };
```

Add if else conditional above the screen.$view.on(click) event handler (line 48 in RWW)

```
if (viewModel.isOktaLogin) {
            OktaLoginInstance.loadOktaLogin();
        } else {

        screen.$view
                .on('click', '.btnLogin', async (e: JQuery.Event) => {
                    try {
```

Add endpoints to Jwt controller or any controller that extends FwJwtController in your app

```
//---------------------------------------------------------------------------------------------
        [HttpPost("okta")]
        [FwControllerMethod(Id: "GENERATE_GUID_HERE", FwControllerActionTypes.Browse, AllowAnonymous: true, ValidateSecurityGroup: false)]
        public async Task<ActionResult<JwtResponseModel>> OktaPost([FromBody] OktaRequest request)
        {
            return await DoOktaPost(request);
        }
        //---------------------------------------------------------------------------------------------
        [HttpPost("oktaverify")]
        [FwControllerMethod(Id: "GENERATE_GUID_HERE", FwControllerActionTypes.Browse, AllowAnonymous: true, ValidateSecurityGroup: false)]
        public async Task<ActionResult<OktaSessionResponseModel>> OktaVerify([FromBody] OktaSessionRequest request)
        {
            request.appConfig = this._appConfig;
            return await FwJwtLogic.OktaVerifySession(request);
        }
```

