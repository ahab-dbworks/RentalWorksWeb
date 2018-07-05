/// <reference path="../deal/deal.ts" />
routes.push({ pattern: /^module\/purchaseorder$/, action: function (match: RegExpExecArray) { return PurchaseOrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/purchaseorder\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return PurchaseOrderController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class PurchaseOrder {
    Module: string = 'PurchaseOrder';
    apiurl: string = 'api/v1/PurchaseOrder';
    caption: string = 'PurchaseOrder';
    ActiveView: string = 'ALL';

    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);

            if (typeof filter !== 'undefined' && filter.datafield === 'agent') {
                filter.search = filter.search.split('%20').reverse().join(', ');
            }

            if (typeof filter !== 'undefined') {
                filter.datafield = filter.datafield.charAt(0).toUpperCase() + filter.datafield.slice(1);
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(filter.search);
            }

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
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);


        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'On Hold', '#EA300F');
        FwBrowse.addLegend($browse, 'No Charge', '#FF8040');
        FwBrowse.addLegend($browse, 'Late', '#FFB3D9');
        FwBrowse.addLegend($browse, 'Foreign Currency', '#95FFCA');
        FwBrowse.addLegend($browse, 'Multi-Warehouse', '#D6E180');
        FwBrowse.addLegend($browse, 'Repair', '#5EAEAE');
        FwBrowse.addLegend($browse, 'L&D', '#400040');


        var department = JSON.parse(sessionStorage.getItem('department'));;
        var location = JSON.parse(sessionStorage.getItem('location'));;

        //FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, function onSuccess(response) {
        //    self.DefaultOrderType = response.DefaultOrderType;
        //    self.DefaultOrderTypeId = response.DefaultOrderTypeId;

        //}, null, null);

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        var self = this;
        var $all = FwMenu.generateDropDownViewBtn('All', true);
        var $new = FwMenu.generateDropDownViewBtn('New', false);
        var $open = FwMenu.generateDropDownViewBtn('Open', false);
        var $received = FwMenu.generateDropDownViewBtn('Received', false);
        var $complete = FwMenu.generateDropDownViewBtn('Complete', false);
        var $void = FwMenu.generateDropDownViewBtn('Void', false);
        var $closed = FwMenu.generateDropDownViewBtn('Closed', false);
        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.search($browse);
        });
        $new.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'NEW';
            FwBrowse.search($browse);
        });
        $open.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'OPEN';
            FwBrowse.search($browse);
        });
        $received.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'RECEIVED';
            FwBrowse.search($browse);
        });
        $complete.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'COMPLETE';
            FwBrowse.search($browse);
        });
        $void.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'VOID';
            FwBrowse.search($browse);
        });
        $closed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CLOSED';
            FwBrowse.search($browse);
        });
        var viewSubitems = [];
        viewSubitems.push($all);
        viewSubitems.push($new);
        viewSubitems.push($open);
        viewSubitems.push($received);
        viewSubitems.push($complete);
        viewSubitems.push($void);
        viewSubitems.push($closed);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        //Location Filter
        var location = JSON.parse(sessionStorage.getItem('location'));
        var $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false);
        var $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        $allLocations.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=ALL';
            FwBrowse.search($browse);
        });
        $userLocation.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=' + location.locationid;
            FwBrowse.search($browse);
        });
        var viewLocation = [];
        viewLocation.push($userLocation);
        viewLocation.push($all);
        var $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };

    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        var $form, $submodulePickListBrowse, $submoduleContractBrowse;
        var self = this;

        $form = jQuery(jQuery('#tmpl-modules-PurchaseOrderForm').html());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
           
            var today = FwFunc.getDate();
            var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            var office = JSON.parse(sessionStorage.getItem('location'));
            var department = JSON.parse(sessionStorage.getItem('department'));

            const usersid = sessionStorage.getItem('usersid');  // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            //FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            //FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);

            //FwFormField.setValueByDataField($form, 'PickDate', today);
            //FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            //FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);
            //FwFormField.setValueByDataField($form, 'BillingWeeks', '0');
            //FwFormField.setValueByDataField($form, 'BillingMonths', '0');

            //$form.find('div[data-datafield="PickTime"]').attr('data-required', false);
            //$form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
            //$form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);

            //FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            //FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            //FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

            //$form.find('div[data-datafield="PendingPo"] input').prop('checked', true);
            //$form.find('div[data-datafield="Rental"] input').prop('checked', true);
            //$form.find('div[data-datafield="Sales"] input').prop('checked', true);
            //$form.find('div[data-datafield="Miscellaneous"] input').prop('checked', true);
            //$form.find('div[data-datafield="Labor"] input').prop('checked', true);
            //FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
            //FwFormField.disable($form.find('[data-datafield="PoAmount"]'));

            //FwFormField.setValue($form, 'div[data-datafield="OrderTypeId"]', this.DefaultOrderTypeId, this.DefaultOrderType);

            //FwFormField.disable($form.find('.frame'));
            //$form.find(".frame .add-on").children().hide();
        };

        //$form.find('[data-datafield="BillToAddressDifferentFromIssuedToAddress"] .fwformfield-value').on('change', function () {
        //    var $this = jQuery(this);
        //    if ($this.prop('checked') === true) {
        //        FwFormField.enable($form.find('.differentaddress'));
        //    }
        //    else {
        //        FwFormField.disable($form.find('.differentaddress'));
        //    }
        //});

        //$form.find('div[data-datafield="OrderTypeId"]').data('onchange', function ($tr) {
        //    self.CombineActivity = $tr.find('.field[data-browsedatafield="CombineActivityTabs"]').attr('data-originalvalue');
        //    $form.find('[data-datafield="CombineActivity"] input').val(self.CombineActivity);

        //    const rentalTab = $form.find('[data-type="tab"][data-caption="Rental"]')
        //        , salesTab = $form.find('[data-type="tab"][data-caption="Sales"]')
        //        , miscTab = $form.find('[data-type="tab"][data-caption="Misc"]')
        //        , laborTab = $form.find('[data-type="tab"][data-caption="Labor"]');
        //    let combineActivity = $form.find('[data-datafield="CombineActivity"] input').val();
        //    if (combineActivity == "true") {
        //        $form.find('.notcombinedtab').hide();
        //        $form.find('.combinedtab').show();
        //    } else if (combineActivity == "false") {
        //        $form.find('.combinedtab').hide();
        //        $form.find('[data-datafield="Rental"] input').prop('checked') ? rentalTab.show() : rentalTab.hide();
        //        $form.find('[data-datafield="Sales"] input').prop('checked') ? salesTab.show() : salesTab.hide();
        //        $form.find('[data-datafield="Miscellaneous"] input').prop('checked') ? miscTab.show() : miscTab.hide();
        //        $form.find('[data-datafield="Labor"] input').prop('checked') ? laborTab.show() : laborTab.hide();
        //    }
        //});


        //$form.find('[data-datafield="NoCharge"] .fwformfield-value').on('change', function () {
        //    var $this = jQuery(this);

        //    if ($this.prop('checked') === true) {
        //        FwFormField.enable($form.find('[data-datafield="NoChargeReason"]'));
        //    } else {
        //        FwFormField.disable($form.find('[data-datafield="NoChargeReason"]'));
        //    }
        //});

        this.events($form);

        return $form;
    };
    
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PurchaseOrderId"] input').val(uniqueids.PurchaseOrderId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };

    //----------------------------------------------------------------------------------------------
    renderGrids($form) {};

    //----------------------------------------------------------------------------------------------
    loadAudit($form) {
        var uniqueid = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        FwModule.loadAudit($form, uniqueid);
    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form) {};

    //----------------------------------------------------------------------------------------------
    events($form: any) {};

    //----------------------------------------------------------------------------------------------
    afterSave($form) {};
};

//----------------------------------------------------------------------------------------------
var PurchaseOrderController = new PurchaseOrder();