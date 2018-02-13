var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var settings = /** @class */ (function () {
                function settings(editor_controller) {
                    this._editor = editor_controller;
                    this.config = new settings_config();
                }
                settings.prototype.init = function () {
                    var _this = this;
                    this.bind().done(function () {
                        _this.events();
                        _this.execute_saved_setting_process();
                    });
                };
                settings.prototype.bind = function () {
                    var source = document.getElementById('designer_settings_template').innerHTML;
                    return dbworksutil.handlebars.render(source, { settings: this.config }, jQuery('#settings_modal .modal-body'));
                };
                settings.prototype.events = function () {
                    var _this = this;
                    var $parent = jQuery('#settings_modal');
                    //jQuery('#tab_nav a').on('click', function (e) {
                    //    e.preventDefault()
                    //    jQuery(this).tab('show')
                    //})
                    $parent.on('click', '#save_settings', function (e) {
                        _this.save_settings_config();
                        //this._editor.modal_settings.close();
                        jQuery('#settings_modal').modal('hide');
                    });
                    $parent.on('click', '#designer_reset', function (e) {
                        var c = confirm('Using the reset feature will refresh the page while attempting to persist your current work. This feature is beta and may not work correctly and may not save any of your work. Do you want to continue to reset the page?');
                        if (c) {
                            //jQuery('#master_designer_container').empty();
                            //new dbworks.editor.controllers.main().init();
                        }
                        else {
                            alert('You just avoided a potential tragedy.');
                        }
                    });
                };
                settings.prototype.update_settings = function () {
                    //this.config.isAutoSave = jQuery('#autosave_setting').is(':checked');
                    this.config.isAutoSaveOn = jQuery('#autosave_1').is(':checked');
                    this.config.isAutoSaveOff = jQuery('#autosave_2').is(':checked');
                    this.config.defaultModuleSavePath = jQuery('#default_save_module_path_setting').val();
                    this.config.defaultMenuSavePath = jQuery('#default_save_menu_path_setting').val();
                    this.execute_saved_setting_process();
                };
                settings.prototype.execute_saved_setting_process = function () {
                    var _this = this;
                    var autoSaveTimer = null;
                    if (this.config.isAutoSaveOn) {
                        dbworksutil.message.message_informational('Auto save active.');
                        autoSaveTimer = setInterval(function () {
                            var hasChanges = _this._editor.get_changed_files_only().length > 0 ? true : false;
                            if (hasChanges) {
                                _this._editor.api_save_module().done(function () {
                                    //fourwallutil.message.message_success('Auto save successful.');
                                });
                            }
                        }, 10000);
                    }
                    if (this.config.isAutoSaveOff) {
                        clearInterval(autoSaveTimer);
                        dbworksutil.message.message_informational('Auto save deactivated.');
                    }
                };
                settings.prototype.get_settings_config = function () {
                    if (typeof (Storage) !== "undefined") {
                        var _settings = JSON.parse(localStorage.getItem("settings_config"));
                        if (_settings != null) {
                            this.config = _settings;
                        }
                    }
                    else {
                        // Sorry! No Web Storage support..
                    }
                };
                settings.prototype.save_settings_config = function () {
                    this.update_settings();
                    if (typeof (Storage) !== "undefined") {
                        var _settings = JSON.stringify(this.config);
                        localStorage.setItem('settings_config', _settings);
                    }
                    else {
                        // Sorry! No Web Storage support..
                    }
                };
                return settings;
            }());
            controllers.settings = settings;
            var settings_config = /** @class */ (function () {
                function settings_config() {
                    this.isAutoSaveOn = false;
                    this.isAutoSaveOff = true;
                    this.defaultModuleSavePath = 'C:\\';
                }
                return settings_config;
            }());
            controllers.settings_config = settings_config;
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.settings.js.map