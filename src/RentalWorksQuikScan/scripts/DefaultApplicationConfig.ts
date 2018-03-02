class ApplicationConfig {
    appbaseurl:          string  = '';
    fwvirtualdirectory:  string  = '';
    appvirtualdirectory: string  = '';
    debugMode:           boolean = false;
    designMode:          boolean = false;
    demoMode:            boolean = false;
    devMode:             boolean = false;
    demoEmail:           string  = 'qsdemo@dbworks.com';
    demoPassword:        string  = 'QSDEMO';
    ajaxTimeoutSeconds:  number  = 20;
    version:             string  = '';
    apiurl:              string  = '';
    photoWidth:          number  = 1024;
    photoHeight:         number  = 1024;
    photoQuality:        number  = 100;
    iPodPhotoQuality:    number  = 100;
    iPhonePhotoQuality:  number  = 100;
}
var applicationConfig = new ApplicationConfig();