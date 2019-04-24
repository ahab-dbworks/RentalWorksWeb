
class SuspendedSession {
    Module: string = 'SuspendedSession';
    caption: string = 'Suspended Session';
    apiurl: string = 'api/v1/suspendedsession';
    nav: string = 'module/suspendedsession';
    id: string = '5FBE7FF8-3770-48C5-855C-4320C961D95A';
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------

}

var SuspendedSessionController = new SuspendedSession();