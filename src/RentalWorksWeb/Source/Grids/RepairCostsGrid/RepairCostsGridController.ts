class RepairCostsGrid {
  Module: string;
  apiurl: string;

  constructor() {
    this.Module = 'RepairCostsGrid';
    this.apiurl = 'api/v1/repaircost';
  }
}

var RepairCostsGridController = new RepairCostsGrid();
//----------------------------------------------------------------------------------------------