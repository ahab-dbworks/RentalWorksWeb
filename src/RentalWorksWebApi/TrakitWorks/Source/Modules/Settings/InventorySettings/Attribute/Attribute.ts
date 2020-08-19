class Attribute {
    Module: string = 'Attribute';
    apiurl: string = 'api/v1/attribute';
    caption: string = Constants.Modules.Settings.children.InventorySettings.children.Attribute.caption;
    nav:     string = Constants.Modules.Settings.children.InventorySettings.children.Attribute.nav;
    id:      string = Constants.Modules.Settings.children.InventorySettings.children.Attribute.id;
    //---------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
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
    renderGrids($form: any) {
        //const $attributeValueGrid = $form.find('div[data-grid="AttributeValueGrid"]');
        //const $attributeValueGridControl = FwBrowse.loadGridFromTemplate('AttributeValueGrid');
        //$attributeValueGrid.empty().append($attributeValueGridControl);
        //$attributeValueGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        AttributeId: FwFormField.getValueByDataField($form, 'AttributeId')
        //    };
        //});
        //$attributeValueGridControl.data('beforesave', request => {
        //    request.AttributeId = FwFormField.getValueByDataField($form, 'AttributeId')
        //});
        //FwBrowse.init($attributeValueGridControl);
        //FwBrowse.renderRuntimeHtml($attributeValueGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'AttributeValueGrid',
            gridSecurityId: '2uvN8jERScu',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    AttributeId: FwFormField.getValueByDataField($form, 'AttributeId'),
                };
            },
            beforeSave: (request: any) => {
                request.AttributeId = FwFormField.getValueByDataField($form, 'AttributeId');
            },
        });
    }


    //---------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $attributeValueGrid = $form.find('[data-name="AttributeValueGrid"]');
        FwBrowse.search($attributeValueGrid);
    }
}
//---------------------------------------------------------------------------------
var AttributeController = new Attribute();