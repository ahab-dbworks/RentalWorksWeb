
class SuspendedSession {
    Module: string = 'SuspendedSession';
    apiurl: string = 'api/v1/suspendedsession';
    caption: string = Constants.Modules.Home.children.SuspendedSession.caption;
	nav: string = Constants.Modules.Home.children.SuspendedSession.nav;
	id: string = Constants.Modules.Home.children.SuspendedSession.id;
    sessionType: string = "";
    //----------------------------------------------------------------------------------------------
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
    };
    //----------------------------------------------------------------------------------------------

}

var SuspendedSessionController = new SuspendedSession();