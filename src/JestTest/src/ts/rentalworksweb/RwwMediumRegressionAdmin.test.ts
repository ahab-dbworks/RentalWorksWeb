import {

    DefaultSettings,
    InventorySettings,
    Warehouse,

    //administrator
    Alert, CustomField, CustomForm, CustomReportLayout, DuplicateRule, EmailHistory, Group, Hotfix, User,
} from './modules/AllModules';
//import { SettingsModule } from '../shared/SettingsModule';
import { MediumRegressionBaseTest } from './RwwMediumRegressionBase';

export class MediumRegressionAdminTest extends MediumRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);
        this.OpenSpecificRecord(new InventorySettings(), null, true);

        let warehouseToSeek: any = {
            Warehouse: "GlobalScope.User~ME.Warehouse",
        }
        this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

        //Administrator
        //this.MediumRegressionOnModule(new Alert());
        //this.MediumRegressionOnModule(new CustomField());
        this.MediumRegressionOnModule(new CustomForm());
        this.MediumRegressionOnModule(new CustomReportLayout());
        //this.MediumRegressionOnModule(new DuplicateRule());
        this.MediumRegressionOnModule(new EmailHistory());
        this.MediumRegressionOnModule(new Group());
        this.MediumRegressionOnModule(new Hotfix());
        this.MediumRegressionOnModule(new User());

    }
    //---------------------------------------------------------------------------------------
}

describe('MediumRegressionAdminTest', () => {
    try {
        new MediumRegressionAdminTest().Run();
    } catch (ex) {
        fail(ex);
    }
});
