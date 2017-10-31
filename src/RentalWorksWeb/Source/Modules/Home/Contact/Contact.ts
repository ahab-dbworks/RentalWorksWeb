﻿declare var FwModule: any;
declare var FwBrowse: any;
declare var FwServices: any;
declare var FwMenu: any;
declare var Mustache: any;
declare var FwFunc: any;
declare var FwNotification: any;

class Contact {
    Module: string;
    apiurl: string;
    caption: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Contact';
        this.apiurl = 'api/v1/contact';
        this.caption = 'Contact';
        this.ActiveView = 'ALL';
    }

    getModuleScreen() {
        var me: Contact = this;
        var screen: any = {};
        screen.$view      = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, me.caption, false, 'BROWSE', true);
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
        var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', function(request) {
            request.activeview = this.ActiveView;
        });
        FwBrowse.addLegend($browse, 'Customer', '#ffff80');
        FwBrowse.addLegend($browse, 'Project',  '#03de3a');
        FwBrowse.addLegend($browse, 'Vendor',   '#20b7ff');

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        var $all: JQuery      = FwMenu.generateDropDownViewBtn('All Contacts', true);
        var $customer: JQuery = FwMenu.generateDropDownViewBtn('Customer Contacts', false);
        var $project: JQuery  = FwMenu.generateDropDownViewBtn('Project Contacts', false);
        var $vendor: JQuery   = FwMenu.generateDropDownViewBtn('Vendor Contacts', false);
        //var $signup   = FwMenu.generateDropDownViewBtn('View Sign Up', false);

        $all.on('click', function() {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            this.ActiveView = 'ALL';
            FwBrowse.databind($browse);
        });
        $customer.on('click', function() {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            this.ActiveView = 'CUSTOMER';
            FwBrowse.databind($browse);
        });
        $project.on('click', function() {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            this.ActiveView = 'DEAL';
            FwBrowse.databind($browse);
        });
        $vendor.on('click', function() {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            this.ActiveView = 'VENDOR';
            FwBrowse.databind($browse);
        });
        //$signup.on('click', function() {
        //    var $browse;
        //    $browse = jQuery(this).closest('.fwbrowse');
        //    this.ActiveView = 'SIGNUP';
        //    FwBrowse.databind($browse);
        //});

        FwMenu.addVerticleSeparator($menuObject);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all);
        viewSubitems.push($customer);
        viewSubitems.push($project);
        viewSubitems.push($vendor);
        //viewSubitems.push($signup);
        var $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var viewModel: any = {};
        var $form: JQuery = jQuery(Mustache.render(jQuery('#tmpl-modules-ContactForm').html(), viewModel));
        $form = FwModule.openForm($form, mode);

        if (mode == 'NEW') {
            FwFormField.setValueByDataField($form, 'ActiveDate', FwFunc.getDate());
        }

        $form
            .on('change', 'div[data-datafield="WebPassword"]', function() {
                throw 'not implented!';
                // need to create web service in new api
                var $this, request;
                $this = jQuery(this);
                request = {
                    method: 'CheckPasswordComplexity',
                    value:  FwFormField.getValueByDataField($form, 'WebPassword'),
                    first:  FwFormField.getValueByDataField($form, 'FirstName'),
                    last:   FwFormField.getValueByDataField($form, 'LastName')
                }
                FwModule.getData($form, request, function(response) {
                    try {
                        if (response.passwordcomplexity.error == true) {
                            $this.addClass('error');
                            FwNotification.renderNotification('ERROR', response.passwordcomplexity.errmsg);
                        } else {
                            $this.removeClass('error');
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, $form);
            })
            .on('change', 'div[data-datafield="WebAccess"]', function() {
                this.setFormProperties($form);
            })
            .on('change', 'div[data-datafield="Inactive"]', function() {
                var $this;
                $this = jQuery(this);
                this.setFormProperties($form);
                if (FwFormField.getValue2($this) === 'T') {
                    FwFormField.setValueByDataField($form, 'InactiveDate"]', FwFunc.getDate());
                } else {
                    FwFormField.setValueByDataField($form, 'InactiveDate', '');
                }
            })
            .on('change', 'div[data-datafield="PersonType"]', function() {
                this.setFormProperties($form);
            })
            .on('change', 'div[data-datafield="FirstName"], div[data-datafield="LastName"]', function() {
                throw 'not implemented';
                // need to add web service in api
                var fname, lname, persontype, $request;
                var fname      = FwFormField.getValueByDataField($form, 'FirstName');
                var lname      = FwFormField.getValueByDataField($form, 'LastName');
                var persontype = FwFormField.getValueByDataField($form, 'PersonType');
                if ((persontype === 'DRIVER') && (fname !== '') && (lname !== '')) {
                    var request = {
                        method: 'GetDriverInfo',
                        fname:  fname,
                        lname:  lname
                    }
                    FwModule.getData($form, request, function(response) {
                        try {
                            if (response.getdriverinfo != null) {
                                FwFormField.setValueByDataField($form, 'Address1',        response.getdriverinfo.add1);
                                FwFormField.setValueByDataField($form, 'Address2',        response.getdriverinfo.add2);
                                FwFormField.setValueByDataField($form, 'City',        response.getdriverinfo.city);
                                FwFormField.setValueByDataField($form, 'State',       response.getdriverinfo.state);
                                FwFormField.setValueByDataField($form, 'ZipCode',         response.getdriverinfo.zip);
                                FwFormField.setValueByDataField($form, 'MobilePhone',    response.getdriverinfo.cellular);
                                FwFormField.setValueByDataField($form, 'Phone',       response.getdriverinfo.phonehome);
                                FwFormField.setValueByDataField($form, 'OfficePhone', response.getdriverinfo.officephone);
                            }
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }, $form);
                }
            })
        ;

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form: JQuery = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ContactId', uniqueids.ContactId);
        FwModule.loadForm(this.Module, $form);
        //$form.find('.contactphoto > .runtime > .image > img').attr('src', 'fwappimage.ashx?uniqueid1=' + uniqueids.contactid + '&uniqueid2=&uniqueid3=&orderby=0');

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    //----------------------------------------------------------------------------------------------
    loadAudit($form: JQuery) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids = function($form: JQuery) {
        // load ContactCompany Grid
        //var nameContactCompany = 'ContactCompany';
        //var $contactCompanyGrid: JQuery = $form.find('div[data-grid="' + nameContactCompany + '"]');
        //var $contactCompanyGridControl: JQuery = FwBrowse.loadGridFromTemplate(nameContactCompany);
        //$contactCompanyGrid.empty().append($contactCompanyGridControl);
        //$contactCompanyGridControl.data('ondatabind', function(request) {
        //    request.module = 'ContactCompany';
        //    request.uniqueids = {
        //        contactid: FwFormField.getValueByDataField($form, 'ContactId')
        //    };
        //    FwServices.grid.method(request,'ContactCompany', 'Browse', $contactCompanyGridControl, function(response) {
        //        FwBrowse.databindcallback($contactCompanyGridControl, response.browse);
        //    });
        //});
        //FwBrowse.init($contactCompanyGridControl);
        //FwBrowse.renderRuntimeHtml($contactCompanyGridControl);
        //FwBrowse.addLegend($contactCompanyGridControl, 'Customer', '#ffff80');
        //FwBrowse.addLegend($contactCompanyGridControl, 'Project',  '#03de3a');
        //FwBrowse.addLegend($contactCompanyGridControl, 'Vendor',   '#20b7ff');

        // load ContactPersonalEvent Grid
        //var nameContactPersonalEvent = 'ContactPersonalEvent';
        //var $contactPersonalEventGrid: JQuery = $form.find('div[data-grid="' + nameContactPersonalEvent + '"]');
        //var $contactPersonalEventGridControl: JQuery = FwBrowse.loadGridFromTemplate(nameContactPersonalEvent);
        //$contactPersonalEventGrid.empty().append($contactPersonalEventGridControl);
        //$contactPersonalEventGridControl.data('ondatabind', function(request) {
        //    request.module = 'ContactPersonalEvent';
        //    request.uniqueids = {
        //        contactid: FwFormField.getValueByDataField($form, 'ContactId')
        //    };
        //    FwServices.grid.method(request,'ContactPersonalEvent', 'Browse', $contactPersonalEventGridControl, function(response) {
        //        FwBrowse.databindcallback($contactPersonalEventGridControl, response.browse);
        //    });
        //});
        //FwBrowse.init($contactPersonalEventGridControl);
        //FwBrowse.renderRuntimeHtml($contactPersonalEventGridControl);
    
        // load ContactNote Grid
        //var nameContactNote = 'ContactNote';
        //var $contactNoteGrid: JQuery = $form.find('div[data-grid="' + nameContactNote + '"]');
        //var $contactNoteGridControl: JQuery = FwBrowse.loadGridFromTemplate(nameContactNote);
        //$contactNoteGrid.empty().append($contactNoteGridControl);
        //$contactNoteGridControl.data('ondatabind', function(request) {
        //    request.module = 'ContactNote';
        //    request.uniqueids = {
        //        contactid: FwFormField.getValueByDataField($form, 'ContactId')
        //    };
        //    FwServices.grid.method(request,'ContactNote', 'Browse', $contactNoteGridControl, function(response) {
        //        FwBrowse.databindcallback($contactNoteGridControl, response.browse);
        //    });
        //});
        //FwBrowse.init($contactNoteGridControl);
        //FwBrowse.renderRuntimeHtml($contactNoteGridControl);
        //FwBrowse.addLegend($contactNoteGridControl, 'Customer', '#ffff80');
        //FwBrowse.addLegend($contactNoteGridControl, 'Project',  '#03de3a');
        //FwBrowse.addLegend($contactNoteGridControl, 'Vendor',   '#20b7ff');

        // load ContactDocument Grid
        //var nameContactDocument = 'ContactDocument';
        //var $contactDocumentGrid: JQuery = $form.find('div[data-grid="' + nameContactDocument + '"]');
        //var $contactDocumentGridControl: JQuery = FwBrowse.loadGridFromTemplate(nameContactDocument);
        //$contactDocumentGrid.empty().append($contactDocumentGridControl);
        //$contactDocumentGridControl.data('ondatabind', function(request) {
        //    request.module = 'ContactDocument';
        //    request.uniqueids = {
        //        contactid: FwFormField.getValueByDataField($form, 'ContactId')
        //    };
        //    FwServices.grid.method(request,'ContactDocument', 'Browse', $contactDocumentGridControl, function(response) {
        //        FwBrowse.databindcallback($contactDocumentGridControl, response.browse);
        //    });
        //});
        //FwBrowse.init($contactDocumentGridControl);
        //FwBrowse.renderRuntimeHtml($contactDocumentGridControl);

        // load ContactEmailHistory Grid
        //var nameContactEmailHistory = 'ContactEmailHistory';
        //var $contactEmailHistoryGrid: JQuery = $form.find('div[data-grid="' + nameContactEmailHistory + '"]');
        //var $contactEmailHistoryGridControl: JQuery = FwBrowse.loadGridFromTemplate(nameContactEmailHistory);
        //$contactEmailHistoryGrid.empty().append($contactEmailHistoryGridControl);
        //$contactEmailHistoryGridControl.data('ondatabind', function(request) {
        //    request.module = 'ContactEmailHistory';
        //    request.uniqueids = {
        //        contactid: FwFormField.getValueByDataField($form, 'ContactId')
        //    };
        //    FwServices.grid.method(request,'ContactEmailHistory', 'Browse', $contactEmailHistoryGridControl, function(response) {
        //        FwBrowse.databindcallback($contactEmailHistoryGridControl, response.browse);
        //    });
        //});
        //FwBrowse.init($contactEmailHistoryGridControl);
        //FwBrowse.renderRuntimeHtml($contactEmailHistoryGridControl);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        //var $contactCompanyGrid: JQuery = $form.find('#contactCompanyBrowse');
        //FwBrowse.search($contactCompanyGrid);

        //var $contactPersonalEventGrid: JQuery = $form.find('#contactEventsBrowse');
        //FwBrowse.search($contactPersonalEventGrid);

        //var $contactNoteGrid: JQuery = $form.find('#contactNoteBrowse');
        //FwBrowse.search($contactNoteGrid);

        //var $contactDocumentGrid: JQuery = $form.find('#contactDocumentBrowse');
        //FwBrowse.search($contactDocumentGrid);

        //var $contactEmailHistoryGrid: JQuery = $form.find('#emailHistoryBrowse');
        //FwBrowse.search($contactEmailHistoryGrid);
    };
    //----------------------------------------------------------------------------------------------
    setFormProperties($form: JQuery) {
        var $webaccess     = $form.find('div[data-datafield="WebAccess"]');
        var $email         = $form.find('div[data-datafield="Email"]');
        var $webpassword   = $form.find('div[data-datafield="WebPassword"]');
        var $inactive      = $form.find('div[data-datafield="Inactive"]');
        var $inactivedate  = $form.find('div[data-datafield="InactiveDate"]');
        var $persontype    = $form.find('div[data-datafield="PersonType"]');
        var $addressfields = $form.find('div[data-type="groupbox"][data-caption="Address"] div[data-control="FwFormField"]');
        var $cellular      = $form.find('div[data-datafield="MobilePhone"]');
        var $phone         = $form.find('div[data-datafield="Phone"]');
        var $officephone   = $form.find('div[data-datafield="OfficePhone"]');

        if (FwFormField.getValue2($webaccess) === 'T') {
            $webpassword.attr('data-required', 'true');
            $email.attr('data-required', 'true');
        } else {
            $webpassword.removeClass('error');
            $email.removeClass('error');
            $webpassword.attr('data-required', 'false');
            $email.attr('data-required', 'false');
        }

        if (FwFormField.getValue2($inactive) === 'T') {
            FwFormField.enable($inactivedate);
        } else {
            FwFormField.disable($inactivedate);
        }
    };
}

(window as any).ContactController = new Contact();