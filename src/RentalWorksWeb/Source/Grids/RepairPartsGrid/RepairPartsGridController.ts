class RepairPartsGrid {
  Module: string;
  apiurl: string;

  constructor() {
    this.Module = 'RepairPartsGrid';
    this.apiurl = 'api/v1/repairpart';
  }
}

var RepairPartsGridController = new RepairPartsGrid();
//----------------------------------------------------------------------------------------------