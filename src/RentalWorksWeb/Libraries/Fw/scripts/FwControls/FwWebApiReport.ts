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
        this.addSettingsTab($form);
        $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]'));
        $form.data('fields', $form.find('.fwformfield[data-isuniqueid!="true"]'));
    }
    //----------------------------------------------------------------------------------------------
    addReportMenu($form, reportOptions) {
        let $menuObject = FwMenu.getMenuControl('default');
        const timeout = 7200; // 2 hour timeout for the ajax request
        const urlHtmlReport = `${applicationConfig.apiurl}Reports/${this.reportName}/index.html`;
        const apiUrl = applicationConfig.apiurl.substring(0, applicationConfig.apiurl.length - 1);
        const authorizationHeader = `Bearer ${sessionStorage.getItem('apiToken')}`;
        let companyName;
        if (JSON.parse(sessionStorage.getItem('controldefaults')).companyname != null) {
            companyName = JSON.parse(sessionStorage.getItem('controldefaults')).companyname;
        }
        
        // Preview Button
        if ((typeof reportOptions.HasExportHtml === 'undefined') || (reportOptions.HasExportHtml === true)) {
            const $btnPreview = FwMenu.addStandardBtn($menuObject, 'Preview');
            FwMenu.addVerticleSeparator($menuObject);
            $btnPreview.on('click', (event: JQuery.Event) => {
                try {
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const request: any = this.getRenderRequest($form);
                        request.renderMode = 'Html';
                        request.parameters = this.convertParameters(this.getParameters($form));
                        request.parameters.companyName = companyName;
                        const reportPageMessage = new ReportPageMessage();
                        reportPageMessage.action = 'Preview';
                        reportPageMessage.apiUrl = apiUrl;
                        reportPageMessage.authorizationHeader = authorizationHeader;
                        reportPageMessage.request = request;

                        const win = window.open(urlHtmlReport);

                        if (!win) {
                            throw 'Disable your popup blocker for this site.';
                        } else {
                            const sendMessage = (event) => {
                                const message = event.data;
                                if (message === urlHtmlReport) {
                                    win.postMessage(reportPageMessage, urlHtmlReport);
                                }
                                if (message === 'ReportUnload') { // removing listener clears window but removes ability to refresh reports
                                    window.removeEventListener('message', sendMessage)
                                }
                            }
                            window.addEventListener('message', sendMessage) // messsage is from new tab (rendered report) indicating page fully loaded. -webpackreport.ts
                        }
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
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const request: any = this.getRenderRequest($form);
                        request.renderMode = 'Html';
                        request.parameters = this.convertParameters(this.getParameters($form));
                        request.parameters.companyName = companyName;
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
                    }
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
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const $confirmation = FwConfirmation.renderConfirmation('Download Excel Workbook', '');
                        $confirmation.find('.fwconfirmationbox').css('width', '450px');

                        const html: Array<string> = [];
                        html.push(`<div class="fwform" data-controller="none" style="background-color: transparent;">`);
                        html.push(`  <div class="flexrow">`);
                        html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield sub-headings" data-caption="Include Sub Headings and Sub Totals" data-datafield="" style="flex: 0 1 200px;"></div>`);
                        html.push(`  </div>`);
                        html.push(`  <div class="flexrow">`);
                        html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield ID-col" data-caption="Include ID columns" data-datafield="" style="flex: 0 1 200px;margin-top:-5px;"></div>`);
                        html.push('  </div>');
                        html.push(`</div>`);

                        FwConfirmation.addControls($confirmation, html.join(''));
                        const $yes = FwConfirmation.addButton($confirmation, 'Download', false);
                        const $no = FwConfirmation.addButton($confirmation, 'Cancel');
                        $confirmation.find('.sub-headings input').prop('checked', false);
                        const request: any = this.getRenderRequest($form);
                        request.downloadPdfAsAttachment = true;
                        const convertedparameters = this.convertParameters(this.getParameters($form));
                        for (let key in convertedparameters) {
                            request[key] = convertedparameters[key];
                        }
                        $yes.on('click', () => {
                            $confirmation.find('.sub-headings input').prop('checked') === true ? request.IncludeSubHeadingsAndSubTotals = true : request.IncludeSubHeadingsAndSubTotals = false;
                            let includeIdColumns: boolean;
                            $confirmation.find('.ID-col input').prop('checked') === true ? includeIdColumns = true : includeIdColumns = false;
                            request.IncludeIdColumns = includeIdColumns;
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
                    }
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
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const request: any = this.getRenderRequest($form);
                        request.renderMode = 'Pdf';
                        request.downloadPdfAsAttachment = false;
                        request.parameters = this.convertParameters(this.getParameters($form));
                        request.parameters.companyName = companyName;
                        const win = window.open('', '_blank');
                        const head = win.document.head || win.document.getElementsByTagName('head')[0];
                        const loader = jQuery(win.document.body.innerHTML = '<div class="loader-container"><div class="loader"></div></div>');
                        const loaderStyle = win.document.createElement('style');
                        loaderStyle.innerHTML = `
                            html, body { min-height:100% !important; }
                            .loader-container { position:absolute;top:0;right:0;left:0;bottom:0;display:flex;align-items:center;justify-content:center; }
                            .loader { border: 16px solid #f3f3f3;border-top: 16px solid #3498db;border-radius:50%;width:120px;height:120px;animation:spin 2s linear infinite; }
                            @keyframes spin {
                                0% { transform: rotate(0deg); }
                                100% { transform: rotate(360deg); }
                            }`;
                        head.appendChild(loaderStyle);
                        FwAppData.apiMethod(true, 'POST', `${this.apiurl}/render`, request, timeout,
                            (successResponse: RenderResponse) => {
                                try {
                                    win.location.href = successResponse.pdfReportUrl;
                                    if (win == null) throw 'Unable to open the report in a new window. Check your popup blocker.'
                                    const setWindowTitle = () => {
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
                                    if (!win) throw 'Disable your popup blocker for this site.';
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            },
                            (errorResponse) => {
                                if (errorResponse !== 'abort') {
                                    FwFunc.showError(errorResponse);
                                }
                            }, null);
                    }
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
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const request: any = this.getRenderRequest($form);
                        request.renderMode = 'Email';
                        request.email.from = '[me]';
                        request.email.to = '[me]';
                        request.email.cc = '';
                        request.email.subject = '[reportname]';
                        request.email.body = '';
                        request.parameters = this.convertParameters(this.getParameters($form));

                        const $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                        request.parameters.companyName = companyName;
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
                    }
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
                                if (requestEmailPdf.parameters != null) {
                                    requestEmailPdf.parameters.companyName = companyName;
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
                                }
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
    addSettingsTab($form: JQuery): any {
        //create new "Settings" tab and tabpage
        const $formTabControl = $form.find('.fwtabs');
        const settingsTabId = FwTabs.addTab($formTabControl, 'Settings', false, 'SETTINGS', false);
        const $settingsPage = jQuery(this.getSettingsTemplate());
        const $settingsTabPage = $formTabControl.find(`#${settingsTabId.tabpageid}`);
        FwControl.renderRuntimeControls($settingsPage.find('.fwcontrol'));
        $settingsTabPage.append($settingsPage);

        //render grid
        const $reportSettingsGrid = $form.find('div[data-grid="ReportSettingsGrid"]');
        const $reportSettingsGridControl = FwBrowse.loadGridFromTemplate('ReportSettingsGrid');
        $reportSettingsGrid.empty().append($reportSettingsGridControl);
        $reportSettingsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                WebUserId: JSON.parse(sessionStorage.getItem('userid')).webusersid
                , ReportName: $form.attr('data-reportname')
            }
        })
        FwBrowse.init($reportSettingsGridControl);
        FwBrowse.renderRuntimeHtml($reportSettingsGridControl);

        //load default settings
        let loadDefaults: boolean = true;
        FwBrowse.search($reportSettingsGridControl);
        $reportSettingsGridControl.data('afterdatabindcallback', function () {
            if (loadDefaults) {
                $form.find('.load-settings').click();
                loadDefaults = false;
            }
        });

        $settingsTabPage
            //Save settings
            .on('click', '.save-settings', e => {
                let description = FwFormField.getValueByDataField($settingsTabPage, 'Description');
                if (description === '') description = "(default)";
                const $settingsControls = $form.find('.fwformfield[data-savesetting!="false"]');
                let $settingsObj: any = [];
                if ($settingsControls.length > 0) {
                    for (let i = 0; i < $settingsControls.length; i++) {
                        let $this = jQuery($settingsControls[i]);
                        const datafield = $this.attr('data-datafield')
                        const type = $this.attr('data-type');
                        $settingsObj.push({
                            DataField: datafield
                            , DataType: type
                            , Value: FwFormField.getValue2($this)
                            , Text: FwFormField.getTextByDataField($form, datafield)
                        });
                    }
                    $settingsObj = JSON.stringify($settingsObj);

                    let request: any = {};
                    request = {
                        WebUserId: JSON.parse(sessionStorage.getItem('userid')).webusersid
                        , ReportName: $form.attr('data-reportname')
                        , Settings: $settingsObj
                        , Description: description
                    }
                    const $tr = $reportSettingsGridControl.find(`tr [data-originalvalue="${description}"]`);
                    if ($tr.length !== 0) request.Id = $tr.parents('tr').find('[data-browsedatafield="Id"]').attr('data-originalvalue');
                    FwAppData.apiMethod(true, 'POST', `api/v1/reportsettings`, request, FwServices.defaultTimeout,
                        (successResponse) => {
                            try {
                                FwBrowse.search($reportSettingsGridControl);
                                $reportSettingsGridControl.data('afterdatabindcallback', function () {
                                    FwBrowse.selectRow($reportSettingsGridControl,
                                        $reportSettingsGridControl.find(`[data-browsedatafield="Id"][data-originalvalue="${successResponse.Id}"]`).parents('tr'));
                                });
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        },
                        null, $form);
                } else {
                    FwNotification.renderNotification('WARNING', 'There are no settings to save.');
                }
            })
            //Load settings
            .on('click', '.load-settings', e => {
                let $tr = $reportSettingsGridControl.find('tr.selected');
                let $settings: any;
                if ($tr.length === 0) { //if none are selected, choose default
                    $tr = $reportSettingsGridControl.find(`tr [data-originalvalue="(default)"]`).parents('tr');
                }
                if ($tr.length !== 0) {
                    $settings = $tr.find('[data-browsedatafield="Settings"]').attr('data-originalvalue');
                    $settings = JSON.parse($settings);
                    for (let i = 0; i < $settings.length; i++) {
                        let item = $settings[i];
                        if (item.DataType === "checkboxlist") {
                            const $checkBoxList = $form.find(`[data-type="checkboxlist"][data-datafield="${item.DataField}"] ol`);
                            $checkBoxList.find('li').attr('data-selected', 'F');
                            $checkBoxList.find('input[type="checkbox"]').prop('checked', false);
                            const checkedValues = item.Value;
                            for (let j = 0; j < checkedValues.length; j++) {
                                $checkBoxList
                                    .find(`li[data-value="${checkedValues[j].value}"]`)
                                    .attr('data-selected', 'T');
                                $checkBoxList
                                    .find(`li[data-value="${checkedValues[j].value}"] input[type="checkbox"]`)
                                    .prop('checked', true);
                            }
                        } else {
                            FwFormField.setValueByDataField($form, $settings[i].DataField, $settings[i].Value, $settings[i].Text);
                        }
                    }
                }
                if (typeof window[$form.attr('data-controller')] !== 'undefined') {
                    const controller = window[$form.attr('data-controller')];
                    if (typeof controller.afterLoad === 'function') {
                        controller.afterLoad($form);
                    }
                }
            });

        //save to "default" when opening a report
        $form.on('click', '.fwform-menu .buttonbar .btn', e => {
            $form.find('.save-settings').click();
        });
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
    getSettingsTemplate() {
        return `
            <div style="width:700px;">
                <div class="flexrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Save current report settings as:" data-datafield="Description" style="max-width:600px; margin:10px;"></div>
                    <div class="fwformcontrol save-settings" data-type="button" style="max-width:60px; margin-top:20px; margin-left:10px;">
                        <i class="material-icons" style="padding-top:5px; margin:0px -10px;">save</i>
                        <span style="float:right; padding-left:10px;">Save</span>
                    </div>
                </div>
                <div class="flexrow settings-grid">
                    <div data-control="FwGrid" data-grid="ReportSettingsGrid"></div>
                    <div class="fwformcontrol load-settings" data-type="button" style="max-width:60px; margin-top:15px; margin-left:10px;">
                        <i class="material-icons" style="padding-top:5px; margin:0px -10px;">open_in_browser</i>
                        <span style="float:right; padding-left:10px;">Load</span>
                    </div>
                </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getEmailTemplate() {
        return `
              <div style="width:540px;">
              <div class="formrow">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="from" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield from" data-caption="From" data-allcaps="false" data-enabled="false"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="tousers" data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield tousers email" data-allcaps="false" data-caption="To (Users)" data-validationname="PersonValidation" data-hasselectall="false" style="box-sizing:border-box;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="ccusers" data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield ccusers email" data-allcaps="false" data-caption="CC (Users)" data-validationname="PersonValidation"  data-hasselectall="false" style="box-sizing:border-box;"></div>
               </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="subject" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield subject" data-caption="Subject" data-allcaps="false" data-enabled="true"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="body" data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield message" data-caption="Message" data-allcaps="false" data-enabled="true"></div>
                </div>
              </div>
            </div>`;
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