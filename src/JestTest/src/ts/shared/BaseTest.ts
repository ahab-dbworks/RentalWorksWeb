require('dotenv').config()
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';

export abstract class BaseTest {

    testToken: string | void = "";
    continueTest: boolean | void = true;
    testTimeout: number = 45000; // 45 seconds

    //---------------------------------------------------------------------------------------
    LogError(testName: string, err: any) {
        Logging.logger.error(testName, err);
    }
    //---------------------------------------------------------------------------------------
    CheckDependencies() {
        if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';
        if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';
    }
    //---------------------------------------------------------------------------------------
    DoBeforeAll() {
        beforeAll(async () => {
            await page.setViewport({ width: 1600, height: 1080 })
                .then()
                .catch(err => Logging.logger.error('Error in BeforeAll: ', err));
        });
    }
    //---------------------------------------------------------------------------------------
    GetTestToken() {
        describe('Get Test Token', () => {
            test('Get Test Token', async () => {
                this.testToken = await TestUtils.getDateTimeToken()
                    //.then((data) => { })
                    .then()
                    .catch(err => Logging.logger.error('Error in GetTestToken: ', err));
            }, 45000);
        });
    }
    //---------------------------------------------------------------------------------------
    DoLogin() {
        describe('Login', () => {
            test('Login', async () => {
                this.continueTest = await TestUtils.authenticate()
                    .then((data) => { })
                    .catch(err => Logging.logger.error('Error in DoLogin: ', err));
            }, 45000);
        });
    }
    //---------------------------------------------------------------------------------------
    DoLogoff() {
        describe('Logoff', () => {
            test('Logoff', async () => {
                this.continueTest = await TestUtils.logoff()
                    .then((data) => { })
                    .catch(err => Logging.logger.error('Error in DoLogoff: ', err));
            }, 45000);
        });
    }
    //---------------------------------------------------------------------------------------
    // this method will be overridden in sub classes for each test collection we want to perform
    PerformTests() { }
    //---------------------------------------------------------------------------------------
    Run() {
        try {
            this.DoBeforeAll();
            this.CheckDependencies();
            this.GetTestToken();
            this.DoLogin();
            this.PerformTests();
            this.DoLogoff();
        } catch (ex) {
            Logging.logger.error('Error in Run.', ex);
        }
    }
    //---------------------------------------------------------------------------------------
}
