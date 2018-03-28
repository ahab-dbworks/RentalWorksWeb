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
  renderGrids = ($form: any) => {

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
      this.calculateCostTotals($form);
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
    // runs after grid load, add, and delete
    FwBrowse.addEventHandler($repairPartGridControl, 'afterdatabindcallback', () => {
      this.calculatePartTotals($form);
    });
    FwBrowse.init($repairPartGridControl); 
    FwBrowse.renderRuntimeHtml($repairPartGridControl);
  } 

  //----------------------------------------------------------------------------------------------


  openBrowse = () => {

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

    let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

    this.ActiveView = 'WarehouseId=' + warehouse.warehouseid;

    $browse.data('ondatabind', request => {
        request.activeview = this.ActiveView;
    });

    //FwAppData.apiMethod(true, 'GET', "api/v1/inventorystatus", null, FwServices.defaultTimeout, function onSuccess(response) {
    //    for (var i = 0; i < response.length; i++) {
    //        FwBrowse.addLegend($browse, response[i].InventoryStatus, response[i].Color);
    //    }
    //}, null, $browse);
            
    return $browse;
  }
  //----------------------------------------------------------------------------------------------

    addBrowseMenuItems = ($menuObject: any) => {
  
      let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
      let $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false);
      let $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true);

      $all.on('click', () => {
        let $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        this.ActiveView = 'WarehouseId=ALL';
        FwBrowse.databind($browse);
      });
      $userWarehouse.on('click', () => {
        let $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        this.ActiveView = 'WarehouseId=' + warehouse.warehouseid;
        FwBrowse.databind($browse);
      });
      
      let viewSubitems: Array<JQuery> = [];
      viewSubitems.push($userWarehouse);
      viewSubitems.push($all);

      let $view;
      $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

      return $menuObject;
    };
  //----------------------------------------------------------------------------------------------
  openForm(mode: string) {
    let $form;
    $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
    $form = FwModule.openForm($form, mode);

    if (mode === 'NEW') {
      $form.find('.ifnew').attr('data-enabled', 'true');
      let today = new Date(Date.now()).toLocaleString().split(',')[0];
      let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
      let office = JSON.parse(sessionStorage.getItem('location'));
      let department = JSON.parse(sessionStorage.getItem('department'));

      FwFormField.setValueByDataField($form, 'RepairDate', today);
      //FwFormField.setValueByDataField($form, 'EstimatedStartDate', date[0]);
      //FwFormField.setValueByDataField($form, 'EstimatedStopDate', date[0]);
      FwFormField.setValueByDataField($form, 'Location', office.location);
      FwFormField.setValueByDataField($form, 'Warehouse', warehouse.warehouse);
      FwFormField.setValueByDataField($form, 'Quantity', 1);


      $form.find('div[data-browsedisplayfield="BarCode"]').attr('data-enabled', true);
      $form.find('div[data-browsedisplayfield="SerialNumber"]').attr('data-enabled', true);
      $form.find('div[data-browsedisplayfield="ICode"]').attr('data-enabled', true);
      $form.find('div[data-browsedisplayfield="RfId"]').attr('data-enabled', true);

      //$form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
      //$form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);

      //FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid);
      //FwFormField.setValueByDataField($form, 'OfficeLocationId', office.locationid);
      //FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid);
      $form.find('div[data-datafield="Department"] input').val(department.department);


      $form.find('div[data-datafield="PoPending"] input').prop('checked', true);
      //FwFormField.disable($form.find('[data-datafield="PoNumber"]'));
      //FwFormField.disable($form.find('[data-datafield="PoAmount"]'));
   
      $form.find('div[data-datafield="ItemId"]').data('onchange', $tr => {
          FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
          //FwFormField.setValue($form, 'div[data-browsedisplayfield="BarCode"]', $tr.find('.field[data-formdatafield="BarCode"]').attr('data-originalvalue'));
          //FwFormField.setValue($form, 'div[data-browsedisplayfield="SerialNumber"]', $tr.find('.field[data-formdatafield="SerialNumber"]').attr('data-originalvalue'));
          //FwFormField.setValue($form, 'div[data-browsedisplayfield="RfId"]', $tr.find('.field[data-formdatafield="RfId"]').attr('data-originalvalue'));
          FwFormField.setValue($form, 'div[data-datafield="InventoryId"]', $tr.find('.field[data-formdatafield="ICode"]').attr('data-originalvalue'));
      });
      // ICode Validation
      $form.find('div[data-datafield="InventoryId"]').data('onchange', $tr => {
          FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
          FwFormField.setValue($form, 'div[data-browsedisplayfield="ICode"]', $tr.find('.field[data-browsedatafield="ICode"]').attr('data-originalvalue'));
      });

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
  saveForm = ($form: any, closetab: boolean, navigationpath: string) => {
    FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
  };

  //----------------------------------------------------------------------------------------------
  afterLoad = ($form: any, $browse: any) => { 
    let $repairCostGrid: any = $form.find('[data-name="RepairCostGrid"]'); 
    FwBrowse.search($repairCostGrid); 
    let $repairPartGrid: any = $form.find('[data-name="RepairPartGrid"]'); 
    FwBrowse.search($repairPartGrid);
  };
  //----------------------------------------------------------------------------------------------
  calculateCostTotals = ($form: any) => {
    let extendedColumn: any = $form.find('.costgridextended');
    let totalSumFromExtended: any = 0;

    for (let i = 1; i < extendedColumn.length; i++) {
      let inputValueFromExtended: any = parseInt($form.find('.costgridextended').eq(i).attr('data-originalvalue'));
      totalSumFromExtended += inputValueFromExtended;
    }
    $form.find('[data-totalfield="CostTotal"] input').val(totalSumFromExtended);
  };
  //----------------------------------------------------------------------------------------------
  calculatePartTotals = ($form: any) => {
    let extendedColumn: any = $form.find('.partgridextended');
    let totalSumFromExtended: any = 0;

    for (let i = 1; i < extendedColumn.length; i++) {
      let inputValueFromExtended: any = parseInt($form.find('.partgridextended').eq(i).attr('data-originalvalue'));
      totalSumFromExtended += inputValueFromExtended;
    }
    $form.find('[data-totalfield="PartTotal"] input').val(totalSumFromExtended);
  };
  //----------------------------------------------------------------------------------------------
  calculateExtended = ($form: any) => {
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
  //------------------------------------------------------------------------------------------------
var RepairController = new Repair();