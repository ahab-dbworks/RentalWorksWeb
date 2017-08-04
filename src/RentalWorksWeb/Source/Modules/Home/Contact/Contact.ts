declare var FwModule: any;
declare var FwBrowse: any;
declare var FwServices: any;
declare var FwMenu: any;
declare var Mustache: any;
declare var FwFunc: any;
declare var FwNotification: any;

class Contact {
    Module: string;
    apiurl: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Contact';
        this.apiurl = 'api/v1/contact';
        this.ActiveView = 'ALL';
    }

    getModuleScreen() {
        var screen, $browse;

        screen            = {};
        screen.$view      = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Contact', false, 'BROWSE', true);
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
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
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
        var $all, $customer, $project, $vendor, $signup, viewSubitems, $view;

        $all      = FwMenu.generateDropDownViewBtn('All Contacts', true);
        $customer = FwMenu.generateDropDownViewBtn('Customer Contacts', false);
        $project  = FwMenu.generateDropDownViewBtn('Project Contacts', false);
        $vendor   = FwMenu.generateDropDownViewBtn('Vendor Contacts', false);
        //ag 02/12/2015 - signup is N/A for TW WEB
        //$signup   = FwMenu.generateDropDownViewBtn('View Sign Up', false);

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

        viewSubitems = [];
        viewSubitems.push($all);
        viewSubitems.push($customer);
        viewSubitems.push($project);
        viewSubitems.push($vendor);
        //viewSubitems.push($signup);
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var viewModel, $form;
        viewModel = {};

        $form = jQuery(Mustache.render(jQuery('#tmpl-modules-ContactForm').html(), viewModel));
        $form = FwModule.openForm($form, mode);

        if (mode == 'NEW') {
            FwFormField.setValue($form, 'div[data-datafield="contact.activedate"]', FwFunc.getDate());
        }

        $form
            .on('change', 'div[data-datafield="webusers.webpassword"]', function() {
                var $this, request;
                $this = jQuery(this);
                request = {
                    method: 'CheckPasswordComplexity',
                    value:  FwFormField.getValue2($this),
                    first:  FwFormField.getValue2($form.find('div[data-datafield="contact.fname"]')),
                    last:   FwFormField.getValue2($form.find('div[data-datafield="contact.lname"]'))
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
            .on('change', 'div[data-datafield="webusers.webaccess"]', function() {
                this.setFormProperties($form);
            })
            .on('change', 'div[data-datafield="contact.inactive"]', function() {
                var $this;
                $this = jQuery(this);
                this.setFormProperties($form);
                if (FwFormField.getValue2($this) === 'T') {
                    FwFormField.setValue($form, 'div[data-datafield="contact.inactivedate"]', FwFunc.getDate());
                } else {
                    FwFormField.setValue($form, 'div[data-datafield="contact.inactivedate"]', '');
                }
            })
            .on('change', 'div[data-datafield="contact.persontype"]', function() {
                this.setFormProperties($form);
            })
            .on('change', 'div[data-datafield="contact.fname"], div[data-datafield="contact.lname"]', function() {
                var fname, lname, persontype, $request;
                fname      = FwFormField.getValue($form, 'div[data-datafield="contact.fname"]');
                lname      = FwFormField.getValue($form, 'div[data-datafield="contact.lname"]');
                persontype = FwFormField.getValue($form, 'div[data-datafield="contact.persontype"]');
                if ((persontype === 'DRIVER') && (fname !== '') && (lname !== '')) {
                    var request = {
                        method: 'GetDriverInfo',
                        fname:  fname,
                        lname:  lname
                    }
                    FwModule.getData($form, request, function(response) {
                        try {
                            if (response.getdriverinfo != null) {
                                FwFormField.setValue($form, 'div[data-datafield="contact.add1"]',        response.getdriverinfo.add1);
                                FwFormField.setValue($form, 'div[data-datafield="contact.add2"]',        response.getdriverinfo.add2);
                                FwFormField.setValue($form, 'div[data-datafield="contact.city"]',        response.getdriverinfo.city);
                                FwFormField.setValue($form, 'div[data-datafield="contact.state"]',       response.getdriverinfo.state);
                                FwFormField.setValue($form, 'div[data-datafield="contact.zip"]',         response.getdriverinfo.zip);
                                FwFormField.setValue($form, 'div[data-datafield="contact.cellular"]',    response.getdriverinfo.cellular);
                                FwFormField.setValue($form, 'div[data-datafield="contact.phone"]',       response.getdriverinfo.phonehome);
                                FwFormField.setValue($form, 'div[data-datafield="contact.officephone"]', response.getdriverinfo.officephone);
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
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val(uniqueids.contactid);
        FwModule.loadForm(this.Module, $form);
        //$form.find('.contactphoto > .runtime > .image > img').attr('src', 'fwappimage.ashx?uniqueid1=' + uniqueids.contactid + '&uniqueid2=&uniqueid3=&orderby=0');

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids = function($form: any) {
        var $contactCompanyGrid,       $contactCompanyGridControl,
            $contactPersonalEventGrid, $contactPersonalEventGridControl,
            $contactNoteGrid,          $contactNoteGridControl,
            $contactDocumentGrid,      $contactDocumentGridControl,
            $contactEmailHistoryGrid,  $contactEmailHistoryGridControl;

        // load ContactCompany Grid
        $contactCompanyGrid = $form.find('div[data-grid="ContactCompany"]');
        $contactCompanyGridControl = jQuery(jQuery('#tmpl-grids-ContactCompanyBrowse').html());
        $contactCompanyGrid.empty().append($contactCompanyGridControl);
        $contactCompanyGridControl.data('ondatabind', function(request) {
            request.module = 'ContactCompany';
            request.uniqueids = {
                contactid: $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val()
            };
            FwServices.grid.method(request,'ContactCompany', 'Browse', $contactCompanyGridControl, function(response) {
                FwBrowse.databindcallback($contactCompanyGridControl, response.browse);
            });
        });
        FwBrowse.init($contactCompanyGridControl);
        FwBrowse.renderRuntimeHtml($contactCompanyGridControl);
        FwBrowse.addLegend($contactCompanyGridControl, 'Customer', '#ffff80');
        FwBrowse.addLegend($contactCompanyGridControl, 'Project',  '#03de3a');
        FwBrowse.addLegend($contactCompanyGridControl, 'Vendor',   '#20b7ff');

        // load ContactPersonalEvent Grid
        $contactPersonalEventGrid = $form.find('div[data-grid="PersonalEvent"]');
        $contactPersonalEventGridControl = jQuery(jQuery('#tmpl-grids-ContactPersonalEventBrowse').html());
        $contactPersonalEventGrid.empty().append($contactPersonalEventGridControl);
        $contactPersonalEventGridControl.data('ondatabind', function(request) {
            request.module = 'ContactPersonalEvent';
            request.uniqueids = {
                contactid: $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val()
            };
            FwServices.grid.method(request,'ContactPersonalEvent', 'Browse', $contactPersonalEventGridControl, function(response) {
                FwBrowse.databindcallback($contactPersonalEventGridControl, response.browse);
            });
        });
        FwBrowse.init($contactPersonalEventGridControl);
        FwBrowse.renderRuntimeHtml($contactPersonalEventGridControl);
    
        // load ContactNote Grid
        $contactNoteGrid = $form.find('div[data-grid="ContactNote"]');
        $contactNoteGridControl = jQuery(jQuery('#tmpl-grids-ContactNoteBrowse').html());
        $contactNoteGrid.empty().append($contactNoteGridControl);
        $contactNoteGridControl.data('ondatabind', function(request) {
            request.module = 'ContactNote';
            request.uniqueids = {
                contactid: $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val()
            };
            FwServices.grid.method(request,'ContactNote', 'Browse', $contactNoteGridControl, function(response) {
                FwBrowse.databindcallback($contactNoteGridControl, response.browse);
            });
        });
        FwBrowse.init($contactNoteGridControl);
        FwBrowse.renderRuntimeHtml($contactNoteGridControl);
        FwBrowse.addLegend($contactNoteGridControl, 'Customer', '#ffff80');
        FwBrowse.addLegend($contactNoteGridControl, 'Project',  '#03de3a');
        FwBrowse.addLegend($contactNoteGridControl, 'Vendor',   '#20b7ff');

        // load ContactDocument Grid
        $contactDocumentGrid = $form.find('div[data-grid="ContactDocument"]');
        $contactDocumentGridControl = jQuery(jQuery('#tmpl-grids-ContactDocumentBrowse').html());
        $contactDocumentGrid.empty().append($contactDocumentGridControl);
        $contactDocumentGridControl.data('ondatabind', function(request) {
            request.module = 'ContactDocument';
            request.uniqueids = {
                contactid: $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val()
            };
            FwServices.grid.method(request,'ContactDocument', 'Browse', $contactDocumentGridControl, function(response) {
                FwBrowse.databindcallback($contactDocumentGridControl, response.browse);
            });
        });
        FwBrowse.init($contactDocumentGridControl);
        FwBrowse.renderRuntimeHtml($contactDocumentGridControl);

        // load ContactEmailHistory Grid
        $contactEmailHistoryGrid = $form.find('div[data-grid="ContactEmailHistory"]');
        $contactEmailHistoryGridControl = jQuery(jQuery('#tmpl-grids-ContactEmailHistoryBrowse').html());
        $contactEmailHistoryGrid.empty().append($contactEmailHistoryGridControl);
        $contactEmailHistoryGridControl.data('ondatabind', function(request) {
            request.module = 'ContactEmailHistory';
            request.uniqueids = {
                contactid: $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val()
            };
            FwServices.grid.method(request,'ContactEmailHistory', 'Browse', $contactEmailHistoryGridControl, function(response) {
                FwBrowse.databindcallback($contactEmailHistoryGridControl, response.browse);
            });
        });
        FwBrowse.init($contactEmailHistoryGridControl);
        FwBrowse.renderRuntimeHtml($contactEmailHistoryGridControl);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        var $contactCompanyGrid,
            $contactPersonalEventGrid,
            $contactNoteGrid,
            $contactDocumentGrid,
            $contactEmailHistoryGrid;

        $contactCompanyGrid = $form.find('#contactCompanyBrowse');
        FwBrowse.search($contactCompanyGrid);

        $contactPersonalEventGrid = $form.find('#contactEventsBrowse');
        FwBrowse.search($contactPersonalEventGrid);

        $contactNoteGrid = $form.find('#contactNoteBrowse');
        FwBrowse.search($contactNoteGrid);

        $contactDocumentGrid = $form.find('#contactDocumentBrowse');
        FwBrowse.search($contactDocumentGrid);

        $contactEmailHistoryGrid = $form.find('#emailHistoryBrowse');
        FwBrowse.search($contactEmailHistoryGrid);
    };
    //----------------------------------------------------------------------------------------------
    setFormProperties($form: any) {
        var $webaccess, $email, $webpassword, $inactive, $inactivedate, $persontype, $addressfields, $cellular, $phone, $officephone;

        $webaccess     = $form.find('div[data-datafield="webusers.webaccess"]');
        $email         = $form.find('div[data-datafield="contact.email"]');
        $webpassword   = $form.find('div[data-datafield="webusers.webpassword"]');
        $inactive      = $form.find('div[data-datafield="contact.inactive"]');
        $inactivedate  = $form.find('div[data-datafield="contact.inactivedate"]');
        $persontype    = $form.find('div[data-datafield="contact.persontype"]');
        $addressfields = $form.find('div[data-type="groupbox"][data-caption="Address"] div[data-control="FwFormField"]');
        $cellular      = $form.find('div[data-datafield="contact.cellular"]');
        $phone         = $form.find('div[data-datafield="contact.phone"]');
        $officephone   = $form.find('div[data-datafield="contact.officephone"]');

        if (FwFormField.getValue2($webaccess) === 'T') {
            $webpassword.attr('data-required', true);
            $email.attr('data-required', true);
        } else {
            $webpassword.removeClass('error');
            $email.removeClass('error');
            $webpassword.attr('data-required', false);
            $email.attr('data-required', false);
        }

        if (FwFormField.getValue2($inactive) === 'T') {
            FwFormField.enable($inactivedate);
        } else {
            FwFormField.disable($inactivedate);
        }

        if (FwFormField.getValue2($persontype) === 'DRIVER') {
            FwFormField.disable($addressfields);
            FwFormField.disable($cellular);
            FwFormField.disable($phone);
            FwFormField.disable($officephone);
        } else {
            FwFormField.enable($addressfields);
            FwFormField.enable($cellular);
            FwFormField.enable($phone);
            FwFormField.enable($officephone);
        }
    };
}

(window as any).ContactController = new Contact();