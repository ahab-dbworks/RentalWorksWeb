//----------------------------------------------------------------------------------------------
interface IProcessCreditCard {
    process($parent: JQuery);
}
//----------------------------------------------------------------------------------------------
function ProcessCreditCardFactory(type: string): IProcessCreditCard {
    switch (type) {
        case "Vistek":
            return new VistekProcessCreditCard();
        default:
            return null;
    }
}