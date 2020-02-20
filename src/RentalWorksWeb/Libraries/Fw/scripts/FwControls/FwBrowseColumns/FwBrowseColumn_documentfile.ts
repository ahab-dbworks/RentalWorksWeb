class FwBrowseColumn_documentfileClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {
        if (typeof dt.ColumnIndex[$field.attr('data-appdocumentidfield')] !== 'number') throw 'FwBrowseColumn_documentfile.databindfield: ' + $field.attr('data-appdocumentidfield') + ' was not returned by the webservice.';
        if (typeof dt.ColumnIndex[$field.attr('data-filenamefield')] !== 'number') throw 'FwBrowseColumn_documentfile.databindfield: ' + $field.attr('data-filenamefield') + ' was not returned by the webservice.';
        if (typeof dt.ColumnIndex[$field.attr('data-fileextensionfield')] !== 'number') throw 'FwBrowseColumn_documentfile.databindfield: ' + $field.attr('data-fileextensionfield') + ' was not returned by the webservice.';
        if (typeof dt.ColumnIndex[$field.attr('data-hasfilefield')] !== 'number') throw 'FwBrowseColumn_documentfile.databindfield: ' + $field.attr('data-hasfilefield') + ' was not returned by the webservice.';
        var appdocumentid = dtRow[dt.ColumnIndex[$field.attr('data-appdocumentidfield')]];
        var documenttypeid = dtRow[dt.ColumnIndex[$field.attr('data-documenttypeidfield')]];
        var uniqueid1 = dtRow[dt.ColumnIndex[$field.attr('data-uniqueid1field')]];
        var uniqueid2 = dtRow[dt.ColumnIndex[$field.attr('data-uniqueid2field')]];
        var filename = dtRow[dt.ColumnIndex[$field.attr('data-filenamefield')]];
        var fileextension = dtRow[dt.ColumnIndex[$field.attr('data-fileextensionfield')]];
        var miscfield = dtRow[dt.ColumnIndex[$field.attr('data-miscfield')]];
        var hasfile = dtRow[dt.ColumnIndex[$field.attr('data-hasfilefield')]];
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
        $field.attr('data-hasfile', hasfile);
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
        throw 'FwBrowseColumn_documentfile.setFieldValue: Not Implemented!';
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = typeof $field.attr('data-ismodified') === 'string' && $field.attr('data-ismodified') === 'true';
        return isModified;
    }
    //---------------------------------------------------------------------------------
    b64toBlob(b64Data: string, contentType: string = '', sliceSize: number = 512): Blob {
        const byteCharacters = atob(b64Data);
        const byteArrays = [];

        for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            const slice = byteCharacters.slice(offset, offset + sliceSize);

            const byteNumbers = new Array(slice.length);
            for (let i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            const byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        const blob = new Blob(byteArrays, { type: contentType });
        return blob;
    }
    //---------------------------------------------------------------------------------
    downloadFile(url: string, filename: string): void {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', url, true);
        xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('apiToken'));
        xhr.responseType = 'blob';
        xhr.onerror = (e) => {
            if (xhr.status !== 200) {
                FwNotification.renderNotification('ERROR', 'Unable to download file.');
            }
        };
        xhr.onload = (e) => {
            if (xhr.status !== 200) return;
            const blob = <Blob>xhr.response;
            const windowUrl = window.URL || window.webkitURL;
            const url = windowUrl.createObjectURL(blob);
            const a = document.createElement("a");
            a.href = url;
            a.setAttribute("download", filename);
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
            windowUrl.revokeObjectURL(url);
        };
        xhr.send();
    }
    //---------------------------------------------------------------------------------
    openFile(url: string, filename: string): void {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', url, true);
        xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('apiToken'));
        xhr.responseType = 'blob';
        xhr.onerror = (e) => {
            if (xhr.status !== 200) {
                FwNotification.renderNotification('ERROR', 'Unable to download file.');
            }
        };
        xhr.onload = (e) => {
            if (xhr.status !== 200) return;
            const blob = <Blob>xhr.response;
            var dataurl = URL.createObjectURL(blob);
            var windowUrl = window.URL || window.webkitURL;
            window.open(dataurl);
            windowUrl.revokeObjectURL(url);
        };
        xhr.send();
    }
    //---------------------------------------------------------------------------------
    // form POST trick for authenticated file downloads, need to make the endpoint anonymous and manually validate jwtToken
    //https://stackoverflow.com/a/59363326
    //downloadWithJwtViaFormPost(url, id, token) {
    //    var jwtInput = $('<input type="hidden" name="jwtToken">').val(token);
    //    var idInput = $('<input type="hidden" name="id">').val(id);
    //    $('<form method="post" target="_blank"></form>')
    //                .attr("action", url)
    //                .append(jwtInput)
    //                .append(idInput)
    //                .appendTo('body')
    //                .submit()
    //                .remove();
    //}
    //---------------------------------------------------------------------------------
    async addFile($browse: JQuery, $fileInput: JQuery<HTMLInputElement>, baseapiurl: string, documentid: string): Promise<boolean> {
        let success = false;
        try {
            const uploadImageRequest = new FwAjaxRequest();
            uploadImageRequest.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/image`);
            uploadImageRequest.httpMethod = 'POST';
            uploadImageRequest.$elementToBlock = $browse;
            let file = null;
            if ($fileInput.length > 0 && $fileInput[0].files.length > 0) {
                file = $fileInput[0].files[0];
                const reader = new FileReader();
                reader.addEventListener("load", async() => {
                    uploadImageRequest.data = {
                        DataUrl: reader.result,
                        FileExtension: file.name.split('.').pop()
                    };
                    const response = await FwAjax.callWebApi<any, boolean>(uploadImageRequest);
                    if (response) {
                        success = true;
                        
                    } else {
                        FwNotification.renderNotification('ERROR', 'File upload failed.');
                    }
                }, false);
                reader.readAsDataURL(file);
            } else {
                //FwNotification.renderNotification('ERROR', 'No file selected to upload.');
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
        finally {
            $fileInput.val('');
        }
        return success;
    }
    //---------------------------------------------------------------------------------
    async deleteFile(baseapiurl: string, documentid: string): Promise<boolean> {
        const deleteImageRequest = new FwAjaxRequest();
        deleteImageRequest.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/file`);
        deleteImageRequest.httpMethod = 'DELETE';
        const isDeleted = await FwAjax.callWebApi<any, boolean>(deleteImageRequest);
        if (isDeleted) {
            
        }
        return isDeleted;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        let html = [];
        var $adiContainer;
        //var appimageid = typeof $field.attr('data-originalvalue') === 'string' ? $field.attr('data-originalvalue') : '';
        const baseapiurl = typeof $field.attr('data-baseapiurl') === 'string' ? $field.attr('data-baseapiurl') : '';
        const documentid = typeof $field.attr('data-appdocumentid') === 'string' ? $field.attr('data-appdocumentid') : '';
        const uniqueid1 = typeof $field.attr('data-uniqueid1') === 'string' ? $field.attr('data-uniqueid1') : '';
        var filename = typeof $field.attr('data-filename') === 'string' ? $field.attr('data-filename') : '';
        var fileextension = typeof $field.attr('data-fileextension') === 'string' ? $field.attr('data-fileextension').toUpperCase() : '';
        const hasfile: boolean = typeof $field.attr('data-hasfile') === 'string' ? $field.attr('data-hasfile') === 'true' : false;
        var webservicetype = '';
        if ($browse.attr('data-type') === 'Browse') {
            webservicetype = 'module';
        } else if ($browse.attr('data-type') === 'Grid') {
            webservicetype = 'grid';
        }
        let idAddImages: string = null;
        if (hasfile) {
            html.push('<i class="material-icons btnOpenFile" title="Open File" style="cursor:pointer;">open_in_new</i>');
            html.push('<i class="material-icons btnDownloadFile" title="Download File" style="cursor:pointer;margin:0 .5em 0 0;">cloud_download</i>');
            html.push(' | ');
            html.push('<i class="material-icons btnDeleteFile" title="Delete File" style="margin:0 0 0 .5em;cursor:pointer;">remove_circle_outline</i>');
            //html.push('  <div class="col2"><i class="material-icons" title="E-mail Document">&#xE0BE;</i></div>'); //email
        } 
        //else {
        //    idAddImages = FwAppData.generateUUID().replace(/-/g, '');
        //    html.push(`<label for="${idAddImages}" style="cursor:pointer;"><i class="material-icons" title="Add File" style="cursor:pointer;">add_circle_outline</i></label><input type="file" style="opacity:0;position:absolute;z-index:-1;" id="${idAddImages}" />`);
        //}
        $field.empty();
        $adiContainer = jQuery(html.join(''));
        if (html.length > 0) {
            $adiContainer.find('.btnOpenFile').on('click', (e: JQuery.ClickEvent) => {
                try {
                    e.stopPropagation();
                    this.openFile(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/file`, `${filename}.${fileextension}`);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            $adiContainer.find('.btnDownloadFile').on('click', (e: JQuery.ClickEvent) => {
                try {
                    e.stopPropagation();
                    this.downloadFile(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/file`, `${filename}.${fileextension}`);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            if (idAddImages !== null) {
                $adiContainer.find(`#${idAddImages}`).on('change', async (e: JQuery.ChangeEvent) => {
                    try {
                        e.stopPropagation();
                        const $fileInput = jQuery(e.currentTarget);
                        const success = await this.addFile($browse, $fileInput, baseapiurl, documentid);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
            $adiContainer.find('.btnDeleteFile').on('click', async (e: JQuery.ClickEvent) => {
                try {
                    e.stopPropagation();
                    const success = await this.deleteFile(baseapiurl, documentid);
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
        let html: string | string[] = [];
        const hasfile: boolean = typeof $field.attr('data-hasfile') === 'string' ? $field.attr('data-hasfile') === 'true' : false;
        let idAddImages: string = null;
        if (hasfile) {
            //html.push('  <div class="col1"><img class="btnOpenFile" src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/16/fileextension-pdf.png" style="vertical-align:middle;cursor:pointer;" /><span class="fileextension" style="padding:0 0 0 5px;">' + fileextension + '</span></div>');
            html.push('<i class="material-icons btnOpenFile" title="Open File" style="cursor:pointer;">open_in_new</i>');
            html.push('<i class="material-icons btnDownloadFile" title="Download File" style="cursor:pointer;margin:0 .5em 0 0;">cloud_download</i>');
            html.push(' | ');
            html.push('<i class="material-icons btnDeleteFile" title="Delete File" style="margin:0 0 0 .5em;cursor:pointer;">remove_circle_outline</i>');
            //html.push('  <div class="col2"><i class="material-icons" title="E-mail Document">&#xE0BE;</i></div>'); //email
        } else {
            idAddImages = FwAppData.generateUUID().replace(/-/g, '');
            html.push(`<label for="${idAddImages}" style="cursor:pointer;"><i class="material-icons" title="Add File" style="cursor:pointer;">add_circle_outline</i></label><input type="file" style="opacity:0;position:absolute;z-index:-1;" id="${idAddImages}" />`);
        }
    };
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_documentfile = new FwBrowseColumn_documentfileClass();