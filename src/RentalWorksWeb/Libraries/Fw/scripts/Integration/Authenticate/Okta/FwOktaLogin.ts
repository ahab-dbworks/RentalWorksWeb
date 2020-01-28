
abstract class FwOktaLoginBase {

    abstract OktaIntegrationSettings(responseSessionInfo: any): void;
    
    loadOktaLogin(): void {
        if (applicationConfig.oktaSignIn.hasTokensInUrl()) {
            applicationConfig.oktaSignIn.authClient.token.parseFromUrl(
                function success(tokens) {
                    // Save the tokens for later use, e.g. if the page gets refreshed:
                    // Add the token to tokenManager to automatically renew the token when needed
                    tokens.forEach(token => {
                        if (token.idToken) {
                            applicationConfig.oktaSignIn.authClient.tokenManager.add('idToken', token);
                        }
                        if (token.accessToken) {
                            applicationConfig.oktaSignIn.authClient.tokenManager.add('accessToken', token);
                        }
                    });

                    // Say hello to the person who just signed in:
                    var idToken = applicationConfig.oktaSignIn.signIn.tokenManager.get('idToken');
                    console.log('Hello, ' + idToken.claims.email);

                    // Remove the tokens from the window location hash
                    window.location.hash = '';
                },
                function error(err) {
                    // handle errors as needed
                    console.error(err);
                }
            );
        } else {
            applicationConfig.oktaSignIn.authClient.session.get().then(async (res) => {
                // Make request here against Okta api to verify session exists and is trusted
                if (res.status !== 'INACTIVE') {
                    const responseVerifyOktaSession: boolean = await FwAjax.callWebApi<any, any>({
                        httpMethod: 'POST',
                        url: `${applicationConfig.apiurl}api/v1/Jwt/oktaverify`,
                        data: {
                            Token: res.id,
                            Apiurl: `${applicationConfig.oktaApiUrl}` + `${res.id}`
                        }
                    });
                    // Session exists, show logged in state, check to see if our apitoken exists in the session.
                    if (res.status === 'ACTIVE' && responseVerifyOktaSession) {
                        if (sessionStorage.getItem('apiToken') === null) {
                            sessionStorage.clear();
                            let $loginWindow = jQuery('body').find('#okta-login-container');
                            const responseJwt = await FwAjax.callWebApi<any, any>({
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
                                const responseSessionInfo = await FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/account/session?applicationid=${FwApplicationTree.currentApplicationId}`,
                                    $elementToBlock: $loginWindow
                                });
                                // WIP - MFA - JG 1-23-20
                                //if (responseSessionInfo.Registered === "F") { //switch to false when email verification done
                                //    //sessionStorage.removeItem('apiToken');
                                //    alert('This appears to be your first time authenticating' + 'Please click the verification link sent to your email and login again');
                                //    const sendVerificationEmail = await FwAjax.callWebApi<any, any>({
                                //        httpMethod: 'POST',
                                //        url: `${applicationConfig.apiurl}api/v1/account/emailverify`,
                                //        $elementToBlock: $loginWindow,
                                //        data: {
                                //            Email: responseSessionInfo.Email
                                //        }
                                //    });
                                //    console.log(sendVerificationEmail.response, 'send verify email response');
                                //    return;
                                //} else {
                                this.OktaIntegrationSettings(responseSessionInfo);
                                
                                //}
                            } else {
                                FwFunc.showMessage(`Error ${responseJwt.statuscode}` + ' ' + `${responseJwt.statusmessage}`);
                                return program.navigate('default');
                            }
              
                        }
                    }
                } else {
                    // No session, show the login form
                    applicationConfig.oktaSignIn.renderEl(
                        { el: '#okta-login-container' },
                        function success(res) {

                            // Nothing to do in this case, the widget will automatically redirect
                            // the user to Okta for authentication, then back to this page if successful
                        },
                        function error(err) {
                            // handle errors as needed
                            console.error(err);
                        }
                    );
                }
            });
        

        }

    }

}