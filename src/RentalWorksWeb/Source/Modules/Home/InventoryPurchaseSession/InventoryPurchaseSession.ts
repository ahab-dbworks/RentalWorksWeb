
class InventoryPurchaseSession {
    Module: string = 'InventoryPurchaseSession';
    apiurl: string = 'api/v1/inventorypurchasesession';
    caption: string = Constants.Modules.Home.children.InventoryPurchaseSession.caption;
    nav: string = Constants.Modules.Home.children.InventoryPurchaseSession.nav;
    id: string = Constants.Modules.Home.children.InventoryPurchaseSession.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasInactive = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------

}

var InventoryPurchaseSessionController = new InventoryPurchaseSession();