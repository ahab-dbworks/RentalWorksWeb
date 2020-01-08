import { BaseTest } from '../shared/BaseTest';

export class LoginLogoutTest extends BaseTest {
    testTimeout = 240000; // 240 seconds
}

describe('LoginLogoutTest', () => {
    try {
        new LoginLogoutTest().Run();
    } catch(ex) {
        fail(ex);
    }
});
