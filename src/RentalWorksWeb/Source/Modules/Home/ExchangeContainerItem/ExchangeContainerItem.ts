routes.push({ pattern: /^module\/exchangecontaineritem$/, action: function (match: RegExpExecArray) { return ExchangeContainerItemController.getModuleScreen(); } });

class ExchangeContainerItem extends Exchange{
    Module: string = 'ExchangeContainerItem';
    caption: string = 'Exchange Container Item';
    nav: string = 'module/exchangecontaineritem';
    id: string = '6B8D5B55-2B79-4569-B0B8-97920295EEDA';
    ContractId: string = '';
    ExchangeResponse: any = {};
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    Type: string = 'Item';
};
var ExchangeContainerItemController = new ExchangeContainerItem();