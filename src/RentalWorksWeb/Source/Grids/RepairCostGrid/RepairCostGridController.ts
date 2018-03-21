class RepairCostGrid {
  Module: string = 'RepairCostGrid';
  apiurl: string = 'api/v1/repaircost';

  generateRow($control, $generatedtr) {
    $generatedtr.find('div[data-browsedatafield="RateId"]').data('onchange', function ($tr) {
      $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
      $generatedtr.find('.field[data-browsedatafield="Unit"] input').val($tr.find('.field[data-browsedatafield="Unit"]').attr('data-originalvalue'));
    });
  };
}

var RepairCostGridController = new RepairCostGrid();
//----------------------------------------------------------------------------------------------