class RwHome {

    Module: string = 'RwHome';

    constructor() {
        
    }

    getHomeScreen() {
        var self = this;
        var applicationOptions = program.getApplicationOptions();
        var screen: any = {};
        screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());

        screen.load = function () {
            var redirectPath = sessionStorage.getItem('redirectPath');
            if (typeof redirectPath === 'string' && redirectPath.length > 0) {
                setTimeout(() => {
                    sessionStorage.removeItem('redirectPath');
                    program.navigate(redirectPath);
                }, 0);
            }
        };

        return screen;
    };
     
};

var RwHomeController = new RwHome();