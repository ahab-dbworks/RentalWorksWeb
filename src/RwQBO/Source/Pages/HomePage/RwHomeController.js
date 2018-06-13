var RwHome = /** @class */ (function () {
    function RwHome() {
        this.Module = 'RwHome';
    }
    RwHome.prototype.getHomeScreen = function () {
        var self = this;
        var applicationOptions = program.getApplicationOptions();
        var screen = {};
        screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());
        screen.load = function () {
            var redirectPath = sessionStorage.getItem('redirectPath');
            if (typeof redirectPath === 'string' && redirectPath.length > 0) {
                setTimeout(function () {
                    sessionStorage.removeItem('redirectPath');
                    program.navigate(redirectPath);
                }, 0);
            }
        };
        return screen;
    };
    ;
    return RwHome;
}());
;
var RwHomeController = new RwHome();
//# sourceMappingURL=RwHomeController.js.map