class FwBrowseColumn_documentimageClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {
        if (typeof dt.ColumnIndex[$field.attr('data-appdocumentidfield')] !== 'number') throw 'FwBrowseColumn_documentimage.databindfield: ' + $field.attr('data-appdocumentidfield') + ' was not returned by the webservice.';
        if (dt.ColumnIndex[$field.attr('data-hasimagesfield')] === undefined) throw 'FwBrowseColumn_documentimage.databindfield: ' + $field.attr('data-hasimagesfield') + ' was not returned by the webservice.';
        var appdocumentid = dtRow[dt.ColumnIndex[$field.attr('data-appdocumentidfield')]];
        var documenttypeid = dtRow[dt.ColumnIndex[$field.attr('data-documenttypeidfield')]];
        var uniqueid1 = dtRow[dt.ColumnIndex[$field.attr('data-uniqueid1field')]];
        var uniqueid2 = dtRow[dt.ColumnIndex[$field.attr('data-uniqueid2field')]];
        var hasimages = dtRow[dt.ColumnIndex[$field.attr('data-hasimagesfield')]];
        //var miscfield = dtRow[dt.ColumnIndex[$field.attr('data-miscfield')]];
        $field.attr('data-appdocumentid', appdocumentid);
        $field.attr('data-documenttypeid', documenttypeid);
        $field.attr('data-uniqueid1', uniqueid1);
        $field.attr('data-uniqueid2', uniqueid2);
        $field.attr('data-hasimages', hasimages);
        //$field.attr('data-miscfield', miscfield);
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
    async loadThumbnails($imagesPopup: JQuery, baseapiurl: string, documentid: string) {
        const request = new FwAjaxRequest();
        request.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/thumbnails?pageno=1&pagesize=10`);
        request.httpMethod = 'GET';
        const getThumbnailsResponse = await FwAjax.callWebApi<any, any>(request);
        let thumbnails = [];
        if (getThumbnailsResponse !== undefined && getThumbnailsResponse !== null && getThumbnailsResponse.Thumbnails !== undefined && getThumbnailsResponse.Thumbnails !== null) {
            thumbnails = getThumbnailsResponse.Thumbnails;
        }
        let html: string | string[] = [];
        for (let i = 0; i < thumbnails.length; i++) {
            const thumbnail = thumbnails[i];
            html.push(`<div class="thumbnail" data-imageid="${thumbnail.ImageId}" style="width:128px;max-height:128px;cursor:pointer;">`);
            html.push(`  <div style="display:flex;align-items:center;justify-content:center;"><img src="${thumbnail.DataUrl}" /></div>`);
            html.push(`</div>`);
        }
        html = html.join('\n');
        $imagesPopup.find('.thumbnails').html(html);
        
        // click on the first thumbnail so that a fullsize image is visible
        const $allThumbnails = $imagesPopup.find('.thumbnail')
        if ($allThumbnails.length > 0) {
            $allThumbnails.eq(0).click();
        }
    }
    //---------------------------------------------------------------------------------
    addImages($imagesPopup: JQuery, baseapiurl: string, documentid: string) {
        const $pasteImageConfirmation = FwConfirmation.renderConfirmation('Upload Images', '');
        let pasteImageHtml: string | string[] = [];
        pasteImageHtml.push('');
        pasteImageHtml.push('<div class="editappdocumentimage">');
        pasteImageHtml.push('    <img class="previewicon" src="data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==">');
        pasteImageHtml.push('    <div class="appdocumentimageupload">');
        pasteImageHtml.push('        <input class="fileinput" type="file">');
        pasteImageHtml.push('    </div>');
        pasteImageHtml.push('    <div class="droporpastefilewrapper">');
        pasteImageHtml.push('        <div class="droporpastefile" contenteditable="true" data-placeholder="drag or paste..."></div>');
        pasteImageHtml.push('    </div>');
        pasteImageHtml.push('</div>');
        pasteImageHtml = pasteImageHtml.join('\n');
        const $pasteImage = jQuery(pasteImageHtml);
        FwConfirmation.addJqueryControl($pasteImageConfirmation, $pasteImage);

        // Add Upload button
        const $btnUploadImage = FwConfirmation.addButton($pasteImageConfirmation, 'Upload', false);
        $btnUploadImage.on('click', (e: JQuery.ClickEvent) => {
            const uploadImageRequest = new FwAjaxRequest();
            uploadImageRequest.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/image`);
            uploadImageRequest.httpMethod = 'POST';
            const $fileInput = <JQuery<HTMLInputElement>>$pasteImage.find('.fileinput');
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
                        FwNotification.renderNotification('SUCCESS', 'Image upload succeeded.');
                        $fileInput.val('');
                        await this.loadThumbnails($imagesPopup, baseapiurl, documentid);
                    } else {
                        FwNotification.renderNotification('ERROR', 'Image upload failed.');
                    }
                }, false);
                reader.readAsDataURL(file);
            } else {
                FwNotification.renderNotification('ERROR', 'No file selected to upload.');
            }
        });
                        
        // Add Close button
        const $btnClosePasteImage = FwConfirmation.addButton($pasteImageConfirmation, 'Close', true);
        $btnClosePasteImage.on('click', (e: JQuery.ClickEvent) => {
            FwConfirmation.destroyConfirmation($pasteImageConfirmation);
        });

        $btnClosePasteImage.focus();
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        let html: string[] | string = [];
        var $adiContainer;
        var appimageid = typeof $field.attr('data-originalvalue') === 'string' ? $field.attr('data-originalvalue') : '';
        const hasimages: boolean = typeof $field.attr('data-hasimages') === 'string' ? $field.attr('data-hasimages') === 'true' : false;
        const baseapiurl = typeof $field.attr('data-baseapiurl') === 'string' ? $field.attr('data-baseapiurl') : '';
        const documentid = typeof $field.attr('data-appdocumentid') === 'string' ? $field.attr('data-appdocumentid') : '';
        var webservicetype = '';
        if ($browse.attr('data-type') === 'Browse') {
            webservicetype = 'module';
        } else if ($browse.attr('data-type') === 'Grid') {
            webservicetype = 'grid';
        }
        if (hasimages) {
            html.push('<i class="material-icons btnImages" title="View Images" style="cursor:pointer;">collections</i>');
        }
        html = html.join('\n');
        $field.html(html);
        $field.find('.btnImages').on('click', async (event: JQuery.ClickEvent) => {
            try {
                //window.open(`${applicationConfig.apiurl}api/v1/appimage/getimage?appimageid=${appimageid}`);
                
                // create confirmation box
                const $confirmation = FwConfirmation.renderConfirmation('Images', '');
                
                // build content for Images Popup
                let html: string[] | string = [];
                html.push('<div style="display:flex;flex-direction:column;flex: 1 1 0;">');
                //html.push('  <div class="largeimagecontainer" style="flex:1 1 0;display:flex;align-items:center;justify-content:center;">');
                html.push('  <div class="largeimagecontainer" style="flex:1 1 0;position:relative;">');
                //html.push('    <i class="material-icons btnLeft" style="font-size:44px;position:absolute;top:50%;left:0;color:#ffffff;cursor:pointer;">keyboard_arrow_left</i>');
                //html.push('    <i class="material-icons btnRight" style="font-size:44px;position:absolute;top:50%;right:0;color:#ffffff;cursor:pointer;">keyboard_arrow_right</i>');
                //html.push(`    <img class="largeimage" src="data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==" style="max-width:100%;" />`);
                html.push('  </div>');
                //html.push('  <div style="flex:0 0 auto;text-align:center;">');
                //html.push('    <a src="javascript:{}"style="color:#0000ff;text-decoration:underline;cursor:pointer;">Set a description</a>')
                ////html.push('    <div>Image Description</div>');
                ////html.push('    <div><input type="text" /></div>');
                //html.push('  </div>');
                //html.push('  <div style="flex:0 0 auto;">');
                //html.push('    <div>Image Number</div>');
                //html.push('    <div><input type="text" /></div>');
                //html.push('  </div>');
                html.push('  <div style="flex:0 0 auto;">');
                html.push('    <div class="toolbar" style="display:flex;justify-content:center;">');
                html.push('      <i class="material-icons btnAdd" style="font-size:44px;cursor:pointer;color:#ffffff;">library_add</i>');
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="thumbnails" style="flex:0 0 100px;display:flex;justify-content:center;overflow-x:auto;">');
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                const $imagesPopup = jQuery(html);
                FwConfirmation.addJqueryControl($confirmation, $imagesPopup);

                // Add Images
                const $btnAddImages = FwConfirmation.addButton($confirmation, 'Add Images', false);
                $btnAddImages.on('click', (e: JQuery.ClickEvent) => {
                    try {
                        this.addImages($imagesPopup, baseapiurl, documentid);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                
                // Add Close button
                const $btnClose = FwConfirmation.addButton($confirmation, 'Close', true);
                $btnClose.on('click', (e: JQuery.ClickEvent) => {
                    try {
                        FwConfirmation.destroyConfirmation($confirmation);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                
                // adjust css styles and positioning on the confirmation box
                $confirmation.find('.fwconfirmationbox').css({
                    width: '95vw',
                    height: '95vh',
                    display: 'flex',
                    flexDirection: 'column',
                    backgroundColor: 'rgba(0,0,0,0.85)'
                });
                $confirmation.find('.title,.more,.fwconfirmation-buttonbar').css({
                    flex: '0 0 auto'
                });
                $confirmation.find('.fwconfirmation-button').css({
                    backgroundColor: '#ffc107'
                });
                $confirmation.find('.title').css({
                    backgroundColor: 'unset'
                });
                $confirmation.find('.body').css({
                    flex: '1 1 0',
                    display: 'flex',
                    maxHeight: '100vh',
                    overflow: 'hidden'
                });
                $confirmation.find('.fwform').css({
                    flex: '1 1 0',
                    display: 'flex',
                    flexDirection: 'column'
                });
                
                // register event handlers
                $imagesPopup.on('click', '.thumbnail', async(e: JQuery.ClickEvent) => {
                    try {
                        const $thumbnail = jQuery(e.currentTarget);
                        const request = new FwAjaxRequest();
                        request.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/image/${$thumbnail.attr('data-imageid')}`);
                        request.httpMethod = 'GET';
                        const getImageResponse = await FwAjax.callWebApi<any, any>(request);
                        if (getImageResponse !== undefined && getImageResponse !== null && getImageResponse.Image !== undefined && getImageResponse.Image !== null) {
                            //$images.find('.largeimage').attr('src', getImageResponse.Image.DataUrl);
                            
                            $imagesPopup.find('.largeimagecontainer').css({
                                backgroundImage: `url('${getImageResponse.Image.DataUrl}')`,
                                backgroundSize: 'contain',
                                backgroundPosition: 'center center',
                                backgroundRepeat: 'no-repeat'
                            });
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                $imagesPopup.on('click', '.btnAdd', async(e: JQuery.ClickEvent) => {
                    try {
                        this.addImages($imagesPopup, baseapiurl, documentid);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })

                await this.loadThumbnails($imagesPopup, baseapiurl, documentid);

                $btnClose.focus();
                event.preventDefault();
                event.stopPropagation();
                return false;
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
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

var FwBrowseColumn_documentimage = new FwBrowseColumn_documentimageClass();