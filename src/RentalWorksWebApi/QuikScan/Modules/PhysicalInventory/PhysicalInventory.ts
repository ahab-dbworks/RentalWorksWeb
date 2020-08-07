var PhysicalInventory:any = {};
//----------------------------------------------------------------------------------------------
PhysicalInventory.getPhysicalInventoryScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
        captionPageTitle:     RwLanguages.translate('Physical Inventory'),
        htmlScanBarcode:      RwPartialController.getScanBarcodeHtml({captionBarcodeICode:RwLanguages.translate('Inventory No.')}),
        captionPhyInvNo:      RwLanguages.translate('Physical Inventory No'),
        captionWarehouse:     RwLanguages.translate('Warehouse'),
        captionDepartment:    RwLanguages.translate('Department'),
        captionStatus:        RwLanguages.translate('Status'),
        captionScheduleDate:  RwLanguages.translate('Schedule Date'),
        captionInventoryType: RwLanguages.translate('Inventory Type'),
        captionCountType:     RwLanguages.translate('Count Type')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-physicalinventory').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    var $control = screen.$view.find('#physicalinventorycontrol');
    var $status  = screen.$view.find('#pi-status');
    var $item    = screen.$view.find('#pi-item');

    $control.fwmobilemodulecontrol({
        buttons: [
            { 
                id:          'selectitem',
                caption:     'Select Item',
                orientation: 'right',
                icon:        '&#xE5CC;', //chevron_right
                state:       0,
                buttonclick: function () {
                    if (properties.item != null) {
                        try {
                            var $this = jQuery(this);
                            var phyInvItem_viewModel = {};
                            var phyInvItem_properties = {
                                phyNo:      properties.item.phyNo,
                                physicalId: properties.item.physicalId
                            };
                            var screenPhyInvItem = RwInventoryController.getPhyInvItemScreen(phyInvItem_viewModel, phyInvItem_properties);
                            program.pushScreen(screenPhyInvItem);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    } else {
                        FwNotification.renderNotification('ERROR', 'An item must be selected to perform this action.');
                    }
                }
            }
        ]
    });
    screen.$view
        .on('change', '.fwmobilecontrol-value', function() {
            try {
                $item.hide();
                $status.empty().hide();
                $control.fwmobilemodulecontrol('hideButton', '#selectitem');
                var $this = jQuery(this);
                if ($this.val() !== '') {
                    var request = {
                        phyNo: $this.val().toString().toUpperCase()
                    };
                    RwServices.callMethod("PhysicalInventory", "GetInventoryItem", request, function(response) {
                        $this.val('');
                        if (response.item.status === 0) {
                            $item.showscreen(response.item);
                        } else {
                            $status.html(response.item.msg).show();
                        }
                    });
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    $item.showscreen = function (item) {
        var rectype;
        $control.fwmobilemodulecontrol('showButton', '#selectitem');
        properties.item = item;

        if (item.rectype === 'R') {
            rectype = 'Rental';
        } else if (item.rectype === 'S') {
            rectype = 'Sales';
        }

        $item.find('.pi-item-description').html(item.description);
        $item.find('.phyno .value').html(item.phyNo);
        $item.find('.status .value').html(item.phyStatus);
        $item.find('.scheduledate .value').html(item.scheduleDate);
        $item.find('.counttype .value').html(item.counttype);
        $item.find('.rectype .value').html(rectype);

        $item.find('.warehouse').toggle(item.warehouse != '');
        $item.find('.warehouse .value').html(item.warehouse);
        $item.find('.department').toggle(item.department !== '');
        $item.find('.department .value').html(item.department);

        $item.show();
    };

    screen.load = function() {
        program.setScanTarget('.fwmobilecontrol-value');
        $control.fwmobilemodulecontrol('hideButton', '#selectitem');
        if (!Modernizr.touch) {
            jQuery('.fwmobilecontrol-value').select();
        }
    };
    
    return screen;
};