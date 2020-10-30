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
    designerProvisioned: boolean;
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
        designerProvisioned: false;
    }
    abstract convertParameters(parameters: any);
    //----------------------------------------------------------------------------------------------
    getFrontEnd() {
        const $form = jQuery(this.frontEndHtml);
        $form.attr('data-reportname', this.reportName);
        $form.on('change', '.fwformfield[data-required="true"].error', function () {
            const $this = jQuery(this);
            const value = FwFormField.getValue2($this);
            if (value !== '' && !$this.hasClass('dev-err')) {
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
        //let companyName = '';
        //if (sessionStorage.getItem('controldefaults') !== null) {
        //    const controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
        //    if (typeof controlDefaults !== 'undefined' && typeof controlDefaults.companyname === 'string') {
        //        companyName = JSON.parse(sessionStorage.getItem('controldefaults')).companyname;
        //    }
        //}

        //justin hoffman 09/04/2020
        let companyName = 'UNKNOWN COMPANY';
        let systemName = 'UNKNOWN SYSTEM';
        if (sessionStorage.getItem('controldefaults') !== null) {
            const controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
            if (typeof controlDefaults !== 'undefined') {
                if (typeof controlDefaults.companyname === 'string') {
                    companyName = controlDefaults.companyname;
                }
                if (typeof controlDefaults.systemname === 'string') {
                    systemName = controlDefaults.systemname;
                }
            }
        }

        if (companyName === '' && sessionStorage.getItem('clientCode') !== null) {
            companyName = sessionStorage.getItem('clientCode');
        }

        // Preview Button
        if ((typeof reportOptions.HasExportHtml === 'undefined') || (reportOptions.HasExportHtml === true)) {
            const $btnPreview = FwMenu.addStandardBtn($menuObject, 'Preview');
            FwMenu.addVerticleSeparator($menuObject);
            $btnPreview.on('click', async (event: JQuery.Event) => {
                try {
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const request: any = this.getRenderRequest($form);
                        request.renderMode = 'Html';
                        request.parameters = await this.getParameters($form).then((value) => this.convertParameters(value));
                        request.parameters.companyName = companyName;
                        request.parameters.systemName = systemName;
                        request.parameters.action = 'Preview';
                        //set orderno as a parameter from front end if the orderid text box exists, some reports are not getting orderno from db.
                        if (request.parameters.hasOrderNo) {
                            request.parameters.orderno = $form.find(`div.fwformfield[data-datafield="OrderId"] .fwformfield-text`).val();
                        }

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
            $btnPrintPdf.on('click', async (event: JQuery.Event) => {
                try {
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const request: any = this.getRenderRequest($form);
                        request.renderMode = 'Html';
                        request.parameters = await this.getParameters($form).then((value) => this.convertParameters(value));
                        request.parameters.companyName = companyName;
                        request.parameters.systemName = systemName;
                        request.parameters.action = 'Print/PDF';
                        //set orderno as a parameter from front end if the orderid text box exists, some reports are not getting orderno from db.
                        if (request.parameters.hasOrderNo) {
                            request.parameters.orderno = $form.find(`div.fwformfield[data-datafield="OrderId"] .fwformfield-text`).val();
                        }

                        const $iframe = jQuery(`<iframe src="${urlHtmlReport}" style="display:none;"></iframe>`);
                        jQuery('.application').append($iframe);
                        $iframe.on('load', () => {
                            const message: any = new ReportPageMessage();
                            message.action = 'PrintHtml';
                            message.apiUrl = apiUrl;
                            message.authorizationHeader = authorizationHeader;
                            message.request = request;
                            $iframe[0].focus();
                            (<any>$iframe[0]).contentWindow.postMessage(message, '*');
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
            $btnDownloadExcel.on('click', async event => {
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
                        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield all-col" data-caption="Include All fields" data-datafield="" style="float:left;width:100px;"></div>`);
                        html.push('  </div>');
                        html.push('  <div class="flexrow fieldsrow" style="display:none;">');
                        html.push('  <div class="flexcolumn">');
                        html.push('  <div class="flexrow" style="padding:0 20px 0 7px;">');
                        html.push('  <div><div class="check-uncheck" style="color:#2626f3;cursor:pointer;float:left;">Uncheck All</div><div class="sort-list" style="color:#2626f3;cursor:pointer; float:right;">Sort List By Name</div></div>');
                        html.push('  </div>');
                        html.push('  <div class="flexrow">');
                        html.push(`    <div data-control="FwFormField" class="fwcontrol fwformfield" data-checkboxlist="persist" data-type="checkboxlist" data-sortable="true" data-orderby="false" data-caption="Include these fields" data-datafield="FieldList" style="flex:1 1 550px;"></div>`);
                        html.push('  </div>');
                        html.push('  </div>');
                        html.push('  </div>');
                        html.push(`</div>`);

                        FwConfirmation.addControls($confirmation, html.join(''));
                        const $yes = FwConfirmation.addButton($confirmation, 'Download', false);
                        const $no = FwConfirmation.addButton($confirmation, 'Cancel');
                        $confirmation.find('.sub-headings input').prop('checked', false);
                        $confirmation.find('.all-col input').prop('checked', true);

                        const request: any = this.getRenderRequest($form);
                        request.downloadPdfAsAttachment = true;
                        //set orderno as a parameter from front end if the orderid text box exists, some reports are not getting orderno from db.
                        if (request.parameters.hasOrderNo) {
                            request.parameters.orderno = $form.find(`div.fwformfield[data-datafield="OrderId"] .fwformfield-text`).val();
                        }
                        const convertedparameters = await this.getParameters($form).then((value) => this.convertParameters(value));
                        for (let key in convertedparameters) {
                            request[key] = convertedparameters[key];
                        }
                        // ----------
                        const renderColumnPopup = ($confirmation) => {
                            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/emptyobject`, null, FwServices.defaultTimeout, function onSuccess(response) {
                                const fieldsAllCheckedUnsorted = [];
                                const fieldsNoneCheckedUnsorted = [];

                                for (let key in response) {
                                    if (!key.startsWith('_')) {
                                        fieldsAllCheckedUnsorted.push({
                                            'value': key,
                                            'text': key,
                                            'selected': 'T',
                                        });
                                        fieldsNoneCheckedUnsorted.push({
                                            'value': key,
                                            'text': key,
                                            'selected': 'F',
                                        })
                                    }
                                }
                                FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fieldsAllCheckedUnsorted, false);
                                $confirmation.find('div[data-datafield="FieldList"]').attr('api-req', 'true');
                                $confirmation.find('.fieldsrow').data('fieldsAllCheckedUnsorted', fieldsAllCheckedUnsorted)
                                $confirmation.find('.fieldsrow').data('fieldsNoneCheckedUnsorted', fieldsNoneCheckedUnsorted);

                                const allChecked = fieldsAllCheckedUnsorted.slice();
                                $confirmation.find('.fieldsrow').data('fieldsAllCheckedSorted', allChecked.sort(function (a, b) { return (a.text > b.text) ? 1 : ((b.text > a.text) ? -1 : 0); }));
                                const noneChecked = fieldsNoneCheckedUnsorted.slice();
                                $confirmation.find('.fieldsrow').data('fieldsNoneCheckedSorted', noneChecked.sort(function (a, b) { return (a.text > b.text) ? 1 : ((b.text > a.text) ? -1 : 0); }));

                            }, function onError(response) {
                                FwFunc.showError(response);
                            }, null);
                        }
                        // ----------
                        $confirmation.find('.all-col input').on('change', e => {
                            const $this = jQuery(e.currentTarget);
                            if ($this.prop('checked') === true) {
                                $confirmation.find('.fieldsrow').hide();
                            }
                            else {
                                $confirmation.find('.fieldsrow').show();
                                if ($confirmation.find('div[data-datafield="FieldList"]').attr('api-req') !== 'true') {
                                    renderColumnPopup($confirmation);
                                }
                            }
                        });
                        // ----------
                        $confirmation.find('.check-uncheck').on('click', e => {
                            if ($confirmation.find('.check-uncheck').text() === 'Check All Fields') {
                                // caption uncheck all
                                $confirmation.find('.check-uncheck').text('Uncheck All Fields');
                                if ($confirmation.find('.sort-list').text() === 'Sort List By Name') {
                                    // check all, sorted
                                    const fields = $confirmation.find('.fieldsrow').data('fieldsAllCheckedUnsorted');
                                    FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                                } else {
                                    // check all, unsorted
                                    const fields = $confirmation.find('.fieldsrow').data('fieldsAllCheckedSorted');
                                    FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                                }
                            } else {
                                //caption check all
                                $confirmation.find('.check-uncheck').text('Check All Fields');
                                if ($confirmation.find('.sort-list').text() === 'Sort List By Name') {
                                    // uncheck all, unsorted
                                    const fields = $confirmation.find('.fieldsrow').data('fieldsNoneCheckedUnsorted');
                                    FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                                } else {
                                    // uncheck all, unsorted
                                    const fields = $confirmation.find('.fieldsrow').data('fieldsNoneCheckedSorted');
                                    FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                                }
                            }
                        });
                        // ----------
                        $confirmation.find('.sort-list').on('click', e => {
                            if ($confirmation.find('.sort-list').text() === 'Sort List By Name') {
                                //caption unsort
                                $confirmation.find('.sort-list').text('Unsort List By Name')
                                if ($confirmation.find('.check-uncheck').text() === 'Check All Fields') {
                                    // uncheck all, sorted
                                    const fields = $confirmation.find('.fieldsrow').data('fieldsNoneCheckedSorted');
                                    FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                                } else {
                                    // check all, sorted
                                    const fields = $confirmation.find('.fieldsrow').data('fieldsAllCheckedSorted');
                                    FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                                }
                            } else {
                                //caption sort
                                $confirmation.find('.sort-list').text('Sort List By Name')
                                if ($confirmation.find('.check-uncheck').text() === 'Check All Fields') {
                                    // uncheck all, unsorted
                                    const fields = $confirmation.find('.fieldsrow').data('fieldsNoneCheckedUnsorted');
                                    FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                                } else {
                                    // check all, unsorted
                                    const fields = $confirmation.find('.fieldsrow').data('fieldsAllCheckedUnsorted');
                                    FwFormField.loadItems($confirmation.find('div[data-datafield="FieldList"]'), fields, false);
                                }
                            }
                        });
                        // ----------
                        $yes.on('click', () => {
                            $confirmation.find('.sub-headings input').prop('checked') === true ? request.IncludeSubHeadingsAndSubTotals = true : request.IncludeSubHeadingsAndSubTotals = false;

                            if ($confirmation.find('.all-col input').prop('checked') === true) {
                                request.includeallcolumns = true;
                            } else {
                                request.includeallcolumns = false;
                                request.excelfields = FwFormField.getValueByDataField($confirmation, 'FieldList');
                            }
                            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/exportexcelxlsx`, request, timeout,
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
            $btnOpenPdf.on('click', async (event: JQuery.Event) => {
                try {
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const request: any = this.getRenderRequest($form);
                        request.renderMode = 'Pdf';
                        request.downloadPdfAsAttachment = false;
                        request.parameters = await this.getParameters($form).then((value) => this.convertParameters(value));
                        request.parameters.companyName = companyName;
                        request.parameters.systemName = systemName;
                        request.parameters.action = 'Print/PDF'
                        //set orderno as a parameter from front end if the orderid text box exists, some reports are not getting orderno from db.
                        if (request.parameters.hasOrderNo) {
                            request.parameters.orderno = $form.find(`div.fwformfield[data-datafield="OrderId"] .fwformfield-text`).val();
                        }
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
            $btnEmailMePdf.on('click', async (event: JQuery.Event) => {
                try {
                    const isValid = FwModule.validateForm($form);
                    if (isValid) {
                        const request: any = this.getRenderRequest($form);
                        request.renderMode = 'Email';
                        request.email.from = '[me]';
                        request.email.to = '[me]';
                        request.email.cc = '';
                        request.email.subject = $form.attr('data-caption');
                        request.email.body = '';
                        request.parameters = await this.getParameters($form).then((value) => this.convertParameters(value));
                        //set orderno as a parameter from front end if the orderid text box exists, some reports are not getting orderno from db.
                        if (request.parameters.hasOrderNo) {
                            request.parameters.orderno = $form.find(`div.fwformfield[data-datafield="OrderId"] .fwformfield-text`).val();
                        }

                        const $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                        request.parameters.companyName = companyName;
                        request.parameters.systemName = systemName;
                        FwAppData.apiMethod(true, 'POST', `${this.apiurl}/render`, request, timeout,
                            (successResponse: RenderResponse) => {
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
                        FwConfirmation.addControls($confirmation, this.getEmailTemplate($form.attr('data-controller')));
                        const $btnSend = FwConfirmation.addButton($confirmation, 'Send', false);
                        FwConfirmation.addButton($confirmation, 'Cancel');

                        if ($form.find('.order-contact-field').length) {
                            this.populateEmailToField($form, $confirmation);
                        }
                        const companyId = FwFormField.getValueByDataField($form, 'CompanyIdField');
                        FwFormField.setValueByDataField($confirmation, 'CompanyIdField', companyId);

                        const $emailToCC = $confirmation.find('.tousers, .ccusers');
                        $emailToCC.off('keydown').on('keydown', e => {
                            let $this;
                            const code = e.keyCode || e.which;
                            try {
                                switch (code) {
                                    case 9: //TAB key
                                        if (jQuery(e.currentTarget).find('.addItem').text().length === 0) {
                                            break;
                                        }
                                    case 13://Enter Key
                                        e.preventDefault();
                                        $this = jQuery(e.currentTarget);
                                        let emailList = FwFormField.getValue2($this);
                                        if (emailList.length > 0) {
                                            emailList = emailList.split(',');
                                        } else {
                                            emailList = [];
                                        }
                                        const value = $this.find('.multiselectitems .addItem').text();
                                        if (emailList.indexOf(value) === -1) {
                                            emailList.push(value);
                                            if (emailList.length) {
                                                emailList = emailList.join(',');
                                            }

                                            const $email = `<div contenteditable="false" class="multiitem" data-multivalue="${value}">
                                                <span>${value}</span>
                                                <i class="material-icons">clear</i>
                                            </div>`

                                            jQuery($email).insertBefore($this.find('.multiselectitems .addItem'));
                                            $this.find('.fwformfield-value').val(emailList);
                                            $this.find('.addItem').text('');
                                        }

                                        //FwFormField.setValue2($this, emailList, emailList);

                                        break;
                                    case 8:  //Backspace
                                        $this = jQuery(e.currentTarget);
                                        const inputLength = $this.find('span.addItem').text().length;
                                        const $item = $this.find('div.multiitem:last');
                                        if (inputLength === 0) {
                                            e.preventDefault();
                                            if ($item.length > 0) {
                                                $item.find('i').click();
                                            }
                                        }
                                        break;
                                }
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        })
                            .off('click', '.multiselectitems i')
                            .on('click', '.multiselectitems i', e => {
                                try {
                                    const $this = jQuery(e.currentTarget);
                                    const $browse = $this.closest('.fwformfield').data('browse');
                                    const $selectedRows = $browse.data('selectedrows');
                                    const selectedRowUniqueIds = $browse.data('selectedrowsuniqueids');
                                    const $item = $this.parent('div.multiitem');
                                    const $valuefield = $item.parent('.multiselectitems').siblings('.fwformfield-value');
                                    //removes item from values
                                    const itemValue = $item.attr('data-multivalue');
                                    let value: any = $valuefield.val();
                                    value = value
                                        .split(',')
                                        .map(s => s.trim())
                                        .filter((value) => {
                                            return value !== itemValue;
                                        })
                                        .join(',');
                                    $valuefield.val(value).change();
                                    //removes item from text
                                    const itemText = $item.find('span').text();
                                    const $textField = $valuefield.siblings('.fwformfield-text');
                                    let text: any = $textField.val();
                                    text = text
                                        .split(',')
                                        .filter((text) => {
                                            return text !== itemText;
                                        })
                                        .join(',');
                                    $textField.val(text);
                                    $item.remove();
                                    if ($selectedRows !== undefined && selectedRowUniqueIds !== undefined) {
                                        if (typeof $selectedRows[itemValue] !== 'undefined') {
                                            delete $selectedRows[itemValue];
                                        }
                                        const index = selectedRowUniqueIds.indexOf(itemValue);
                                        if (index != -1) {
                                            selectedRowUniqueIds.splice(index, 1);
                                        }
                                    }
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            })
                            .on('blur', 'div[contenteditable="true"]', e => {
                                const $this = jQuery(e.currentTarget);
                                if ($this.find('.addItem').text().length !== 0) {
                                    const $fwformfield = $this.parents('.fwformfield');
                                    const $input = $this.find('span.addItem');
                                    const value = $input.text();

                                    let emails = FwFormField.getValue2($fwformfield);
                                    if (emails.length) {
                                        emails = emails + ',' + value;
                                    } else {
                                        emails = value;
                                    }
                                    FwFormField.setValue2($fwformfield, emails, emails, false);
                                    $this.find('span.addItem').text('')
                                }
                            });

                        if (companyId != '' && companyId != null) {
                            this.addViewAllContacts($confirmation);
                        }

                        const signature = sessionStorage.getItem('emailsignature');
                        if (typeof signature != 'undefined' && signature != '') {
                            $confirmation.find('.signature').show();
                            $confirmation.find('.signature .value').html(signature);
                        }

                        let email = '[me]';
                        if (sessionStorage.getItem('email') !== null && sessionStorage.getItem('email') !== '') {
                            email = sessionStorage.getItem('email');
                        } else {
                            FwNotification.renderNotification('ERROR', 'Add an email to your account to enable this function.');
                            $btnSend.css({ 'pointer-events': 'none', 'background-color': 'light-gray' });
                        }
                        FwFormField.setValueByDataField($confirmation, 'from', email);
                        FwFormField.setValueByDataField($confirmation, 'subject', $form.attr('data-caption'));

                        $btnSend.click(async (event: JQuery.Event) => {
                            try {
                                const $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                                const requestEmailPdf: any = this.getRenderRequest($form);
                                let body = '<pre>' + FwFormField.getValueByDataField($confirmation, 'body') + '</pre>' + '<p>' + signature + '</p>';
                                requestEmailPdf.renderMode = 'Email';
                                requestEmailPdf.email.from = FwFormField.getValueByDataField($confirmation, 'from');
                                requestEmailPdf.email.to = FwFormField.getValueByDataField($confirmation, 'tousers');
                                requestEmailPdf.email.cc = FwFormField.getValueByDataField($confirmation, 'ccusers');
                                requestEmailPdf.email.subject = FwFormField.getValueByDataField($confirmation, 'subject');
                                requestEmailPdf.email.body = body;
                                requestEmailPdf.parameters = await this.convertParameters(this.getParameters($form));
                                //set orderno as a parameter from front end if the orderid text box exists, some reports are not getting orderno from db.
                                if (requestEmailPdf.parameters.hasOrderNo) {
                                    requestEmailPdf.parameters.orderno = $form.find(`div.fwformfield[data-datafield="OrderId"] .fwformfield-text`).val();
                                }
                                if (requestEmailPdf.parameters != null && requestEmailPdf.email.to != '') {
                                    requestEmailPdf.parameters.companyName = companyName;
                                    requestEmailPdf.parameters.systemName = systemName;
                                    FwAppData.apiMethod(true, 'POST', `${this.apiurl}/render`, requestEmailPdf, timeout,
                                        (successResponse) => {
                                            try {
                                                FwConfirmation.destroyConfirmation($confirmation);
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

        //Customize
        FwMenu.addVerticleSeparator($menuObject);
        const $btnCustomize = FwMenu.addStandardBtn($menuObject, 'Customize');
        $btnCustomize.on('click', (event: JQuery.Event) => {
            let $popupForm, popupCaption;
            const customReportLayoutId = FwFormField.getValueByDataField($form, 'CustomReportLayoutId');
            if (customReportLayoutId === '') {
                if (typeof (<any>window["CustomReportLayoutController"]).openForm === 'function') {
                    $popupForm = <any>window["CustomReportLayoutController"].openForm('NEW');
                    FwFormField.setValueByDataField($popupForm, 'BaseReport', $form.attr('data-reportname'), '', true);
                    FwFormField.disable($popupForm.find('[data-datafield="BaseReport"]'));
                }
            } else {
                if (typeof (<any>window["CustomReportLayoutController"]).loadForm === 'function') {
                    $popupForm = <any>window["CustomReportLayoutController"].loadForm({ CustomReportLayoutId: customReportLayoutId });
                }
            }
            popupCaption = FwFormField.getTextByDataField($form, 'CustomReportLayoutId') || 'Custom Report Layout';
            const $popupControl = FwPopup.renderPopup($popupForm, {}, popupCaption);
            $popupControl.find('.fwconfirmationbox').css({ 'width': '80vw', 'height': '80vh', 'overflow': 'auto' });
            $popupForm.data('usereportlayout', true);
            $popupForm.data('$reportfrontend', $form);
            FwPopup.showPopup($popupControl);
        });

        if (typeof (<any>window[$form.attr('data-controller')]).addReportMenuItems === 'function') {
            $menuObject = (<any>window[$form.attr('data-controller')]).addReportMenuItems($menuObject, $form);
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

        const reportName = $form.attr('data-reportname');

        //render grid
        //const $reportSettingsGrid = $form.find('div[data-grid="ReportSettingsGrid"]');
        //const $reportSettingsGridControl = FwBrowse.loadGridFromTemplate('ReportSettingsGrid');
        //$reportSettingsGrid.empty().append($reportSettingsGridControl);
        //$reportSettingsGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        WebUserId: JSON.parse(sessionStorage.getItem('userid')).webusersid
        //        , ReportName: reportName
        //    }
        //})
        //FwBrowse.init($reportSettingsGridControl);
        //FwBrowse.renderRuntimeHtml($reportSettingsGridControl);

        let loadDefaults: boolean = true;
        FwBrowse.renderGrid({
            nameGrid: 'ReportSettingsGrid',
            gridSecurityId: 'arqFEggnNSrA6',
            moduleSecurityId: 'arqFEggnNSrA6',
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    WebUserId: JSON.parse(sessionStorage.getItem('userid')).webusersid,
                    ReportName: reportName
                }
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                if (loadDefaults) {
                    $form.find('.load-settings').click();
                    loadDefaults = false;
                };
            },
            beforeSave: (request: any) => {
                request.uniqueids = {
                    WebUserId: JSON.parse(sessionStorage.getItem('userid')).webusersid,
                    ReportName: reportName,

                }
            }
        });
        const $reportSettingsGridControl = $form.find('[data-name="ReportSettingsGrid"]');
        FwBrowse.search($reportSettingsGridControl);

        const $reportLayoutValidation = $form.find('[data-datafield="CustomReportLayoutId"]');
        $reportLayoutValidation.data('beforevalidate', ($form, $reportLayoutValidation, request) => {
            request.uniqueids = {
                'BaseReport': reportName
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
                            , Text: FwFormField.getText2($this)
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
                if ($tr.length) {
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
    addOpenEmailToListButton($confirmation: JQuery, fieldname: string) {
        const $btn = jQuery(`<div class="email-${fieldname}">
                        <i class="material-icons" style="color: #4caf50; cursor:pointer;">add_box</i>
                      </div>`);

        $confirmation.find(`[data-datafield="${fieldname}"] .fwformfield-control`).append($btn);

        return $btn;
    }
    //----------------------------------------------------------------------------------------------
    populateEmailToField($form: JQuery, $confirmation: JQuery) {
        const request: any = {};
        request.uniqueids = {
            OrderId: FwFormField.getValue2($form.find('.order-contact-field'))
        }
        FwAppData.apiMethod(true, 'POST', `api/v1/ordercontact/browse`, request, FwServices.defaultTimeout,
            (successResponse) => {
                try {
                    if (successResponse.Rows.length) {
                        const rows = successResponse.Rows;
                        const isOrderedByIndex = successResponse.ColumnIndex.IsOrderedBy;
                        const emailIndex = successResponse.ColumnIndex.Email;
                        const emails = rows.filter(item => item[isOrderedByIndex] == true).map(item => item[emailIndex]).join(',');
                        FwFormField.setValueByDataField($confirmation, 'tousers', emails);
                        //FwFormField.setValueByDataField($confirmation, 'tousers', emails);
                        //for (let i = 0; i < emails.length; i++) {
                        //    const $email = `<div contenteditable="false" class="multiitem" data-multivalue="${emails[i][emailIndex]}">
                        //                        <span>${emails[i][emailIndex]}</span>
                        //                        <i class="material-icons">clear</i>
                        //                    </div>`
                        //    jQuery($email).insertBefore($confirmation.find('.tousers .multiselectitems .addItem'));
                        //}
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            null, $confirmation.find('.fwconfirmationbox'));
    }
    //----------------------------------------------------------------------------------------------
    getContacts($form: JQuery, $confirmation: JQuery, datafield: string) {
        const request: any = {};
        const companyId = FwFormField.getValueByDataField($form, 'CompanyIdField') ?? '';
        let apiurl = 'api/v1/companycontact/browse';;

        companyId == '' ? request.uniqueids = {} : request.uniqueids = { CompanyId: companyId };

        FwAppData.apiMethod(true, 'POST', apiurl, request, FwServices.defaultTimeout,
            (successResponse) => {
                try {
                    this.renderContactsList($form, $confirmation, successResponse, companyId, datafield);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            null, $confirmation.find('.fwconfirmationbox'));
    }
    //----------------------------------------------------------------------------------------------
    addViewAllContacts($confirmation: JQuery) {
        const $controls = $confirmation.find('.tousers, .ccusers');
        const companyId = FwFormField.getValueByDataField($confirmation, 'CompanyIdField');
        for (let i = 0; i < $controls.length; i++) {
            const $browse = jQuery($controls[i]).data('browse');
            const $viewAll = jQuery(`<div class="view-contacts ${companyId != '' ? '' : 'active'}">View ${companyId != '' ? 'All' : 'Company'} Contacts</div>`);

            $viewAll.css({ 'font-size': '.9em', 'text-align': 'center', 'cursor': 'pointer', 'color': '#0D47A1', 'float': 'right', 'clear': 'right' });

            $viewAll.on('click', e => {
                const $this = jQuery(e.currentTarget);

                if ($this.hasClass('active')) {
                    $this.removeClass('active');
                    if (companyId != '') {
                        $browse.data('ondatabind', request => {
                            request.uniqueids = { CompanyId: companyId };
                        })
                        FwBrowse.search($browse);
                    }
                    $this.text('View All Contacts')
                } else {
                    $this.addClass('active');
                    $browse.data('ondatabind', request => {
                        request.uniqueids = {};
                    })
                    FwBrowse.search($browse);
                    $this.text('View Company Contacts')
                }
            });

            $browse.find('.pager').append($viewAll);
        }
    }
    //----------------------------------------------------------------------------------------------
    renderContactsList($form: JQuery, $confirmation: JQuery, response: any, companyId: string, datafield: string, $contactList?: JQuery) {
        const rows = response.Rows;
        const personIndex = response.ColumnIndex.Person;
        const contactTitleIndex = response.ColumnIndex.ContactTitle;
        const emailIndex = response.ColumnIndex.Email;
        const companyIndex = response.ColumnIndex.Company;

        if (!$contactList) {
            $contactList = FwConfirmation.renderConfirmation('Contacts', '');
            FwConfirmation.addButton($contactList, 'Close');
        }
        rows.sort((a, b) => (a[personIndex] > b[personIndex]) ? 1 : ((b[personIndex] > a[personIndex]) ? -1 : 0));
        const html: any = [];
        html.push('<div class="contact-list">')
        html.push('<div class="table" style="overflow:auto;">')
        html.push('<table>');
        html.push('<thead>');
        html.push('<tr>');
        html.push('<th></th>');
        html.push('<th>Contact</th>');
        html.push('<th>Contact Title</th>');
        html.push('<th>Company</th>');
        html.push('<th>E-Mail</th>');
        html.push('</tr>');
        html.push('</thead>');
        html.push('<tbody>');

        for (let i = 0; i < rows.length; i++) {
            html.push(`<tr><td><input type="checkbox" class="value"></td>
                           <td class="contact-person">${rows[i][personIndex]}</td>
                           <td class="contact-title">${rows[i][contactTitleIndex]}</td>
                           <td class="contact-company">${rows[i][companyIndex]}</td>
                           <td class="contact-email">${rows[i][emailIndex]}</td>
                      </tr>`);
        }
        html.push('</tbody>');
        html.push('</table>');
        html.push('</div>');
        if (companyId != '') {
            html.push('<div class="show-all" style="text-align:center;margin-top: 1rem;">');
            html.push('<span style="font-size:.8rem;text-decoration:underline; color:blue; cursor:pointer;">Show All Contacts</span>');
            html.push('</div>');
        }
        html.push('</div>');
        FwConfirmation.addControls($contactList, html.join(''));

        const toUsers = FwFormField.getValueByDataField($confirmation, 'tousers');
        let toEmails: any = [];

        if (toUsers.length) {
            toEmails = toUsers.split(',').map(item => {
                return item.trim();
            });

            //check boxes for emails already in list
            for (let i = 0; i < toEmails.length; i++) {
                $contactList.find(`td.contact-email:contains(${toEmails[i]})`)
                    .parents('tr')
                    .addClass('checked tousers')
                    .find('input.value')
                    .prop('checked', true);
            }
        }

        const ccUsers = FwFormField.getValueByDataField($confirmation, 'ccusers');
        let ccEmails: any = [];

        if (ccUsers.length) {
            ccEmails = ccUsers.split(',').map(item => {
                return item.trim();
            });

            //check boxes for emails already in list
            for (let i = 0; i < ccEmails.length; i++) {
                $contactList.find(`td.contact-email:contains(${ccEmails[i]})`)
                    .parents('tr')
                    .addClass('checked ccusers')
                    .find('input.value')
                    .prop('checked', true);
            }
        }
        $contactList.off('click', 'tbody tr td input.value');
        $contactList.on('click', 'tbody tr td input.value', e => {
            e.stopPropagation();
            const $this = jQuery(e.currentTarget);
            let emailList;
            let isChecked = $this.prop('checked');
            const $tr = $this.parents('tr');
            const email = $tr.find('.contact-email').text();

            if (datafield === 'tousers') {
                emailList = toEmails;
            } else if (datafield === 'ccusers') {
                emailList = ccEmails;
            }

            if (isChecked) {
                emailList.push(email);
                $tr.addClass(`checked ${datafield}`);
                FwFormField.setValueByDataField($confirmation, datafield, emailList.join(', '));
            } else {
                if ($tr.hasClass('tousers')) {
                    toEmails = toEmails.filter(item => item !== email);
                    $tr.removeClass('tousers');
                    FwFormField.setValueByDataField($confirmation, 'tousers', toEmails.join(', '));
                } else if ($tr.hasClass('ccusers')) {
                    ccEmails = ccEmails.filter(item => item !== email);
                    $tr.removeClass('ccusers');
                    FwFormField.setValueByDataField($confirmation, 'ccusers', ccEmails.join(', '));
                }
                $tr.removeClass('checked');
            }
        });

        $contactList.off('click', 'tbody tr');
        $contactList.on('click', 'tbody tr', e => {
            jQuery(e.currentTarget).find('input.value').click();
        });

        $contactList.off('click', '.show-all');
        $contactList.on('click', '.show-all', e => {
            const request: any = {};
            request.uniqueids = {};
            let apiurl = 'api/v1/companycontact/browse';

            FwAppData.apiMethod(true, 'POST', apiurl, request, FwServices.defaultTimeout,
                (successResponse) => {
                    try {
                        $contactList.find('.contact-list').empty();
                        this.renderContactsList($form, $confirmation, successResponse, '', datafield, $contactList);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                },
                null, $contactList.find('.fwconfirmationbox'));
        });
    }
    //----------------------------------------------------------------------------------------------
    async getParameters($form: JQuery): Promise<any> {
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

            if (parameters.CustomReportLayoutId != "" && parameters.CustomReportLayoutId != undefined) {
                const customReportLayout = FwAjax.callWebApi<any, any>({
                    httpMethod: 'GET',
                    url: `${applicationConfig.apiurl}api/v1/customreportlayout/${parameters.CustomReportLayoutId}`,
                    $elementToBlock: $form
                });

                await customReportLayout
                    .then((values: any) => {
                        parameters.ReportTemplate = values.Html;
                    });
            }

            parameters.Locale = FwLocale.Locale;

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
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Report Layout" data-datafield="CustomReportLayoutId" data-validationname="CustomReportLayoutValidation" style="flex:0 1 575px; margin:10px;"></div>
                </div>
                <div class="flexrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Save current report settings as:" data-datafield="Description" style="max-width:600px; margin:10px;"></div>
                    <div class="fwformcontrol save-settings" data-type="button" style="max-width:60px; margin-top:20px; margin-left:10px;">
                        <i class="material-icons" style="padding-top:5px; margin:0px -10px;">save</i>
                        <span style="float:right; padding-left:10px;">Save</span>
                    </div>
                </div>
                <div class="flexrow settings-grid" style="margin-left:5px;">
                    <div data-control="FwGrid" data-grid="ReportSettingsGrid"></div>
                    <div class="fwformcontrol load-settings" data-type="button" style="max-width:60px; margin-top:15px; margin-left:10px;">
                        <i class="material-icons" style="padding-top:5px; margin:0px -10px;">open_in_browser</i>
                        <span style="float:right; padding-left:10px;">Load</span>
                    </div>
                </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getEmailTemplate(controller: string) {
        return `
        <div class="fwform" data-controller="${controller}">
              <div style="min-width:540px; max-width:40vw;">
                  <div class="formrow">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-datafield="from" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield from" data-caption="From" data-allcaps="false" data-enabled="false"></div>
                      </div>    
                      <div class="flexrow">
                          <div data-datafield="tousers" data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield tousers email" data-allcaps="false" data-caption="To (Users)" data-validationname="ReportCompanyContactValidation" data-hasselectall="false" style="box-sizing:border-box;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-datafield="ccusers" data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield ccusers email" data-allcaps="false" data-caption="CC (Users)" data-validationname="ReportCompanyContactValidation" data-hasselectall="false" style="box-sizing:border-box;"></div>
                      /div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-datafield="subject" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield subject" data-caption="Subject" data-allcaps="false" data-enabled="true"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-datafield="body" data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield message" data-caption="Message" data-allcaps="false" data-enabled="true"></div>
                      </div>
                      <div class="fwformfield signature" style="display:none;padding:.5rem;">
                          <div class="fwformfield-caption">Signature</div>
                          <div class="value"></div>
                      </div>
                      <div data-datafield="CompanyIdField" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-allcaps="false" data-enabled="true" style="display:none;"></div>
                  </div>
              </div>
            </div>`;
        //<div data-datafield="ccusers" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ccusers email" data-caption="CC" data-allcaps="false" style="box-sizing:border-box;"></div>
        //<div data-datafield="tousers" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield tousers email" data-caption="To" data-allcaps="false" style="box-sizing:border-box;"></div>          
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