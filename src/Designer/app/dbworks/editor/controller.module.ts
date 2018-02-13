namespace dbworks.editor.controllers {

    export class module {

        _editor: editor.controllers.main;

        constructor(editor_controller: editor.controllers.main) {
            this._editor = editor_controller;
        }

        init(): void {            
            this.bind().done(() => {                
                this.setup();
                this.events();                
            });
        }

        setup(): void {
            jQuery('#structure_input_fields').show();
        }

        bind(): JQueryPromise<string> {
            var source = document.getElementById('designer_module_template').innerHTML;
            return dbworksutil.handlebars.render(source, { settings: this._editor._settings.config }, jQuery('#module_modal .modal-body'));            
        }

        events(): void {
            var $parent = jQuery('#module_modal');

            $parent.on('click', '#generate_module', (e) => {
                if (this._editor.validate_module()) {
                    this.create_module();
                } else {
                    alert('Fill in missing information.');
                }
            });

            $parent.on('show.bs.modal', (e) => {
                this.bind().done(() => {
                    jQuery('#structure_input_fields').show();
                });
            });
        }

        create_module(): void {
            jQuery('#structure_loading').show();
            this.api_create_module().always(() => {
                jQuery('.structure_response_container').hide();
            }).done((_folder) => {              
                this._editor.bind_file_nav(_folder).done(() => {
                    this._editor.bind_recents().done(() => {
                        jQuery('#structure_success, .structure_response_container').show();
                        //jQuery('ul.tabs').tabs();     
                        jQuery('#module_modal').modal('hide');
                        this._editor._developer.clear_editor();
                        dbworksutil.message.message_success('New module generated successfully.');
                    });                    
                });          
            }).fail((xhr) => {
                jQuery('#structure_error').show();
            });
        }

        api_create_module(): JQueryPromise<any> {
            var modulePath = jQuery('#module_path').val() == null ? 'C:\\' : jQuery('#module_path').val();
            var url = this._editor.api_v1 + 'createmodule?moduleName=' + jQuery('#module_name').val() + '&modulePath=' + modulePath + '&menuPath=' + jQuery('#menu_path').val();
            return dbworksutil.rest.GET<editor.models.folder>(url).done((_folder) => {
                this._editor.open_folders.push(_folder);
                this._editor.files = _folder.files;
                this._editor.store_recently_used_module_data(_folder.folderName, _folder.path);
            });
        }
    }

}