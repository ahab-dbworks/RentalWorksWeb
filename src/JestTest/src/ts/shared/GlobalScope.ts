import { FwGlobalScope } from '../fwjest/FwGlobalScope';

export class GlobalScope extends FwGlobalScope {

    //use this class to register global values that can be referenced between tests
    //                                          root_object.object~id.fieldname

    //For example:
    //   store some default unit value as:      GlobalScope.DefaultSettings~1.DefaultUnit
    //   store some important customer name as: GlobalScope.Customer~ABCD1234.CustomerName

}
