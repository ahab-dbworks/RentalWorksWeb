using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class ContractSignature : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public ContractSignature(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task CreateContract(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreateContract";
            string usersid, contracttype, contractid, orderid, responsiblepersonid;
            string printname = string.Empty;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractType");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "responsiblePersonId");
            //FwValidate.TestPropertyDefined(METHOD_NAME, request, "signatureImage");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                usersid = session.security.webUser.usersid;
                contracttype = request.contractType;
                contractid = request.contractId;
                orderid = request.orderId;
                responsiblepersonid = request.responsiblePersonId;
                if (FwValidate.IsPropertyDefined(request, "printname"))
                {
                    printname = request.printname;
                }

                // Create the contract
                response.createcontract = await WebCreateContractAsync(usersid, contracttype, contractid, orderid, responsiblepersonid, printname);

                if (string.IsNullOrEmpty(contractid)) contractid = response.createcontract.contractId;

                // insert the signature image
                if (FwValidate.IsPropertyDefined(request, "signatureImage"))
                {
                    await FwSqlData.InsertAppImageAsync(conn, this.ApplicationConfig.DatabaseSettings, contractid, string.Empty, string.Empty, "CONTRACT_SIGNATURE", string.Empty, "JPG", request.signatureImage);
                }

                if ((FwValidate.IsPropertyDefined(request, "images")) && (request.images != null) && (request.images.Count > 0))
                {
                    byte[] image;
                    for (int i = 0; i < request.images.Count; i++)
                    {
                        image = Convert.FromBase64String(request.images[i].ToString());
                        //FwSqlData.InsertAppImage(conn, contractid, string.Empty, string.Empty, "CONTRACT_IMAGE", string.Empty, "JPG", image);
                        await FwSqlData.WebInsertAppDocumentAsync(conn, this.ApplicationConfig.DatabaseSettings, contractid, string.Empty, "CONTRACT IMAGE", "", usersid, image, "JPG");
                    }
                }

                if (contracttype == RwAppData.CONTRACT_TYPE_OUT)
                {
                    var qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qry.Add("with AgentEmail_CTE(agentemail) as (");
                    qry.Add("    select top 1 agentemail = isnull(u.email, '')");
                    qry.Add("    from ordercontract oc with (nolock)");
                    qry.Add("      left outer join dealorder o with (nolock) on (oc.orderid = o.orderid)");
                    qry.Add("      left outer join users u with (nolock) on (o.agentid = u.usersid)");
                    qry.Add("    where oc.contractid = @contractid");
                    qry.Add(")");
                    qry.Add(", Order_CTE(orderno, orderdesc) as (");
                    qry.Add("    select top 1 orderno = isnull(orderno, ''), orderdesc = isnull(orderdesc, '')");
                    qry.Add("    from dealorder with (nolock)");
                    qry.Add("    where orderid = @orderid");
                    qry.Add(")");
                    qry.Add("select ae.agentemail, o.orderno, o.orderdesc");
                    qry.Add("from AgentEmail_CTE ae, Order_CTE o");
                    qry.AddParameter("@contractid", response.createcontract.contractId);
                    qry.AddParameter("@orderid", orderid);
                    await qry.ExecuteAsync();

                    response.from = qry.GetField("agentemail").ToString().TrimEnd();
                    response.subject = qry.GetField("orderno").ToString().TrimEnd() + " - " + qry.GetField("orderdesc").ToString().TrimEnd() + " - Out Contract";
                } 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebCreateContractAsync(string usersid, string contracttype, string contractid, string orderId, string responsiblePersonId, string printname)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic result;
                FwSqlCommand sp, qryUpdateDealOrderDetail, qryUpdateContract;

                FwSqlCommand qryInputByUser;
                string inputbyusersid, namefml;
                if (!string.IsNullOrEmpty(contractid))
                {
                    qryInputByUser = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qryInputByUser.Add("select c.inputbyusersid, u.namefml");
                    qryInputByUser.Add("from contract c join usersview u on (c.inputbyusersid = u.usersid)");
                    qryInputByUser.Add("where contractid = @contractid");
                    qryInputByUser.AddParameter("@contractid", contractid);
                    await qryInputByUser.ExecuteAsync();
                    inputbyusersid = qryInputByUser.GetField("inputbyusersid").ToString().TrimEnd();
                    namefml = qryInputByUser.GetField("namefml").ToString().TrimEnd();
                    if (usersid != inputbyusersid)
                    {
                        throw new FwBadRequestException("Only the session owner " + namefml + " can create a contract.");
                    }
                }

                sp = new FwSqlCommand(conn, "dbo.webcreatecontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@contracttype", contracttype);
                sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.InputOutput, contractid);
                sp.AddParameter("@orderid", orderId);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@personprintname", printname);
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await sp.ExecuteAsync();
                result = new ExpandoObject();
                result.contractId = sp.GetParameter("@contractid").ToString().TrimEnd();
                result.status = sp.GetParameter("@status").ToInt32();
                result.msg = sp.GetParameter("@msg").ToString().TrimEnd();

                if ((contracttype == RwAppData.CONTRACT_TYPE_OUT) && (!string.IsNullOrEmpty(responsiblePersonId)))
                {
                    qryUpdateDealOrderDetail = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qryUpdateDealOrderDetail.Add("update dealorderdetail");
                    qryUpdateDealOrderDetail.Add("set responsiblepersonid = @responsiblepersonid");
                    qryUpdateDealOrderDetail.Add("where orderid           = @orderid");
                    qryUpdateDealOrderDetail.AddParameter("@responsiblepersonid", responsiblePersonId);
                    qryUpdateDealOrderDetail.AddParameter("@orderid", orderId);
                    await qryUpdateDealOrderDetail.ExecuteNonQueryAsync();

                    qryUpdateContract = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                    qryUpdateContract.Add("update contract");
                    qryUpdateContract.Add("   set responsiblepersonid = @responsiblepersonid");
                    qryUpdateContract.Add(" where contractId          = @contractId");
                    qryUpdateContract.AddParameter("@responsiblepersonid", responsiblePersonId);
                    qryUpdateContract.AddParameter("@contractId", result.contractId);
                    await qryUpdateContract.ExecuteNonQueryAsync();
                }

                return result; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SendEmail(dynamic request, dynamic response, dynamic session)
        {
            string usersid = this.AppData.GetUsersId(session);
            string contractid = request.contractId;
            string webusersid = session.security.webUser.webusersid;
            string from = request.from;
            string to = request.to;
            string cc = request.cc;
            string subject = request.subject;
            string body = request.body;

            string contractType = null;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select contracttype");
                    qry.Add("from contract with (nolock)");
                    qry.Add("where contractid = @contractid");
                    qry.AddParameter("@contractid", request.contractId);
                    await qry.ExecuteAsync();
                    contractType = qry.GetField("contracttype").ToString().TrimEnd();
                }
            }

            if (!string.IsNullOrEmpty(contractType))
            {
                switch(contractType)
                {
                    case "OUT":       
                        // send the email
                        OutContractReport outContractReport = new OutContractReport(this.ApplicationConfig);
                        await outContractReport.EmailPdfAsync(usersid, webusersid, contractid, from, to, cc, subject, body);
                        break;
                    case "IN":
                        InContractReport inContractReport = new InContractReport(this.ApplicationConfig);
                        await inContractReport.EmailPdfAsync(usersid, webusersid, contractid, from, to, cc, subject, body);
                        break;
                    default:
                        throw new Exception($"Emailing contract type: {contractType} is currently unsupported.");
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task LoadContactEmails(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry;

                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select *");
                qry.Add("  from dbo.funccompcontact2(@relatedtoid, @allcompanycontacts)");
                qry.Add(" where inactive <> 'T'");
                qry.Add("   and email    <> ''");
                qry.Add("order by lname");
                qry.AddParameter("@relatedtoid", request.orderid);
                qry.AddParameter("@allcompanycontacts", "F");

                response.contacts = await qry.QueryToDynamicList2Async(); 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}