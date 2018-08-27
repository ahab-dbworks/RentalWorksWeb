class FwWebApiReport {
    constructor(reportName, apiurl, frontEndHtml) {
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
    getFrontEnd() {
        let $form = jQuery(this.frontEndHtml);
        $form.attr('data-reportname', this.reportName);
        $form.on('change', '.fwformfield[data-required="true"].error', function () {
            var $this, value;
            $this = jQuery(this);
            value = FwFormField.getValue2($this);
            if (value !== '') {
                $this.removeClass('error');
            }
        });
        return $form;
    }
    getRenderRequest($form) {
        let request = new RenderRequest();
        if (typeof $form.data('getRenderRequest') === 'function') {
            request = $form.data('getRenderRequest')(request);
        }
        return request;
    }
    load($form, reportOptions) {
        let formid = program.uniqueId(8);
        let $fwcontrols = $form.find('.fwcontrol').addBack();
        FwControl.renderRuntimeControls($fwcontrols);
        FwControl.setIds($fwcontrols, formid);
        this.addReportMenu($form, reportOptions);
        $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]'));
        $form.data('fields', $form.find('.fwformfield[data-isuniqueid!="true"]'));
    }
    addReportMenu($form, reportOptions) {
        let me = this;
        let $menuObject = FwMenu.getMenuControl('default');
        let timeout = 7200;
        let urlHtmlReport = `${applicationConfig.apiurl}Reports/${me.reportName}/index.html`;
        let apiUrl = applicationConfig.apiurl.substring(0, applicationConfig.apiurl.length - 1);
        let authorizationHeader = 'Bearer ' + sessionStorage.getItem('apiToken');
        if ((typeof reportOptions.HasExportHtml === 'undefined') || (reportOptions.HasExportHtml === true)) {
            let $btnPreview = FwMenu.addStandardBtn($menuObject, 'Preview');
            $btnPreview.on('click', (event) => {
                try {
                    let request = me.getRenderRequest($form);
                    request.renderMode = 'Html';
                    request.parameters = this.getParameters($form);
                    let win = window.open(urlHtmlReport);
                    if (!win) {
                        throw 'Please disable your popup blocker for this site!';
                    }
                    else {
                        setTimeout(() => {
                            let message = new ReportPageMessage();
                            message.action = 'Preview';
                            message.apiUrl = apiUrl;
                            message.authorizationHeader = authorizationHeader;
                            message.request = request;
                            win.postMessage(message, '*');
                        }, 50);
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
            let $btnPrintPdf = FwMenu.addStandardBtn($menuObject, 'Print HTML');
            $btnPrintPdf.on('click', (event) => {
                try {
                    let request = me.getRenderRequest($form);
                    request.renderMode = 'Html';
                    request.parameters = this.getParameters($form);
                    let $iframe = jQuery('<iframe style="display:none;" />');
                    jQuery('.application').append($iframe);
                    $iframe.attr('src', urlHtmlReport);
                    $iframe.on('load', () => {
                        setTimeout(() => {
                            let message = new ReportPageMessage();
                            message.action = 'PrintHtml';
                            message.apiUrl = apiUrl;
                            message.authorizationHeader = authorizationHeader;
                            message.request = request;
                            $iframe[0].focus();
                            $iframe[0].contentWindow.postMessage(message, '*');
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
            let $btnOpenPdf = FwMenu.addStandardBtn($menuObject, 'View PDF');
            $btnOpenPdf.on('click', (event) => {
                var request, webserviceurl, $notification;
                try {
                    let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    let request = me.getRenderRequest($form);
                    request.renderMode = 'Pdf';
                    request.downloadPdfAsAttachment = false;
                    request.parameters = this.getParameters($form);
                    FwAppData.apiMethod(true, 'POST', me.apiurl + '/render', request, timeout, (successResponse) => {
                        try {
                            let win = window.open(successResponse.pdfReportUrl);
                            let startTime = new Date();
                            let setWindowTitle = () => {
                                if (win.document) {
                                    win.document.title = "Report (PDF)";
                                }
                                else {
                                    setTimeout(setWindowTitle, 10);
                                }
                            };
                            setWindowTitle();
                            if (!win)
                                throw 'Please disable your popup blocker for this site!';
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                        finally {
                            FwNotification.closeNotification($notification);
                        }
                    }, (errorResponse) => {
                        FwNotification.closeNotification($notification);
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
            let $btnDownloadPdf = FwMenu.addStandardBtn($menuObject, 'Download PDF');
            $btnDownloadPdf.on('click', (event) => {
                try {
                    let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    let request = me.getRenderRequest($form);
                    request.renderMode = 'Pdf';
                    request.downloadPdfAsAttachment = true;
                    request.parameters = this.getParameters($form);
                    FwAppData.apiMethod(true, 'POST', me.apiurl + '/render', request, timeout, (successResponse) => {
                        try {
                            let win = window.open(successResponse.pdfReportUrl);
                            let startTime = new Date();
                            let setWindowTitle = () => {
                                if (win.document) {
                                    win.document.title = "Report (PDF)";
                                }
                                else {
                                    setTimeout(setWindowTitle, 10);
                                }
                            };
                            setWindowTitle();
                            if (!win)
                                throw 'Please disable your popup blocker for this site!';
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                        finally {
                            FwNotification.closeNotification($notification);
                        }
                    }, (errorResponse) => {
                        FwNotification.closeNotification($notification);
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
            let $btnEmailMePdf = FwMenu.addStandardBtn($menuObject, 'E-mail (to me)');
            $btnEmailMePdf.on('click', (event) => {
                try {
                    let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    let request = me.getRenderRequest($form);
                    request.renderMode = 'Email';
                    request.email.from = '[me]';
                    request.email.to = '[me]';
                    request.email.cc = '';
                    request.email.subject = '[reportname]';
                    request.email.body = '';
                    request.parameters = this.getParameters($form);
                    FwAppData.apiMethod(true, 'POST', me.apiurl + '/render', request, timeout, (successResponse) => {
                        try {
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                        finally {
                            FwNotification.closeNotification($notification);
                        }
                    }, (errorResponse) => {
                        FwNotification.closeNotification($notification);
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
            let $btnEmailPdf = FwMenu.addStandardBtn($menuObject, 'E-mail');
            $btnEmailPdf.on('click', (event) => {
                try {
                    let $confirmation = FwConfirmation.renderConfirmation(FwLanguages.translate('E-mail PDF'), '');
                    FwConfirmation.addControls($confirmation, this.getEmailTemplate());
                    let email = '[me]';
                    if (sessionStorage.getItem('email') !== null) {
                        email = sessionStorage.getItem('email');
                    }
                    FwFormField.setValueByDataField($confirmation, 'from', email);
                    $confirmation.find('.tousers').data('onchange', function ($selectedRows) {
                        let emailArray = new Array();
                        for (let i = 0; i < $selectedRows.length; i++) {
                            let $tr = $selectedRows[i];
                            let email = $tr.find('[data-cssclass="Email"]').text();
                            emailArray.push(email);
                        }
                        FwFormField.setValueByDataField($confirmation, 'to', emailArray.join('; '));
                    });
                    $confirmation.find('.ccusers').data('onchange', function ($selectedRows) {
                        let emailArray = new Array();
                        for (let i = 0; i < $selectedRows.length; i++) {
                            let $tr = $selectedRows[i];
                            let email = $tr.find('[data-cssclass="Email"]').text();
                            emailArray.push(email);
                        }
                        FwFormField.setValueByDataField($confirmation, 'cc', emailArray.join('; '));
                    });
                    FwFormField.setValueByDataField($confirmation, 'subject', FwTabs.getTabByElement($form).attr('data-caption'));
                    let $btnSend = FwConfirmation.addButton($confirmation, 'Send');
                    FwConfirmation.addButton($confirmation, 'Cancel');
                    $btnSend.click((event) => {
                        try {
                            let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                            let requestEmailPdf = me.getRenderRequest($form);
                            requestEmailPdf.renderMode = 'Email';
                            requestEmailPdf.email.to = '[me]';
                            requestEmailPdf.email.cc = '';
                            requestEmailPdf.email.subject = '[reportname]';
                            requestEmailPdf.email.body = '';
                            requestEmailPdf.parameters = this.getParameters($form);
                            FwAppData.apiMethod(true, 'POST', this.apiurl + '/render', requestEmailPdf, timeout, (successResponse) => {
                                try {
                                    FwNotification.renderNotification('SUCCESS', 'Email Sent');
                                }
                                catch (ex) {
                                    FwFunc.showError(ex);
                                }
                                finally {
                                    FwNotification.closeNotification($notification);
                                }
                            }, (errorResponse) => {
                                FwNotification.closeNotification($notification);
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
            $btnDownloadExcel.on('click', (event) => {
                try {
                    let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    let request = me.getRenderRequest($form);
                    request.downloadPdfAsAttachment = true;
                    FwAppData.apiMethod(true, 'POST', this.apiurl + '/render', request, timeout, (successResponse) => {
                        try {
                            let $iframe = jQuery('<iframe style="display:none;" />');
                            jQuery('.application').append($iframe);
                            $iframe.attr('src', successResponse.downloadurl);
                            setTimeout(function () {
                                $iframe.remove();
                            }, 500);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                        finally {
                            FwNotification.closeNotification($notification);
                        }
                    }, (errorResponse) => {
                        FwNotification.closeNotification($notification);
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
    }
    getParameters($form) {
        let parameters = null;
        let isvalid = FwModule.validateForm($form);
        if (isvalid) {
            parameters = {};
            let $fields = $form.find('div[data-control="FwFormField"]');
            $fields.each(function (index, element) {
                let $field = jQuery(element);
                if (typeof $field.attr('data-datafield') === 'string') {
                    parameters[$field.attr('data-datafield')] = FwFormField.getValue2($field);
                }
            });
        }
        else {
            throw 'Please fill in the required fields.';
        }
        return parameters;
    }
    getEmailTemplate() {
        return `
            <div style="width:540px;">
              <div class="formrow">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="from" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield from" data-caption="From" data-enabled="true"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="tousers" data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield tousers" data-caption="To (Users)" data-validationname="PersonValidation" data-hasselectall="false" style="float:left;box-sizing:border-box;width:20%;"></div>
                  <div data-datafield="to" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield to" data-caption="To" data-enabled="true" style="float:left;box-sizing:border-box;width:80%;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="ccusers" data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield ccusers" data-caption="CC (Users)" data-validationname="PersonValidation"  data-hasselectall="false" style="float:left;box-sizing:border-box;width:20%;"></div>
                  <div data-datafield="cc" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield cc" data-caption="CC" data-enabled="true" style="float:left;box-sizing:border-box;width:80%;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="subject" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield subject" data-caption="Subject" data-enabled="true"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="body" data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield message" data-caption="Message" data-enabled="true"></div>
                </div>
              </div>
            </div>
        `;
    }
}
class ReportPageMessage {
    constructor() {
        this.action = 'None';
        this.apiUrl = '';
        this.authorizationHeader = '';
        this.request = new RenderRequest();
    }
}
class RenderRequest {
    constructor() {
        this.parameters = {};
        this.email = new EmailInfo();
        this.uniqueid = '';
        this.downloadPdfAsAttachment = false;
    }
}
class RenderResponse {
    constructor() {
        this.htmlReportUrl = '';
        this.pdfReportUrl = '';
    }
}
class EmailInfo {
    constructor() {
        this.from = '';
        this.to = '';
        this.cc = '';
        this.subject = '';
        this.body = '';
    }
}
//# sourceMappingURL=FwWebApiReport.js.map