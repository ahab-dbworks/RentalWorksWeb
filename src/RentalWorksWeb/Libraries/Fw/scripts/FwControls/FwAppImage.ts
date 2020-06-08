class FwAppImageClass {
    blankDataUrl = 'data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==';
    //---------------------------------------------------------------------------------
    init = function ($control: JQuery) {
        $control
            .on('click', '.btnAdd', function (event) {
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
                        html.push('  <input class="inputfile" type="file" style="opacity:0;width:0;height:0;">');
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
                        .on('change', '.inputfile', function () {
                            var blob;
                            try {
                                for (var i = 0; i < this.files.length; i++) {
                                    if (this.files[i].type.indexOf("fullsizeimage") === 0) {
                                        blob = this.files[i];
                                        break;
                                    }
                                }
                                var $image = jQuery(FwAppImage.getAddImageHtml($control));
                                FwAppImage.fileToDataUrl($control, $image, blob);
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
                                    FwAppImage.dropImage($control, $pasteimage.get(0), event);
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
                    const $fullsizeimage = $control.find('.fullsizeimage');
                    const appimageid = $fullsizeimage.attr('data-appimageid');
                    const $thumbnail = $control.find(`.thumb[data-appimageid="${appimageid}"]`);
                    const $confirmation = FwConfirmation.renderConfirmation('Confirm', 'Delete Image?');
                    const $btnOk = FwConfirmation.addButton($confirmation, 'OK');
                    FwConfirmation.addButton($confirmation, 'Cancel');
                    $btnOk.on('click', function () {
                        FwAppImage.deleteImage($control, $fullsizeimage);
                        let prevImageNo = -1;
                        const $thumbs = $thumbnail.closest('.thumbnails').find('.thumb');
                        for (let i = 0; i < $thumbs.length; i++) {
                            if ($thumbs.eq(i).attr('data-appimageid') === appimageid) {
                                if (i > 0) {
                                    prevImageNo = i - 1;
                                } else {
                                    prevImageNo = 0;
                                }
                                break;
                            }
                        }
                        if (prevImageNo > 0) {
                            let datestamp = $thumbs.eq(prevImageNo).attr('data-datestamp');
                            FwAppImage.selectImage($control, appimageid, datestamp);
                        } else {
                            FwAppImage.selectImage($control, null, null);
                        }
                        $thumbnail.remove();
                        
                    });
                    return false;
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.fullsizeimage', function (event) {
                try {
                    var appimageid = jQuery(this).attr('data-appimageid');
                    var html = [];
                    html.push('<img style="max-width:100%;" src="' + applicationConfig.apiurl + 'api/v1/appimage/getimage?appimageid=' + appimageid + '&thumbnail=false' + '\" >');
                    let htmlString = html.join('\n');
                    var $confirmation = FwConfirmation.renderConfirmation('Image Viewer', htmlString);
                    $confirmation.find('.message').css({
                        'text-align': 'center'
                    })
                    var $btnClose = FwConfirmation.addButton($confirmation, 'Close', true);
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
            ;
    };
    //---------------------------------------------------------------------------------
    selectImage($control: JQuery, appimageid: string, datestamp: string) {
        if (appimageid !== null) {
            $control.find('.fullsizeimage')
                .attr('data-appimageid', appimageid)
                .css('background-image', `url(${applicationConfig.apiurl}api/v1/appimage/getimage?appimageid=${appimageid}&thumbnail=false)`);
        } else {
            $control.find('.fullsizeimage')
                .attr('data-appimageid', '')
                .css('background-image', `url(${FwAppImage.blankDataUrl})`);
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
        var data_type, data_rendermode, html;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'designer');
        html = [];
        html.push('<div class="designer">');
        html.push(FwControl.generateDesignerHandle('Image', $control.attr('id')));
        html.push('<div class="image"></div>');
        html.push('<div class="toolbar"></div>');
        html.push('</div>');
        $control.html(html.join(''));
    };
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var data_type, data_rendermode, html, caption = '';
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'runtime');
        html = [];
        html.push('<div class="runtime">');
        html.push('<div class="header">');
        if ($control.is('[data-caption]')) {
            caption = $control.attr('data-caption');
            html.push('  <div class="fwcontrol fwmenu default" data-control="FwMenu"></div>');
            html.push('  <div class="title">' + caption + '</div>');
            html.push('</div>');
        }
        html.push('  <div class="toolbar">');
        if ($control.attr('data-hasadd') !== 'false') {
            html.push('    <div class="button btnAdd"><i class="material-icons">&#xE145;</i></div>'); //add
        }
        if ($control.attr('data-hasdelete') !== 'false') {
            html.push('        <div class="button btnDelete" title="Delete"><i class="material-icons">&#xE872;</i></div>');
        }
        html.push('    <div class="button btnRefresh"><i class="material-icons">&#xE5D5;</i></div>'); //refresh
        html.push('  </div>');
        html.push('  <div class="imageviewer">');
        html.push('    <div class="images"></div>');
        html.push('  </div>')
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
        let viewerwidth: string = '200px';
        let viewerheight: string = '200px';
        if ($control.attr('data-viewerwidth') !== undefined) {
            viewerwidth = $control.attr('data-viewerwidth');
        }
        if ($control.attr('data-viewerheight') !== undefined) {
            viewerheight = $control.attr('data-viewerheight');
        }
        html.push(`  <div class="image" data-mode="${mode}" data-appimageid="${image.AppImageId}">`);
        html.push('    <div class="imagecontrol">');
        html.push(`      <div class="imagecontainer" style="width:${viewerwidth};height:${viewerheight};">`);
        html.push(`        <div class="fullsizeimage" data-mimetype="${image.MimeType}" style="background-image:url(${url})" data-appimageid="${image.AppImageId}">`);
        html.push('        </div>');
       // html.push('        <img class="image" data-mimetype="' + image.MimeType + '" src="' + url + '" data-appimageid="' + image.AppImageId + '" />');
        html.push('      </div>');
        html.push('    </div>');
        html.push('    <div class="datestamp">');
        html.push('      ' + image.DateStamp);
        html.push('    </div>');
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
        var $form, response;
        $form = $control.closest('.fwform');
        if (($form.length === 0) || ($form.attr('data-mode') !== 'NEW')) {
            FwAppImage.getAppImages($control);
        } else {
            response = {
                images: []
            };
            FwAppImage.getAppImagesCallback($control, response);
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
    dropImage($control, element, event) {
        var files, blob, reader, $image, $form, $pastedimage, timer, addImageAttempts;
        event.stopPropagation();
        event.preventDefault();
        $form = $control.closest('.fwform');
        if (($form.length === 0) || ($form.attr('data-mode') !== 'NEW')) {
            $image = jQuery(FwAppImage.getAddImageHtml($control));
            let dataTransfer = (event.dataTransfer || event.originalEvent.dataTransfer);
            files = dataTransfer.files;
            for (var i = 0; i < files.length; i++) {
                if (files[i].type.indexOf("image") === 0) {
                    blob = files[i];
                    FwAppImage.fileToDataUrl($control, $image, blob);
                    break;
                }
            }
        } else if (($form.length === 1) && ($form.attr('data-mode') === 'NEW')) {
            throw 'You need to save the form before you can add images.';
        }
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
    getAppImagesCallback($control, images) {
        var $divimages, image, $image, $addimage, html, thumbnails;
        try {
            $divimages = $control.find('div.images');
            $divimages.empty();
            html = [];
            thumbnails = [];

            //html.push('<section class="gallery"><div class="carousel">');

            //for (var i = 0; i < images.length; i++) {
            //    if (i === 0) {
            //        html.push('<input type="radio" id="image1" name="gallery-control" checked>');
            //    } else {
            //        html.push('<input type="radio" id="image' + (i + 1) + '" name="gallery-control">');
            //    }
            //}

            //html.push('<input type="checkbox" id="fullscreen" name="gallery-fullscreen-control"/><div class="wrap">');

            //for (var imageno = 0; imageno < images.length; imageno++) {
            //    image = images[imageno];
            //    $image = FwAppImage.getImageHtml($control, 'VIEW', image);
            //    html.push('<figure class="image' + (imageno + 1) + '">');
            //    html.push($image);
            //    html.push('</figure>');
            //    thumbnails.push('<label for="image' + (imageno + 1) + '" data-appimageid="' + image.AppImageId + '" class="thumb image' + (imageno + 1) + '" style="background-image:url(' + jQuery($image).find('img').attr('src') + '&thumbnail=true)"></label>');
            //}
            html.push('<div class="fullsizeimagepager">');
            html.push('    <div class="pageleftcontainer"><i class="material-icons">chevron_left</i></div>');
            if (images.length > 0) {
                html.push(FwAppImage.getImageHtml($control, 'VIEW', images[0]));
            } else {
                html.push(FwAppImage.getImageHtml($control, 'New', null));
                FwAppImage.selectImage($control, null, null);
            }
            html.push('    <div class="pagerightcontainer"><i class="material-icons">chevron_right</i></div>');
            html.push('</div>');
            html.push('<div class="thumbnails">');
            for (var imageno = 0; imageno < images.length; imageno++) {
                image = images[imageno];
                let thumnailurl = `${applicationConfig.apiurl}api/v1/appimage/getimage?appimageid=${image.AppImageId}&thumbnail=true`;
                html.push(`<label for="image${imageno + 1}" data-appimageid="${image.AppImageId}" data-datestamp="${image.DateStamp}" class="thumb image${imageno + 1}" style="background-image:url(${thumnailurl})"></label>`);
            }
            //html.push(thumbnails.join(''));
            html.push('</div>');
            //html.push('</div></section > ');
            html = html.join('');
            $divimages.append(html);
            if (typeof $control.data('recenterpopup') === 'function') {
                $control.data('recenterpopup')();
            }

            //const $carousel = $control.find('.carousel');
            //Sortable.create($divimages.find('.thumbnails').get(0), {
            //    onEnd: function (evt) {
            //        let imageToDisplay;
            //        const $item = jQuery(evt.item);
            //        const $thumbnails = $item.parents('.thumbnails').children();
            //        for (let i = 0; i < $thumbnails.length; i++) {
            //            const $thumb = jQuery($thumbnails[i]);
            //            if (i === 0) {
            //                imageToDisplay = $thumb.attr('for');
            //            }
            //            const request: any = {};
            //            request.AppImageId = $thumb.attr('data-appimageid');
            //            request.OrderBy = i + 1;
            //            FwAppData.apiMethod(true, 'POST', `api/v1/appimage/repositionimage`, request, applicationConfig.ajaxTimeoutSeconds,
            //                (response: any) => {
            //                },
            //                (error: any) => {
            //                    FwFunc.showError(error);
            //                }, $control
            //            );
            //        }
            //        $carousel.find('input:checked').attr('checked', false);
            //        $carousel.find(`#${imageToDisplay}`).attr('checked', true);
            //    }
            //});
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
            //imagedataurl = imagedataurl.substring(imagedataurl.indexOf(',') + 1, imagedataurl.length);
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
                    //$image.remove();
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