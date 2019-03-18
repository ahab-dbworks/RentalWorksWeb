class TiwBase extends Base {
    constructor() {
        super();
    }
    getDefaultScreen() {
        let viewModel = {
            captionProgramTitle: 'TrakitWorks',
            valueYear: new Date().getFullYear(),
            valueVersion: applicationConfig.version
        };
        let screen = {};
        screen = FwBasePages.getDefaultScreen(viewModel);
        screen.$view
            .on('click', '.btnLogin', function () {
            try {
                program.navigate('login');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .find('.programlogo').empty().html('<div class="bgothm">Trakit<span class="tiwred">Works<span><span style="font-size:14px;vertical-align:super;">&#174;</span></div>');
        return screen;
    }
    getLoginScreen() {
        let screen = super.getLoginScreen();
        screen.$view.find('.programlogo').empty().html('<div class="bgothm">Trakit<span class="tiwred">Works<span><span style="font-size:14px;vertical-align:super;">&#174;</span></div>');
        return screen;
    }
}
var TiwBaseController = new TiwBase();
//# sourceMappingURL=TiwBase.js.map