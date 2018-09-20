var FwWebApiReport = (function () {
    function FwWebApiReport(reportName, apiurl, frontEndHtml) {
        this.reportName = '';
        this.apiurl = '';
        this.frontEndHtml = '';
        this.reportName = reportName;
        this.apiurl = apiurl;
        this.frontEndHtml = frontEndHtml;
        this.reportOptions = {
            HasExportHtml: true,
            HasExportPdf: true,
            HasEmailHtml: true,
            HasEmailPdf: true,
            HasEmailMePdf: true,
            HasDownloadExcel: false
        };
    }
    FwWebApiReport.prototype.getFrontEnd = function () {
        var $form = jQuery(this.frontEndHtml);
        $form.attr('data-reportname', this.reportName);
        $form.on('change', '.fwformfield[data-required="true"].error', function () {
            var $this, value, errorTab;
            $this = jQuery(this);
            value = FwFormField.getValue2($this);
            errorTab = $this.closest('.tabpage').attr('data-tabid');
            if (value !== '') {
                $this.removeClass('error');
                if ($this.closest('.tabpage.active').has('.error').length === 0) {
                    $this.parents('.fwcontrol .fwtabs').find('#' + errorTab).removeClass('error');
                }
            }
        });
        return $form;
    };
    FwWebApiReport.prototype.getRenderRequest = function ($form) {
        var request = new RenderRequest();
        if (typeof $form.data('getRenderRequest') === 'function') {
            request = $form.data('getRenderRequest')(request);
        }
        return request;
    };
    FwWebApiReport.prototype.load = function ($form, reportOptions) {
        var formid = program.uniqueId(8);
        var $fwcontrols = $form.find('.fwcontrol').addBack();
        FwControl.renderRuntimeControls($fwcontrols);
        FwControl.setIds($fwcontrols, formid);
        this.addReportMenu($form, reportOptions);
        $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]'));
        $form.data('fields', $form.find('.fwformfield[data-isuniqueid!="true"]'));
    };
    FwWebApiReport.prototype.addReportMenu = function ($form, reportOptions) {
        var _this = this;
        var me = this;
        var $menuObject = FwMenu.getMenuControl('default');
        var timeout = 7200;
        var urlHtmlReport = applicationConfig.apiurl + "Reports/" + me.reportName + "/index.html";
        var apiUrl = applicationConfig.apiurl.substring(0, applicationConfig.apiurl.length - 1);
        var authorizationHeader = 'Bearer ' + sessionStorage.getItem('apiToken');
        if ((typeof reportOptions.HasExportHtml === 'undefined') || (reportOptions.HasExportHtml === true)) {
            var $btnPreview = FwMenu.addStandardBtn($menuObject, 'Preview');
            $btnPreview.on('click', function (event) {
                try {
                    var request_1 = me.getRenderRequest($form);
                    request_1.renderMode = 'Html';
                    request_1.parameters = _this.getParameters($form);
                    var win_1 = window.open(urlHtmlReport);
                    if (!win_1) {
                        throw 'Please disable your popup blocker for this site!';
                    }
                    else {
                        setTimeout(function () {
                            var message = new ReportPageMessage();
                            message.action = 'Preview';
                            message.apiUrl = apiUrl;
                            message.authorizationHeader = authorizationHeader;
                            message.request = request_1;
                            win_1.postMessage(message, '*');
                        }, 50);
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
            var $btnPrintPdf = FwMenu.addStandardBtn($menuObject, 'Print HTML');
            $btnPrintPdf.on('click', function (event) {
                try {
                    var request_2 = me.getRenderRequest($form);
                    request_2.renderMode = 'Html';
                    request_2.parameters = _this.getParameters($form);
                    var $iframe_1 = jQuery('<iframe style="display:none;" />');
                    jQuery('.application').append($iframe_1);
                    $iframe_1.attr('src', urlHtmlReport);
                    $iframe_1.on('load', function () {
                        setTimeout(function () {
                            var message = new ReportPageMessage();
                            message.action = 'PrintHtml';
                            message.apiUrl = apiUrl;
                            message.authorizationHeader = authorizationHeader;
                            message.request = request_2;
                            $iframe_1[0].focus();
                            $iframe_1[0].contentWindow.postMessage(message, '*');
                        }, 0);
                    });
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        FwMenu.addVerticleSeparator($menuObject);
        if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
            var $btnOpenPdf = FwMenu.addStandardBtn($menuObject, 'View PDF');
            $btnOpenPdf.on('click', function (event) {
                var request, webserviceurl, $notification;
                try {
                    var $notification_1 = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    var request_3 = me.getRenderRequest($form);
                    request_3.renderMode = 'Pdf';
                    request_3.downloadPdfAsAttachment = false;
                    request_3.parameters = _this.getParameters($form);
                    FwAppData.apiMethod(true, 'POST', me.apiurl + '/render', request_3, timeout, function (successResponse) {
                        try {
                            var win_2 = window.open(successResponse.pdfReportUrl);
                            var startTime = new Date();
                            var setWindowTitle_1 = function () {
                                if (win_2.document) {
                                    win_2.document.title = "Report (PDF)";
                                }
                                else {
                                    setTimeout(setWindowTitle_1, 10);
                                }
                            };
                            setWindowTitle_1();
                            if (!win_2)
                                throw 'Please disable your popup blocker for this site!';
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                        finally {
                            FwNotification.closeNotification($notification_1);
                        }
                    }, function (errorResponse) {
                        FwNotification.closeNotification($notification_1);
                        if (errorResponse !== 'abort') {
                            FwFunc.showError(errorResponse);
                        }
                    }, null);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
            var $btnDownloadPdf = FwMenu.addStandardBtn($menuObject, 'Download PDF');
            $btnDownloadPdf.on('click', function (event) {
                try {
                    var $notification_2 = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    var request = me.getRenderRequest($form);
                    request.renderMode = 'Pdf';
                    request.downloadPdfAsAttachment = true;
                    request.parameters = _this.getParameters($form);
                    FwAppData.apiMethod(true, 'POST', me.apiurl + '/render', request, timeout, function (successResponse) {
                        try {
                            var win_3 = window.open(successResponse.pdfReportUrl);
                            var startTime = new Date();
                            var setWindowTitle_2 = function () {
                                if (win_3.document) {
                                    win_3.document.title = "Report (PDF)";
                                }
                                else {
                                    setTimeout(setWindowTitle_2, 10);
                                }
                            };
                            setWindowTitle_2();
                            if (!win_3)
                                throw 'Please disable your popup blocker for this site!';
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                        finally {
                            FwNotification.closeNotification($notification_2);
                        }
                    }, function (errorResponse) {
                        FwNotification.closeNotification($notification_2);
                        if (errorResponse !== 'abort') {
                            FwFunc.showError(errorResponse);
                        }
                    }, null);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        FwMenu.addVerticleSeparator($menuObject);
        if ((typeof reportOptions.HasEmailMePdf === 'undefined') || (reportOptions.HasEmailMePdf === true)) {
            var $btnEmailMePdf = FwMenu.addStandardBtn($menuObject, 'E-mail (to me)');
            $btnEmailMePdf.on('click', function (event) {
                try {
                    var $notification_3 = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    var request = me.getRenderRequest($form);
                    request.renderMode = 'Email';
                    request.email.from = '[me]';
                    request.email.to = '[me]';
                    request.email.cc = '';
                    request.email.subject = '[reportname]';
                    request.email.body = '';
                    request.parameters = _this.getParameters($form);
                    FwAppData.apiMethod(true, 'POST', me.apiurl + '/render', request, timeout, function (successResponse) {
                        try {
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                        finally {
                            FwNotification.closeNotification($notification_3);
                        }
                    }, function (errorResponse) {
                        FwNotification.closeNotification($notification_3);
                        if (errorResponse !== 'abort') {
                            FwFunc.showError(errorResponse);
                        }
                    }, null);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if ((typeof reportOptions.HasEmailPdf === 'undefined') || (reportOptions.HasEmailPdf === true)) {
            var $btnEmailPdf = FwMenu.addStandardBtn($menuObject, 'E-mail');
            $btnEmailPdf.on('click', function (event) {
                try {
                    var $confirmation_1 = FwConfirmation.renderConfirmation(FwLanguages.translate('E-mail PDF'), '');
                    FwConfirmation.addControls($confirmation_1, _this.getEmailTemplate());
                    var email = '[me]';
                    if (sessionStorage.getItem('email') !== null) {
                        email = sessionStorage.getItem('email');
                    }
                    FwFormField.setValueByDataField($confirmation_1, 'from', email);
                    $confirmation_1.find('.tousers').data('onchange', function ($selectedRows) {
                        var emailArray = new Array();
                        for (var i = 0; i < $selectedRows.length; i++) {
                            var $tr = $selectedRows[i];
                            var email_1 = $tr.find('[data-cssclass="Email"]').text();
                            emailArray.push(email_1);
                        }
                        FwFormField.setValueByDataField($confirmation_1, 'to', emailArray.join('; '));
                        FwFormField.setValueByDataField($confirmation_1, 'tousers', '', '');
                    });
                    $confirmation_1.find('.ccusers').data('onchange', function ($selectedRows) {
                        var emailArray = new Array();
                        for (var i = 0; i < $selectedRows.length; i++) {
                            var $tr = $selectedRows[i];
                            var email_2 = $tr.find('[data-cssclass="Email"]').text();
                            emailArray.push(email_2);
                        }
                        FwFormField.setValueByDataField($confirmation_1, 'cc', emailArray.join('; '));
                        FwFormField.setValueByDataField($confirmation_1, 'ccusers', '', '');
                    });
                    FwFormField.setValueByDataField($confirmation_1, 'subject', FwTabs.getTabByElement($form).attr('data-caption'));
                    var $btnSend = FwConfirmation.addButton($confirmation_1, 'Send');
                    FwConfirmation.addButton($confirmation_1, 'Cancel');
                    $btnSend.click(function (event) {
                        try {
                            var $notification_4 = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                            var requestEmailPdf = me.getRenderRequest($form);
                            requestEmailPdf.renderMode = 'Email';
                            requestEmailPdf.email.to = '[me]';
                            requestEmailPdf.email.cc = '';
                            requestEmailPdf.email.subject = '[reportname]';
                            requestEmailPdf.email.body = '';
                            requestEmailPdf.parameters = _this.getParameters($form);
                            FwAppData.apiMethod(true, 'POST', _this.apiurl + '/render', requestEmailPdf, timeout, function (successResponse) {
                                try {
                                    FwNotification.renderNotification('SUCCESS', 'Email Sent');
                                }
                                catch (ex) {
                                    FwFunc.showError(ex);
                                }
                                finally {
                                    FwNotification.closeNotification($notification_4);
                                }
                            }, function (errorResponse) {
                                FwNotification.closeNotification($notification_4);
                                if (errorResponse !== 'abort') {
                                    FwFunc.showError(errorResponse);
                                }
                            }, $form);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        FwMenu.addVerticleSeparator($menuObject);
        if ((typeof reportOptions.HasDownloadExcel === 'undefined') || (reportOptions.HasDownloadExcel === true)) {
            var $btnDownloadExcel = FwMenu.addStandardBtn($menuObject, 'Download Excel');
            $btnDownloadExcel.on('click', function (event) {
                try {
                    var $notification_5 = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    var request = me.getRenderRequest($form);
                    request.downloadPdfAsAttachment = true;
                    FwAppData.apiMethod(true, 'POST', _this.apiurl + '/render', request, timeout, function (successResponse) {
                        try {
                            var $iframe_2 = jQuery('<iframe style="display:none;" />');
                            jQuery('.application').append($iframe_2);
                            $iframe_2.attr('src', successResponse.downloadurl);
                            setTimeout(function () {
                                $iframe_2.remove();
                            }, 500);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                        finally {
                            FwNotification.closeNotification($notification_5);
                        }
                    }, function (errorResponse) {
                        FwNotification.closeNotification($notification_5);
                        if (errorResponse !== 'abort') {
                            FwFunc.showError(errorResponse);
                        }
                    }, null);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if (typeof window[$form.attr('data-controller')].addReportMenuItems === 'function') {
            $menuObject = window[$form.attr('data-controller')].addReportMenuItems($menuObject, $form);
        }
        FwControl.renderRuntimeControls($menuObject.find('.fwcontrol').addBack());
        $form.find('.fwform-menu').append($menuObject);
    };
    FwWebApiReport.prototype.getParameters = function ($form) {
        var parameters = null;
        var isvalid = FwModule.validateForm($form);
        if (isvalid) {
            parameters = {};
            var $fields = $form.find('div[data-control="FwFormField"]');
            $fields.each(function (index, element) {
                var $field = jQuery(element);
                if (typeof $field.attr('data-datafield') === 'string') {
                    parameters[$field.attr('data-datafield')] = FwFormField.getValue2($field);
                }
            });
        }
        else {
            throw 'Please fill in the required fields.';
        }
        return parameters;
    };
    FwWebApiReport.prototype.getEmailTemplate = function () {
        return "\n            <div style=\"width:540px;\">\n              <div class=\"formrow\">\n                <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                  <div data-datafield=\"from\" data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield from\" data-caption=\"From\" data-enabled=\"true\"></div>\n                </div>\n                <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                  <div data-datafield=\"tousers\" data-control=\"FwFormField\" data-type=\"multiselectvalidation\" class=\"fwcontrol fwformfield tousers\" data-caption=\"To (Users)\" data-validationname=\"PersonValidation\" data-hasselectall=\"false\" style=\"float:left;box-sizing:border-box;width:20%;\"></div>\n                  <div data-datafield=\"to\" data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield to\" data-caption=\"To\" data-enabled=\"true\" style=\"float:left;box-sizing:border-box;width:80%;\"></div>\n                </div>\n                <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                  <div data-datafield=\"ccusers\" data-control=\"FwFormField\" data-type=\"multiselectvalidation\" class=\"fwcontrol fwformfield ccusers\" data-caption=\"CC (Users)\" data-validationname=\"PersonValidation\"  data-hasselectall=\"false\" style=\"float:left;box-sizing:border-box;width:20%;\"></div>\n                  <div data-datafield=\"cc\" data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield cc\" data-caption=\"CC\" data-enabled=\"true\" style=\"float:left;box-sizing:border-box;width:80%;\"></div>\n                </div>\n                <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                  <div data-datafield=\"subject\" data-control=\"FwFormField\" data-type=\"text\" class=\"fwcontrol fwformfield subject\" data-caption=\"Subject\" data-enabled=\"true\"></div>\n                </div>\n                <div class=\"fwcontrol fwcontainer fwform-fieldrow\" data-control=\"FwContainer\" data-type=\"fieldrow\">\n                  <div data-datafield=\"body\" data-control=\"FwFormField\" data-type=\"textarea\" class=\"fwcontrol fwformfield message\" data-caption=\"Message\" data-enabled=\"true\"></div>\n                </div>\n              </div>\n            </div>\n        ";
    };
    return FwWebApiReport;
}());
var ReportPageMessage = (function () {
    function ReportPageMessage() {
        this.action = 'None';
        this.apiUrl = '';
        this.authorizationHeader = '';
        this.request = new RenderRequest();
    }
    return ReportPageMessage;
}());
var RenderRequest = (function () {
    function RenderRequest() {
        this.parameters = {};
        this.email = new EmailInfo();
        this.uniqueid = '';
        this.downloadPdfAsAttachment = false;
    }
    return RenderRequest;
}());
var RenderResponse = (function () {
    function RenderResponse() {
        this.htmlReportUrl = '';
        this.pdfReportUrl = '';
    }
    return RenderResponse;
}());
var EmailInfo = (function () {
    function EmailInfo() {
        this.from = '';
        this.to = '';
        this.cc = '';
        this.subject = '';
        this.body = '';
    }
    return EmailInfo;
}());
//# sourceMappingURL=FwWebApiReport.js.map