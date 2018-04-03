routes.push({ pattern: /^module\/repair$/, action: function (match: RegExpExecArray) { return RepairController.getModuleScreen(); } });
// routes.push({ pattern: /^module\/repair\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return RepairController.getModuleScreen(filter); } });
//---------------------------------------------------------------------------------
class Repair {
    Module: string = 'Repair';
    apiurl: string = 'api/v1/repair';
    caption: string = 'Repair Order';
    ActiveView: string;

    getModuleScreen = () => {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = () => {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };

  //----------------------------------------------------------------------------------------------
  renderGrids = ($form: any) => {

      let $repairCostGrid, $repairCostGridControl; 
      let $repairPartGrid, $repairPartGridControl; 

      //----------------------------------------------------------------------------------------------
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
            
    return $browse;
  }
  //----------------------------------------------------------------------------------------------

    addBrowseMenuItems = ($menuObject: any) => {
  
      const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
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
  openForm = (mode: string) => {
      let $form;
      $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
      $form = FwModule.openForm($form, mode);
      $form.find('.icodesales').hide();

      if (mode === 'NEW') {
          $form.find('.ifnew').attr('data-enabled', 'true');
          const today = new Date(Date.now()).toLocaleString().split(',')[0];
          const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
          const office = JSON.parse(sessionStorage.getItem('location'));
          const department = JSON.parse(sessionStorage.getItem('department'));

          $form.find('div[data-datafield="Department"] input').val(department.department);
          FwFormField.setValueByDataField($form, 'RepairDate', today);
          FwFormField.setValueByDataField($form, 'Location', office.location);
          FwFormField.setValueByDataField($form, 'Warehouse', warehouse.warehouse);
          FwFormField.setValueByDataField($form, 'Quantity', 1);

          $form.find('div[data-datafield="PendingRepair"] input').prop('checked', false);
          $form.find('div[data-datafield="PoPending"] input').prop('checked', true);
          FwFormField.enable($form.find('div[data-displayfield="BarCode"]'));
          FwFormField.enable($form.find('div[data-displayfield="SerialNumber"]'));
          FwFormField.enable($form.find('div[data-displayfield="ICode"]'));
          FwFormField.enable($form.find('div[data-displayfield="RfId"]'));
          FwFormField.enable($form.find('div[data-displayfield="DamageOrderNumber"]'));

        // BarCode, SN, RFID Validation
          $form.find('div[data-datafield="ItemId"]').data('onchange', $tr => {
              FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-displayfield="BarCode"] ',$tr.find('.field[data-formdatafield="ItemId"]'), $tr.find('.field[data-formdatafield="BarCode"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-displayfield="ICode"] ',$tr.find('.field[data-formdatafield="ItemId"]'), $tr.find('.field[data-formdatafield="ICode"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-displayfield="SerialNumber"] ',$tr.find('.field[data-formdatafield="ItemId"]'), $tr.find('.field[data-formdatafield="SerialNumber"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-displayfield="RfId"] ',$tr.find('.field[data-formdatafield="ItemId"]'), $tr.find('.field[data-formdatafield="RfId"]').attr('data-originalvalue'));
              FwFormField.disable($form.find('div[data-displayfield="ICode"]'));

          });
          // ICode Validation
          $form.find('div[data-datafield="InventoryId"]').data('onchange', $tr => {
              FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
              FwFormField.enable($form.find('div[data-datafield="Quantity"]'));
              FwFormField.disable($form.find('div[data-displayfield="BarCode"]'));
              FwFormField.disable($form.find('div[data-displayfield="SerialNumber"]'));
              FwFormField.disable($form.find('div[data-displayfield="RfId"]'));
          });

          // Order Validation
          $form.find('div[data-datafield="DamageOrderId"]').data('onchange', $tr => {
              FwFormField.setValue($form, 'div[data-datafield="DamageOrderDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-datafield="DamageDeal"]', $tr.find('.field[data-formdatafield="Deal"]').attr('data-originalvalue'));
          });

          // Tax Option Validation
          $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
              FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-formdatafield="RentalTaxRate1"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-formdatafield="SalesTaxRate1"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-formdatafield="LaborTaxRate1"]').attr('data-originalvalue'));
          });

          // Sales or Rent Order
         $form.find('.repairavailforradio').on('change', $tr => {
            if (FwFormField.getValue($form, '.repairavailforradio') === 'S') {
                $form.find('.icodesales').show();
                $form.find('.icoderental').hide();
            }
            else {
                $form.find('.icodesales').hide();
                $form.find('.icoderental').show();
            }
          });

          FwFormField.disable($form.find('.frame'));
          $form.find(".frame .add-on").children().hide();
      }

      return $form;
  };

  //----------------------------------------------------------------------------------------------
  loadForm = (uniqueids: any) => {
      let $form: JQuery = this.openForm('EDIT');
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
          let inputValueFromExtended: any = parseFloat($form.find('.costgridextended').eq(i).attr('data-originalvalue'));
          totalSumFromExtended += inputValueFromExtended;
      }
      $form.find('[data-totalfield="CostTotal"] input').val(totalSumFromExtended);
  };
  //----------------------------------------------------------------------------------------------
  calculatePartTotals = ($form: any) => {
      let extendedColumn: any = $form.find('.partgridextended');
      let totalSumFromExtended: any = 0;

      for (let i = 1; i < extendedColumn.length; i++) {
          let inputValueFromExtended: any = parseFloat($form.find('.partgridextended').eq(i).attr('data-originalvalue'));
          totalSumFromExtended += inputValueFromExtended;
      }
      $form.find('[data-totalfield="PartTotal"] input').val(totalSumFromExtended);
  };
  //----------------------------------------------------------------------------------------------
    //calculateExtended = ($form: any) => {
    //  let extendedSum: any = Number($form.find('.costgridextended').eq(1).attr('data-originalvalue'));
    //  let discountValue: any = parseInt($form.find('.costgriddiscount').eq(1).attr('data-originalvalue'));
    //  let rateValue: any = parseInt($form.find('.costgridrate').eq(1).attr('data-originalvalue'));
    //  let quantityValue: any = parseInt($form.find('.costgridquantity').eq(1).attr('data-originalvalue'));
    //  let extendedSum: any = 0;

    //  console.log("extendedColumn.length: ", extendedColumn.length);
    //  console.log("discountValue: ", discountValue);

    //  extendedSum = (quantityValue * rateValue) - discountValue;
    
    //  $form.find('.costgridextended').eq(1).val(extendedSum);
    //};
}
  //------------------------------------------------------------------------------------------------
var RepairController = new Repair();