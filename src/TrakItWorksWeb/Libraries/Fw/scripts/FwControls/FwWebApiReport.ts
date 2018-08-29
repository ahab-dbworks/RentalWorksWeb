//----------------------------------------------------------------------------------------------
class FwWebApiReport {
    Module: string;
    reportName: string = '';
    apiurl: string = '';
    frontEndHtml: string = '';
    reportOptions: {
        HasExportHtml: boolean,
        HasExportPdf: boolean,
        HasEmailHtml: boolean,
        HasEmailPdf: boolean,
        HasEmailMePdf: boolean,
        HasDownloadExcel: boolean
    };
    //----------------------------------------------------------------------------------------------
    constructor(reportName, apiurl, frontEndHtml) {
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
    //----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    getRenderRequest($form) {
        let request = new RenderRequest();
        if (typeof $form.data('getRenderRequest') === 'function') {
            request = $form.data('getRenderRequest')(request);
        }
        return request;
    }
    //----------------------------------------------------------------------------------------------
    //getDownloadExcelRequest($form) {
    //    let request: any = {
    //        parameters: {}
    //    };
    //    if (typeof $form.data('getexportrequest') === 'function') {
    //        request = $form.data('getexportrequest')(request);
    //    }
    //    return request;
    //}
    //----------------------------------------------------------------------------------------------
    load($form: JQuery, reportOptions) {
        let formid = program.uniqueId(8);
        let $fwcontrols = $form.find('.fwcontrol').addBack();
        FwControl.renderRuntimeControls($fwcontrols);
        FwControl.setIds($fwcontrols, formid);
        this.addReportMenu($form, reportOptions);
        $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]'));
        $form.data('fields', $form.find('.fwformfield[data-isuniqueid!="true"]'));
    }
    //----------------------------------------------------------------------------------------------
    addReportMenu($form, reportOptions) {
        let me = this;
        let $menuObject = FwMenu.getMenuControl('default');
        let timeout = 7200; // 2 hour timeout for the ajax request
        let urlHtmlReport = `${applicationConfig.apiurl}Reports/${me.reportName}/index.html`;
        let apiUrl = applicationConfig.apiurl.substring(0, applicationConfig.apiurl.length - 1);
        let authorizationHeader = 'Bearer ' + sessionStorage.getItem('apiToken');

        // Preview Button
        if ((typeof reportOptions.HasExportHtml === 'undefined') || (reportOptions.HasExportHtml === true)) {
            let $btnPreview = FwMenu.addStandardBtn($menuObject, 'Preview');
            $btnPreview.on('click', (event: JQuery.Event) => {
                try {
                    //let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Preview...');
                    let request = me.getRenderRequest($form);
                    request.renderMode = 'Html';
                    request.parameters = this.getParameters($form);
                    //if (typeof request.uniqueid === 'undefined' || request.uniqueid === null) {
                    //    throw 'request.uniqueid is required.'; // you need to implement: $form.data('getRenderRequest', function() {});
                    //}
                    let win = window.open(urlHtmlReport);
                    if (!win) {
                        throw 'Please disable your popup blocker for this site!';
                    } else {
                        setTimeout(() => {
                            let message = new ReportPageMessage();
                            message.action = 'Preview';
                            message.apiUrl = apiUrl;
                            message.authorizationHeader = authorizationHeader;
                            message.request = request;
                            win.postMessage(message, '*');
                        }, 50);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }

        // Print HTML button
        if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
            let $btnPrintPdf = FwMenu.addStandardBtn($menuObject, 'Print HTML');
            $btnPrintPdf.on('click', (event: JQuery.Event) => {
                try {
                    //let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Preview...');
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
                            (<any>$iframe[0]).contentWindow.postMessage(message, '*');
                        }, 0);
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        FwMenu.addVerticleSeparator($menuObject);

        // View PDF button
        if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
            let $btnOpenPdf = FwMenu.addStandardBtn($menuObject, 'View PDF');
            $btnOpenPdf.on('click', (event: JQuery.Event) => {
                var request, webserviceurl, $notification;
                try {
                    let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    let request = me.getRenderRequest($form);
                    request.renderMode = 'Pdf';
                    request.downloadPdfAsAttachment = false;
                    request.parameters = this.getParameters($form);
                    FwAppData.apiMethod(true, 'POST', me.apiurl + '/render', request, timeout,
                        (successResponse: RenderResponse) => {
                            try {
                                let win = window.open(successResponse.pdfReportUrl);
                                let startTime = new Date();
                                let setWindowTitle = () => {
                                    if (win.document) // If loaded
                                    {
                                        win.document.title = "Report (PDF)";
                                    }
                                    else // If not loaded yet
                                    {
                                        setTimeout(setWindowTitle, 10); // Recheck again every 10 ms
                                    }
                                }
                                setWindowTitle();
                                if (!win) throw 'Please disable your popup blocker for this site!';
                            } catch (ex) {
                                FwFunc.showError(ex);
                            } finally {
                                FwNotification.closeNotification($notification);
                            }
                        },
                        (errorResponse) => {
                            FwNotification.closeNotification($notification);
                            if (errorResponse !== 'abort') {
                                FwFunc.showError(errorResponse);
                            }
                        }, null);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }

        // Download PDF button
        if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
            let $btnDownloadPdf = FwMenu.addStandardBtn($menuObject, 'Download PDF');
            $btnDownloadPdf.on('click', (event: JQuery.Event) => {
                try {
                    let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    let request = me.getRenderRequest($form);
                    request.renderMode = 'Pdf';
                    request.downloadPdfAsAttachment = true;
                    request.parameters = this.getParameters($form);
                    FwAppData.apiMethod(true, 'POST', me.apiurl + '/render', request, timeout,
                        (successResponse: RenderResponse) => {
                            try {
                                let win = window.open(successResponse.pdfReportUrl);
                                let startTime = new Date();
                                let setWindowTitle = () => {
                                    if (win.document) // If loaded
                                    {
                                        win.document.title = "Report (PDF)";
                                    }
                                    else // If not loaded yet
                                    {
                                        setTimeout(setWindowTitle, 10); // Recheck again every 10 ms
                                    }
                                }
                                setWindowTitle();
                                if (!win) throw 'Please disable your popup blocker for this site!';
                            } catch (ex) {
                                FwFunc.showError(ex);
                            } finally {
                                FwNotification.closeNotification($notification);
                            }
                        },
                        (errorResponse) => {
                            FwNotification.closeNotification($notification);
                            if (errorResponse !== 'abort') {
                                FwFunc.showError(errorResponse);
                            }
                        }, null);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }

        FwMenu.addVerticleSeparator($menuObject);

        // E-mail (to me)
        if ((typeof reportOptions.HasEmailMePdf === 'undefined') || (reportOptions.HasEmailMePdf === true)) {
            let $btnEmailMePdf = FwMenu.addStandardBtn($menuObject, 'E-mail (to me)');
            $btnEmailMePdf.on('click', (event: JQuery.Event) => {
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
                    FwAppData.apiMethod(true, 'POST', me.apiurl + '/render', request, timeout,
                        (successResponse: RenderResponse) => {
                            try {

                            } catch (ex) {
                                FwFunc.showError(ex);
                            } finally {
                                FwNotification.closeNotification($notification);
                            }
                        },
                        (errorResponse) => {
                            FwNotification.closeNotification($notification);
                            if (errorResponse !== 'abort') {
                                FwFunc.showError(errorResponse);
                            }
                        }, null);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }

        // E-mail button
        if ((typeof reportOptions.HasEmailPdf === 'undefined') || (reportOptions.HasEmailPdf === true)) {
            let $btnEmailPdf = FwMenu.addStandardBtn($menuObject, 'E-mail');
            $btnEmailPdf.on('click', (event: JQuery.Event) => {
                try {
                    let $confirmation = FwConfirmation.renderConfirmation(FwLanguages.translate('E-mail PDF'), '');
                    FwConfirmation.addControls($confirmation, this.getEmailTemplate());
                    let email = '[me]';
                    if (sessionStorage.getItem('email') !== null) {
                        email = sessionStorage.getItem('email');
                    }
                    FwFormField.setValueByDataField($confirmation, 'from', email);
                    $confirmation.find('.tousers').data('onchange', function ($selectedRows) {
                        let emailArray = new Array<string>();
                        for (let i = 0; i < $selectedRows.length; i++) {
                            let $tr = $selectedRows[i];
                            let email = $tr.find('[data-cssclass="Email"]').text();
                            emailArray.push(email);
                        }
                        FwFormField.setValueByDataField($confirmation, 'to', emailArray.join('; '));
                        FwFormField.setValueByDataField($confirmation, 'tousers', '', '');
                    });
                    $confirmation.find('.ccusers').data('onchange', function ($selectedRows) {
                        let emailArray = new Array<string>();
                        for (let i = 0; i < $selectedRows.length; i++) {
                            let $tr = $selectedRows[i];
                            let email = $tr.find('[data-cssclass="Email"]').text();
                            emailArray.push(email);
                        }
                        FwFormField.setValueByDataField($confirmation, 'cc', emailArray.join('; '));
                        FwFormField.setValueByDataField($confirmation, 'ccusers', '', '');
                    });
                    FwFormField.setValueByDataField($confirmation, 'subject', FwTabs.getTabByElement($form).attr('data-caption'));
                    let $btnSend = FwConfirmation.addButton($confirmation, 'Send');
                    FwConfirmation.addButton($confirmation, 'Cancel');
                    $btnSend.click((event: JQuery.Event) => {
                        try {
                            let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                            let requestEmailPdf = me.getRenderRequest($form);
                                requestEmailPdf.renderMode = 'Email';
                                requestEmailPdf.email.to = '[me]';
                                requestEmailPdf.email.cc = '';
                                requestEmailPdf.email.subject = '[reportname]';
                                requestEmailPdf.email.body = '';
                                requestEmailPdf.parameters = this.getParameters($form);
                            FwAppData.apiMethod(true, 'POST', this.apiurl + '/render', requestEmailPdf, timeout,
                                (successResponse) => {
                                    try {
                                        FwNotification.renderNotification('SUCCESS', 'Email Sent');
                                    } catch (ex) {
                                        FwFunc.showError(ex);
                                    } finally {
                                        FwNotification.closeNotification($notification);
                                    }
                                },
                                (errorResponse) => {
                                    FwNotification.closeNotification($notification);
                                    if (errorResponse !== 'abort') {
                                        FwFunc.showError(errorResponse);
                                    }
                                }, $form);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    //$confirmation.on('change', '[data-datafield="tousers"] .fwformfield-value',
                    //    (event: JQuery.Event) => {
                    //        let requestGetEmailByWebUsersId: any = {};
                    //        requestGetEmailByWebUsersId.webusersids = (<HTMLInputElement>event.target).value;
                    //        requestGetEmailByWebUsersId.to = FwFormField.getValueByDataField($confirmation, 'to');
                    //        FwAppData.apiMethod(true, 'POST', this.apiurl + '/getemailbywebusersid', requestGetEmailByWebUsersId, timeout,
                    //            (successResponse) => {
                    //                try {
                    //                    FwFormField.setValueByDataField($confirmation, 'to', successResponse.emailto);
                    //                    FwFormField.setValueByDataField($confirmation, 'tousers', '', '');
                    //                } catch (ex) {
                    //                    FwFunc.showError(ex);
                    //                }
                    //            }, null, null);
                    //    });
                    //$confirmation.on('change', '[data-datafield="ccusers"] .fwformfield-value',
                    //    (event: JQuery.Event) => {
                    //        var requestGetEmailByWebUsersId: any = {};
                    //        requestGetEmailByWebUsersId.webusersids = (<HTMLInputElement>event.target).value;
                    //        requestGetEmailByWebUsersId.to = FwFormField.getValueByDataField($confirmation, 'cc');
                    //        FwAppData.apiMethod(true, 'POST', this.apiurl + '/getemailbywebusersid', requestGetEmailByWebUsersId, timeout,
                    //            (successResponse) => {
                    //                try {
                    //                    FwFormField.setValueByDataField($confirmation, 'cc', successResponse.emailto);
                    //                    FwFormField.setValueByDataField($confirmation, 'ccusers', '', '');
                    //                } catch (ex) {
                    //                    FwFunc.showError(ex);
                    //                }
                    //            }, null, null);
                    //    }
                    //);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }

        // Download Excel button
        FwMenu.addVerticleSeparator($menuObject);
        if ((typeof reportOptions.HasDownloadExcel === 'undefined') || (reportOptions.HasDownloadExcel === true)) {
            var $btnDownloadExcel = FwMenu.addStandardBtn($menuObject, 'Download Excel');
            $btnDownloadExcel.on('click', (event) => {
                try {
                    let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    let request = me.getRenderRequest($form);
                    //request.renderMode = 'excel';
                    request.downloadPdfAsAttachment = true;
                    FwAppData.apiMethod(true, 'POST', this.apiurl + '/render', request, timeout,
                        (successResponse) => {
                            try {
                                let $iframe = jQuery('<iframe style="display:none;" />');
                                jQuery('.application').append($iframe);
                                $iframe.attr('src', successResponse.downloadurl);
                                setTimeout(function () {
                                    $iframe.remove();
                                }, 500);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            } finally {
                                FwNotification.closeNotification($notification);
                            }
                        },
                        (errorResponse) => {
                            FwNotification.closeNotification($notification);
                            if (errorResponse !== 'abort') {
                                FwFunc.showError(errorResponse);
                            }
                        }, null);
                } catch (ex) {
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
    //----------------------------------------------------------------------------------------------
    getParameters($form: JQuery) {
        let parameters: any = null;
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
        } else {
            throw 'Please fill in the required fields.';
        }

        return parameters;
    }
    //----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
}

type ActionType =  'None' | 'Preview' | 'PrintHtml';
type RenderMode =  'Html' | 'Pdf' | 'Email';

class ReportPageMessage {
    action: ActionType = 'None';
    apiUrl: string = '';
    authorizationHeader: string = '';
    request: RenderRequest = new RenderRequest();
}

class RenderRequest {
    renderMode: RenderMode;
    parameters: any = {};
    email = new EmailInfo();
    uniqueid: string = '';
    downloadPdfAsAttachment: boolean = false;
}

class RenderResponse {
    renderMode: RenderMode;
    htmlReportUrl: string = '';
    pdfReportUrl: string = '';
}

class EmailInfo {
    from: string = '';
    to: string = '';
    cc: string = '';
    subject: string = '';
    body: string = '';
}

