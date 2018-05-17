routes.push({ pattern: /^module\/repair$/, action: function (match: RegExpExecArray) { return RepairController.getModuleScreen(); } });
routes.push({ pattern: /^module\/repair\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return RepairController.getModuleScreen(filter); } });

//---------------------------------------------------------------------------------
class Repair {
    Module: string = 'Repair';
    apiurl: string = 'api/v1/repair';
    caption: string = 'Repair Order';
    ActiveView: string = 'ALL';
    
    getModuleScreen = (filter?: {datafield: string, search: string}) => {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        let $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            // Dashboard search
            if (typeof filter !== 'undefined') {
                let datafields = filter.datafield.split('%20');
                for (var i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                let parsedSearch = filter.search.replace(/%20/g, " ");
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(parsedSearch);
            }

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

      this.ActiveView = 'WarehouseId=ALL';

      $browse.data('ondatabind', request => {
          request.activeview = this.ActiveView;
      });

      FwAppData.apiMethod(true, 'GET', "api/v1/inventorystatus", null, FwServices.defaultTimeout, function onSuccess(response) {
          const out = response.filter(item => {
              return item.StatusType === 'OUT' 
          })
          const intransit = response.filter(item => {
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
          this.calculateTotals($form, 'cost');
      });
      
      $form.find('.costgridnumber').on('change', $tr => {
         console.log("change in costgridnumber")
      });
   
      FwBrowse.init($repairCostGridControl); 
      FwBrowse.renderRuntimeHtml($repairCostGridControl);

      // Potentially can be used to update grid fields in real-time
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
          this.calculateTotals($form, 'part');
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
      const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', true);
      const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, false);
      let view = [];
      view[0] = 'WarehouseId=ALL';

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
      viewSubItems.push($all);
      viewSubItems.push($userWarehouse);

      let $view;
      $view = FwMenu.addViewBtn($menuObject, 'Warehouse', viewSubItems);

      return $menuObject;
  };

  //----------------------------------------------------------------------------------------------
  openForm = (mode: string) => {
      let $form;
      $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
      $form = FwModule.openForm($form, mode);

      $form.find('.warehouseid').hide();
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
          $form.find('.releasesection').hide();

          const today = new Date(Date.now()).toLocaleString().split(',')[0];
          const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
          const office = JSON.parse(sessionStorage.getItem('location'));
          const department = JSON.parse(sessionStorage.getItem('department'));
          const userId = JSON.parse(sessionStorage.getItem('userid'));
          const locationId = JSON.parse(sessionStorage.getItem('location'));

          FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid,  department.department);
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
  saveForm = ($form: any, parameters: any) => {
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
    
      FwFormField.disable($form.find('div[data-displayfield="BarCode"]'));
      FwFormField.disable($form.find('div[data-displayfield="SerialNumber"]'));
      FwFormField.disable($form.find('div[data-displayfield="ICode"]'));
      FwFormField.disable($form.find('div[data-displayfield="RfId"]'));
      FwFormField.disable($form.find('div[data-displayfield="DamageOrderNumber"]'));
      FwFormField.disable($form.find('div[data-datafield="AvailFor"]'));
      FwFormField.disable($form.find('div[data-datafield="RepairType"]'));           
      FwFormField.disable($form.find('div[data-datafield="PendingRepair"]'));
  };

  //----------------------------------------------------------------------------------------------
  estimateOrder($form) {
      var self = this;
      let $confirmation, $yes, $no;
      $confirmation = FwConfirmation.renderConfirmation('Estimate', '');
      $confirmation.find('.fwconfirmationbox').css('width', '450px');
      let html = [];

     if ($form.data('hasCompleted') === true) {   
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>This Repair Order has already been completed and cannot be unestimated.</div>');
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));
        $no = FwConfirmation.addButton($confirmation, 'OK');

      }

    else if ($form.data('hasEstimated') === true) {
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>Would you like to cancel this estimate for this Repair Order?</div>');
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));

