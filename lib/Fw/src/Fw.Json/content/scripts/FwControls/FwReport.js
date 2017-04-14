//----------------------------------------------------------------------------------------------
var FwReport = {
    ModuleOptions: {
        ReportOptions: {
            HasExportHtml: true,
            HasExportPdf:  true,
            HasEmailHtml:  true,
            HasEmailPdf:   true,
            HasEmailMePdf: true,
            HasDownloadExcel: false
        }
    }
};
//----------------------------------------------------------------------------------------------
FwReport.getFrontEnd = function(projectprefix, reportname, templateName) {
    var html, $form, $actionView, templateHtml;
    
    templateHtml = jQuery('#' + templateName).html();
    if (typeof templateHtml === 'undefined') throw 'Unable to find template: ' + templateName + '.  A common way for this to happen is if the "Module" property on the Report Controller is incorrect.';
    html = [];
    html.push(templateHtml);

    $form = jQuery(html.join(''));
    $form.attr('data-projectprefix', projectprefix);
    $form.attr('data-reportname', reportname);
    $form.on('change', '.fwformfield[data-required="true"].error', function() {
        var $this, value;
        $this = jQuery(this);
        value = FwFormField.getValue2($this);
        if (value !== '') {
            $this.removeClass('error');
        }
    });
    FwReport.setExportTab($form);
    
    return $form;
};
//----------------------------------------------------------------------------------------------
FwReport.setExportTab = function($form) {
    var html, $exporttabpagecontents, $exporttabpage;

    html = [];
    html.push('<div class="formpage">');
    html.push('  <div class="htmltemplates">');
    html.push('    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Report StyleSheet">');
    html.push('      <textarea class="stylesheetcodemirror"></textarea>');
    html.push('    </div>');
    html.push('    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Report Header">');
    html.push('      <textarea class="headercodemirror"></textarea>');
    html.push('    </div>');
    html.push('    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Report Body">');
    html.push('      <textarea class="bodycodemirror"></textarea>');
    html.push('    </div>');
    html.push('    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Report Footer">');
    html.push('      <textarea class="footercodemirror"></textarea>');
    html.push('    </div>');
    html.push('  </div>');
    html.push('  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Document Properties">');
    html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
    html.push('      <div data-datafield="pagewidth" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Page Width (in)" style="float:left;width:110px;"></div>');
    html.push('      <div data-datafield="pageheight" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Page Height (in)" style="float:left;width:110px;"></div>');
    html.push('      <div data-datafield="margintop" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Margin Top (in)" style="float:left;width:110px;"></div>');
    html.push('      <div data-datafield="marginright" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Margin Right (in)" style="float:left;width:110px;"></div>');
    html.push('      <div data-datafield="marginbottom" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Margin Bottom (in)" style="float:left;width:110px;"></div>');
    html.push('      <div data-datafield="marginleft" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Margin Left (in)" style="float:left;width:110px;"></div>');
    html.push('      <div data-datafield="headerheight" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Header Height (in)" style="float:left;width:110px;"></div>');
    html.push('      <div data-datafield="footerheight" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Footer Height (in)" style="float:left;width:110px;"></div>');
    html.push('    </div>');
    html.push('  </div>');
    html.push('</div>');
    html = html.join('');
    $exporttabpagecontents = jQuery(html);
    $exporttabpage = $form.find('.exporttabpage')
    if ($exporttabpage.length === 0) throw 'Missing tabpage with class "exporttabpage".';
    $exporttabpage.append($exporttabpagecontents);
    $form.find('.exporttab').hide();
};
//----------------------------------------------------------------------------------------------
FwReport.getExportRequest = function($form) {
    var request, template, fromdate, todate;
    request = {
        templates: {
            stylesheet: $form.data('stylesheetcodemirror').getValue(),
            header:     $form.data('headercodemirror').getValue(),
            body:       $form.data('bodycodemirror').getValue(),
            footer:     $form.data('footercodemirror').getValue()
        },
        parameters: {}
    };
    if (typeof $form.data('getexportrequest') === 'function')
    {
        request = $form.data('getexportrequest')(request);
    }

    return request;
};
//----------------------------------------------------------------------------------------------
FwReport.getDownloadExcelRequest = function ($form) {
    var request, template, fromdate, todate;
    request = {
        templates: {
            excel: $form.data('exceltemplate')
        },
        parameters: {}
    };
    if (typeof $form.data('getexportrequest') === 'function') {
        request = $form.data('getexportrequest')(request);
    }

    return request;
};
//----------------------------------------------------------------------------------------------
FwReport.load = function($form, reportOptions) {
    var stylesheetcodemirror, headercodemirror, bodycodemirror, footercodemirror, $fwcontrols, formid;
    
    formid      = program.uniqueId(8);
    $fwcontrols = $form.find('.fwcontrol').addBack();
    FwControl.renderRuntimeControls($fwcontrols);
    FwControl.setIds($fwcontrols, formid);

    FwReport.addReportMenu($form, reportOptions);

    $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]'));
    $form.data('fields',    $form.find('.fwformfield[data-isuniqueid!="true"]'));

    stylesheetcodemirror = CodeMirror.fromTextArea($form.find('.stylesheetcodemirror')[0], {
        mode: 'css',
        lineNumbers: true
    });
    $form.data('stylesheetcodemirror', stylesheetcodemirror);
    stylesheetcodemirror.setSize(935, 300);
    stylesheetcodemirror.setValue(jQuery('#tmpl-reports-' + $form.attr('data-reportname') + 'StyleSheet').html());

    headercodemirror = CodeMirror.fromTextArea($form.find('.headercodemirror')[0], {
        mode: 'htmlembeded',
        lineNumbers: true
    });
    $form.data('headercodemirror', headercodemirror);
    headercodemirror.setSize(935, 300);
    headercodemirror.setValue(jQuery('#tmpl-reports-' + $form.attr('data-reportname') + 'Header').html());

    bodycodemirror = CodeMirror.fromTextArea($form.find('.bodycodemirror')[0], {
        mode: 'htmlembeded',
        lineNumbers: true
    });
    $form.data('bodycodemirror', bodycodemirror);
    bodycodemirror.setSize(935, 300);
    bodycodemirror.setValue(jQuery('#tmpl-reports-' + $form.attr('data-reportname') + 'Body').html());

    footercodemirror = CodeMirror.fromTextArea($form.find('.footercodemirror')[0], {
        mode: 'htmlembeded',
        lineNumbers: true
    });
    $form.data('footercodemirror', footercodemirror);
    footercodemirror.setSize(935, 300);
    footercodemirror.setValue(jQuery('#tmpl-reports-' + $form.attr('data-reportname') + 'Footer').html());

    var webserviceurl, request;
    webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/GetDefaultPrintOptions';
    request = {};
    FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, function(response) {
        FwFormField.setValue($form, 'div[data-datafield="pagewidth"]',    response.printoptions.PageWidth);
        FwFormField.setValue($form, 'div[data-datafield="pageheight"]',   response.printoptions.PageHeight);
        FwFormField.setValue($form, 'div[data-datafield="margintop"]',    response.printoptions.MarginTop);
        FwFormField.setValue($form, 'div[data-datafield="marginright"]',  response.printoptions.MarginRight);
        FwFormField.setValue($form, 'div[data-datafield="marginbottom"]', response.printoptions.MarginBottom);
        FwFormField.setValue($form, 'div[data-datafield="marginleft"]',   response.printoptions.MarginLeft);
        FwFormField.setValue($form, 'div[data-datafield="headerheight"]', response.printoptions.HeaderHeight);
        FwFormField.setValue($form, 'div[data-datafield="footerheight"]', response.printoptions.FooterHeight);
    }, null, $form);

    $form.find('.exporttab').closest('.tabs').data('ontabchange', function($tab) {
        try {
            if ($tab.hasClass('exporttab')) {
                $form.data('stylesheetcodemirror').refresh();
                $form.data('headercodemirror').refresh();
                $form.data('bodycodemirror').refresh();
                $form.data('footercodemirror').refresh();
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });
};
//----------------------------------------------------------------------------------------------
FwReport.addReportMenu = function($form, reportOptions) {
    var $btnPreview, $btnOpenPdf, $btnDownloadPdf, $btnEmailHtml, $btnEmailMePdf, $btnEmailPdf, $menuObject;

    $menuObject = FwMenu.getMenuControl('default');
    timeout     = 7200; // 2 hour timeout for the ajax request

    if ((typeof reportOptions.HasExportHtml === 'undefined') || (reportOptions.HasExportHtml === true)) {
        $btnPreview = FwMenu.addStandardBtn($menuObject, 'Preview');
        $btnPreview.on('click', function(event) {
            var request, webserviceurl, timeout, $notification;
            try {
                request       = FwReport.getExportRequest($form);
                webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/Preview';
                
                FwAppData.jsonPost(true, webserviceurl, request, timeout, 
                    function(response) {
                        var win;
                        try {
                            win = window.open(response.downloadurl);
                            if (!win) throw 'Please disable your popup blocker for this site!';
                        } catch (ex) {
                            FwFunc.showError(ex);
                        } finally {
                            FwNotification.closeNotification($notification);
                        }
                    }, 
                    function(errorThrown) {
                        FwNotification.closeNotification($notification);
                        if (errorThrown !== 'abort') {
                            FwFunc.showError(errorThrown);
                        }
                    }, null
                );
                $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Preview...');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
        $btnPrintPdf = FwMenu.addStandardBtn($menuObject, 'Print HTML');
        $btnPrintPdf.on('click', function(event) {
            var request, webserviceurl, timeout, $notification;
            try {
                request       = FwReport.getExportRequest($form);
                webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/Preview';
                
                FwAppData.jsonPost(true, webserviceurl, request, timeout, 
                    function(response) {
                        var win, $iframe;
                        try {
                            $iframe = jQuery('<iframe style="display:none;" />');
                            jQuery('.application').append($iframe);
                            $iframe.attr('src', response.downloadurl);
                            $iframe.on('load', function() {
                                setTimeout(function() {
                                    $iframe[0].focus();
                                    $iframe[0].contentWindow.print();
                                    //setTimeout(function() {
                                    //    $iframe.remove();
                                    //}, 10000);
                                }, 500);
                            });
                        } catch (ex) {
                            FwFunc.showError(ex);
                        } finally {
                            FwNotification.closeNotification($notification);
                        }
                    }, 
                    function(errorThrown) {
                        FwNotification.closeNotification($notification);
                        if (errorThrown !== 'abort') {
                            FwFunc.showError(errorThrown);
                        }
                    }, null
                );
                $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Preview...');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    FwMenu.addVerticleSeparator($menuObject);
    if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
        $btnOpenPdf = FwMenu.addStandardBtn($menuObject, 'View PDF');
        $btnOpenPdf.on('click', function(event) {
            var request, webserviceurl, $notification;
            try {
                request       = FwReport.getExportRequest($form);
                request.parameters.downloadasattachment = false;
                webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/DownloadPdf';
                FwAppData.jsonPost(true, webserviceurl, request, timeout, 
                    function(response) {
                        var win;
                        try {
                            win = window.open(response.downloadurl);
                            if (!win) throw 'Please disable your popup blocker for this site!';
                        } catch (ex) {
                            FwFunc.showError(ex);
                        } finally {
                            FwNotification.closeNotification($notification);
                        }
                    }, 
                    function(errorThrown) {
                        FwNotification.closeNotification($notification);
                        if (errorThrown !== 'abort') {
                            FwFunc.showError(errorThrown);
                        }
                    }, null
                );
                $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    }

    if ((typeof reportOptions.HasExportPdf === 'undefined') || (reportOptions.HasExportPdf === true)) {
        $btnDownloadPdf = FwMenu.addStandardBtn($menuObject, 'Download PDF');
        $btnDownloadPdf.on('click', function(event) {
            var request, webserviceurl, $notification;
            try {
                request       = FwReport.getExportRequest($form);
                request.parameters.downloadasattachment = true;
                webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/DownloadPdf';
                FwAppData.jsonPost(true, webserviceurl, request, timeout, 
                    function(response) {
                        var win, $iframe;
                        try {
                            $iframe = jQuery('<iframe style="display:none;" />');
                            jQuery('.application').append($iframe);
                            $iframe.attr('src', response.downloadurl);
                            setTimeout(function() {
                                $iframe.remove();
                            }, 500);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        } finally {
                            FwNotification.closeNotification($notification);
                        }
                    }, 
                    function(errorThrown) {
                        FwNotification.closeNotification($notification);
                        if (errorThrown !== 'abort') {
                            FwFunc.showError(errorThrown);
                        }
                    }, null
                );
                $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    }

    FwMenu.addVerticleSeparator($menuObject);
    if ((typeof reportOptions.HasEmailMePdf === 'undefined') || (reportOptions.HasEmailMePdf === true)) {
        $btnEmailMePdf = FwMenu.addStandardBtn($menuObject, 'E-mail (to me)');
        $btnEmailMePdf.on('click', function(event) {
            var request, webserviceurl, $notification;
            try {
                request = FwReport.getExportRequest($form);
                request.email = {
                    to: '[me]',
                    cc: '',
                    subject: '[reportname]',
                    body: ''
                };
                webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/SendPdfEmail';
                FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, 
                    function(response) {
                        try {
                            FwNotification.renderNotification('SUCCESS', 'Email Sent');
                        } catch (ex) {
                            FwFunc.showError(ex);
                        } finally {
                            FwNotification.closeNotification($notification);
                        }
                    }, function(errorThrown) {
                        FwNotification.closeNotification($notification);
                        if (errorThrown !== 'abort') {
                            FwFunc.showError(errorThrown);
                        }
                    }, null
                );
                 $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    }

    //if ((typeof reportOptions.HasEmailHtml === 'undefined') || (reportOptions.HasEmailHtml == true)) {
    //    $btnEmailHtml = FwMenu.addStandardBtn($menuObject, 'E-mail Html');
    //    $btnEmailHtml.on('click', function(event) {
    //        var request, webserviceurl;
    //        try {
    //            request = FwReport.getExportRequest($form);
    //            request.email = {
    //                to: '',
    //                cc: '',
    //                subject: '',
    //                body: ''
    //            };
    //            webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/ExportHtmlEmail';
    //            FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, 
    //                function(response) {
    //                    try {
    //                        FwNotification.renderNotification('SUCCESS', 'Email Sent');
    //                    } catch (ex) {
    //                        FwFunc.showError(ex);
    //                    }
    //                }, null, $form
    //            );
    //        } catch(ex) {
    //            FwFunc.showError(ex);
    //        }
    //    });
    //}

    if ((typeof reportOptions.HasEmailPdf === 'undefined') || (reportOptions.HasEmailPdf === true)) {
        $btnEmailPdf = FwMenu.addStandardBtn($menuObject, 'E-mail');
        $btnEmailPdf.on('click', function(event) {
            var requestGetFromEmail, requestSendPdfEmail, webserviceurl, $confirmation, $btnSend;
            try {
                requestGetFromEmail = {};
                webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/GetFromEmail';
                FwAppData.jsonPost(true, webserviceurl, requestGetFromEmail, FwServices.defaultTimeout, 
                    function(responseGetFromEmail) {
                        try {
                            $confirmation = FwConfirmation.renderConfirmation(FwLanguages.translate('E-mail PDF'));
                            FwConfirmation.addControls($confirmation, jQuery('#tmpl-controls-FwReport-Email').html());
                            FwFormField.setValueByDataField($confirmation, 'from', responseGetFromEmail.fromemail);
                            FwFormField.setValueByDataField($confirmation, 'subject', FwTabs.getTabByElement($form).attr('data-caption'));
                            $btnSend = FwConfirmation.addButton($confirmation, 'Send');
                            FwConfirmation.addButton($confirmation, 'Cancel');
                            $btnSend.click(function(event) {
                                var $notification;
                                try {
                                    requestSendPdfEmail = FwReport.getExportRequest($form)
                                    requestSendPdfEmail.email = FwReport.getParameters($confirmation);
                                    webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/SendPdfEmail';
                                    FwAppData.jsonPost(true, webserviceurl, requestSendPdfEmail, FwServices.defaultTimeout, 
                                        function(response) {
                                            try {
                                                FwNotification.renderNotification('SUCCESS', 'Email Sent');
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            } finally {
                                                FwNotification.closeNotification($notification);
                                            }
                                        }, function(errorThrown) {
                                            FwNotification.closeNotification($notification);
                                            if (errorThrown !== 'abort') {
                                                FwFunc.showError(errorThrown);
                                            }
                                        }, null
                                    );
                                    $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                            $confirmation.on('change', '[data-datafield="tousers"] .fwformfield-value', function(event) {
                                var requestGetEmailByWebUsersId = {};
                                requestGetEmailByWebUsersId.webusersids = this.value;
                                requestGetEmailByWebUsersId.to = FwFormField.getValueByDataField($confirmation, 'to');
                                webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/GetEmailByWebUsersId';
                                FwAppData.jsonPost(true, webserviceurl, requestGetEmailByWebUsersId, FwServices.defaultTimeout, 
                                    function(response) {
                                        try {
                                            FwFormField.setValueByDataField($confirmation, 'to', response.emailto);
                                            FwFormField.setValueByDataField($confirmation, 'tousers', '', '');
                                        } catch (ex) {
                                            FwFunc.showError(ex);
                                        }
                                    }, null, null
                                );
                            });
                            $confirmation.on('change', '[data-datafield="ccusers"] .fwformfield-value', function(event) {
                                var requestGetEmailByWebUsersId = {};
                                requestGetEmailByWebUsersId.webusersids = this.value;
                                requestGetEmailByWebUsersId.to = FwFormField.getValueByDataField($confirmation, 'cc');
                                webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/GetEmailByWebUsersId';
                                FwAppData.jsonPost(true, webserviceurl, requestGetEmailByWebUsersId, FwServices.defaultTimeout, 
                                    function(response) {
                                        try {
                                            FwFormField.setValueByDataField($confirmation, 'cc', response.emailto);
                                            FwFormField.setValueByDataField($confirmation, 'ccusers', '', '');
                                        } catch (ex) {
                                            FwFunc.showError(ex);
                                        }
                                    }, null, null
                                );
                            });
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }, null, null
                );
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    }

    FwMenu.addVerticleSeparator($menuObject);
    if ((typeof reportOptions.HasDownloadExcel === 'undefined') || (reportOptions.HasDownloadExcel === true)) {
        var $btnDownloadExcel = FwMenu.addStandardBtn($menuObject, 'Download Excel');
        $btnDownloadExcel.on('click', function (event) {
            var request, webserviceurl, $notification;
            try {
                request = FwReport.getDownloadExcelRequest($form);
                request.parameters.downloadasattachment = true;
                webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/DownloadExcel';
                FwAppData.jsonPost(true, webserviceurl, request, timeout,
                    function (response) {
                        var win, $iframe;
                        try {
                            $iframe = jQuery('<iframe style="display:none;" />');
                            jQuery('.application').append($iframe);
                            $iframe.attr('src', response.downloadurl);
                            setTimeout(function () {
                                $iframe.remove();
                            }, 500);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        } finally {
                            FwNotification.closeNotification($notification);
                        }
                    },
                    function (errorThrown) {
                        FwNotification.closeNotification($notification);
                        if (errorThrown !== 'abort') {
                            FwFunc.showError(errorThrown);
                        }
                    }, null
                );
                $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Report...');
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
};
//----------------------------------------------------------------------------------------------
FwReport.getData = function($form, request, responseFunc) {
    var webserviceurl;
    webserviceurl = 'services.ashx?path=/reports/' + $form.attr('data-reportname') + '/GetData';
    FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, responseFunc, null, $form);
};
//----------------------------------------------------------------------------------------------
FwReport.getParameters = function($form) {
    var isvalid, $fields, $field, parameters=null;
    isvalid    = FwModule.validateForm($form);
    if (isvalid) {
        parameters = {};
        $fields    = $form.find('div[data-control="FwFormField"]');
        $fields.each(function(index, element) {
            $field = jQuery(element);
            if (typeof $field.attr('data-datafield') === 'string') {
                parameters[$field.attr('data-datafield')] = FwFormField.getValue2($field);
            }
        });
    } else {
        throw 'Please fill in the required fields.';
    }

    return parameters;
};
//----------------------------------------------------------------------------------------------