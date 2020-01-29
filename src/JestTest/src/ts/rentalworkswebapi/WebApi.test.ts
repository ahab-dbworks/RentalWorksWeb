import { Jwt, Account, Customer } from "./src";

describe('WebApiTest', () => {
    const jwt = Jwt.test();
    const account = Account.test();
    const customer = Customer.test(account);
});