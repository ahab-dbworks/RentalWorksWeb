class OktaLogin extends FwOktaLoginBase {

    OktaIntegrationSettings(responseSessionInfo: any): void {
        sessionStorage.setItem('email', responseSessionInfo.webUser.email);
        sessionStorage.setItem('fullname', responseSessionInfo.webUser.fullname);
        sessionStorage.setItem('name', responseSessionInfo.webUser.name);  //justin 05/06/2018
        sessionStorage.setItem('usersid', responseSessionInfo.webUser.usersid);  //justin 05/25/2018  //C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
        sessionStorage.setItem('browsedefaultrows', responseSessionInfo.webUser.browsedefaultrows);
        sessionStorage.setItem('applicationtheme', responseSessionInfo.webUser.applicationtheme);
        sessionStorage.setItem('lastLoggedIn', new Date().toLocaleTimeString());
        sessionStorage.setItem('serverVersion', responseSessionInfo.serverVersion);
        //sessionStorage.setItem('applicationOptions', JSON.stringify(responseSessionInfo.applicationOptions));
        sessionStorage.setItem('userType', responseSessionInfo.webUser.usertype);
        sessionStorage.setItem('applicationtree', JSON.stringify(responseSessionInfo.applicationtree));
        sessionStorage.setItem('clientCode', responseSessionInfo.clientcode);
        sessionStorage.setItem('location', JSON.stringify(responseSessionInfo.location));
        sessionStorage.setItem('defaultlocation', JSON.stringify(responseSessionInfo.location));
        sessionStorage.setItem('warehouse', JSON.stringify(responseSessionInfo.warehouse));
        sessionStorage.setItem('department', JSON.stringify(responseSessionInfo.department));
        sessionStorage.setItem('webusersid', responseSessionInfo.webUser.webusersid);
        sessionStorage.setItem('userid', JSON.stringify(responseSessionInfo.webUser));
        if (responseSessionInfo.webUser.usertype == 'CONTACT') {
            sessionStorage.setItem('deal', JSON.stringify(responseSessionInfo.deal));
        }
        jQuery('html').removeClass('theme-material');
        program.navigate('home');
    }
}


var OktaLoginInstance = new OktaLogin()