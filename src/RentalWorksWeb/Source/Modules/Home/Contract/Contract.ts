class Contract {
    Module: string;
    apiurl: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Contract';
        this.apiurl = 'api/v1/contract'; 
        this.ActiveView = 'ALL';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Contract', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);

        FwBrowse.addLegend($browse, 'Unassigned Items', '#FF0000');
        FwBrowse.addLegend($browse, 'Pending Exchanges', '#FFFF00');
        FwBrowse.addLegend($browse, 'Migrated', '#8080FF');
        FwBrowse.addLegend($browse, 'Inactive Deal', '#C0C0C0');
        FwBrowse.addLegend($browse, 'Truck (No Charge)', '#FFFF00');
        FwBrowse.addLegend($browse, 'Adjusted Billing Date', '#FF8080');
        FwBrowse.addLegend($browse, 'Voided Items', '#00FFFF');

        return $browse;
    }


    addBrowseMenuItems($menuObject: any) {
        var self = this;
        var $all: JQuery = FwMenu.generateDropDownViewBtn('All', true);
        var $rentalssales: JQuery = FwMenu.generateDropDownViewBtn('Rentals / Sales', true);
        var $repair: JQuery = FwMenu.generateDropDownViewBtn('Repair', false);
        var $sales: JQuery = FwMenu.generateDropDownViewBtn('Sales', false);

        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $rentalssales.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'RENTALSALES';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $repair.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'REPAIR';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $sales.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'SALES';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
      

        FwMenu.addVerticleSeparator($menuObject);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all);
        viewSubitems.push($rentalssales);
        viewSubitems.push($repair);
        viewSubitems.push($sales);

        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'Department', viewSubitems);

        return $menuObject;
    };



    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
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

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="ContractId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var type = FwFormField.getValueByDataField($form, 'ContractType');
        var $billing = $form.find('[data-datafield="BillingDate" .fwformfield-caption');

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
}

var ContractController = new Contract();