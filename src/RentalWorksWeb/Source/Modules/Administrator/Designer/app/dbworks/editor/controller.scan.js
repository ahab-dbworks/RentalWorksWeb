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
                    //this.folder = new editor.models.folder({ folderName: 'C:\dbworksprojects' });
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
                            //jQuery('.modal-content').addClass('scan_extended_body_folder');
                            //jQuery('.scan_content').hide();            
                            //jQuery('#scan_folder').show();
                        }
                        //this.events();
                    });
                };
                scan.prototype.bind_folder_and_files = function (folder) {
                    var $promise = $.Deferred();
                    return dbworksutil.handlebars.render(templates.editor.scan_folders_files, { folder: folder }, jQuery('#scan_modal #scan_folder'));
                };
                scan.prototype.events = function () {
                    var _this = this;
                    var $parent = jQuery('#scan_modal');
                    //$parent.on('click', '#scan_files, #scan_rescan', (e) => {                
                    //    $parent.find('.loader').slideDown('fast');
                    //    var path = jQuery('#scan_path').val();
                    //    //jQuery('.scan_content').hide();
                    //    //jQuery('#scanning').show();
                    //    this.folder = new editor.models.folder({ folderName: path, parentPath: path });
                    //    //jQuery(e.target).addClass('disabled');
                    //    this.original_hierarchy = [];
                    //    this.hierarchy = [];
                    //    this.folder_parent = { path: path, stopAtNode: path, stopAtDepth: 0 };
                    //    if (+jQuery('#scan_modal').data('config') == 0) {
                    //        var name = jQuery('#scan_module_name').val();
                    //        this._editor.api_open_module(path, name).done((_folder) => {
                    //            this._editor.bind_file_nav(_folder).done(() => {
                    //                jQuery('#scan_modal').modal('hide');
                    //                jQuery('ul.tabs').tabs();
                    //            });
                    //        }).fail(() => {
                    //            fourwallutil.message.message_warning('Module ' + name + ' could not be found.');
                    //            this.bind();
                    //            }).always(() => {
                    //                $parent.find('.loader').slideUp('fast');
                    //            });
                    //    }
                    //    if (+jQuery('#scan_modal').data('config') == 1) {
                    //        this.api_get_path_folder_and_file_structure(path).done(() => {
                    //            jQuery('#scan_back, #scan_rescan').show();
                    //            console.log(this.folder);
                    //        }).always(() => {
                    //            $parent.find('.loader').slideUp('fast');
                    //            });  
                    //    }
                    //});
                    //$parent.on('click', '#scan_files, #scan_rescan', (e) => {
                    //    this._editor._scan2.init();
                    //});
                    //$parent.on('click', '.scan_folder_section', (e) => {
                    //    var $target = jQuery(e.currentTarget), path = $target.data('path'), node = $target.data('node'), depth = $target.data('depth');
                    //    var folder = this.get_folder_by_path_2(path);
                    //    this.folder_breadcrumbs.push({ path: path, stopAtNode: node, stopAtDepth: depth });
                    //    this.bind_folder_and_files(folder).done(() => {
                    //        jQuery('#scan_back, #scan_rescan').show();
                    //    });
                    //});
                    //$parent.on('click', '.scan_breadcrumb, .scan_breadcrumb_icon_main', (e) => {
                    //    var $target = jQuery(e.currentTarget), path = $target.data('path'), node = $target.data('node'), depth = $target.data('depth');
                    //    var folder = this.get_folder_by_path_2(path);                
                    //    this.remove_unused_breadcrumbs(path, node, depth);
                    //    this.bind_folder_and_files(folder).done(() => {
                    //        jQuery('#scan_back, #scan_rescan').show();
                    //    });
                    //});
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
                //private get_folder_by_path_2(path: string): editor.models.folder {
                //    var folder = this.folder, nodes: { node: string, depth: number }[] = [], i = 0, resetIndex = false;
                //    path.split('\\').forEach((_part, _index, _arry) => {
                //        nodes.push({ node: _part, depth: _index + 1 });
                //    });
                //    while (i < folder.folders.length) {
                //        if (nodes.length == 0) {
                //            break;
                //        }
                //        var _folder = folder.folders[i];                
                //        for (var x = 0; x < nodes.length; x++) {
                //            var _node = nodes[x];
                //            if (_folder.folderName == _node.node && _folder.depth == _node.depth) {
                //                folder = _folder;
                //                nodes.splice(i, 1);  
                //                resetIndex = true;
                //                break;
                //            } else {
                //                resetIndex = false;
                //            }
                //        }
                //        if (resetIndex) {
                //            i = 0;
                //        } else {
                //            i++;
                //        }
                //    }            
                //    return folder;
                //}
                scan.prototype.create_folder_structures = function () {
                    this.hierarchy.forEach(function (_fullPath) {
                        //this.generate_folder_structure_by_path(_fullPath);
                    });
                    this.original_hierarchy.forEach(function (_path) {
                        //this.add_files_to_their_respective_folders(_path);
                    });
                    //this.bind(this.folder);
                };
                scan.prototype.remove_files_and_paths_from_hierarchy = function (hierarchy, removeFiles) {
                    for (var i = 0; i < hierarchy.length; i++) {
                        var parts = hierarchy[i].split('\\');
                        // check if any of the parts contain anything from the ignoreFoldersList const and remove the whole path from the list
                        var hasUnspportedFolder = parts.every(function (_p) {
                            var isValid = true;
                            ignoreFoldersList.forEach(function (_ignore) {
                                if (_p == _ignore)
                                    isValid = false;
                            });
                            return isValid;
                        });
                        if (!hasUnspportedFolder) {
                            // has an unsuported folder path, let's remove it
                            hierarchy.splice(i, 1);
                            // lets go back one since we remove one and everything moved up
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
                //private generate_folder_structure_by_path(fullPath: string): void {
                //    var parts = this.get_path_depth(fullPath), currentFolder: editor.models.folder, parentFolder = null;
                //    currentFolder = this.folder;
                //    for (var x = 0; x < parts.length; x++) {
                //        if (currentFolder.folders.length != 0) {
                //            for (var i = 0; i < currentFolder.folders.length; i++) {
                //                // check if a folder matching the nodeName exists
                //                var hasFolder: boolean = false, isNewFolder: boolean = false;
                //                if (parts[x].node == currentFolder.folders[i].folderName && parts[x].depth == currentFolder.folders[i].depth) {
                //                    hasFolder = true;
                //                    parts.splice(x, 1);
                //                    x--;
                //                } else {
                //                    // it didn't match so break
                //                    if((currentFolder.folders.length - 1) == i) {
                //                        isNewFolder = true;
                //                    } else {
                //                        continue;
                //                    }
                //                }                        
                //                if (hasFolder) {
                //                    // exists
                //                    currentFolder = currentFolder.folders[i];
                //                    break;
                //                }
                //                if (isNewFolder) {
                //                    // doesnt exists                                                    
                //                    currentFolder.folders.push(new editor.models.folder({ folderName: parts[x].node, depth: parts[x].depth, path: fullPath }));
                //                    currentFolder = currentFolder.folders[currentFolder.folders.length - 1];
                //                    break;
                //                }
                //            }
                //        } else {
                //            currentFolder.folders.push(new editor.models.folder({ folderName: parts[x].node, depth: parts[x].depth, path: fullPath }));
                //            currentFolder = currentFolder.folders[currentFolder.folders.length - 1];
                //        }                
                //    }
                //}
                //private add_files_to_their_respective_folders(path: string): void {
                //    var fileName: string = null, isFileValid: boolean = false, parts = path.split('\\'), folder: models.folder = null, pathWithoutFile: string = null;
                //    if (parts.length >= 2) {
                //        fileName = parts[parts.length - 1];
                //        // let's check if the last part of the path that we selected as file is actually a valid file we care about. 
                //        // note: don't forget, folders can contain periods aswell, so we check for a period but we also check for supported extension types.
                //        if (fileName.indexOf('.') != -1) {
                //            // it's a file, maybe
                //            var fileExt = fileName.split('.')[1];
                //            for (var i = 0; i < approvedFilesList.length; i++) {
                //                if (fileExt == approvedFilesList[i]) {
                //                    isFileValid = true;
                //                    break;
                //                }
                //            }
                //        } else {
                //            // it's not a file
                //        }
                //        parts.splice((parts.length - 1), 1);
                //        pathWithoutFile = parts.join('\\');
                //        if (isFileValid) {
                //            folder = this.get_folder_by_path_2(pathWithoutFile);
                //            if (folder != null) {
                //                var hasFile = this.has_file_in_folder(folder, fileName);
                //                if (!hasFile) {
                //                    folder.files.push(new models.file({ fileName: fileName }));
                //                    // this just sets the folder model property isValidModule flag to true if it contains htm, htm, ts, cs files
                //                    this.identify_valid_module_folder(folder);
                //                }
                //            }                                        
                //        }
                //    }            
                //}
                scan.prototype.get_path_depth = function (path) {
                    var obj = [];
                    // notice: depth: _index + 1 - we add this because by default all the folder/files have a parent already. So we compensate for this by adding one additional depth to every folder.
                    path.split('\\').forEach(function (_part, _index, _arry) {
                        obj.push({ node: _part, depth: _index + 1 });
                    });
                    return obj;
                };
                scan.prototype.has_file_in_folder = function (folder, fileName) {
                    // this is a hack, we should reliably not need to do this if we sorted through our files correctly in the first place - TODO
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
                        // if folder contains more that 4 files, it's not a valid module folder
                        folder.isValidModuleFolder = false;
                    }
                    if (folder.files.length < 4) {
                        // if folder contains 3 or less files, it's not a valid module folder
                        folder.isValidModuleFolder = false;
                    }
                    if (folder.files.length == 4) {
                        // if folder contains 4 files lets check to see if the 4 files are valid module files
                        for (var i = 0; i < folder.files.length; i++) {
                            for (var e = 0; e < expectedFiles.length; e++) {
                                if (folder.files[i].ext == expectedFiles[e]) {
                                    expectedFiles.splice(e, 1);
                                    break;
                                }
                            }
                        }
                        if (expectedFiles.length == 0) {
                            // if the files are valid, mark that folder as a valid module folder
                            folder.isValidModuleFolder = true;
                        }
                        else {
                            folder.isValidModuleFolder = false;
                            // if not it's not a valid module folder
                        }
                    }
                };
                scan.prototype.identify_typescript_file_set = function (folder) {
                    for (var i = 0; i < folder.files.length; i++) {
                        if (folder.files[i].ext == 'ts') {
                            var ts = folder.files[i].fileName;
                            // TODO
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