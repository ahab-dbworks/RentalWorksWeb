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
    getFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, field: any, originalvalue: any): void {
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
    async loadThumbnails($field: JQuery, $imagesPopup: JQuery, baseapiurl: string, documentid: string) {
        const request = new FwAjaxRequest();
        request.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/thumbnails?pageno=1&pagesize=50`);
        request.httpMethod = 'GET';
        const getThumbnailsResponse = await FwAjax.callWebApi<any, any>(request);
        let thumbnails = [];
        if (getThumbnailsResponse !== undefined && getThumbnailsResponse !== null && getThumbnailsResponse.Thumbnails !== undefined && getThumbnailsResponse.Thumbnails !== null) {
            thumbnails = getThumbnailsResponse.Thumbnails;
        }
        let html: string | string[] = [];
        for (let i = 0; i < thumbnails.length; i++) {
            const thumbnail = thumbnails[i];
            html.push(`<div class="thumbnail" data-imageid="${thumbnail.ImageId}" style="width:128px;max-height:128px;cursor:pointer;float:left;">`);
            html.push(`  <div style="display:flex;align-items:center;justify-content:center;"><img src="${thumbnail.DataUrl}" /></div>`);
            html.push(`</div>`);
        }
        html = html.join('\n');
        $imagesPopup.find('.thumbnails').width(128 * thumbnails.length).html(html);
        
        // click on the first thumbnail so that a fullsize image is visible
        const $allThumbnails = $imagesPopup.find('.thumbnail');
        const $btnImageViewer = $field.find('.btnImageViewer');
        if ($allThumbnails.length > 0) {
            $btnImageViewer.text('collections');
            $allThumbnails.eq(0).click();
        } else {
            $btnImageViewer.text('add_circle_outline');
            $imagesPopup.find('.largeimagecontainer').css({
                backgroundImage: 'url(data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==)'
            })
        }
    }
    //---------------------------------------------------------------------------------
    addImages($field: JQuery, $imagesPopup: JQuery, baseapiurl: string, documentid: string) {
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

        $pasteImage.find('.fileinput').on('change', (e: JQuery.ChangeEvent) => {
            try {
                const uploadImageRequest = new FwAjaxRequest();
                uploadImageRequest.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/image`);
                uploadImageRequest.httpMethod = 'POST';
                uploadImageRequest.$elementToBlock = $imagesPopup;
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
                            $fileInput.val('');
                            await this.loadThumbnails($field, $imagesPopup, baseapiurl, documentid);
                        } else {
                            FwNotification.renderNotification('ERROR', 'Image upload failed.');
                        }
                    }, false);
                    reader.readAsDataURL(file);
                } else {
                    FwNotification.renderNotification('ERROR', 'No file selected to upload.');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        // Add Upload button
        //const $btnUploadImage = FwConfirmation.addButton($pasteImageConfirmation, 'Upload', false);
        //$btnUploadImage.on('click', (e: JQuery.ClickEvent) => {
        //    const uploadImageRequest = new FwAjaxRequest();
        //    uploadImageRequest.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/image`);
        //    uploadImageRequest.httpMethod = 'POST';
        //    const $fileInput = <JQuery<HTMLInputElement>>$pasteImage.find('.fileinput');
        //    let file = null;
        //    if ($fileInput.length > 0 && $fileInput[0].files.length > 0) {
        //        file = $fileInput[0].files[0];
        //        const reader = new FileReader();
        //        reader.addEventListener("load", async() => {
        //            uploadImageRequest.data = {
        //                DataUrl: reader.result,
        //                FileExtension: file.name.split('.').pop()
        //            };
        //            const response = await FwAjax.callWebApi<any, boolean>(uploadImageRequest);
        //            if (response) {
        //                FwNotification.renderNotification('SUCCESS', 'Image upload succeeded.');
        //                $fileInput.val('');
        //                await this.loadThumbnails($field, $imagesPopup, baseapiurl, documentid);
        //            } else {
        //                FwNotification.renderNotification('ERROR', 'Image upload failed.');
        //            }
        //        }, false);
        //        reader.readAsDataURL(file);
        //    } else {
        //        FwNotification.renderNotification('ERROR', 'No file selected to upload.');
        //    }
        //});
        
        // Add Close button
        const $btnClosePasteImage = FwConfirmation.addButton($pasteImageConfirmation, 'Close', true);

        $btnClosePasteImage.focus();
    }
    //---------------------------------------------------------------------------------
    async deleteImage($field: JQuery, $imagesPopup: JQuery, baseapiurl: string, documentid: string, imageid: string): Promise<boolean> {
        const deleteImageRequest = new FwAjaxRequest();
        deleteImageRequest.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/image/${imageid}`);
        deleteImageRequest.httpMethod = 'DELETE';
        const isDeleted = await FwAjax.callWebApi<any, boolean>(deleteImageRequest);
        if (isDeleted) {
            this.loadThumbnails($field, $imagesPopup, baseapiurl, documentid);
        }
        return isDeleted;
    }
    //---------------------------------------------------------------------------------
    async openImageViewer($browse: JQuery, $tr: JQuery, $field: JQuery, baseapiurl: string, documentid: string) {
        let mode: 'VIEW'|'NEW'|'EDIT' = 'VIEW';
        if ($tr.hasClass('viewmode')) {
            mode = 'VIEW';
        }
        else if ($tr.hasClass('newmode')) {
            mode = 'NEW';
        }
        else if ($tr.hasClass('editmode')) {
            mode = 'EDIT';
        }
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
        //html.push('    <div class="toolbar" style="display:flex;justify-content:center;">');
        //html.push('      <i class="material-icons btnAdd" style="font-size:44px;cursor:pointer;color:#ffffff;">library_add</i>');
        //html.push('    </div>');
        html.push('  </div>');
        html.push('  <div class="thumbnailscontainer" style="flex:0 0 100px;position:relative;overflow:hidden;overflow-x:auto;">');
        html.push('    <div class="thumbnails" style="position:absolute;white-space:no-wrap;/*flex:0 0 100px;display:flex;justify-content:center;*/"></div>');
        html.push('  </div>');
        html.push('</div>');
        html = html.join('\n');
        const $imagesPopup = jQuery(html);
        FwConfirmation.addJqueryControl($confirmation, $imagesPopup);

        // Add Images
        if (mode === 'NEW' || mode === 'EDIT') {
            const $btnAddImages = FwConfirmation.addButton($confirmation, 'Add Images', false);
            const idAddImages = FwAppData.generateUUID().replace(/-/g, '');
            $btnAddImages.html(`<label for="${idAddImages}" style="cursor:pointer;">Add Images</label><input type="file" style="opacity:0;position:absolute;z-index:-1;" id="${idAddImages}" />`);
            $btnAddImages.find(`#${idAddImages}`).on('change', (e: JQuery.ChangeEvent) => {
                const $fileInput = jQuery<HTMLInputElement>(e.currentTarget);
                try {
                    const uploadImageRequest = new FwAjaxRequest();
                    uploadImageRequest.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/image`);
                    uploadImageRequest.httpMethod = 'POST';
                    uploadImageRequest.$elementToBlock = $imagesPopup;
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
                                await this.loadThumbnails($field, $imagesPopup, baseapiurl, documentid);
                            } else {
                                FwNotification.renderNotification('ERROR', 'Image upload failed.');
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
            });
        }

        //const $btnAddImages = FwConfirmation.addButton($confirmation, 'Add Images', false);
        //$btnAddImages.on('click', (e: JQuery.ClickEvent) => {
        //    try {
        //        this.addImages($field, $imagesPopup, baseapiurl, documentid);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});

        // Delete Image
        if (mode === 'NEW' || mode === 'EDIT') {
            const $btnDeleteImage = FwConfirmation.addButton($confirmation, 'Delete Image', false);
            $btnDeleteImage.on('click', (e: JQuery.ClickEvent) => {
                try {
                    const $confirmDelete = FwConfirmation.renderConfirmation('Confirm', 'Delete this Image?');
                    const $btnDelete = FwConfirmation.addButton($confirmDelete, 'Delete', false);
                    $btnDelete.on('click', async (e: JQuery.ClickEvent) => {
                        try {
                            const imageid = $imagesPopup.find('.largeimagecontainer').attr('data-imageid');
                            const isDeleted = await this.deleteImage($field, $imagesPopup, baseapiurl, documentid, imageid);
                            if (isDeleted) {
                                FwConfirmation.destroyConfirmation($confirmDelete);
                            }
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    const $btnClose = FwConfirmation.addButton($confirmDelete, 'Close', true);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        
        // Add Close button
        const $btnClose = FwConfirmation.addButton($confirmation, 'Close', true);
        
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
                const imageid = $thumbnail.attr('data-imageid');
                request.url = encodeURI(`${applicationConfig.apiurl}${baseapiurl}/${documentid}/image/${imageid}`);
                request.httpMethod = 'GET';
                const getImageResponse = await FwAjax.callWebApi<any, any>(request);
                if (getImageResponse !== undefined && getImageResponse !== null && getImageResponse.Image !== undefined && getImageResponse.Image !== null) {
                    //$images.find('.largeimage').attr('src', getImageResponse.Image.DataUrl);
                            
                    $imagesPopup.find('.largeimagecontainer')
                        .attr('data-imageid', imageid)
                        .css({
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
                this.addImages($field, $imagesPopup, baseapiurl, documentid);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })

        await this.loadThumbnails($field, $imagesPopup, baseapiurl, documentid);

        $btnClose.focus();
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
            html.push('<i class="material-icons btnImageViewer" title="View Images" style="cursor:pointer;color:#000000;">collections</i>');
        } 
        //else {
        //    html.push('<i class="material-icons btnImageViewer" title="Add Images" style="cursor:pointer;">add_circle_outline</i>');
        //}
        html = html.join('\n');
        $field.html(html);
        $field.find('.btnImageViewer').on('click', async (event: JQuery.ClickEvent) => {
            try {
                event.stopPropagation();
                this.openImageViewer($browse, $tr, $field, baseapiurl, documentid);
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
        var appimageid = typeof $field.attr('data-originalvalue') === 'string' ? $field.attr('data-originalvalue') : '';
        const hasimages: boolean = typeof $field.attr('data-hasimages') === 'string' ? $field.attr('data-hasimages') === 'true' : false;
        const baseapiurl = typeof $field.attr('data-baseapiurl') === 'string' ? $field.attr('data-baseapiurl') : '';
        const documentid = typeof $field.attr('data-appdocumentid') === 'string' ? $field.attr('data-appdocumentid') : '';
        let html: string | string[] = [];
        if (hasimages) {
            html.push('<i class="material-icons btnImageViewer" title="View Images" style="cursor:pointer;color:#000000;">collections</i>');
        } else {
            html.push('<i class="material-icons btnImageViewer" title="Add Images" style="cursor:pointer;color:#000000;">add_circle_outline</i>');
        }
        html = html.join('');
        $field.html(html);
        $field.find('.btnImageViewer').on('click', async (event: JQuery.ClickEvent) => {
            try {
                event.stopPropagation();
                this.openImageViewer($browse, $tr, $field, baseapiurl, documentid);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_documentimage = new FwBrowseColumn_documentimageClass();