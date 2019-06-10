using FwStandard.AppManager;
﻿using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Modules.Reports.OutContractReport
{
    public class OutContractReportLogic
    {
        private FwApplicationConfig _appConfig = null;
        private FwUserSession _userSession = null;
        public OutContractReportLogic(FwApplicationConfig appConfig, FwUserSession userSession)
        {
            this._appConfig = appConfig;
            this._userSession = userSession;
        }

        public async Task<OutContractReport> Get(string contractid)
        {
            List<Task> tasks = new List<Task>();
            Task<OutContractReport> taskContract;
            Task<List<OutContractItem>> taskContractItems;
            
            using (FwSqlConnection conn = new FwSqlConnection(this._appConfig.DatabaseSettings.ConnectionString, true))
            {
                await conn.OpenAsync();

                // load the Contract header
                FwSqlCommand qry1 = new FwSqlCommand(conn, this._appConfig.DatabaseSettings.ReportTimeout);
                qry1.AddColumn("ContractDate", false, FwDataTypes.Date);
                qry1.AddColumn("BillingStartDate", false, FwDataTypes.Date);
                qry1.AddColumn("BillingStopDate", false, FwDataTypes.Date);
                qry1.AddColumn("EstimatedStartDate", false, FwDataTypes.Date);
                qry1.AddColumn("EstimatedEndDate", false, FwDataTypes.Date);
                qry1.AddColumn("ReceivedByDate", false, FwDataTypes.Date);
                qry1.AddColumn("ReceivedBySignatureDataUrl", false, FwDataTypes.JpgDataUrl);
                qry1.Add("select top 1");
                qry1.Add("  ContractNumber = c.contractno,");
                qry1.Add("  ContractDate = c.contractdate,");
                qry1.Add("  ContractTime = c.contracttime,");
                //qry1.Add("  LocationName = (select company ");
                //qry1.Add("                  from control");
                //qry1.Add("                  where controlid = '1'),");
                qry1.Add("  LocationName = l.company,");  //jh 06/10/2019 use location company name here
                qry1.Add("  LocationAddress1 = l.add1,");
                qry1.Add("  LocationAddress2 = l.add2,");
                qry1.Add("  LocationCity = l.city,");
                qry1.Add("  LocationStateOrProvince = l.state,");
                qry1.Add("  LocationZipCode = l.zip,");
                qry1.Add("  LocationCountry = lc.country,");
                qry1.Add("  LocationPhone = (case l.phone");
                qry1.Add("                     when '(   )    -    ' then ''");
                qry1.Add("                     else l.phone");
			    qry1.Add("                end),");
                qry1.Add("  LocationFax = (case l.fax");
                qry1.Add("                     when '(   )    -    ' then ''");
                qry1.Add("                     else l.fax");
			    qry1.Add("                end),");
                qry1.Add("  LocationEmail = l.email,");
                qry1.Add("  LocationUrl = l.webaddress,");
                qry1.Add("  OrderNumber = o.orderno,");
                qry1.Add("  OrderDescription = o.orderdesc,");
                qry1.Add("  Deal = d.deal,");
                qry1.Add("  PoNumber = dod.activepono,");
                qry1.Add("  PaymentTerms = pterms.payterms,");
                qry1.Add("  BillingCycle = o.ratetype,");
                qry1.Add("  BillingStartDate = o.billperiodstart,");
                qry1.Add("  BillingStopDate = o.billperiodend,");
                qry1.Add("  DealAddress1 = d.add1,");
                qry1.Add("  DealAddress2 = d.add2,");
                qry1.Add("  DealCity = d.city,");
                qry1.Add("  DealStateOrProvince = d.state,");
                qry1.Add("  DealZipCode = d.zip,");
                qry1.Add("  DealCountry = dc.country,");
                qry1.Add("  DealPhone = (case d.phone");
                qry1.Add("                     when '(   )    -    ' then ''");
                qry1.Add("                     else d.phone");
			    qry1.Add("               end),");
                qry1.Add("  DealFax =  (case d.fax");
                qry1.Add("                     when '(   )    -    ' then ''");
                qry1.Add("                     else d.fax");
			    qry1.Add("              end),");
                qry1.Add("  EstimatedStartDate = o.estrentfrom,");
                qry1.Add("  EstimatedEndDate = o.estrentto,");
                qry1.Add("  Agent = ua.namefml,");
                qry1.Add("  AgentEmail = ua.email,");
                qry1.Add("  ReceivedByPrintName = c.personprintname,");
                qry1.Add("  ReceivedBySignatureDataUrl = ai.image,");
                qry1.Add("  ReceivedByDate = ai.datestamp");
                qry1.Add("from contract c with (nolock) ");
                qry1.Add("  left outer join ordercontract oc with (nolock) on (c.contractid = oc.contractid)");
                qry1.Add("  left outer join location l with (nolock) on (c.locationid = l.locationid)");
				qry1.Add("  left outer join country lc with (nolock) on (l.countryid = lc.countryid)");
                qry1.Add("  left outer join dealorder o with (nolock) on (oc.orderid = o.orderid)");
				qry1.Add("  left outer join dealorderdetail dod with (nolock) on (o.orderid = dod.orderid)");
				qry1.Add("  left outer join payterms pterms with (nolock) on (o.paytermsid = pterms.paytermsid)");
                qry1.Add("  left outer join deal d with (nolock) on (c.dealid = d.dealid)");
				qry1.Add("  left outer join country dc with (nolock) on (d.countryid = lc.countryid)");
				qry1.Add("  left outer join users ua with (nolock) on (o.agentid = ua.usersid)");
				qry1.Add("  left outer join appimage ai with (nolock) on (c.contractid = ai.uniqueid1 and ai.rectype = 'CONTRACT_SIGNATURE')");
                qry1.Add("where c.contractid = @contractid");
                qry1.AddParameter("@contractid", contractid);
                taskContract = qry1.QueryToTypedObjectAsync<OutContractReport>();
                tasks.Add(taskContract);

                // load the Contract Items, we will need to separate the Sales and Rental Items later, and leave a placehholder column "Barcodes" where we can merge in the barcode numbers
                FwSqlCommand qry2 = new FwSqlCommand(conn, this._appConfig.DatabaseSettings.ReportTimeout);
                qry2.AddColumn("QuantityOrdered", false, FwDataTypes.DecimalStringNoTrailingZeros);
                qry2.AddColumn("QuantityOut", false, FwDataTypes.DecimalStringNoTrailingZeros);
                qry2.AddColumn("TotalOut", false, FwDataTypes.DecimalStringNoTrailingZeros);
                // common table expression to compute the TotalOut quantity
                qry2.Add(";with TotalOutCte as");
                qry2.Add("(");
                qry2.Add("  select ot.masteritemid, totalqty=sum(ot.qty)");
                qry2.Add("  from ordercontract oc with (nolock)");
                qry2.Add("    join ordertran ot with (nolock) on (oc.contractid = ot.outreceivecontractid)");
                qry2.Add("  where oc.orderid = (select orderid");
                qry2.Add("                      from ordercontract with (nolock)");
                qry2.Add("                      where contractid = @contractid)");
                qry2.Add("    and ot.inreturncontractid = ''");
                qry2.Add("  group by masteritemid");
                qry2.Add(")");
                // the main query
                qry2.Add("select RecordType=fcid.rectype, MasterItemId=fcid.masteritemid, ICode=fcid.masternodisplay, MasterNoColor=fcid.masternocolor, Description=fcid.description, DescriptionColor=fcid.descriptioncolor, QuantityOrdered=mi.qtyordered, QuantityOut=fcid.quantity, TotalOut=toc.totalqty, ItemClass=fcid.itemclass, Notes=fcid.notes, Barcode=fcid.barcode");
                qry2.Add("from dbo.funccontractitemdetail(@contractid) fcid");
                qry2.Add("  left outer join masteritem mi with (nolock) on (fcid.masteritemid = mi.masteritemid)");
                qry2.Add("  left outer join TotalOutCte toc on (toc.masteritemid = fcid.masteritemid)");
                qry2.Add("order by fcid.rectype, fcid.orderby, fcid.barcode");
                qry2.AddParameter("@contractid", contractid);
                taskContractItems = qry2.QueryToTypedListAsync<OutContractItem>();
                tasks.Add(taskContractItems);

                await Task.WhenAll(new Task[] { taskContract, taskContractItems });
            }

            OutContractReport report = taskContract.Result;
            if (report == null)
            {
                throw new Exception($"Unable to find contractid: {contractid}.  Please verify that the calling application is connected to the same database as the API.");
            }
            List<OutContractItem> rentalItems = new List<OutContractItem>();
            List<OutContractItem> salesItems = new List<OutContractItem>();
            Dictionary<string, List<OutContractItem>> groupRentalContractItemByMasterItemId = new Dictionary<string, List<OutContractItem>>();
            Dictionary<string, List<OutContractItem>> groupSalesContractItemByMasterItemId = new Dictionary<string, List<OutContractItem>>();
            for (int contractItemNo = 0; contractItemNo < taskContractItems.Result.Count; contractItemNo++)
            {
                OutContractItem item = taskContractItems.Result[contractItemNo];
                // the query returns a row for each barcode, so we only want to collect the first row for each masteritemid
                if (item.RecordType == "R")
                {
                    if (!groupRentalContractItemByMasterItemId.ContainsKey(item.MasterItemId))
                    {
                        groupRentalContractItemByMasterItemId[item.MasterItemId] = new List<OutContractItem>();
                        rentalItems.Add(item);
                    }
                    if (item.Barcode.Length > 0)
                    {
                        groupRentalContractItemByMasterItemId[item.MasterItemId].Add(item);
                    }
                }
                else if (item.RecordType == "S")
                {
                    if (!groupSalesContractItemByMasterItemId.ContainsKey(item.MasterItemId))
                    {
                        groupSalesContractItemByMasterItemId[item.MasterItemId] = new List<OutContractItem>();
                        salesItems.Add(item);
                    }
                    if (item.Barcode.Length > 0)
                    {
                        groupSalesContractItemByMasterItemId[item.MasterItemId].Add(item);
                    }
                }
            }
            for (int i = 0; i < rentalItems.Count; i++)
            {
                OutContractItem item = rentalItems[i];
                StringBuilder sbBarcodes = new StringBuilder();
                decimal qtyOut = 0;
                for (int j = 0; j < groupRentalContractItemByMasterItemId[item.MasterItemId].Count; j++)
                {
                    // create a comma separated list of barcode numbers by masteritemid
                    if (sbBarcodes.Length > 0)
                    {
                        sbBarcodes.Append(", ");
                    }
                    sbBarcodes.Append(groupRentalContractItemByMasterItemId[item.MasterItemId][j].Barcode);

                    // sum the QuantityOut
                    qtyOut += Convert.ToDecimal(groupRentalContractItemByMasterItemId[item.MasterItemId][j].QuantityOut);
                }
                item.Barcode = sbBarcodes.ToString();
                item.QuantityOut = qtyOut.ToString("G29"); // formatted with no trailing zeros when the value is an integer
            }
            for (int i = 0; i < salesItems.Count; i++)
            {
                OutContractItem item = salesItems[i];
                StringBuilder sbBarcodes = new StringBuilder();
                decimal qtyOut = 0;
                for (int j = 0; j < groupSalesContractItemByMasterItemId[item.MasterItemId].Count; j++)
                {
                    if (sbBarcodes.Length > 0)
                    {
                        sbBarcodes.Append(", ");
                    }
                    sbBarcodes.Append(groupSalesContractItemByMasterItemId[item.MasterItemId][j].Barcode);
                    
                    // sum the QuantityOut
                    qtyOut += Convert.ToDecimal(groupSalesContractItemByMasterItemId[item.MasterItemId][j].QuantityOut);
                }
                item.Barcode = sbBarcodes.ToString();
                item.QuantityOut = qtyOut.ToString("G29"); // formatted with no trailing zeros when the value is an integer
            }
            report.RentalItems = rentalItems;
            report.SalesItems = salesItems;
            return report;
        }
    }
}
