class SuspendedSession {
    constructor() {
        this.Module = 'SuspendedSession';
        this.apiurl = 'api/v1/suspendedsession';
        this.caption = Constants.Modules.Home.children.SuspendedSession.caption;
        this.nav = Constants.Modules.Home.children.SuspendedSession.nav;
        this.id = Constants.Modules.Home.children.SuspendedSession.id;
        this.sessionType = "";
    }
    openBrowse() {
        let $browse;
        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        if ((this.sessionType === "RECEIVE") || (this.sessionType === "RETURN")) {
            $browse.find(`tr.fieldnames .column .field[data-formdatafield="OrderNumber"] .caption`).text("PO Number");
            $browse.find(`tr.fieldnames .column .field[data-formdatafield="DealOrVendor"] .caption`).text("Vendor");
            $browse.find(`tr.fieldnames .column .field[data-formdatafield="OrderDescription"] .caption`).text("PO Description");
        }
        else {
            $browse.find(`tr.fieldnames .column .field[data-formdatafield="OrderNumber"] .caption`).text("Order Number");
            $browse.find(`tr.fieldnames .column .field[data-formdatafield="DealOrVendor"] .caption`).text("Deal");
            $browse.find(`tr.fieldnames .column .field[data-formdatafield="OrderDescription"] .caption`).text("Order Description");
        }
        return $browse;
    }
    ;
}
var SuspendedSessionController = new SuspendedSession();
//# sourceMappingURL=SuspendedSession.js.map