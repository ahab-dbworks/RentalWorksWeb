class FwOktaSignIn {
    constructor() {
        this.renderEl = null;
        this.hasTokensInUrl = null;
        this.authClient = null;
        this.signIn = null;
        this.baseUrl = "";
        this.clientId = "";
        this.redirectUri = "";
        this.authParams = {
            issuer: "",
            responseType: ['token', 'id_token'],
            display: 'page'
        };
    }
}
//# sourceMappingURL=FwOktaSignIn.js.map