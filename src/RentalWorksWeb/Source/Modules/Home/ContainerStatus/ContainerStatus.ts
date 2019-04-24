routes.push({ pattern: /^module\/containerstatus$/, action: function (match: RegExpExecArray) { return ContainerStatusController.getModuleScreen(); } });

class ContainerStatus extends OrderStatusBase {
    Module: string = 'ContainerStatus';
    caption: string = 'Container Status';
    nav: string = 'module/containerstatus';
    id: string = '0CD07ACF-D9A4-42A3-A288-162398683F8A';
    Type: string = 'ContainerItem';
}
var ContainerStatusController = new ContainerStatus();