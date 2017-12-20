namespace dbworks.editor.controllers {

    export class scan2 {

        _editor: editor.controllers.main;
        folder: editor.models.folder;
        hierarchy: string[];
        breadcrumbs: { path: string, name: string }[];
        rootpath: string;

        //_debug_timers: { parse_path: number, create_folder: number, create_file: number };

        constructor(editor_controller: editor.controllers.main) {            
            this._editor = editor_controller;
            this.folder = new editor.models.folder;
            this.hierarchy = [];
            this.breadcrumbs = [];
            this.rootpath = null;
        }

        init(): void {
            this.handlebar_helpers();
            this.bind().done(() => {
                this.prepare_folder();
                this.events();
            });
        }

        prepare_folder(): void {

            jQuery('#open_folder').hide();
            this.folder.folderName = 'root';

        }

        events(): void {
            var $parent = jQuery('#scan_modal');

            $parent.on('click', '#open_file, #scan_files, #scan_rescan', (e) => {

                var $loader = jQuery(e.target).parent().parent().find('.loader');                

                $loader.fadeIn('fast');

                jQuery('#scan_files').addClass('disabled');
                var path = jQuery('#scan_path').val();
                this.folder.files = [];
                this.folder.folders = [];
                this.folder.folderName = "root";
                this.folder.path = path;
                var folderName = jQuery('#scan_module_name').val();
                var menuPath = jQuery('#scan_module_menu_path').val();

                if (menuPath == '') {
                    menuPath = null;
                }

                jQuery('#open_folder, #scan_files').hide();

                if (+jQuery('#scan_modal').data('config') == 0) {
                    this._editor.api_open_module(path, folderName, menuPath).done((_folder) => {
                        this._editor.bind_file_nav(_folder).done(() => {
                            jQuery('#scan_modal').modal('hide');
                            //jQuery('ul.tabs').tabs();
                        });
                    }).fail(() => {
                        dbworksutil.message.message_warning('Module ' + folderName + ' could not be found.');
                        this.bind();
                        }).always(() => {
                            //$parent.find('.loader').slideUp('fast');
                            $loader.fadeOut('fast');
                        });
                }

                if (+jQuery('#scan_modal').data('config') == 1) {
                    this.api_get_path_folder_and_file_structure(folderName, path, menuPath).done((paths) => {
                        this.prcoess_path_2(paths);
                        this.identify_module_folder(this.folder);
                        this.bind_folder_and_files(this.folder).done(() => {
                            $loader.fadeOut('fast');
                            //jQuery('#scan_modal').slideUp('fast');
                            jQuery('.scan_content').hide();
                            jQuery('#scan_folder').show();
                            jQuery('#scan_files').removeClass('disabled');
                        });
                    });    
                }

                
            });

            $parent.on('click', '.scan_folder_section', (e) => {
                var $target = jQuery(e.currentTarget),
                    path: string = $target.data('path'),
                    folder: editor.models.folder = this.folder,
                    name = $target.data('node');
                
                path.split('\\').forEach((_path) => {
                    if (_path == 'C:') {
                        folder = this.folder;
                    } else {
                        folder = this.get_folder(_path, folder);
                    }                     
                });                
                
                this.bind_folder_and_files(folder).done(() => {
                    jQuery('#scan_back, #scan_rescan').show();
                    if (folder.potentialModuleFolder) {
                        jQuery('#open_folder').show().data('path', path).data('name', name);
                    } else {
                        jQuery('#open_folder').hide().data('path', '').data('name', '');
                    }
                });
            });            

            $parent.on('click', '.scan_breadcrumb, .scan_breadcrumb_icon_main', (e) => {
                var $target = jQuery(e.currentTarget),
                    path: string = $target.data('path');                                                 
                this.bind_folder_and_files(this.get_folder_from_breadcrumb(path)).done(() => {
                    jQuery('#open_folder').hide();
                    jQuery('#scan_back, #scan_rescan').show();
                });
            });

            $parent.on('click', '.scan_open_module', (e) => {

                var path = this.folder.path + '\\' + jQuery(e.currentTarget).data('path'),
                    folderName = jQuery(e.currentTarget).data('name');

                var pathParts = path.split('\\');
                pathParts.pop();

                this._editor.api_open_module(pathParts.join('\\'), folderName, null).done((_folder) => {                    
                    this._editor.bind_file_nav(_folder).done(() => {                    
                        //jQuery('ul.tabs').tabs();
                        dbworksutil.message.message_success('Added the ' + _folder.folderName + ' module to the module navigation tool bar.');
                    });
                });

            });

            $parent.on('click', '#open_folder', (e) => {

                var path = this.folder.path + '\\' + jQuery(e.currentTarget).data('path'),
                    folderName = jQuery(e.currentTarget).data('name');

                var pathParts = path.split('\\');
                pathParts.pop();

                this._editor.api_open_module(pathParts.join('\\'), folderName, null).done((_folder) => {
                    this._editor.bind_file_nav(_folder).done(() => {
                        //jQuery('ul.tabs').tabs();
                        dbworksutil.message.message_success('Added the ' + _folder.folderName + ' module to the module navigation tool bar.');
                    });
                });

            });

        }        

        bind(folder?: editor.models.folder): JQueryPromise<string> {
            var $promise = jQuery.Deferred();
            var source = document.getElementById('designer_scan_template').innerHTML;
            return dbworksutil.handlebars.render(source, { folder: folder, settings: this._editor._settings.config }, jQuery('#scan_modal .modal-body'));
        }

        bind_folder_and_files(folder?: editor.models.folder): JQueryPromise<string> {
            var $promise = jQuery.Deferred();
            this.create_breadcrumbs(folder);
            this.identify_module_folder(folder);
            var source = document.getElementById('designer_scan_folders_files_template').innerHTML;
            return dbworksutil.handlebars.render(source, { folder: folder, breadcrumbs: this.breadcrumbs }, jQuery('#scan_modal #scan_folder'));
        }

        identify_module_folder(folder: editor.models.folder): void {
            folder.folders.forEach((_folder) => {                
                var hasForm = false,
                    hasFormLogic = false,
                    hasBrowse = false,
                    hasTooManyFiles = false,
                    hasAppropriateFiles = false;

                _folder.files.forEach((_file, _index, _arry) => {

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

                if (hasForm && hasFormLogic && hasBrowse && !hasTooManyFiles) hasAppropriateFiles = true;                

                if (hasAppropriateFiles) {
                    _folder.isValidModuleFolder = true;
                }

                if (hasForm && hasFormLogic && hasBrowse && hasTooManyFiles) {
                    _folder.potentialModuleFolder = true;
                }

            });                        
        }

        create_breadcrumbs(folder: editor.models.folder): void {
            
            this.breadcrumbs = [];

            var path = this.folder.path;

            this.rootpath = path;

            this.breadcrumbs.push({ path: path, name: 'Root' });

            if (folder.path != this.folder.path) {
                folder.path.split('\\').forEach((_folder) => {

                    path = path + '\\' + _folder;

                    this.breadcrumbs.push({ path: path, name: _folder });

                });
            }                        
        }  

        prcoess_path_2(pathToProcess: string[]): void {                       

            pathToProcess.forEach((_path) => {
                var pathOnly = this.remove_files_from_path(_path);                
                var folder = this.create_folder_2(pathOnly, this.folder);
                this.add_file_to_folder_2(_path, folder);                                
            });
            
            console.log(this.folder);
        }

        split_path_from_file(fullPath: string): { path: string[], file: string } {            
            var splitPath = fullPath.split('\\'), pathAndFile: { path: string[], file: string } = { path: [], file: null }, file = splitPath[splitPath.length - 1], path = fullPath.replace(file, '').split('\\');

            if (path.length !== 0) {
                path.forEach((_path, _index, _arry) => { if(_path == '') _arry.splice(_index, 1) });
                pathAndFile.path = path;
            } else {
                pathAndFile.path = null;
            }            
            pathAndFile.file = file;

            return pathAndFile;
        }

        add_file_to_folder_2(path: string, folder: editor.models.folder): void {
            var fileParts = path.split('\\')[path.split('\\').length - 1].split('.'), fileName = fileParts[0] + '.' + fileParts[1], ext = fileParts[1];
            folder.files.push(new editor.models.file({ path: path, fileName: fileName, ext: ext, hasChanged: false }));
        }       

        create_folder_2(path: string, folder: editor.models.folder): editor.models.folder {

            var currentFolder = folder,
                isNewFolder = false,
                hasFolder = false,
                pathParts = path.split('\\'),
                currentFolderName = null,
                pathToFolder = [];
                        
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
        }

        remove_files_from_path(path: string): string {
            var cleanPath = path.split('\\');

            if (cleanPath[cleanPath.length - 1].indexOf('.') != -1) {
                cleanPath.pop();
            }
            return cleanPath.join('\\');
        }

        get_folder(path: string, folder: editor.models.folder): editor.models.folder {
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
        }        

        get_parent_folder(path: string): editor.models.folder {
            var splitPath = path.split('\\'), folder: editor.models.folder = this.folder;

            splitPath.pop();

            splitPath.forEach((_path) => {
                folder = this.get_folder(_path, folder);
            });

            return folder;
        }

        get_folder_from_breadcrumb(path: string): editor.models.folder {
            var folder = this.folder;

            if (path != folder.path) {

                var cleanPath = path.replace(folder.path, '');
                var pathParts = cleanPath.split('\\').filter((_p => _p != ''));

                pathParts.forEach((_path) => {

                        for (var i = 0; i < folder.folders.length; i++) {

                            if (folder.folders[i].folderName == _path) {
                                folder = folder.folders[i];                            
                                break;
                            }

                        }

                    });

            }            

            return folder;
        }

        clean_hierarchy(hierarchy: string[]): string[] {
            var noneValidTypes = /.git|.npmignore|.jshintignore|.jshintrc|.yml|.jsm|.jade|.sln|.dll|.log|.exe|.txt|.TXT|.csproj|.config|.user|.cmd|.pdb|.json|.ide|.lock|.suo|.bin|.bnf|.md|.jst|.pp|.xml|.props|.nupkg|.rtf|.rsp|.jpg|.gif|.svg|.png|.bmp|.xsc|.xsd|.xss|.cache|.disco|.wsdl|.map|.ps1|.ico|.asax|.csatf|.ttf|.eot|.pdf|.zip|.sql|.cur|.woff|.woff2|_._|LICENSE|LICENCE/;   
            hierarchy = hierarchy.filter((_path) => {
                return !_path.match(noneValidTypes);
            });

            return hierarchy;
        }

        private handlebar_helpers(): void {

            Handlebars.registerHelper('doesfolderhavefolders', function (folder, options) {

                if (folder.folders.length > 0) {
                    if (folder.files.length == 0 || folder.files.length > 0) {
                        return options.fn(this);
                    }
                } else {
                    if (folder.files.length > 0) {
                        return options.fn(this);
                    } else {
                        return options.inverse(this);
                    }

                }
            });

        }

        api_get_path_folder_and_file_structure(folderName: string, path: string, menuPath: string): JQueryPromise<string[]> {
            var url = this._editor.api_v1 + 'getpathfolderandfilestructure?folderName=' + folderName + '&path=' + path + '&menuPath=' + menuPath,
                $promise = jQuery.Deferred();
            dbworksutil.rest.GET<string[]>(url).done((hierarchy) => {
                hierarchy = this.clean_hierarchy(hierarchy);
                $promise.resolve(hierarchy);
            });
            return $promise;
        }

    }

}