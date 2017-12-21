namespace dbworks.editor.controllers {

    export class options_context {

        _editor: editor.controllers.main;

        constructor(editor_controller: editor.controllers.main) {
            this._editor = editor_controller;
        }

        init() {
            this.bind().done(() => {
                this.events();
            });
        }

        bind(): JQueryPromise<string> {
            var source = document.getElementById('designer_options_context_template').innerHTML;
            return dbworksutil.handlebars.render(source, null, jQuery('#context_menu_options'));
        }

        events(): void {
            var $parent = jQuery('#main_master_body');

            $parent.on('click', '#context_options', (e) => {
                var displayPanel = +jQuery('#main_content_body').data('panel');
                this.toggle_contexts(displayPanel);
                jQuery('#context_menu_options').slideToggle('fast');
            });

            $parent.on('change', '.display_size_option', (e) => {

                var sizeType = +jQuery("input:radio[name ='displaysizeoption']:checked").val();
                this.toggle_display_size(sizeType);

            });
        }

        toggle_contexts(displayPanel: number): void {
            jQuery('.context_menu_nav').hide();
            switch (displayPanel) {
                case 0:
                    jQuery('#edit_options').show();
                    break;
                case 1:
                    jQuery('#preview_options').show();
                    //jQuery('#').show();
                    break;
                default:
                    jQuery('#default_options').show();
            }
        }

        toggle_display_size(size: number): void {
            var $preview = jQuery('#preview_view');
            switch (size) {
                case 1:
                    $preview.width('100%').height('100%');
                    break;
                case 2:
                    $preview.width('1024px');
                    break;
                case 3:
                    $preview.width('480px');
                    break;
                default:
                    $preview.width('100%').height('100%');
            }
        }
    }

}