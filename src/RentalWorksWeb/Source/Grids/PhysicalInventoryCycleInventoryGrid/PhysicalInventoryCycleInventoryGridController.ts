class PhysicalInventoryCycleInventoryGrid {
    Module: string = 'PhysicalInventoryCycleInventoryGrid';
    apiurl: string = 'api/v1/physicalinventorycycleinventory';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="AvailableFor"]').text($tr.find('.field[data-browsedatafield="AvailFor"]').attr('data-originalvalue'));
        });
    }
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'InventoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                break;
        }
    }
}

var PhysicalInventoryCycleInventoryGridController = new PhysicalInventoryCycleInventoryGrid();
//----------------------------------------------------------------------------------------------