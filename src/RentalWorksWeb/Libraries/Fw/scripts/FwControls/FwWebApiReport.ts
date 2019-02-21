//----------------------------------------------------------------------------------------------
abstract class FwWebApiReport {
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
            HasDownloadExcel: true
        };
    }
    abstract convertParameters(parameters: any);
    //----------------------------------------------------------------------------------------------
    getFrontEnd() {
        const $form = jQuery(this.frontEndHtml);
        $form.attr('data-reportname', this.reportName);
        $form.on('change', '.fwformfield[data-required="true"].error', function () {
            const $this = jQuery(this);
            const value = FwFormField.getValue2($this);
            if (value !== '') {
                $this.removeClass('error');
                if ($this.closest('.tabpage.active').has('.error').length === 0) {
                    const errorTab = $this.closest('.tabpage').attr('data-tabid');
                    $this.parents('.fwcontrol .fwtabs').find(`#${errorTab}`).removeClass('error');
                }
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
    load($form: JQuery, reportOptions) {
        const $fwcontrols = $form.find('.fwcontrol').addBack();
        FwControl.renderRuntimeControls($fwcontrols);
        const formId = program.uniqueId(8);
        FwControl.setIds($fwcontrols, formId);
        this.addReportMenu($form, reportOptions);
        $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]'));
        $form.data('fields', $form.find('.fwformfield[data-isuniqueid!="true"]'));
    }
    //----------------------------------------------------------------------------------------------
    addReportMenu($form, reportOptions) {
        let $menuObject = FwMenu.getMenuControl('default');
        let timeout = 7200; // 2 hour timeout for the ajax request
        let urlHtmlReport = `${applicationConfig.apiurl}Reports/${this.reportName}/index.html`;
        let apiUrl = applicationConfig.apiurl.substring(0, applicationConfig.apiurl.length - 1);
        let authorizationHeader = `Bearer ${sessionStorage.getItem('apiToken')}`;

        // Preview Button
        if ((typeof reportOptions.HasExportHtml === 'undefined') || (reportOptions.HasExportHtml === true)) {
            let $btnPreview = FwMenu.addStandardBtn($menuObject, 'Preview');
            FwMenu.addVerticleSeparator($menuObject);
            $btnPreview.on('click', (event: JQuery.Event) => {
                try {
                    let request = this.getRenderRequest($form);
                    request.renderMode = 'Html';
                    request.parameters = this.convertParameters(this.getParameters($form));
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
            const $btnPrintPdf = FwMenu.addStandardBtn($menuObject, 'Print HTML');
            FwMenu.addVerticleSeparator($menuObject);
            $btnPrintPdf.on('click', (event: JQuery.Event) => {
                try {
                    const request: any = this.getRenderRequest($form);
                    request.renderMode = 'Html';
                    request.parameters = this.convertParameters(this.getParameters($form));
                    const $iframe = jQuery(`<iframe src="${urlHtmlReport}" style="display:none;"></iframe>`);
                    jQuery('.application').append($iframe);
                    $iframe.on('load', () => {
                        setTimeout(() => {
                            const message: any = new ReportPageMessage();
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
        // Download Excel button
        if ((typeof reportOptions.HasDownloadExcel === 'undefined') || (reportOptions.HasDownloadExcel === true)) {
            const $btnDownloadExcel = FwMenu.addStandardBtn($menuObject, 'Download Excel');
            FwMenu.addVerticleSeparator($menuObject);
            $btnDownloadExcel.on('click', event => {
                try {
                    const $confirmation = FwConfirmation.renderConfirmation('Download Excel Workbook', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');

                    const html: Array<string> = [];
                    html.push(`<div class="fwform" data-controller="none" style="background-color: transparent;">`);
                    html.push(`  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">`);
                    html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield sub-headings" data-caption="Include Sub Headings and Sub Totals" data-datafield="" style="float:left;width:100px;"></div>`);
                    html.push(`  </div>`);
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield ID-col" data-caption="Include ID columns" data-datafield="" style="float:left;width:100px;"></div>`);
                    html.push('  </div>');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield color-col" data-caption="Include Color columns" data-datafield="" style="float:left;width:100px;"></div>`);
                    html.push('  </div>');
                    html.push(`</div>`);

                    FwConfirmation.addControls($confirmation, html.join(''));
                    const $yes = FwConfirmation.addButton($confirmation, 'Download', false);
                    const $no = FwConfirmation.addButton($confirmation, 'Cancel');
                    $confirmation.find('.sub-headings input').prop('checked', false);
                    const request: any = this.getRenderRequest($form);
                    request.downloadPdfAsAttachment = true;
                    $yes.on('click', () => {
                        $confirmation.find('.sub-headings input').prop('checked') === true ? request.IncludeSubHeadingsAndSubTotals = true : request.IncludeSubHeadingsAndSubTotals = false;
                        let includeIdColumns: boolean;
                        $confirmation.find('.ID-col input').prop('checked') === true ? includeIdColumns = true : includeIdColumns = false;
                        request.IncludeIdColumns = includeIdColumns;
                        let includeColorColumns: boolean;
                        $confirmation.find('.color-col input').prop('checked') === true ? includeColorColumns = true : includeColorColumns = false;
                        request.IncludeColorColumns = includeColorColumns;

                        const convertedparameters = this.convertParameters(this.getParameters($form));
                        for (let key in convertedparameters) {
                            request[key] = convertedparameters[key];
                        }
                        FwAppData.apiMethod(true, 'POST', `${this.apiurl}/exportexcelxlsx/${this.reportName}`, request, timeout,
                            (successResponse) => {
                                try {
                                    const $iframe = jQuery(`<iframe src="${applicationConfig.apiurl}${successResponse.downloadUrl}" style="display:none;"></iframe>`);
                                    jQuery('#application').append($iframe);
                                    setTimeout(function () {
                                        $iframe.remove();
                                    }, 500);
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            },
                            (errorResponse) => {
                                if (errorResponse !== 'abort') {
                                    FwFunc.showError(errorResponse);
                                }
                            }, null);
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwNotification.renderNotification('INFO', 'Downloading Excel Workbook...');
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        // View PDF button
        if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
            const $btnOpenPdf = FwMenu.addStandardBtn($menuObject, 'View PDF');
            FwMenu.addVerticleSeparator($menuObject);
            $btnOpenPdf.on('click', (event: JQuery.Event) => {
                try {
                    const request: any = this.getRenderRequest($form);
                    request.renderMode = 'Pdf';
                    request.downloadPdfAsAttachment = false;
                    request.parameters = this.convertParameters(this.getParameters($form));
                    const win = window.open('about:blank', 'newtab');
                    let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    //document.appendChild($notification)
                    FwAppData.apiMethod(true, 'POST', `${this.apiurl}/render`, request, timeout,
                        (successResponse: RenderResponse) => {
                            try {
                                win.location.href = successResponse.pdfReportUrl;
                                FwNotification.renderNotification('INFO', 'Downloading Excel Workbook...');
                                if (win == null) throw 'Unable to open the report in a new window. Check your popup blocker.'
                                var setWindowTitle = () => {
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
                                if (!win) throw 'Disable your popup blocker for this site!';
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
        //if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
        //    let $btnDownloadPdf = FwMenu.addStandardBtn($menuObject, 'Download PDF');
        //    FwMenu.addVerticleSeparator($menuObject);
        //    $btnDownloadPdf.on('click', (event: JQuery.Event) => {
        //        try {
        //            let $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
        //            let request = this.getRenderRequest($form);
        //            request.renderMode = 'Pdf';
        //            request.downloadPdfAsAttachment = true;
        //            request.parameters = this.getParameters($form);
        //            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/render`, request, timeout,
        //                (successResponse: RenderResponse) => {
        //                    try {
        //                        let win = window.open(successResponse.pdfReportUrl);
        //                        let startTime = new Date();
        //                        let setWindowTitle = () => {
        //                            if (win.document) // If loaded
        //                            {
        //                                win.document.title = "Report (PDF)";
        //                            }
        //                            else // If not loaded yet
        //                            {
        //                                setTimeout(setWindowTitle, 10); // Recheck again every 10 ms
        //                            }
        //                        }
        //                        setWindowTitle();
        //                        if (!win) throw 'Please disable your popup blocker for this site!';
        //                    } catch (ex) {
        //                        FwFunc.showError(ex);
        //                    } finally {
        //                        FwNotification.closeNotification($notification);
        //                    }
        //                },
        //                (errorResponse) => {
        //                    FwNotification.closeNotification($notification);
        //                    if (errorResponse !== 'abort') {
        //                        FwFunc.showError(errorResponse);
        //                    }
        //                }, null);
        //        } catch (ex) {
        //            FwFunc.showError(ex);
        //        }
        //    });
        //}
        // E-mail (to me)
        if ((typeof reportOptions.HasEmailMePdf === 'undefined') || (reportOptions.HasEmailMePdf === true)) {
            const $btnEmailMePdf = FwMenu.addStandardBtn($menuObject, 'E-mail (to me)');
            FwMenu.addVerticleSeparator($menuObject);
            $btnEmailMePdf.on('click', (event: JQuery.Event) => {
                try {
                    const $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                    const request: any = this.getRenderRequest($form);
                    request.renderMode = 'Email';
                    request.email.from = '[me]';
                    request.email.to = '[me]';
                    request.email.cc = '';
                    request.email.subject = '[reportname]';
                    request.email.body = '';
                    request.parameters = this.convertParameters(this.getParameters($form));
                    FwAppData.apiMethod(true, 'POST', `${this.apiurl}/render`, request, timeout,
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
            const $btnEmailPdf = FwMenu.addStandardBtn($menuObject, 'E-mail');
            $btnEmailPdf.on('click', (event: JQuery.Event) => {
                try {
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const $confirmation = FwConfirmation.renderConfirmation(FwLanguages.translate('E-mail PDF'), '');
                        FwConfirmation.addControls($confirmation, this.getEmailTemplate());
                        const $btnSend = FwConfirmation.addButton($confirmation, 'Send');
                        FwConfirmation.addButton($confirmation, 'Cancel');

                        let email = '[me]';
                        if (sessionStorage.getItem('email') !== null && sessionStorage.getItem('email') !== '') {
                            email = sessionStorage.getItem('email');
                        } else {
                            FwNotification.renderNotification('ERROR', 'Add an email to your account to enable this function.');
                            $btnSend.css({ 'pointer-events': 'none', 'background-color': 'light-gray' });
                        }
                        FwFormField.setValueByDataField($confirmation, 'from', email);
                        FwFormField.setValueByDataField($confirmation, 'subject', FwTabs.getTabByElement($form).attr('data-caption'));

                        $btnSend.click((event: JQuery.Event) => {
                            try {
                                const $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                                const requestEmailPdf: any = this.getRenderRequest($form);
                                requestEmailPdf.renderMode = 'Email';
                                requestEmailPdf.email.from = FwFormField.getValueByDataField($confirmation, 'from');
                                requestEmailPdf.email.to = $confirmation.find('[data-datafield="tousers"] input.fwformfield-text').val();
                                requestEmailPdf.email.cc = $confirmation.find('[data-datafield="ccusers"] input.fwformfield-text').val();
                                requestEmailPdf.email.subject = FwFormField.getValueByDataField($confirmation, 'subject');
                                requestEmailPdf.email.body = FwFormField.getValueByDataField($confirmation, 'body');
                                requestEmailPdf.parameters = this.convertParameters(this.getParameters($form));
                                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/render`, requestEmailPdf, timeout,
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
                    }
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

        if (typeof window[$form.attr('data-controller')].addReportMenuItems === 'function') {
            $menuObject = window[$form.attr('data-controller')].addReportMenuItems($menuObject, $form);
        }

        FwControl.renderRuntimeControls($menuObject.find('.fwcontrol').addBack());

        $form.find('.fwform-menu').append($menuObject);
    }
    //----------------------------------------------------------------------------------------------
    getParameters($form: JQuery): any {
        try {
            const parameters: any = {};
            const isvalid = FwModule.validateForm($form);
            if (isvalid) {
                const $fields = $form.find('div[data-control="FwFormField"]');
                $fields.each(function (index, element) {
                    const $field = jQuery(element);
                    if (typeof $field.attr('data-datafield') === 'string') {
                        parameters[$field.attr('data-datafield')] = FwFormField.getValue2($field);
                    }
                });
            } else {
                throw 'Please fill in the required fields.';
            }
            return parameters;
        } catch (ex) {
            FwFunc.showError(ex)
        }
    }
    //----------------------------------------------------------------------------------------------
    getEmailTemplate() {
        return `
            <div style="width:540px;">
              <div class="formrow">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="from" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield from" data-caption="From" data-enabled="false"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="tousers" data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield tousers email" data-caption="To (Users)" data-validationname="PersonValidation" data-hasselectall="false" style="box-sizing:border-box;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="ccusers" data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield ccusers email" data-caption="CC (Users)" data-validationname="PersonValidation"  data-hasselectall="false" style="box-sizing:border-box;"></div>
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

type ActionType = 'None' | 'Preview' | 'PrintHtml';
type RenderMode = 'Html' | 'Pdf' | 'Email';

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
    IncludeSubHeadingsAndSubTotals: boolean;
    IncludeColorColumns: boolean;
    IncludeIdColumns: boolean;
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