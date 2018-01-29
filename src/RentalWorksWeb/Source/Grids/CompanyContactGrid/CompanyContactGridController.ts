class CompanyContactGrid {
    Module: string;
    apiurl: string;
    ActiveView: string;

    constructor() {
        this.Module = 'CompanyContactGrid';
        this.apiurl = 'api/v1/companycontact';
        this.ActiveView = 'ALL';
    }

    loadRelatedValidationFields(validationName, $valuefield, $tr) {
        var $form;
        $form = $valuefield.closest('.fwform');

        if (validationName === 'ContactValidation') {
            $form.find('.editrow .email input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        }

        if (validationName === 'CompanyValidation') {
            $form.find('.editrow .type input').val($tr.find('.field[data-browsedatafield="CompanyType"]').attr('data-originalvalue'));
        }

    }; 

    //addGridMenuItems($menuObject) {

    //    var self = this;
    //    var $all: JQuery = FwMenu.generateDropDownViewBtn('All Contacts', true);
    //    var $lead: JQuery = FwMenu.generateDropDownViewBtn('Lead Contacts', false);
    //    var $prospect: JQuery = FwMenu.generateDropDownViewBtn('Prospect Contacts', false);
    //    var $customer: JQuery = FwMenu.generateDropDownViewBtn('Customer Contacts', false);
    //    var $deal: JQuery = FwMenu.generateDropDownViewBtn('Deal Contacts', false);
    //    var $vendor: JQuery = FwMenu.generateDropDownViewBtn('Vendor Contacts', false);

    //    $all.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'ALL';
    //        FwBrowse.databind($browse);
    //    });
    //    $lead.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'LEAD';
    //        FwBrowse.databind($browse);
    //    });
    //    $prospect.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'PROSPECT';
    //        FwBrowse.databind($browse);
    //    });
    //    $customer.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'CUSTOMER';
    //        FwBrowse.databind($browse);
    //    });
    //    $deal.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'DEAL';
    //        FwBrowse.databind($browse);
    //    });
    //    $vendor.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'VENDOR';
    //        FwBrowse.databind($browse);
    //    });

    //    FwMenu.addVerticleSeparator($menuObject);

    //    var viewSubitems: Array<JQuery> = [];
    //    viewSubitems.push($all);
    //    viewSubitems.push($lead);
    //    viewSubitems.push($prospect);
    //    viewSubitems.push($customer);
    //    viewSubitems.push($deal);
    //    viewSubitems.push($vendor);

    //    var $view;
    //    $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

    //    return $menuObject;
    //};
}

(<any>window).CompanyContactGridController = new CompanyContactGrid();
//----------------------------------------------------------------------------------------------