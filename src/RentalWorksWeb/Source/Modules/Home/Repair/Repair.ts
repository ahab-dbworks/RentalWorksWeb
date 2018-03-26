routes.push({ pattern: /^module\/repair$/, action: function (match: RegExpExecArray) { return RepairController.getModuleScreen(); } });
// routes.push({ pattern: /^module\/repair\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return RepairController.getModuleScreen(filter); } });
//---------------------------------------------------------------------------------
class Repair {
  Module: string = 'Repair';
  apiurl: string = 'api/v1/repair';
  caption: string = 'Repair Order';
  ActiveView: string;

  getModuleScreen() {
    var me: Repair = this;
    var screen: any = {};
    screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
    screen.viewModel = {};
    screen.properties = {};

    var $browse: JQuery = this.openBrowse();

    screen.load = function () {
      FwModule.openModuleTab($browse, me.caption, false, 'BROWSE', true);
      FwBrowse.databind($browse);
      FwBrowse.screenload($browse);
    };
    screen.unload = function () {
      FwBrowse.screenunload($browse);
    };

    return screen;
  };

  //----------------------------------------------------------------------------------------------
  renderGrids($form: any) {
    let self = this;

    let $repairCostGrid, $repairCostGridControl; 
 
    $repairCostGrid = $form.find('div[data-grid="RepairCostGrid"]'); 
    $repairCostGridControl = jQuery(jQuery('#tmpl-grids-RepairCostGridBrowse').html()); 
    $repairCostGrid.empty().append($repairCostGridControl); 
    $repairCostGridControl.data('ondatabind', request => { 
      request.uniqueids = { 
        RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val() 
      } 
    }); 
    $repairCostGridControl.data('beforesave', request => { 
      request.RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 
    }) 
    // runs after grid load, add, and delete
    FwBrowse.addEventHandler($repairCostGridControl, 'afterdatabindcallback', () => {
      self.calculateTotals($form);
    });
   
    FwBrowse.init($repairCostGridControl); 
    FwBrowse.renderRuntimeHtml($repairCostGridControl);
    
    //----------------------------------------------------------------------------------------------
    let $repairPartGrid, $repairPartGridControl; 
 
    $repairPartGrid = $form.find('div[data-grid="RepairPartGrid"]'); 
    $repairPartGridControl = jQuery(jQuery('#tmpl-grids-RepairPartGridBrowse').html()); 
    $repairPartGrid.empty().append($repairPartGridControl); 
    $repairPartGridControl.data('ondatabind', request => { 
      request.uniqueids = { 
        RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val() 
      } 
    }); 
    $repairPartGridControl.data('beforesave', request => { 
      request.RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 
    }) 
    FwBrowse.init($repairPartGridControl); 
    FwBrowse.renderRuntimeHtml($repairPartGridControl);
  } 

  //----------------------------------------------------------------------------------------------
  openBrowse() {
    let self = this;
    let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
    $browse = FwModule.openBrowse($browse);
    FwBrowse.addLegend($browse, 'Foreign Currency', '#95FFCA');
    FwBrowse.addLegend($browse, 'High Priority', '#EA300F');
    FwBrowse.addLegend($browse, 'Not Billed', '#0fb70c');
    FwBrowse.addLegend($browse, 'Billable', '#0c6fcc');
    FwBrowse.addLegend($browse, 'Outside', '#fffb38');
    FwBrowse.addLegend($browse, 'Released', '#d6d319');
    FwBrowse.addLegend($browse, 'Transferred', '#d10e90');
    FwBrowse.addLegend($browse, 'Pending Repair', '#b997db');

    return $browse;
  };

