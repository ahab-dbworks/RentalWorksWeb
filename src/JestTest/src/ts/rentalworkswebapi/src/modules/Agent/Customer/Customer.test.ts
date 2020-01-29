import { Jwt } from "../../AccountServices/Jwt/Jwt";
import { Account } from "../../AccountServices/Account/Account";
import { Customer } from "./Customer";



describe('CustomerTest', () => {
    const jwt = Jwt.test();
    const account = Account.test();
    const customer = Customer.test(account);
});
