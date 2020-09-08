class FwOktaSignIn {
    renderEl = null;
    hasTokensInUrl = null;
    authClient = null;
    signIn = null;
    baseUrl = "";
    clientId = "";
    redirectUri = "";
    authParams = {
        issuer: "",
        responseType: ['token', 'id_token'],
        display: 'page'
    }
}
