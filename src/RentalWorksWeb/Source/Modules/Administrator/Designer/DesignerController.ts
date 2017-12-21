declare var FwModule: any;
declare var FwBrowse: any;
declare var FwHybridMasterController: any;
declare var FwAppData: any;
//declare var applicationConfig: any;
declare var FwControl: any;
declare var FwSettings: any;
declare var FwApplicationTree: any;

class DesignerStaging {
    Module: string;

    constructor() {
        this.Module = 'Designer';
    }

    loadDesigner(argh1, argh2) {
        var screen: any = {};

        var $designerRWTemplate = jQuery(jQuery('#tmpl-modules-Designer').html());

            screen.$view = $designerRWTemplate;

            screen.load = () => {
                this.prepareDesigner();
            };
            screen.unload = () => {
                jQuery('#master_designer_container').remove();
            };

            
        return screen;
    }

    prepareDesigner(): void {
        new index().start();
    }

}

(window as any).DesignerController = new DesignerStaging();