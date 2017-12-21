var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var options_context = /** @class */ (function () {
                function options_context(editor_controller) {
                    this._editor = editor_controller;
                }
                options_context.prototype.init = function () {
                    var _this = this;
                    this.bind().done(function () {
                        _this.events();
                    });
                };
                options_context.prototype.bind = function () {
                    var source = document.getElementById('designer_options_context_template').innerHTML;
                    return dbworksutil.handlebars.render(source, null, jQuery('#context_menu_options'));
                };
                options_context.prototype.events = function () {
                    var _this = this;
                    var $parent = jQuery('#main_master_body');
                    $parent.on('click', '#context_options', function (e) {
                        var displayPanel = +jQuery('#main_content_body').data('panel');
                        _this.toggle_contexts(displayPanel);
                        jQuery('#context_menu_options').slideToggle('fast');
                    });
                    $parent.on('change', '.display_size_option', function (e) {
                        var sizeType = +jQuery("input:radio[name ='displaysizeoption']:checked").val();
                        _this.toggle_display_size(sizeType);
                    });
                };
                options_context.prototype.toggle_contexts = function (displayPanel) {
                    jQuery('.context_menu_nav').hide();
                    switch (displayPanel) {
                        case 0:
                            jQuery('#edit_options').show();
                            break;
                        case 1:
                            jQuery('#preview_options').show();
                            //jQuery('#').show();
                            break;
                        default:
                            jQuery('#default_options').show();
                    }
                };
                options_context.prototype.toggle_display_size = function (size) {
                    var $preview = jQuery('#preview_view');
                    switch (size) {
                        case 1:
                            $preview.width('100%').height('100%');
                            break;
                        case 2:
                            $preview.width('1024px');
                            break;
                        case 3:
                            $preview.width('480px');
                            break;
                        default:
                            $preview.width('100%').height('100%');
                    }
                };
                return options_context;
            }());
            controllers.options_context = options_context;
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.options_context.js.map