        $yes = FwConfirmation.addButton($confirmation, 'Cancel Estimate', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', cancelEstimate);

    } else {
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>Would you like to make an estimate for this Repair Order?</div>');
        html.push('  </div>');
        html.push('</div>');

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
              FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Estimated');
              FwConfirmation.destroyConfirmation($confirmation);
              FwModule.refreshForm($form, self);
          }, function onError(response) {
              $yes.on('click', makeEstimate);
              $yes.text('Estimate');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
              FwModule.refreshForm($form, self);
          }, $form);

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
              FwModule.refreshForm($form, self);
          }, function onError(response) {
              $yes.on('click', cancelEstimate);
              $yes.text('Cancel Estimate');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
              FwModule.refreshForm($form, self);
          }, $form);
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
          html.push('    <div>This Repair Order has already been completed.</div>');
          html.push('  </div>');
          html.push('</div>');

          FwConfirmation.addControls($confirmation, html.join(''));
          $no = FwConfirmation.addButton($confirmation, 'OK');

      } else if ($form.data('hasEstimated') === true) {
          html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
          html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
          html.push('    <div>Would you like to complete this Repair Order?</div>');
          html.push('  </div>');
          html.push('</div>');

          FwConfirmation.addControls($confirmation, html.join(''));
          $yes = FwConfirmation.addButton($confirmation, 'Complete', false);
          $no = FwConfirmation.addButton($confirmation, 'Cancel');

          $yes.on('click', makeComplete);

      } else {
          html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
          html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
          html.push('    <div>Not yet estimated. Do you want to make estimate and complete this Repair Order?</div>');
          html.push('  </div>');
          html.push('</div>');

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
              FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Completed');
              FwConfirmation.destroyConfirmation($confirmation);
              FwModule.refreshForm($form, self);
              $form.data('hasCompleted', true);
          }, function onError(response) {
              $yes.on('click', makeComplete);
              $yes.text('Complete');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
              FwModule.refreshForm($form, self);
              $form.data('hasCompleted', true);
          }, $form);
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
      html.push('    <div>Would you like to void this Repair Order?</div>');
      html.push('  </div>');
      html.push('</div>');

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
              FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Voided');
              FwConfirmation.destroyConfirmation($confirmation);
              FwModule.refreshForm($form, self);
          }, function onError(response) {
              $yes.on('click', makeVoid);
              $yes.text('Void');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
              FwModule.refreshForm($form, self);
          }, $form);
                  
      };
  };

  //----------------------------------------------------------------------------------------------
  releaseItems($form) {
    var self = this;
    let $confirmation, $yes, $no;
    const releasedQuantityForm = +FwFormField.getValueByDataField($form, 'ReleasedQuantity');
    const quantityForm = +FwFormField.getValueByDataField($form, 'Quantity');
    const totalQuantity = quantityForm - releasedQuantityForm;
    $confirmation = FwConfirmation.renderConfirmation('Release Items', '');
    $confirmation.find('.fwconfirmationbox').css('width', '450px');
    let html = [];

    if (quantityForm > releasedQuantityForm && quantityForm > 0 ) {
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

        FwConfirmation.addControls($confirmation, html.join(''));

        const ICode = $form.find('[data-datafield="InventoryId"] input.fwformfield-text').val();
        $confirmation.find('div[data-caption="I-Code"] input').val(ICode);

        const ItemDescription = FwFormField.getValueByDataField($form, 'ItemDescription');
        $confirmation.find('div[data-caption="Description"] input').val(ItemDescription);

        const Quantity = +FwFormField.getValueByDataField($form, 'Quantity');
        $confirmation.find('div[data-caption="Quantity"] input').val(Quantity);

        const ReleasedQuantity = +FwFormField.getValueByDataField($form, 'ReleasedQuantity');
        $confirmation.find('div[data-caption="Released"] input').val(ReleasedQuantity);

        const QuantityToRelease = Quantity - ReleasedQuantity;
        $confirmation.find('div[data-caption="Quantity to Release"] input').val(QuantityToRelease);

        FwFormField.disable($confirmation.find('div[data-caption="I-Code"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Description"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Quantity"]'));
        FwFormField.disable($confirmation.find('div[data-caption="Released"]'));


        $yes = FwConfirmation.addButton($confirmation, 'Release', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', release);

    } else {
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>There are either no items to release or number chosen is greater than items available.</div>');
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));
 
        $no = FwConfirmation.addButton($confirmation, 'OK');
    }

      function release() {
          let request: any = {};
          const RepairId = FwFormField.getValueByDataField($form, 'RepairId');
          const releasedQuantityConfirmation = +FwFormField.getValueByDataField($confirmation, 'ReleasedQuantity');
        
        if (releasedQuantityConfirmation <= totalQuantity) {
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Releasing...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST',  `api/v1/repair/releaseitems/${RepairId}/${releasedQuantityConfirmation}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Items Successfully Released');
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form, self);
            }, function onError(response) {
                $yes.on('click', release);
                $yes.text('Release');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
            }, $form);
        
        } else {
            FwFunc.showError("You are attempting to release more items than are available.");
        }
      };
  };

  //----------------------------------------------------------------------------------------------
  calculateTotals($form: any, gridType: string) {
      const billableColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="Billable"]');
      const extendedColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="Extended"]');
      const discountColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="DiscountAmount"]');
      const taxColumn: any = $form.find('.' + gridType + 'grid [data-browsedatafield="Tax"]');
      let totalSumFromExtended: any = 0;
      let totalSumFromDiscount: any = 0;
      let totalSumFromTax: any = 0;

      for (let i = 1; i < billableColumn.length; i++) {
          // Only calculate billable items
          if (billableColumn.eq(i).attr('data-originalvalue') === "true") {
              // Extended Column
              let inputValueFromExtended: any = +extendedColumn.eq(i).attr('data-originalvalue');
              totalSumFromExtended += inputValueFromExtended;
              // DiscountAmount Column
              let inputValueFromDiscount: any = +discountColumn.eq(i).attr('data-originalvalue');
              totalSumFromDiscount += inputValueFromDiscount;
              // Tax Column
              let inputValueFromTax: any = +taxColumn.eq(i).attr('data-originalvalue');
              totalSumFromTax += inputValueFromTax;
          }
      }

      totalSumFromExtended = +totalSumFromExtended.toFixed(2);
      totalSumFromDiscount = +totalSumFromDiscount.toFixed(2);
      totalSumFromTax = +totalSumFromTax.toFixed(2);

      $form.find('.' + gridType + 'totals [data-totalfield="SubTotal"] input').val(totalSumFromExtended);
      $form.find('.' + gridType + 'totals [data-totalfield="Discount"] input').val(totalSumFromDiscount);
      $form.find('.' + gridType + 'totals [data-totalfield="SalesTax"] input').val(totalSumFromTax);
      $form.find('.' + gridType + 'totals [data-totalfield="GrossTotal"] input').val(totalSumFromExtended + totalSumFromDiscount);
      $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val(totalSumFromTax + totalSumFromExtended);
  };

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
                    TrackedBy: 'QUANTITY',
                };
                break;
            case 'SalesInventoryValidation':
                request.uniqueids = {
                    Classification: 'I',
                    TrackedBy: 'QUANTITY',
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

//---------------------------------------------------------------------------------
//Browse Void Option
FwApplicationTree.clickEvents['{AFA36551-F49E-4FB9-84DD-A54A423CCFF3}'] = function (event) {
    var $browse, repairId;
    try {
        $browse = jQuery(this).closest('.fwbrowse');
        const RepairId = $browse.find('.selected [data-browsedatafield="RepairId"]').attr('data-originalvalue');
        if (RepairId != null) {
              var self = this;
              let $confirmation, $yes, $no;
              $confirmation = FwConfirmation.renderConfirmation('Void', '');
              $confirmation.find('.fwconfirmationbox').css('width', '450px');
              let html = [];
              html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
              html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
              html.push('    <div>Would you like to void this Repair Order?</div>');
              html.push('  </div>');
              html.push('</div>');

              FwConfirmation.addControls($confirmation, html.join(''));
              $yes = FwConfirmation.addButton($confirmation, 'Void', false);
              $no = FwConfirmation.addButton($confirmation, 'Cancel');

              $yes.on('click', makeVoid);

              function makeVoid() {
                  let request: any = {};

                  FwFormField.disable($confirmation.find('.fwformfield'));
                  FwFormField.disable($yes);
                  $yes.text('Voiding...');
                  $yes.off('click');

                  FwAppData.apiMethod(true, 'POST',  `api/v1/repair/void/${RepairId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                      FwNotification.renderNotification('SUCCESS', 'Repair Order Successfully Voided');
                      FwConfirmation.destroyConfirmation($confirmation);
                      FwBrowse.databind($browse);
                  }, function onError(response) {
                      $yes.on('click', makeVoid);
                      $yes.text('Void');
                      FwFunc.showError(response);
                      FwFormField.enable($confirmation.find('.fwformfield'));
                      FwFormField.enable($yes);
                      FwBrowse.databind($browse);
                  }, $browse);
                  
              };
        } else {
            throw new Error("Please select a Repair Order to void.");
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//------------------------------------------------------------------------------------------------
var RepairController = new Repair();