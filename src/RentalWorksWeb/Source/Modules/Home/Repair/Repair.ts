routes.push({ pattern: /^module\/repair$/, action: function (match: RegExpExecArray) { return RepairController.getModuleScreen(); } });
// routes.push({ pattern: /^module\/repair\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return RepairController.getModuleScreen(filter); } });

//---------------------------------------------------------------------------------
class Repair {
    Module: string = 'Repair';
    apiurl: string = 'api/v1/repair';
    caption: string = 'Repair Order';
    ActiveView: string = 'ALL';
    
    getModuleScreen = () => {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        let $browse: JQuery = this.openBrowse();

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
  openBrowse = () => {

    let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
    $browse = FwModule.openBrowse($browse);

    let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
    this.ActiveView = 'WarehouseId=' + warehouse.warehouseid;

    $browse.data('ondatabind', request => {
        request.activeview = this.ActiveView;
    });

    FwAppData.apiMethod(true, 'GET', "api/v1/inventorystatus", null, FwServices.defaultTimeout, function onSuccess(response) {
      let out = response.filter(item => {
        return item.StatusType === 'OUT' 
      })
      let intransit = response.filter(item => {
        return item.StatusType === 'INTRANSIT' 
      })

        FwBrowse.addLegend($browse, 'Foreign Currency', '#95FFCA');
        FwBrowse.addLegend($browse, 'Priority', '#EA300F');
        FwBrowse.addLegend($browse, 'Not Billed', '#0fb70c');
        FwBrowse.addLegend($browse, 'Billable', '#0c6fcc');
        FwBrowse.addLegend($browse, 'Outside', '#fffb38');
        FwBrowse.addLegend($browse, 'Released', '#d6d319');
        FwBrowse.addLegend($browse, 'Pending Repair', out[0].Color);
        FwBrowse.addLegend($browse, 'Transferred', intransit[0].Color);
        }, null, $browse);
            
    return $browse;
  }

  //----------------------------------------------------------------------------------------------
  renderGrids = ($form: any) => {

      let $repairCostGrid: any;
      let $repairCostGridControl: any; 
      let $repairPartGrid: any;
      let $repairPartGridControl: any; 
      let $repairReleaseGrid: any;
      let $repairReleaseGridControl: any; 

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
      
      $form.find('.costgridnumber').on('change', $tr => {
         console.log("change")
      });
   
      FwBrowse.init($repairCostGridControl); 
      FwBrowse.renderRuntimeHtml($repairCostGridControl);

      //$repairCostGridControl.on('change', '[data-browsedatafield="DiscountAmount"] input.value', () => {
      //  alert('test');
      //});
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
     
      //----------------------------------------------------------------------------------------------
      $repairReleaseGrid = $form.find('div[data-grid="RepairReleaseGrid"]'); 
      $repairReleaseGridControl = jQuery(jQuery('#tmpl-grids-RepairReleaseGridBrowse').html()); 
      $repairReleaseGrid.empty().append($repairReleaseGridControl); 
      $repairReleaseGridControl.data('ondatabind', request => { 
          request.uniqueids = { 
              RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val() 
          } 
      }); 
      $repairReleaseGridControl.data('beforesave', request => { 
          request.RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 
      }) 
   
      FwBrowse.init($repairReleaseGridControl); 
      FwBrowse.renderRuntimeHtml($repairReleaseGridControl);
  } 

  //----------------------------------------------------------------------------------------------
  addBrowseMenuItems = ($menuObject: any) => {
      let self = this;
      const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
      const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false);
      const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true);
      let view = [];
      view[0] = 'WarehouseId=' + warehouse.warehouseid;

      $all.on('click', function() {
          let $browse;
          $browse = jQuery(this).closest('.fwbrowse');
          self.ActiveView = 'WarehouseId=ALL';

          FwBrowse.search($browse);      
      });

      $userWarehouse.on('click', function() {
          let $browse;
          $browse = jQuery(this).closest('.fwbrowse');
          self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;

          FwBrowse.search($browse);      
      });
      
