
class SuspendedSession {
    Module: string = 'SuspendedSession';
    apiurl: string = 'api/v1/suspendedsession';
    caption: string = Constants.Modules.Home.SuspendedSession.caption;
	nav: string = Constants.Modules.Home.SuspendedSession.nav;
	id: string = Constants.Modules.Home.SuspendedSession.id;
   
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var self = this;
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------

}

var SuspendedSessionController = new SuspendedSession();