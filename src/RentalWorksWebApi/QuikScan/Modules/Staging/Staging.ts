class StagingControllerClass {
    //----------------------------------------------------------------------------------------------
    getStagingScreen(viewModel, properties) {
        var pageTitle = '';
        let pageSubTitle = '';
        var locationdata = null, moduleproperties = null;
        //const applicationOptions = program.getApplicationOptions();
        switch (properties.moduleType) {
            case RwConstants.moduleTypes.Order:
                 switch(properties.activityType) {
                    case RwConstants.activityTypes.Staging:
                        if (properties.stagingType === RwConstants.stagingType.Normal) {
                            pageTitle = RwLanguages.translate('Staging');
                        }
                }
                break;
            case RwConstants.moduleTypes.Transfer:
                switch(properties.activityType) {
                    case RwConstants.activityTypes.Staging:
                        pageTitle = RwLanguages.translate('Transfer') + ' ' + RwLanguages.translate('Out');
                        break;
                }
                break;
            case RwConstants.moduleTypes.Truck:
                pageTitle = RwLanguages.translate('Package Truck');
                break;
        }
        const combinedViewModel = jQuery.extend({
            captionPageTitle:         pageTitle
          , captionPageSubTitle:      pageSubTitle
          , captionStage:              RwLanguages.translate('Stage')
          , captionUnstage:            RwLanguages.translate('Unstage')
          , captionCancel:             RwLanguages.translate('Cancel')
          , captionUnstageMode:        RwLanguages.translate('Mode')
          , captionResponsiblePerson:  RwLanguages.translate('Responsible Person')
          , valueTxtQty:               ''
          , captionQty:                RwLanguages.translate('Qty')
          , captionSummary:            RwLanguages.translate('Summary')
          , captionOrdered:            RwLanguages.translate('Ordered')
          , captionICodeDesc:          RwLanguages.translate('I-Code')
          , captionSub:                RwLanguages.translate('Sub')
          , captionOut:                RwLanguages.translate('Out')
          , captionIn:                 RwLanguages.translate('In')
          , captionStaged:             RwLanguages.translate('Staged')
          , captionRemaining:          RwLanguages.translate('Remaining')
          , captionAddToOrder:         RwLanguages.translate('Would you like to add this to the order?')
          , captionAddComplete:        RwLanguages.translate('Add Complete')
          , captionAddItem:            RwLanguages.translate('Add Item')
          , captionYes:                RwLanguages.translate('Yes')
          , captionDesc:               RwLanguages.translate('Description')
          , captionPendingListButton:  RwLanguages.translate('Pending')
          , captionScanButton:         RwLanguages.translate('Scan')
          , captionRFIDButton:         RwLanguages.translate('RFID')
          , captionStagedListButton:   RwLanguages.translate('Staged')
          , captionCreateContract:     RwLanguages.translate('Create Contract')
          , captionSerialNoSelection:  RwLanguages.translate('Serial No. Selection')
          , captionApplyAllQtyItems:   RwLanguages.translate('Apply All Qty Items')
          , captionOverrideAvailabilityReservation: RwLanguages.translate('Override Availability Reservation')
          , captionStageConsignedItem: RwLanguages.translate('Stage Consigned Item')
          , captionTransferRepair:     RwLanguages.translate('Transfer Repair')
          , captionAddContainerToOrder:RwLanguages.translate('Add Container to Order')
          , captionSubItem:            RwLanguages.translate('Substitute Item')
          , captionSubComplete:        RwLanguages.translate('Substitute Complete')
        }, viewModel);

        combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-staging').html(), combinedViewModel, {});
        const screen: any = {};
        screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);

        const $primarywindow      = screen.$view.find('#staging-primary');
        const $orderlocation      = screen.$view.find('#staging-orderlocation');
        const $substituteitemskit = screen.$view.find('#staging-substituteitemkit');

        screen.$modulecontrol = screen.$view.find('.modulecontrol');
        screen.$modulecontrol.fwmobilemodulecontrol({
            buttons: [
                {
                    id: 'search-btnhome',
                    caption: 'Home',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       'search',
                    buttonclick: function () {
                        try {
                            program.navigate('home/home');
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id: 'ordersuspendedsessions-back',
                    caption: 'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       'ordersuspendedsessions',
                    buttonclick: function () {
                        try {
                            screen.getCurrentPage().back();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id: 'ordersuspendedsessions-newsession',
                    caption: 'New Session',
                    orientation: 'right',
                    icon:        '&#xE5CC;', //continue
                    state:       'ordersuspendedsessions',
                    buttonclick: function () {
                        try {
                            var request = {
                                orderid: screen.getOrderId()
                            };
                            RwServices.callMethod('Staging', 'CreateSuspendedSession', request, function (response) {
                                try {
                                    screen.setContractId(response.contractid);
                                    screen.setSessionNo(response.sessionno);
                                    screen.pages.staging.forward();
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id: 'staging-close',
                    caption: 'Close',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       'staging',
                    buttonclick: function () {
                        try {
                            screen.getCurrentPage().back();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id:          'staging-menu',
                    type:        'menu',
                    orientation: 'right',
                    icon:        '&#xE5D4;', //more_vert
                    state:       'staging',
                    menuoptions: [
                        {
                            id:      'applyallqtyitems',
                            caption: 'Apply All Qty Items',
                            buttonclick: function() {
                                try {
                                    var $confirmation = FwConfirmation.renderConfirmation('Confirm',  RwLanguages.translate('Apply All Qty Items') + '?');
                                    var $btnok = FwConfirmation.addButton($confirmation, 'OK', true);
                                    $btnok.focus();
                                    FwConfirmation.addButton($confirmation, 'Cancel', true);
                                    $btnok.on('click', function() {
                                        try {
                                            var request = {
                                                orderid:    screen.getOrderId(),
                                                contractid: screen.getContractId()
                                            };
                                            RwServices.order.stageAllQtyItems(request, screen.getStagingPendingItemsCallback);
                                        } catch(ex) {
                                            FwFunc.showError(ex);
                                        }
                                    });
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            }
                        },
                        {
                            id:      'selectorderlocation',
                            caption: 'Select Order Location',
                            buttonclick: function() {
                                $primarywindow.hide();
                                $orderlocation.showscreen();
                            }
                        }
                    ]
                },
                {
                    id: 'staging-createcontract',
                    caption: 'Create Contract',
                    orientation: 'right',
                    icon:        '&#xE5CC;', //chevron_right
                    state:       'staging',
                    isVisible: function () {
                        var isVisible = false;
                        if (sessionStorage.getItem('users_enablecreatecontract') !== null) {
                            isVisible = (sessionStorage.getItem('users_enablecreatecontract') === 'T');
                        }
                        return isVisible;
                    },
                    buttonclick: function () {
                        try {
                            var request = {
                                orderid: screen.getOrderId(),
                                contractid: screen.getContractId()
                            };
                            RwServices.callMethod('Staging', 'OrdertranExists', request, function (response) {
                                try {
                                    if (response.ordertranExists) {
                                        if (properties.moduleType === 'Transfer') {
                                            var request = {
                                                contractId:          screen.getContractId(),
                                                contractType:        'OUT',
                                                orderId:             screen.getOrderId(),
                                                responsiblePersonId: ((typeof properties.responsibleperson !== 'undefined') && (properties.responsibleperson.showresponsibleperson === 'T')) ? properties.responsibleperson.responsiblepersonid : '',
                                                printname:           '',
                                                signatureImage:      '',
                                                images:              []
                                            };
                                            RwServices.callMethod('Staging', 'CreateContract', request, function (response) {
                                                if (response.createcontract.status === 0) {
                                                    var $confirmation = FwConfirmation.renderConfirmation('Message', response.createcontract.msg);
                                                    var $ok           = FwConfirmation.addButton($confirmation, 'OK', true);

                                                    $ok.on('click', function () {
                                                        program.navigate('home/home');
                                                    });
                                                } else {
                                                    FwFunc.showError(response.createcontract.msg);
                                                }
                                            });
                                        } else {
                                            properties.contract = {
                                                contractType: 'OUT',
                                                contractId: screen.getContractId(),
                                                orderId: screen.getOrderId(),
                                                responsiblePersonId: ((typeof properties.responsibleperson !== 'undefined') && (properties.responsibleperson.showresponsibleperson === 'T')) ? properties.responsibleperson.responsiblepersonid : ''
                                            };
                                            program.pushScreen(RwOrderController.getContactSignatureScreen(viewModel, properties));
                                        }
                                    } else {
                                        FwFunc.showMessage("There is no activity on this Staging Session!");
                                    }
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id: 'selectserialno-back',
                    caption: 'Back',
                    orientation: 'left',
                    icon: '&#xE5CB;', //chevron_left
                    state: 'selectserialno',
                    buttonclick: function () {
                        try {
                            screen.getCurrentPage().back();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id: 'selectserialno-setmeters',
                    caption: 'Set Meters',
                    orientation: 'right',
                    icon: '&#xE5CC;', //continue
                    state: 'selectserialno',
                    buttonclick: function () {
                        try {
                            var masterid = screen.pages.selectserialno.getMasterId();
                            var masteritemid = screen.pages.selectserialno.getMasterItemId();
                            var description = screen.pages.selectserialno.getDescription();
                            var masterno = screen.pages.selectserialno.getMasterNo();
                            screen.pages.serialmeters.forward(masterid, masteritemid, description, masterno);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id: 'serialmeters-back',
                    caption: 'Back',
                    orientation: 'left',
                    icon: '&#xE5CB;', //chevron_left
                    state: 'serialmeters',
                    buttonclick: function () {
                        try {
                            screen.getCurrentPage().back();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id: 'serialmeters-finish',
                    caption: 'Finish',
                    orientation: 'right',
                    icon: '&#xE5CC;', //continue
                    state: 'serialmeters',
                    buttonclick: function () {
                        try {
                            screen.pagehistory.pop();
                            screen.pagehistory.pop();
                            screen.getCurrentPage().show();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }
            ]
        });
        //----------------------------------------------------------------------------------------------------
        // Pending Items
        //----------------------------------------------------------------------------------------------------
        screen.$view.find('#pendingsearch').fwmobilesearch({
            service: 'Staging',
            method: 'GetPendingItems',
            searchModes: [
                {
                    caption: 'Scan (no refresh)',
                    placeholder: 'Scan Bar Code / I-Code',
                    value: 'code',
                    hasVirtualNumpad: true,
                    //hasPager: false,
                    //hasSearchResults: false,
                    search: function (value, plugin) {
                        if (value.length > 0) {
                            screen.scanCode(value);
                            plugin.clearsearchbox();
                        } else {
                            return true; // do a normal search
                        }
                    },
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                },
                {
                    caption: 'Search by Description',
                    placeholder: 'Description',
                    value: 'description',
                    //search: function (description) {
                    //    if (description.length > 0) {
                    //        screen.$view.find('#pendingsearch').fwmobilesearch('search');
                    //    }
                    //},
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                }
            ],
            getRequest: function () {
                var request = {
                    orderid: screen.getOrderId(),
                    contractid: ''
                };
                return request;
            },
            cacheItemTemplate: false,
            itemTemplate: function (model) {
                var html: string | string[] = [], isClickableRentalItem = false, isClickableSalesItem = false;
                //const isHeaderRow = ((model.itemclass === 'N') || (model.missingqty === 0));
                //let cssClass = '';
                //if (!isHeaderRow) {
                //    isClickableRentalItem = ((model.rectype === 'R') &&
                //        ((model.trackedby === 'QUANTITY') || (model.trackedby === 'SERIALNO')) &&
                //        (model.itemclass[0] !== 'N') &&
                //        (model.qtysub === 0)
                //    );
                //    isClickableSalesItem = (model.rectype === 'S');
                //    if (isClickableRentalItem || isClickableSalesItem) {
                //        if (cssClass.length > 0) {
                //            cssClass += ' ';
                //        }
                //        cssClass += 'link';
                //    }
                //}
                //if (cssClass.length > 0) {
                //    cssClass += ' ';
                //}
                //cssClass += 'itemclass-' + model.itemclass;
                let availablefor = '';
                if (model.rectype === 'R') {
                    availablefor = 'Rent';
                } else if (model.rectype === 'S') {
                    availablefor = 'Sell';
                } else if (model.rectype === 'P') {
                    availablefor = 'Parts';
                }
                html.push(
`<div data-itemclass="{{itemclass}}" data-rectype="{{rectype}}">
  <div class="row">
    <div class="title">{{description}}</div>
    </div>
  <div class="row">
    <div class="col1 masterno caption">I-Code:</div>
    <div class="col2 masterno value">{{masterno}}</div>
    <div class="col3 missingqty caption">Remaining:</div>
    <div class="col4 missingqty value">{{missingqty}}</div>
  </div>
  <div class="row">`);

                if (model.rectype === 'R') {
                    html.push(
`  
   <div class="col1 trackedby caption">Tracked By:</div>
   <div class="col2 trackedby value">{{trackedby}}</div>`);
                } else {
                    html.push(
`  <div class="col1 availfor caption">Available For:</div>
  <div class="col2 availfor value">` + availablefor + `</div>`);
                }
                html.push(
`  <div class="col3 qtyordered caption">Ordered:</div>
  <div class="col4 qtyordered value">{{qtyordered}}</div>
</div>
<div class="row">
  <div class="col1 caption"></div>
  <div class="col2 value"></div>
  <div class="col3 qtystagedandout caption">Staged/Out:</div>
  <div class="col4 qtystagedandout value">{{qtystagedandout}}</div>
</div>
<div class="row">`);
                if (model.consignorid !== '') {
                    html.push(
`  <div class="col1 vendor caption">Consignor:</div>
  <div class="col2 vendor value">{{vendor}}</div>`);
                } else if (model.subvendorid !== '') {
                    html.push(
`  <div class="col1 vendor caption">Sub-Vendor:</div>
  <div class="col234 vendor value">{{vendor}} <span style="color:#ffff00;">(PO Sub-Receive to move to Staged)</span></div>`);
                }
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            hasRecordClick: (model: any): boolean => {
                //return ((model.trackedby === 'SERIALNO' || model.trackedby === 'QUANTITY' || model.subbyquantity) && (model.qtystillout > 0));
                let hasRecordClick = false;
                const isHeaderRow = ((model.itemclass === 'N') || (model.missingqty === 0));
                if (!isHeaderRow) {
                    const isClickableRentalItem = ((model.rectype === 'R') &&
                        ((model.trackedby === 'QUANTITY') || (model.trackedby === 'SERIALNO') || (model.trackedby === 'BARCODE')) &&
                        (model.itemclass[0] !== 'N') &&
                        (model.qtysub === 0)
                    );
                    const isClickableSalesItem = (model.rectype === 'S');
                    hasRecordClick = isClickableRentalItem || isClickableSalesItem;
                }
                return hasRecordClick;
            },
            recordClick: function (recorddata, $record) {
                var $this, requestStageItem, requestSelectSerialNo, rectype, trackedby;
                try {
                    $this = jQuery(this);
                    rectype = recorddata.rectype;
                    trackedby = recorddata.trackedby;
                    if (rectype === 'R') {
                        if (trackedby === 'QUANTITY') {
                            requestStageItem = {
                                orderid:              screen.getOrderId(),
                                code:                 '',
                                masteritemid:         recorddata.masteritemid,
                                qty:                  0,
                                additemtoorder:       false,
                                addcompletetoorder:   false,
                                releasefromrepair:    false,
                                unstage:              false,
                                vendorid:             '',
                                meter:                0,
                                location:             '',
                                locationdata:         screen._locationdata(),
                                addcontainertoorder:  false,
                                overridereservation:  false,
                                stageconsigned:       false,
                                transferrepair:       false,
                                removefromcontainer:  false,
                                contractid:           screen.getContractId(),
                                ignoresuspendedin:    false,
                                consignorid:          recorddata.consignorid,
                                consignoragreementid: recorddata.consignoragreementid,
                                playStatus:           false
                            };
                            RwServices.order.pdastageitem(requestStageItem, function (responseStageItem) {
                                properties.responseStageItem = responseStageItem;
                                screen.pdastageitemCallback(responseStageItem);
                            });
                        } else if (trackedby === 'SERIALNO') {
                            var masterid        = recorddata.masterid;
                            var masteritemid    = recorddata.masteritemid;
                            var description     = recorddata.description;
                            var masterno        = recorddata.masterno;
                            var missingqty      = recorddata.missingqty;
                            var qtyordered      = recorddata.qtyordered;
                            var qtystagedandout = recorddata.qtystagedandout;
                            //screen.pages.selectserialno.forward(masterid, masteritemid, description, masterno, missingqty, qtyordered, qtystagedandout);
                            screen.renderPopupQty();
                            screen.$popupQty.find('#staging-popupQty-pnlSelectSerialNo').show();
                            screen.$popupQty.find('#staging-popupQty-pnlSubstituteKit').show();

                            jQuery('#staging-popupQty-btnSelectSerialNo').on('click', function () {
                                screen.pages.selectserialno.forward(masterid, masteritemid, description, masterno, missingqty, qtyordered, qtystagedandout);
                                FwPopup.destroyPopup(screen.$popupQty);
                            });
                            screen.$popupQty.find('#staging-popupQty-btnSubstituteItem').on('click', function () {
                                $primarywindow.hide();
                                $substituteitemskit.showscreen(recorddata.masterid, recorddata.masteritemid, recorddata.masterno, recorddata.description);
                                FwPopup.destroyPopup(screen.$popupQty);
                            });
                        } else if (trackedby === 'BARCODE') {
                            screen.renderPopupQty();
                            screen.$popupQty.find('#staging-popupQty-pnlSubstituteKit').show();
                            screen.$popupQty.find('#staging-popupQty-btnSubstituteItem').on('click', function () {
                                $primarywindow.hide();
                                $substituteitemskit.showscreen(recorddata.masterid, recorddata.masteritemid, recorddata.masterno, recorddata.description);
                                FwPopup.destroyPopup(screen.$popupQty);
                            });
                        }
                    } else if (rectype === 'S') {
                        requestStageItem = {
                            orderid:              screen.getOrderId(),
                            code:                 '',
                            masteritemid:         recorddata.masteritemid,
                            qty:                  0,
                            additemtoorder:       false,
                            addcompletetoorder:   false,
                            releasefromrepair:    false,
                            unstage:              false,
                            vendorid:             '',
                            meter:                0,
                            location:             '',
                            locationdata:         screen._locationdata(),
                            addcontainertoorder:  false,
                            overridereservation:  false,
                            stageconsigned:       false,
                            transferrepair:       false,
                            removefromcontainer:  false,
                            contractid:           screen.getContractId(),
                            ignoresuspendedin:    false,
                            consignorid:          '',
                            consignoragreementid: '',
                            playStatus:           false
                        };
                        RwServices.order.pdastageitem(requestStageItem, function (responseStageItem) {
                            properties.responseStageItem = responseStageItem;
                            screen.pdastageitemCallback(responseStageItem);
                        });
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            afterLoad: function (plugin, response) {
                if (sessionStorage.getItem('users_qsallowapplyallqtyitems') === 'T') {
                    screen.$modulecontrol.fwmobilemodulecontrol('showButton', '#applyallqtyitems');
                } else {
                    screen.$modulecontrol.fwmobilemodulecontrol('hideButton', '#applyallqtyitems');
                }
                var showhideselectorder = (moduleproperties.syscontrol.itemsinrooms === true) ? 'showButton' : 'hideButton';
                screen.$modulecontrol.fwmobilemodulecontrol(showhideselectorder, '#selectorderlocation');
            }
        });
        //----------------------------------------------------------------------------------------------------
        // Staged Items
        //----------------------------------------------------------------------------------------------------
        screen.$view.find('#stagedsearch').fwmobilesearch({
            service: 'Staging',
            method: 'GetStagedItems',
            searchModes: [
                {
                    caption: 'Scan (no refresh)',
                    placeholder: 'Scan Bar Code / I-Code',
                    value: 'code',
                    hasVirtualNumpad: true,
                    //hasPager: false,
                    //hasSearchResults: false,
                    search: function (value, plugin) {
                        if (value.length > 0) {
                            screen.scanCode(value);
                            plugin.clearsearchbox();
                        } else {
                            return true;
                        }
                    },
                    click: function (plugin) {
                        try {
                            plugin.clearsearchbox();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    caption: 'Search by Description',
                    placeholder: 'Search by Description',
                    value: 'description',
                    click: function (plugin) {
                        try {
                            plugin.clearsearchbox();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }
            ],
            getRequest: function () {
                var request = {
                    orderid: screen.getOrderId(),
                    contractid: screen.getContractId()
                };
                return request;
            },
            cacheItemTemplate: false,
            itemTemplate: function (model: any, dt: FwJsonDataTable, rowno: number) {
                const itemclass = model.itemclass;
                const rectype = model.rectype;
                const qty = model.quantity;
                let html: string | string[] = [];
                let cssClass: string = '';
                let isContainerHeaderRow = ((itemclass === 'N') || (qty === 0));
                let availablefor: string = '';
                if (rectype === 'R') { availablefor = 'Rent'; }
                else if (rectype === 'S') { availablefor = 'Sell'; }
                else if (rectype === 'P') { availablefor = 'Parts'; }
                html.push(
`<div class="${cssClass}" data-rectype="${rectype}" data-itemclass="${itemclass}">
  <div class="row">
    <div class="title">{{description}}</div>
  </div>`);
                if (isContainerHeaderRow) {
                    html.push('  <div class="details" style="display:none;">');
                } else {
                    html.push('  <div class="details">');
                }
                html.push('    <div class="row">');
                html.push('      <div class="col1 caption masterno">I-Code:</div>');
                html.push('      <div class="col2 value masterno">{{masterno}}</div>');
                html.push('      <div class="col3 caption staged">Staged:</div>');
                html.push('      <div class="col4 value staged">{{quantity}}</div>');
                html.push('</div>');
                if (model.rectype !== 'R') {
                    html.push(
`    <div class="row">;
      <div class="col1 caption rectype">Available For:</div>
      <div class="col2 value rectype">${availablefor}</div>
    </div>`);
                }
                if (model.rectype === 'R') {
                    html.push(
`    <div class="row">
      <div class="col1 caption trackedby">Tracked By:</div>
      <div class="col2 value trackedby">{{trackedby}}</div>
    </div>`);
                }
                if (model.barcode !== '') {
                    html.push('    <div class="row">');
                    switch (model.trackedby) {
                        case 'BARCODE':
                            html.push('      <div class="col1 caption barcode">Barcode:</div>');
                            break;
                        case 'SERIALNO':
                            html.push('      <div class="col1 caption barcode">Serial No:</div>');
                            break;
                        case 'RFID':
                            html.push('      <div class="col1 caption barcode">RFID:</div>');
                            break;
                    }

                    html.push('      <div class="col2 value barcode">{{barcode}}</div>');
                    html.push('    </div>');
                }
                if (model.vendorid !== '') {
                    html.push('    <div class="row">');
                    html.push('      <div class="col1 caption vendor">Vendor/Consignor:</td>');
                    html.push('      <div class="col234 value vendor" colspan="3">{{vendor}}</div>');
                    html.push('    </div>');
                }
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;

            },
            hasRecordClick: (model: any, dt: FwJsonDataTable, rowno: number): boolean => {
                let isContainerHeaderRow = ((model.itemclass === 'N') || (model.qty === 0));
                return (((model.itemclass !== 'NI') && (!isContainerHeaderRow)) ||
                        ((model.itemclass === 'NI') && (rowno > 0) && (dt.Rows[rowno - 1][dt.ColumnIndex.itemclass] === 'N')));
            },
            recordClick: function (recorddata, $record) {
                try {
                    const $this = jQuery(this);
                    const $contextmenu = FwContextMenu.render(recorddata.description, null);
                    FwContextMenu.addMenuItem($contextmenu, 'Unstage Item', function () {
                        try {
                            var requestStageItem = {
                                orderid: screen.getOrderId(),
                                code: recorddata.barcode,
                                masteritemid: recorddata.masteritemid,
                                qty: parseFloat(recorddata.quantity),
                                additemtoorder: false,
                                addcompletetoorder: false,
                                releasefromrepair: false,
                                unstage: true,
                                vendorid: recorddata.vendorid,
                                meter: 0,
                                location: '',
                                locationdata: screen._locationdata(),
                                addcontainertoorder: false,
                                overridereservation: false,
                                stageconsigned: false,
                                transferrepair: false,
                                removefromcontainer: false,
                                contractid: screen.getContractId(),
                                ignoresuspendedin: false,
                                consignorid: recorddata.consignorid,
                                consignoragreementid: recorddata.consignoragreementid,
                                playStatus: false
                            };
                            RwServices.order.pdastageitem(requestStageItem, function (responseStageItem) {
                                responseStageItem.unstageqty = requestStageItem.qty;
                                properties.responseStageItem = responseStageItem;
                                screen.unstageItemCallback(responseStageItem);
                                screen.$modulemodeselector.fwmobilemoduletabs('clickTab', '#tabstaged')
                            });
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            afterLoad: function (plugin, response) {
                jQuery('#staging-stagedList-pnlCreateContract')
                    .toggle((applicationConfig.designMode) || ((sessionStorage.users_enablecreatecontract === 'T') && (response.searchresults.Rows.length > 0)));
            }
        });

        screen.$modulemodeselector = screen.$view.find('.modulemodeselector');
        screen.$modulemodeselector.fwmobilemoduletabs({
            tabs: [
                {
                    id:          'tabpending',
                    caption:     'Pending',
                    buttonclick: function () {
                        try {
                            jQuery('#staging-stagedList').hide();
                            jQuery('#staging-scan').hide();
                            jQuery('#staging-rfid').hide();
                            jQuery('#staging-pendingList').show();
                            jQuery('#staging-scan').attr('data-mode', 'PENDING');
                            RwRFID.unregisterRFIDEvents();
                            program.setScanTargetLpNearfield('#pendingsearch .searchbox');
                            screen.$view.find('#pendingsearch').fwmobilesearch('refresh');
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id:          'tabrfid',
                    caption:     'RFID',
                    buttonclick: function () {
                        try {
                            var request;
                            jQuery('#staging-stagedList').hide();
                            jQuery('#staging-pendingList').hide();
                            jQuery('#staging-scan').hide();
                            jQuery('#staging-rfid').show();
                            jQuery('#staging-scan').attr('data-mode', 'RFID');
                            RwRFID.registerRFIDEvents(screen.rfidscan);
                            request = {
                                rfidmode: 'STAGING',
                                sessionid: screen.getOrderId(),
                                portal: device.uuid
                            };
                            RwServices.order.getRFIDStatus(request, function (response) {
                                screen.$view.find('.rfid-rfidstatus .exceptions .statusitem-value').html(response.exceptions.length);
                                screen.$view.find('.rfid-rfidstatus .pending .statusitem-value').html(response.pending.length);
                                (response.exceptions.length > 0) ? screen.$view.find('.btnexception').show() : screen.$view.find('.btnexception').hide();
                                (response.pending.length > 0) ? screen.$view.find('.btnpending').show() : screen.$view.find('.btnpending').show();
                            });
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                },
                {
                    id:          'tabstaged',
                    caption:     'Staged',
                    buttonclick: function () {
                        try {
                            jQuery('#staging-pendingList').hide();
                            jQuery('#staging-scan').hide();
                            jQuery('#staging-rfid').hide();
                            jQuery('#staging-stagedList').show();
                            jQuery('#staging-scan').attr('data-mode', 'STAGEDLIST');
                            RwRFID.unregisterRFIDEvents();
                            program.setScanTargetLpNearfield('#stagedsearch .searchbox');
                            screen.$view.find('#stagedsearch').fwmobilesearch('refresh');
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }
            ]
        });
        screen.$modulemodeselector.fwmobilemoduletabs('hideTab', '#tabpending');
        screen.$modulemodeselector.fwmobilemoduletabs('hideTab', '#tabrfid');
        screen.$modulemodeselector.fwmobilemoduletabs('hideTab', '#tabstaged');

        var searchModes = [];
        switch(properties.moduleType) {
            case 'Order':
                searchModes.push({
                    caption: 'Order No', placeholder: 'Scan Order No Barcode', value: 'orderno',
                    //search: function (orderno) {
                    //    if (orderno.length > 0) {
                    //        screen.selectOrder(orderno, false);
                    //    } else {
                    //        return true;
                    //    }
                    //},
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                });

                searchModes.push({
                    caption: 'Description', placeholder: 'Description', value: 'orderdesc',
                    click: function (plugin) {
                        //plugin.clearsearchbox();
                    }
                });

                searchModes.push({
                    caption: 'Deal', placeholder: 'Deal', value: 'deal',
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                });

                searchModes.push({
                    caption: 'Suspended Sessions', placeholder: 'Session No', value: 'sessionno',
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                });
                break;
            case 'Truck':
                searchModes.push({
                    caption: 'Truck No', placeholder: 'Scan Truck No Barcode', value: 'orderno',
                    search: function (orderno) {
                        if (orderno.length > 0) {
                            screen.selectOrder(orderno, false);
                        }
                    },
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                });

                searchModes.push({
                    caption: 'Description', placeholder: 'Description', value: 'orderdesc',
                    click: function (plugin) {
                        //plugin.clearsearchbox();
                    }
                });

                searchModes.push({
                    caption: 'Suspended Sessions', placeholder: 'Session No', value: 'sessionno',
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                });
                break;
            case 'Transfer':
                searchModes.push({
                    caption: 'Transfer No', placeholder: 'Scan Transfer No Barcode', value: 'orderno',
                    //search: function (orderno) {
                    //    if (orderno.length > 0) {
                    //        screen.selectOrder(orderno, false);
                    //    }
                    //},
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                });

                searchModes.push({
                    caption: 'Description', placeholder: 'Description', value: 'orderdesc',
                    click: function (plugin) {
                        //plugin.clearsearchbox();
                    }
                });

                searchModes.push({
                    caption: 'Suspended Sessions', placeholder: 'Session No', value: 'sessionno',
                    click: function (plugin) {
                        plugin.clearsearchbox();
                    }
                });
                break;
        }
    
        screen.$search = screen.$view.find('.search').fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase: true,
            service: 'Staging',
            method: 'GetSearchResults',
            getRequest: function() {
                var request = {
                    moduletype: properties.moduleType
                };
                return request;
            },
            queryTimeout: 30,
            searchModes: searchModes,
            cacheItemTemplate: false,
            itemTemplate: function (model) {
                var html: string | string[] = [];
                html.push('<div class="item">');
                html.push('  <div class="row1"><span class="orderdesc">{{orderdesc}}</span></div>');
                html.push('  <div class="row2">');
                html.push('    <div class="col1">');
                if (typeof model.orderno !== 'undefined') {
                    html.push('      <div class="datafield orderno">');
                    switch(properties.moduleType) {
                        case 'Order':
                            html.push('        <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                            break;
                        case 'Truck':
                            html.push('        <div class="caption">' + RwLanguages.translate('Truck No') + ':</div>');
                            break;
                        case 'Transfer':
                            html.push('        <div class="caption">' + RwLanguages.translate('Transfer No') + ':</div>');
                            break;
                    }
                    html.push('        <div class="value">{{orderno}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.orderdate !== 'undefined') {
                    html.push('      <div class="datafield orderdate">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Date') + ':</div>');
                    html.push('        <div class="value">{{orderdate}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.deal !== 'undefined') {
                    html.push('      <div class="datafield deal">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Deal') + ':</div>');
                    html.push('        <div class="value">{{deal}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.estrentfrom !== 'undefined') {
                    html.push('      <div class="datafield estrentfrom">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Est') + ':</div>');
                    html.push('        <div class="value">{{estrentfrom}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.fromwarehouse !== 'undefined') {
                    html.push('      <div class="datafield fromwarehouse">');
                    html.push('        <div class="caption">' + RwLanguages.translate('From Warehouse') + ':</div>');
                    html.push('        <div class="value">{{fromwarehouse}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.towarehouse !== 'undefined') {
                    html.push('      <div class="datafield towarehouse">');
                    html.push('        <div class="caption">' + RwLanguages.translate('To Warehouse') + ':</div>');
                    html.push('        <div class="value">{{towarehouse}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.department !== 'undefined') {
                    html.push('      <div class="datafield department">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Department') + ':</div>');
                    html.push('        <div class="value">{{department}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.origorderno !== 'undefined') {
                    html.push('      <div class="datafield origorderno">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                    html.push('        <div class="value">{{origorderno}}</div>');
                    html.push('      </div>');
                }
                html.push('    </div>');
                html.push('    <div class="col2">');
                if (typeof model.status !== 'undefined') {
                    html.push('      <div class="datafield status">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Status') + ':</div>');
                    html.push('        <div class="value">{{status}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.statusdate !== 'undefined') {
                    html.push('      <div class="datafield statusdate">');
                    html.push('        <div class="caption">' + RwLanguages.translate('As Of') + ':</div>');
                    html.push('        <div class="value">{{statusdate}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.sessionno !== 'undefined') {
                    html.push('      <div class="datafield sessionno">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Session No') + ':</div>');
                    html.push('        <div class="value">{{sessionno}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.username !== 'undefined') {
                    html.push('      <div class="datafield username">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Owner') + ':</div>');
                    html.push('        <div class="value">{{username}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.pickdate !== 'undefined') {
                    html.push('      <div class="datafield pickdate">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Pick Date') + ':</div>');
                    html.push('        <div class="value">{{pickdate}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.shipdate !== 'undefined') {
                    html.push('      <div class="datafield shipdate">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Ship Date') + ':</div>');
                    html.push('        <div class="value">{{shipdate}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.receivedate !== 'undefined') {
                    html.push('      <div class="datafield receivedate">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Receive Date') + ':</div>');
                    html.push('        <div class="value">{{receivedate}}</div>');
                    html.push('      </div>');
                }
                html.push('    </div>');
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            beforeSearch: function () {
                
            },
            afterLoad: function (plugin, response) {
                var searchOption = plugin.getSearchOption();
                var isOrderNoSearch = searchOption === 'orderno';
                if (isOrderNoSearch) {
                    var searchText = plugin.getSearchText();
                    if (searchText.length > 0) {
                        if (response.searchresults.TotalRows === 0) {
                            program.playStatus(false);
                        } else if (response.searchresults.TotalRows === 1) {
                            var colOrderNo = response.searchresults.ColumnIndex['orderno'];
                            if (response.searchresults.Rows[0][colOrderNo].toUpperCase() === searchText.toUpperCase()) {
                                program.playStatus(true);
                                var $items = plugin.$element.find('.searchresults .item');
                                if ($items.length === 1) {
                                    var $item = $items.eq(0);
                                    $item.click();
                                }
                            }
                        }
                    }
                } 
            },
            recordClick: function (model) {
                try {
                    var mode = screen.$search.attr('data-mode');
                    if (mode === 'orderno' || mode === 'orderdesc' || mode === 'deal') {
                        if (screen.getIsSuspendedSessionsEnabled()) {
                            var request1 = {
                                orderid: model.orderid,
                                pageno: 0,
                                pagesize: 0
                            };
                            RwServices.callMethod('Staging', 'GetOrderSuspendedSessions', request1, function (response1) {
                                try {
                                    if (response1.searchresults.Rows.length > 0) {
                                        screen.setOrder(model);
                                        screen.pages.ordersuspendedsessions.forward();
                                    } else {
                                        // since there are no suspended sessions, make a new contractid
                                        var request2 = {
                                            orderid: model.orderid
                                        };
                                        RwServices.callMethod('Staging', 'CreateSuspendedSession', request2, function (response2) {
                                            try {
                                                var data = JSON.parse(JSON.stringify(model));
                                                data.contractid = response2.contractid;
                                                data.sessionno = response2.sessionno;
                                                screen.setOrder(data);
                                                screen.pages.staging.forward();
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        });
                                    }
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                        else { // if (!screen.getIsSuspendedSessionsEnabled())
                            screen.setOrder(model);
                            screen.pages.staging.forward();
                        }
                    }
                    else if (mode === 'sessionno') {
                        screen.setOrder(model);
                        screen.pages.staging.forward();
                    }

                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });

        screen.$ordersuspendedsessions = screen.$view.find('.ordersuspendedsessions').fwmobilesearch({
            hasIcon: true,
            placeholder: 'Search',
            upperCase: true,
            service: 'Staging',
            method: 'GetOrderSuspendedSessions',
            getRequest: function() {
                var request = {
                    orderid: screen.getOrderId(),
                    moduletype: properties.moduleType
                };
                return request;
            },
            queryTimeout: 30,
            searchModes: [
                {
                    caption: 'Suspended Sessions', placeholder: 'Session No', value: 'sessionno', visible: false,
                    click: function (plugin) {
                        plugin.clearsearchbox();
                        plugin.search();
                    }
                }
            ],
            cacheItemTemplate: false,
            itemTemplate: function (model) {
                var html: string | string[] = [];
                html.push('<div class="item">');
                html.push('  <div class="row1"><span class="orderdesc">{{orderdesc}}</span></div>');
                html.push('  <div class="row2">');
                html.push('    <div class="col1">');
                if (typeof model.orderno !== 'undefined') {
                    html.push('      <div class="datafield orderno">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Order No') + ':</div>');
                    html.push('        <div class="value">{{orderno}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.orderdate !== 'undefined') {
                    html.push('      <div class="datafield orderdate">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Date') + ':</div>');
                    html.push('        <div class="value">{{orderdate}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.deal !== 'undefined') {
                    html.push('      <div class="datafield deal">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Deal') + ':</div>');
                    html.push('        <div class="value">{{deal}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.estrentfrom !== 'undefined') {
                    html.push('      <div class="datafield estrentfrom">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Est') + ':</div>');
                    html.push('        <div class="value">{{estrentfrom}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.warehouse !== 'undefined') {
                    html.push('      <div class="datafield warehouse">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Warehouse') + ':</div>');
                    html.push('        <div class="value">{{warehouse}}</div>');
                    html.push('      </div>');
                }
                html.push('    </div>');
                html.push('    <div class="col2">');
                if (typeof model.status !== 'undefined') {
                    html.push('      <div class="datafield">');
                    html.push('        <div class="caption status">' + RwLanguages.translate('Status') + ':</div>');
                    html.push('        <div class="value status">{{status}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.statusdate !== 'undefined') {
                    html.push('      <div class="datafield">');
                    html.push('        <div class="caption statusdate">' + RwLanguages.translate('As Of') + ':</div>');
                    html.push('        <div class="value statusdate">{{statusdate}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.sessionno !== 'undefined') {
                    html.push('      <div class="datafield sessionno">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Session No') + ':</div>');
                    html.push('        <div class="value">{{sessionno}}</div>');
                    html.push('      </div>');
                }
                if (typeof model.username !== 'undefined') {
                    html.push('      <div class="datafield username">');
                    html.push('        <div class="caption">' + RwLanguages.translate('Owner') + ':</div>');
                    html.push('        <div class="value">{{username}}</div>');
                    html.push('      </div>');
                }
                html.push('    </div>');
                html.push('  </div>');
                html.push('</div>');
                html = html.join('\n');
                return html;
            },
            beforeSearch: function () {
                //screen.showScanBarcodeScreen(true);
            },
            afterLoad: function (plugin, response) {
                var searchOption = plugin.getSearchOption();
                var isSessionNoSearch = searchOption === 'sessionno';
                if (isSessionNoSearch) {
                    var searchText = plugin.getSearchText();
                    if (searchText.length > 0) {
                        if (response.searchresults.TotalRows === 0) {
                            program.playStatus(false);
                        } else if (response.searchresults.TotalRows === 1) {
                            var colSessionNo = response.searchresults.ColumnIndex['sessionno'];
                            if (response.searchresults.Rows[0][colSessionNo].toUpperCase() === searchText.toUpperCase()) {
                                program.playStatus(true);
                                var $items = plugin.$element.find('.searchresults .item');
                                if ($items.length === 1) {
                                    var $item = $items.eq(0);
                                    $item.click();
                                }
                            }
                        }
                    }
                }
            },
            recordClick: function (model) {
                try {
                    screen.setOrder(model);
                    screen.pages.staging.forward();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });

        screen.$view.find('.btnScanOrderNo').on('click', function () {
            try {
                screen.pages.scanorderno.forward();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        screen.$view.find('.btnOrderSearch').on('click', function () {
            try {
                screen.pages.search.forward();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        screen.$view.find('.btnSuspendedSessions').on('click', function () {
            try {
                screen.pages.suspendedsessions.forward();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        screen.setOrder = function (data) {
            if (typeof data.orderid !== 'undefined') {
                screen.setOrderId(data.orderid);
            }
            if (typeof data.orderno !== 'undefined') {
                screen.setOrderNo(data.orderno);
            }
            if (typeof data.orderdesc !== 'undefined') {
                screen.setOrderDesc(data.orderdesc);
            }
            if (typeof data.contractid !== 'undefined') {
                screen.setContractId(data.contractid);
            }
            if (typeof data.sessionno !== 'undefined') {
                screen.setSessionNo(data.sessionno);
            }
        };

        screen._locationdata = function(locationData?: object) {
            if (locationData) {
                locationdata = locationData;
            } else {
                return locationdata;
            }
        };

        screen.orderid = '';
        screen.setOrderId = function (orderid) {
            screen.orderid = orderid;
        };
        screen.getOrderId = function () {
            return screen.orderid;
        };

        screen.orderno = '';
        screen.setOrderNo = function (orderno) {
            screen.orderno = orderno;
        };
        screen.getOrderNo = function (orderno) {
            return screen.orderno;
        };

        screen.orderdesc = '';
        screen.setOrderDesc = function (orderdesc) {
            screen.orderdesc = orderdesc;
        };
        screen.getOrderDesc = function () {
            return screen.orderdesc;
        };

        screen.contractid = '';
        screen.setContractId = function (contractid) {
            screen.contractid = contractid;
        };
        screen.getContractId = function () {
            return screen.contractid;
        };

        screen.sessionno = '';
        screen.setSessionNo = function (sessionno) {
            screen.sessionno = sessionno;
        };
        screen.getSessionNo = function () {
            return screen.sessionno;
        };

        screen.getIsSuspendedSessionsEnabled = function () {
            return screen.isSuspendedSessionsEnabled;
        };

        screen.setIsSuspendedSessionsEnabled = function (isenabled) {
            screen.isSuspendedSessionsEnabled = isenabled;
        };

        screen.getCurrentPage = function () {
            return screen.pagehistory[screen.pagehistory.length - 1];
        };

        screen.pagehistory = [];
        screen.pages = {
            reset: function () {
                screen.$view.find('.page-stagingmenu')
                    .hide();
                screen.$view.find('.page-search')
                    .hide();
                screen.$search.find('.option[data-value="orderno"]').hide();
                screen.$search.find('.option[data-value="orderdesc"]').hide();
                screen.$search.find('.option[data-value="deal"]').hide();
                screen.$search.find('.option[data-value="sessionno"]').hide();
                screen.pages.ordersuspendedsessions.getElement().hide();
                screen.pages.staging.getElement().hide();
                screen.pages.selectserialno.getElement().hide();
                screen.pages.serialmeters.getElement().hide();
                screen.$modulemodeselector.fwmobilemoduletabs('hideTab', '#tabpending');
                screen.$modulemodeselector.fwmobilemoduletabs('hideTab', '#tabrfid');
                screen.$modulemodeselector.fwmobilemoduletabs('hideTab', '#tabstaged');
                program.setScanTargetLpNearfield('');
            },
            stagingmenu: {
                name: 'stagingmenu',
                getElement: function() {
                    return screen.$view.find('.page-stagingmenu');
                },
                show: function () {
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', this.name);
                    if (screen.getIsSuspendedSessionsEnabled()) {
                        var request = {
                            pageno: 1,
                            pagesize: 1,
                            searchmode: 'sessionno',
                            searchvalue: '',
                            moduletype: properties.moduleType
                        };
                        RwServices.callMethod('Staging', 'GetSearchResults', request, function (response) {
                            if (response.searchresults.TotalRows > 0) {
                                screen.pages.stagingmenu.show2();
                                screen.$view.find('.btnSuspendedSessions').show();
                            } else {
                                screen.pages.search.forward();
                            }
                        }, true);
                    } else {
                        screen.pages.stagingmenu.show2();
                    }
                },
                show2: function() {
                    screen.pages.reset();
                    screen.$view.find('.btnSuspendedSessions').hide();
                    FwMobileMasterController.setTitle('');
                    screen.$view.find('.page-stagingmenu').show();
                },
                forward: function () {
                    screen.pagehistory.push(screen.pages.stagingmenu);
                    screen.pages.stagingmenu.show();
                },
                back: function () {

                }
            },
            search: {
                name: 'search',
                getElement: function () {
                    return screen.$view.find('.page-search');
                },
                show: function () {
                    screen.pages.reset();
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', this.name);
                    FwMobileMasterController.setTitle('Select Order...');
                    if (screen.getIsSuspendedSessionsEnabled()) {
                        var request = {
                            pageno: 1,
                            pagesize: 1,
                            searchmode: 'sessionno',
                            searchvalue: '',
                            moduletype: properties.moduleType
                        };
                        RwServices.callMethod('Staging', 'GetSearchResults', request, function (response) {
                            if (response.searchresults.TotalRows > 0) {
                                screen.$modulecontrol.fwmobilemodulecontrol('showButton', '#search-btnhome');
                            }
                        }, true);
                    }
                    screen.$search.find('.option[data-value="orderno"]').show().click();
                    screen.$search.find('.option[data-value="orderdesc"]').show();
                    screen.$search.find('.option[data-value="deal"]').show();
                    screen.$view.find('.page-search').show();
                    screen.$search.fwmobilesearch('search');
                    screen.setOrderId('');
                    screen.setOrderNo('');
                    screen.setOrderDesc('');
                },
                forward: function() {
                    screen.pagehistory.push(screen.pages.search);
                    screen.pages.search.show();
                },
                back: function () {
                    screen.pagehistory.pop();
                    screen.getCurrentPage().show();
                }
            },
            suspendedsessions: {
                name: 'suspendedsessions',
                getElement: function () {
                    return screen.$view.find('.page-search');
                },
                show: function () {
                    screen.pages.reset();
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', this.name);
                    FwMobileMasterController.setTitle('Select Suspended Session...');
                    screen.$modulecontrol.fwmobilemodulecontrol('showButton', '#ordersuspendedsessions-back');
                    screen.$search.find('.option[data-value="sessionno"]').click();
                    screen.$view.find('.page-search').show();
                    screen.$search.fwmobilesearch('search');
                    screen.setOrderId('');
                    screen.setOrderNo('');
                    screen.setOrderDesc('');
                },
                forward: function () {
                    screen.pagehistory.push(screen.pages.suspendedsessions);
                    screen.pages.suspendedsessions.show();
                },
                back: function () {
                    screen.pagehistory.pop();
                    screen.getCurrentPage().show();
                }
            },
            ordersuspendedsessions: {
                name: 'ordersuspendedsessions',
                getElement: function () {
                    return screen.$view.find('.page-ordersuspendedsessions');
                },
                show: function () {
                    screen.pages.reset();
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', this.name);
                    FwMobileMasterController.setTitle('Select Suspended Session...');
                    screen.$view.find('.page-ordersuspendedsessions').show();
                    screen.$ordersuspendedsessions.fwmobilesearch('search');
                },
                forward: function() {
                    screen.pagehistory.push(screen.pages.ordersuspendedsessions);
                    screen.pages.ordersuspendedsessions.show();
                },
                back: function () {
                    screen.pagehistory.pop();
                    screen.getCurrentPage().show();
                }
            },
            staging: {
                name: 'staging',
                getElement: function () {
                    return screen.$view.find('.page-staging');
                },
                show: function () {
                    screen.pages.reset();
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', this.name);
                    screen.$modulemodeselector.fwmobilemoduletabs('showTab', '#tabpending');
                    screen.$modulemodeselector.fwmobilemoduletabs('showTab', '#tabstaged');
                    if (screen.getIsSuspendedSessionsEnabled()) {
                        FwMobileMasterController.setTitle(screen.getOrderNo() + ' - ' + screen.getOrderDesc() + ' (Session: ' + screen.getSessionNo() + ')');
                    } else {
                        FwMobileMasterController.setTitle(screen.getOrderNo() + ' - ' + screen.getOrderDesc());
                    }
                    screen.pages.staging.getElement().show();
                    jQuery('.tab.active').click();
                    jQuery(window).on('scroll', function() {
                        screen.evalWindowPosition();
                    });
                    jQuery(window).on('touchmove', function() {
                        screen.evalWindowPosition();
                    });
                    screen.$modulecontrol.fwmobilemodulecontrol('hideButton', '#applyallqtyitems');
                    screen.$modulecontrol.fwmobilemodulecontrol('hideButton', '#selectorderlocation');
                    screen.$modulemodeselector.fwmobilemoduletabs('clickTab', '#tabpending');
                    screen.toggleRfid();
                },
                forward: function() {
                    screen.pagehistory.push(screen.pages.staging);
                    screen.pages.staging.show();
                    screen.promptResponsiblePerson();
                },
                back: function () {
                    screen.confirmCancelSuspendContract(function () {
                        screen.setContractId('');
                        screen.setSessionNo('');
                        screen.$view.find('#staging-pendingList-ul').empty();
                        screen.$view.find('#staging-stagedList-ul').empty();
                        screen.pagehistory.pop();
                        screen.getCurrentPage().show();
                        jQuery(window).off('scroll').off('touchmove');
                        screen.$view.find('#pendingsearch').fwmobilesearch('clearsearchresults');
                    });
                }
            },
            selectserialno: {
                name: 'selectserialno',
                getElement: function () {
                    return screen.$view.find('.page-selectserialno');
                },
                show: function (masterid, masteritemid) {
                    screen.pages.reset();
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', this.name);
                    FwMobileMasterController.setTitle('Select Serial No(s)...');
                    var $pageselectserialno = screen.pages.selectserialno.getElement();
                    $pageselectserialno.show();
                    if (typeof masteritemid === 'undefined') {
                        masteritemid = screen.pages.selectserialno.getMasterItemId();
                    }
                    screen.pages.selectserialno.funcserialfrm(masteritemid);
                },
                forward: function(masterid, masteritemid, description, masterno, remaining, ordered, stagedandout) {
                    var $pageselectserialno = screen.pages.selectserialno.getElement();
                    $pageselectserialno.find('.info').empty();
                    $pageselectserialno.find('.items').empty();
                    var request = {
                        orderid:      screen.getOrderId(),
                        masterid:     masterid,
                        masteritemid: masteritemid
                    };
                    RwServices.callMethod('Staging', 'LoadSerialNumbers', request, function(response) {
                        try {
                            for(var i = 0; i < response.funcserialmeterout.length; i++) {
                                var $divSerialNo = jQuery('<div>')
                                    .addClass('serialnorow')
                                    .attr('data-masteritemid', response.funcserialmeterout[i].masteritemid)
                                    .attr('data-rentalitemid', response.funcserialmeterout[i].rentalitemid)
                                    .html('<div class="flexrow"><div class="caption">Serial No:</div><div class="value">' + response.funcserialmeterout[i].mfgserial + '</div></div>')
                                    .click(function() {
                                        try {
                                            var remaining = parseInt(screen.$view.find('.qtyremainingoutwsuspend .value').html());
                                            var $this     = jQuery(this);
                                            if ((remaining !== 0) || $this.hasClass('selected')) {
                                                var masteritemid           = $this.attr('data-masteritemid');
                                                var rentalitemid           = $this.attr('data-rentalitemid');
                                                var internalchar           = '';
                                                var meter                  = 0;
                                                var toggledelete           = true;
                                                var containeritemid        = '';       // mv 2016-12-12 not sure about this container code;
                                                var containeroutcontractid = '';
                                                screen.insertSerialSession(masteritemid, rentalitemid, internalchar, meter, toggledelete, containeritemid, containeroutcontractid,
                                                    function() {
                                                        $this.toggleClass('selected');
                                                        screen.pages.selectserialno.funcserialfrm(masteritemid);
                                                    }, function (response) {
                                                        $this.addClass('error');
                                                        $this.find('.errormessage').remove();
                                                        $this.append(jQuery('<div class="errormessage">').html(response.statusmessage));
                                                    }
                                                );
                                            }
                                        } catch(ex) {
                                            FwFunc.showError(ex);
                                        }
                                    })
                                ;
                                if ((response.funcserialmeterout[i].itemstatus === 'S') || (response.funcserialmeterout[i].itemstatus === 'O')) {
                                    $divSerialNo.addClass('selected');
                                }
                                $pageselectserialno.find('.items').append($divSerialNo);
                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    screen.pagehistory.push(screen.pages.selectserialno);
                    screen.pages.selectserialno.show(masterid, masteritemid, description, masterno, remaining, ordered, stagedandout);
                },
                back: function () {
                    screen.pagehistory.pop();
                    screen.getCurrentPage().show();
                },
                getMasterId: function() {
                    return screen.pages.selectserialno.masterid;
                },
                setMasterId: function(masterid) {
                    screen.pages.selectserialno.masterid = masterid;
                },
                getMasterItemId: function() {
                    return screen.pages.selectserialno.masteritemid;
                },
                setMasterItemId: function(masteritemid) {
                    screen.pages.selectserialno.masteritemid = masteritemid;
                },
                getMasterNo: function() {
                    return screen.pages.selectserialno.masterno;
                },
                setMasterNo: function(masterno) {
                    screen.pages.selectserialno.masterno = masterno;
                },
                getDescription: function() {
                    return screen.pages.selectserialno.description;
                },
                setDescription: function(description) {
                    screen.pages.selectserialno.description = description;
                },
                funcserialfrm: function(masteritemid) {
                    var $pageselectserialno = screen.pages.selectserialno.getElement();
                    var request = {
                        orderid:      screen.getOrderId(),
                        contractid:   screen.getContractId(),
                        masteritemid: masteritemid
                    };
                    RwServices.callMethod('Staging', 'funcserialfrm', request, function(response) {
                        try {
                            if (response !== null) {
                                screen.pages.selectserialno.setMasterId(response.funcserialfrm.masterid);
                                screen.pages.selectserialno.setMasterItemId(response.funcserialfrm.masteritemid);
                                screen.pages.selectserialno.setMasterNo(response.funcserialfrm.masterno);
                                screen.pages.selectserialno.setDescription(response.funcserialfrm.description);
                                if (response.funcserialfrm.metered === 'T') {
                                    screen.$modulecontrol.fwmobilemodulecontrol('showButton', '#selectserialno-setmeters');
                                }
                                var html = [];
                                html.push('<div class="row1">');
                                html.push('  <div class="masterno">' + RwLanguages.translate('I-Code') + ': ' + response.funcserialfrm.masterno + '</div><div class="description">' + response.funcserialfrm.description + '</div>');
                                html.push('</div>');
                                html.push('<div class="row2">');
                                html.push('  <div class="fsffield qtyremainingoutwsuspend"><div class="caption">' + RwLanguages.translate('Remaining') + ':</div><div class="value">' + response.funcserialfrm.qtyremainingoutwsuspend + '</div></div>');
                                html.push('  <div class="fsffield qtystaged"><div class="caption">' + RwLanguages.translate('Staged') + ':</div><div class="value">' + response.funcserialfrm.qtystaged + '</div></div>');
                                html.push('  <div class="fsffield qtyout"><div class="caption">' + RwLanguages.translate('Out') + ':</div><div class="value">' + response.funcserialfrm.qtyout + '</div></div>');
                                html.push('</div>');
                                html.push('<div class="row3">');
                                html.push('  <div class="fsffield qtyordered"><div class="caption">' + RwLanguages.translate('Ordered') + ':</div><div class="value">' + response.funcserialfrm.qtyordered + '</div></div>');
                                html.push('  <div class="fsffield subqty"><div class="caption">' + RwLanguages.translate('Sub') + ':</div><div class="value">' + response.funcserialfrm.subqty + '</div></div>');
                                html.push('  <div class="fsffield qtyin"><div class="caption">' + RwLanguages.translate('In') + ':</div><div class="value">' + response.funcserialfrm.qtyin + '</div></div>');
                                html.push('</div>');
                                $pageselectserialno.find('.info').html(html.join('\n'));
                            } else {
                                FwFunc.showMessage('Item not found!');
                                screen.pages.selectserialno.back();
                            }
                        
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            },
            serialmeters: {
                name: 'serialmeters',
                getElement: function () {
                    return screen.$view.find('.page-serialmeters');
                },
                show: function (masterid, masteritemid, description, masterno) {
                    screen.pages.reset();
                    screen.$modulecontrol.fwmobilemodulecontrol('changeState', this.name);
                    FwMobileMasterController.setTitle('Enter Meter Data...');
                    var $pageselectserialno = screen.pages.serialmeters.getElement();
                    $pageselectserialno.show();
                    $pageselectserialno.show();
                },
                forward: function(masterid, masteritemid, description, masterno) {
                    var $pageserialmeters = screen.pages.serialmeters.getElement();
                    $pageserialmeters.find('.info').html('<div class="masterno">' + RwLanguages.translate('I-Code') + ': ' + masterno + '</div><div class="description">' + description + '</div>');
                    $pageserialmeters.find('.items').empty();
                    var request = {
                        orderid:      screen.getOrderId(),
                        masterid:     masterid,
                        masteritemid: masteritemid,
                        onlystagedorout: true
                    };
                    RwServices.callMethod('Staging', 'funcserialmeterout', request, function(response) {
                        try {
                            for(var i = 0; i < response.funcserialmeterout.length; i++) {
                                var $divSerialNo = jQuery('<div>')
                                    .addClass('serialnorow')
                                    .attr('data-masteritemid', response.funcserialmeterout[i].masteritemid)
                                    .attr('data-rentalitemid', response.funcserialmeterout[i].rentalitemid)
                                    .data('recorddata', response.funcserialmeterout[i])
                                ;
                                if (response.funcserialmeterout[i].meterlast === response.funcserialmeterout[i].meterout) {
                                    $divSerialNo.attr('data-valueset', 'false');
                                } else {
                                    $divSerialNo.attr('data-valueset', 'true');
                                }
                                if ((response.funcserialmeterout[i].itemstatus === 'S') || (response.funcserialmeterout[i].itemstatus === 'O')) {
                                    $divSerialNo.addClass('selected');
                                }
                                $pageserialmeters.find('.items').append($divSerialNo);
                                var $serialinfo = jQuery('<div class="serialinfo">')
                                    .html('<div class="caption">Serial No:</div><div class="value">' + response.funcserialmeterout[i].mfgserial + '</div><div class="expander"><i class="material-icons">&#xE5CF;</i></div>') //expand_more
                                    .click(function() {
                                        try {
                                            var $this = jQuery(this);
                                            var $expander = $this.closest('.serialnorow').find('.expander');
                                            var $expandable = $this.closest('.serialnorow').find('.expandable');
                                            var $expandableisvisible = $expandable.is(':visible');
                                            $pageserialmeters.find('.expander').html('<i class="material-icons">&#xE5CF;</i>'); //expand_more
                                            $pageserialmeters.find('.expandable').hide();
                                            if ($expandableisvisible) {
                                                $expandable.hide();
                                                $expander.html('<i class="material-icons">&#xE5CF;</i>'); //expand_more
                                            } else {
                                                $expandable.show();
                                                $expander.html('<i class="material-icons">&#xE5CE;</i>'); //expand_less
                                            }
                                        } catch(ex) {
                                            FwFunc.showError(ex);
                                        }
                                    })
                                ;
                                $divSerialNo.append($serialinfo);
                                var $expandable = jQuery('<div class="expandable" style="display:none;">');
                                $divSerialNo.append($expandable);
                                var $row1 = jQuery('<div class="row1">');
                                var $row2 = jQuery('<div class="row2">');
                                $expandable.append($row1, $row2);
                                var $lastvalue = jQuery('<div class="psmfield lastvalue">');
                                $row1.append($lastvalue);
                                var $lastvaluecaption = jQuery('<div class="psmfield caption">');
                                $lastvaluecaption.text(RwLanguages.translate('Last Value') + ':');
                                var $lastvaluevalue = jQuery('<div class="psmfield value">').html(numberWithCommas(parseFloat(response.funcserialmeterout[i].meterlast).toFixed(2)));
                                $lastvalue.append($lastvaluecaption, $lastvaluevalue);
                                var $outvalue = jQuery('<div class="psmfield oldvalue">');
                                $row2.append($outvalue);
                                var $outvaluecaption = jQuery('<div class="psmfield caption">');
                                $outvaluecaption.text(RwLanguages.translate('Out Value') + ':');
                                var $outvaluevalue = jQuery('<div class="psmfield value">').html(numberWithCommas(parseFloat(response.funcserialmeterout[i].meterout).toFixed(2)));
                                $outvalue.append($outvaluecaption, $outvaluevalue);
                                $outvaluevalue.on('click', function () {
                                    try {
                                        var $confirmation, $ok, $cancel, html = [], $this;
                                        $this = jQuery(this);
                                        $confirmation = FwConfirmation.renderConfirmation('Enter Meter Out Value', '');
                                        $ok = FwConfirmation.addButton($confirmation, 'Ok', false);
                                        $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                                        html.push('<div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" style="color:#555555;" data-caption="Out Value" data-datafield="meteroutvalue" data-minvalue="' + $this.closest('.serialnorow').find('.lastvalue .value').html() + '" data-formatnumeric="true"></div>');
                                        FwConfirmation.addControls($confirmation, html.join(''));
                                        FwFormField.setValue($confirmation, 'div[data-datafield="meteroutvalue"]', parseFloat($this.html()));
                                        $ok.on('click', function () {
                                            try {
                                                var masteritemid           = $this.closest('.serialnorow').data('recorddata').masteritemid;
                                                var rentalitemid           = $this.closest('.serialnorow').data('recorddata').rentalitemid;
                                                var internalchar           = '';
                                                var meter                  = FwFormField.getValue($confirmation, 'div[data-datafield="meteroutvalue"]');
                                                var toggledelete           = false;
                                                var containeritemid        = '';
                                                var containeroutcontractid = '';
                                                screen.insertSerialSession(masteritemid, rentalitemid, internalchar, meter, toggledelete, containeritemid, containeroutcontractid, function funcOnSuccess() {
                                                    FwConfirmation.destroyConfirmation($confirmation);
                                                    $this.html(numberWithCommas(parseFloat(meter).toFixed(2)));
                                                    $this.closest('.serialnorow').attr('data-valueset', 'true');
                                                });
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        });
                                    } catch (ex) {
                                        FwFunc.showError(ex);
                                    }
                                });

                            }
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    screen.pagehistory.push(screen.pages.serialmeters);
                    screen.pages.serialmeters.show(masterid, masteritemid, description, masterno);
                },
                back: function () {
                    screen.pagehistory.pop();
                    screen.getCurrentPage().show();
                }
            }
        };

        screen.insertSerialSession = function(masteritemid, rentalitemid, internalchar, meter, toggledelete, containeritemid, containeroutcontractid, funcOnSuccess, funcOnError) {
            var request = {
                contractid:             screen.getContractId(), // mv 2016-12-13 this causes the serial item to immediately go into the OUT status rather than on suspended contract like I was expecting. RentalWorks is passing a blank for this.
                orderid:                screen.getOrderId(),
                masteritemid:           masteritemid,
                rentalitemid:           rentalitemid,
                internalchar:           internalchar,
                meter:                  meter,
                toggledelete:           toggledelete,
                containeritemid:        containeritemid,
                containeroutcontractid: containeroutcontractid
            };
            RwServices.callMethod('Staging', 'InsertSerialSession', request, function(response) {
                try {
                    if (response.status === 0) {
                        funcOnSuccess();
                    } else {
                        funcOnError(response);
                    }
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            }, true);
        };
    
        screen.toggleRfid = function () {
            if (screen.pagehistory.length > 0 && screen.getCurrentPage().name === 'staging') {
                if (RwRFID.isConnected) {
                    screen.$modulemodeselector.fwmobilemoduletabs('showTab', '#tabrfid');
                    var requestRFIDClear = {
                        sessionid: screen.getOrderId()
                    };
                    RwServices.order.rfidClearSession(requestRFIDClear, function(response) {});
                } else {
                    screen.$modulemodeselector.fwmobilemoduletabs('hideTab', '#tabrfid');
                    screen.$modulemodeselector.fwmobilemoduletabs('clickTab', '#tabpending');
                }
            }
        };

        screen.showPopupQty = function() {
            FwPopup.showPopup(screen.$popupQty);
        };
        screen.hidePopupQty = function() {
            FwPopup.destroyPopup(screen.$popupQty);
            jQuery('#scanBarcodeView-txtBarcodeData').val('');
        };
        screen.pdastageitemCallback = function(responseStageItem) {
            var $liPendingItem, parentid, $liParent, itemclass, $liChildren;
        
            screen.renderPopupQty();
            jQuery('#staging-popupQty')
                .data('responseStageItem',    responseStageItem)
                .data('code',                 responseStageItem.request.code)
                .data('masterno',             responseStageItem.webStageItem.masterNo)
                .data('masteritemid',         responseStageItem.webStageItem.masterItemId)
                .data('stageconsigned',       (typeof responseStageItem.request.stageconsigned === 'boolean') ? responseStageItem.request.stageconsigned : false)
                .data('consignorid',          responseStageItem.request.consignorid)
                .data('consignoragreementid', responseStageItem.request.consignoragreementid)
            ;

            // refresh the pending list if the Pending Tab is active
            if (responseStageItem.request.qty > 0) {
                if (screen.$modulemodeselector.fwmobilemoduletabs('isActive', '#tabpending')) {
                    screen.$modulemodeselector.fwmobilemoduletabs('clickTab', '#tabpending');
                }
                // refresh the staged list if the Staged Tab list is active
                else if (screen.$modulemodeselector.fwmobilemoduletabs('isActive', '#tabstaged')) {
                    screen.$modulemodeselector.fwmobilemoduletabs('clickTab', '#tabstaged')
                }
            }

            jQuery('#staging-popupQty-genericMsg')  .html(responseStageItem.webStageItem.genericMsg);
            jQuery('#staging-popupQty-msg')         .html(responseStageItem.webStageItem.msg);
            jQuery('#staging-popupQty-masterNo')    .html(responseStageItem.webStageItem.masterNo);
            jQuery('#staging-popupQty-description') .html(responseStageItem.webStageItem.description).show();
            jQuery('#staging-popupQty-code')        .html(responseStageItem.request.code).toggle(responseStageItem.request.code.length > 0);
            jQuery('#staging-popupQty-orderDesc')   .html(screen.getOrderDesc());
            jQuery('#staging-popupQty-qtyOrdered')  .html(String(responseStageItem.webStageItem.qtyOrdered));
            jQuery('#staging-popupQty-qtySub')      .html(String(responseStageItem.webStageItem.qtySub));
            jQuery('#staging-popupQty-qtyOut')      .html(String(responseStageItem.webStageItem.qtyOut));
            jQuery('#staging-popupQty-qtyIn')       .html(String(responseStageItem.webStageItem.qtyIn));
            jQuery('#staging-popupQty-qtyStaged')   .html(String(responseStageItem.webStageItem.qtyStaged));
            jQuery('#staging-popupQty-qtyRemaining').html(String(responseStageItem.webStageItem.qtyRemaining));
            if (responseStageItem.request.playStatus) {
                program.playStatus(responseStageItem.webStageItem.status === 0);
            }
            if (responseStageItem.webStageItem.status === 0) {
                jQuery('#staging-popupQty-genericMsg').removeClass('qserror').addClass('qssuccess');
                jQuery('#staging-popupQty-qty-txtQty')  .val(String(responseStageItem.webStageItem.qtyRemaining));
            } else {
                jQuery('#staging-popupQty-genericMsg').removeClass('qssuccess').addClass('qserror');
            }
            jQuery('#staging-popupQty-messages').toggle((applicationConfig.designMode) || ((responseStageItem.webStageItem.genericMsg.length > 0) || (responseStageItem.webStageItem.msg.length > 0)) && !((responseStageItem.webStageItem.isICode) && (responseStageItem.request.qty === 0)));  // hides the error so it doesn't show staged 0 message the first time you scan a qty item and it's prompting for the qty.
            jQuery('#staging-popupQty-genericMsg')    .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.genericMsg.length > 0));
            jQuery('#staging-popupQty-msg')           .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.msg.length > 0));
            jQuery('#staging-popupQty-qty')           .toggle((applicationConfig.designMode) || ((responseStageItem.request.qty === 0) && (responseStageItem.webStageItem.isICode) && !(responseStageItem.webStageItem.showAddCompleteToOrder || responseStageItem.webStageItem.showAddItemToOrder)));
            jQuery('#staging-popupQty-btnStageQtyItem').toggle(true);
            jQuery('#staging-popupQty-btnUnstageMasterItem').toggle(false);
            jQuery('#staging-popupQty-fields')        .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.masterNo.length > 0));
            jQuery('#staging-popupQty-pnlAddToOrder') .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showAddCompleteToOrder || responseStageItem.webStageItem.showAddItemToOrder || responseStageItem.webStageItem.showoverridereservation || responseStageItem.webStageItem.showstageconsigneditem || responseStageItem.webStageItem.showtransferrepair || responseStageItem.webStageItem.showaddcontainertoorder));
            jQuery('#staging-popupQty-btnAddComplete').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showAddCompleteToOrder));
            jQuery('#staging-popupQty-btnAddItem')    .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showAddItemToOrder));
            jQuery('#staging-popupQty-unstageBarcode').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showUnstage));
            jQuery('#staging-popupQty-btnOverrideAvailabilityReservation').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showoverridereservation));
            jQuery('#staging-popupQty-btnStageConsignedItem').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showstageconsigneditem));
            jQuery('#staging-popupQty-btnTransferRepair').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showtransferrepair));
            jQuery('#staging-popupQty-btnAddContainerToOrder').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showaddcontainertoorder));
            jQuery('#staging-popupQty-pnlSubstitute')  .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showsubstituteitem || responseStageItem.webStageItem.showsubstitutecomplete));
            jQuery('#staging-popupQty-btnSubItem')     .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showsubstituteitem));
            jQuery('#staging-popupQty-btnSubComplete') .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showsubstitutecomplete));

            if (!(responseStageItem.webStageItem.showsubstituteitem || responseStageItem.webStageItem.showsubstitutecomplete) && (responseStageItem.webStageItem.status === 0) && (responseStageItem.webStageItem.qtyRemaining !== 0)) {
                screen.$popupQty.find('#staging-popupQty-pnlSubstituteKit').show();
                screen.$popupQty.find('#staging-popupQty-btnSubstituteItem').on('click', function () {
                    $primarywindow.hide();
                    $substituteitemskit.showscreen(responseStageItem.webStageItem.masterId, responseStageItem.webStageItem.masterItemId, responseStageItem.webStageItem.masterNo, responseStageItem.webStageItem.description);
                    FwPopup.destroyPopup(screen.$popupQty);
                });
            }

            if (responseStageItem.webStageItem.status === 0) {
                if (responseStageItem.webStageItem.isIcode) {
                    screen.hidePopupQty();
                } else {
                    if (responseStageItem.request.qty === 0) {
                        screen.showPopupQty();
                    } else {
                        screen.showPopupQty();
                        //setTimeout(
                        //    function() {
                        //        screen.hidePopupQty();
                        //    }
                        //  , 3000);
                    }
                }
            } else {
                screen.showPopupQty();
            }
        };
        screen.getRowCountItem = function(count) {
            var li = [];
            li.push('<li class="normal lineitemcount">');
            li.push('  <div>' + count.toString() + ' ' +  RwLanguages.translate('line items') + '</div>');
            li.push('</li>');
            return li.join('');
        };
        screen.getStagingStagedItemsCallback = function(response) {
            var dt, ul, li, isHeaderRow, cssClass, availablefor;
        
            try {
                dt = response.getStagingStagedItems;
                ul = [];
                for (var i = 0; i < dt.Rows.length; i++) {
                    var qtysub    = dt.Rows[i][dt.ColumnIndex.qtysub];
                    var itemclass = dt.Rows[i][dt.ColumnIndex.itemclass];
                    var rectype   = dt.Rows[i][dt.ColumnIndex.rectype];
                    var qty = dt.Rows[i][dt.ColumnIndex.quantity];
                    li = [];
                    cssClass = '';
                    isHeaderRow = ((itemclass === 'N') || (qty === 0));
                    if (((itemclass !== 'NI') && (!isHeaderRow)) ||
                        ((itemclass === 'NI') && (i > 0) && (dt.Rows[i-1][dt.ColumnIndex.itemclass] === 'N')) 
                        // && (qtysub === 0) I think this should be here, right now subbed qty items aren't showing up on the staged tab, so this has to be commented out to workaround that issue
                        ) {
                        if (cssClass.length > 0) {
                            cssClass += ' ';
                        }
                        cssClass += 'link';
                    }
                    if (cssClass.length > 0) {
                        cssClass += ' ';
                    }
                    cssClass += 'itemclass-' + itemclass;
                    availablefor = '';
                         if (rectype === 'R') { availablefor = RwLanguages.translate('Rent'); }
                    else if (rectype === 'S') { availablefor = RwLanguages.translate('Sell'); }
                    else if (rectype === 'P') { availablefor = RwLanguages.translate('Parts'); }
                    li.push('<li class="' + cssClass +
                        '" data-code="' + dt.Rows[i][dt.ColumnIndex.barcode] + 
                        '" data-rectype="' + rectype + 
                        '" data-description="' + dt.Rows[i][dt.ColumnIndex.description] + 
                        '" data-masterid="' + dt.Rows[i][dt.ColumnIndex.masterid] + 
                        '" data-masterno="' + dt.Rows[i][dt.ColumnIndex.masterno] + 
                        '" data-masteritemid="' + dt.Rows[i][dt.ColumnIndex.masteritemid] + 
                        '" data-vendorid="' + dt.Rows[i][dt.ColumnIndex.vendorid] + 
                        '" data-quantity="' + dt.Rows[i][dt.ColumnIndex.quantity] + 
                        '" data-consignorid="' + dt.Rows[i][dt.ColumnIndex.consignorid] + 
                        '" data-consignoragreementid="' + dt.Rows[i][dt.ColumnIndex.consignoragreementid] + 
                        '" data-trackedby="' + dt.Rows[i][dt.ColumnIndex.trackedby] + '">');
                        li.push('<div class="description">' + dt.Rows[i][dt.ColumnIndex.description] + '</div>');
                        if (isHeaderRow) {
                            li.push('<table style="display:none;">');
                        } else {
                            li.push('<table>');
                        }
                            li.push('<tbody>');
                                li.push('<tr>');
                                    li.push('<td class="col1 key masterno">' + RwLanguages.translate('I-Code') + ':</td>');
                                    li.push('<td class="col2 value masterno">' + dt.Rows[i][dt.ColumnIndex.masterno] + '</td>');
                                    li.push('<td class="col3 key staged">' + RwLanguages.translate('Staged') + ':</td>');
                                    li.push('<td class="col4 value staged">' + String(dt.Rows[i][dt.ColumnIndex.quantity]) + '</td>');
                                li.push('</tr>');
                                if (dt.Rows[i][dt.ColumnIndex.rectype] !== 'R') {
                                    li.push('<tr>');
                                        li.push('<td class="col1 key rectype">' + RwLanguages.translate('Available For') + ':</td>');
                                        li.push('<td class="col2 value rectype">' + availablefor + '</td>');
                                    li.push('</tr>');
                                }
                                if (dt.Rows[i][dt.ColumnIndex.rectype] === 'R') {
                                    li.push('<tr>');
                                        li.push('<td class="col1 key trackedby">' + RwLanguages.translate('Tracked By') + ':</td>');
                                        li.push('<td class="col2 value trackedby">' + dt.Rows[i][dt.ColumnIndex.trackedby] + '</td>');
                                    li.push('</tr>');
                                }
                                if (dt.Rows[i][dt.ColumnIndex.barcode] !== '') {
                                    li.push('<tr>');
                                        var trackedby = dt.Rows[i][dt.ColumnIndex.trackedby];
                                        switch(trackedby) {
                                            case 'BARCODE':
                                                li.push('<td class="col1 key barcode">Barcode:</td>');
                                                break;
                                            case 'SERIALNO':
                                                li.push('<td class="col1 key barcode">Serial No:</td>');
                                                break;
                                            case 'RFID':
                                                li.push('<td class="col1 key barcode">RFID:</td>');
                                                break;
                                        }
                                    
                                        li.push('<td class="col2 value barcode">' + dt.Rows[i][dt.ColumnIndex.barcode] + '</td>');
                                    li.push('</tr>');
                                }
                                if (dt.Rows[i][dt.ColumnIndex.vendorid] !== '') {
                                    li.push('<tr>');
                                        li.push('<td class="col1 key vendor">' + RwLanguages.translate('Vendor/Consignor') + ':</td>');
                                        li.push('<td class="col234 value vendor" colspan="3">' + dt.Rows[i][dt.ColumnIndex.vendor] + '</td>');
                                    li.push('</tr>');
                                }
                            li.push('</tbody>');
                        li.push('</table>');
                    li.push('</li>');
                    ul.push(li.join(''));
                }
                ul.push(screen.getRowCountItem(dt.Rows.length));
                jQuery('#staging-stagedList-ul').html(ul.join('')) ;
                jQuery('#staging-stagedList-pnlCreateContract')
                    .toggle((applicationConfig.designMode) || ((sessionStorage.users_enablecreatecontract === 'T') && (dt.Rows.length > 0)));
            } catch(ex) {
                FwFunc.showError(ex);
            }
        };
        screen.unstageItemCallback = function(responseStageItem) {
            var $liPendingItem;
        
            screen.renderPopupQty();
            jQuery('#staging-popupQty')
                .data('code',                 responseStageItem.request.code)
                .data('masterno',             responseStageItem.webStageItem.masterNo)
                .data('masteritemid',         responseStageItem.webStageItem.masterItemId)
                .data('vendorid',             responseStageItem.request.vendorid)
                .data('consignorid',          responseStageItem.request.consignorid)
                .data('consignoragreementid', responseStageItem.request.consignoragreementid)
            ;
        
            if (screen.$modulemodeselector.fwmobilemoduletabs('isActive', '#tabpending')) {
                screen.$modulemodeselector.fwmobilemoduletabs('clickTab', '#tabpending');
                jQuery('#staging-pendingList-ul > li.lineitemcount').remove();
                jQuery('#staging-pendingList-ul').append(screen.getRowCountItem(jQuery('#staging-pendingList-ul > li').length));
            }

            jQuery('#staging-popupQty-genericMsg')  .html(responseStageItem.webStageItem.genericMsg);
            jQuery('#staging-popupQty-msg')         .html(responseStageItem.webStageItem.msg);
            jQuery('#staging-popupQty-masterNo')    .html(responseStageItem.webStageItem.masterNo);
            jQuery('#staging-popupQty-description') .html(responseStageItem.webStageItem.description).show();
            jQuery('#staging-popupQty-orderDesc')   .html(screen.getOrderDesc());
            jQuery('#staging-popupQty-qtyOrdered')  .html(String(responseStageItem.webStageItem.qtyOrdered));
            jQuery('#staging-popupQty-qtySub')      .html(String(responseStageItem.webStageItem.qtySub));
            jQuery('#staging-popupQty-qtyOut')      .html(String(responseStageItem.webStageItem.qtyOut));
            jQuery('#staging-popupQty-qtyIn')       .html(String(responseStageItem.webStageItem.qtyIn));
            jQuery('#staging-popupQty-qtyStaged')   .html(String(responseStageItem.webStageItem.qtyStaged));
            jQuery('#staging-popupQty-qtyRemaining').html(String(responseStageItem.webStageItem.qtyRemaining));
            if (responseStageItem.request.playStatus) {
                program.playStatus(responseStageItem.webStageItem.status === 0);
            }
            if (responseStageItem.webStageItem.status === 0) {
                jQuery('#staging-popupQty-genericMsg').removeClass('qserror').addClass('qssuccess');
                var txtQtyVal = (typeof responseStageItem.unstageqty === 'number') ? String(responseStageItem.unstageqty) : String(responseStageItem.webStageItem.qtyRemaining);
                jQuery('#staging-popupQty-qty-txtQty')  .val(txtQtyVal);
            } else {
                jQuery('#staging-popupQty-genericMsg').removeClass('qssuccess').addClass('qserror');
            }
            jQuery('#staging-popupQty-messages').toggle((applicationConfig.designMode) || ((responseStageItem.webStageItem.genericMsg.length > 0) || (responseStageItem.webStageItem.msg.length > 0)) && !((responseStageItem.webStageItem.isICode) && (responseStageItem.request.qty === 0)));  // hides the error so it doesn't show staged 0 message the first time you scan a qty item and it's prompting for the qty.
            jQuery('#staging-popupQty-genericMsg')    .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.genericMsg.length > 0));
            jQuery('#staging-popupQty-msg')           .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.msg.length > 0));
            jQuery('#staging-popupQty-qty')           .toggle((applicationConfig.designMode) || ((responseStageItem.request.qty === 0) && (responseStageItem.webStageItem.isICode) && !(responseStageItem.webStageItem.showAddCompleteToOrder || responseStageItem.webStageItem.showAddItemToOrder)));
            jQuery('#staging-popupQty-btnStageQtyItem').toggle(false);
            jQuery('#staging-popupQty-btnUnstageMasterItem').toggle(true);
            jQuery('#staging-popupQty-fields')        .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.masterNo.length > 0));
            jQuery('#staging-popupQty-pnlAddToOrder') .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showAddCompleteToOrder || responseStageItem.webStageItem.showAddItemToOrder || responseStageItem.webStageItem.showoverridereservation || responseStageItem.webStageItem.showstageconsigneditem || responseStageItem.webStageItem.showtransferrepair || responseStageItem.webStageItem.showaddcontainertoorder));
            jQuery('#staging-popupQty-btnAddComplete').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showAddCompleteToOrder));
            jQuery('#staging-popupQty-btnAddItem')    .toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showAddItemToOrder));
            jQuery('#staging-popupQty-unstageBarcode').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showUnstage));
            jQuery('#staging-popupQty-btnOverrideAvailabilityReservation').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showoverridereservation));
            jQuery('#staging-popupQty-btnStageConsignedItem').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showstageconsigneditem));
            jQuery('#staging-popupQty-btnTransferRepair').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showtransferrepair));
            jQuery('#staging-popupQty-btnAddContainerToOrder').toggle((applicationConfig.designMode) || (responseStageItem.webStageItem.showaddcontainertoorder));
            jQuery('#staging-popupQty-pnlSubstitute')  .toggle((applicationConfig.designMode) || false);
            if (responseStageItem.webStageItem.status === 0) {
                if (responseStageItem.webStageItem.masterNo.length === 0) {
                    screen.hidePopupQty();
                } else {
                    if (responseStageItem.request.qty === 0) {
                        screen.showPopupQty();
                    } else {
                        screen.hidePopupQty();
                    }
                }
            } else {
                screen.showPopupQty();
            }
        };
        screen.rfiditem = function(itemtype) {
            var html;
            html = [];
            html.push('<div class="rfid-item ' + itemtype + '">');
                html.push('<div class="rfid-item-title"></div>');
                html.push('<div class="rfid-item-info">');
                    if (itemtype === 'processed' || itemtype === 'exception') {
                        html.push('<div class="rfid-data rfid">');
                            html.push('<div class="item-caption">RFID:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                        html.push('<div class="rfid-data barcode">');
                            html.push('<div class="item-caption">Barcode:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                        html.push('<div class="rfid-data serial">');
                            html.push('<div class="item-caption">Serial No:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                    }
                    if (itemtype === 'exception') {
                        html.push('<div class="rfid-data message">');
                            html.push('<div class="item-caption">Message:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                    }
                    if (itemtype === 'pending') {
                        html.push('<div class="rfid-data ordered">');
                            html.push('<div class="item-caption">Ordered:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                        html.push('<div class="rfid-data staged">');
                            html.push('<div class="item-caption">Staged/Out:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                        html.push('<div class="rfid-data remaining">');
                            html.push('<div class="item-caption">Remaining:</div>');
                            html.push('<div class="item-value"></div>');
                        html.push('</div>');
                    }
                html.push('</div>');
            html.push('</div>');
            return jQuery(html.join(''));
        };
        screen.rfidscan = function(epcs) {
            var request;
            if (epcs !== '') {
                screen.$view.find('.rfid-rfidstatus').show();
                screen.$view.find('.rfid-items').empty();
                screen.$view.find('.rfid-placeholder').show();
                screen.$view.find('.rfid-rfidstatus .processed .statusitem-value').html('0');
                screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
                screen.$view.find('.rfid-rfidbuttons .btnpending').hide();
                screen.$view.find('.rfid-rfidbuttons .btnexception').hide();
                screen.$view.find('.rfid-rfidbuttons .btnstaging').hide();
                request = {
                    rfidmode:  'STAGING',
                    sessionid: screen.getOrderId(),
                    portal:    device.uuid,
                    tags:      epcs
                };
                RwServices.callMethod("Staging", "RFIDScan", request, function(response) {
                    var $item;

                    for (var i = 0; i < response.processed.length; i++) {
                        $item = screen.rfiditem('processed');
                        $item.find('.rfid-item-title').html(response.processed[i].title);
                        $item.find('.rfid-data.rfid .item-value').html(response.processed[i].rfid);
                        $item.find('.rfid-data.barcode .item-value').html((response.processed[i].barcode !== '') ? response.processed[i].barcode : '-');
                        $item.find('.rfid-data.serial .item-value').html((response.processed[i].serialno !== '') ? response.processed[i].serialno : '-');
                        if (response.processed[i].duplicatescan === 'T') {
                            $item.addClass('duplicate');
                        }

                        screen.$view.find('.rfid-items').append($item);
                    }

                    screen.$view.find('.rfid-rfidstatus .processed .statusitem-value').html(response.processed.length);
                    screen.$view.find('.rfid-rfidstatus .exceptions .statusitem-value').html(response.exceptions.length);
                    screen.$view.find('.rfid-rfidstatus .pending .statusitem-value').html(response.pending.length);
                    (response.exceptions.length > 0) ? screen.$view.find('.btnexception').show() : screen.$view.find('.btnexception').hide();
                    (response.pending.length > 0)    ? screen.$view.find('.btnpending').show()   : screen.$view.find('.btnpending').hide();
                    (response.processed.length > 0)  ? screen.$view.find('.btnclear').show()     : screen.$view.find('.btnclear').hide();
                    if (response.processed.length > 0) {
                        screen.$view.find('.rfid-placeholder').hide();
                    }
                });
            }
        };
        screen.exceptionconfirmation = function(exceptiontype, exceptionmessage, rfid) {
            var $confirmation, $cancel, request: any = {};
            $confirmation = FwConfirmation.renderConfirmation('Exception', '<div class="exceptionmessage"></div><div class="exceptionbuttons"></div>');
            $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);
            switch (exceptiontype) {
                case '207': //Item count exceeds quantity ordered - Item
                case '209': //Item not on order - Item
                    $confirmation.find('.exceptionbuttons').append('<div class="additem">Add item to order?</div>');
                    break;
                case '208': //Item count exceeds quantity ordered - Item/Complete
                case '210': //Item not on order - Item/Complete
                    $confirmation.find('.exceptionbuttons')
                        .append('<div class="additem">Add item to order?</div>')
                        .append('<div class="addcomplete">Add complete to order?</div>')
                    ;
                    break;
                case '215':
                    $confirmation.find('.exceptionbuttons').append('<div class="overrideconflict">Override availability conflict?</div>');
                    break;
                case '110': //Item in repair can transfer
                    $confirmation.find('.exceptionbuttons').append('<div class="transferiteminrepair">Transfer item in repair?</div>');
                    break;
                case '100': //Item is in repair
                    $confirmation.find('.exceptionbuttons').append('<div class="releaseandstage">Release and stage?</div>');
                    break;
                case '104': //Item is Staged on Order
                case '301': //I-Code / Bar Code not found in Inventory.
                    break;
            }
            $confirmation.find('.exceptionbuttons').append('<div class="tagfinder">Tag Finder</div>');
            $confirmation.find('.exceptionbuttons').append('<div class="clear">Clear item?</div>');
            $confirmation.find('.exceptionmessage').append(exceptionmessage);

            $confirmation.find('.exceptionbuttons')
                .on('click', '.tagfinder', function () {
                    try {
                        FwConfirmation.destroyConfirmation($confirmation);
                        RwRFID.startTagFinder(rfid);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '.additem, .addcomplete, .overrideconflict, .releaseandstage, .clear', function () {
                    if (jQuery(this).hasClass('additem')) {
                        request.method = 'AddItem';
                    } else if (jQuery(this).hasClass('addcomplete')) {
                        request.method = 'AddComplete';
                    } else if (jQuery(this).hasClass('overrideconflict')) {
                        request.method = 'OverrideConflict';
                    } else if (jQuery(this).hasClass('transferiteminrepair')) {
                        request.method = 'TransferItemInRepair';
                    } else if (jQuery(this).hasClass('releaseandstage')) {
                        request.method = 'ReleaseAndStage';
                    } else if (jQuery(this).hasClass('clear')) {
                        request.method = 'Clear';
                    }
                    request.sessionid    = screen.getOrderId();
                    request.rfid         = rfid;
                    request.portal       = device.uuid;
                    RwServices.order.processrfidexception(request, function(response) {
                        if ((request.method === 'ReleaseAndStage') && (response.process.status !== 0)) {
                            screen.exceptionconfirmation(response.process.status.toString(), response.process.msg, request.rfid);
                        } else {
                            screen.$view.find('.rfid-items').empty();
                            for (var i = 0; i < response.exceptions.length; i++) {
                                const $item = screen.rfiditem('exception');
                                $item.find('.rfid-item-title').html(response.exceptions[i].title);
                                $item.find('.rfid-data.rfid .item-value').html(response.exceptions[i].rfid);
                                $item.find('.rfid-data.barcode .item-value').html((response.exceptions[i].barcode !== '') ? response.exceptions[i].barcode : '-');
                                $item.find('.rfid-data.serial .item-value').html((response.exceptions[i].serialno !== '') ? response.exceptions[i].serialno : '-');
                                $item.find('.rfid-data.message .item-value').html(response.exceptions[i].message);
                                $item.attr('data-exceptiontype', response.exceptions[i].exceptiontype);
                                screen.$view.find('.rfid-items').append($item);
                            }
                        }
                    });
                    FwConfirmation.destroyConfirmation($confirmation);
                })
            ;
        };

        screen.scanCode = function (code: string) {
            var strippedCode = RwAppData.stripBarcode(code.toUpperCase());
            if (screen.getCurrentPage().name === 'search') {
                screen.$search.fwmobilesearch('setsearchmode', 'orderno');
                screen.$search.fwmobilesearch('setSearchText', strippedCode, true);
            } else if (screen.getCurrentPage().name === 'staging') {
                const scanMode = jQuery('#staging-scan').attr('data-mode');
                if (scanMode === 'PENDING') {
                    screen.$view.find('#pendingsearch').fwmobilesearch('setsearchmode', 'code');
                    screen.$view.find('#pendingsearch').fwmobilesearch('setSearchText', strippedCode, false);
                } else if (scanMode === 'STAGEDLIST') {
                    screen.$view.find('#stagedsearch').fwmobilesearch('setsearchmode', 'code');
                    screen.$view.find('#stagedsearch').fwmobilesearch('setSearchText', strippedCode, false);
                }
                if (strippedCode.length > 0) {
                    var requestStageItem = {
                        orderid: screen.getOrderId(),
                        code: strippedCode,
                        masteritemid: '',
                        qty: 0,
                        additemtoorder: false,
                        addcompletetoorder: false,
                        releasefromrepair: false,
                        unstage: false,
                        vendorid: '',
                        meter: 0,
                        location: '',
                        locationdata: screen._locationdata(),
                        addcontainertoorder: false,
                        overridereservation: false,
                        stageconsigned: false,
                        transferrepair: false,
                        removefromcontainer: false,
                        contractid: screen.getContractId(),
                        ignoresuspendedin: false,
                        consignorid: '',
                        consignoragreementid: '',
                        playStatus: true
                    };

                    FwAppData.jsonPost(true, 'api/v1/mobile?path=/order/pdastageitem', requestStageItem, null,
                        function success(responseStageItem) {
                            properties.responseStageItem = responseStageItem;
                            screen.pdastageitemCallback(responseStageItem);
                        },
                        function fail(response) {
                            FwFunc.showError(response);
                        },
                        null);
                }
            }
        };

        screen.$view
            .on('click', '.rfid-item', function() {
                var $this, $selectedrfiditem;
                $this             = jQuery(this);
                $selectedrfiditem = screen.$view.find('.rfid-item.selected');
                if (typeof $selectedrfiditem !== 'undefined') {
                    $selectedrfiditem.removeClass('selected');
                }
                $this.addClass('selected');
                if ($this.hasClass('exception')) {
                    screen.exceptionconfirmation($this.attr('data-exceptiontype'), $this.find('.rfid-data.message .item-value').html(), $this.find('.rfid-data.rfid .item-value').html());
                }
            })
            .on('click', '.rfid-rfidbuttons .btnclear', function() {
                screen.$view.find('.rfid-items').empty();
                screen.$view.find('.rfid-placeholder').show();
                screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
                screen.$view.find('.rfid-rfidstatus .processed .statusitem-value').html('0');
            })
            .on('click', '.rfid-rfidbuttons .btnpending', function() {
                var $item, request;
                screen.$view.find('.rfid-rfidstatus').hide();
                screen.$view.find('.rfid-items').empty();
                screen.$view.find('.rfid-placeholder').hide();
                screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
                screen.$view.find('.rfid-rfidbuttons .btnpending').hide();
                screen.$view.find('.rfid-rfidbuttons .btnexception').hide();
                screen.$view.find('.rfid-rfidbuttons .btnstaging').show();

                request = {
                    rfidmode:  'STAGING',
                    sessionid: screen.getOrderId()
                };
                RwServices.order.loadRFIDPending(request, function(response) {
                    for (var i = 0; i < response.pending.length; i++) {
                        $item = screen.rfiditem('pending');
                        $item.find('.rfid-item-title').html(response.pending[i].description);
                        $item.find('.rfid-data.ordered .item-value').html(response.pending[i].qtyordered);
                        $item.find('.rfid-data.staged .item-value').html(response.pending[i].qtystagedandout);
                        $item.find('.rfid-data.remaining .item-value').html(response.pending[i].qtyordered - response.pending[i].qtystagedandout);
                        screen.$view.find('.rfid-items').append($item);
                    }
                });
            })
            .on('click', '.rfid-rfidbuttons .btnexception', function() {
                var $item, request;
                screen.$view.find('.rfid-rfidstatus').hide();
                screen.$view.find('.rfid-items').empty();
                screen.$view.find('.rfid-placeholder').hide();
                screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
                screen.$view.find('.rfid-rfidbuttons .btnpending').hide();
                screen.$view.find('.rfid-rfidbuttons .btnexception').hide();
                screen.$view.find('.rfid-rfidbuttons .btnstaging').show();

                request = {
                    sessionid: screen.getOrderId(),
                    portal:    device.uuid
                };
                RwServices.order.loadRFIDExceptions(request, function(response) {
                    for (var i = 0; i < response.exceptions.length; i++) {
                        $item = screen.rfiditem('exception');
                        $item.find('.rfid-item-title').html(response.exceptions[i].title);
                        $item.find('.rfid-data.rfid .item-value').html(response.exceptions[i].rfid);
                        $item.find('.rfid-data.barcode .item-value').html((response.exceptions[i].barcode !== '') ? response.exceptions[i].barcode : '-');
                        $item.find('.rfid-data.serial .item-value').html((response.exceptions[i].serialno !== '') ? response.exceptions[i].serialno : '-');
                        $item.find('.rfid-data.message .item-value').html(response.exceptions[i].message);
                        $item.attr('data-exceptiontype', response.exceptions[i].exceptiontype);
                        screen.$view.find('.rfid-items').append($item);
                    }
                });
            })
            .on('click', '.rfid-rfidbuttons .btnstaging', function() {
                var request;
                screen.$view.find('.rfid-rfidstatus').show();
                screen.$view.find('.rfid-items').empty();
                screen.$view.find('.rfid-placeholder').show();
                screen.$view.find('.rfid-rfidstatus .processed .statusitem-value').html('0');
                screen.$view.find('.rfid-rfidbuttons .btnclear').hide();
                screen.$view.find('.rfid-rfidbuttons .btnpending').hide();
                screen.$view.find('.rfid-rfidbuttons .btnexception').hide();
                screen.$view.find('.rfid-rfidbuttons .btnstaging').hide();

                request = {
                    rfidmode:  'STAGING',
                    sessionid: screen.getOrderId(),
                    portal:    device.uuid
                };
                RwServices.order.getRFIDStatus(request, function(response) {
                    screen.$view.find('.rfid-rfidstatus .exceptions .statusitem-value').html(response.exceptions.length);
                    screen.$view.find('.rfid-rfidstatus .pending .statusitem-value').html(response.pending.length);
                    (response.exceptions.length > 0) ? screen.$view.find('.btnexception').show() : screen.$view.find('.btnexception').hide();
                    (response.pending.length > 0)    ? screen.$view.find('.btnpending').show()   : screen.$view.find('.btnpending').show();
                });
            })
            .on('click', '.orderlocationclear', function() {
                screen.$view.find('#staging-bottomtray .orderlocation').remove();
                screen.refreshbottomspacer();

                locationdata = null;
            })
        ;

        screen.displayOrderLocation = function (orderlocation) {
            screen.$view.find('#staging-bottomtray .orderlocation').remove();

            let html = `<div class="orderlocation">
                            <div class="item"><div class="caption">Location:</div><div class="value">${orderlocation}</div></div>
                            <div class="orderlocationclear btnclear">Clear</div>
                        </div>`;
            screen.$view.find('#staging-bottomtray').append(html);

            screen.refreshbottomspacer();
        };
        screen.refreshbottomspacer = function () {
            screen.$view.find('#staging-bottomspacer').css('height', screen.$view.find('#staging-bottomtray').height());
        };

        screen.renderPopupQty = function() {
            var template = Mustache.render(jQuery('#tmpl-Staging-PopupQty').html(), {
                    captionStage:              RwLanguages.translate('Stage')
                  , captionUnstage:            RwLanguages.translate('Unstage')
                  , captionCancel:             RwLanguages.translate('Cancel')
                  , captionUnstageMode:        RwLanguages.translate('Mode')
                  , captionResponsiblePerson:  RwLanguages.translate('Responsible Person')
                  , valueTxtQty:               ''
                  , captionQty:                RwLanguages.translate('Qty')
                  , captionSummary:            RwLanguages.translate('Summary')
                  , captionOrdered:            RwLanguages.translate('Ordered')
                  , captionICodeDesc:          RwLanguages.translate('I-Code')
                  , captionSub:                RwLanguages.translate('Sub')
                  , captionOut:                RwLanguages.translate('Out')
                  , captionIn:                 RwLanguages.translate('In')
                  , captionStaged:             RwLanguages.translate('Staged')
                  , captionRemaining:          RwLanguages.translate('Remaining')
                  , captionAddToOrder:         RwLanguages.translate('Would you like to add this to the order?')
                  , captionAddComplete:        RwLanguages.translate('Add Complete')
                  , captionAddItem:            RwLanguages.translate('Add Item')
                  , captionYes:                RwLanguages.translate('Yes')
                  , captionDesc:               RwLanguages.translate('Description')
                  , captionPendingListButton:  RwLanguages.translate('Pending')
                  , captionScanButton:         RwLanguages.translate('Scan')
                  , captionRFIDButton:         RwLanguages.translate('RFID')
                  , captionStagedListButton:   RwLanguages.translate('Staged')
                  , captionCreateContract:     RwLanguages.translate('Create Contract')
                  , captionSerialNoSelection:  RwLanguages.translate('Serial No. Selection')
                  , captionApplyAllQtyItems:   RwLanguages.translate('Apply All Qty Items')
                  , captionOverrideAvailabilityReservation: RwLanguages.translate('Override Availability Reservation')
                  , captionStageConsignedItem: RwLanguages.translate('Stage Consigned Item')
                  , captionTransferRepair:     RwLanguages.translate('Transfer Repair')
                  , captionAddContainerToOrder:RwLanguages.translate('Add Container to Order')
                  , captionSubItem:            RwLanguages.translate('Substitute Item')
                  , captionSubComplete:        RwLanguages.translate('Substitute Complete')
                  , captionSelectSerialNo:     RwLanguages.translate('Select Serial No(s)')
            });
            var $popupcontent = jQuery(template);
            if (typeof screen.$popupQty === 'object' && screen.$popupQty.length > 0) {
                FwPopup.destroyPopup(screen.$popupQty);
            }
            screen.$popupQty = FwPopup.renderPopup($popupcontent, {ismodal:false});
            screen.$popupQty.find('#staging-popupQty-messages').hide();
            screen.$popupQty.find('#staging-popupQty-fields').hide();
            screen.$popupQty.find('#staging-popupQty-pnlAddToOrder').hide();
            screen.$popupQty.find('#staging-popupQty-pnlSubstitute').hide();
            screen.$popupQty.find('#staging-popupQty-unstageBarcode').hide();
            screen.$popupQty.find('#staging-popupQty-qty').hide();
            screen.$popupQty.find('#staging-popupQty-pnlSelectSerialNo').hide();
            screen.$popupQty.find('#staging-popupQty-pnlSubstituteKit').hide();
            FwPopup.showPopup(screen.$popupQty);
            screen.$popupQty
                .on('click', '#staging-popupQty-btnUnstageBarcode', function() {
                    var qty, requestStageItem;
                    try {
                        requestStageItem = {
                            orderid:               screen.getOrderId(),
                            code:                  RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code')),
                            masteritemid:          jQuery('#staging-popupQty').data('masteritemid'),
                            qty:                   0,
                            additemtoorder:        false,
                            addcompletetoorder:    false,
                            releasefromrepair:     false,
                            unstage:               true,
                            vendorid:              '',
                            meter:                 0,
                            location:              '',
                            locationdata:          screen._locationdata(),
                            addcontainertoorder:   false,
                            overridereservation:   false,
                            stageconsigned:        false,
                            transferrepair:        false,
                            removefromcontainer:   false,
                            contractid:            screen.getContractId(),
                            ignoresuspendedin:     false,
                            consignorid:           '',
                            consignoragreementid:  '',
                            playStatus:            false,
                            trackedBy:             'BARCODE'
                        };
                        RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                            try {
                                screen.pdastageitemCallback(responseStageItem);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#quote-btnSubtract', function() {
                    var quantity;
                    quantity = Number(jQuery('#staging-popupQty-qty-txtQty').val()) - 1;
                    if (quantity >= 0) {
                        jQuery('#staging-popupQty-qty-txtQty').val(quantity);
                    }
                })
                .on('click', '#quote-btnAdd', function() {
                    var quantity;
                    quantity = Number(jQuery('#staging-popupQty-qty-txtQty').val()) + 1;
                    jQuery('#staging-popupQty-qty-txtQty').val(quantity);
                })
                .on('click', '#staging-popupQty-btnCancel', function() {
                    FwPopup.destroyPopup(screen.$popupQty);
                })
                .on('click', '#staging-popupQty-btnStageQtyItem', function() {
                    var qty, requestStageItem;
                    try {
                        qty = parseFloat(<string>jQuery('#staging-popupQty-qty-txtQty').val());
                        if (isNaN(qty)) {
                            FwFunc.showError('Qty is required.');
                        } else {
                            requestStageItem = {
                                orderid:               screen.getOrderId(),
                                code:                  RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code')),
                                masteritemid:          jQuery('#staging-popupQty').data('masteritemid'),
                                qty:                   qty,
                                additemtoorder:        false,
                                addcompletetoorder:    false,
                                releasefromrepair:     false,
                                unstage:               false,
                                vendorid:              '',
                                meter:                 0,
                                location:              '',
                                locationdata:          screen._locationdata(),
                                addcontainertoorder:   false,
                                overridereservation:   false,
                                stageconsigned:        false,
                                transferrepair:        false,
                                removefromcontainer:   false,
                                contractid:            screen.getContractId(),
                                ignoresuspendedin:     false,
                                consignorid:           jQuery('#staging-popupQty').data('consignorid'),
                                consignoragreementid:  jQuery('#staging-popupQty').data('consignoragreementid'),
                                playStatus:            false,
                                trackedBy:             'QUANTITY'
                            };
                            RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                                try {
                                    screen.pdastageitemCallback(responseStageItem);
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#staging-popupQty-btnUnstageQtyItem', function() {
                    var qty, requestStageItem;
                    try {
                        qty = parseFloat(<string>jQuery('#staging-popupQty-qty-txtQty').val());
                        if (isNaN(qty)) {
                            FwFunc.showError('Qty is required.');
                        } else {
                            requestStageItem = {
                                orderid:               screen.getOrderId(),
                                code:                  RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code')),
                                masteritemid:          jQuery('#staging-popupQty').data('masteritemid'),
                                qty:                   qty,
                                additemtoorder:        false,
                                addcompletetoorder:    false,
                                releasefromrepair:     false,
                                unstage:               true,
                                vendorid:              jQuery('#staging-popupQty').data('vendorid'),
                                meter:                 0,
                                location:              '',
                                locationdata:          screen._locationdata(),
                                addcontainertoorder:   false,
                                overridereservation:   false,
                                stageconsigned:        false,
                                transferrepair:        false,
                                removefromcontainer:   false,
                                contractid:            screen.getContractId(),
                                ignoresuspendedin:     false,
                                consignorid:           jQuery('#staging-popupQty').data('consignorid'),
                                consignoragreementid:  jQuery('#staging-popupQty').data('consignoragreementid'),
                                playStatus:            false,
                                trackedBy:             'QUANTITY'
                            };
                            RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                                try {
                                    screen.pdastageitemCallback(responseStageItem);
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#staging-popupQty-btnAddComplete', function() {
                    var qty, requestStageItem;
                    try {
                        qty = parseFloat(jQuery('#staging-popupQty').data('responseStageItem').request.qty);
                        if (isNaN(qty)) {
                            qty = 0;
                        }
                        requestStageItem = {
                            orderid:               screen.getOrderId(),
                            code:                  RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code')),
                            masteritemid:          jQuery('#staging-popupQty').data('masteritemid'),
                            qty:                   qty,
                            additemtoorder:        false,
                            addcompletetoorder:    true,
                            releasefromrepair:     false,
                            unstage:               false,
                            vendorid:              '',
                            meter:                 0,
                            location:              '',
                            locationdata:          screen._locationdata(),
                            addcontainertoorder:   false,
                            overridereservation:   false,
                            stageconsigned:        (typeof jQuery('#staging-popupQty').data('stageconsigned') === 'boolean') ? jQuery('#staging-popupQty').data('stageconsigned') : false,
                            transferrepair:        false,
                            removefromcontainer:   false,
                            contractid:            screen.getContractId(),
                            ignoresuspendedin:     false,
                            consignorid:           jQuery('#staging-popupQty').data('consignorid'),
                            consignoragreementid:  jQuery('#staging-popupQty').data('consignoragreementid')
                        };
                        RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                            try {
                                screen.pdastageitemCallback(responseStageItem);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#staging-popupQty-btnAddItem', function() {
                    var qty, requestStageItem;
                    try {
                        qty = parseFloat(jQuery('#staging-popupQty').data('responseStageItem').request.qty);
                        if (isNaN(qty)) {
                            qty = 0;
                        }
                        requestStageItem = {
                            orderid:               screen.getOrderId(),
                            code:                  RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code')),
                            masteritemid:          jQuery('#staging-popupQty').data('masteritemid'),
                            qty:                   qty,
                            additemtoorder:        true,
                            addcompletetoorder:    false,
                            releasefromrepair:     false,
                            unstage:               false,
                            vendorid:              '',
                            meter:                 0,
                            location:              '',
                            locationdata:          screen._locationdata(),
                            addcontainertoorder:   false,
                            overridereservation:   false,
                            stageconsigned:        (typeof jQuery('#staging-popupQty').data('stageconsigned') === 'boolean') ? jQuery('#staging-popupQty').data('stageconsigned') : false,
                            transferrepair:        false,
                            removefromcontainer:   false,
                            contractid:            screen.getContractId(),
                            ignoresuspendedin:     false,
                            consignorid:           jQuery('#staging-popupQty').data('consignorid'),
                            consignoragreementid:  jQuery('#staging-popupQty').data('consignoragreementid')
                        };
                        RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                            try {
                                screen.pdastageitemCallback(responseStageItem);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#staging-popupQty-btnUnstageMasterItem', function() {
                    var request, $this, orderid, masteritemid, contractid, vendorid, qty, trackedby;
                    try {
                        $this        = jQuery(this);
                        orderid      = screen.getOrderId();
                        masteritemid = jQuery('#staging-popupQty').data('masteritemid');
                        contractid   = screen.getContractId();
                        vendorid     = jQuery('#staging-popupQty').data('vendorid');
                        qty          = parseFloat(<string>jQuery('#staging-popupQty-qty-txtQty').val());
                        trackedby    = $this.data('trackedby');
                        if (isNaN(qty)) {
                            throw 'Please enter a valid quantity.';
                        }
                        request = {
                            orderid:       orderid,
                            masteritemid:  masteritemid,
                            vendorid:      vendorid,
                            contractid:    contractid,
                            qty:           qty
                        };
                        if ((trackedby === 'BARCODE') || (trackedby === 'SERIALNO')) {
                            qty = 1;
                        }
                        RwServices.order.unstageItem(request, function(response) {
                            try {
                                screen.hidePopupQty();
                                if (response.unstageItem.status !== 0) throw response.unstageItem.msg;
                                screen.getStagingStagedItemsCallback(response);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#staging-popupQty-btnOverrideAvailabilityReservation', function() {
                    var qty, requestStageItem;
                    try {
                        qty = parseFloat(jQuery('#staging-popupQty').data('responseStageItem').request.qty);
                        if (isNaN(qty)) {
                            qty = 0;
                        }
                        requestStageItem = {
                            orderid:               screen.getOrderId(),
                            code:                  RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code')),
                            masteritemid:          jQuery('#staging-popupQty').data('masteritemid'),
                            qty:                   qty,
                            additemtoorder:        false,
                            addcompletetoorder:    false,
                            releasefromrepair:     false,
                            unstage:               false,
                            vendorid:              '',
                            meter:                 0,
                            location:              '',
                            locationdata:          screen._locationdata(),
                            addcontainertoorder:   false,
                            overridereservation:   true,
                            stageconsigned:        (typeof jQuery('#staging-popupQty').data('stageconsigned') === 'boolean') ? jQuery('#staging-popupQty').data('stageconsigned') : false,
                            transferrepair:        false,
                            removefromcontainer:   false,
                            contractid:            screen.getContractId(),
                            ignoresuspendedin:     false,
                            consignorid:           jQuery('#staging-popupQty').data('consignorid'),
                            consignoragreementid:  jQuery('#staging-popupQty').data('consignoragreementid')
                        };
                        RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                            try {
                                screen.pdastageitemCallback(responseStageItem);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#staging-popupQty-btnStageConsignedItem', function() {
                    var qty, requestStageItem;
                    try {
                        qty = parseFloat(jQuery('#staging-popupQty').data('responseStageItem').request.qty);
                        if (isNaN(qty)) {
                            qty = 0;
                        }
                        requestStageItem = {
                            orderid:               screen.getOrderId(),
                            code:                  RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code')),
                            masteritemid:          jQuery('#staging-popupQty').data('masteritemid'),
                            qty:                   qty,
                            additemtoorder:        false,
                            addcompletetoorder:    false,
                            releasefromrepair:     false,
                            unstage:               false,
                            vendorid:              '',
                            meter:                 0,
                            location:              '',
                            locationdata:          screen._locationdata(),
                            addcontainertoorder:   false,
                            overridereservation:   false,
                            stageconsigned:        true,
                            transferrepair:        false,
                            removefromcontainer:   false,
                            contractid:            screen.getContractId(),
                            ignoresuspendedin:     false,
                            consignorid:           jQuery('#staging-popupQty').data('consignorid'),
                            consignoragreementid:  jQuery('#staging-popupQty').data('consignoragreementid')
                        };
                        RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                            try {
                                screen.pdastageitemCallback(responseStageItem);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#staging-popupQty-btnTransferRepair', function() {
                    var qty, requestStageItem;
                    try {
                        qty = parseFloat(jQuery('#staging-popupQty').data('responseStageItem').request.qty);
                        if (isNaN(qty)) {
                            qty = 0;
                        }
                        requestStageItem = {
                            orderid:               screen.getOrderId(),
                            code:                  RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code')),
                            masteritemid:          jQuery('#staging-popupQty').data('masteritemid'),
                            qty:                   qty,
                            additemtoorder:        false,
                            addcompletetoorder:    false,
                            releasefromrepair:     false,
                            unstage:               false,
                            vendorid:              '',
                            meter:                 0,
                            location:              '',
                            locationdata:          screen._locationdata(),
                            addcontainertoorder:   false,
                            overridereservation:   false,
                            stageconsigned:        false,
                            transferrepair:        true,
                            removefromcontainer:   false,
                            contractid:            screen.getContractId(),
                            ignoresuspendedin:     false,
                            consignorid:           jQuery('#staging-popupQty').data('consignorid'),
                            consignoragreementid:  jQuery('#staging-popupQty').data('consignoragreementid')
                        };
                        RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                            try {
                                screen.pdastageitemCallback(responseStageItem);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#staging-popupQty-btnAddContainerToOrder', function() {
                    var qty, requestStageItem;
                    try {
                        qty = parseFloat(jQuery('#staging-popupQty').data('responseStageItem').request.qty);
                        if (isNaN(qty)) {
                            qty = 0;
                        }
                        requestStageItem = {
                            orderid:               screen.getOrderId(),
                            code:                  RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code')),
                            masteritemid:          jQuery('#staging-popupQty').data('masteritemid'),
                            qty:                   qty,
                            additemtoorder:        false,
                            addcompletetoorder:    false,
                            releasefromrepair:     false,
                            unstage:               false,
                            vendorid:              '',
                            meter:                 0,
                            location:              '',
                            locationdata:          screen._locationdata(),
                            addcontainertoorder:   true,
                            overridereservation:   false,
                            stageconsigned:        false,
                            transferrepair:        false,
                            removefromcontainer:   false,
                            contractid:            screen.getContractId(),
                            ignoresuspendedin:     false,
                            consignorid:           jQuery('#staging-popupQty').data('consignorid'),
                            consignoragreementid:  jQuery('#staging-popupQty').data('consignoragreementid')
                        };
                        RwServices.order.pdastageitem(requestStageItem, function(responseStageItem) {
                            try {
                                screen.pdastageitemCallback(responseStageItem);
                            } catch(ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '#staging-popupQty-btnSubItem', function() {
                    var code = RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code'));
                    screen.hidePopupQty();
                    RwServices.order.getorderitemstosub({}, function(response) {
                        screen.subsitutepopup('Item', response.itemstosub, code, 'F');
                    });
                })
                .on('click', '#staging-popupQty-btnSubComplete', function() {
                    var code = RwAppData.stripBarcode(jQuery('#staging-popupQty').data('code'));
                    screen.hidePopupQty();
                    RwServices.order.getordercompletestosub({}, function(response) {
                        screen.subsitutepopup('Complete', response.completestosub, code, 'T');
                    });
                })
            ;
        };

        screen.subsitutepopup = function(caption, data, code, subcomplete) {
            var $confirm, $cancel, html = [];

                    html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">Item scanned is a substitute for ' + caption +'(s) present on this order.</div>');
                    html.push('<br />');
                    html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">Select the ' + caption + ' to substitute.</div>');
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].qtyremaining > 0) {
                            html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                            html.push('  <div style="float:left;width:60%;display:flex;flex-direction:column;justify-content:center;height:33px;"><div style="font-weight:bold;font-size:12px;">' + data[i].description + '</div></div>');
                            html.push('  <div style="float:right;"><div class="button default" data-masteritemid="' + data[i].masteritemid + '" data-substituteid="' + data[i].substituteid + '" data-qty="' + data[i].qty + '" data-step="select">Select</div></div>');
                            html.push('</div>');
                        }
                    }

                    $confirm = FwConfirmation.renderConfirmation('Select Substitute ' + caption, html.join(''));
                    $cancel  = FwConfirmation.addButton($confirm, 'Cancel', true);

                    $confirm
                        .on('click', '.button', function() {
                            var $this;
                            $this = jQuery(this);
                            if ($this.attr('data-step') === 'select') {
                                var $btns = $confirm.find('.button');
                                $btns.each(function(index, element) {
                                    var $btn;
                                    $btn = jQuery(element);
                                    $btn.attr('data-step', 'select').addClass('default').removeClass('green').html('Select');
                                });
                                $this.attr('data-step', 'confirm').removeClass('default').addClass('green').html('Confirm');
                            } else if ($this.attr('data-step') === 'confirm') {
                                var request;
                                request = {
                                    orderid:             screen.getOrderId(),
                                    reducemasteritemid:  $this.attr('data-masteritemid'),
                                    replacewithmasterid: $this.attr('data-substituteid'),
                                    qtytosubstitute:     $this.attr('data-qty'),
                                    substitutecomplete:  subcomplete
                                };
                                RwServices.order.substituteatstaging(request, function(response) {
                                    if (response.status > 0) {
                                        FwFunc.showError(response.msg);
                                    } else {
                                        FwConfirmation.destroyConfirmation($confirm);
                                        screen.$view.find('#scanBarcodeView-txtBarcodeData').val(code).change();
                                    }
                                });
                            }
                        })
                    ;
        };

        screen.confirmCancelSuspendContract = function (callback) {
            var requestOTE = {
                orderid: screen.getOrderId(),
                contractid: screen.getContractId()
            };
            if (requestOTE.contractid.length === 0) {
                callback();
            }
            else {
                RwServices.callMethod('Staging', 'OrdertranExists', requestOTE, function (responseOTE) {
                    try {
                        if (!responseOTE.ordertranExists) {
                            var requestCancelContract = {
                                contractid: screen.getContractId()
                            };
                            RwServices.callMethod('Staging', 'CancelContract', requestCancelContract, function () {
                                //try {
                                
                                //} catch (ex) {
                                //    FwFunc.showError(ex);
                                //}
                            });
                        
                        }
                        callback();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        };

        screen.promptResponsiblePerson = function () {
            var requestGetResponsiblePerson = {
                activitytype: RwConstants.activityTypes.Staging,
                orderid: screen.getOrderId()
            };
            RwServices.callMethod('Staging', 'GetResponsiblePerson', requestGetResponsiblePerson, function (response) {
                try {
                    properties.responsibleperson = response.responsibleperson;
                    if ((typeof properties.responsibleperson !== 'undefined') && (properties.responsibleperson.showresponsibleperson === 'T')) {
                        var $confirmation, $select, html = [];
                        $confirmation = FwConfirmation.renderConfirmation('Select Responsible Person', '');
                        $select = FwConfirmation.addButton($confirmation, 'Select', true);
                        html.push('<div data-control="FwFormField" class="fwcontrol fwformfield" id="responsibleperson" data-caption="Responsible Person" data-type="select" data-field="" />');
                        FwConfirmation.addControls($confirmation, html.join(''));
                        $confirmation.find('.body').css('color', 'black');
                        FwFormField.loadItems($confirmation.find('#responsibleperson'), properties.responsibleperson.responsiblepersons, true);
                        if (properties.responsibleperson.responsiblepersonid !== '') {
                            FwFormField.setValue($confirmation, '#responsibleperson', properties.responsibleperson.responsiblepersonid);
                        }
                        $select.on('click', function () {
                            try {
                                properties.responsibleperson.responsiblepersonid = FwFormField.getValue($confirmation, '#responsibleperson');
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        };

        $orderlocation.find('#orderlocationcontroller').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        $orderlocation.hide();
                        $primarywindow.show();
                    }
                },
                {
                    caption:     'Select Location',
                    orientation: 'right',
                    icon:        '&#xE5CC;', //chevron_right
                    state:       0,
                    buttonclick: function () {
                        var locationdata = $orderlocation.find('.location.selected').data('recorddata');
                        if (locationdata != null) {
                            screen._locationdata(locationdata);
                            screen.displayOrderLocation(locationdata.location)
                            $orderlocation.hide();
                            $primarywindow.show();
                            screen.refreshbottomspacer();
                        } else {
                            FwNotification.renderNotification('ERROR', 'Select a location.')
                        }
                    }
                }
            ]
        });
        $orderlocation.showscreen = function () {
            $orderlocation.show();
            $orderlocation.searchlocation('');
        };
        $orderlocation.searchlocation = function (searchvalue) {
            $orderlocation.find('.orderlocations').empty();
            var request = {
                orderid:     screen.getOrderId(),
                searchvalue: searchvalue
            };
            RwServices.callMethod("Staging", "SearchLocations", request, function(response) {
                if (response.locations.length > 0) {
                    for (var item of response.locations) {
                        var html = [];
                        html.push('<div class="location">');
                        html.push(`  <div class="row1"><div class="title">${item.location}</div></div>`);
                        html.push('  <div class="row2">');
                        html.push('    <div class="col1">');
                        html.push('      <div class="datafield masterno">');
                        html.push('        <div class="caption">Building:</div>');
                        html.push(`        <div class="value">${item.building}</div>`);
                        html.push('      </div>');
                        html.push('    </div>');
                        html.push('    <div class="col2">');
                        html.push('      <div class="datafield rate">');
                        html.push('        <div class="caption">Floor:</div>');
                        html.push(`        <div class="value">${item.floor}</div>`);
                        html.push('      </div>');
                        html.push('    </div>');
                        html.push('  </div>');
                        html.push('  <div class="row3">');
                        html.push('    <div class="col1">');
                        html.push('      <div class="datafield masterno">');
                        html.push('        <div class="caption">Space:</div>');
                        html.push(`        <div class="value">${item.space}</div>`);
                        html.push('      </div>');
                        html.push('    </div>');
                        html.push('  </div>');
                        html.push('</div>');
                        var $item = jQuery(html.join(''));
                        $item.data('recorddata', item);

                        $orderlocation.find('.orderlocations').append($item);
                    }
                } else {
                    var $zeroitems = jQuery('<div class="zeroitems">0 Locations Found</div>');
                    $orderlocation.find('.orderlocations').append($zeroitems);
                }
            });
        };
        $orderlocation
            .on('change', '.fwmobilecontrol-value', function () {
                var value = jQuery(this).val();
                if (value != '') {
                    $orderlocation.searchlocation(value);
                }
            })
            .on('click', '.location', function () {
                var $this = jQuery(this);
                $this.siblings().removeClass('selected');
                $this.addClass('selected');
            })
        ;

        $substituteitemskit.find('#substituteitemkitcontroller').fwmobilemodulecontrol({
            buttons: [
                {
                    caption:     'Back',
                    orientation: 'left',
                    icon:        '&#xE5CB;', //chevron_left
                    state:       0,
                    buttonclick: function () {
                        $substituteitemskit.hideScreen();
                        $primarywindow.show();

                        program.onScanBarcode = function (barcode, barcodeType) {
                            try {
                                screen.scanCode(barcode);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        }
                    }
                },
                {
                    caption:     'Submit Substitution',
                    orientation: 'right',
                    icon:        '&#xE5CC;', //chevron_right
                    state:       0,
                    buttonclick: function () {
                        var request = {
                            //Quantity: 
                            SessionId: $substituteitemskit.data('sessionid')
                        }

                        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/applysubstitutesession`, request, FwServices.defaultTimeout, function onSuccess(response) {
                            if (response.success) {
                                $substituteitemskit.hideScreen();
                                $primarywindow.show();
                                screen.refreshbottomspacer();

                                program.onScanBarcode = function (barcode, barcodeType) {
                                    try {
                                        screen.scanCode(barcode);
                                    } catch (ex) {
                                        FwFunc.showError(ex);
                                    }
                                }
                            } else {
                                FwNotification.renderNotification('ERROR', response.msg);
                            }
                        }, null, null);
                    }
                }
            ]
        });
        $substituteitemskit.showscreen = function (masterid, masteritemid, icode, caption) {
            var request = {
                OrderId:     screen.getOrderId(),
                OrderItemId: masteritemid
            }
            RwServices.callMethod("Staging", "StartSubstituteSession", request, function(response) {
                $substituteitemskit.show();
                $substituteitemskit.data('sessionid', response.sessionid);

                $substituteitemskit.find('.iteminfo').html(`${icode} - ${caption}`);

                program.onScanBarcode = function (barcode, barcodeType) {
                    try {
                        $substituteitemskit.scanCode(barcode);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            });
        };
        $substituteitemskit.hideScreen = function () {
            $substituteitemskit.hide();
            $substituteitemskit.find('.fwmobilecontrol-value').val('');
            $substituteitemskit.find('.substituteitems').html('');
            $substituteitemskit.data('sessionid', '');
        };
        $substituteitemskit.scanCode = function (code: string) {
            var strippedCode = RwAppData.stripBarcode(code.toUpperCase());

            var request = {
                SessionId:     $substituteitemskit.data('sessionid'),
                Code:          strippedCode,
                Qty:           0
            };
            RwServices.callMethod("Staging", "StageSubstituteItem", request, function(response) {
                if (response.stagesubstituteitem.status === 0) {
                    $substituteitemskit.renderItems(response.items);
                    $substituteitemskit.find('.fwmobilecontrol-value').val('');
                } else if (response.stagesubstituteitem.status === 107) {
                    var $confirmation = FwConfirmation.renderConfirmation('How Many?', '');
                    var $ok           = FwConfirmation.addButton($confirmation, 'OK', false);
                    var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

                    FwConfirmation.addControls($confirmation, '<div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Qty" data-datafield="qty" data-minvalue="0" data-formatnumeric="true"></div>');

                    $ok.on('click', function () {
                        request.Qty = FwFormField.getValueByDataField($confirmation, 'qty');
                        FwConfirmation.destroyConfirmation($confirmation);

                        RwServices.callMethod("Staging", "StageSubstituteItem", request, function(response) {
                            if (response.stagesubstituteitem.status === 0) {
                                $substituteitemskit.renderItems(response.items);
                                $substituteitemskit.find('.fwmobilecontrol-value').val('');
                            } else {
                                FwNotification.renderNotification('ERROR', response.stagesubstituteitem.msg);
                            }
                        });
                    });
                } else {
                    FwNotification.renderNotification('ERROR', response.stagesubstituteitem.msg);
                }
            });
        };
        $substituteitemskit.renderItems = function (items) {
            for (var item of items) {
                var html = [];
                html.push('<div class="item">');
                html.push(`  <div class="row1"><div class="title">${item.master}</div></div>`);
                html.push('  <div class="row2">');
                html.push('    <div class="col1">');
                html.push('      <div class="datafield masterno">');
                html.push('        <div class="caption">I-Code:</div>');
                html.push(`        <div class="value">${item.masterno}</div>`);
                html.push('      </div>');
                html.push('    </div>');
                html.push('    <div class="col2">');
                html.push('      <div class="datafield rate">');
                html.push('        <div class="caption">Qty:</div>');
                html.push(`        <div class="value">${item.qty}</div>`);
                html.push('      </div>');
                html.push('    </div>');
                html.push('  </div>');
                if (item.barcode) {
                    html.push('  <div class="row3">');
                    html.push('    <div class="col1">');
                    html.push('      <div class="datafield masterno">');
                    html.push('        <div class="caption">Barcode:</div>');
                    html.push(`        <div class="value">${item.barcode}</div>`);
                    html.push('      </div>');
                    html.push('    </div>');
                    html.push('  </div>');
                }
                html.push('</div>');
                var $item = jQuery(html.join(''));
                $item.data('recorddata', item);

                $substituteitemskit.find('.substituteitems').append($item);
            }
        };
        $substituteitemskit
            .on('change', '.fwmobilecontrol-value', function () {
                var value = jQuery(this).val();
                if (value != '') {
                    $substituteitemskit.scanCode(value);
                }
            })
        ;

        screen.load = function() {
            program.setScanTarget('');
            program.setScanTargetLpNearfield('');

            program.onScanBarcode = function (barcode, barcodeType) {
                try {
                    screen.scanCode(barcode);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }

            if (typeof window.ZebraRFIDAPI3 !== 'undefined') {
                // setup TSL RFID Reader
                RwRFID.registerRFIDEvents(screen.rfidscan);
                window.ZebraRFIDAPI3.registerListener('deviceConnected', 'deviceConnected_stagingcontrollerjs_getStagingScreen', function () {
                    //FwNotification.renderNotification('INFO', 'Staging: RFID Connected');
                    screen.toggleRfid();
                });
                window.ZebraRFIDAPI3.registerListener('deviceDisconnected', 'deviceDisconnected_stagingcontrollerjs_getStagingScreen', function () {
                    //FwNotification.renderNotification('INFO', 'Staging: RFID Disconnected');
                    screen.toggleRfid();
                });
            }
            else if (typeof window.TslReader !== 'undefined') {
                // setup TSL RFID Reader
                RwRFID.registerRFIDEvents(screen.rfidscan);
            
                window.TslReader.registerListener('deviceConnected', 'deviceConnected_stagingcontrollerjs_getStagingScreen', function() {
                    //FwNotification.renderNotification('INFO', 'Staging: RFID Connected');
                    screen.toggleRfid();
                });
                window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_stagingcontrollerjs_getStagingScreen', function() {
                    //FwNotification.renderNotification('INFO', 'Staging: RFID Disconnected');
                    screen.toggleRfid();
                });
            }

            RwServices.callMethod("Staging", "LoadModuleProperties", {}, function(response) {
                moduleproperties = response;
            });

            screen.setIsSuspendedSessionsEnabled(sessionStorage.getItem('stagingSuspendedSessionsEnabled') === 'true');
            if (screen.getIsSuspendedSessionsEnabled()) {
                screen.pages.stagingmenu.forward();
            } else {
                screen.pages.search.forward();
            }
        };

        screen.unload = function() {
            program.setScanTarget('#scanBarcodeView-txtBarcodeData');
            program.setScanTargetLpNearfield('');
            program.onScanBarcode = null;
            if (typeof window.ZebraRFIDAPI3 !== 'undefined') {
                window.ZebraRFIDAPI3.unregisterListener('deviceConnected', 'deviceConnected_stagingcontrollerjs_getStagingScreen');
                window.ZebraRFIDAPI3.unregisterListener('deviceDisconnected', 'deviceDisconnected_stagingcontrollerjs_getStagingScreen');
                RwRFID.unregisterRFIDEvents();
            }
            else if (typeof window.TslReader !== 'undefined') {
                window.TslReader.unregisterListener('deviceConnected', 'deviceConnected_stagingcontrollerjs_getStagingScreen');
                window.TslReader.unregisterListener('deviceDisconnected', 'deviceDisconnected_stagingcontrollerjs_getStagingScreen');
                RwRFID.unregisterRFIDEvents();
            }
            jQuery(window).off('scroll').off('touchmove');
        };

        screen.evalWindowPosition = function() {
            if (jQuery(window).scrollTop() > 152) {
                var $jumptotop;

                $jumptotop = jQuery('<div>')
                    .html('<i class="material-icons">&#xE5D8;</i>') //arrow_upward
                    .addClass('fwchip jumptotop')
                    .on('click', function() {
                        jQuery(window).scrollTop(0);
                    });

                if (screen.$view.find('.jumptotop').length == 0) {
                    screen.$view.append($jumptotop);
                }
            } else {
                screen.$view.find('.jumptotop').remove();
            }
        };

        screen.beforeNavigateAway = function (navigateAway) {
            screen.confirmCancelSuspendContract(navigateAway);
        };

        return screen;
    };
}

var StagingController = new StagingControllerClass();