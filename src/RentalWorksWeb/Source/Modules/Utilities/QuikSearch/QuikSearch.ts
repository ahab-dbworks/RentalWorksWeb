routes.push({ pattern: /^module\/quiksearch$/, action: function (match: RegExpExecArray) { return QuikSearchController.getModuleScreen(); } });

class QuikSearch {
    Module:    string = 'QuikSearch';
    apiurl:    string = 'api/v1/quiksearch';
    caption:   string = Constants.Modules.Inventory.children.QuikSearch.caption;
    nav:       string = Constants.Modules.Inventory.children.QuikSearch.nav;
    id:        string = Constants.Modules.Inventory.children.QuikSearch.id;
    SessionId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        let search = new SearchInterface();
        const webUsersId = JSON.parse(sessionStorage.getItem('userid')).webusersid;
        const $popup = search.renderSearchPopup(null, webUsersId, 'Main', null);

        screen.load = () => { };
        screen.unload = function () { };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
}
var QuikSearchController = new QuikSearch();