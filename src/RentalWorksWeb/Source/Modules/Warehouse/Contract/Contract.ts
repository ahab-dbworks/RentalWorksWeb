class Contract extends ContractBase {
    Module:             string = 'Contract';
    apiurl:             string = 'api/v1/contract';
    caption:            string = Constants.Modules.Warehouse.children.Contract.caption;
    nav:                string = Constants.Modules.Warehouse.children.Contract.nav;
    id:                 string = Constants.Modules.Warehouse.children.Contract.id;
    ActiveViewFields:   any    = {};
    ActiveViewFieldsId: string;
    BillingDate:        string;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);
        super.afterAddBrowseMenuItems(options);
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Contract', '', (e: JQuery.ClickEvent) => {
            try {
                this.printContract(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Void Entire Contract', 'bwrnjBpQv1P', (e: JQuery.ClickEvent) => {
            try {
                this.voidContract(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------

var ContractController = new Contract();