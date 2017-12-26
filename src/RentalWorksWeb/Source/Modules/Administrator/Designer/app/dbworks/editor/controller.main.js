var geval = eval;
var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var main = /** @class */ (function () {
                function main() {
                    this.api_v1 = applicationConfig.apiurl + 'api/v1/designer/';
                    this.files = [];
                    this.recent_modules = [];
                    this.scrolltop = 0;
                    this.open_folders = [];
                    this.nav_mode = 1;
                    this._settings = new editor.controllers.settings(this);
                    this._developer = new editor.controllers.developer(this);
                    this._structure = new editor.controllers.module(this);
                    //this._scan = new editor.controllers.scan(this);           
                    this._scan2 = new editor.controllers.scan2(this);
                    this._designer = new editor.controllers.desginer(this);
                    this._options_context = new editor.controllers.options_context(this);
                    this._process_fwgrid = new editor.controllers.process_fwgrids(this);
                }
                main.prototype.init = function () {
                    var _this = this;
                    this._settings.get_settings_config();
                    this.bind().done(function () {
                        _this.bind_recents().done(function () {
                            _this.events();
                            _this._structure.init();
                            //this._scan.init();
                            _this._scan2.init();
                            _this._settings.init();
                            _this._developer.init();
                            _this._designer.init();
                            _this._options_context.init();
                            dbworksutil.message.alert_setup();
                        });
                    });
                };
                main.prototype.setup = function () {
                    //jQuery('.dropdown-toggle').dropdown();
                };
                main.prototype.events = function () {
                    var _this = this;
                    var $parent = jQuery('#master_designer_container');
                    $parent.on('click', '.new_file_option', function (e) {
                        //var menu_action = jQuery(e.target).data('type');
                        //this.file_new(menu_action);
                        jQuery('#module_modal').modal('show');
                    });
                    $parent.on('click', '.open_file_option', function (e) {
                        //this.modal_scan.open();
                        var config = +jQuery(e.currentTarget).data('config');
                        jQuery('#scan_modal').data('config', config).modal('show');
                    });
                    $parent.on('click', '.file_save', function (e) {
                        _this.api_save_module().done(function (data) {
                            dbworksutil.message.message_success('Save successful.');
                        });
                    });
                    $parent.on('click', '.editable_file', function (e) {
                        var folderIndex = jQuery(e.currentTarget).parent().parent().parent().data('index'), fileIndex = jQuery(e.currentTarget).data('index'), file = _this.get_file_from_folder(folderIndex, fileIndex);
                        jQuery('#active_editable_file').text(file.fileName);
                        jQuery('#main_content_body').data('activefolderindex', folderIndex).data('activefileindex', fileIndex);
                        _this.toggle_view(file, 0);
                    });
                    $parent.on('click', '#recents', function (e) {
                        _this.bind_recents().done(function () {
                            jQuery('#recents_modal').modal('show');
                        });
                    });
                    $parent.on('click', '.clear_recent_options', function (e) {
                        localStorage.setItem("recent_modules", null);
                        _this.bind_recents();
                    });
                    $parent.on('click', '.expand_module_folder', function (e) {
                        var index = +jQuery(e.currentTarget).parent().parent().parent().data('index'), folder = _this.open_folders[index];
                        _this.bind_filenav_2(folder, index);
                    });
                    $parent.on('click', '.edit_module_folder', function (e) {
                        alert('edit not implemented');
                    });
                    $parent.on('click', '.delete_module_folder', function (e) {
                        var c = confirm('Are you sure you want to hard delete this folder?');
                        if (c) {
                            alert('delete not implemented');
                        }
                    });
                    $parent.on('click', '#expand_collapsed_folders', function (e) {
                        _this.bind_filenav_1();
                    });
                    $parent.on('click', '.expand_collapsed_folder', function (e) {
                        var folderIndex = jQuery(e.currentTarget).data('index'), folder = _this.open_folders[folderIndex];
                        _this.bind_filenav_2(folder, folderIndex);
                    });
                    $parent.on('click', '.close_full_module', function (e) {
                        var path = jQuery(e.target).data('path'), name = jQuery(e.target).data('name'), index = jQuery(e.currentTarget).parent().parent().parent().data('index'), folder = _this.open_folders[index];
                        _this.remove_folder_from_folders(index);
                        _this.bind_file_nav(folder);
                    });
                    $parent.on('click', '.dev_options', function (e) {
                        var view = +jQuery(e.currentTarget).data('view'), folderIndex = jQuery('#main_content_body').data('activefolderindex'), fileIndex = jQuery('#main_content_body').data('activefileindex'), file = _this.get_file_from_folder(folderIndex, fileIndex);
                        jQuery('#main_content_body').data('panel', view);
                        _this.toggle_view(file, view);
                    });
                    $parent.on('click', '#design', function (e) {
                        jQuery("#design_panel").slideToggle('fast');
                    });
                    $parent.on('change', '#render_style_select', function (e) {
                        jQuery('html').removeClass('theme-default theme-classic theme-material').
                            addClass(jQuery(e.currentTarget).val());
                    });
                    $parent.on('click', '#editor_settings', function (e) {
                        jQuery('#settings_modal').modal('show');
                    });
                    $parent.on('click', '.structure_close', function (e) {
                        jQuery('#new_structure_container').hide();
                    });
                    $parent.on('click', '.open_recent_option', function (e) {
                        var path = jQuery(e.target).data('path'), name = jQuery(e.target).data('name'), menuPath = jQuery(e.target).data('menupath');
                        if (!_this.validate_recent_file_is_open(path, name)) {
                            _this.api_open_module(path, name, menuPath).done(function (_folder) {
                                switch (_this.nav_mode) {
                                    case 1:
                                        _this.bind_filenav_1();
                                        break;
                                    case 2:
                                        _this.bind_filenav_2(_folder, null);
                                        break;
                                    default:
                                }
                            });
                        }
                        else {
                            dbworksutil.message.message_default(name + ' is already open.');
                        }
                    });
                };
                main.prototype.bind = function () {
                    var _this = this;
                    var source = document.getElementById('designer_main_template').innerHTML;
                    return dbworksutil.handlebars.render(source, { version: product.version }, jQuery('#master_designer_container')).done(function () {
                        _this.setup();
                    });
                };
                main.prototype.bind_file_nav = function (folder) {
                    var $promise = jQuery.Deferred();
                    switch (this.nav_mode) {
                        case 1:
                            this.bind_filenav_1().done(function (data) {
                                $promise.resolveWith(data);
                            });
                            break;
                        case 2:
                            this.bind_filenav_2(folder, null).done(function (data) {
                                $promise.resolveWith(data);
                            });
                            break;
                        default:
                    }
                    return $promise;
                };
                main.prototype.bind_filenav_1 = function () {
                    this.nav_mode = 1;
                    var source = document.getElementById('designer_filenav_mode_1_template').innerHTML;
                    return dbworksutil.handlebars.render(source, { folders: this.open_folders }, jQuery('#file_management_container'));
                };
                main.prototype.bind_filenav_2 = function (folder, folderIndex) {
                    this.nav_mode = 2;
                    var source = document.getElementById('designer_filenav_mode_2_template').innerHTML;
                    return dbworksutil.handlebars.render(source, { files: folder.files, index: folderIndex, folders: this.open_folders }, jQuery('#file_management_container'));
                };
                main.prototype.bind_recents = function () {
                    this.get_recently_used_module_data();
                    var source = document.getElementById('designer_recents_template').innerHTML;
                    return dbworksutil.handlebars.render(source, { recent: this.recent_modules }, jQuery('#recents_modal .modal-body'));
                };
                main.prototype.validate_module = function () {
                    var isValid = false;
                    if (jQuery('#module_name').val() != null && jQuery('#module_name').val() != '')
                        isValid = true;
                    return isValid;
                };
                main.prototype.evaluate_form_javascript = function () {
                    this.files.forEach(function (_f) {
                        if (_f.fileName.indexOf('Grid') == -1 && _f.ext == 'ts') {
                            var convjs = editor.controllers.ts_to_js_compiler.compile(_f.fileContents);
                            geval(convjs.outputText);
                        }
                    });
                };
                main.prototype.destory_form_javascript = function () {
                };
                main.prototype.render = function (file) {
                    var content = file.fileContents, $root = jQuery('<div>' + content + '</div>'), $fwControls = $root.find('.fwcontrol');
                    if (file.ext === 'htm') {
                        jQuery('#preview_view').empty();
                        jQuery('#preview_view').html(this._developer.code_mirror.getValue());
                        var $controls = jQuery('#preview_view').find('.fwcontrol');
                        FwControl.renderRuntimeControls($controls);
                    }
                    else {
                        jQuery('#preview_view').html('<strong>File type ' + file.ext.toUpperCase() + ' not supported in preview.</strong>');
                    }
                };
                main.prototype.toggle_view = function (file, view) {
                    if (jQuery('.CodeMirror-scroll').scrollTop() != 0) {
                        this.scrolltop = jQuery('.CodeMirror-scroll').scrollTop();
                    }
                    jQuery('.main_editor_view').hide();
                    switch (view) {
                        case 0:
                            jQuery('#code_view').show();
                            this._developer.configure_codemirror(file.ext);
                            this._developer.edit_file(file);
                            jQuery('.CodeMirror-scroll').scrollTop(this.scrolltop);
                            break;
                        case 1:
                            jQuery('#preview_view').show();
                            //this.evaluate_form_javascript();
                            this.render(file);
                            this._process_fwgrid.process_2(jQuery('<div>' + file.fileContents + '</div>'));
                            //this._process_fwgrid.process(file);
                            break;
                        default:
                            dbworksutil.message.message_warning('Oops, view not found.');
                    }
                };
                main.prototype.file_new = function (action) {
                    switch (action) {
                        case 'structure':
                            //jQuery('#new_structure_container').show();
                            break;
                        case 'html':
                            break;
                        case 'js':
                            break;
                        case 'less':
                            break;
                    }
                    this.modal_structure.open();
                };
                main.prototype.get_changed_files_only = function () {
                    var changed_files = new Array();
                    this.files.forEach(function (_file) {
                        if (_file.hasChanged)
                            changed_files.push(new editor.models.file(_file));
                    });
                    return changed_files;
                };
                main.prototype.get_changed_files_from_open_folders = function () {
                    var changed_files = new Array();
                    this.open_folders.forEach(function (_folder) {
                        _folder.files.forEach(function (_file) {
                            if (_file.hasChanged)
                                changed_files.push(new editor.models.file(_file));
                        });
                    });
                    return changed_files;
                };
                main.prototype.get_file = function (index) {
                    if (index == undefined) {
                        index = +jQuery('#main_content_body').data('activefileindex');
                    }
                    if (index == null) {
                        dbworksutil.message.message({ message: 'activefileindex data attribute has a null value.', alertType: 'warning' });
                    }
                    return this.files[index];
                };
                main.prototype.get_file_from_folder = function (folderIndex, fileIndex) {
                    return this.open_folders[folderIndex].files[fileIndex];
                };
                main.prototype.remove_folder_from_folders = function (index) {
                    for (var i = 0; i < this.open_folders.length; i++) {
                        if (i == index) {
                            this.open_folders.splice(i, 1);
                            break;
                        }
                    }
                };
                main.prototype.get_recently_used_module_data = function () {
                    if (typeof (Storage) !== "undefined") {
                        this.recent_modules = JSON.parse(localStorage.getItem("recent_modules"));
                        if (this.recent_modules == null || undefined) {
                            this.recent_modules = [];
                        }
                    }
                    else {
                        // Sorry! No Web Storage support..
                    }
                };
                main.prototype.store_recently_used_module_data = function (key, value) {
                    // only save top 5            
                    if (typeof (Storage) !== "undefined") {
                        this.get_recently_used_module_data();
                        if (this.recent_modules.length == 5) {
                            this.recent_modules.pop();
                            this.recent_modules.unshift({ key: key, value: value });
                        }
                        else {
                            this.recent_modules.push({ key: key, value: value });
                        }
                        localStorage.setItem("recent_modules", JSON.stringify(this.recent_modules));
                    }
                    else {
                        // Sorry! No Web Storage support..
                    }
                };
                main.prototype.validate_recent_file_is_open = function (path, name) {
                    var isOpen = false;
                    for (var i = 0; i < this.open_folders.length; i++) {
                        if (this.open_folders[i].path == path && this.open_folders[i].folderName == name) {
                            isOpen = true;
                            break;
                        }
                    }
                    return isOpen;
                };
                main.prototype.remove_recently_used_module_data = function (path, name) {
                    if (typeof (Storage) !== "undefined") {
                        this.get_recently_used_module_data();
                        for (var i = 0; i < this.recent_modules.length; i++) {
                            if (this.recent_modules[i].key == name && this.recent_modules[i].value == path) {
                                this.recent_modules.splice(i, 1);
                                localStorage.setItem("recent_modules", JSON.stringify(this.recent_modules));
                                this.bind_recents();
                                break;
                            }
                        }
                    }
                    this.bind_recents();
                };
                main.prototype.api_open_module = function (path, name, menuPath) {
                    var _this = this;
                    var url = this.api_v1 + 'openmodule?modulePath=' + path + '&moduleName=' + name + '&menuPath=' + menuPath;
                    return dbworksutil.rest.GET(url, false, {
                        message: "We can't find that module. We'll remove it from your Recents."
                    }).done(function (_folder) {
                        _this.open_folders.push(new editor.models.folder(_folder));
                        _this.files = _folder.files;
                        console.log(_this.files);
                    });
                };
                main.prototype.api_save_module = function () {
                    var url = this.api_v1 + 'savestructure', changedFiles = this.get_changed_files_from_open_folders(), $promise = jQuery.Deferred();
                    //this.files.forEach(f => f.hasChanged = false);
                    if (changedFiles.length != 0) {
                        this.open_folders.forEach(function (_folder) { return _folder.files.forEach(function (_file) { return _file.hasChanged = false; }); });
                        return $promise.resolve(dbworksutil.rest.POST(url, changedFiles, { message: "An error occured when you tried to save. Try again." }));
                    }
                    else {
                        dbworksutil.message.message_informational('Nothing to save.');
                        return $promise.reject();
                    }
                };
                return main;
            }());
            controllers.main = main;
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.main.js.map