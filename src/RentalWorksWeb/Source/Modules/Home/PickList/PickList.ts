declare var FwModule: any;
declare var FwBrowse: any;
declare var FwServices: any;
declare var FwMenu: any;
declare var Mustache: any;
declare var FwFunc: any;
declare var FwNotification: any;

class PickList {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'PickList';
        this.apiurl = 'api/v1/picklist';
    }

    getModuleScreen() {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, "PickList", false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var self = this;
        var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);
  
        return $browse;
       
    }

    openForm(mode: string) {
        var $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'PickListId', uniqueids.PickListId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: JQuery, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    renderGrids($form: JQuery) {
        var $pickListItemGrid: JQuery = $form.find('div[data-grid="PickListItemGrid"]');
        var $pickListItemGridControl: JQuery = jQuery(jQuery('#tmpl-grids-' + "PickListItemGrid" + 'Browse').html());
        $pickListItemGrid.empty().append($pickListItemGridControl);
        $pickListItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PickListId: FwFormField.getValueByDataField($form, 'PickListId')
            };
        })
        FwBrowse.init($pickListItemGridControl);
        FwBrowse.renderRuntimeHtml($pickListItemGridControl);
    
    }

    afterLoad($form: JQuery) {
        var $pickListItemGrid: JQuery = $form.find('[data-name="' + "PickListItemGrid" + '"]');
        FwBrowse.search($pickListItemGrid);
    }
}

(<any>window).PickListController = new PickList();