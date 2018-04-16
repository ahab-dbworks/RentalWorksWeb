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
      
      $form.find('.costgridnumber').on('change', $tr => {
         console.log("change")
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

      if (mode === 'NEW') {
          $form.find('.ifnew').attr('data-enabled', 'true');
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
  saveForm = ($form: any, closetab: boolean, navigationpath: string) => {
      FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
  };

  //----------------------------------------------------------------------------------------------
  afterLoad = ($form: any, $browse: any) => { 
      let $repairCostGrid: any = $form.find('[data-name="RepairCostGrid"]'); 
      FwBrowse.search($repairCostGrid); 
      let $repairPartGrid: any = $form.find('[data-name="RepairPartGrid"]'); 
      FwBrowse.search($repairPartGrid);

      var $pending = $form.find('div.fwformfield[data-datafield="PoPending"] input').prop('checked');
      if ($pending === true) {
          FwFormField.disable($form.find('div[data-datafield="PoNumber"]'));
      }
      else {
          FwFormField.enable($form.find('div[data-datafield="PoNumber"]'));
      } 
  };

  //----------------------------------------------------------------------------------------------
  completeOrder($form) {

      let $confirmation, $yes, $no;
      $confirmation = FwConfirmation.renderConfirmation('Complete', '');
      $confirmation.find('.fwconfirmationbox').css('width', '450px');
      let html = [];
      html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
      html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
      html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Description" data-datafield="DamageOrderDescription" data-noduplicate="true" data-enabled="false" style="float:left;width:300px;"></div>');
      html.push('    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="RepairDate" data-noduplicate="true" data-enabled="true" style="float:left;width:125px;"></div>');
      html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="User" data-datafield="UserId" data-displayfield="DamageScannedBy" data-noduplicate="true" data-enabled="true" data-validationname="UserValidation" style="float:left;width:300px;"></div>');
      html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="LocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation" style="float:left;max-width:200px;"></div>');
      html.push('  </div>');
      html.push('</div>');

      let copyConfirmation = html.join('');
      let RepairId = FwFormField.getValueByDataField($form, 'RepairId');

      FwConfirmation.addControls($confirmation, html.join(''));
      const today = new Date(Date.now()).toLocaleString().split(',')[0];
      const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
      const office = JSON.parse(sessionStorage.getItem('location'));
      const userId = JSON.parse(sessionStorage.getItem('userid'));
      const locationId = JSON.parse(sessionStorage.getItem('location'));

      //let orderNumber, deal, description, dealId;
      //$confirmation.find('div[data-caption="Type"] input').val(this.Module);
      //orderNumber = FwFormField.getValueByDataField($form, this.Module + 'Number');
      //$confirmation.find('div[data-caption="No"] input').val(orderNumber);
      //deal = $form.find('[data-datafield="DealId"] input.fwformfield-text').val();
      //$confirmation.find('div[data-caption="Deal"] input').val(deal);
      //description = FwFormField.getValueByDataField($form, 'Description');
      //$confirmation.find('div[data-caption="Description"] input').val(description);
      //$confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-text').val(deal);
      //dealId = $form.find('[data-datafield="DealId"] input.fwformfield-value').val();
      //$confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-value').val(dealId);

      //FwFormField.disable($confirmation.find('div[data-caption="Type"]'));
      //FwFormField.disable($confirmation.find('div[data-caption="No"]'));
      //FwFormField.disable($confirmation.find('div[data-caption="Deal"]'));
      //FwFormField.disable($confirmation.find('div[data-caption="Description"]'));

      //$confirmation.find('div[data-datafield="CopyRatesFromInventory"] input').prop('checked', true);
      //$confirmation.find('div[data-datafield="CopyDates"] input').prop('checked', true);
      //$confirmation.find('div[data-datafield="CopyLineItemNotes"] input').prop('checked', true);
      //$confirmation.find('div[data-datafield="CombineSubs"] input').prop('checked', true);
      //$confirmation.find('div[data-datafield="CopyDocuments"] input').prop('checked', true);

      $yes = FwConfirmation.addButton($confirmation, 'Complete', false);
      $no = FwConfirmation.addButton($confirmation, 'Cancel');

      $yes.on('click', makeComplete);

      function makeComplete() {

          let request: any = {};
          request.CopyToType = $confirmation.find('[data-type="radio"] input:checked').val();
          request.CopyToDealId = FwFormField.getValueByDataField($confirmation, 'CopyToDealId');
          request.CopyRatesFromInventory = FwFormField.getValueByDataField($confirmation, 'CopyRatesFromInventory');
          request.CopyDates = FwFormField.getValueByDataField($confirmation, 'CopyDates');
          request.CopyLineItemNotes = FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes');
          request.CombineSubs = FwFormField.getValueByDataField($confirmation, 'CombineSubs');
          request.CopyDocuments = FwFormField.getValueByDataField($confirmation, 'CopyDocuments');

          if (request.CopyRatesFromInventory == "T") {
              request.CopyRatesFromInventory = "False"
          };

          for (let key in request) {
              if (request.hasOwnProperty(key)) {
                  if (request[key] == "T") {
                      request[key] = "True";
                  } else if (request[key] == "F") {
                      request[key] = "False";
                  }
              }
          };


          FwFormField.disable($confirmation.find('.fwformfield'));
          FwFormField.disable($yes);
          $yes.text('Copying...');
          $yes.off('click');
          FwAppData.apiMethod(true, 'POST', 'api/v1/Repair/complete/' + RepairId, request, FwServices.defaultTimeout, function onSuccess(response) {
              FwNotification.renderNotification('SUCCESS', 'Order Successfully Copied');
              FwConfirmation.destroyConfirmation($confirmation);

              let uniqueids: any = {};
              if (request.CopyToType == "O") {
                  uniqueids.RepairId = response.RepairId;
                  let $form = OrderController.loadForm(uniqueids);
              } else if (request.CopyToType == "Q") {
                  uniqueids.QuoteId = response.QuoteId;
                  let $form = QuoteController.loadForm(uniqueids);
              }
              FwModule.openModuleTab($form, "", true, 'FORM', true)

          }, function onError(response) {
              $yes.on('click', makeComplete);
              $yes.text('Copy');
              FwFunc.showError(response);
              FwFormField.enable($confirmation.find('.fwformfield'));
              FwFormField.enable($yes);
          }, $form);
      };
  };

  //----------------------------------------------------------------------------------------------
  estimateOrder($form) {
        let $confirmation, $yes, $no;

        $confirmation = FwConfirmation.renderConfirmation('Estimate', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        let html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Description" data-datafield="DamageOrderDescription" data-noduplicate="true" data-enabled="false" style="float:left;width:300px;"></div>');
        html.push('    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="RepairDate" data-noduplicate="true" data-enabled="true" style="float:left;width:125px;"></div>');
        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="User" data-datafield="UserId" data-displayfield="DamageScannedBy" data-noduplicate="true" data-enabled="true" data-validationname="UserValidation" style="float:left;width:300px;"></div>');
        html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="LocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation" style="float:left;max-width:200px;"></div>');
        html.push('  </div>');
        html.push('</div>');

        let copyConfirmation = html.join('');
        let RepairId = FwFormField.getValueByDataField($form, 'RepairId');

        FwConfirmation.addControls($confirmation, html.join(''));

        //let orderNumber, deal, description, dealId;
        //$confirmation.find('div[data-caption="Type"] input').val(this.Module);
        //orderNumber = FwFormField.getValueByDataField($form, this.Module + 'Number');
        //$confirmation.find('div[data-caption="No"] input').val(orderNumber);
        //deal = $form.find('[data-datafield="DealId"] input.fwformfield-text').val();
        //$confirmation.find('div[data-caption="Deal"] input').val(deal);
        //description = FwFormField.getValueByDataField($form, 'Description');
        //$confirmation.find('div[data-caption="Description"] input').val(description);
        //$confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-text').val(deal);
        //dealId = $form.find('[data-datafield="DealId"] input.fwformfield-value').val();
        //$confirmation.find('div[data-datafield="CopyToDealId"] input.fwformfield-value').val(dealId);

        //FwFormField.disable($confirmation.find('div[data-caption="Type"]'));
        //FwFormField.disable($confirmation.find('div[data-caption="No"]'));
        //FwFormField.disable($confirmation.find('div[data-caption="Deal"]'));
        //FwFormField.disable($confirmation.find('div[data-caption="Description"]'));

        //$confirmation.find('div[data-datafield="CopyRatesFromInventory"] input').prop('checked', true);
        //$confirmation.find('div[data-datafield="CopyDates"] input').prop('checked', true);
        //$confirmation.find('div[data-datafield="CopyLineItemNotes"] input').prop('checked', true);
        //$confirmation.find('div[data-datafield="CombineSubs"] input').prop('checked', true);
        //$confirmation.find('div[data-datafield="CopyDocuments"] input').prop('checked', true);

        $yes = FwConfirmation.addButton($confirmation, 'Estimate', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', makeEstimate);

        function makeEstimate() {

            let request: any = {};
            request.CopyToType = $confirmation.find('[data-type="radio"] input:checked').val();
            request.CopyToDealId = FwFormField.getValueByDataField($confirmation, 'CopyToDealId');
            request.CopyRatesFromInventory = FwFormField.getValueByDataField($confirmation, 'CopyRatesFromInventory');
            request.CopyDates = FwFormField.getValueByDataField($confirmation, 'CopyDates');
            request.CopyLineItemNotes = FwFormField.getValueByDataField($confirmation, 'CopyLineItemNotes');
            request.CombineSubs = FwFormField.getValueByDataField($confirmation, 'CombineSubs');
            request.CopyDocuments = FwFormField.getValueByDataField($confirmation, 'CopyDocuments');

            if (request.CopyRatesFromInventory == "T") {
                request.CopyRatesFromInventory = "False"
            };

            for (let key in request) {
                if (request.hasOwnProperty(key)) {
                    if (request[key] == "T") {
                        request[key] = "True";
                    } else if (request[key] == "F") {
                        request[key] = "False";
                    }
                }
            };


            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Copying...');
            $yes.off('click');
            FwAppData.apiMethod(true, 'POST', 'api/v1/Repair/complete/' + RepairId, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Order Successfully Copied');
                FwConfirmation.destroyConfirmation($confirmation);

                let uniqueids: any = {};
                if (request.CopyToType == "O") {
                    uniqueids.RepairId = response.RepairId;
                    let $form = OrderController.loadForm(uniqueids);
                } else if (request.CopyToType == "Q") {
                    uniqueids.QuoteId = response.QuoteId;
                    let $form = QuoteController.loadForm(uniqueids);
                }
                FwModule.openModuleTab($form, "", true, 'FORM', true)

            }, function onError(response) {
                $yes.on('click', makeEstimate);
                $yes.text('Copy');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
            }, $form);
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

// using complete security guid
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

// using estimate security guid
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

//------------------------------------------------------------------------------------------------
var RepairController = new Repair();