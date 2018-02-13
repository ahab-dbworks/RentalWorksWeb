var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var ignoreFoldersList = ['.git', '.vs'];
            var approvedFilesList = ['ts', 'js', 'cs', 'htm', 'html', 'hbs', 'css', 'less', 'json', 'xml'];
            var scan = (function () {
                function scan(editor_controller) {
                    this._editor = editor_controller;
                    this.original_hierarchy = [];
                    this.hierarchy = [];
                    this.folder = null;
                    this.folder_breadcrumbs = [];
                    this.folder_parent = null;
                }
                scan.prototype.init = function () {
                    var _this = this;
                    this.handlebar_helpers();
                    this.bind().done(function () {
                        _this.setup();
                        _this.events();
                    });
                };
                scan.prototype.setup = function () {
                    jQuery('#scan_fields').show();
                };
                scan.prototype.bind = function (folder) {
                    var $promise = $.Deferred();
                    return dbworksutil.handlebars.render(templates.editor.scan, { folder: folder, parentFolder: this.folder_parent, breadcrumbs: this.folder_breadcrumbs, settings: this._editor._settings.config }, jQuery('#scan_modal .modal-body')).done(function () {
                        if (folder != null) {
                        }
                    });
                };
                scan.prototype.bind_folder_and_files = function (folder) {
                    var $promise = $.Deferred();
                    return dbworksutil.handlebars.render(templates.editor.scan_folders_files, { folder: folder }, jQuery('#scan_modal #scan_folder'));
                };
                scan.prototype.events = function () {
                    var _this = this;
                    var $parent = jQuery('#scan_modal');
                    $parent.on('click', '.scan_toggle_icon', function (e) {
                        var $section = jQuery(e.currentTarget).parent().next('.scan_toggle_container');
                        if ($section.is(':visible')) {
                            jQuery(e.currentTarget).addClass('scan_toggle_icon_rotate');
                        }
                        else {
                            jQuery(e.currentTarget).removeClass('scan_toggle_icon_rotate');
                        }
                        $section.slideToggle();
                    });
                    $parent.on('show.bs.modal', function (e) {
                        var config = jQuery('#scan_modal').data('config');
                        _this.bind().done(function () {
                            if (config == 0) {
                                jQuery('#module_only_container, #scan_files').hide();
                                jQuery('#open_file').show();
                                jQuery('#scan_modal .modal-title').html('Open');
                            }
                            if (config == 1) {
                                jQuery('#module_name_container, #open_file').hide();
                                jQuery('#scan_files').show();
                                jQuery('#scan_modal .modal-title').html('Scan');
                            }
                            jQuery('#scan_fields').show();
                        });
                    });
                };
                scan.prototype.remove_unused_breadcrumbs = function (path, stopAtNode, stopAtDepth) {
                    if (path != this.folder_parent.stopAtNode && stopAtDepth != this.folder_parent.stopAtDepth) {
                        this.folder_breadcrumbs.forEach(function (_crumb, _index, _arry) {
                            if (_crumb.path == path && _crumb.stopAtNode == stopAtNode && _crumb.stopAtDepth == stopAtDepth) {
                                var count = 0;
                                _arry.forEach(function (_count, _countIndex) {
                                    if (_countIndex > _index) {
                                        count++;
                                    }
                                });
                                _arry.splice((_index + 1), count);
                            }
                        });
                    }
                    else {
                        this.folder_breadcrumbs = [];
                    }
                };
                scan.prototype.create_folder_structures = function () {
                    this.hierarchy.forEach(function (_fullPath) {
                    });
                    this.original_hierarchy.forEach(function (_path) {
                    });
                };
                scan.prototype.remove_files_and_paths_from_hierarchy = function (hierarchy, removeFiles) {
                    for (var i = 0; i < hierarchy.length; i++) {
                        var parts = hierarchy[i].split('\\');
                        var hasUnspportedFolder = parts.every(function (_p) {
                            var isValid = true;
                            ignoreFoldersList.forEach(function (_ignore) {
                                if (_p == _ignore)
                                    isValid = false;
                            });
                            return isValid;
                        });
                        if (!hasUnspportedFolder) {
                            hierarchy.splice(i, 1);
                            i--;
                        }
                        else {
                            if (removeFiles) {
                                var file = parts[parts.length - 1].indexOf('.') === -1 ? null : parts[parts.length - 1];
                                if (!null) {
                                    parts.splice((parts.length - 1), 1);
                                    hierarchy[i] = parts.join('\\');
                                }
                            }
                        }
                    }
                    return hierarchy;
                };
                scan.prototype.get_distinct_folder_structures = function () {
                    var x = this.hierarchy.filter(function (v, i, a) {
                        return a.indexOf(v) === i;
                    });
                    this.hierarchy = x;
                };
                scan.prototype.get_path_depth = function (path) {
                    var obj = [];
                    path.split('\\').forEach(function (_part, _index, _arry) {
                        obj.push({ node: _part, depth: _index + 1 });
                    });
                    return obj;
                };
                scan.prototype.has_file_in_folder = function (folder, fileName) {
                    var hasFile = false;
                    for (var i = 0; i < folder.files.length; i++) {
                        if (folder.files[i].fileName == fileName) {
                            hasFile = true;
                            break;
                        }
                    }
                    return hasFile;
                };
                scan.prototype.identify_valid_module_folder = function (folder) {
                    var expectedFiles = ['htm', 'htm', 'cs', 'ts'];
                    if (folder.files.length > 4) {
                        folder.isValidModuleFolder = false;
                    }
                    if (folder.files.length < 4) {
                        folder.isValidModuleFolder = false;
                    }
                    if (folder.files.length == 4) {
                        for (var i = 0; i < folder.files.length; i++) {
                            for (var e = 0; e < expectedFiles.length; e++) {
                                if (folder.files[i].ext == expectedFiles[e]) {
                                    expectedFiles.splice(e, 1);
                                    break;
                                }
                            }
                        }
                        if (expectedFiles.length == 0) {
                            folder.isValidModuleFolder = true;
                        }
                        else {
                            folder.isValidModuleFolder = false;
                        }
                    }
                };
                scan.prototype.identify_typescript_file_set = function (folder) {
                    for (var i = 0; i < folder.files.length; i++) {
                        if (folder.files[i].ext == 'ts') {
                            var ts = folder.files[i].fileName;
                        }
                    }
                };
                scan.prototype.handlebar_helpers = function () {
                    Handlebars.registerHelper('doesfolderhavefolders', function (folder, options) {
                        if (folder.folders.length > 0) {
                            if (folder.files.length == 0 || folder.files.length > 0) {
                                return options.fn(this);
                            }
                        }
                        else {
                            if (folder.files.length > 0) {
                                return options.fn(this);
                            }
                            else {
                                return options.inverse(this);
                            }
                        }
                    });
                };
                scan.prototype.api_get_path_folder_and_file_structure = function (path) {
                    var _this = this;
                    var url = this._editor.api_v1 + 'getpathfolderandfilestructure?path=' + path;
                    return dbworksutil.rest.GET(url).done(function (hierarchy) {
                        _this.original_hierarchy = JSON.parse(JSON.stringify(hierarchy));
                        _this.hierarchy = JSON.parse(JSON.stringify(hierarchy));
                        _this.remove_files_and_paths_from_hierarchy(_this.original_hierarchy, false);
                        _this.remove_files_and_paths_from_hierarchy(_this.hierarchy, true);
                        _this.get_distinct_folder_structures();
                        _this.create_folder_structures();
                        _this.bind_folder_and_files(_this.folder).done(function () {
                            jQuery('#scan_modal .loader').slideUp('fast');
                            jQuery('.scan_content').hide();
                            jQuery('#scan_folder').show();
                            jQuery('#scan_files').removeClass('disabled');
                        });
                    });
                };
                return scan;
            }());
            controllers.scan = scan;
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.scan.js.map