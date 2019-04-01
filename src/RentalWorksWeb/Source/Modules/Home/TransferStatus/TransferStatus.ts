routes.push({ pattern: /^module\/transferstatus$/, action: function (match: RegExpExecArray) { return TransferStatusController.getModuleScreen(); } });

class TransferStatus extends OrderStatus {
    Module: string = 'TransferStatus';
    caption: string = 'Transfer Status';
    nav: string = 'module/transferstatus';
    id: string = '58D5D354-136E-40D5-9675-B74FD7807D6F';
    Type: string = 'Transfer';
}
var TransferStatusController = new TransferStatus();