routes.push({ pattern: /^module\/picklist$/, action: function (match: RegExpExecArray) { return PickListController.getModuleScreen(); } });

class PickList {
    Module: string = 'PickList';
    apiurl: string = 'api/v1/picklist';
    ActiveView: string = 'ALL';

    constructor() {
        var self = this;
        //Cancel Pick List confirmation button
    }

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
    openBrowse() {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    };
    openForm(mode, parentmoduleinfo?) {
        var $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //if (typeof parentmoduleinfo !== 'undefined') {
        //    $form.find('div[data-datafield="OrderId"]').find('input.fwformfield-value').val(parentmoduleinfo.OrderId).change();
        //    $form.find('div[data-datafield="OrderId"]').find('input.fwformfield-text').val(parentmoduleinfo.OrderId);
        //}

     
        //$form.find('div.submenubutton [data-securityid="4F196091-36A4-4AA9-BDB8-3689F01C08B3"]')
        //    .on('click', function () {
        //        var $browse, pickListId, pickListNumber, controller, pickListInfo;
        //        try {
        //            var $pickListReport = RwPickListReportController.openForm();
        //            $browse = jQuery(this).closest('.fwbrowse');
        //            controller = $browse.attr('data-controller');
        //            pickListInfo.Module = PickListController.Module;
        //            pickListInfo.pickListId = FwFormField.getValueByDataField($form, 'PickListId');
        //            pickListInfo.pickListNumber = FwFormField.getValueByDataField($form, 'PickListNumber');
        //            //var $pickListReport = RwPickListReportController.openForm();
        //            //var $pickListReport = FwModule.openModuleTab($browse, 'Pick List Report for ' + pickListNumber, true, 'REPORT', true);
        //            //var $pickListReport = window[controller]['openForm']('EDIT', pickListInfo);
        //            FwModule.openSubModuleTab($browse, $pickListReport);
        //            $browse.find('div.fwformfield[data-datafield="PickListId"] input').val(pickListId);
        //            $browse.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val(pickListNumber);

        //        }
        //        catch (ex) {
        //            FwFunc.showError(ex);
        //        }
        //    });

        return $form;
    };

    loadForm(uniqueids) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'PickListId', uniqueids.PickListId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    saveForm($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    };
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
    afterLoad($form) {
        var $pickListItemGrid = $form.find('[data-name="PickListItemGrid"]');
        FwBrowse.search($pickListItemGrid);
    };
}
//---------------------------------------------------------------------------------
var PickListController = new PickList();
//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{3BF7AEF3-BF52-4B8B-8324-910A92005B2B}'] = function (event) {
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
FwApplicationTree.clickEvents['{069BBE73-5B14-4F3E-A594-8699676D9B8E}'] = function (event) {
    var $form, pickListNumber, pickListId;
    try {
        $form = jQuery(this).closest('.fwform');
        pickListNumber = $form.find('div.fwformfield[data-datafield="PickListNumber"] input').val();
        pickListId = $form.find('div.fwformfield[data-datafield="PickListId"] input').val();
        $form = RwPickListReportController.openForm();
        FwModule.openModuleTab($form, 'Pick List Report for ' + pickListNumber, true, 'REPORT', true);
        $form.find('div.fwformfield[data-datafield="PickListId"] input').val(pickListId);
        $form.find('div.fwformfield[data-datafield="PickListId"] .fwformfield-text').val(pickListNumber);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
//Browse Print Pick List
FwApplicationTree.clickEvents['{51C78FB1-CD66-431F-A7BA-FFFB3BFDFD6C}'] = function (event) {
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
            throw new Error("Please select a Pick List to print");
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
