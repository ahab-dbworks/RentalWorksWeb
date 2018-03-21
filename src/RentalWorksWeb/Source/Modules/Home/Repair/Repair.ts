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

    var $repairCostGrid, $repairCostGridControl; 
 
    $repairCostGrid = $form.find('div[data-grid="RepairCostGrid"]'); 
    $repairCostGridControl = jQuery(jQuery('#tmpl-grids-RepairCostGridBrowse').html()); 
    $repairCostGrid.empty().append($repairCostGridControl); 
    $repairCostGridControl.data('ondatabind', function (request) { 
      request.uniqueids = { 
        RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val() 
      } 
    }); 
    $repairCostGridControl.data('beforesave', function (request) { 
      request.RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 
    }) 
    FwBrowse.setAfterSaveCallback($repairCostGridControl, ($repairCostGridControl: JQuery, $tr: JQuery) => {
      this.calculateTotals($form, 'cost');
    });
    FwBrowse.setAfterDeleteCallback($repairCostGridControl, ($repairCostGridControl: JQuery, $tr: JQuery) => {
      this.calculateTotals($form, 'cost');
    });
    FwBrowse.init($repairCostGridControl); 
    FwBrowse.renderRuntimeHtml($repairCostGridControl);
    
    //----------------------------------------------------------------------------------------------
    var $repairPartGrid, $repairPartGridControl; 
 
    $repairPartGrid = $form.find('div[data-grid="RepairPartGrid"]'); 
    $repairPartGridControl = jQuery(jQuery('#tmpl-grids-RepairPartGridBrowse').html()); 
    $repairPartGrid.empty().append($repairPartGridControl); 
    $repairPartGridControl.data('ondatabind', function (request) { 
      request.uniqueids = { 
        RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val() 
      } 
    }); 
    $repairPartGridControl.data('beforesave', function (request) { 
      request.RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 
    }) 
    FwBrowse.init($repairPartGridControl); 
    FwBrowse.renderRuntimeHtml($repairPartGridControl);
  } 

    //----------------------------------------------------------------------------------------------
  openBrowse() {
    var self = this;
    var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
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
    openForm(mode: string) {
      var $form;

      $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
      $form = FwModule.openForm($form, mode);

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
    afterLoad($form: any) { 
 
     var $repairCostGrid: any = $form.find('[data-name="RepairCostGrid"]'); 
     FwBrowse.search($repairCostGrid); 
      var $repairPartGrid: any = $form.find('[data-name="RepairPartGrid"]'); 
     FwBrowse.search($repairPartGrid); 
    };

  totals($form: any) {
    var self = this;
    var gridTypes = ['cost'];
    setTimeout(function () {
        for (var i = 0; i < gridTypes.length; i++) {
            self.calculateTotals($form, gridTypes[i]);
        }
    }, 4000);
  }
  calculateTotals($form: any, gridType: string) {
      var totals = 0, finalTotal;
      setTimeout(function () {
        var periodExtended = $form.find('.' + gridType + 'grid .periodextended.editablefield');
        if (periodExtended.length > 0) {
          periodExtended.each(function () {
            var value = jQuery(this).text();
            if (value.charAt(0) === '$') {
                value = value.slice(1).replace(/,/g, '');
            }
            var toNumber = parseFloat(parseFloat(value).toFixed(2));

            totals += toNumber;
            finalTotal = totals.toLocaleString();

          });

          $form.find('.' + gridType + 'totals [data-totalfield="Total"] input').val(finalTotal);
        }
      }, 2000);
    };

}
//---------------------------------------------------------------------------------
var RepairController = new Repair();
