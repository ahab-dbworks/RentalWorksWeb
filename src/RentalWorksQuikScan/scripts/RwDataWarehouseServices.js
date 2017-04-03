var RwDataWarehouseServices = {
    parameters: {}
  , reports: {}
};
//----------------------------------------------------------------------------------------------
RwDataWarehouseServices.parameters.getCompanyDepartments   = function(request, doneCallback) {RwAppData.jsonPost(true, 'rwdatawarehouseservices.ashx?path=/parameters/getcompanydepartments', request, doneCallback, 1200);};
RwDataWarehouseServices.reports.customerRevenueByMonth     = function(request, doneCallback) {RwAppData.jsonPost(true, 'rwdatawarehouseservices.ashx?path=/reports/customerrevenuebymonth',   request, doneCallback, 1200);};
