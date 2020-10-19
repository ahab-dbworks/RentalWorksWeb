// don't make this file TypeScript or it causes source map errors in the console, the way it gets built
var ApplicationConfig = (function () {
    function ApplicationConfig() {
        this.appbaseurl = '';
        this.fwvirtualdirectory = '';
        this.appvirtualdirectory = '';
        this.debugMode = false;
        this.designMode = false;
        this.ajaxTimeoutSeconds = 300;
        this.version = '';
        this.apiurl = '';
        this.defaultPeek = true,
            this.photoQuality = 100;
        this.photoWidth = 640;
        this.photoHeight = 640;
        this.customLogin = false;
        this.client = '';
        this.allCaps = false;
    }
    return ApplicationConfig;
}());
var applicationConfig = new ApplicationConfig();