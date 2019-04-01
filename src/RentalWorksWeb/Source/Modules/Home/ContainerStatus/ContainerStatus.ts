routes.push({ pattern: /^module\/containerstatus$/, action: function (match: RegExpExecArray) { return ContainerStatusController.getModuleScreen(); } });

class ContainerStatus extends OrderStatusBase {
    Module: string = 'ContainerStatus';
    caption: string = 'Container Status';
    nav: string = 'module/containerstatus';
    id: string = 'F2238A1C-7A75-498D-86D2-2033CD8EBF95';
    Type: string = 'Item';
}
var ContainerStatusController = new ContainerStatus();