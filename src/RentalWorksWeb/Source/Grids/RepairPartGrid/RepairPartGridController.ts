class RepairPartGrid {
  Module: string;
  apiurl: string;

  constructor() {
    this.Module = 'RepairPartGrid';
    this.apiurl = 'api/v1/repairpart';
  }
  generateRow($control, $generatedtr) {
    $generatedtr.find('div[data-browsedatafield="ICode"]').data('onchange', function ($tr) {
      $generatedtr.find('.field[data-browsedatafield="InventoryId"] input').val($tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue'));
      $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
    });
  };
}

var RepairPartGridController = new RepairPartGrid();
//----------------------------------------------------------------------------------------------