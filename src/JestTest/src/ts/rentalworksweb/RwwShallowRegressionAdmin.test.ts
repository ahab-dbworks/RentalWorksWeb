import {
    Alert, CustomField, CustomForm, CustomReportLayout, DuplicateRule, EmailHistory, Group, Hotfix, User,
} from './modules/AllModules';
import { ShallowRegressionBaseTest } from './RwwShallowRegressionBase';


export class ShallowRegressionAdminTest extends ShallowRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        this.ShallowRegressionOnModule(new Alert());
        this.ShallowRegressionOnModule(new CustomField());
        this.ShallowRegressionOnModule(new CustomForm());
        this.ShallowRegressionOnModule(new CustomReportLayout());
        this.ShallowRegressionOnModule(new DuplicateRule());
        this.ShallowRegressionOnModule(new EmailHistory());
        this.ShallowRegressionOnModule(new Group());
        this.ShallowRegressionOnModule(new Hotfix());
        this.ShallowRegressionOnModule(new User());
    }
    //---------------------------------------------------------------------------------------
}

describe('ShallowRegressionAdminTest', () => {
    try {
        new ShallowRegressionAdminTest().Run();
    } catch(ex) {
        fail(ex);
    }
});
