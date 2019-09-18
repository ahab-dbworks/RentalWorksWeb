import { MediumRegressionTest } from './RwwMediumRegression.test';
import { User, DefaultSettings, Project, RentalInventory } from './modules/AllModules';
import { Logging } from '../shared/Logging';

export class Issue944Test extends MediumRegressionTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        Logging.logInfo(`Issue944Test.PerformTests()`);
        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);
        this.MediumRegressionOnModule(new Project());            // this module fails because "Description" is a required field.  It is a textarea
        // I would like to be able to control checkboxes here
        this.MediumRegressionOnModule(new RentalInventory());    // I would like to be able to control radio groups here
    }
    //---------------------------------------------------------------------------------------
}
new Issue944Test().Run();
