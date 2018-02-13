namespace dbworks.editor.controllers {

    export class desginer {

        _editor: editor.controllers.main;

        constructor(editor_controller: editor.controllers.main) {
            this._editor = editor_controller;
        }

        init(): void {
            this.bind().done(() => {
                //this.setup()
                //this.events();
            });            
        }

        setup(): void {
            jQuery('.design_module_section').sortable({
                connectWith: ".body",
                dropOnEmpty: true,
                containment: "body",
                distance: 5,
                delay: 100,
                scroll: true,
                zIndex: 9999
            }).disableSelection();
            //jQuery('.design_module').draggable({ appendTo: 'body' });
        }
        
        events(): void {
            var $parent = jQuery('#slide-out');

            jQuery(".design_module_section").on("dragstart", '.design_module', (e, ui) => {
                jQuery(e.target).addClass('draggable_design_module');
                jQuery(e.currentTarget).css('z-index', 1000);
                jQuery(this).css('z-index', 1000);
                console.log(ui);
                console.log(e);

            });

            $parent.on('drag', '.design_module', (e) => {
                alert('hihihihihi');
                dbworksutil.debug.display_log(null, 'sorted!');

            });
            
        }

        bind(): JQueryPromise<string> {
            var source = document.getElementById('designer_designer_template').innerHTML;
            return dbworksutil.handlebars.render(source, {}, jQuery('#designer_modal'));
        }

    }

}