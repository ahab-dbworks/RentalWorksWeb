class FwBrowseColumn_appdocumentimageClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {
        if (typeof dt.ColumnIndex[$field.attr('data-browseappdocumentidfield')] !== 'number') throw 'FwBrowseColumn_appdocumentimage.databindfield: ' + $field.attr('data-browseappdocumentidfield') + ' was not returned by the webservice.';
        if (typeof dt.ColumnIndex[$field.attr('data-browsefilenamefield')] !== 'number') throw 'FwBrowseColumn_appdocumentimage.databindfield: ' + $field.attr('data-browsefilenamefield') + ' was not returned by the webservice.';
        if (typeof dt.ColumnIndex[$field.attr('data-browsefileextensionfield')] !== 'number') throw 'FwBrowseColumn_appdocumentimage.databindfield: ' + $field.attr('data-browsefileextensionfield') + ' was not returned by the webservice.';
        var appdocumentid = dtRow[dt.ColumnIndex[$field.attr('data-browseappdocumentidfield')]];
        var documenttypeid = dtRow[dt.ColumnIndex[$field.attr('data-documenttypeidfield')]];
        var uniqueid1 = dtRow[dt.ColumnIndex[$field.attr('data-uniqueid1field')]];
        var uniqueid2 = dtRow[dt.ColumnIndex[$field.attr('data-uniqueid2field')]];
        var filename = dtRow[dt.ColumnIndex[$field.attr('data-browsefilenamefield')]];
        var fileextension = dtRow[dt.ColumnIndex[$field.attr('data-browsefileextensionfield')]];
        var miscfield = dtRow[dt.ColumnIndex[$field.attr('data-miscfield')]];
        $field.attr('data-appdocumentid', appdocumentid);
        $field.attr('data-documenttypeid', documenttypeid);
        $field.attr('data-uniqueid1', uniqueid1);
        $field.attr('data-uniqueid2', uniqueid2);
        $field.attr('data-filename', filename);
        $field.attr('data-fileextension', fileextension);
        $field.attr('data-miscfield', miscfield);
        $field.data('filedataurl', '');
        $field.attr('data-filepath', '');
        $field.attr('data-ismodified', 'false');
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {
        if ($field.attr('data-uniqueid1field') !== undefined && $field.attr('data-uniqueid1') !== undefined) {
            field[$field.attr('data-uniqueid1field')] = $field.attr('data-uniqueid1');
        }
        if ($field.attr('data-uniqueid2field') !== undefined && $field.attr('data-uniqueid2') !== undefined) {
            field[$field.attr('data-uniqueid2field')] = $field.attr('data-uniqueid2');
        }
        field.FileIsModified = typeof $field.attr('data-ismodified') === 'string' && $field.attr('data-ismodified') === 'true';
        if (field.FileIsModified) {
            field.FileDataUrl = $field.data('filedataurl');
            field.FilePath = $field.attr('data-filepath');
            field.Extension = typeof $field.attr('data-fileextension') === 'string' ? $field.attr('data-fileextension').toUpperCase() : '';
        }
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        throw 'FwBrowseColumn_appdocumentimage.setFieldValue: Not Implemented!';
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = typeof $field.attr('data-ismodified') === 'string' && $field.attr('data-ismodified') === 'true';
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        let html = [];
        var $adiContainer;
        var appimageid = typeof $field.attr('data-originalvalue') === 'string' ? $field.attr('data-originalvalue') : '';
        var filename = typeof $field.attr('data-filename') === 'string' ? $field.attr('data-filename') : '';
        var fileextension = typeof $field.attr('data-fileextension') === 'string' ? $field.attr('data-fileextension').toUpperCase() : '';
        var webservicetype = '';
        if ($browse.attr('data-type') === 'Browse') {
            webservicetype = 'module';
        } else if ($browse.attr('data-type') === 'Grid') {
            webservicetype = 'grid';
        }
        switch (fileextension) {
            case 'PDF':
                html.push('<div class="viewcell">');
                html.push('  <div class="col1"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/16/fileextension-pdf.png" style="vertical-align:middle;cursor:pointer;" /><span class="fileextension" style="padding:0 0 0 5px;">' + fileextension + '</span></div>');
                html.push('  <div class="col2"><i class="material-icons" title="E-mail Document">&#xE0BE;</i></div>'); //email
                html.push('</div>');
                break;
            case 'DOC': case 'DOCX': case 'TXT': case 'RTF':
                html.push('<div class="viewcell">');
                html.push('  <div class="col1"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/16/fileextension-document.png" style="vertical-align:middle;cursor:pointer;" /><span class="fileextension" style="padding:0 0 0 5px;">' + fileextension + '</span></div>');
                html.push('  <div class="col2"><i class="material-icons" title="E-mail Document">&#xE0BE;</i></div>'); //email
                html.push('</div>');
                break;
            case 'XLS': case 'XLSX': case 'CSV':
                html.push('<div class="viewcell">');
                html.push('  <div class="col1"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/16/fileextension-spreadsheet.png" style="vertical-align:middle;cursor:pointer;" /><span class="fileextension" style="padding:0 0 0 5px;">' + fileextension + '</span></div>');
                html.push('  <div class="col2"><i class="material-icons" title="E-mail Document">&#xE0BE;</i></div>'); //email
                html.push('</div>');
                break;
            case 'PNG': case 'JPG': case 'JPEG': case 'GIF': case 'TIF': case 'TIFF': case 'BMP':
                html.push('<div class="viewcell">');
                html.push('  <div class="col1"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/16/fileextension-image.png" style="vertical-align:middle;cursor:pointer;" /><span class="fileextension" style="padding:0 0 0 5px;">' + fileextension + '</span></div>');
                html.push('  <div class="col2"><i class="material-icons" title="E-mail Document">&#xE0BE;</i></div>'); //email
                html.push('</div>');
                break;
            default:
                if (appimageid.length > 0) {
                    html.push('<div class="viewcell">');
                    html.push('  <div class="col1"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/16/fileextension-generic.png" style="vertical-align:middle;cursor:pointer;" /><span class="fileextension" style="padding:0 0 0 5px;">' + fileextension + '</span></div>');
                    html.push('  <div class="col2"><i class="material-icons" title="E-mail Document">&#xE0BE;</i></div>'); //email
                    html.push('</div>');
                }
                break;
        }
        $field.empty();
        $adiContainer = jQuery(html.join(''));
        if (html.length > 0) {
            $adiContainer.on('click', 'img', function (event) {
                try {
                    window.open(`${applicationConfig.apiurl}api/v1/appimage/getimage?appimageid=${appimageid}`);
                    event.preventDefault();
                    return false;
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });

            $adiContainer.on('click', 'i', function (event) {
                var requestGetFromEmail, requestSendDocumentEmail, webserviceurl, $confirmation, $btnSend;
                try {
                    event.preventDefault();
                    requestGetFromEmail = {
                        module: $browse.attr('data-name')
                    };
                    webserviceurl = 'services.ashx?path=/' + webservicetype + '/' + $browse.attr('data-name') + '/GetFromEmail';
                    FwAppData.jsonPost(true, webserviceurl, requestGetFromEmail, FwServices.defaultTimeout,
                        function (responseGetFromEmail) {
                            try {
                                $confirmation = FwConfirmation.renderConfirmation(FwLanguages.translate('E-mail Document'), '');
                                var dlg = [];
                                dlg.push('<div style="width:540px;">');
                                dlg.push('  <div class="formrow">');
                                dlg.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                                dlg.push('      <div data-datafield="from" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="From" data-enabled="false"></div>');
                                dlg.push('    </div>');
                                dlg.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                                dlg.push('      <div data-datafield="tousers" data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="To (lookup)" data-validationname="PersonValidation" data-hasselectall="false" style="float:left;box-sizing:border-box;width:30%;"></div>');
                                dlg.push('      <div data-datafield="to" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="To" data-enabled="true" style="float:left;box-sizing:border-box;width:70%;"></div>');
                                dlg.push('    </div>');
                                dlg.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                                dlg.push('      <div data-datafield="ccusers" data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="CC (lookup)" data-validationname="PersonValidation"  data-hasselectall="false" style="float:left;box-sizing:border-box;width:30%;"></div>');
                                dlg.push('      <div data-datafield="cc" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="CC" data-enabled="true" style="float:left;box-sizing:border-box;width:70%;"></div>');
                                dlg.push('    </div>');
                                dlg.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                                dlg.push('      <div data-datafield="subject" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Subject" data-enabled="true"></div>');
                                dlg.push('    </div>');
                                dlg.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                                dlg.push('      <div data-datafield="body" data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Body" data-enabled="true"></div>');
                                dlg.push('    </div>');
                                dlg.push('  </div>');
                                dlg.push('</div>');
                                FwConfirmation.addControls($confirmation, dlg.join('\n'));
                                FwFormField.setValueByDataField($confirmation, 'from', responseGetFromEmail.fromemail);
                                FwFormField.setValueByDataField($confirmation, 'subject', filename);
                                $btnSend = FwConfirmation.addButton($confirmation, 'Send', false);
                                FwConfirmation.addButton($confirmation, 'Cancel');
                                $btnSend.click(function (event) {
                                    var $notification;
                                    try {
                                        if (FwFormField.getValueByDataField($confirmation, 'to').length === 0) {
                                            throw "To field is required.";
                                        }
                                        requestSendDocumentEmail = {
                                            module: $browse.attr('data-name'),
                                            appimageid: appimageid,
                                            filename: filename,
                                            fileextension: fileextension,
                                            email: FwBrowseColumn_appdocumentimage.getParameters($confirmation)
                                        };
                                        webserviceurl = 'services.ashx?path=/' + webservicetype + '/' + $browse.attr('data-name') + '/SendDocumentEmail';
                                        FwAppData.jsonPost(true, webserviceurl, requestSendDocumentEmail, FwServices.defaultTimeout,
                                            function (response) {
                                                try {
                                                    FwNotification.renderNotification('SUCCESS', 'Email Sent');
                                                    FwConfirmation.destroyConfirmation($confirmation);
                                                } catch (ex) {
                                                    FwFunc.showError(ex);
                                                } finally {
                                                    FwNotification.closeNotification($notification);
                                                }
                                            }, function (errorThrown) {
                                                FwNotification.closeNotification($notification);
                                                if (errorThrown !== 'abort') {
                                                    FwFunc.showError(errorThrown);
                                                }
                                            }, null
                                        );
                                        $notification = FwNotification.renderNotification('PERSISTENTINFO', 'Preparing Email...');
                                    } catch (ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                                $confirmation.on('change', '[data-datafield="tousers"] .fwformfield-value', function (event) {
                                    var requestGetEmailByWebUsersId = {
                                        module: $browse.attr('data-name'),
                                        webusersids: this.value,
                                        to: FwFormField.getValueByDataField($confirmation, 'to')
                                    };
                                    webserviceurl = 'services.ashx?path=/' + webservicetype + '/' + $browse.attr('data-name') + '/GetEmailByWebUsersId';
                                    FwAppData.jsonPost(true, webserviceurl, requestGetEmailByWebUsersId, FwServices.defaultTimeout,
                                        function (response) {
                                            try {
                                                FwFormField.setValueByDataField($confirmation, 'to', response.emailto);
                                                FwFormField.setValueByDataField($confirmation, 'tousers', '', '');
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        }, null, null
                                    );
                                });
                                $confirmation.on('change', '[data-datafield="ccusers"] .fwformfield-value', function (event) {
                                    var requestGetEmailByWebUsersId = {
                                        module: $browse.attr('data-name'),
                                        webusersids: this.value,
                                        to: FwFormField.getValueByDataField($confirmation, 'cc')
                                    };
                                    webserviceurl = 'services.ashx?path=/' + webservicetype + '/' + $browse.attr('data-name') + '/GetEmailByWebUsersId';
                                    FwAppData.jsonPost(true, webserviceurl, requestGetEmailByWebUsersId, FwServices.defaultTimeout,
                                        function (response) {
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
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });


            $field.append($adiContainer);
        }
    }
    //---------------------------------------------------------------------------------
    getParameters($form): any {
        var isvalid, $fields, $field, parameters = null;
        isvalid = FwModule.validateForm($form);
        if (isvalid) {
            parameters = {};
            $fields = $form.find('div[data-control="FwFormField"]');
            $fields.each(function (index, element) {
                $field = jQuery(element);
                if (typeof $field.attr('data-datafield') === 'string') {
                    parameters[$field.attr('data-datafield')] = FwFormField.getValue2($field);
                }
            });
        } else {
            throw 'Please fill in the required fields.';
        }

        return parameters;
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        var $adi_upload, $adi_progress, $adi_progressPopup;
        let html = [];
        html.push('<div class="editappdocumentimage">');
        html.push('  <img class="previewicon" src="data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==" />');
        html.push('  <div class="appdocumentimageupload"><input type="file" /></div>');
        html.push('  <div class="droporpastefilewrapper"><div class="droporpastefile" contenteditable="true" data-placeholder="drag or paste..."></div></div>');
        html.push('</div>');
        let htmlString = html.join('');
        $adi_upload = jQuery(htmlString)
            .on('paste', '.droporpastefile', function (event) {
                var items, file, isWebkit, $image, $form;
                try {
                    isWebkit = /webkit/.test(navigator.userAgent.toLowerCase());
                    $form = $field.closest('.fwform');
                    if (isWebkit) {
                        items = ((<any>event).clipboardData || (<any>event.originalEvent).clipboardData).items;
                        if (items.length === 0) throw 'Browsers only support pasting image data.  You can drag and drop files though.';
                        file = items[0].getAsFile();
                        switch (file.type) {
                            case "image/png":
                                file.name = "pastedimage.png";
                                $field.attr('data-ismodified', 'true');
                                FwBrowse.appdocumentimageLoadFile($browse, $field, file);
                                break;
                            case "image/jpg":
                            case "image/jpeg":
                                file.name = "pastedimage.jpg";
                                $field.attr('data-ismodified', 'true');
                                FwBrowse.appdocumentimageLoadFile($browse, $field, file);
                                break;
                            case "image/bmp":
                                file.name = "pastedimage.bmp";
                                $field.attr('data-ismodified', 'true');
                                FwBrowse.appdocumentimageLoadFile($browse, $field, file);
                                break;
                            case "image/tif":
                            case "image/tiff":
                                file.name = "pastedimage.tif";
                                $field.attr('data-ismodified', 'true');
                                FwBrowse.appdocumentimageLoadFile($browse, $field, file);
                                break;
                            case "image/gif":
                                file.name = "pastedimage.gif";
                                $field.attr('data-ismodified', 'true');
                                FwBrowse.appdocumentimageLoadFile($browse, $field, file);
                                break;
                            default:
                                throw 'Unsupported file type.';
                        }
                    }
                    else {
                        setTimeout(function () {
                            var dataUrl, file, filename;
                            try {
                                if ($field.find('.droporpastefile > img').length > 0) {
                                    dataUrl = $field.find('.droporpastefile > img').attr('src');
                                    file = dataUrl.toString().substring(dataUrl.indexOf(',') + 1, dataUrl.length);
                                    filename = 'image.' + dataUrl.toString().substring(dataUrl.indexOf('/') + 1, dataUrl.indexOf(';')).replace('jpeg', 'jpg');
                                    $field.data('filedataurl', dataUrl);
                                    $field.data('filepath', filename);
                                    $field.find('.droporpastefile').empty();
                                    if (dataUrl.indexOf('data:application/pdf;') === 0) {
                                        $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-pdf.png');
                                    } else if (dataUrl.indexOf('data:image/') === 0) {
                                        $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-image.png');
                                    } else if (dataUrl.indexOf('data:application/vnd.ms-excel;') === 0) { // mv 2018-06-22 converted from typescript and this was failing and doesn't make sense: || reader.result.indexOf('data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;') === 0) {
                                        $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-spreadsheet.png');
                                    } else if (dataUrl.indexOf('data:application/msword;') === 0) { // mv 2018-06-22 converted from typescript and this was failing and doesn't make sense: || reader.result.indexOf('data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;') === 0) {
                                        $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-document.png');
                                    } else {
                                        $field.find('.previewicon').attr('src', 'theme/fwimages/icons/16/fileextension-generic.png');
                                    }
                                }
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        }, 1);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('dragover', '.droporpastefile', function (event) {
                var dataTransfer;
                try {
                    event.stopPropagation();
                    event.preventDefault();
                    dataTransfer = (<any>event).dataTransfer || (<any>event.originalEvent).dataTransfer;
                    dataTransfer.dropEffect = 'copy';
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('drop', '.droporpastefile', function (event) {
                var file, filepath, dataTransfer;
                try {
                    event.stopPropagation();
                    event.preventDefault();
                    dataTransfer = (<any>event).dataTransfer || (<any>event.originalEvent).dataTransfer;
                    file = dataTransfer.files[0];
                    $field.attr('data-ismodified', 'true');
                    FwBrowse.appdocumentimageLoadFile($browse, $field, file);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('change', 'input[type="file"]', function (e) {
                var $file, file, filepath;
                try {
                    $file = $adi_upload.find('input[type=file]');
                    file = $file[0].files[0];
                    $field.attr('data-ismodified', 'true');
                    FwBrowse.appdocumentimageLoadFile($browse, $field, file);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            ;
        $field.empty().append($adi_upload);
    };
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_appdocumentimage = new FwBrowseColumn_appdocumentimageClass();