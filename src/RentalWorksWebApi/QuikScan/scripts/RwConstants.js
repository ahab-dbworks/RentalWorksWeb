var RwRoutes = [];
RwConstants = {};
RwConstants.itemTypes = {
    BarCoded:    'BarCoded',
    NonBarCoded: 'NonBarCoded',
    None:        'None'
};
RwConstants.moduleTypes = {
    Order:      'Order',
    SubReceive: 'SubReceive',
    SubReturn:  'SubReturn',
    PhyInv:     'PhyInv',
    Transfer:   'Transfer',
    Truck:      'Truck'
};
RwConstants.activityTypes = {
    CheckIn:    'CheckIn',
    Staging:    'Staging',
    SubReceive: 'SubReceive',
    SubReturn:  'SubReturn',
    AssetDisposition: 'AssetDisposition',
    RfidStaging: 'RfidStaging'
};
RwConstants.checkInModes = {
    SingleOrder:   'SingleOrder',
    MultiOrder:    'MultiOrder',
    Session:       'Session',
    Deal:          'Deal'
};
RwConstants.repairModes = {
    Complete: 'Complete',
    Release:  'Release'
};
RwConstants.stagingType = {
    Normal: 'Normal',
    RfidPortal:  'RfidPortal'
};
RwConstants.checkInType = {
    Normal: 'Normal',
    RfidPortal: 'RfidPortal'
};
RwConstants.STAGING_STATUS_QTY_EXCEEDS_ORDER_CAN_ADD_ITEM             = 207;
RwConstants.STAGING_STATUS_QTY_EXCEEDS_ORDER_CAN_ADD_ITEM_OR_COMPLETE = 208;
RwConstants.STAGING_STATUS_QTY_EXCEEDS_ORDER_CAN_ADD_CONTAINER        = 217;
RwConstants.STAGING_STATUS_ITEM_NOT_ON_ORDER_CAN_ADD_ITEM             = 209;
RwConstants.STAGING_STATUS_ITEM_NOT_ON_ORDER_CAN_ADD_ITEM_OR_COMPLETE = 210;
RwConstants.STAGING_STATUS_ITEM_NOT_ON_ORDER_CAN_ADD_CONTAINER        = 219;
RwConstants.STAGING_STATUS_CONFLICT_WITH_RESERVATION_CAN_OVERRIDE     = 215;
RwConstants.STAGING_STATUS_ITEM_IN_REPAIR_CAN_TRANSFER                = 110;
RwConstants.STAGING_STATUS_ITEM_IN_REPAIR                             = 100;
