var DesignerStaging = /** @class */ (function () {
    function DesignerStaging() {
        this.Module = 'Designer';
    }
    DesignerStaging.prototype.loadDesigner = function (argh1, argh2) {
        var _this = this;
        var screen = {};
        var $designerRWTemplate = jQuery(jQuery('#tmpl-modules-Designer').html());
        screen.$view = $designerRWTemplate;
        screen.load = function () {
            _this.prepareDesigner();
        };
        screen.unload = function () {
            jQuery('#master_designer_container').remove();
        };
        return screen;
    };
    DesignerStaging.prototype.prepareDesigner = function () {
        new index().start();
    };
    return DesignerStaging;
}());
window.DesignerController = new DesignerStaging();
//# sourceMappingURL=DesignerController.js.map