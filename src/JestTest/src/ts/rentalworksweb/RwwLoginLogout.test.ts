import { BaseTest } from '../shared/BaseTest';

export class LoginLogoutTest extends BaseTest {
    testTimeout = 240000; // 240 seconds
}

new LoginLogoutTest().Run();
