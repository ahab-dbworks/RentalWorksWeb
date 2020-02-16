#Creating new reports in RentalWorks Web

##1. Copy an existing report file structure
- In the API, open WebpackReports -> src -> Reports.
- Find a report in the same section as the one where you intend to add will be and copy and paste the folder.
- Change the names of the controller file names and update all class names and other specific naming conventions to that file.
- In the web project navigate to Source -> Modules -> Reports and duplicate any report folder and again change the relevant file names/class names etc.
- Make sure to change everything to avoid errors, including the HTML template string that refers to a controller ex -

```
const returnOnAssetTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Return On Asset Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ReturnOnAssetReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
```

##2. Add the report to the complete reports list module
- Open the Constants.ts file, in the Web project navigate to scripts -> constants
- Find the Object name containing the JSON that relates to your report and add your report in the children key eg -

```
AccountingReports: {
                    id: 'Reports.AccountingReports',
                    caption: 'Accounting Reports',
                    nodetype: 'Category',
                    children: {
                        ArAgingReport: {                    id: 'KHw5yX5TubQ', caption: 'A/R Aging', nav: 'reports/aragingreport', nodetype: 'Module', description: 'List unpaid Invoices, and their corresponding aging totals.  Report is subtotalled by Deal and Customer.' },
                        DailyReceiptsReport: {              id: 'OLyFIS7rBvr8', caption: 'Daily Receipts', nav: 'reports/dailyreceiptsreport', nodetype: 'Module', description: 'List Daily Receipts.  Report is subtotalled by Deal and Customer.' },
                        GlDistributionReport: {             id: 'ClMQ5QkZq4PY', caption: 'G/L Distribution', nav: 'reports/gldistributionreport', nodetype: 'Module', description: 'Summarize transaction totals by Account over a date range.' }
                    }
```

- Be sure to replace the ID with the GUID that your WebAPI controller also uses. eg -

```
[Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id:"KHw5yX5TubQ")]
    public class ArAgingReportController : AppReportController
```

##3. Configure the loader and controller for the report
- The controller should have some kind of method to fetch data, like RunReportAsync
- If the report lists information about items, or items and how they relate to an order, you will most likely return a FwJsonDataTable as the response,
or add the FwJsonTable as a response property amidst other properties needed for report headers etc.
- The loader should have a qry to recieve data from a stored procedure or dbo table eg -

```
{
    [FwSqlTable("dbo.funcvaluesheetweb(@orderid, @rentalvalue, @salesvalue, @filterby, @mode)")]
    public class ManifestSummaryReportLoader : AppReportLoader
    {
```


```
    using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetorderprintheader", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    qry.AddParameter("@orderid", SqlDbType.Text, ParameterDirection.Input, request.OrderId);
                    AddPropertiesAsQueryColumns(qry);
                    Task<ManifestHeaderLoader> taskOrder = qry.QueryToTypedObjectAsync<ManifestHeaderLoader>();
                    Order = taskOrder.Result;
                }
```

##4. Manipulate the Data in the handlebars template
- Navigate to the index.ts file inside the report folder in the WebAPI project WebpackReports -> Reports -> 'SOMENAME'Reports -> 'SOMENAME'Report -> index.ts
- The file should have an API request specified to the URL to run the report (created in the API Controller in the same folder)

```
Ajax.post<DataTable>(`${apiUrl}/api/v1/manifestsummaryreport/runreport`, authorizationHeader, parameters)
                        .then((response: any) => {
                            const data: any = response;
                            data.Items = DataTable.toObjectList(response.ItemsTable);
                            data.Company = parameters.companyName;
                            data.OrderNumber = parameters.orderno;
                            data.Report = "Value Sheet";
```

- The response contains the data we need to manipulate for the report. With the example above, the properties attached to our data object are injected into the handlebars template
- The handlebars template uses standard HTML but has the ability to apply logic through handlebars helpers - 

```
<tbody>
            {{#each Items}}
            {{#ifEquals RowType "detail"}}
    <tr data-rowtype="{{RowType}}" class="description-row">
```

- The property values are injected into the template using double curly braces around the property name 

```
<div style="max-width: 283px;" class="rpt-flexcolumn">
        <span class="sub-title blue">{{Department}}</span>
        <span class="title blue">{{Report}}</span>
    </div>
```

##Note 
- While developing reports locally, we want to build the report we are using or all of the reports using the proper node command.
- We can run the command that "watches" the report for any updates so we can live refresh without having to build the report each time a change is made.
- Ex for a single report: npm run watch-reports-dev-byname ReturnListReport.

- We can manipulate other report information through various stages of the report in the Web controller typescript file. eg - 
In the afterLoad method or convertParameters method etc...





