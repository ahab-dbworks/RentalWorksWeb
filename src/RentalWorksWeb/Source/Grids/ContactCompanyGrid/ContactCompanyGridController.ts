class ContactCompanyGrid {
    Module: string = 'ContactCompanyGrid';
    apiurl: string = 'api/v1/companycontact';
    ActiveView: string = 'ALL';

    generateRow($control, $generatedtr) {
        var $form;
        $form = $control.closest('.fwform');

        //if ($generatedtr[0].textContent === 'more_vert') {
        //    $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($control.closest('.fwform').find('div[data-datafield="OfficePhone"]').attr('data-originalvalue'));
        //    $generatedtr.find('.field[data-browsedatafield="Email"] input').val($control.closest('.fwform').find('div[data-datafield="Email"]').attr('data-originalvalue'));
        //}

        $generatedtr.find('div[data-browsedatafield="CompanyId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="CompanyType"]').text($tr.find('.field[data-browsedatafield="CompanyType"]').attr('data-originalvalue'))
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.value').val($form.find('div[data-datafield="ContactTitleId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.text').val($form.find('div[data-datafield="ContactTitleId"] input.fwformfield-text').val());
            $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($form.find('div[data-datafield="OfficePhone"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Email"] input').val($form.find('div[data-datafield="Email"]').attr('data-originalvalue'));
        });
    };
    //--------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ContactTitleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontacttitle`);
                break;
        }
    }
    //--------------------------------------------------------------------------------------------

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

var ContactCompanyGridController = new ContactCompanyGrid();
//----------------------------------------------------------------------------------------------