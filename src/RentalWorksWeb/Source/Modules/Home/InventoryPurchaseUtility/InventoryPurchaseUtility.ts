routes.push({ pattern: /^module\/inventorypurchaseutility$/, action: function (match: RegExpExecArray) { return InventoryPurchaseUtilityController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class InventoryPurchaseUtility {
    Module: string = 'InventoryPurchaseUtility';
    caption: string = Constants.Modules.Home.InventoryPurchaseUtility.caption;
    nav: string = Constants.Modules.Home.InventoryPurchaseUtility.nav;
    id: string = Constants.Modules.Home.InventoryPurchaseUtility.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        var $form = this.openForm('EDIT');
        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('[data-datafield="InventoryId"]').data('onchange', $tr => {
            const trackedBy = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            if (trackedBy === 'QUANTITY') {
                $form.find('.itemsgrid').hide();
            } else {
                $form.find('.itemsgrid').show();
            }

            //default unit cost
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        const $itemGrid = $form.find('div[data-grid="InventoryPurchaseItemGrid"]');
        const $itemGridControl = FwBrowse.loadGridFromTemplate('InventoryPurchaseItemGrid');
        $itemGrid.empty().append($itemGridControl);
        $itemGridControl.data('ondatabind', request => {
            request.uniqueids = {
                PickListId: FwFormField.getValueByDataField($form, 'PickListId')
            };
        });
        FwBrowse.init($itemGridControl);
        FwBrowse.renderRuntimeHtml($itemGridControl);
    }
    //----------------------------------------------------------------------------------------------
};
//----------------------------------------------------------------------------------------------
var InventoryPurchaseUtilityController = new InventoryPurchaseUtility();