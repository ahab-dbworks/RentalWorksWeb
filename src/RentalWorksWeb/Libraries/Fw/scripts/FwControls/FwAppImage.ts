class FwAppImageClass {
    blankDataUrl = 'data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==';
    //---------------------------------------------------------------------------------
    init = ($control: JQuery) => {
        $control
            .on('click', '.btnAdd', (event) => {
                var $confirmation;
                try {
                    var html = [];
                    var ismobile = jQuery('html').hasClass('mobile');
                    var isdesktop = jQuery('html').hasClass('desktop');
                    var iscordova = (typeof (<any>navigator).camera !== 'undefined') && (typeof (<any>navigator).camera.getPicture === 'function');
                    if (iscordova) {
                        html.push('Take a picture...');
                    } else {
                        html.push('<div class="addimage">');
                        html.push('  <div class="droporpastefilewrapper" style="border:1px dashed #000000;height:100px;width:300px;position:relative;">');
                        if (/Trident/.test(navigator.userAgent)) {
                            html.push('    <div style="position:absolute;top:0;right:0;bottom:0;left:0;z-index:1;text-align:center;line-height:100px;box-sizing:border-box;">drag image here</div>');
                            html.push('    <div class="pasteimage" style="position:absolute;top:0;right:0;bottom:0;left:0;z-index:2;line-height:100px;padding:0 10px;box-sizing:border-box;cursor:text;"></div>');
                        } else {
                            html.push('    <div style="position:absolute;top:0;right:0;bottom:0;left:0;z-index:1;text-align:center;line-height:100px;box-sizing:border-box;">drag or paste image here</div>');
                            html.push('    <div class="pasteimage" contenteditable="true" style="position:absolute;top:0;right:0;bottom:0;left:0;z-index:2;line-height:100px;padding:0 10px;box-sizing:border-box;cursor:text;"></div>');
                        }
                        html.push('  </div>');
                        html.push('  <input class="inputfile" type="file" multiple style="opacity:0;width:0;height:0;">');
                        html.push('</div>');
                    }
                    let htmlString = html.join('\n');
                    $confirmation = FwConfirmation.renderConfirmation('Upload Image', htmlString);
                    $confirmation.addClass('FwAppImageUploadImageConfirmation')
                    if (iscordova) {
                        var $btnTakePicture = FwConfirmation.addButton($confirmation, 'Take a Picture', false);
                        $btnTakePicture.on('click', function () {
                            try {
                                (<any>navigator).camera.getPicture(
                                    //success
                                    function (imageUri) {
                                        var img, request, $images, i;
                                        try {
                                            if ((typeof window.screen !== 'undefined') && (typeof (<any>window).screen.lockOrientation === 'function')) {
                                                (<any>window).screen.lockOrientation('portrait-primary');
                                            };
                                            var $image = jQuery(FwAppImage.getAddImageHtml($control));
                                            var dataUrl = 'data:image/jpeg;base64,' + imageUri;
                                            let $fullsizeimage = $image.find('.fullsizeimage');
                                            $fullsizeimage.attr('src', dataUrl);
                                            $fullsizeimage.attr('data-mimetype', 'image/jpeg');
                                            $fullsizeimage.attr('data-filename', 'attachment.jpg');
                                            $image.find('.btnClear').show();
                                            $fullsizeimage = $image.find('.fullsizeimage');
                                            FwAppImage.addImage($control, $image);
                                        } catch (ex) {
                                            FwFunc.showError(ex);
                                        }
                                    }
                                    //error
                                    , function (message) {
                                        try {
                                            if ((typeof window.screen !== 'undefined') && (typeof (<any>window).screen.lockOrientation === 'function')) {
                                                (<any>window).screen.lockOrientation('portrait-primary');
                                            }
                                        } catch (ex) {
                                            FwFunc.showError(ex);
                                        }
                                    }
                                    , {
                                        destinationType: (<any>window).Camera.DestinationType.DATA_URL
                                        , sourceType: (<any>window).Camera.PictureSourceType.CAMERA
                                        , allowEdit: false
                                        , correctOrientation: true
                                        , encodingType: (<any>window).Camera.EncodingType.JPEG
                                        , quality: applicationConfig.photoQuality
                                        , targetWidth: applicationConfig.photoWidth
                                        , targetHeight: applicationConfig.photoHeight
                                    }
                                );
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } else {
                        var $btnSelectFile = FwConfirmation.addButton($confirmation, 'or Select File...', false);
                        $btnSelectFile.on('click', function () {
                            try {
                                var $inputfile = $confirmation.find('.inputfile');
                                $inputfile.click();
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                        $btnSelectFile.focus();
                    }
                    FwConfirmation.addButton($confirmation, 'Close', true);
                    $confirmation
                        .on('change', '.inputfile', async (e: JQuery.ChangeEvent) => {
                            var blob;
                            try {
                                //for (var i = 0; i < this.files.length; i++) {
                                //    if (this.files[i].type.indexOf("image") === 0) {
                                //        blob = this.files[i];
                                //        break;
                                //    }
                                //}
                                //var $image = jQuery(FwAppImage.getAddImageHtml($control));
                                //FwAppImage.fileToDataUrl($control, $image, blob);
                                await this.addFiles($control, e.target.files);
                                FwConfirmation.destroyConfirmation($confirmation);
                            } catch (ex) {
                                jQuery(this).val('');
                                FwFunc.showError(ex);
                            }
                        })
                        .on('paste', '.pasteimage', function (event) {
                            try {
                                FwAppImage.pasteImage($control, this, event);
                                FwConfirmation.destroyConfirmation($confirmation);
                            } catch (ex) {
                                jQuery(this).empty();
                                FwFunc.showError('Please paste an image.');
                            }
                            event.preventDefault();
                            return false;
                        })
                        .on('dragover', '.pasteimage', function (event) {
                            try {
                                var $this = jQuery(this);
                                FwAppImage.dragoverImage($control, this, event);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        })
                        .on('dragenter', '.pasteimage', function (event) {
                            try {
                                var $this = jQuery(this);
                                $this.addClass('dragover');
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        })
                        .on('dragleave', '.pasteimage', function (event) {
                            try {
                                var $this = jQuery(this);
                                $this.removeClass('dragover');
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        })
                        .on('drop', '.pasteimage', function (event) {
                            try {
                                var $pasteimage = jQuery(this);
                                if (($pasteimage.length === 1) && ($pasteimage.is(':visible'))) {
                                    FwAppImage.dropImage($control, event);
                                }
                                FwConfirmation.destroyConfirmation($confirmation);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        })
                        ;
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnRefresh', function (event) {
                try {
                    FwAppImage.getAppImages($control);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnDelete', function (e) {
                try {
                    if ($control.find('.thumb').length > 0) {
                        const $fullsizeimage = $control.find('.fullsizeimage');
                        const appimageid = $fullsizeimage.attr('data-appimageid');
                        const $deleteThumbnail = $control.find(`.thumb[data-appimageid="${appimageid}"]`);
                        const $confirmation = FwConfirmation.renderConfirmation('Confirm', 'Delete Image?');
                        const $btnOk = FwConfirmation.addButton($confirmation, 'OK');
                        $btnOk.focus();
                        FwConfirmation.addButton($confirmation, 'Cancel');
                        $btnOk.on('click', function () {
                            FwAppImage.deleteImage($control, $fullsizeimage);
                            let deleteThumnailNo = -1;
                            let $thumbs = $control.find('.thumb');
                            for (let i = 0; i < $thumbs.length; i++) {
                                if ($thumbs.eq(i).attr('data-appimageid') === appimageid) {
                                    deleteThumnailNo = i;
                                    break;
                                }
                            }
                            if (deleteThumnailNo >= 0) {
                                $deleteThumbnail.remove();
                                $thumbs = $control.find('.thumb');
                                let newThumbnailNo = deleteThumnailNo;
                                if ($thumbs.length === 0) {
                                    FwAppImage.selectImage($control, null, null);
                                } else {
                                    if (newThumbnailNo > $thumbs.length - 1) {
                                        newThumbnailNo = $thumbs.length - 1;
                                    }
                                    const $newThumbnail = $thumbs.eq(newThumbnailNo);
                                    if ($newThumbnail.length > 0) {
                                        let ct_appimageid = $newThumbnail.attr('data-appimageid');
                                        let ct_datestamp = $newThumbnail.attr('data-datestamp');
                                        FwAppImage.selectImage($control, ct_appimageid, ct_datestamp);
                                        //FwAppImage.updatePageInfo($control, newThumbnailNo + 1, $thumbs.length);
                                    } else {
                                        FwAppImage.selectImage($control, null, null);
                                    }
                                }
                            } else {
                                FwAppImage.selectImage($control, null, null);
                            }
                        });
                    }
                    return false;
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnPrevImage', function (e) {
                try {
                    const $fullsizeimage = $control.find('.fullsizeimage');
                    const appimageid = $fullsizeimage.attr('data-appimageid');
                    //const $currentThumbnail = $control.find(`.thumb[data-appimageid="${appimageid}"]`);
                    let currentThumnailNo = -1;
                    const $thumbs = $control.find('.thumb');
                    for (let i = 0; i < $thumbs.length; i++) {
                        if ($thumbs.eq(i).attr('data-appimageid') === appimageid) {
                            currentThumnailNo = i;
                            break;
                        }
                    }
                    if (currentThumnailNo > 0) {
                        const clickThumbnailNo = currentThumnailNo - 1;
                        const $clickThumnail = $thumbs.eq(clickThumbnailNo);
                        if ($clickThumnail.length > 0) {
                            let ct_appimageid = $clickThumnail.attr('data-appimageid');
                            let ct_datestamp = $clickThumnail.attr('data-datestamp');
                            FwAppImage.selectImage($control, ct_appimageid, ct_datestamp);
                            //FwAppImage.updatePageInfo($control, clickThumbnailNo + 1, $thumbs.length);
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnNextImage', function (e) {
                try {
                    const $fullsizeimage = $control.find('.fullsizeimage');
                    const appimageid = $fullsizeimage.attr('data-appimageid');
                    //const $currentThumbnail = $control.find(`.thumb[data-appimageid="${appimageid}"]`);
                    let currentThumnailNo = -1;
                    const $thumbs = $control.find('.thumb');
                    for (let i = 0; i < $thumbs.length; i++) {
                        if ($thumbs.eq(i).attr('data-appimageid') === appimageid) {
                            currentThumnailNo = i;
                            break;
                        }
                    }
                    if (currentThumnailNo >= 0 && currentThumnailNo < $thumbs.length - 1) {
                        const clickThumbnailNo = currentThumnailNo + 1;
                        const $clickThumnail = $thumbs.eq(clickThumbnailNo);
                        if ($clickThumnail.length > 0) {
                            let ct_appimageid = $clickThumnail.attr('data-appimageid');
                            let ct_datestamp = $clickThumnail.attr('data-datestamp');
                            FwAppImage.selectImage($control, ct_appimageid, ct_datestamp);
                            //FwAppImage.updatePageInfo($control, clickThumbnailNo + 1, $thumbs.length);
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.fullsizeimage', function (event) {
                try {
                    var appimageid = jQuery(this).attr('data-appimageid');
                    if (appimageid.length > 0) {
                        var html = [];
                        html.push(`<div style="position:absolute;top:0;right:0;bottom:0;left:0;background-repeat:no-repeat;background-size:contain;background-position:center center;background-image:url(${applicationConfig.apiurl}api/v1/appimage/getimage?appimageid=${appimageid}&thumbnail=false)"></div>`);
                        let htmlString = html.join('\n');
                        let title = '<i class="material-icons btnClose" style="cursor:pointer;color:#ffffff">&#xE5CD;</i>';
                        var $confirmation = FwConfirmation.renderConfirmation(title, '');
                        $confirmation.find('.body')
                            .css({
                                justifyContent: 'center',
                                alignItems: 'center',
                                position: 'relative'
                            })
                            .html(htmlString);
                        //$confirmation.find('.message').css({
                        //    'text-align': 'center'
                        //})
                        //var $btnClose = FwConfirmation.addButton($confirmation, 'Close', true);
                        $confirmation.on('click', '.btnClose', (e) => {
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
                            backgroundColor: 'unset',
                            display: 'flex',
                            justifyContent: 'flex-end'
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
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.thumb', function (event) {
                try {
                    const $this = jQuery(this);
                    const appimageid = $this.attr('data-appimageid');
                    const datestamp = $this.attr('data-datestamp');
                    FwAppImage.selectImage($control, appimageid, datestamp);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnToggleThumbnails', function (event) {
                try {
                    const $thumbnails = $control.find('.thumbnails');
                    const showThumbnails = !$thumbnails.is(':visible');
                    $control.attr('data-showthumbnails', showThumbnails.toString());
                    $thumbnails.toggle(showThumbnails);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            ;
        if ($control.attr('data-hasadd') !== 'false') {
            $control.on('drop', '.image', function (event) {
                try {
                    FwAppImage.dropImage($control, event);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        }
    };
    //---------------------------------------------------------------------------------
    selectImage($control: JQuery, appimageid: string, datestamp: string) {
        $control.find('.thumb').attr('data-selected', 'false');
        if (appimageid !== null) {
            $control.find('.fullsizeimage')
                .attr('data-appimageid', appimageid)
                .css('background-image', `url(${applicationConfig.apiurl}api/v1/appimage/getimage?appimageid=${appimageid}&thumbnail=false)`);
            $control.find(`.thumb[data-appimageid="${appimageid}"]`).attr('data-selected', 'true');
            const $fullsizeimage = $control.find('.fullsizeimage');
            let currentThumnailNo = -1;
            const $thumbs = $control.find('.thumb');
            for (let i = 0; i < $thumbs.length; i++) {
                if ($thumbs.eq(i).attr('data-appimageid') === appimageid) {
                    currentThumnailNo = i;
                    break;
                }
            }
            if (currentThumnailNo != -1) {
                FwAppImage.updatePageInfo($control, currentThumnailNo + 1, $thumbs.length);
            } else {
                FwAppImage.updatePageInfo($control, '-', '-');
            }
        } else {
            $control.find('.fullsizeimage')
                .attr('data-appimageid', '')
                .css('background-image', `url(${FwAppImage.blankDataUrl})`);
            FwAppImage.updatePageInfo($control, '-', '-');
        }
        if (datestamp !== null) {
            $control.find('.datestamp')
                .text(datestamp);
        } else {
            $control.find('.datestamp')
                .text('');
        }
    };
    //---------------------------------------------------------------------------------
    getHtmlTag(data_type) {
        var html, properties, i;
        properties = this.getDesignerProperties(data_type);
        html = [];
        html.push('<div ');
        for (i = 0; i < properties.length; i++) {
            html.push(properties[i].attribute + '="' + properties[i].defaultvalue + '"');
        }
        html.push('>');
        html.push('</div>');
        html = html.join('');
        return html;
    };
    //---------------------------------------------------------------------------------
    getDesignerProperties(data_type) {
        var properties = [];
        //id
        let propId = {
            caption: 'ID'
            , datatype: 'string'
            , attribute: 'id'
            , defaultvalue: ''
            , visible: true
            , enabled: true
        };
        propId.defaultvalue = FwControl.generateControlId('image');
        //class
        let propClass = {
            caption: 'CSS Class'
            , datatype: 'string'
            , attribute: 'class'
            , defaultvalue: 'fwcontrol fwappimage'
            , visible: false
            , enabled: false
        };
        // data-control
        let propDataControl = {
            caption: 'Control'
            , datatype: 'string'
            , attribute: 'data-control'
            , defaultvalue: 'FwAppImage'
            , visible: true
            , enabled: false
        };
        // data-type
        let propDataType = {
            caption: 'Type'
            , datatype: 'string'
            , attribute: 'data-type'
            , defaultvalue: data_type
            , visible: false
            , enabled: false
        };
        // data-version
        let propDataVersion = {
            caption: 'Version'
            , datatype: 'string'
            , attribute: 'data-version'
            , defaultvalue: '1'
            , visible: false
            , enabled: false
        };
        // data-rendermode
        let propRenderMode = {
            caption: 'Render Mode'
            , datatype: 'string'
            , attribute: 'data-rendermode'
            , defaultvalue: 'template'
            , visible: false
            , enabled: false
        };

        properties.push(propId);
        properties.push(propClass);
        properties.push(propDataControl);
        properties.push(propDataType);
        properties.push(propDataVersion);
        properties.push(propRenderMode);

        return properties;
    };
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control) {
        $control.attr('data-rendermode', 'designer');
        let html = [];
        html.push('<div class="designer">');
        html.push(FwControl.generateDesignerHandle('Image', $control.attr('id')));
        html.push('<div class="image"></div>');
        html.push('<div class="toolbar"></div>');
        html.push('</div>');
        $control.html(html.join(''));
    };
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        let caption = '';
        $control.attr('data-rendermode', 'runtime');
        let html: string|string[] = [];
        html.push('<div class="runtime">');
        caption = $control.attr('data-caption');
        if (typeof caption === 'string' && caption.length > 0) {
            html.push('<div class="header">');
            html.push('  <div class="fwcontrol fwmenu default" data-control="FwMenu"></div>');
            html.push('  <div class="title">' + caption + '</div>');
            html.push('</div>');
        }
        html.push('  <div class="toolbar">');
        html.push('  </div>');
        html.push('  <div class="imageviewer">');
        html.push('    <div class="fullsizeimagepager">');
        html.push('    </div>');
        html.push('  </div>')
        html.push('  <div class="pageinfo">- of -</div>');
        html.push('  <div class="thumbnails" style="display:none"></div>');
        html.push('</div>');
        html = html.join('');
        $control.html(html);
        $control.find('.FwAppImage').data('$control', $control);
        var $fwmenu = $control.find('.fwmenu');
        FwControl.renderRuntimeHtml($fwmenu);
        FwControl.loadControls($fwmenu);
    }
    //---------------------------------------------------------------------------------
    getImageHtml($control, mode, image) {
        var html = [], url = FwAppImage.blankDataUrl, btnClearDisplay = 'none';
        if (mode === 'NEW') {
            image = {
                AppImageId: '',
                DateStamp: '',
                Description: '',
                Extension: '',
                MimeType: '',
                Width: 0,
                Height: 0,
                RecType: '',
                OrderBy: 0
            };
        } else {
            url = `${applicationConfig.apiurl}api/v1/appimage/getimage?appimageid=${image.AppImageId}`;
        }
        
        html.push(`    <div class="fullsizeimage" data-mimetype="${image.MimeType}" style="background-image:url(${url})" data-appimageid="${image.AppImageId}"></div>`);
        //html.push(`    <div class="datestamp">${image.DateStamp}</div>`);
        html.push('  </div>');
        let htmlString = html.join('');
        return htmlString;
    }
    //---------------------------------------------------------------------------------
    getAddImageHtml($control) {
        var html, imageHtml;
        imageHtml = FwAppImage.getImageHtml($control, 'NEW', null);
        html = [];
        html.push('<div class="addimage">');
        html.push(imageHtml);
        html.push('</div>');
        html = html.join('\n');
        return html;
    }
    //---------------------------------------------------------------------------------
    renderTemplateHtml($control) {
        var data_type, data_rendermode, html, $tabsChildren, $tabpagesChildren;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'template');
        html = [];
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    loadControl($control) {
        const $form = $control.closest('.fwform');
        let mode = 'EDIT';
        if ($form.length > 0) {
            mode = $form.attr('data-mode');
        }

        if (mode !== 'NEW') {
            let html: string | string[] = [];
            if ($control.attr('data-hasadd') !== 'false') {
                html.push('    <div class="button btnAdd" title="Add"><i class="material-icons">&#xE145;</i></div>'); //add
            }
            if ($control.attr('data-hasdelete') !== 'false') {
                html.push('        <div class="button btnDelete" title="Delete"><i class="material-icons">&#xE872;</i></div>');
            }
            if ($control.attr('data-hastogglethumbnails') !== 'false') {
                html.push('        <div class="button btnToggleThumbnails" title="Toggle Thumbnails"><i class="material-icons">&#xE3B6;</i></div>');
            }
            html.push('    <div class="button btnRefresh" title="Refresh"><i class="material-icons">&#xE5D5;</i></div>'); //refresh
            html = html.join('\n');
            const $toolbar = $control.find('.toolbar');
            $toolbar.html(html);
        }

        let viewerheight: string = '200px';
        if ($control.attr('data-viewerheight') !== undefined) {
            viewerheight = $control.attr('data-viewerheight');
        }
        const fullsizeImageHtml: string | string[] = [];
        if (mode !== 'NEW') {
            fullsizeImageHtml.push('      <div class="btnPrevImage" title="Previous Image"><i class="material-icons">chevron_left</i></div>');
        }
        fullsizeImageHtml.push(`      <div class="image" style="height:${viewerheight};"></div>`);
        if (mode !== 'NEW') {
            fullsizeImageHtml.push('      <div class="btnNextImage" title="Next Image"><i class="material-icons">chevron_right</i></div>');
        }
        const $fullsizeimagepager = $control.find('.fullsizeimagepager');
        $fullsizeimagepager.html(fullsizeImageHtml);


        if (($form.length === 0) || (mode !== 'NEW')) {
            FwAppImage.getAppImages($control);
        } else {

            FwAppImage.getAppImagesCallback($control, []);
        }
    }
    //---------------------------------------------------------------------------------
    pasteImage($control, element, event) {
        var items, blob, isWebkit, $image, $form;
        isWebkit = /webkit/.test(navigator.userAgent.toLowerCase());
        $form = $control.closest('.fwform');
        if (($form.length === 0) || ($form.attr('data-mode') !== 'NEW')) {
            $image = jQuery(FwAppImage.getAddImageHtml($control));
            if (isWebkit) {
                items = (event.clipboardData || event.originalEvent.clipboardData).items;
                for (var i = 0; i < items.length; i++) {
                    if (items[i].type.indexOf("image") === 0) {
                        blob = items[i].getAsFile();
                        break;
                    }
                }
                FwAppImage.fileToDataUrl($control, $image, blob);
            } else {
                window.setTimeout(function () {
                    var dataUrl, file, filename, mimetype, $fullsizeimage;
                    if ($control.find('.pasteimage > img').length > 0) {
                        dataUrl = $image.find('.pasteimage > img').attr('src');
                        file = dataUrl.toString().substring(dataUrl.indexOf(',') + 1, dataUrl.length);
                        mimetype = dataUrl.toString().substring(dataUrl.indexOf(':') + 1, dataUrl.indexOf(';'));
                        filename = 'attachment.' + dataUrl.toString().substring(dataUrl.indexOf('/') + 1, dataUrl.indexOf(';')).replace('jpeg', 'jpg');
                        $image.find('.pasteimage').html($image.find('.pasteimage').attr('data-placeholder'));
                        $fullsizeimage = $image.find('.fullsizeimage');
                        $fullsizeimage.attr('src', dataUrl);
                        $fullsizeimage.attr('data-mimetype', mimetype);
                        $fullsizeimage.attr('data-filename', filename);
                        $image.find('.btnClear').show();
                        $fullsizeimage = $image.find('.fullsizeimage');
                        let addImageAttempts = 0;
                        let timer = window.setInterval(function () {
                            try {
                                if ((($fullsizeimage.attr('src') !== '') || ($fullsizeimage.attr('src') !== FwAppImage.blankDataUrl))) {
                                    window.clearInterval(timer);
                                    FwAppImage.addImage($control, $image);
                                } else {
                                    if (addImageAttempts === 1) {
                                        if (confirm('Would you like to continue to wait for the browser to load this image?  Usually if you get this far, you are trying to upload an image that is unreasonably large to be storing in the database and we recommend compressing it first to avoid performance problems.  Would you like to continue waiting?')) {
                                            addImageAttempts = 0;
                                        } else {
                                            window.clearInterval(timer);
                                        }
                                    } else {
                                        addImageAttempts++;
                                    }
                                }
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        }, 100);
                    }
                }, 1);
            }
        } else if (($form.length === 1) && ($form.attr('data-mode') === 'NEW')) {
            throw 'You need to save the form before you can add images.';
        }
    };
    //---------------------------------------------------------------------------------
    dragoverImage($control, element, event) {
        var dataTransfer;
        event.stopPropagation();
        event.preventDefault();
        dataTransfer = (event.dataTransfer || event.originalEvent.dataTransfer);
        dataTransfer.dropEffect = 'copy';
    }
    //---------------------------------------------------------------------------------
    async dropImage($control: JQuery, event: any) {
        event.stopPropagation();
        event.preventDefault();
        const $form = $control.closest('.fwform');
        if (($form.length === 0) || ($form.attr('data-mode') !== 'NEW')) {
            let dataTransfer = (event.dataTransfer || event.originalEvent.dataTransfer);
            const files = dataTransfer.files;
            await this.addFiles($control, files);
        } else if (($form.length === 1) && ($form.attr('data-mode') === 'NEW')) {
            throw 'You need to save the form before you can add images.';
        }
    }
    //---------------------------------------------------------------------------------
    async addFiles($control, files: FileList): Promise<any> {
        return new Promise<any>(async (resolve, reject) => {
            for (var i = 0; i < files.length; i++) {
                if (files[i].type.indexOf("image") === 0) {
                    const file: File = files[i];
                    await this.addFile($control, file);
                }
            }
            FwAppImage.getAppImages($control);
            resolve();
        });
    }
    //---------------------------------------------------------------------------------
    async addFile($control, file: File): Promise<any> {
        return new Promise<any>(async (resolve, reject) => {
            if (file === null) throw new Error('Unable to load image. file is null. [FwAppImage.addFile]');
            if (!(file instanceof Blob)) throw new Error('Unable to load image. file is not an instance of Blob [FwAppImage.addFile]');
            let reader = new FileReader();
            reader.onload = async (event: ProgressEvent) => {
                var c;
                try {
                    const dataUrl = (<any>event.target).result;
                    //file = dataUrl.toString().substring(dataUrl.indexOf(',') + 1, dataUrl.length);
                    const filename = "attachment." + file.type.substring(file.type.indexOf('/') + 1, file.type.length).replace('jpeg', 'jpg');
                    
                    let request = new FwAjaxRequest<any>();
                    request.url = `${applicationConfig.apiurl}api/v1/appimage`;
                    request.httpMethod = 'POST';
                    request.$elementToBlock = $control;
                    request.data = {
                        Description: '',
                        Extension: file.type.toString().split('/')[1].toUpperCase().replace('JPEG', 'JPG'),
                        ImageDataUrl: dataUrl,
                        Rectype: '',
                        Filename: filename,
                        MimeType: file.type
                    }
                    FwAppImage.setGetAppImagesRequest($control, request.data);
                    let response = await FwAjax.callWebApi<any, any>(request);
                    resolve();
                } catch (ex) {
                    reject(ex);
                }
            };
            reader.readAsDataURL(file);
        });
    }
    //---------------------------------------------------------------------------------
    fileToDataUrl($control, $image, blob) {
        var result, dataUrl, file, filename, $fullsizeimage;
        result = '';
        if (blob === null) throw 'Unable to load image. blob is null. [FwAppImage.fileToDataUrl]';
        if (!(blob instanceof Blob)) throw 'Unable to load image. blob is not an instance of Blob [FwAppImage.fileToDataUrl]';
        let reader = new FileReader();
        reader.onload = function (event) {
            var c;
            try {
                dataUrl = (<any>event.target).result;
                file = dataUrl.toString().substring(dataUrl.indexOf(',') + 1, dataUrl.length);
                filename = "attachment." + blob.type.substring(blob.type.indexOf('/') + 1, blob.type.length).replace('jpeg', 'jpg');
                $image.find('.pasteimage').html('');
                $fullsizeimage = $image.find('.fullsizeimage');
                $fullsizeimage.attr('src', dataUrl);
                $fullsizeimage.attr('data-mimetype', blob.type);
                $fullsizeimage.attr('data-filename', filename);
                $image.find('.btnClear').show();
                $fullsizeimage = $image.find('.fullsizeimage');
                let addImageAttempts = 0;
                let timer: number = window.setInterval(function () {
                    try {
                        if ((($fullsizeimage.attr('src') !== '') || ($fullsizeimage.attr('src') !== FwAppImage.blankDataUrl))) {
                            window.clearInterval(timer);
                            FwAppImage.addImage($control, $image);
                        } else {
                            if (addImageAttempts === 1) {
                                if (confirm('Would you like to continue to wait for the browser to load this image?  Usually if you get this far, you are trying to upload an image that is unreasonably large to be storing in the database and we recommend compressing it first to avoid performance problems.  Would you like to continue waiting?')) {
                                    addImageAttempts = 0;
                                } else {
                                    window.clearInterval(timer);
                                }
                            } else {
                                addImageAttempts++;
                            }
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, 100);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
        reader.readAsDataURL(blob);
        return result;
    }
    //---------------------------------------------------------------------------------
    getAppImages($control) {
        var request: any = {};
        FwAppImage.setGetAppImagesRequest($control, request);
        let url: string = `api/v1/appimage/getimages?`;
        let first: boolean = true;
        for (let key in request) {
            if (!first) {
                url += '&';
            } else {
                first = false;
            }
            url += `${key}=${request[key]}`;
        }
        FwAppData.apiMethod(true, 'GET', url, request, applicationConfig.ajaxTimeoutSeconds,
            (response: any) => {
                try {
                    FwAppImage.getAppImagesCallback($control, response);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            (error: any) => {
                FwFunc.showError(error);
            }, $control
        );
    }
    //---------------------------------------------------------------------------------
    setGetAppImagesRequest($control, request) {
        var uniqueid1, uniqueid2, uniqueid3, $form, $uniqueid1field, $uniqueid2field, $uniqueid3field, url, description = '', rectype = '';
        $form = $control.closest('.fwform');

        if ($control.is('[data-uniqueid1]')) {
            uniqueid1 = $control.attr('data-uniqueid1');
        } else {
            $uniqueid1field = $form.find('div[data-datafield="' + $control.attr('data-uniqueid1field') + '"]');
            if ($uniqueid1field.length === 0) throw 'FwAppImage.loadControl: Unable to find data-uniqueid1field: ' + $control.attr('data-uniqueid1field');
            uniqueid1 = FwFormField.getValue2($uniqueid1field);
        }
        request.uniqueid1 = uniqueid1;

        if ($control.is('[data-uniqueid2]')) {
            uniqueid2 = $control.attr('data-uniqueid2');
        } else {
            $uniqueid2field = $form.find('div[data-datafield="' + $control.attr('data-uniqueid2field') + '"]');
            uniqueid2 = ($uniqueid2field.length > 0) ? FwFormField.getValue2($uniqueid2field) : '';
        }
        request.uniqueid2 = uniqueid2;

        if ($control.is('[data-uniqueid3]')) {
            uniqueid3 = $control.attr('data-uniqueid3');
        } else {
            $uniqueid3field = $form.find('div[data-datafield="' + $control.attr('data-uniqueid3field') + '"]');
            uniqueid3 = ($uniqueid3field.length > 0) ? FwFormField.getValue2($uniqueid3field) : '';
        }
        request.uniqueid3 = uniqueid3;

        if ($control.is('[data-description]')) {
            description = $control.attr('data-description');
            request.description = description;
        }
        if ($control.is('[data-rectype]')) {
            rectype = $control.attr('data-rectype');
            request.rectype = rectype;
        }
    }
    //---------------------------------------------------------------------------------
    updatePageInfo($control: JQuery, imageno: string|number, imagecount: string|number) {
        const $fwform = $control.closest('.fwform');
        let mode = 'EDIT';
        if ($fwform.length > 0) {
            mode = $fwform.attr('data-mode');
        }
        if (mode === 'NEW') {
            $control.find('.pageinfo').html(`Record must be saved before images can be added.`);
        }
        else if (imageno === '-') {
            $control.find('.pageinfo').html(`No images to display`);
        } else {
            $control.find('.pageinfo').html(`${imageno} of ${imagecount}`);
        }
    }
    //---------------------------------------------------------------------------------
    getAppImagesCallback($control, images) {
        try {
            //console.log(images);
            let imageno: string = '-';
            if (images.length > 0) {
                imageno = '1';
            }
            FwAppImage.updatePageInfo($control, imageno, images.length);

            // load the thumbnails
            let thumbnails: string | string[] = [];
            for (var imageIndex = 0; imageIndex < images.length; imageIndex++) {
                const image = images[imageIndex];
                let thumnailurl = `${applicationConfig.apiurl}api/v1/appimage/getimage?appimageid=${image.AppImageId}&thumbnail=true`;
                thumbnails.push(`<label for="image${imageIndex + 1}" data-appimageid="${image.AppImageId}" data-datestamp="${image.DateStamp}" class="thumb image${imageIndex + 1}" style="background-image:url(${thumnailurl})"></label>`);
            }
            thumbnails = thumbnails.join('');
            const $thumbnails = $control.find('.thumbnails');
            $thumbnails.html(thumbnails);
            
            const showThumbnails = $control.attr('data-showthumbnails') === 'true';
            if (showThumbnails) {
                $thumbnails.toggle(showThumbnails);
            }
            if (typeof $control.data('recenterpopup') === 'function') {
                $control.data('recenterpopup')();
            }

            Sortable.create($thumbnails.get(0), {
                onEnd: function (evt) {
                    //let imageToDisplay;
                    const $item = jQuery(evt.item);
                    const $fullsizeimage = $control.find('.fullsizeimage');
                    const appimageid = $fullsizeimage.attr('data-appimageid');
                    const $thumbs = $thumbnails.find('.thumb');
                    let currentThumnailNo = -1;
                    for (let i = 0; i < $thumbs.length; i++) {
                        const $thumb = $thumbs.eq(i);
                        if ($thumbs.eq(i).attr('data-appimageid') === appimageid) {
                            currentThumnailNo = i;
                        }
                        const request: any = {};
                        request.AppImageId = $thumb.attr('data-appimageid');
                        request.OrderBy = i + 1;
                        FwAppData.apiMethod(true, 'POST', `api/v1/appimage/repositionimage`, request, applicationConfig.ajaxTimeoutSeconds,
                            (response: any) => {
                                
                            },
                            (error: any) => {
                                FwFunc.showError(error);
                            }, $control
                        );
                    }
                    FwAppImage.updatePageInfo($control, currentThumnailNo + 1, $thumbs.length);
                }
            });

            // load the medium size image
            const $image = $control.find('.image');
            if (images.length > 0) {
                const html = FwAppImage.getImageHtml($control, 'VIEW', images[0]);
                $image
                    .attr('data-appimageid', images[0].AppImageId)
                    .html(html);
                FwAppImage.selectImage($control, images[0].AppImageId, images[0].DateStamp);
            } else {
                const html = FwAppImage.getImageHtml($control, 'NEW', null);
                $image
                    .attr('data-appimageid', '')
                    .html(html);
                FwAppImage.selectImage($control, null, null);
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //---------------------------------------------------------------------------------
    addImage($control, $image) {
        var request, $fullsizeimage, imagedataurl = '', filename = '', mimetype = '', extension = '', description, rectype;
        $fullsizeimage = $image.find('.fullsizeimage');
        if (($fullsizeimage.attr('src') !== '') && ($fullsizeimage.attr('src') !== FwAppImage.blankDataUrl)) {
            imagedataurl = $fullsizeimage.attr('src');
            filename = $fullsizeimage.attr('data-filename');
            mimetype = $fullsizeimage.attr('data-mimetype');
            extension = mimetype.toString().split('/')[1].toUpperCase().replace('JPEG', 'JPG');
        }
        if ((($fullsizeimage.attr('src') === '') || ($fullsizeimage.attr('src') === FwAppImage.blankDataUrl))) {
            throw 'Image is required.';
        }
        description = $control.is('[data-description]') ? $control.attr('data-description') : '';
        rectype = $control.is('[data-rectype]') ? $control.attr('data-rectype') : '';
        request = {
            Description: description,
            Extension: extension,
            ImageDataUrl: imagedataurl,
            Rectype: rectype,
            Filename: filename,
            MimeType: mimetype
        };
        FwAppImage.setGetAppImagesRequest($control, request);
        FwAppData.apiMethod(true, 'POST', `api/v1/appimage`, request, applicationConfig.ajaxTimeoutSeconds,
            (response: any) => {
                try {
                    FwAppImage.getAppImages($control);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            (error: any) => {
                FwFunc.showError(error);
            }, $control
        );
    }
    //---------------------------------------------------------------------------------
    deleteImage($control, $image) {
        var request, appimageid;
        appimageid = $image.attr('data-appimageid');
        request = {
            AppImageId: appimageid
        };
        FwAppImage.setGetAppImagesRequest($control, request);
        FwAppData.apiMethod(true, 'DELETE', `api/v1/appimage`, request, applicationConfig.ajaxTimeoutSeconds,
            (response: any) => {
                try {
                    if (typeof $control.data('recenterpopup') === 'function') {
                        $control.data('recenterpopup')();
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            (error: any) => {
                FwFunc.showError(error);
            }, $control
        );
    }
    //---------------------------------------------------------------------------------
}

var FwAppImage = new FwAppImageClass();