      let viewSubItems: Array<JQuery> = [];
      viewSubItems.push($userWarehouse);
      viewSubItems.push($all);

      let $view;
      $view = FwMenu.addViewBtn($menuObject, 'View', viewSubItems);

      return $menuObject;
  };

  //----------------------------------------------------------------------------------------------
  openForm(mode: string) {
      let $form;
      $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
      $form = FwModule.openForm($form, mode);

      $form.find('.warehouseid').hide();
      $form.find('.departmentid').hide();
      $form.find('.locationid').hide();
      $form.find('.inputbyuserid').hide();
      $form.find('.icodesales').hide();

      // Tax Option Validation
      $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
          FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-formdatafield="RentalTaxRate1"]').attr('data-originalvalue'));
          FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-formdatafield="SalesTaxRate1"]').attr('data-originalvalue'));
          FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-formdatafield="LaborTaxRate1"]').attr('data-originalvalue'));
      });

      // Complete / Estimate
      $form.find('.complete').on('click', $tr => {
          this.completeOrder($form);  
      });

      $form.find('.estimate').on('click', $tr => {
         this.estimateOrder($form);
      });
                                                                   
      // Release Items
      $form.find('.releaseitems').on('click', $tr => {
         this.releaseItems($form);
      });

      // New Orders
      if (mode === 'NEW') {
          $form.find('.ifnew').attr('data-enabled', 'true');
          $form.find('.completeestimate').hide();

          const today = new Date(Date.now()).toLocaleString().split(',')[0];
          const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
          const office = JSON.parse(sessionStorage.getItem('location'));
          const department = JSON.parse(sessionStorage.getItem('department'));
          const userId = JSON.parse(sessionStorage.getItem('userid'));
          const locationId = JSON.parse(sessionStorage.getItem('location'));

          $form.find('div[data-displayfield="Department"] input').val(department.department);
          FwFormField.setValue($form, '.departmentid', department.departmentid);
          FwFormField.setValueByDataField($form, 'Priority', 'MED');          
          FwFormField.setValueByDataField($form, 'RepairDate', today);
          FwFormField.setValueByDataField($form, 'Location', office.location);
          FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid);
          FwFormField.setValueByDataField($form, 'Warehouse', warehouse.warehouse);
          FwFormField.setValueByDataField($form, 'Quantity', 1);
          FwFormField.setValueByDataField($form, 'InputByUserId', userId.webusersid);
          FwFormField.setValueByDataField($form, 'LocationId', locationId.locationid);

          $form.find('div[data-datafield="PendingRepair"] input').prop('checked', false);
          $form.find('div[data-datafield="PoPending"] input').prop('checked', true);
          FwFormField.enable($form.find('div[data-displayfield="BarCode"]'));
          FwFormField.enable($form.find('div[data-displayfield="SerialNumber"]'));
          FwFormField.enable($form.find('div[data-displayfield="ICode"]'));
          FwFormField.enable($form.find('div[data-displayfield="RfId"]'));
          FwFormField.enable($form.find('div[data-displayfield="DamageOrderNumber"]'));
          FwFormField.enable($form.find('div[data-datafield="AvailFor"]'));
          FwFormField.enable($form.find('div[data-datafield="RepairType"]'));           
          FwFormField.enable($form.find('div[data-datafield="PendingRepair"]'));

          // BarCode, SN, RFID Validation
          $form.find('div[data-datafield="ItemId"]').data('onchange', $tr => {
              FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-displayfield="BarCode"]',$tr.find('.field[data-formdatafield="ItemId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="BarCode"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-displayfield="ICode"]',$tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="ICode"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-displayfield="SerialNumber"] ',$tr.find('.field[data-formdatafield="ItemId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="SerialNumber"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-displayfield="RfId"]',$tr.find('.field[data-formdatafield="ItemId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="RfId"]').attr('data-originalvalue'));
              FwFormField.disable($form.find('div[data-displayfield="ICode"]'));
          });

          // ICode Validation
          $form.find('div[data-datafield="InventoryId"]').data('onchange', $tr => {
              FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
              FwFormField.enable($form.find('div[data-datafield="Quantity"]'));
              FwFormField.disable($form.find('div[data-displayfield="BarCode"]'));
              FwFormField.disable($form.find('div[data-displayfield="SerialNumber"]'));
              FwFormField.disable($form.find('div[data-displayfield="RfId"]'));

              if (FwFormField.getValue($form, '.repairavailforradio') === 'S') {
                  FwFormField.setValue($form, '.icoderental', $tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'));
              } 
              else { 
                  FwFormField.setValue($form, '.icodesales', $tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'));
              }
          });

          // Department Validation
          $form.find('div[data-datafield="DepartmentId"]').data('onchange', $tr => {
              FwFormField.setValue($form, '.departmentid', $tr.find('.field[data-formdatafield="DepartmentId"]').attr('data-originalvalue'));
          });

          // Order Validation
          $form.find('div[data-datafield="DamageOrderId"]').data('onchange', $tr => {
              FwFormField.setValue($form, 'div[data-datafield="DamageOrderDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
              FwFormField.setValue($form, 'div[data-datafield="DamageDeal"]', $tr.find('.field[data-formdatafield="Deal"]').attr('data-originalvalue'));
          });

          // Sales or Rent Order
         $form.find('.repairavailforradio').on('change', $tr => {
              if (FwFormField.getValueByDataField($form, 'RepairType') === 'OWNED') {
                  if (FwFormField.getValue($form, '.repairavailforradio') === 'S') {
                      $form.find('.icodesales').show();
                      $form.find('.icoderental').hide();              
              }
                  else {
                      $form.find('.icodesales').hide();
                      $form.find('.icoderental').show();              
                  }
                
              }
         });

         // Repair Type Change
         $form.find('.repairtyperadio').on('change', $tr => {
              if (FwFormField.getValueByDataField($form, 'RepairType') === 'OUTSIDE') {
                  $form.find('.itemid').hide();
                  FwFormField.disable($form.find('div[data-datafield="AvailFor"]'));
              }
              else {
                  $form.find('.itemid').show();
                  FwFormField.enable($form.find('div[data-datafield="AvailFor"]'));
              }
         });

          // Pending PO Number
          $form.find('[data-datafield="PoPending"] .fwformfield-value').on('change', function () {
              var $this = jQuery(this);
              if ($this.prop('checked') === true) {
                  FwFormField.disable($form.find('div[data-datafield="PoNumber"]'));
              }
              else {
                 FwFormField.enable($form.find('div[data-datafield="PoNumber"]'));
              }
          });

          FwFormField.disable($form.find('.frame'));
          $form.find(".frame .add-on").children().hide();
      }

      this.events($form);
      return $form;
  };

  //----------------------------------------------------------------------------------------------
  loadForm = (uniqueids: any) => {
      let $form: JQuery = this.openForm('EDIT');
      $form = this.openForm('EDIT');
      $form.find('div.fwformfield[data-datafield="RepairId"] input').val(uniqueids.RepairId);
      FwModule.loadForm(this.Module, $form);

      $form.find('[data-datafield="PoPending"] .fwformfield-value').on('change', function () {
          var $this = jQuery(this);
          if ($this.prop('checked') === true) {
              FwFormField.disable($form.find('div[data-datafield="PoNumber"]'));
          }
          else {
             FwFormField.enable($form.find('div[data-datafield="PoNumber"]'));
          }
      });
    
      return $form;
  };

  //----------------------------------------------------------------------------------------------
  events($form: JQuery): void {

      $form.find('[data-name="RepairReleaseGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
          try {
              var buildingId = $form.find('div.fwformfield[data-datafield="BuildingId"] input').val();
              var floorId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');

              var $spaceGridControl: any;
              $spaceGridControl = $form.find('[data-name="SpaceGrid"]');
              $spaceGridControl.data('ondatabind', function (request) {
                  request.uniqueids = {
                      BuildingId: buildingId,
                      FloorId: floorId
                  }
              })
              $spaceGridControl.data('beforesave', function (request) {
                  request.BuildingId = buildingId;
                  request.FloorId = floorId;
              });
              FwBrowse.search($spaceGridControl);

          } catch (ex) {
              FwFunc.showError(ex);
          }
      });

    
  }

  //----------------------------------------------------------------------------------------------
  saveForm($form: any, parameters: any) {
      FwModule.saveForm(this.Module, $form, parameters);
  }

  //----------------------------------------------------------------------------------------------
  afterLoad = ($form: any, $browse: any) => { 
      let $repairCostGrid: any = $form.find('[data-name="RepairCostGrid"]'); 
      FwBrowse.search($repairCostGrid); 
      let $repairPartGrid: any = $form.find('[data-name="RepairPartGrid"]'); 
      FwBrowse.search($repairPartGrid);
      let $repairReleaseGrid: any = $form.find('[data-name="RepairReleaseGrid"]'); 
      FwBrowse.search($repairReleaseGrid);

      console.log('STATUS:  ', FwFormField.getValueByDataField($form, 'Status'))

      if (FwFormField.getValueByDataField($form, 'Status') === 'ESTIMATED') {
        $form.data('hasEstimated', true);
      } else {
        $form.data('hasEstimated', false);
      }

      if (FwFormField.getValueByDataField($form, 'Status') === 'COMPLETE') {
        $form.data('hasCompleted', true);
      } else {
        $form.data('hasCompleted', false);
      }

      var $pending = $form.find('div.fwformfield[data-datafield="PoPending"] input').prop('checked');
      if ($pending === true) {
          FwFormField.disable($form.find('div[data-datafield="PoNumber"]'));
      }
      else {
          FwFormField.enable($form.find('div[data-datafield="PoNumber"]'));
      } 
  };

  //----------------------------------------------------------------------------------------------
  estimateOrder($form) {
      var self = this;
      let $confirmation, $yes, $no;
      $confirmation = FwConfirmation.renderConfirmation('Estimate', '');
      $confirmation.find('.fwconfirmationbox').css('width', '450px');
      let html = [];

    if ($form.data('hasEstimated') === true) {
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>Would you like to cancel this estimate for this order?</div>');
        html.push('  </div>');
        html.push('</div>');

        let copyConfirmation = html.join('');
        FwConfirmation.addControls($confirmation, html.join(''));

        $yes = FwConfirmation.addButton($confirmation, 'Cancel Estimate', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', cancelEstimate);

    } else {
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>Would you like to make an estimate for this order?</div>');
        html.push('  </div>');
        html.push('</div>');

        let copyConfirmation = html.join('');
        FwConfirmation.addControls($confirmation, html.join(''));

        $yes = FwConfirmation.addButton($confirmation, 'Estimate', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', makeEstimate);
    }
   
      function makeEstimate() {
          $form.data('hasEstimated', true);
          let request: any = {};
          const RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 

          FwFormField.disable($confirmation.find('.fwformfield'));
          FwFormField.disable($yes);

          $yes.text('Estimating...');
          $yes.off('click');

          FwAppData.apiMethod(true, 'POST',  `api/v1/repair/estimate/${RepairId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
              FwNotification.renderNotification('SUCCESS', 'Order Successfully Estimated');
              FwConfirmation.destroyConfirmation($confirmation);
          }, function onError(response) {
              $yes.on('click', makeEstimate);
              $yes.text('Estimate');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
          }, $form);

          FwModule.refreshForm($form, self);
      };
      function cancelEstimate() {
          $form.data('hasEstimated', false)
          let request: any = {};
          const RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 

          FwFormField.disable($confirmation.find('.fwformfield'));
          FwFormField.disable($yes);

          $yes.text('Canceling Estimate...');
          $yes.off('click');

          FwAppData.apiMethod(true, 'POST',  `api/v1/repair/estimate/${RepairId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
              FwNotification.renderNotification('SUCCESS', 'Estimate Successfully Cancelled');
              FwConfirmation.destroyConfirmation($confirmation);
          }, function onError(response) {
              $yes.on('click', cancelEstimate);
              $yes.text('Cancel Estimate');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
          }, $form);

          FwModule.refreshForm($form, self);
      };
  };

  //----------------------------------------------------------------------------------------------
  completeOrder($form) {
      var self = this;
      let $confirmation, $yes, $no;
      $confirmation = FwConfirmation.renderConfirmation('Complete', '');
      $confirmation.find('.fwconfirmationbox').css('width', '450px');
      let html = [];

      if ($form.data('hasCompleted') === true) {   
          html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
          html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
          html.push('    <div>This order has already been completed.</div>');
          html.push('  </div>');
          html.push('</div>');

          let copyConfirmation = html.join('');

          FwConfirmation.addControls($confirmation, html.join(''));
 
          $no = FwConfirmation.addButton($confirmation, 'OK');

      }

       else if ($form.data('hasEstimated') === true) {
          html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
          html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
          html.push('    <div>Would you like to complete this order?</div>');
          html.push('  </div>');
          html.push('</div>');

          let copyConfirmation = html.join('');

          FwConfirmation.addControls($confirmation, html.join(''));
          $yes = FwConfirmation.addButton($confirmation, 'Complete', false);
          $no = FwConfirmation.addButton($confirmation, 'Cancel');

          $yes.on('click', makeComplete);

      } else {
          html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
          html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
          html.push('    <div>Not yet estimated. Do you want to make estimate and complete this order?</div>');
          html.push('  </div>');
          html.push('</div>');

          let copyConfirmation = html.join('');

          FwConfirmation.addControls($confirmation, html.join(''));
          $yes = FwConfirmation.addButton($confirmation, 'Complete', false);
          $no = FwConfirmation.addButton($confirmation, 'Cancel');

          $yes.on('click', makeComplete);
      }

      function makeComplete() {
          let request: any = {};
          const RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 

          FwFormField.disable($confirmation.find('.fwformfield'));
          FwFormField.disable($yes);
          $yes.text('Completing...');
          $yes.off('click');

          FwAppData.apiMethod(true, 'POST',  `api/v1/repair/complete/${RepairId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
              FwNotification.renderNotification('SUCCESS', 'Order Successfully Completed');
              FwConfirmation.destroyConfirmation($confirmation);
          }, function onError(response) {
              $yes.on('click', makeComplete);
              $yes.text('Complete');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
          }, $form);
          $form.data('hasCompleted', true);
          FwModule.refreshForm($form, self);
      };
  };

  //----------------------------------------------------------------------------------------------
  voidOrder($form) {
      var self = this;
      let $confirmation, $yes, $no;
      $confirmation = FwConfirmation.renderConfirmation('Void', '');
      $confirmation.find('.fwconfirmationbox').css('width', '450px');
      let html = [];
      html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
      html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
      html.push('    <div>Would you like to void this order?</div>');
      html.push('  </div>');
      html.push('</div>');

      let copyConfirmation = html.join('');

      FwConfirmation.addControls($confirmation, html.join(''));
      $yes = FwConfirmation.addButton($confirmation, 'Void', false);
      $no = FwConfirmation.addButton($confirmation, 'Cancel');

      $yes.on('click', makeVoid);

      function makeVoid() {
          let request: any = {};
          const RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 

          FwFormField.disable($confirmation.find('.fwformfield'));
          FwFormField.disable($yes);
          $yes.text('Voiding...');
          $yes.off('click');

          FwAppData.apiMethod(true, 'POST',  `api/v1/repair/void/${RepairId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
              FwNotification.renderNotification('SUCCESS', 'Order Successfully Voided');
              FwConfirmation.destroyConfirmation($confirmation);
          }, function onError(response) {
              $yes.on('click', makeVoid);
              $yes.text('Void');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
          }, $form);
                  
          FwModule.refreshForm($form, self);

      };
  };

  //----------------------------------------------------------------------------------------------
  releaseItems($form) {
    var self = this;
      let $confirmation, $yes, $no;
      $confirmation = FwConfirmation.renderConfirmation('Release Items', '');
      $confirmation.find('.fwconfirmationbox').css('width', '450px');
      let html = [];
      html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
      html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
      html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" style="width:90px;float:left;"></div>');
      html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="ItemDescription" style="width:340px; float:left;"></div>');
      html.push('  </div>');
      html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
      html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" style="width:75px; float:left;"></div>');
      html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Released" data-datafield="Released" style="width:75px;float:left;"></div>');
      html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity to Release" data-datafield="ReleasedQuantity" data-enabled="true" style="width:75px;float:left;"></div>');
      html.push('  </div>');
      html.push('</div>');

      let copyConfirmation = html.join('');

      FwConfirmation.addControls($confirmation, html.join(''));

      const ICode = $form.find('[data-datafield="InventoryId"] input.fwformfield-text').val();
      $confirmation.find('div[data-caption="I-Code"] input').val(ICode);

      const ItemDescription = FwFormField.getValueByDataField($form, 'ItemDescription');
      $confirmation.find('div[data-caption="Description"] input').val(ItemDescription);

      const Quantity = FwFormField.getValueByDataField($form, 'Quantity');
      $confirmation.find('div[data-caption="Quantity"] input').val(Quantity);

      FwFormField.disable($confirmation.find('div[data-caption="I-Code"]'));
      FwFormField.disable($confirmation.find('div[data-caption="Description"]'));
      FwFormField.disable($confirmation.find('div[data-caption="Quantity"]'));
      FwFormField.disable($confirmation.find('div[data-caption="Released"]'));


      $yes = FwConfirmation.addButton($confirmation, 'Release', false);
      $no = FwConfirmation.addButton($confirmation, 'Cancel');

      $yes.on('click', release);

      function release() {
          let request: any = {};
          const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
          const ReleasedQuantity = FwFormField.getValueByDataField($confirmation, 'ReleasedQuantity');

        console.log(ReleasedQuantity, 'ReleasedQuantity')
        
          FwFormField.disable($confirmation.find('.fwformfield'));
          FwFormField.disable($yes);
          $yes.text('Releasing...');
          $yes.off('click');

          FwAppData.apiMethod(true, 'POST',  `api/v1/repair/releaseitems/${RepairId}/${ReleasedQuantity}`, request, FwServices.defaultTimeout, function onSuccess(response) {
              FwNotification.renderNotification('SUCCESS', 'Items Successfully Released');
              FwConfirmation.destroyConfirmation($confirmation);
          }, function onError(response) {
              $yes.on('click', release);
              $yes.text('Release');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
          }, $form);

          FwModule.refreshForm($form, self);
      };
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

  //----------------------------------------------------------------------------------------------
  beforeValidate = ($browse, $grid, request) => {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        switch (validationName) {
            case 'AssetValidation':
                request.uniqueids = {
                    WarehouseId: warehouse.warehouseid
                };
                break;
            case 'RentalInventoryValidation':
                request.uniqueids = {
                    Classification: 'I',
                    TrackedBy: 'QUANTITY'
                };
                break;
            case 'SalesInventoryValidation':
                request.uniqueids = {
                    Classification: 'I',
                    TrackedBy: 'QUANTITY'
                };
                break;
        };
    }
}

// using COMPLETE security guid
FwApplicationTree.clickEvents['{6EE5D9E2-8075-43A6-8E81-E2BCA99B4308}'] = function (event) {
    let $form
    $form = jQuery(this).closest('.fwform');

    try {
        RepairController.completeOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

// using ESTIMATE security guid
FwApplicationTree.clickEvents['{AEDCEB81-2A5A-4779-8A88-25FD48E88E6A}'] = function (event) {
    let $form
    $form = jQuery(this).closest('.fwform');

    try {
        RepairController.estimateOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

// using VOID security guid
FwApplicationTree.clickEvents['{9F58C03B-89CD-484A-8332-CDBF9961A258}'] = function (event) {
    let $form
    $form = jQuery(this).closest('.fwform');

    try {
        RepairController.voidOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

// using RELEASE security guid
FwApplicationTree.clickEvents['{EE709549-C91C-473E-96CC-2DB121082FB5}'] = function (event) {
    let $form
    $form = jQuery(this).closest('.fwform');

    try {
        RepairController.releaseItems($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//------------------------------------------------------------------------------------------------
var RepairController = new Repair();