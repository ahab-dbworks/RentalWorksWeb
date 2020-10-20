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
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        this.events($form);

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
    afterLoad($form: any) {
        const $attributeValueGrid = $form.find('[data-name="AttributeValueGrid"]');
        FwBrowse.search($attributeValueGrid);

        const numericOnly = FwFormField.getValueByDataField($form, 'NumericOnly');
        if (numericOnly) {
            $form.find('.valuegrid').hide();
        }
        else {
            $form.find('.valuegrid').show();
        }
    }
    //-------------------------------------------------------------------------------------------------------
    events($form: any) {
        $form.find('[data-datafield="NumericOnly"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                $form.find('.valuegrid').hide();
            }
            else {
                $form.find('.valuegrid').show();
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------
    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'AttributeValueGrid',
            gridSecurityId: '2uvN8jERScu',
            moduleSecurityId: this.id,
            $form: $form,
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
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'InventoryTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
        }
    }
}
//---------------------------------------------------------------------------------
var AttributeController = new Attribute();