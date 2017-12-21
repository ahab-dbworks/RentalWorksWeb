namespace dbworks.editor.controllers {

    export class settings {
        _editor: editor.controllers.main;
        config: settings_config;

        constructor(editor_controller: editor.controllers.main) {
            this._editor = editor_controller;
            this.config = new settings_config();
        }

        init(): void {
            this.bind().done(() => {
                this.events();
                this.execute_saved_setting_process();
            });
        }        

        bind(): JQueryPromise<string> {
            var source = document.getElementById('designer_settings_template').innerHTML;
            return dbworksutil.handlebars.render(source, { settings: this.config }, jQuery('#settings_modal .modal-body'));
        }

        events(): void {
            var $parent = jQuery('#settings_modal');

            //jQuery('#tab_nav a').on('click', function (e) {
            //    e.preventDefault()
            //    jQuery(this).tab('show')
            //})

            $parent.on('click', '#save_settings', (e) => {
                this.save_settings_config();
                //this._editor.modal_settings.close();
                jQuery('#settings_modal').modal('hide');
            });

            $parent.on('click', '#designer_reset', (e) => {

                var c = confirm('Using the reset feature will refresh the page while attempting to persist your current work. This feature is beta and may not work correctly and may not save any of your work. Do you want to continue to reset the page?');

                if (c) {

                    //jQuery('#master_designer_container').empty();
                    //new dbworks.editor.controllers.main().init();
                } else {
                    alert('You just avoided a potential tragedy.');
                }

            });
        }

        update_settings(): void {
            //this.config.isAutoSave = jQuery('#autosave_setting').is(':checked');
            this.config.isAutoSaveOn = jQuery('#autosave_1').is(':checked');
            this.config.isAutoSaveOff = jQuery('#autosave_2').is(':checked');
            this.config.defaultModuleSavePath = jQuery('#default_save_module_path_setting').val();
            this.config.defaultMenuSavePath = jQuery('#default_save_menu_path_setting').val();
            this.execute_saved_setting_process();
        }

        execute_saved_setting_process(): void {
            var autoSaveTimer = null;
            if (this.config.isAutoSaveOn) {
                dbworksutil.message.message_informational('Auto save active.');
                autoSaveTimer = setInterval(() => {
                    var hasChanges = this._editor.get_changed_files_only().length > 0 ? true : false;
                    if (hasChanges) {
                        this._editor.api_save_module().done(() => {
                            //fourwallutil.message.message_success('Auto save successful.');
                        });
                    }                    
                }, 10000);                
            }

            if (this.config.isAutoSaveOff) {
                clearInterval(autoSaveTimer);
                dbworksutil.message.message_informational('Auto save deactivated.');
            }
        }

        get_settings_config(): void {
            if (typeof (Storage) !== "undefined") {
                var _settings = JSON.parse(localStorage.getItem("settings_config"));
                if (_settings != null) {
                    this.config = _settings;
                }
                
            } else {
                // Sorry! No Web Storage support..
            }
        }

        save_settings_config(): void {
            this.update_settings();
            if (typeof (Storage) !== "undefined") {
                var _settings = JSON.stringify(this.config);    
                localStorage.setItem('settings_config', _settings);
            } else {
                // Sorry! No Web Storage support..
            }
        }

    }

    export class settings_config {

        isAutoSaveOn: boolean;
        isAutoSaveOff: boolean;
        defaultModuleSavePath: string;
        defaultMenuSavePath: string;

        constructor() {
            this.isAutoSaveOn = false;
            this.isAutoSaveOff = true;
            this.defaultModuleSavePath = 'C:\\';
        }
    }

}