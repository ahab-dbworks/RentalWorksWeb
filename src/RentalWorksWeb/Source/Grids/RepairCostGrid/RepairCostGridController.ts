class RepairCostGrid {
  Module: string;
  apiurl: string;

  constructor() {
    this.Module = 'RepairCostGrid';
    this.apiurl = 'api/v1/repaircost';
  }

  generateRow($control, $generatedtr) {
    $generatedtr.find('div[data-browsedatafield="ICode"]').data('onchange', function ($tr) {
      $generatedtr.find('.field[data-browsedatafield="RateId"] input').val($tr.find('.field[data-browsedatafield="RateId"]').attr('data-originalvalue'));
      $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
    });
  };
}

var RepairCostGridController = new RepairCostGrid();
//----------------------------------------------------------------------------------------------