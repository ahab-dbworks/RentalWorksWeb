namespace dbworks.editor.controllers {

    export class main {

        api_v1: string;
        files: editor.models.file[];
        modal_structure: dbworksutil.modal;
        modal_scan: dbworksutil.modal;
        modal_settings: dbworksutil.modal;
        recent_modules: { key: string, value: string }[];
        open_folders: editor.models.folder[];
        scrolltop: number;
        nav_mode: number;
        _settings: editor.controllers.settings;
        _developer: editor.controllers.developer;
        _structure: editor.controllers.module;
        //_scan: editor.controllers.scan;
        _scan2: editor.controllers.scan2;
        _designer: editor.controllers.desginer;
        _options_context: editor.controllers.options_context;
        _process_fwgrid: editor.controllers.process_fwgrids;

        constructor() {
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

        init(): void {                     
            this._settings.get_settings_config();
            this.bind().done(() => {
                this.bind_recents().done(() => {
                    this.events();
                    this._structure.init();
                    //this._scan.init();
                    this._scan2.init();
                    this._settings.init();
                    this._developer.init();
                    this._designer.init();       
                    this._options_context.init();
                    dbworksutil.message.alert_setup();
                });                                    
            });
        }

        setup(): void {
            //jQuery('.dropdown-toggle').dropdown();
        }

        events(): void {
            var $parent = jQuery('#master_designer_container');

            $parent.on('click', '.new_file_option', (e) => {
                //var menu_action = jQuery(e.target).data('type');
                //this.file_new(menu_action);
                jQuery('#module_modal').modal('show');
            });

            $parent.on('click', '.open_file_option', (e) => {
                //this.modal_scan.open();
                var config = +jQuery(e.currentTarget).data('config');

                jQuery('#scan_modal').data('config', config).modal('show');
            });
            
            $parent.on('click', '.file_save', (e) => {
                this.api_save_module().done((data) => {
                    dbworksutil.message.message_success('Save successful.');
                });
            });

            $parent.on('click', '.editable_file', (e) => {
                var folderIndex = jQuery(e.currentTarget).parent().parent().parent().data('index'),
                    fileIndex = jQuery(e.currentTarget).data('index'),
                    file = this.get_file_from_folder(folderIndex, fileIndex);
                jQuery('#active_editable_file').text(file.fileName);
                jQuery('#main_content_body').data('activefolderindex', folderIndex).data('activefileindex', fileIndex);
                this.toggle_view(file, 0);
            });          

            $parent.on('click', '#recents', (e) => {       
                this.bind_recents().done(() => {
                    jQuery('#recents_modal').modal('show');
                });                
            });

            $parent.on('click', '.clear_recent_options', (e) => {

                localStorage.setItem("recent_modules", null);
                this.bind_recents();
                
            });

            $parent.on('click', '.expand_module_folder', (e) => {                            
                var index = +jQuery(e.currentTarget).parent().parent().parent().data('index'), folder = this.open_folders[index];
                this.bind_filenav_2(folder, index);
                
            });

            $parent.on('click', '.edit_module_folder', (e) => {

                alert('edit not implemented');

            });

            $parent.on('click', '.delete_module_folder', (e) => {

                var c = confirm('Are you sure you want to hard delete this folder?');

                if (c) {
                    alert('delete not implemented');
                }
                
            });

            $parent.on('click', '#expand_collapsed_folders', (e) => {   
                this.bind_filenav_1();
            });

            $parent.on('click', '.expand_collapsed_folder', (e) => {
                var folderIndex = jQuery(e.currentTarget).data('index'), folder = this.open_folders[folderIndex];
                this.bind_filenav_2(folder, folderIndex);
            });

            $parent.on('click', '.close_full_module', (e) => {                
                var path = jQuery(e.target).data('path'),
                    name = jQuery(e.target).data('name'),
                    index = jQuery(e.currentTarget).parent().parent().parent().data('index'),
                    folder = this.open_folders[index];
                this.remove_folder_from_folders(index);
                this.bind_file_nav(folder);               
            });

            $parent.on('click', '.dev_options', (e) => {                
                var view = +jQuery(e.currentTarget).data('view'),
                    folderIndex = jQuery('#main_content_body').data('activefolderindex'),
                    fileIndex = jQuery('#main_content_body').data('activefileindex'),
                    file = this.get_file_from_folder(folderIndex, fileIndex);
                jQuery('#main_content_body').data('panel', view);
                this.toggle_view(file, view);                
            });

            $parent.on('click', '#design', (e) => {

                jQuery("#design_panel").slideToggle('fast');
                
            });

            $parent.on('change', '#render_style_select', (e) => {
                jQuery('html').removeClass('theme-default theme-classic theme-material').
                    addClass(jQuery(e.currentTarget).val());
            });

            $parent.on('click', '#editor_settings', (e) => {
             
                jQuery('#settings_modal').modal('show');

            });

            $parent.on('click', '.structure_close', (e) => {
                jQuery('#new_structure_container').hide();
            });

            $parent.on('click', '.open_recent_option', (e) => {
                var path = jQuery(e.target).data('path'), name = jQuery(e.target).data('name'), menuPath = jQuery(e.target).data('menupath');
                if (!this.validate_recent_file_is_open(path, name)) {
                    this.api_open_module(path, name, menuPath).done((_folder) => {
                        switch (this.nav_mode) {
                            case 1:
                                this.bind_filenav_1();
                                break;
                            case 2:
                                this.bind_filenav_2(_folder, null);
                                break;
                            default:

                        }
                    });
                } else {
                    dbworksutil.message.message_default(name + ' is already open.');
                }
                                
            });
  
        }

        bind(): JQueryPromise<string> {
            var source = document.getElementById('designer_main_template').innerHTML;
            return dbworksutil.handlebars.render(source, { version: product.version }, jQuery('#master_designer_container')).done(() => {
                this.setup();
            });
        }

        bind_file_nav(folder: editor.models.folder): JQueryPromise<string> {
            var $promise = jQuery.Deferred<any>();
            switch (this.nav_mode) {
                case 1:
                    this.bind_filenav_1().done((data) => {
                        $promise.resolveWith(data);
                    });
                    break;
                case 2:
                    this.bind_filenav_2(folder, null).done((data) => {
                        $promise.resolveWith(data);
                    });
                    break;
                default:
            }
            return $promise;
        }

        bind_filenav_1(): JQueryPromise<string> {
            this.nav_mode = 1;
            var source = document.getElementById('designer_filenav_mode_1_template').innerHTML;
            return dbworksutil.handlebars.render(source, { folders: this.open_folders }, jQuery('#file_management_container'));
            
        }

        bind_filenav_2(folder: editor.models.folder, folderIndex: number): JQueryPromise<string> {
            this.nav_mode = 2;
            var source = document.getElementById('designer_filenav_mode_2_template').innerHTML;
            return dbworksutil.handlebars.render(source, { files: folder.files, index: folderIndex, folders: this.open_folders }, jQuery('#file_management_container'));
        }        

        bind_recents(): JQueryPromise<string> {
            this.get_recently_used_module_data();
            var source = document.getElementById('designer_recents_template').innerHTML;
            return dbworksutil.handlebars.render(source, { recent: this.recent_modules }, jQuery('#recents_modal .modal-body'));
        }

        validate_module(): boolean {
            var isValid = false;
            if (jQuery('#module_name').val() != null && jQuery('#module_name').val() != '') isValid = true;
            return isValid;
        }

        evaluate_form_javascript(): void {
            
            this.files.forEach((_f) => {

                if (_f.fileName.indexOf('Grid') == -1 && _f.ext == 'ts') {

                    var convjs = editor.controllers.ts_to_js_compiler.compile(_f.fileContents);
                    geval(convjs.outputText);
                }

            });

        }

        destory_form_javascript(): void {



        }

        render(file: editor.models.file): void {            
            var content = file.fileContents, $root = jQuery('<div>' + content + '</div>'), $fwControls = $root.find('.fwcontrol');
            if (file.ext === 'htm') {
                jQuery('#preview_view').empty();
                jQuery('#preview_view').html(this._developer.code_mirror.getValue());
                var $controls = jQuery('#preview_view').find('.fwcontrol');
                FwControl.renderRuntimeControls($controls);
            } else {
                jQuery('#preview_view').html('<strong>File type ' + file.ext.toUpperCase() + ' not supported in preview.</strong>');
            }            

        }

        toggle_view(file: editor.models.file, view: number): void {    
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
                    //this._process_fwgrid.process_2(jQuery('<div>' + file.fileContents + '</div>'));
                    this._process_fwgrid.process();
                    //this._process_fwgrid.process(file);
                    break;
                default:
                    dbworksutil.message.message_warning('Oops, view not found.');
            }
        }

        file_new(action: string): void {

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

        }

        get_changed_files_only(): editor.models.file[] {
            var changed_files: editor.models.file[] = new Array<editor.models.file>();            
            this.files.forEach((_file) => {
                if (_file.hasChanged) changed_files.push(new editor.models.file(_file));
            });
            return changed_files;
        }

        get_changed_files_from_open_folders(): editor.models.file[] {
            var changed_files: editor.models.file[] = new Array<editor.models.file>();
            this.open_folders.forEach((_folder) => {
                _folder.files.forEach((_file) => {
                    if (_file.hasChanged) changed_files.push(new editor.models.file(_file));
                });
            });
            return changed_files;
        }

        get_file(index?: number): editor.models.file {
            if (index == undefined) {
                index = +jQuery('#main_content_body').data('activefileindex');
            }

            if (index == null) {
                dbworksutil.message.message({ message: 'activefileindex data attribute has a null value.', alertType: 'warning' });
            }
            return this.files[index];
        }

        get_file_from_folder(folderIndex: number, fileIndex: number): editor.models.file {
            return this.open_folders[folderIndex].files[fileIndex];            
        }

        remove_folder_from_folders(index: number): void {
            for (var i = 0; i < this.open_folders.length; i++) {
                if (i == index) {
                    this.open_folders.splice(i, 1);
                    break;
                }
            }
        }

        get_recently_used_module_data(): void {
            if (typeof (Storage) !== "undefined") {
                this.recent_modules = JSON.parse(localStorage.getItem("recent_modules"));        
                if (this.recent_modules == null || undefined) {
                    this.recent_modules = [];
                }
            } else {
                // Sorry! No Web Storage support..
            }
        }

        store_recently_used_module_data(key: string, value: string): void {
            // only save top 5            
            if (typeof (Storage) !== "undefined") {
                this.get_recently_used_module_data();
                
                if (this.recent_modules.length == 5) {
                    this.recent_modules.pop();
                    this.recent_modules.unshift({ key: key, value: value });
                } else {
                    this.recent_modules.push({ key: key, value: value });
                }

                localStorage.setItem("recent_modules", JSON.stringify(this.recent_modules));

            } else {
                // Sorry! No Web Storage support..
            }
        }

        validate_recent_file_is_open(path: string, name: string): boolean {
            var isOpen = false;
            for (var i = 0; i < this.open_folders.length; i++) {

                if (this.open_folders[i].path == path && this.open_folders[i].folderName == name) {
                    isOpen = true;
                    break;
                }

            }
            return isOpen;
        }

        remove_recently_used_module_data(path: string, name: string): void {
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

        }

        api_open_module(path: string, name: string, menuPath: string): JQueryPromise<editor.models.folder> {
            var url = this.api_v1 + 'openmodule?modulePath=' + path + '&moduleName=' + name + '&menuPath=' + menuPath;
            return dbworksutil.rest.GET<editor.models.folder>(url, false, {
                message: "We can't find that module. We'll remove it from your Recents."
            }).done((_folder) => {
                this.open_folders.push(new editor.models.folder(_folder));
                this.files = _folder.files;
                console.log(this.files);
            });
        }

        api_save_module(): JQueryPromise<any> {  
            var url = this.api_v1 + 'savestructure', changedFiles = this.get_changed_files_from_open_folders(), $promise = jQuery.Deferred();
            //this.files.forEach(f => f.hasChanged = false);
            if (changedFiles.length != 0) {

                this.open_folders.forEach(_folder => _folder.files.forEach(_file => _file.hasChanged = false));
                return $promise.resolve(dbworksutil.rest.POST(url, changedFiles, { message: "An error occured when you tried to save. Try again." }));

            } else {
                dbworksutil.message.message_informational('Nothing to save.');
                return $promise.reject();

            }
            

        }

    }

}
