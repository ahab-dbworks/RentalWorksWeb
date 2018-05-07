class PartsInventoryValidation {
    Module: string = 'PartsInventoryValidation';
    apiurl: string = 'api/v1/partsinventory';

    addLegend($control) {
        FwBrowse.addLegend($control, 'Item', '#FFFFFF');
        FwBrowse.addLegend($control, 'Accessory', '#fffa00');
        FwBrowse.addLegend($control, 'Complete', '#0080ff');
        FwBrowse.addLegend($control, 'Kit', '#00c400');
        FwBrowse.addLegend($control, 'Misc', '#ff0080');
        FwBrowse.addLegend($control, 'Container', '#FF8040');
    }
}

var PartsInventoryValidationController = new PartsInventoryValidation();