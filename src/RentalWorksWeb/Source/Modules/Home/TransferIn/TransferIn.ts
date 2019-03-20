routes.push({ pattern: /^module\/transferin$/, action: function (match: RegExpExecArray) { return TransferInController.getModuleScreen(); } });

class TransferIn extends CheckIn{
    Module: string = 'TransferIn';
    caption: string = 'Transfer In';
    nav: string = 'module/transferin';
    id: string = 'D9F487C2-5DC1-45DF-88A2-42A05679376C';
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
}
var TransferInController = new TransferIn();