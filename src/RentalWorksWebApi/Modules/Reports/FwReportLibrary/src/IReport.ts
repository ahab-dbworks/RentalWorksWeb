export interface IReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void;
}