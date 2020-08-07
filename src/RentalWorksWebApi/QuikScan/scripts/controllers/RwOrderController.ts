var RwOrderController: any = {};
//----------------------------------------------------------------------------------------------
RwOrderController.getPageTitle = function(properties) {
    var captionPageTitle;
    switch (properties.moduleType) {
        case RwConstants.moduleTypes.Order:
             switch(properties.activityType) {
                case RwConstants.activityTypes.CheckIn:
                    switch(properties.checkInMode) {
                        case RwConstants.checkInModes.SingleOrder:
                        case RwConstants.checkInModes.MultiOrder:
                        case RwConstants.checkInModes.Session:
                        case RwConstants.checkInModes.Deal:
                             captionPageTitle = RwLanguages.translate('Check-In');
                            break;
                        case RwConstants.checkInModes.FillContainer:
                             captionPageTitle = RwLanguages.translate('Fill Container');
                            break;
                    }
                    break;
                case RwConstants.activityTypes.Staging:
                    if (properties.stagingType === RwConstants.stagingType.Normal) {
                        captionPageTitle = RwLanguages.translate('Staging');
                    } else if (properties.stagingType === RwConstants.stagingType.RfidPortal) {
                        captionPageTitle = RwLanguages.translate('RFID Staging / Check-Out');
                    }
                    break;
            }
            break;
        case RwConstants.moduleTypes.SubReceive:
            captionPageTitle = RwLanguages.translate('PO Sub-Receive');
            break;
        case RwConstants.moduleTypes.SubReturn:
            captionPageTitle = RwLanguages.translate('PO Sub-Return');
            break;
        case RwConstants.moduleTypes.PhyInv:
            captionPageTitle = RwLanguages.translate('Physical Inventory');
            break;
        case RwConstants.moduleTypes.Transfer:
            switch(properties.activityType) {
                case RwConstants.activityTypes.Staging:
                    captionPageTitle = RwLanguages.translate('Transfer') + ' ' + RwLanguages.translate('Out');
                    break;
                case RwConstants.activityTypes.CheckIn:
                    captionPageTitle = RwLanguages.translate('Transfer') + ' ' + RwLanguages.translate('In');
                    break;
            }
            break;
        case RwConstants.moduleTypes.Truck:
            captionPageTitle = RwLanguages.translate('Package Truck');
            break;
    }
    return captionPageTitle;
}
//----------------------------------------------------------------------------------------------
