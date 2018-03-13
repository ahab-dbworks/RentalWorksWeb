routes.push({ pattern: /^module\/repair$/, action: function (match: RegExpExecArray) { return RepairController.getModuleScreen(); } });
// routes.push({ pattern: /^module\/repair\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return RepairController.getModuleScreen(filter); } });
//---------------------------------------------------------------------------------
class Repair {
  Module: string;
  apiurl: string;
  caption: string;
  ActiveView: string;

  constructor() {
    this.Module = 'Repair';
    this.apiurl = 'api/v1/repair';
    this.caption = 'Repair Order';
    this.ActiveView = 'ALL';
    var self = this;
  }

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

    var $repairCostsGrid, $repairCostsGridControl; 
 
    $repairCostsGrid = $form.find('div[data-grid="RepairCostsGrid"]'); 
    $repairCostsGridControl = jQuery(jQuery('#tmpl-grids-RepairCostsGridBrowse').html()); 
    $repairCostsGrid.empty().append($repairCostsGridControl); 
    $repairCostsGridControl.data('ondatabind', function (request) { 
      request.uniqueids = { 
        RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val() 
      } 
    }); 
    $repairCostsGridControl.data('beforesave', function (request) { 
      request.RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 
    }) 
    FwBrowse.init($repairCostsGridControl); 
    FwBrowse.renderRuntimeHtml($repairCostsGridControl);
    
    //----------------------------------------------------------------------------------------------
    var $repairPartsGrid, $repairPartsGridControl; 
 
    $repairPartsGrid = $form.find('div[data-grid="RepairPartsGrid"]'); 
    $repairPartsGridControl = jQuery(jQuery('#tmpl-grids-RepairPartsGridBrowse').html()); 
    $repairPartsGrid.empty().append($repairPartsGridControl); 
    $repairPartsGridControl.data('ondatabind', function (request) { 
      request.uniqueids = { 
        RepairId: $form.find('div.fwformfield[data-datafield="RepairId"] input').val() 
      } 
    }); 
    $repairPartsGridControl.data('beforesave', function (request) { 
      request.RepairId = FwFormField.getValueByDataField($form, 'RepairId'); 
    }) 
    FwBrowse.init($repairPartsGridControl); 
    FwBrowse.renderRuntimeHtml($repairPartsGridControl);
  } 

    //----------------------------------------------------------------------------------------------
  openBrowse() {
    var self = this;
    var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
    $browse = FwModule.openBrowse($browse);
    FwBrowse.addLegend($browse, 'High Priority', '#EA300F');
    FwBrowse.addLegend($browse, 'No Charge', '#FF8040');
    FwBrowse.addLegend($browse, 'Transferred', '#FFB3D9');
    FwBrowse.addLegend($browse, 'Foreign Currency', '#95FFCA');
    FwBrowse.addLegend($browse, 'Multi-Warehouse', '#D6E180');
    FwBrowse.addLegend($browse, 'Repair', '#5EAEAE');
    FwBrowse.addLegend($browse, 'L&D', '#400040');

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
 
   var $repairCostsGrid: any = $form.find('[data-name="RepairCostsGrid"]'); 
   FwBrowse.search($repairCostsGrid); 
    var $repairPartsGrid: any = $form.find('[data-name="RepairPartsGrid"]'); 
   FwBrowse.search($repairPartsGrid); 
  };

}
//---------------------------------------------------------------------------------
var RepairController = new Repair();
