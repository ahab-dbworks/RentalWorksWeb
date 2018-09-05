routes.push({ pattern: /^module\/container$/, action: function (match: RegExpExecArray) { return ContainerController.getModuleScreen(); } });

class Container {
    Module: string = 'Container';
    apiurl: string = 'api/v1/containeritem';
    caption: string = 'Container';
    nameItemAttributeValueGrid: string = 'ItemAttributeValueGrid';
    nameItemQcGrid: string = 'ItemQcGrid';
    ActiveView: string = 'ALL';
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        var self = this;
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });

        FwAppData.apiMethod(true, 'GET', "api/v1/inventorystatus", null, FwServices.defaultTimeout, function onSuccess(response) {
            for (var i = 0; i < response.length; i++) {
                if (response[i].InventoryStatus == 'IN CONTAINER') {
                    FwBrowse.addLegend($browse, response[i].InventoryStatus, response[i].Color);
                }
            }
            FwBrowse.addLegend($browse, 'INCOMPLETE', '#FF0000');
        }, null, $browse);

        return $browse;
    };
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject: any) {
        var self = this;
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        var $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false);
        var $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true);
        var view = [];
        view[0] = 'WarehouseId=' + warehouse.warehouseid;

        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'WarehouseId=ALL';

            view[0] = self.ActiveView;
            if (view.length > 1) {
                self.ActiveView = view.join(', ');
            }

            FwBrowse.search($browse);
        });
        $userWarehouse.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'WarehouseId=' + warehouse.warehouseid;

            view[0] = self.ActiveView;
            if (view.length > 1) {
                self.ActiveView = view.join(', ');
            }

            FwBrowse.search($browse);
        });

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($userWarehouse);
        viewSubitems.push($all);

        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'Warehouse', viewSubitems);

        return $menuObject;
    };
    //---------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        // var $form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(AssetController.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        return $form;
    };
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ItemId', uniqueids.ItemId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //---------------------------------------------------------------------------------------------
    loadAudit($form: JQuery) {
        var uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
        FwModule.loadAudit($form, uniqueid);
    };
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
           <div data-name="Container" data-control="FwBrowse" data-type="Browse" id="ContainerBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="ContainerController" data-hasinactive="true">
             <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-isuniqueid="true" data-datafield="ItemId" data-browsedatatype="key"></div>
             </div>
             <div class="column flexcolumn" max-width="250px" data-visible="true">
               <div class="field" data-caption="Barcode No." data-datafield="BarCode" data-browsedatatype="text" data-sort="asc"></div>
             </div>
             <div class="column flexcolumn" max-width="250px" data-visible="true">
               <div class="field" data-caption="Serial No." data-datafield="SerialNumber" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="I-Code" data-datafield="ICode" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="450px" data-visible="true">
               <div class="field" data-caption="Description" data-datafield="Description" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Container I-Code" data-datafield="ContainerICode" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="450px" data-visible="true">
               <div class="field" data-caption="ContainerDescription" data-datafield="Container Description" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="125px" data-visible="true">
               <div class="field" data-caption="Container Status" data-datafield="ContainerStatus" data-cellcolor="ContainerStatusColor" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="100px" data-visible="true">
               <div class="field" data-caption="As Of" data-datafield="ContainerStatusDate" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="250px" data-visible="true">
               <div class="field" data-caption="Location" data-datafield="CurrentLocation" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-datafield="Inactive" data-browsedatatype="text" data-visible="false"></div>
             </div>
             <div class="column spacer" data-width="auto" data-visible="true"></div>
           </div>
    `;
    };
    //---------------------------------------------------------------------------------------------
}

var ContainerController = new Container();