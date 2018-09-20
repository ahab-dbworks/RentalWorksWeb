class Contract {
    Module: string = 'Contract';
    apiurl: string = 'api/v1/contract';
    caption: string = 'Contract';
    ActiveView: string = 'ALL';

    getModuleScreen = () => {
        let screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

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
    }

    openBrowse = () => {
        let $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const location = JSON.parse(sessionStorage.getItem('location'));
        this.ActiveView = 'LocationId=' + location.locationid;

        $browse.data('ondatabind', request => {
            request.activeview = this.ActiveView;
        });

        FwBrowse.addLegend($browse, 'Unassigned Items', '#FF0000');
        FwBrowse.addLegend($browse, 'Pending Exchanges', '#FFFF00');
        FwBrowse.addLegend($browse, 'Migrated', '#8080FF');
        FwBrowse.addLegend($browse, 'Inactive Deal', '#C0C0C0');
        FwBrowse.addLegend($browse, 'Truck (No Charge)', '#FFFF00');
        FwBrowse.addLegend($browse, 'Adjusted Billing Date', '#FF8080');
        FwBrowse.addLegend($browse, 'Voided Items', '#00FFFF');

        return $browse;
    }

    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems = ($menuObject) => {
        let self = this;
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false);
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        $allLocations.on('click', function () {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=ALL';
            FwBrowse.search($browse);
        });
        $userLocation.on('click', function () {
            let $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=' + location.locationid;
            FwBrowse.search($browse);
        });
        const viewLocation = [];
        viewLocation.push($userLocation);
        viewLocation.push($allLocations);
        let $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContractId"] input').val(uniqueids.ContractId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    renderGrids($form) {
        var $contractSummaryGrid;
        var $contractSummaryGridControl;
        $contractSummaryGrid = $form.find('div[data-grid="ContractSummaryGrid"]');
        $contractSummaryGridControl = jQuery(jQuery('#tmpl-grids-ContractSummaryGridBrowse').html());
        $contractSummaryGrid.empty().append($contractSummaryGridControl);
        $contractSummaryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            };
        });
        FwBrowse.init($contractSummaryGridControl);
        FwBrowse.renderRuntimeHtml($contractSummaryGridControl);

        var $contractRentalDetailGrid;
        var $contractRentalDetailGridControl;
        $contractRentalDetailGrid = $form.find('.rentaldetailgrid div[data-grid="ContractDetailGrid"]');
        $contractRentalDetailGridControl = jQuery(jQuery('#tmpl-grids-ContractDetailGridBrowse').html());
        $contractRentalDetailGrid.empty().append($contractRentalDetailGridControl);
        $contractRentalDetailGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                RecType: "R"
            };
        });
        FwBrowse.init($contractRentalDetailGridControl);
        FwBrowse.renderRuntimeHtml($contractRentalDetailGridControl);

        var $contractSalesDetailGrid;
        var $contractSalesDetailGridControl;
        $contractSalesDetailGrid = $form.find('.salesdetailgrid div[data-grid="ContractDetailGrid"]');
        $contractSalesDetailGridControl = jQuery(jQuery('#tmpl-grids-ContractDetailGridBrowse').html());
        $contractSalesDetailGrid.empty().append($contractSalesDetailGridControl);
        $contractSalesDetailGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                RecType: "S"
            };
        });
        FwBrowse.init($contractSalesDetailGridControl);
        FwBrowse.renderRuntimeHtml($contractSalesDetailGridControl);

        var $contractExchangeItemGrid;
        var $contractExchangeItemGridControl;
        $contractExchangeItemGrid = $form.find('div[data-grid="ContractExchangeItemGrid"]');
        $contractExchangeItemGridControl = jQuery(jQuery('#tmpl-grids-ContractExchangeItemGridBrowse').html());
        $contractExchangeItemGrid.empty().append($contractExchangeItemGridControl);
        $contractExchangeItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                RecType: "S"
            };
        });
        FwBrowse.init($contractExchangeItemGridControl);
        FwBrowse.renderRuntimeHtml($contractExchangeItemGridControl);
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="ContractId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $contractSummaryGrid;
        $contractSummaryGrid = $form.find('[data-name="ContractSummaryGrid"]');
        FwBrowse.search($contractSummaryGrid);

        var $contractRentalGrid;
        $contractRentalGrid = $form.find('.rentaldetailgrid [data-name="ContractDetailGrid"]');
        FwBrowse.search($contractRentalGrid);

        var $contractSalesGrid;
        $contractSalesGrid = $form.find('.salesdetailgrid [data-name="ContractDetailGrid"]');
        FwBrowse.search($contractSalesGrid);

        var $contractExchangeItemGrid;
        $contractExchangeItemGrid = $form.find('[data-name="ContractExchangeItemGrid"]');
        FwBrowse.search($contractExchangeItemGrid);

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

        let showSales = $form.find('[data-datafield="Sales"] input').val(),
            showRental = $form.find('[data-datafield="Rental"] input').val(),
            showExchange = $form.find('[data-datafield="Exchange"] input').val();

        if (showSales === 'false') {
            $form.find('[data-type="tab"][data-caption="Sales Detail"]').hide();
        }

        if (showRental === 'false') {
            $form.find('[data-type="tab"][data-caption="Rental Detail"]').hide();
        }

        if (showExchange === 'true') {
            $form.find('.summary').hide();
        } else {
            $form.find('.exchange').hide();
        }
    }
}

var ContractController = new Contract();