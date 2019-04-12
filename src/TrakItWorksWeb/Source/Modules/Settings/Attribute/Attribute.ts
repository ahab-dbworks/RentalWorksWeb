class Attribute {
    Module: string = 'Attribute';
    apiurl: string = 'api/v1/attribute';
    //---------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Attribute', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //---------------------------------------------------------------------------------
    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //---------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    }
    //---------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="AttributeId"] input').val(uniqueids.AttributeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //---------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="AttributeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    //---------------------------------------------------------------------------------
    renderGrids($form: any) {
        var $attributeValueGrid: any;
        var $attributeValueGridControl: any;

        // load AttributeValue Grid
        $attributeValueGrid = $form.find('div[data-grid="AttributeValueGrid"]');
        $attributeValueGridControl = jQuery(jQuery('#tmpl-grids-AttributeValueGridBrowse').html());
        $attributeValueGrid.empty().append($attributeValueGridControl);
        $attributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                AttributeId: $form.find('div.fwformfield[data-datafield="AttributeId"] input').val()
            };
        });
        $attributeValueGridControl.data('beforesave', function (request) {
            request.AttributeId = $form.find('div.fwformfield[data-datafield="AttributeId"] input').val()
        });
        FwBrowse.init($attributeValueGridControl);
        FwBrowse.renderRuntimeHtml($attributeValueGridControl);
    }
    //---------------------------------------------------------------------------------
    afterLoad($form: any) {
        var $attributeValueGrid: any;

        $attributeValueGrid = $form.find('[data-name="AttributeValueGrid"]');
        FwBrowse.search($attributeValueGrid);
    }
}
//---------------------------------------------------------------------------------
var AttributeController = new Attribute();