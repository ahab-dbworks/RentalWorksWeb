var dbworks;
(function (dbworks) {
    var editor;
    (function (editor) {
        var controllers;
        (function (controllers) {
            var scan2 = /** @class */ (function () {
                //_debug_timers: { parse_path: number, create_folder: number, create_file: number };
                function scan2(editor_controller) {
                    this._editor = editor_controller;
                    this.folder = new editor.models.folder;
                    this.hierarchy = [];
                    this.breadcrumbs = [];
                    this.rootpath = null;
                }
                scan2.prototype.init = function () {
                    var _this = this;
                    this.handlebar_helpers();
                    this.bind().done(function () {
                        _this.prepare_folder();
                        _this.events();
                    });
                };
                scan2.prototype.prepare_folder = function () {
                    jQuery('#open_folder').hide();
                    this.folder.folderName = 'root';
                };
                scan2.prototype.events = function () {
                    var _this = this;
                    var $parent = jQuery('#scan_modal');
                    $parent.on('click', '#open_file, #scan_files, #scan_rescan', function (e) {
                        var $loader = jQuery(e.target).parent().parent().find('.loader');
                        $loader.fadeIn('fast');
                        jQuery('#scan_files').addClass('disabled');
                        var path = jQuery('#scan_path').val();
                        _this.folder.files = [];
                        _this.folder.folders = [];
                        _this.folder.folderName = "root";
                        _this.folder.path = path;
                        var folderName = jQuery('#scan_module_name').val();
                        var menuPath = jQuery('#scan_module_menu_path').val();
                        if (menuPath == '') {
                            menuPath = null;
                        }
                        jQuery('#open_folder, #scan_files').hide();
                        if (+jQuery('#scan_modal').data('config') == 0) {
                            _this._editor.api_open_module(path, folderName, menuPath).done(function (_folder) {
                                _this._editor.bind_file_nav(_folder).done(function () {
                                    jQuery('#scan_modal').modal('hide');
                                    //jQuery('ul.tabs').tabs();
                                });
                            }).fail(function () {
                                dbworksutil.message.message_warning('Module ' + folderName + ' could not be found.');
                                _this.bind();
                            }).always(function () {
                                //$parent.find('.loader').slideUp('fast');
                                $loader.fadeOut('fast');
                            });
                        }
                        if (+jQuery('#scan_modal').data('config') == 1) {
                            _this.api_get_path_folder_and_file_structure(folderName, path, menuPath).done(function (paths) {
                                _this.prcoess_path_2(paths);
                                _this.identify_module_folder(_this.folder);
                                _this.bind_folder_and_files(_this.folder).done(function () {
                                    $loader.fadeOut('fast');
                                    //jQuery('#scan_modal').slideUp('fast');
                                    jQuery('.scan_content').hide();
                                    jQuery('#scan_folder').show();
                                    jQuery('#scan_files').removeClass('disabled');
                                });
                            });
                        }
                    });
                    $parent.on('click', '.scan_folder_section', function (e) {
                        var $target = jQuery(e.currentTarget), path = $target.data('path'), folder = _this.folder, name = $target.data('node');
                        path.split('\\').forEach(function (_path) {
                            if (_path == 'C:') {
                                folder = _this.folder;
                            }
                            else {
                                folder = _this.get_folder(_path, folder);
                            }
                        });
                        _this.bind_folder_and_files(folder).done(function () {
                            jQuery('#scan_back, #scan_rescan').show();
                            if (folder.potentialModuleFolder) {
                                jQuery('#open_folder').show().data('path', path).data('name', name);
                            }
                            else {
                                jQuery('#open_folder').hide().data('path', '').data('name', '');
                            }
                        });
                    });
                    $parent.on('click', '.scan_breadcrumb, .scan_breadcrumb_icon_main', function (e) {
                        var $target = jQuery(e.currentTarget), path = $target.data('path');
                        _this.bind_folder_and_files(_this.get_folder_from_breadcrumb(path)).done(function () {
                            jQuery('#open_folder').hide();
                            jQuery('#scan_back, #scan_rescan').show();
                        });
                    });
                    $parent.on('click', '.scan_open_module', function (e) {
                        var path = _this.folder.path + '\\' + jQuery(e.currentTarget).data('path'), folderName = jQuery(e.currentTarget).data('name');
                        var pathParts = path.split('\\');
                        pathParts.pop();
                        _this._editor.api_open_module(pathParts.join('\\'), folderName, null).done(function (_folder) {
                            _this._editor.bind_file_nav(_folder).done(function () {
                                //jQuery('ul.tabs').tabs();
                                dbworksutil.message.message_success('Added the ' + _folder.folderName + ' module to the module navigation tool bar.');
                            });
                        });
                    });
                    $parent.on('click', '#open_folder', function (e) {
                        var path = _this.folder.path + '\\' + jQuery(e.currentTarget).data('path'), folderName = jQuery(e.currentTarget).data('name');
                        var pathParts = path.split('\\');
                        pathParts.pop();
                        _this._editor.api_open_module(pathParts.join('\\'), folderName, null).done(function (_folder) {
                            _this._editor.bind_file_nav(_folder).done(function () {
                                //jQuery('ul.tabs').tabs();
                                dbworksutil.message.message_success('Added the ' + _folder.folderName + ' module to the module navigation tool bar.');
                            });
                        });
                    });
                };
                scan2.prototype.bind = function (folder) {
                    var $promise = jQuery.Deferred();
                    var source = document.getElementById('designer_scan_template').innerHTML;
                    return dbworksutil.handlebars.render(source, { folder: folder, settings: this._editor._settings.config }, jQuery('#scan_modal .modal-body'));
                };
                scan2.prototype.bind_folder_and_files = function (folder) {
                    var $promise = jQuery.Deferred();
                    this.create_breadcrumbs(folder);
                    this.identify_module_folder(folder);
                    var source = document.getElementById('designer_scan_folders_files_template').innerHTML;
                    return dbworksutil.handlebars.render(source, { folder: folder, breadcrumbs: this.breadcrumbs }, jQuery('#scan_modal #scan_folder'));
                };
                scan2.prototype.identify_module_folder = function (folder) {
                    folder.folders.forEach(function (_folder) {
                        var hasForm = false, hasFormLogic = false, hasBrowse = false, hasTooManyFiles = false, hasAppropriateFiles = false;
                        _folder.files.forEach(function (_file, _index, _arry) {
                            if (_file.fileName.indexOf('Form') != -1 && (_file.ext == 'html' || _file.ext == 'htm')) {
                                hasForm = true;
                            }
                            if (_file.fileName.indexOf('Controller') == -1 && _file.ext == 'ts') {
                                hasFormLogic = true;
                            }
                            if (_file.fileName.indexOf('Browse') != -1 && (_file.ext == 'html' || _file.ext == 'htm')) {
                                if (_file.fileName.indexOf('Grid') == -1) {
                                    hasBrowse = true;
                                }
                            }
                            if (_arry.length > 5) {
                                hasTooManyFiles = true; // if there are more than 5 files in a module folder it might not be a module folder. i'm really just making this up. maybe there are better qualifications for identiying a true module folder. they seem to change module folders though so it's hard to truly know what one is.
                            }
                        });
                        if (hasForm && hasFormLogic && hasBrowse && !hasTooManyFiles)
                            hasAppropriateFiles = true;
                        if (hasAppropriateFiles) {
                            _folder.isValidModuleFolder = true;
                        }
                        if (hasForm && hasFormLogic && hasBrowse && hasTooManyFiles) {
                            _folder.potentialModuleFolder = true;
                        }
                    });
                };
                scan2.prototype.create_breadcrumbs = function (folder) {
                    var _this = this;
                    this.breadcrumbs = [];
                    var path = this.folder.path;
                    this.rootpath = path;
                    this.breadcrumbs.push({ path: path, name: 'Root' });
                    if (folder.path != this.folder.path) {
                        folder.path.split('\\').forEach(function (_folder) {
                            path = path + '\\' + _folder;
                            _this.breadcrumbs.push({ path: path, name: _folder });
                        });
                    }
                };
                scan2.prototype.prcoess_path_2 = function (pathToProcess) {
                    var _this = this;
                    pathToProcess.forEach(function (_path) {
                        var pathOnly = _this.remove_files_from_path(_path);
                        var folder = _this.create_folder_2(pathOnly, _this.folder);
                        _this.add_file_to_folder_2(_path, folder);
                    });
                    console.log(this.folder);
                };
                scan2.prototype.split_path_from_file = function (fullPath) {
                    var splitPath = fullPath.split('\\'), pathAndFile = { path: [], file: null }, file = splitPath[splitPath.length - 1], path = fullPath.replace(file, '').split('\\');
                    if (path.length !== 0) {
                        path.forEach(function (_path, _index, _arry) { if (_path == '')
                            _arry.splice(_index, 1); });
                        pathAndFile.path = path;
                    }
                    else {
                        pathAndFile.path = null;
                    }
                    pathAndFile.file = file;
                    return pathAndFile;
                };
                scan2.prototype.add_file_to_folder_2 = function (path, folder) {
                    var fileParts = path.split('\\')[path.split('\\').length - 1].split('.'), fileName = fileParts[0] + '.' + fileParts[1], ext = fileParts[1];
                    folder.files.push(new editor.models.file({ path: path, fileName: fileName, ext: ext, hasChanged: false }));
                };
                scan2.prototype.create_folder_2 = function (path, folder) {
                    var currentFolder = folder, isNewFolder = false, hasFolder = false, pathParts = path.split('\\'), currentFolderName = null, pathToFolder = [];
                    for (var i = 0; i < pathParts.length; i++) {
                        pathToFolder.push(pathParts[i]);
                        if (currentFolder.folders.length != 0) {
                            for (var e = 0; e < currentFolder.folders.length; e++) {
                                if (currentFolder.folders[e].folderName == pathParts[i]) {
                                    currentFolder = currentFolder.folders[e];
                                    pathParts.splice(i, 1);
                                    i--;
                                    hasFolder = true;
                                    break;
                                }
                            }
                        }
                        if (!hasFolder) {
                            currentFolder.folders.push(new editor.models.folder({ folderName: pathParts[i], path: pathToFolder.join('\\') }));
                            currentFolder = currentFolder.folders[currentFolder.folders.length - 1];
                            pathParts.splice(i, 1);
                            i--;
                        }
                        // reset any validator values
                        hasFolder = false;
                    }
                    return currentFolder;
                };
                scan2.prototype.remove_files_from_path = function (path) {
                    var cleanPath = path.split('\\');
                    if (cleanPath[cleanPath.length - 1].indexOf('.') != -1) {
                        cleanPath.pop();
                    }
                    return cleanPath.join('\\');
                };
                scan2.prototype.get_folder = function (path, folder) {
                    var hasFoundFolder = false;
                    if (folder.folders.length !== 0) {
                        for (var i = 0; i < folder.folders.length; i++) {
                            if (folder.folders[i].folderName == path) {
                                folder = folder.folders[i];
                                hasFoundFolder = true;
                                break;
                            }
                        }
                    }
                    if (!hasFoundFolder) {
                        folder = null;
                    }
                    return folder;
                };
                scan2.prototype.get_parent_folder = function (path) {
                    var _this = this;
                    var splitPath = path.split('\\'), folder = this.folder;
                    splitPath.pop();
                    splitPath.forEach(function (_path) {
                        folder = _this.get_folder(_path, folder);
                    });
                    return folder;
                };
                scan2.prototype.get_folder_from_breadcrumb = function (path) {
                    var folder = this.folder;
                    if (path != folder.path) {
                        var cleanPath = path.replace(folder.path, '');
                        var pathParts = cleanPath.split('\\').filter((function (_p) { return _p != ''; }));
                        pathParts.forEach(function (_path) {
                            for (var i = 0; i < folder.folders.length; i++) {
                                if (folder.folders[i].folderName == _path) {
                                    folder = folder.folders[i];
                                    break;
                                }
                            }
                        });
                    }
                    return folder;
                };
                scan2.prototype.clean_hierarchy = function (hierarchy) {
                    var noneValidTypes = /.git|.npmignore|.jshintignore|.jshintrc|.yml|.jsm|.jade|.sln|.dll|.log|.exe|.txt|.TXT|.csproj|.config|.user|.cmd|.pdb|.json|.ide|.lock|.suo|.bin|.bnf|.md|.jst|.pp|.xml|.props|.nupkg|.rtf|.rsp|.jpg|.gif|.svg|.png|.bmp|.xsc|.xsd|.xss|.cache|.disco|.wsdl|.map|.ps1|.ico|.asax|.csatf|.ttf|.eot|.pdf|.zip|.sql|.cur|.woff|.woff2|_._|LICENSE|LICENCE/;
                    hierarchy = hierarchy.filter(function (_path) {
                        return !_path.match(noneValidTypes);
                    });
                    return hierarchy;
                };
                scan2.prototype.handlebar_helpers = function () {
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
                scan2.prototype.api_get_path_folder_and_file_structure = function (folderName, path, menuPath) {
                    var _this = this;
                    var url = this._editor.api_v1 + 'getpathfolderandfilestructure?folderName=' + folderName + '&path=' + path + '&menuPath=' + menuPath, $promise = jQuery.Deferred();
                    dbworksutil.rest.GET(url).done(function (hierarchy) {
                        hierarchy = _this.clean_hierarchy(hierarchy);
                        $promise.resolve(hierarchy);
                    });
                    return $promise;
                };
                return scan2;
            }());
            controllers.scan2 = scan2;
        })(controllers = editor.controllers || (editor.controllers = {}));
    })(editor = dbworks.editor || (dbworks.editor = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.scan.2.js.map