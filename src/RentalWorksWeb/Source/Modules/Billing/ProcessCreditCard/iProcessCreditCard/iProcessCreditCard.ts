//----------------------------------------------------------------------------------------------
interface IProcessCreditCard {
    process($parent: JQuery);
}
//----------------------------------------------------------------------------------------------
function ProcessCreditCardFactory(type: string): IProcessCreditCard {
    switch (type) {
        case "Visitek":
            return new VisitekProcessCreditCard();
        default:
            return null;
    }
}