import { SettingsModule } from "../../../shared/SettingsModule";

export class GlAccount extends SettingsModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'GlAccount';
        //this.moduleBtnId = '#btnModule3F551271-C157-44F6-B06A-8F835F7A2084';
        this.moduleCaption = 'Chart of Accounts';
    }
    //---------------------------------------------------------------------------------------
}