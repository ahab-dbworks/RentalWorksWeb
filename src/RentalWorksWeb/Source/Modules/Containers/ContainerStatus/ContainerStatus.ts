routes.push({ pattern: /^module\/containerstatus$/, action: function (match: RegExpExecArray) { return ContainerStatusController.getModuleScreen(); } });

class ContainerStatus extends OrderStatusBase {
    Module:  string = 'ContainerStatus';
    apiurl:  string = 'api/v1/containerstatus'
    caption: string = Constants.Modules.Container.children.ContainerStatus.caption;
    nav:     string = Constants.Modules.Container.children.ContainerStatus.nav;
    id:      string = Constants.Modules.Container.children.ContainerStatus.id;
    Type:    string = 'ContainerItem';
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ContainerItemId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontaineritem`);
                break;
            case 'CategoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                break;
            case 'SubCategoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'InventoryTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
        }
    }
}
var ContainerStatusController = new ContainerStatus();