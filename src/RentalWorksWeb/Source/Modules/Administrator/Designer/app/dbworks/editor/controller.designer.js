var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var desginer = (function () {
                function desginer(editor_controller) {
                    this._editor = editor_controller;
                }
                desginer.prototype.init = function () {
                    this.bind().done(function () {
                        //this.setup()
                        //this.events();
                    });
                };
                desginer.prototype.setup = function () {
                    jQuery('.design_module_section').sortable({
                        connectWith: ".body",
                        dropOnEmpty: true,
                        containment: "body",
                        distance: 5,
                        delay: 100,
                        scroll: true,
                        zIndex: 9999
                    }).disableSelection();
                    //jQuery('.design_module').draggable({ appendTo: 'body' });
                };
                desginer.prototype.events = function () {
                    var _this = this;
                    var $parent = jQuery('#slide-out');
                    jQuery(".design_module_section").on("dragstart", '.design_module', function (e, ui) {
                        jQuery(e.target).addClass('draggable_design_module');
                        jQuery(e.currentTarget).css('z-index', 1000);
                        jQuery(_this).css('z-index', 1000);
                        console.log(ui);
                        console.log(e);
                    });
                    $parent.on('drag', '.design_module', function (e) {
                        alert('hihihihihi');
                        dbworksutil.debug.display_log(null, 'sorted!');
                    });
                };
                desginer.prototype.bind = function () {
                    var source = document.getElementById('designer_designer_template').innerHTML;
                    return dbworksutil.handlebars.render(source, {}, jQuery('#designer_modal'));
                };
                return desginer;
            }());
            controllers.desginer = desginer;
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.designer.js.map