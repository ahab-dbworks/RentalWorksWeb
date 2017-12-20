var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var module = (function () {
                function module(editor_controller) {
                    this._editor = editor_controller;
                }
                module.prototype.init = function () {
                    var _this = this;
                    this.bind().done(function () {
                        _this.setup();
                        _this.events();
                    });
                };
                module.prototype.setup = function () {
                    jQuery('#structure_input_fields').show();
                };
                module.prototype.bind = function () {
                    var source = document.getElementById('designer_module_template').innerHTML;
                    return dbworksutil.handlebars.render(source, { settings: this._editor._settings.config }, jQuery('#module_modal .modal-body'));
                };
                module.prototype.events = function () {
                    var _this = this;
                    var $parent = jQuery('#module_modal');
                    $parent.on('click', '#generate_module', function (e) {
                        if (_this._editor.validate_module()) {
                            _this.create_module();
                        }
                        else {
                            alert('Fill in missing information.');
                        }
                    });
                    $parent.on('show.bs.modal', function (e) {
                        _this.bind().done(function () {
                            jQuery('#structure_input_fields').show();
                        });
                    });
                };
                module.prototype.create_module = function () {
                    var _this = this;
                    jQuery('#structure_loading').show();
                    this.api_create_module().always(function () {
                        jQuery('.structure_response_container').hide();
                    }).done(function (_folder) {
                        _this._editor.bind_file_nav(_folder).done(function () {
                            _this._editor.bind_recents().done(function () {
                                jQuery('#structure_success, .structure_response_container').show();
                                //jQuery('ul.tabs').tabs();     
                                jQuery('#module_modal').modal('hide');
                                _this._editor._developer.clear_editor();
                                dbworksutil.message.message_success('New module generated successfully.');
                            });
                        });
                    }).fail(function (xhr) {
                        jQuery('#structure_error').show();
                    });
                };
                module.prototype.api_create_module = function () {
                    var _this = this;
                    var modulePath = jQuery('#module_path').val() == null ? 'C:\\' : jQuery('#module_path').val();
                    var url = this._editor.api_v1 + 'createmodule?moduleName=' + jQuery('#module_name').val() + '&modulePath=' + modulePath + '&menuPath=' + jQuery('#menu_path').val();
                    return dbworksutil.rest.GET(url).done(function (_folder) {
                        _this._editor.open_folders.push(_folder);
                        _this._editor.files = _folder.files;
                        _this._editor.store_recently_used_module_data(_folder.folderName, _folder.path);
                    });
                };
                return module;
            }());
            controllers.module = module;
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.module.js.map