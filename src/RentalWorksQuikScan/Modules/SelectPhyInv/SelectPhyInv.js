var RwSelectPhyInv = {};
//----------------------------------------------------------------------------------------------
RwSelectPhyInv.getSelectPhyInvScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
        captionPageTitle:    RwLanguages.translate('Physical Inventory')
      , htmlScanBarcode:     RwPartialController.getScanBarcodeHtml({captionBarcodeICode:RwLanguages.translate('Inventory No.')})
      , captionPhyInvNo:     RwLanguages.translate('Physical Inventory No') + ':'
      , captionWarehouse:    RwLanguages.translate('Warehouse') + ':'
      , captionDepartment:   RwLanguages.translate('Department:')
      , captionFor:          RwLanguages.translate('For')
      , captionCount:        RwLanguages.translate('Count')
      , captionItems:        RwLanguages.translate('Items')
      , captionConfirm:      RwLanguages.translate('Select Physical Inventory')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-selectPhyInv').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.$view.find('#selectPhyInvView-response').hide();
    screen.$view.find('#selectPhyInvView-error').hide();
    
    screen.$view
        .on('change', '#scanBarcodeView-txtBarcodeData', function() {
            var $this, request;
            try {
                $this = jQuery(this);
                request = {
                    phyNo: $this.val().toUpperCase()
                };
                RwServices.order.selectPhyInv(request, function(response) {
                    jQuery('#selectPhyInvView-phyInvNo').html(response.webSelectPhyInv.phyNo);
                    jQuery('#selectPhyInvView-description').html(response.webSelectPhyInv.description);
                    jQuery('#selectPhyInvView-phyStatus').html(response.webSelectPhyInv.phyStatus);
                    jQuery('#selectPhyInvView-scheduleDate').html(response.webSelectPhyInv.scheduleDate);
                    jQuery('#selectPhyInvView-countType').html(response.webSelectPhyInv.counttype);
                    jQuery('#selectPhyInvView-recType').html(response.webSelectPhyInv.rectype);
                    jQuery('#selectPhyInvView-warehouse').html(response.webSelectPhyInv.warehouse);
                    jQuery('#selectPhyInvView-department').html(response.webSelectPhyInv.department);
                    jQuery('#selectPhyInvView-msgValue').html(response.webSelectPhyInv.msg);
                    properties.webSelectPhyInv = response.webSelectPhyInv;
                    program.playStatus(response.webSelectPhyInv.status == 0);

                    jQuery('#selectPhyInvView-response').show();
                    jQuery('#selectPhyInvView-location')
                        .toggle((applicationConfig.designMode) || (response.webSelectPhyInv.status === 0));
                    jQuery('#selectPhyInvView-locationWarehouse')
                        .toggle((applicationConfig.designMode) || (response.webSelectPhyInv.warehouse.length > 0));
                    jQuery('#selectPhyInvView-locationDept')
                        .toggle((applicationConfig.designMode) || (response.webSelectPhyInv.department.length > 0));
                    jQuery('#selectPhyInvView-messages')
                        .toggle((applicationConfig.designMode) || (response.webSelectPhyInv.msg.length > 0));
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#selectPhyInvView-btnYes', function() {
            var $this, screenPhyInvItem, phyInvItem_viewModel, phyInvItem_properties;
            try {
                $this = jQuery(this);
                phyInvItem_viewModel = {};
                phyInvItem_properties = {
                    phyNo:      properties.webSelectPhyInv.phyNo
                  , physicalId: properties.webSelectPhyInv.physicalId
                };
                screenPhyInvItem = RwInventoryController.getPhyInvItemScreen(phyInvItem_viewModel, phyInvItem_properties);
                program.pushScreen(screenPhyInvItem);
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.load = function() {
        program.setScanTarget('#scanBarcodeView-txtBarcodeData');
        if (!Modernizr.touch) {
            jQuery('#scanBarcodeView-txtBarcodeData').select();
        }
    };
    
    return screen;
};