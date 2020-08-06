var ReceiveOnSet = {};
//----------------------------------------------------------------------------------------------
ReceiveOnSet.getModuleScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $fwcontrols, $canvas, canvas, $findpo, $findset, $newset, $scan, $signature, $itemupdate;
    combinedViewModel = jQuery.extend({
        captionPageTitle:         RwLanguages.translate('Receive On Set'),
        captionPONo:              RwLanguages.translate('PO No.'),
        captionBtnBack:           RwLanguages.translate('Back'),
        captionBtnContinue:       RwLanguages.translate('Continue...'),
        captionVendor:            RwLanguages.translate('Vendor'),
        captionPrimaryVendor:     RwLanguages.translate('Buyer'),
        captionManufacturer:      RwLanguages.translate('Manufacturer'),
        captionProduction:        RwLanguages.translate('Production'),
        captionPO:                RwLanguages.translate('PO'),
        captionSetNo:             RwLanguages.translate('Set No.'),
        captionBtnCreateNew:      RwLanguages.translate('Create New Set'),
        captionOrder:             RwLanguages.translate('Order'),
        captionDeal:              RwLanguages.translate('Deal'),
        captionICode:             RwLanguages.translate('I-Code'),
        captionBtnCreateContract: RwLanguages.translate('Create Contract'),
        captionClearSignature:    RwLanguages.translate('Clear Signature'),
        captionCreateContract:    RwLanguages.translate('Create Contract')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-receiveonset').html(), combinedViewModel);

    screen            = {};
    screen.$view      = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.properties = properties;

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.init($fwcontrols);
    FwControl.renderRuntimeHtml($fwcontrols);

    $canvas = screen.$view.find('#rfv-canvasSignature');
    canvas = $canvas[0];
    canvas.onselectstart = function () { return false; }

    $findpo     = screen.$view.find('.rfv-findpo');
    $findset    = screen.$view.find('.rfv-findset');
    $newset     = screen.$view.find('.rfv-newset');
    $scan       = screen.$view.find('.rfv-scan');
    $itemupdate = screen.$view.find('.rfv-itemupdate');
    $signature  = screen.$view.find('.rfv-signaturecapture');

    $findpo.find('#posearch').fwmobilesearch({
        service:   'ReceiveOnSet',
        method:    'POSearch',
        upperCase: true,
        searchModes: [
            { value: 'PONO',        caption: 'PO No.' },
            { value: 'DEPARTMENT',  caption: 'Department' },
            { value: 'VENDOR',      caption: 'Vendor' },
            { value: 'DESCRIPTION', caption: 'Description' }
        ],
        itemTemplate: function() {
            var html = [];

            html.push('<div class="record">');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed bold">Department:</div><div class="value">{{department}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed bold">Production:</div><div class="value">{{deal}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Status:</div><div class="value">{{status}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Acquire:</div><div class="value">{{acquire}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">PO No:</div><div class="value">{{orderno}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Description:</div><div class="value">{{orderdesc}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Set No:</div><div class="value">{{setno}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Vendor:</div><div class="value">{{vendor}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Primary Vendor:</div><div class="value">{{buyer}}</div>');
            html.push('  </div>');
            html.push('</div>');
            html = html.join('');

            return html;
        },
        recordClick: function(recorddata) {
            var request;
            screen.properties.selectedpo = recorddata;
            screen.properties.showall    = ((recorddata.dealid == '') ? true : false)
            request = {
                orderid: screen.properties.selectedpo.orderid
            };
            RwServices.callMethod("ReceiveOnSet", "GetPOReceiveContractID", request, function(response) {
                screen.properties.receivecontractid = response.outreceivecontractid;
                screen.properties.podealid          = recorddata.dealid;
                screen.properties.departmentid      = recorddata.departmentid;
                $findpo.hide();
                if (recorddata.setno !== '') {
                    $findset.find('#setsearch input').val(recorddata.setno);
                }
                $findset.showscreen();
            });
        }
    });
    $findpo.showscreen = function() {
        $findpo.show();
        program.setScanTarget('.rfv-findpo #posearch input');
        $findpo.find('#posearch').fwmobilesearch('search');
    };

    $findset.find('#setsearch').fwmobilesearch({ 
        service:   'ReceiveOnSet',
        method:    'OrderSearch',
        upperCase: true,
        getRequest: function() {
            var request = {
                dealid:       screen.properties.podealid,
                departmentid: screen.properties.departmentid,
                showall:      screen.properties.showall
            };
            return request;
        },
        searchModes: [
            { value: 'SETNO',        caption: 'Set No.' },
            { value: 'SETCHARACTER', caption: 'Set Character' },
            { value: 'STATUS',       caption: 'Status' }
        ],
        itemTemplate: function() {
            var html = [];

            html.push('<div class="record">');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed bold">Department:</div><div class="value">{{department}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed bold">Set Character:</div><div class="value">{{orderdesc}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">' + RwLanguages.translate('Deal') + ':</div><div class="value">{{deal}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Status:</div><div class="value">{{status}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Set No:</div><div class="value">{{setno}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Start Date:</div><div class="value">{{estrentfrom}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">End Date:</div><div class="value">{{estrentto}}</div>');
            html.push('  </div>');
            html.push('</div>');
            html = html.join('');

            return html;
        },
        recordClick: function(recorddata) {
            screen.properties.selectedorder = recorddata;
            $findset.hide();
            $scan.showscreen();

            $findset.$back.hide();
            $findset.$newset.hide();
        }
    });
    $findset.$back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', false, function() { //back
        $findset.hide();
        $findpo.showscreen();

        $findset.$back.hide();
        $findset.$newset.hide();
    });
    $findset.$newset = FwMobileMasterController.addFormControl(screen, 'New Set', 'right', '&#xE145;', false, function() { //new
        $findset.hide();
        $newset.showscreen();

        $findset.$back.hide();
        $findset.$newset.hide();
    });
    $findset.showscreen = function() {
        $findset.show();
        $findset.$back.show();
        $findset.$newset.show();
        program.setScanTarget('.rfv-findset #setsearch input');
        $findset.find('#setsearch').fwmobilesearch('search');
    };

    $newset
        .on('change', 'div[data-required="true"].error', function() {
            var $this, value;
            $this = jQuery(this);
            value = FwFormField.getValue2($this);
            if (value !== '') {
                $this.removeClass('error');
            }
        })
        .on('change', 'div[data-datafield="production"] .fwformfield-value', function() {
            if (this.value === '') {
                $newset.find('div[data-datafield="setno"] .fwformfield-value').val('');
                $newset.find('div[data-datafield="setno"] .fwformfield-text').val('');
                FwFormField.toggle($newset.find('div[data-datafield="setno"]'), false);
            } else {
                var request;
                FwFormField.toggle($newset.find('div[data-datafield="setno"]'), true);
                request = {
                    production: $newset.find('div[data-datafield="production"] .fwformfield-value').val()
                }
                RwServices.callMethod("ReceiveOnSet", "LoadSets", request, function(response) {
                    FwFormField.loadItems($newset.find('div[data-datafield="setno"]'), response.sets);
                });
            }
        })
    ;
    $newset.$back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', false, function() { //back
        $newset.hide();
        $findset.showscreen();

        $newset.$back.hide();
        $newset.$save.hide();
    });
    $newset.$save = FwMobileMasterController.addFormControl(screen, 'Save', 'right', '&#xE161;', false, function() { //save
        if ($newset.validatefields()) {
            var request;
            request = {
                poid:         screen.properties.selectedpo.orderid,
                production:   $newset.find('div[data-datafield="production"] .fwformfield-value').val(),
                setno:        $newset.find('div[data-datafield="setno"] .fwformfield-value').val(),
                description:  $newset.find('div[data-datafield="setcharacter"] .fwformfield-value').val(),
                eststartdate: $newset.find('div[data-datafield="eststartdate"] .fwformfield-value').val(),
                estenddate:   $newset.find('div[data-datafield="estenddate"] .fwformfield-value').val()
            };
            RwServices.callMethod("ReceiveOnSet", "NewOrder", request, function(response) {
                if ((typeof response.record.errno !== 'undefined') && (response.record.errno !== 0)) {
                    FwFunc.showError(response.record.errmsg);
                } else {
                    screen.properties.selectedorder = response.record[0];
                    $newset.hide();
                    $newset.$back.hide();
                    $newset.$save.hide();
                    $scan.showscreen();
                }
            });
        }
    });
    $newset.showscreen = function() {
        $newset.empty().html(Mustache.render(jQuery('#tmpl-receiveonsetnewsettemplate').html())).show();
        FwControl.renderRuntimeControls($newset.find('.fwcontrol'));
        $newset.$back.show();
        $newset.$save.show();

        if ($newset.find('div[data-datafield="production"] .fwformfield-value').is(':empty')) {
            RwServices.callMethod("ReceiveOnSet", "LoadProductions", {}, function(response) {
                FwFormField.loadItems($newset.find('div[data-datafield="production"]'), response.productions);
            });
        }
    };
    $newset.validatefields = function() {
        var $fields, isvalid = true;
        $fields = $newset.find('.fwformfield');
        $fields.each(function(index) {
            var $field = jQuery(this);

            if (($field.attr('data-required') === 'true') && ($field.attr('data-enabled') === 'true')) {
                if ($field.find('.fwformfield-value').val() === '') {
                    isvalid = false;
                    $field.addClass('error');
                }
            }
            if ($field.hasClass('error')) {
                isvalid = false;
            }
            if (isvalid) {
                $field.removeClass('error');
            }
        })

        return isvalid;
    };

    $scan
        .on('change', '.fwmobilecontrol-value', function() {
            var $this, $records;
            $this = jQuery(this);
            $records = $scan.find('.items .record');
            $records.each(function(index, element) {
                var $record;
                $record = jQuery(element);
                if ($record.data('recorddata').masterno === $this.val()) {
                    $record.click();
                    return false;
                }
            });
            $this.val('');
        })
        .on('click', '.record', function() {
            var $this, $confirmation, $cancel, $submit, html = [];
            $this = jQuery(this);
            if ($this.data('recorddata').qtyremaining > 0) {
                $scan.hide();
                $scan.$back.hide();
                $scan.$createcontract.hide();
                FwMobileMasterController.tabcontrols.clearcontrols();

                $itemupdate.showscreen($this.data('recorddata'));
            }
        })
    ;
    $scan.$back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', false, function() { //back
        $scan.hide();
        $findset.showscreen();

        $scan.$back.hide();
        //$scan.data('mode', '');
        $scan.$createcontract.hide();
        FwMobileMasterController.tabcontrols.clearcontrols();
    });
    $scan.$createcontract = FwMobileMasterController.addFormControl(screen, 'Create Contract', 'right', '&#xE161;', false, function() { //submit
        $scan.hide();
        $scan.$back.hide();
        $scan.$createcontract.hide();
        FwMobileMasterController.tabcontrols.clearcontrols();

        $signature.showscreen();
    });
    $scan.showscreen = function() {
        $scan.show();
        program.setScanTarget('.rfv-scan .fwmobilecontrol-value');
        $scan.$back.show();
        //if ((typeof $scan.data('mode') == 'undefined') || ($scan.data('mode') == '')) {
        //    $scan.data('mode', 'REMAINING');
        //}
        $scan.loaditems();
        program.setScanTarget('.rfv-scan .fwmobilecontrol-value');

        //$tabremaining = FwMobileMasterController.tabcontrols.addtab('Remaining', ($scan.data('mode') == 'REMAINING'));
        //$tabremaining.on('click', function() {
        //    $scan.data('mode', 'REMAINING');
        //    $scan.loaditems();
        //});
        //$taball = FwMobileMasterController.tabcontrols.addtab('All', ($scan.data('mode') == 'ALL'));
        //$taball.on('click', function() {
        //    $scan.data('mode', 'ALL');
        //    $scan.loaditems();
        //});
    };
    $scan.loaditems = function() {
        var request;
        $scan.find('.items').empty();
        request = {
            //mode:              $scan.data('mode'),
            orderid:           screen.properties.selectedpo.orderid,
            receivecontractid: ((typeof screen.properties.receivecontractid !== 'undefined') ? screen.properties.receivecontractid : '')
        };
        RwServices.callMethod("ReceiveOnSet", "LoadItems", request, function(response) {
            var html = [], $item;

            html.push('<div class="record">');
            html.push('  <div class="row">');
            html.push('    <div class="value master" style="color: #00aaff;text-decoration: underline;"></div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">I-Code:</div><div class="value masterno"></div>');
            html.push('    <div class="caption fixed">Remaining:</div><div class="value remaining"></div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Tracked By:</div><div class="value trackedby"></div>');
            html.push('    <div class="caption fixed">Session:</div><div class="value session"></div>');
            html.push('  </div>');
            html.push('</div>');
            html = html.join('');

            for (var i = 0; i < response.items.length; i++) {
                $item = jQuery(html);
                $item.find('.master').html(response.items[i].master);
                $item.find('.masterno').html(response.items[i].masterno);
                $item.find('.remaining').html(response.items[i].qtyremaining);
                $item.find('.trackedby').html(response.items[i].trackedby);
                $item.find('.session').html(response.items[i].qtysession);
                $item.data('recorddata', response.items[i]);

                $scan.find('.items').append($item).show();
                if (response.items[i].qtysession > 0) {
                    $scan.$createcontract.show();
                }
            }

            if (response.items.length === 0) {
                $scan.find('.items').append('<div class="norecords" style="text-align:center;">0 records found.</div>').show();
            }
        });
    };
    $scan.getpicturepopup = function(recorddata, barcode) {
        var $confirmation, $cancel, $takepicture, html = [];
        $confirmation = FwConfirmation.renderConfirmation('Take a picture?', '');
        $takepicture  = FwConfirmation.addButton($confirmation, 'Take Picture', false);
        $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);
        $confirmation.find('.body').css('color', '#404040');
                
        if (recorddata.trackedby === 'BARCODE') {
            html.push('<div>• Toggle between product and asset to determine if the picture is for the universal item or the assigned bar code asset.</div>');
            html.push('<div class="tabselector" style="margin-top:15px;">');
            html.push('  <div class="tab active" data-tab="PRODUCT">Product</div>');
            html.push('  <div class="tab" data-tab="ASSET">Asset</div>');
            html.push('</div>');
            FwConfirmation.addControls($confirmation, html.join(''));

            $confirmation.on('click', '.tabselector .tab:not(.active)', function() {
                var $this;
                $this = jQuery(this);
                $this.addClass('active');
                $this.siblings().removeClass('active');
            });
        }

        $takepicture.on('click', function() {
            try {
                if (typeof navigator.camera === 'undefined' || !program.hasCamera) {
                    throw 'Camera is not supported in the current environment.';
                }
                navigator.camera.getPicture(
                    function(imageData) { //success
                        var request;
                        try {
                            if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                window.screen.lockOrientation('portrait-primary');
                            }
                            if ((recorddata.trackedby === 'BARCODE') && (barcode !== '') && ($confirmation.find('.tab[data-tab="ASSET"]').hasClass('active'))) {
                                request = {
                                    poid:         screen.properties.selectedpo.orderid,
                                    masteritemid: recorddata.masteritemid,
                                    barcode:      barcode,
                                    images:       [imageData]
                                };
                                RwServices.callMethod("ReceiveOnSet", "POReceiveImage", request, function(response) {
                                    try {
                                        program.playStatus(true);
                                        FwConfirmation.destroyConfirmation($confirmation);
                                    } catch(ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            } else {
                                request = {
                                    uniqueid1: recorddata.masterid,
                                    images:    [imageData]
                                };
                                RwServices.inventory.addInventoryWebImage(request, function(response) {
                                    try {
                                        program.playStatus(true);
                                        FwConfirmation.destroyConfirmation($confirmation);
                                    } catch(ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    },
                    function(message) { //error
                        try {
                            if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                                window.screen.lockOrientation('portrait-primary');
                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    },
                    {
                        destinationType:    Camera.DestinationType.DATA_URL,
                        sourceType:         Camera.PictureSourceType.CAMERA,
                        allowEdit :         false,
                        correctOrientation: true,
                        encodingType:       Camera.EncodingType.JPEG,
                        quality:            applicationConfig.photoQuality,
                        targetWidth:        applicationConfig.photoWidth,
                        targetHeight:       applicationConfig.photoHeight 
                    }
                );
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
    };

    $itemupdate
        .on('click', '.asl-add', function() {
            var $this, qtyreceiving, $qty, currentqty, $location;
            currentqty        = 0;
            $this             = jQuery(this);
            $qty              = $this.siblings('.qty');
            $location         = $qty.parents('.assetsetlocation-record').find('.location');
            qtyreceiving      = FwFormField.getValue($itemupdate, 'div[data-datafield="quantity"]');
            $itemupdate.find('.assetsetlocation-record .qty').each(function(index, element) {
                currentqty = currentqty + parseInt(jQuery(element).html());
            });
            if ((qtyreceiving > currentqty) && ($location.val() !== '')) {
                $qty.html(parseInt($qty.html()) + 1);
                $itemupdate.find('div[data-datafield="quantity"]').attr('data-minvalue', (currentqty + 1));
            }
        })
        .on('click', '.asl-subtract', function() {
            var $this, qtyreceiving, $qty, currentqty;
            currentqty        = 0;
            $this             = jQuery(this);
            $qty              = $this.siblings('.qty');
            qtyreceiving      = FwFormField.getValue($itemupdate, 'div[data-datafield="quantity"]');
            $itemupdate.find('.assetsetlocation-record .qty').each(function(index, element) {
                currentqty = currentqty + parseInt(jQuery(element).html());
            });
            if ((qtyreceiving >= currentqty) && (parseInt($qty.html()) !== 0)) {
                $qty.html(parseInt($qty.html()) - 1);
                $itemupdate.find('div[data-datafield="quantity"]').attr('data-minvalue', (currentqty - 1));
            }
        })
        .on('change', '.location', function() {
            var $this = jQuery(this);
            $itemupdate.find('.assetsetlocation-record .location').each(function(index, element) {
                if ((element !== $this[0]) && (jQuery(element).val() === $this.val())) {
                    $this.val('');
                    $this.parents('.assetsetlocation-record').find('.qty').html('0');
                }
            });
            if (($this.val() !== '') && (!$this.hasClass('triggered'))) {
                $this.addClass('triggered');
                $itemupdate.addassetsetlocation();
            }
        })
    ;
    $itemupdate.showscreen = function(recorddata) {
        var html = [];
        $itemupdate.show();
        $itemupdate.$cancel.show();
        $itemupdate.$submit.show();

        $itemupdate.data('recorddata', recorddata);
        $itemupdate.find('.item-description').html(recorddata.master)
        $itemupdate.find('.item-icode').html('I-Code: ' + recorddata.masterno)
        $itemupdate.find('.remaining').html(recorddata.qtyremaining);
        $itemupdate.find('.received').html(recorddata.qtyreceived);
        $itemupdate.find('.session').html(recorddata.qtysession);
        $itemupdate.find('.returned').html(recorddata.qtyreturned);

        $itemupdate.find('.item-fields').empty();
        if (recorddata.trackedby === 'BARCODE') {
            html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Barcode" data-type="text" data-required="true" data-datafield="barcode" />');
            html.push('</div>');
            html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Asset Location" data-type="text" data-required="true" data-datafield="assetlocation" />');
            html.push('</div>');
            program.setScanTarget('div[data-datafield="barcode"] .fwformfield-value');
        } else if (recorddata.trackedby === 'QUANTITY') {
            html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('  <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Quantity To Receive" data-type="number" data-minvalue="0" data-maxvalue="' + recorddata.qtyremaining + '" data-required="true" data-datafield="quantity" />');
            html.push('</div>');
            html.push('<div class="assetsetlocation">');
            html.push('  <div class="assetsetlocation-titlerow flexrow">');
            html.push('    <div class="assetsetlocation-col1">Item Set Location</div>');
            html.push('    <div class="assetsetlocation-col2">Qty</div>');
            html.push('  </div>');
            html.push('  <div class="assetsetlocation-body"> </div>');
            html.push('</div>');
        }
        $itemupdate.find('.item-fields').append(html.join(''));
        FwControl.renderRuntimeControls($itemupdate.find('.item-fields .fwcontrol'));
        $itemupdate.addassetsetlocation();
    };
    $itemupdate.$cancel = FwMobileMasterController.addFormControl(screen, 'Cancel', 'left', '&#xE14C;', false, function() { //cancel
        $itemupdate.hide();
        $itemupdate.data('recorddata', null);
        $itemupdate.$cancel.hide();
        $itemupdate.$submit.hide();
        $scan.showscreen();
    });
    $itemupdate.$submit = FwMobileMasterController.addFormControl(screen, 'Submit', 'right', '&#xE161;', false, function() { //save
        var request = {};
        if ($itemupdate.validate()) {
            request.recorddata        = $itemupdate.data('recorddata');
            request.orderid           = screen.properties.selectedpo.orderid;
            request.selectedorderid   = screen.properties.selectedorder.orderid;
            request.receivecontractid = ((typeof screen.properties.receivecontractid !== 'undefined') ? screen.properties.receivecontractid : '');
            if ($itemupdate.data('recorddata').trackedby === 'BARCODE') {
                request.barcode           = $itemupdate.find('div[data-datafield="barcode"] .fwformfield-value').val();
                request.location          = $itemupdate.find('div[data-datafield="assetlocation"] .fwformfield-value').val();
                request.qty               = '1';
            } else if ($itemupdate.data('recorddata').trackedby === 'QUANTITY') {
                var $asl_records;
                request.qty               = $itemupdate.find('div[data-datafield="quantity"] .fwformfield-value').val();
                request.assetsetlocations = [];
                $asl_records = $itemupdate.find('.assetsetlocation-record');
                for (var i = 0; i < $asl_records.length; i++) {
                    var location, qty;
                    location = jQuery($asl_records[i]).find('.location').val();
                    qty      = jQuery($asl_records[i]).find('.qty').html();
                    if ((location !== '') && (qty !== '0')) {
                        request.assetsetlocations.push({'location': location, 'qty': qty});
                    }
                }
            }

            RwServices.callMethod("ReceiveOnSet", "POReceive", request, function(response) {
                if (response.receive.errno === "0") {
                    screen.properties.receivecontractid = response.receive.receivecontractid;
                    $itemupdate.hide();
                    $itemupdate.$cancel.hide();
                    $itemupdate.$submit.hide();
                    $scan.getpicturepopup($itemupdate.data('recorddata'), request.barcode);
                    $scan.showscreen();
                    $itemupdate.data('recorddata', null);
                } else {
                    alert(response.receive.errmsg);
                }
            });
        }
    });
    $itemupdate.addassetsetlocation = function() {
        var html = [], $assetsetlocation;

        html.push('<div class="assetsetlocation-record flexrow" data-recordcount="' + $itemupdate.find('.assetsetlocation-record').length + '">');
        html.push('  <div class="assetsetlocation-col1">');
        html.push('    <input type="text" class="location" />');
        html.push('  </div>');
        html.push('  <div class="assetsetlocation-col2 flexrow">');
        html.push('    <i class="material-icons md-dark asl-subtract">&#xE15D;</i>'); //remove_circle_outline
        html.push('    <div class="qty">0</div>');
        html.push('    <i class="material-icons md-dark asl-add">&#xE148;</i>'); //add_circle_outline
        html.push('  </div>');
        html.push('</div>');
        $assetsetlocation = jQuery(html.join(''));

        $itemupdate.find('.assetsetlocation-body').append($assetsetlocation);
    };
    $itemupdate.validate = function() {
        var valid = true;
        if ($itemupdate.data('recorddata').trackedby === 'BARCODE') {
            if ($itemupdate.find('div[data-datafield="barcode"] .fwformfield-value').val() === '') {
                $itemupdate.find('div[data-datafield="barcode"]').addClass('error');
                valid = false;
            } else {
                $itemupdate.find('div[data-datafield="barcode"]').removeClass('error');
            }
            if ($itemupdate.find('div[data-datafield="assetlocation"] .fwformfield-value').val() === '') {
                $itemupdate.find('div[data-datafield="assetlocation"]').addClass('error');
                valid = false;
            } else {
                $itemupdate.find('div[data-datafield="assetlocation"]').removeClass('error');
            }
        } else if ($itemupdate.data('recorddata').trackedby === 'QUANTITY') {
            if ($itemupdate.find('div[data-datafield="quantity"] .fwformfield-value').val() <= 0) {
                $itemupdate.find('div[data-datafield="quantity"]').addClass('error');
                valid = false;
            } else {
                $itemupdate.find('div[data-datafield="quantity"]').removeClass('error');
            }
        }

        return valid;
    };

    $signature.showscreen = function() {
        $signature.$back.show();
        $signature.$createcontract.show();

        window.setTimeout(function() {
            screen.onWindowResize();
        }, 500);
        if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
            window.screen.lockOrientation('landscape-primary');
            window.setTimeout(function() {
                $signature.show();
                screen.onWindowResize();
            }, 500);
        } else {
            window.addEventListener('resize', screen.onWindowResize, false);
            screen.onWindowResize();
            $signature.show();
        }
        $signature.signaturePad();
    };
    $signature.$back = FwMobileMasterController.addFormControl(screen, 'Back', 'left', '&#xE5CB;', false, function() { //back
        $signature.hide();
        $signature.$back.hide();
        $signature.$createcontract.hide();
        $scan.showscreen();
        if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
            window.screen.lockOrientation('portrait-primary');
        }
        window.removeEventListener('resize', screen.onWindowResize, false);
    });
    $signature.$createcontract = FwMobileMasterController.addFormControl(screen, 'Create Contract', 'right', '', false, function() {
        var request;
        request = {
            poid:              screen.properties.selectedpo.orderid,
            receivecontractid: screen.properties.receivecontractid,
            orderid:           screen.properties.selectedorder.orderid,
            signatureimage:    screen.$view.find('.rfv-signaturecapture').signaturePad().getSignatureImage('image/jpeg').replace('data:image/jpeg;base64,', '')
        };
        RwServices.callMethod("ReceiveOnSet", "CreateContract", request, function(response) {
            var $confirmation, $ok;
            $confirmation = FwConfirmation.renderConfirmation('Message', 'RECEIVE Contract created (' + response.contract.receivecontractno + ')<br />OUT Contract created (' + response.contract.outcontractno + ')');
            $ok           = FwConfirmation.addButton($confirmation, 'Ok', true);

            if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
                window.screen.lockOrientation('portrait-primary');
            }
            window.removeEventListener('resize', screen.onWindowResize, false);

            $ok.on('click', function() {
                program.navigate('home/home');
            });
        });
    });

    screen.onWindowResize = function() {
        var width = $signature.width() - 6;
        if (width > window.innerWidth) {
            width = window.innerWidth - 6;
        }
        canvas.width = width;
        $signature.find('.clearButton').click();
    };

    screen.load = function() {
        $findpo.showscreen();
    };

    screen.unload = function () {
        $findpo.find('#posearch').fwmobilesearch('destroy');
        $findset.find('#setsearch').fwmobilesearch('destroy');
    };
    
    return screen;
};
//----------------------------------------------------------------------------------------------