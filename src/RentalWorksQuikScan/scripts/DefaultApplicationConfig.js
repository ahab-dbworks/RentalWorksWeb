var ApplicationConfig = (function () {
    function ApplicationConfig() {
        this.appbaseurl = '';
        this.fwvirtualdirectory = '';
        this.appvirtualdirectory = '';
        this.debugMode = false;
        this.designMode = false;
        this.demoMode = false;
        this.devMode = false;
        this.demoEmail = 'qsdemo@dbworks.com';
        this.demoPassword = 'QSDEMO';
        this.ajaxTimeoutSeconds = 20;
        this.version = '';
        this.apiurl = '';
        this.photoWidth = 1024;
        this.photoHeight = 1024;
        this.photoQuality = 100;
        this.iPodPhotoQuality = 100;
        this.iPhonePhotoQuality = 100;
    }
    return ApplicationConfig;
}());
var applicationConfig = new ApplicationConfig();
//# sourceMappingURL=DefaultApplicationConfig.js.map