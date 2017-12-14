var Contact = /** @class */ (function () {
    function Contact() {
        //----------------------------------------------------------------------------------------------
        this.renderGrids = function ($form) {
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
            var companyContactId = jQuery('[data-name="Contact"]').find('div > table > tbody > .selected > td > [data-formdatafield="CompanyContactId"]').text();
            if (companyContactId == "" || undefined || null) {
                var $contactNoteGrid;
                var $contactNoteGridControl;
                $contactNoteGrid = $form.find('div[data-grid="ContactNoteGrid"]');
                $contactNoteGridControl = jQuery(jQuery('#tmpl-grids-ContactNoteGridBrowse').html());
                $contactNoteGrid.empty().append($contactNoteGridControl);
                $contactNoteGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        ContactId: $form.find('div.fwformfield[data-datafield="ContactId"] input').val()
                    };
                });
                $contactNoteGridControl.data('beforesave', function (request) {
                    request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
                });
                FwBrowse.init($contactNoteGridControl);
                FwBrowse.renderRuntimeHtml($contactNoteGridControl);
            }
            else {
                var $contactNoteGrid;
                var $contactNoteGridControl;
                $contactNoteGrid = $form.find('div[data-grid="ContactNoteGrid"]');
                $contactNoteGridControl = jQuery(jQuery('#tmpl-grids-ContactNoteGridBrowse').html());
                $contactNoteGrid.empty().append($contactNoteGridControl);
                $contactNoteGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        ContactId: $form.find('div.fwformfield[data-datafield="ContactId"] input').val(),
                        CompanyContactId: $form.find('div.fwformfield[data-datafield="CompanyContactId"] input').val()
                    };
                });
                $contactNoteGridControl.data('beforesave', function (request) {
                    request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
                    request.CompanyContactId = FwFormField.getValueByDataField($form, 'CompanyContactId');
                });
                FwBrowse.init($contactNoteGridControl);
                FwBrowse.renderRuntimeHtml($contactNoteGridControl);
            }
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
        this.Module = 'Contact';
        this.apiurl = 'api/v1/contact';
        this.caption = 'Contact';
        this.ActiveView = 'ALL';
    }
    Contact.prototype.getModuleScreen = function () {
        var me = this;
        var screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
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
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.openBrowse = function () {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'Lead', '#ff8040');
        FwBrowse.addLegend($browse, 'Prospect', '#ff0080');
        FwBrowse.addLegend($browse, 'Customer', '#ffff80');
        FwBrowse.addLegend($browse, 'Deal', '#03de3a');
        FwBrowse.addLegend($browse, 'Vendor', '#20b7ff');
        return $browse;
    };
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.addBrowseMenuItems = function ($menuObject) {
        var self = this;
        var $all = FwMenu.generateDropDownViewBtn('All Contacts', true);
        var $lead = FwMenu.generateDropDownViewBtn('Lead Contacts', false);
        var $prospect = FwMenu.generateDropDownViewBtn('Prospect Contacts', false);
        var $customer = FwMenu.generateDropDownViewBtn('Customer Contacts', false);
        var $deal = FwMenu.generateDropDownViewBtn('Deal Contacts', false);
        var $vendor = FwMenu.generateDropDownViewBtn('Vendor Contacts', false);
        //var $signup   = FwMenu.generateDropDownViewBtn('View Sign Up', false);
        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.databind($browse);
        });
        $lead.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LEAD';
            FwBrowse.databind($browse);
        });
        $prospect.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'PROSPECT';
            FwBrowse.databind($browse);
        });
        $customer.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CUSTOMER';
            FwBrowse.databind($browse);
        });
        $deal.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'DEAL';
            FwBrowse.databind($browse);
        });
        $vendor.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'VENDOR';
            FwBrowse.databind($browse);
        });
        //$signup.on('click', function() {
        //    var $browse;
        //    $browse = jQuery(this).closest('.fwbrowse');
        //    this.ActiveView = 'SIGNUP';
        //    FwBrowse.databind($browse);
        //});
        FwMenu.addVerticleSeparator($menuObject);
        var viewSubitems = [];
        viewSubitems.push($all);
        viewSubitems.push($lead);
        viewSubitems.push($prospect);
        viewSubitems.push($customer);
        viewSubitems.push($deal);
        viewSubitems.push($vendor);
        //viewSubitems.push($signup);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);
        return $menuObject;
    };
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.openForm = function (mode) {
        var viewModel = {};
        var companyContactId = jQuery('[data-name="Contact"]').find('div > table > tbody > .selected > td > [data-formdatafield="CompanyContactId"]').text();
        if (companyContactId == "" || undefined || null) {
            this.apiurl = 'api/v1/contact';
            var $form = jQuery(Mustache.render(jQuery('#tmpl-modules-ContactForm').html(), viewModel));
        }
        else {
            this.apiurl = 'api/v1/companycontact';
            var $form = jQuery(Mustache.render(jQuery('#tmpl-modules-CompanyContactForm').html(), viewModel));
        }
        $form = FwModule.openForm($form, mode);
        if (mode == 'NEW') {
            FwFormField.setValueByDataField($form, 'ActiveDate', FwFunc.getDate());
        }
        $form
            .on('change', 'div[data-datafield="WebPassword"]', function () {
            //throw 'not implented!';
            //// need to create web service in new api
            //var $this, request;
            //$this = jQuery(this);
            //request = {
            //    method: 'CheckPasswordComplexity',
            //    value:  FwFormField.getValueByDataField($form, 'WebPassword'),
            //    first:  FwFormField.getValueByDataField($form, 'FirstName'),
            //    last:   FwFormField.getValueByDataField($form, 'LastName')
            //}
            //FwModule.getData($form, request, function(response) {
            //    try {
            //        if (response.passwordcomplexity.error == true) {
            //            $this.addClass('error');
            //            FwNotification.renderNotification('ERROR', response.passwordcomplexity.errmsg);
            //        } else {
            //            $this.removeClass('error');
            //        }
            //    } catch (ex) {
            //        FwFunc.showError(ex);
            //    }
            //}, $form);
        })
            .on('change', 'div[data-datafield="WebAccess"]', function () {
            //this.setFormProperties($form);
        })
            .on('change', 'div[data-datafield="Inactive"]', function () {
            //var $this;
            //$this = jQuery(this);
            //this.setFormProperties($form);
            //if (FwFormField.getValue2($this) === 'T') {
            //    FwFormField.setValueByDataField($form, 'InactiveDate"]', FwFunc.getDate());
            //} else {
            //    FwFormField.setValueByDataField($form, 'InactiveDate', '');
            //}
        })
            .on('change', 'div[data-datafield="PersonType"]', function () {
            //this.setFormProperties($form);
        })
            .on('change', 'div[data-datafield="FirstName"], div[data-datafield="LastName"]', function () {
            //throw 'not implemented';
            //// need to add web service in api
            //var fname, lname, persontype, $request;
            //var fname      = FwFormField.getValueByDataField($form, 'FirstName');
            //var lname      = FwFormField.getValueByDataField($form, 'LastName');
            //var persontype = FwFormField.getValueByDataField($form, 'PersonType');
            //if ((persontype === 'DRIVER') && (fname !== '') && (lname !== '')) {
            //    var request = {
            //        method: 'GetDriverInfo',
            //        fname:  fname,
            //        lname:  lname
            //    }
            //    FwModule.getData($form, request, function(response) {
            //        try {
            //            if (response.getdriverinfo != null) {
            //                FwFormField.setValueByDataField($form, 'Address1',        response.getdriverinfo.add1);
            //                FwFormField.setValueByDataField($form, 'Address2',        response.getdriverinfo.add2);
            //                FwFormField.setValueByDataField($form, 'City',        response.getdriverinfo.city);
            //                FwFormField.setValueByDataField($form, 'State',       response.getdriverinfo.state);
            //                FwFormField.setValueByDataField($form, 'ZipCode',         response.getdriverinfo.zip);
            //                FwFormField.setValueByDataField($form, 'MobilePhone',    response.getdriverinfo.cellular);
            //                FwFormField.setValueByDataField($form, 'Phone',       response.getdriverinfo.phonehome);
            //                FwFormField.setValueByDataField($form, 'OfficePhone', response.getdriverinfo.officephone);
            //            }
            //        } catch (ex) {
            //            FwFunc.showError(ex);
            //        }
            //    }, $form);
            //}
        });
        return $form;
    };
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.loadForm = function (uniqueids) {
        var $form = this.openForm('EDIT');
        var companyContactId = jQuery('[data-name="Contact"]').find('div > table > tbody > .selected > td > [data-formdatafield="CompanyContactId"]').text();
        if (companyContactId == "" || undefined || null) {
            FwFormField.setValueByDataField($form, 'ContactId', uniqueids.ContactId);
        }
        else {
            FwFormField.setValueByDataField($form, 'CompanyContactId', uniqueids.CompanyContactId);
        }
        FwModule.loadForm(this.Module, $form);
        //$form.find('.contactphoto > .runtime > .image > img').attr('src', 'fwappimage.ashx?uniqueid1=' + uniqueids.contactid + '&uniqueid2=&uniqueid3=&orderby=0');
        return $form;
    };
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.loadAudit = function ($form) {
        //var uniqueid;
        //uniqueid = $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val();
        //FwModule.loadAudit($form, uniqueid);
    };
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.afterLoad = function ($form) {
        //var $contactCompanyGrid: JQuery = $form.find('#contactCompanyBrowse');
        //FwBrowse.search($contactCompanyGrid);
        //var $contactPersonalEventGrid: JQuery = $form.find('#contactEventsBrowse');
        //FwBrowse.search($contactPersonalEventGrid);
        var $contactNoteGrid;
        $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);
        //var $contactDocumentGrid: JQuery = $form.find('#contactDocumentBrowse');
        //FwBrowse.search($contactDocumentGrid);
        //var $contactEmailHistoryGrid: JQuery = $form.find('#emailHistoryBrowse');
        //FwBrowse.search($contactEmailHistoryGrid);
    };
    ;
    return Contact;
}());
window.ContactController = new Contact();
//# sourceMappingURL=Contact.js.map