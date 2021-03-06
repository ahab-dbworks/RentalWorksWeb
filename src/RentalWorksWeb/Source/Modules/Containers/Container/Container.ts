routes.push({ pattern: /^module\/container$/, action: function (match: RegExpExecArray) { return ContainerController.getModuleScreen(); } });

class Container {
    Module: string = 'Container';
    apiurl: string = 'api/v1/containeritem';
    caption: string = Constants.Modules.Container.children.Container.caption;
    nav: string = Constants.Modules.Container.children.Container.nav;
    id: string = Constants.Modules.Container.children.Container.id;
    //caption: string = 'Container';
    //nav: string = 'module/container';
    //id: string = '28A49328-FFBD-42D5-A492-EDF540DF7011';
    nameItemAttributeValueGrid: string = 'ItemAttributeValueGrid';
    nameItemQcGrid: string = 'ItemQcGrid';
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);
        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }
        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($userWarehouse, $all);
        FwMenu.addViewBtn(options.$menu, 'Warehouse', viewSubitems, true, "WarehouseId");
    }
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        // var $form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form.find('.container-section').show();
        $form = FwModule.openForm($form, mode);

        $form.find('.container-status').on('click', e => {
            const orderInfo: any = {};
            orderInfo.OrderId = FwFormField.getValueByDataField($form, 'ContainerItemId');
            orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'BarCode');
            const $orderStatusForm = ContainerStatusController.openForm('EDIT', orderInfo);
            FwModule.openSubModuleTab($form, $orderStatusForm);
            const $tab = FwTabs.getTabByElement($orderStatusForm);
            $tab.find('.caption').html('Container Status');
        });

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');

        const uniqueid = uniqueids.ContainerItemId || uniqueids.ContainerId;

        FwFormField.setValueByDataField($form, 'ItemId', uniqueid);
        FwModule.loadForm(this.Module, $form);
        $form.find('.repairOrderSubModule').append(AssetController.openRepairOrderBrowse($form));
        $form.find('.orderSubModule').append(AssetController.openOrderBrowse($form));
        $form.find('.transferSubModule').append(AssetController.openTransferBrowse($form));
        $form.find('.invoiceSubModule').append(AssetController.openInvoiceBrowse($form));
        $form.find('.retiredSubModule').append(AssetController.openRetiredBrowse($form));

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------------------
    loadAudit($form: JQuery) {
        const uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
        FwModule.loadAudit($form, uniqueid);
    }
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
           <div data-name="Container" data-control="FwBrowse" data-type="Browse" id="ContainerBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="ContainerController" data-hasinactive="true">
             <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-isuniqueid="false" data-datafield="ItemId" data-browsedatatype="text"></div>
             </div>
             <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-isuniqueid="true" data-datafield="ContainerItemId" data-browsedatatype="key"></div>
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
               <div class="field" data-caption="Container Description" data-datafield="ContainerDescription" data-browsedatatype="text" data-sort="off"></div>
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
             <div class="column flexcolumn" max-width="250px" data-visible="true">
                 <div class="field" data-caption="Bar Code Status" data-datafield="InventoryStatus" data-cellcolor="Color" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-datafield="Inactive" data-browsedatatype="text" data-visible="false"></div>
             </div>
             <div class="column spacer" data-width="auto" data-visible="true"></div>
           </div>
           `;
    }
    //---------------------------------------------------------------------------------------------
    getFormTemplate(){
        return AssetController.getFormTemplate();
    }
    //---------------------------------------------------------------------------------------------
}

var ContainerController = new Container();