routes.push({ pattern: /^module\/quiksearch$/, action: function (match: RegExpExecArray) { return QuikSearchController.getModuleScreen(); } });

class QuikSearch {
    Module: string = 'QuikSearch';
    caption: string = Constants.Modules.Utilities.QuikSearch.caption;
    nav: string = Constants.Modules.Utilities.QuikSearch.nav;
    id: string = Constants.Modules.Utilities.QuikSearch.id;
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

        screen.load = () => {
        };
        screen.unload = function () {
            FwPopup.destroyPopup($popup);
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------

}
var QuikSearchController = new QuikSearch();