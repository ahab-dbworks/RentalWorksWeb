routes.push({ pattern: /^module\/picklist$/, action: function (match: RegExpExecArray) { return PickListController.getModuleScreen(); } });

class PickList {
    Module: string = 'PickList';
    apiurl: string = 'api/v1/picklist';
    caption: string = Constants.Modules.Home.PickList.caption;
	nav: string = Constants.Modules.Home.PickList.nav;
	id: string = Constants.Modules.Home.PickList.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, "Pick List", false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const self = this;
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //$form.find('.printpicklist').on('click', function () {
        //    var $form, $report, pickListNumber, pickListId;
        //    try {
        //        $form = jQuery(this).closest('.fwform');
        //        pickListNumber = $form.find('div.fwformfield[data-datafield="PickListNumber"] input').val();
        //        pickListId = $form.find('div.fwformfield[data-datafield="PickListId"] input').val();
        //        $report = RwPickListReportController.openForm();
        //        FwModule.openSubModuleTab($form, $report);
        //        $report.find('div.fwformfield[data-datafield="PickListId"] input').val(pickListId);
        //        $report.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val(pickListNumber);
        //        jQuery('.tab.submodule.active').find('.caption').html('Print Pick List');
        //    }
        //    catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'PickListId', uniqueids.PickListId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $allWarehouses = FwMenu.generateDropDownViewBtn('ALL', false, "ALL");
        const $userWarehouse = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);

        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }

        let viewWarehouse = [];
        viewWarehouse.push($allWarehouses, $userWarehouse);
        FwMenu.addViewBtn($menuObject, 'Warehouse', viewWarehouse, true, "WarehouseId");
        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        var $pickListItemGrid = $form.find('div[data-grid="PickListItemGrid"]');
        var $pickListItemGridControl = jQuery(jQuery('#tmpl-grids-' + "PickListItemGrid" + 'Browse').html());
        $pickListItemGrid.empty().append($pickListItemGridControl);
        $pickListItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PickListId: FwFormField.getValueByDataField($form, 'PickListId')
            };
        });
        FwBrowse.init($pickListItemGridControl);
        FwBrowse.renderRuntimeHtml($pickListItemGridControl);
    };
    //----------------------------------------------------------------------------------------------
    cancelPickList(pickListId, pickListNumber) {
        var $confirmation, $yes, $no, self;
        self = this;
        $confirmation = FwConfirmation.renderConfirmation('Cancel Pick List', '<div style="white-space:pre;">\n' +
            'Cancel Pick List ' + pickListNumber + ' ?</div>');
        $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        $no = FwConfirmation.addButton($confirmation, 'No');
        $yes.on('click', function () {
            FwAppData.apiMethod(true, 'DELETE', 'api/v1/picklist/' + pickListId, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwNotification.renderNotification('SUCCESS', 'Pick List Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    //Close tab
                    var $tab = jQuery('div.tab.active');
                    var $control = jQuery(this).closest('.fwcontrol');
                    var $form = jQuery('#' + $tab.attr('data-tabpageid')).find('.fwform');
                    if (typeof $form !== 'undefined') {
                        FwModule.closeForm($form, $tab);
                    }
                    else {
                        var isactivetab = $tab.hasClass('active');
                        var $newactivetab = (($tab.next().length > 0) ? $tab.next() : $tab.prev());
                        FwTabs.removeTab($tab);
                        if (isactivetab) {
                            FwTabs.setActiveTab($control, $newactivetab);
                        }
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $confirmation);
        });
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        var $pickListItemGrid = $form.find('[data-name="PickListItemGrid"]');
        FwBrowse.search($pickListItemGrid);
    };
    //----------------------------------------------------------------------------------------------
}

//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents[Constants.Modules.Home.PickList.form.menuItems.CancelPickList.id] = function (event) {
    var $form, pickListId, pickListNumber;
    try {
        $form = jQuery(this).closest('.fwform');
        pickListId = $form.find('div.fwformfield[data-datafield="PickListId"] input').val();
        pickListNumber = $form.find('div.fwformfield[data-datafield="PickListNumber"] input').val();
        PickListController.cancelPickList(pickListId, pickListNumber);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
//Print Pick List
FwApplicationTree.clickEvents[Constants.Modules.Home.PickList.form.menuItems.PrintPickList.id] = function (event) {
    var $form, $report, pickListNumber, pickListId;
    try {
        $form = jQuery(this).closest('.fwform');
        pickListNumber = $form.find('div.fwformfield[data-datafield="PickListNumber"] input').val();
        pickListId = $form.find('div.fwformfield[data-datafield="PickListId"] input').val();
        $report = RwPickListReportController.openForm();
        FwModule.openSubModuleTab($form, $report);
        $report.find('div.fwformfield[data-datafield="PickListId"] input').val(pickListId);
        $report.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val(pickListNumber);
        jQuery('.tab.submodule.active').find('.caption').html('Print Pick List');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
//Browse Print Pick List
FwApplicationTree.clickEvents[Constants.Modules.Home.PickList.browse.menuItems.PrintPickList.id] = function (event) {
    var $browse, pickListId, pickListNumber;
    try {
        $browse = jQuery(this).closest('.fwbrowse');
        pickListNumber = $browse.find('.selected [data-browsedatafield="PickListNumber"]').attr('data-originalvalue');
        pickListId = $browse.find('.selected [data-browsedatafield="PickListId"]').attr('data-originalvalue');
        if (pickListId != null) {
            $browse = RwPickListReportController.openForm();
            FwModule.openModuleTab($browse, 'Pick List Report for ' + pickListNumber, true, 'REPORT', true);
            $browse.find('div.fwformfield[data-datafield="PickListId"] input').val(pickListId);
            $browse.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val(pickListNumber);
        } else {
            FwNotification.renderNotification('WARNING', 'Select a Picklist to print.');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------

var PickListController = new PickList();