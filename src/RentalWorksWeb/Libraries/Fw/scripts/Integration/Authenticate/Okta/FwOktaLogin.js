var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class FwOktaLoginBase {
    loadOktaLogin() {
        if (applicationConfig.oktaSignIn.hasTokensInUrl()) {
            applicationConfig.oktaSignIn.authClient.token.parseFromUrl(function success(tokens) {
                tokens.forEach(token => {
                    if (token.idToken) {
                        applicationConfig.oktaSignIn.authClient.tokenManager.add('idToken', token);
                    }
                    if (token.accessToken) {
                        applicationConfig.oktaSignIn.authClient.tokenManager.add('accessToken', token);
                    }
                });
                var idToken = applicationConfig.oktaSignIn.signIn.tokenManager.get('idToken');
                console.log('Hello, ' + idToken.claims.email);
                window.location.hash = '';
            }, function error(err) {
                console.error(err);
            });
        }
        else {
            applicationConfig.oktaSignIn.authClient.session.get().then((res) => __awaiter(this, void 0, void 0, function* () {
                if (res.status !== 'INACTIVE') {
                    const responseVerifyOktaSession = yield FwAjax.callWebApi({
                        httpMethod: 'POST',
                        url: `${applicationConfig.apiurl}api/v1/Jwt/oktaverify`,
                        data: {
                            Token: res.id,
                            Apiurl: `${applicationConfig.oktaApiUrl}` + `${res.id}`
                        }
                    });
                    if (res.status === 'ACTIVE' && responseVerifyOktaSession) {
                        if (sessionStorage.getItem('apiToken') === null) {
                            sessionStorage.clear();
                            let $loginWindow = jQuery('body').find('#okta-login-container');
                            const responseJwt = yield FwAjax.callWebApi({
                                httpMethod: 'POST',
                                url: `${applicationConfig.apiurl}api/v1/Jwt/okta`,
                                $elementToBlock: $loginWindow,
                                data: {
                                    Email: res.login,
                                    Token: res.id
                                }
                            });
                            if ((responseJwt.statuscode == 0) && (typeof responseJwt.access_token !== 'undefined')) {
                                sessionStorage.setItem('apiToken', responseJwt.access_token);
                                localStorage.setItem('email', res.login);
                                const responseSessionInfo = yield FwAjax.callWebApi({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/account/session?applicationid=${FwApplicationTree.currentApplicationId}`,
                                    $elementToBlock: $loginWindow
                                });
                                this.OktaIntegrationSettings(responseSessionInfo);
                            }
                            else {
                                FwFunc.showMessage(`Error ${responseJwt.statuscode}` + ' ' + `${responseJwt.statusmessage}`);
                                return program.navigate('default');
                            }
                        }
                    }
                }
                else {
                    applicationConfig.oktaSignIn.renderEl({ el: '#okta-login-container' }, function success(res) {
                    }, function error(err) {
                        console.error(err);
                    });
                }
            }));
        }
    }
}
//# sourceMappingURL=FwOktaLogin.js.map