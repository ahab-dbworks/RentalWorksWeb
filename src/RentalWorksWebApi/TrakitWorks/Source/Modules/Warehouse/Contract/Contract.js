routes.push({ pattern: /^module\/contract$/, action: function (match) { return ContractController.getModuleScreen(); } });
class Contract {
    constructor() {
        this.Module = 'Contract';
        this.apiurl = 'api/v1/contract';
        this.caption = Constants.Modules.Warehouse.children.Contract.caption;
        this.nav = Constants.Modules.Warehouse.children.Contract.nav;
        this.id = Constants.Modules.Warehouse.children.Contract.id;
        this.ActiveViewFields = {};
        this.getModuleScreen = () => {
            let screen, $browse;
            screen = {};
            screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
            $browse = this.openBrowse();
            screen.load = () => {
                FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            };
            screen.unload = () => {
                FwBrowse.screenunload($browse);
            };
            return screen;
        };
        this.openBrowse = () => {
            var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
            $browse = FwModule.openBrowse($browse);
            const self = this;
            switch (this.Module) {
                case 'Contract':
                    $browse.data('ondatabind', function (request) {
                        request.activeviewfields = self.ActiveViewFields;
                    });
                    FwBrowse.addLegend($browse, 'Unassigned Items', '#FF0000');
                    FwBrowse.addLegend($browse, 'Pending Exchanges', '#FFFF00');
                    FwBrowse.addLegend($browse, 'Migrated', '#8080FF');
                    FwBrowse.addLegend($browse, 'Inactive Deal', '#C0C0C0');
                    FwBrowse.addLegend($browse, 'Voided Items', '#00FFFF');
                    break;
                case 'Manifest':
                    $browse.data('ondatabind', function (request) {
                        request.activeviewfields = self.ActiveViewFields;
                        request.uniqueids.ContractType = 'MANIFEST';
                    });
                    FwBrowse.addLegend($browse, 'Voided Items', '#00FFFF');
                    break;
                case 'TransferReceipt':
                    $browse.data('ondatabind', function (request) {
                        request.activeviewfields = self.ActiveViewFields;
                        request.uniqueids.ContractType = 'RECEIPT';
                    });
                    FwBrowse.addLegend($browse, 'Voided Items', '#00FFFF');
                    break;
            }
            return $browse;
        };
    }
    addBrowseMenuItems(options) {
        options.hasNew = false;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);
        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }
        const viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
    addFormMenuItems(options) {
        FwMenu.addFormMenuButtons(options);
        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Order', '', (e) => {
            try {
                this.printContract(options.$form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    openForm(mode, parentModuleInfo) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    loadForm(uniqueids) {
        var $form;
        const module = (this.Module == 'Contract' ? 'Contract' : 'Manifest');
        $form = this.openForm('EDIT');
        $form.find(`div.fwformfield[data-datafield="${module}Id"] input`).val(uniqueids[`${module}Id`]);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    renderGrids($form) {
        const module = (this.Module == 'Contract' ? 'Contract' : 'Manifest');
        const $contractSummaryGrid = $form.find('div[data-grid="ContractSummaryGrid"]');
        ;
        const $contractSummaryGridControl = FwBrowse.loadGridFromTemplate('ContractSummaryGrid');
        $contractSummaryGrid.empty().append($contractSummaryGridControl);
        $contractSummaryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, `${module}Id`)
            };
        });
        FwBrowse.init($contractSummaryGridControl);
        FwBrowse.renderRuntimeHtml($contractSummaryGridControl);
        const $contractRentalDetailGrid = $form.find('.rentaldetailgrid div[data-grid="ContractDetailGrid"]');
        const $contractRentalDetailGridControl = FwBrowse.loadGridFromTemplate('ContractDetailGrid');
        $contractRentalDetailGrid.empty().append($contractRentalDetailGridControl);
        $contractRentalDetailGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, `${module}Id`),
                RecType: "R"
            };
        });
        FwBrowse.init($contractRentalDetailGridControl);
        FwBrowse.renderRuntimeHtml($contractRentalDetailGridControl);
        const $contractSalesDetailGrid = $form.find('.salesdetailgrid div[data-grid="ContractDetailGrid"]');
        const $contractSalesDetailGridControl = FwBrowse.loadGridFromTemplate('ContractDetailGrid');
        $contractSalesDetailGrid.empty().append($contractSalesDetailGridControl);
        $contractSalesDetailGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, `${module}Id`),
                RecType: "S"
            };
        });
        FwBrowse.init($contractSalesDetailGridControl);
        FwBrowse.renderRuntimeHtml($contractSalesDetailGridControl);
        const $contractExchangeItemGrid = $form.find('div[data-grid="ContractExchangeItemGrid"]');
        const $contractExchangeItemGridControl = FwBrowse.loadGridFromTemplate('ContractExchangeItemGrid');
        $contractExchangeItemGrid.empty().append($contractExchangeItemGridControl);
        $contractExchangeItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, `${module}Id`),
                RecType: "S"
            };
        });
        FwBrowse.init($contractExchangeItemGridControl);
        FwBrowse.renderRuntimeHtml($contractExchangeItemGridControl);
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
        const $contractSummaryGrid = $form.find('[data-name="ContractSummaryGrid"]');
        FwBrowse.search($contractSummaryGrid);
        const $contractRentalGrid = $form.find('.rentaldetailgrid [data-name="ContractDetailGrid"]');
        FwBrowse.search($contractRentalGrid);
        const $contractSalesGrid = $form.find('.salesdetailgrid [data-name="ContractDetailGrid"]');
        FwBrowse.search($contractSalesGrid);
        const $contractExchangeItemGrid = $form.find('[data-name="ContractExchangeItemGrid"]');
        FwBrowse.search($contractExchangeItemGrid);
        if (this.Module == 'Contract') {
            var type = FwFormField.getValueByDataField($form, 'ContractType');
            var $billing = $form.find('[data-datafield="BillingDate"] .fwformfield-caption');
            switch (type) {
                case 'RECEIVE':
                    $billing.html('Billing Start');
                    break;
                case 'OUT':
                    $billing.html('Billing Start');
                    break;
                case 'IN':
                    $billing.html('Billing Stop');
                    break;
                case 'RETURN':
                    $billing.html('Billing Stop');
                    break;
                case 'LOST':
                    $billing.html('Billing Stop');
                    break;
                default:
                    $billing.html('Billing Date');
                    break;
            }
        }
        const showSales = FwFormField.getValueByDataField($form, 'Sales');
        const showRental = FwFormField.getValueByDataField($form, 'Rental');
        const showExchange = FwFormField.getValueByDataField($form, 'Exchange');
        if (showSales === 'false') {
            $form.find('[data-type="tab"][data-caption="Sales Detail"]').hide();
        }
        if (showRental === 'false') {
            $form.find('[data-type="tab"][data-caption="Rental Detail"]').hide();
        }
        if (showExchange === 'true') {
            $form.find('.summary').hide();
        }
        else {
            $form.find('.exchange').hide();
        }
    }
    printContract($form) {
        try {
            const $report = OutContractReportController.openForm();
            FwModule.openSubModuleTab($form, $report);
            const module = (this.Module == 'Contract' ? 'Contract' : 'Manifest');
            const contractId = $form.find(`div.fwformfield[data-datafield="${module}Id"] input`).val();
            $report.find(`div.fwformfield[data-datafield="${module}Id"] input`).val(contractId);
            const contractNumber = $form.find(`div.fwformfield[data-datafield="${module}Number"] input`).val();
            $report.find(`div.fwformfield[data-datafield="${module}Id"] .fwformfield-text`).val(contractNumber);
            jQuery('.tab.submodule.active').find('.caption').html(`Print ${module}`);
            const printTab = jQuery('.tab.submodule.active');
            printTab.find('.caption').html(`Print ${module}`);
            const recordTitle = jQuery('.tabs .active[data-tabtype="FORM"] .caption').text();
            printTab.attr('data-caption', `${module} ${recordTitle}`);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    voidContract($form) {
        try {
            const request = {};
            request.ContractId = FwFormField.getValueByDataField($form, 'ContractId');
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/voidcontract`, request, FwServices.defaultTimeout, response => {
                let $confirmation = FwConfirmation.renderConfirmation('Error', response.msg);
                FwConfirmation.addButton($confirmation, 'OK', true);
            }, ex => FwFunc.showError(ex), $form);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
}
var ContractController = new Contract();
//# sourceMappingURL=Contract.js.map