  //----------------------------------------------------------------------------------------------
  addBrowseMenuItems($menuObject) {
    var self = this;
    var $all = FwMenu.generateDropDownViewBtn('All', true);
    var $confirmed = FwMenu.generateDropDownViewBtn('Confirmed', false);
    var $active = FwMenu.generateDropDownViewBtn('Active', false);
    var $hold = FwMenu.generateDropDownViewBtn('Hold', false);
    var $closed = FwMenu.generateDropDownViewBtn('Closed', false);
    $all.on('click', function () {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        self.ActiveView = 'ALL';
        FwBrowse.databind($browse);
    });
    $confirmed.on('click', function () {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        self.ActiveView = 'CONFIRMED';
        FwBrowse.databind($browse);
    });
    $active.on('click', function () {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        self.ActiveView = 'ACTIVE';
        FwBrowse.databind($browse);
    });
    $hold.on('click', function () {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        self.ActiveView = 'HOLD';
        FwBrowse.databind($browse);
    });
    $closed.on('click', function () {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        self.ActiveView = 'CLOSED';
        FwBrowse.databind($browse);
    });
    var viewSubitems = [];
    viewSubitems.push($all);
    viewSubitems.push($confirmed);
    viewSubitems.push($active);
    viewSubitems.push($hold);
    viewSubitems.push($closed);
    var $view;
    $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);
    //Location Filter
    var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
    var $allLocations = FwMenu.generateDropDownViewBtn('ALL Warehouses', false);
    var $userWarehouse = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true);
    $allLocations.on('click', function () {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        self.ActiveView = 'WarehouseId=ALL';
        FwBrowse.databind($browse);
    });
    $userWarehouse.on('click', function () {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;
        FwBrowse.databind($browse);
    });
    var viewLocation = [];
    viewLocation.push($userWarehouse);
    viewLocation.push($all);
    var $locationView;
    $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
    return $menuObject;
  };
  //----------------------------------------------------------------------------------------------
  openForm(mode: string) {
    var $form;
    $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
    $form = FwModule.openForm($form, mode);

    if (mode === 'NEW') {
      $form.find('.ifnew').attr('data-enabled', 'true');
      var today = new Date(Date.now()).toLocaleString().split(',')[0];
      //var date = today.split(',');
      var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
      var office = JSON.parse(sessionStorage.getItem('location'));
      var department = JSON.parse(sessionStorage.getItem('department'));

      FwFormField.setValueByDataField($form, 'RepairDate', today);
      //FwFormField.setValueByDataField($form, 'EstimatedStartDate', date[0]);
      //FwFormField.setValueByDataField($form, 'EstimatedStopDate', date[0]);
      FwFormField.setValueByDataField($form, 'Location', office.location);
      FwFormField.setValueByDataField($form, 'Warehouse', warehouse.warehouse);

      //$form.find('div[data-datafield="PickTime"]').attr('data-required', false);
      //$form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
      //$form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);

      //FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid);
      //FwFormField.setValueByDataField($form, 'OfficeLocationId', office.locationid);
      //FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid);
      $form.find('div[data-datafield="Department"] input').val(department.department);


      $form.find('div[data-datafield="PoPending"] input').prop('checked', true);
      //FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
      //FwFormField.disable($form.find('[data-datafield="PoAmount"]'));


      FwFormField.disable($form.find('.frame'));
      $form.find(".frame .add-on").children().hide();
    }

    return $form;
  };

  //----------------------------------------------------------------------------------------------
  loadForm(uniqueids: any) {
    var $form: JQuery = this.openForm('EDIT');
    $form = this.openForm('EDIT');
    $form.find('div.fwformfield[data-datafield="RepairId"] input').val(uniqueids.RepairId);
    FwModule.loadForm(this.Module, $form);

    return $form;
  };

  //----------------------------------------------------------------------------------------------
  saveForm($form: any, closetab: boolean, navigationpath: string) {
    FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
  };

  //----------------------------------------------------------------------------------------------
  afterLoad($form: any, $browse: any) { 
    var $repairCostGrid: any = $form.find('[data-name="RepairCostGrid"]'); 
    FwBrowse.search($repairCostGrid); 
    var $repairPartGrid: any = $form.find('[data-name="RepairPartGrid"]'); 
    FwBrowse.search($repairPartGrid);
  };

  //----------------------------------------------------------------------------------------------
  calculateTotals($form: any) {
    let extendedColumn: any = $form.find('.costgridextended');
    let totalSumFromExtended: any = 0;

    for (let i = 1; i < extendedColumn.length; i++) {
      let inputValueFromExtended: any = parseInt($form.find('.costgridextended').eq(i).attr('data-originalvalue'));
      totalSumFromExtended += inputValueFromExtended;
    }
    $form.find('[data-totalfield="Total"] input').val(totalSumFromExtended);
  };
  //----------------------------------------------------------------------------------------------
  calculateExtended($form: any) {
    //let extendedSum: any = Number($form.find('.costgridextended').eq(1).attr('data-originalvalue'));
    let discountValue: any = parseInt($form.find('.costgriddiscount').eq(1).attr('data-originalvalue'));
    let rateValue: any = parseInt($form.find('.costgridrate').eq(1).attr('data-originalvalue'));
    let quantityValue: any = parseInt($form.find('.costgridquantity').eq(1).attr('data-originalvalue'));
    let extendedSum: any = 0;

    //console.log("extendedColumn.length: ", extendedColumn.length);
    console.log("discountValue: ", discountValue);

    extendedSum = (quantityValue * rateValue) - discountValue;
    
    $form.find('.costgridextended').eq(1).val(extendedSum);
  };
}
//---------------------------------------------------------------------------------
var RepairController = new Repair();