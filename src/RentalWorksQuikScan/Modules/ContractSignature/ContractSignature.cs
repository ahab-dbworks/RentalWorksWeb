﻿using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;

namespace RentalWorksQuikScan.Modules
{
    public class ContractSignature
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void CreateContract(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreateContract";
            string usersid, contracttype, contractid, orderid, responsiblepersonid;
            string printname = string.Empty;

            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractType");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "contractId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "responsiblePersonId");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "signatureImage");
            usersid             = session.security.webUser.usersid;
            contracttype        = request.contractType;
            contractid          = request.contractId;
            orderid             = request.orderId;
            responsiblepersonid = request.responsiblePersonId;
            if (FwValidate.IsPropertyDefined(request, "printname"))
            {
                printname = request.printname;
            }
            
            // Create the contract
            response.createcontract = WebCreateContract(usersid, contracttype, contractid, orderid, responsiblepersonid, printname);

            // insert the signature image
            FwSqlData.InsertAppImage(FwSqlConnection.RentalWorks, contractid, string.Empty, string.Empty, "CONTRACT_SIGNATURE", string.Empty, "JPG", request.signatureImage);

            if ((FwValidate.IsPropertyDefined(request, "images")) && (request.images.Length > 0))
            {
                byte[] image;
                for (int i = 0; i < request.images.Length; i++)
                {
                    image = Convert.FromBase64String(request.images[i]);
                    FwSqlData.InsertAppImage(FwSqlConnection.RentalWorks, contractid, string.Empty, string.Empty, "CONTRACT_IMAGE", string.Empty, "JPG", image);
                }
            }

            if (contracttype == RwAppData.CONTRACT_TYPE_OUT)
            {
                FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
                qry.Add("select orderno, orderdesc");
                qry.Add("  from dealorder with (nolock)");
                qry.Add(" where orderid = @orderid");
                qry.AddParameter("@orderid", orderid);
                qry.Execute();

                response.subject = qry.GetField("orderno").ToString().TrimEnd() + " - " + qry.GetField("orderdesc").ToString().TrimEnd() + " - Out Contract";
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic WebCreateContract(string usersid, string contracttype, string contractid, string orderId, string responsiblePersonId, string printname)
        {
            dynamic result;
            FwSqlCommand sp, qryUpdateDealOrderDetail, qryUpdateContract;

            FwSqlCommand qryInputByUser;
            string inputbyusersid, namefml;
            if (!string.IsNullOrEmpty(contractid)) {
                qryInputByUser = new FwSqlCommand(FwSqlConnection.RentalWorks);
                qryInputByUser.Add("select c.inputbyusersid, u.namefml");
                qryInputByUser.Add("from contract c join usersview u on (c.inputbyusersid = u.usersid)");
                qryInputByUser.Add("where contractid = @contractid");
                qryInputByUser.AddParameter("@contractid", contractid);
                qryInputByUser.Execute();
                inputbyusersid = qryInputByUser.GetField("inputbyusersid").ToString().TrimEnd();
                namefml = qryInputByUser.GetField("namefml").ToString().TrimEnd();
                if (usersid != inputbyusersid) {
                    throw new Exception("Only the session owner " + namefml + " can create a contract.");
                }
            }

            sp = new FwSqlCommand(FwSqlConnection.RentalWorks, "dbo.webcreatecontract");
            sp.AddParameter("@contracttype",    contracttype);
            sp.AddParameter("@contractid",      SqlDbType.NVarChar, ParameterDirection.InputOutput, contractid);
            sp.AddParameter("@orderid",         orderId);
            sp.AddParameter("@usersid",         usersid);
            sp.AddParameter("@personprintname", printname);
            sp.AddParameter("@status",          SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",             SqlDbType.NVarChar, ParameterDirection.Output);
            sp.Execute();
            result            = new ExpandoObject();
            result.contractId = sp.GetParameter("@contractid").ToString().TrimEnd();
            result.status     = sp.GetParameter("@status").ToInt32();
            result.msg        = sp.GetParameter("@msg").ToString().TrimEnd();

            if ((contracttype == RwAppData.CONTRACT_TYPE_OUT) && (!string.IsNullOrEmpty(responsiblePersonId)))
            {
                qryUpdateDealOrderDetail = new FwSqlCommand(FwSqlConnection.RentalWorks);
                qryUpdateDealOrderDetail.Add("update dealorderdetail");
                qryUpdateDealOrderDetail.Add("set responsiblepersonid = @responsiblepersonid");
                qryUpdateDealOrderDetail.Add("where orderid           = @orderid");
                qryUpdateDealOrderDetail.AddParameter("@responsiblepersonid", responsiblePersonId);
                qryUpdateDealOrderDetail.AddParameter("@orderid", orderId);
                qryUpdateDealOrderDetail.ExecuteNonQuery();

                qryUpdateContract = new FwSqlCommand(FwSqlConnection.RentalWorks);
                qryUpdateContract.Add("update contract");
                qryUpdateContract.Add("   set responsiblepersonid = @responsiblepersonid,");
                qryUpdateContract.Add(" where contractId          = @contractId");
                qryUpdateContract.AddParameter("@responsiblepersonid", responsiblePersonId);
                qryUpdateContract.AddParameter("@contractId",          result.contractId);
                qryUpdateContract.ExecuteNonQuery();
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void SendEmail(dynamic request, dynamic response, dynamic session)
        {
            string usersid = RwAppData.GetUsersId(session);
            string contractid = request.contractId;
            string webusersid = session.security.webUser.webusersid;
            string from = request.from;
            string to = request.to;
            string cc = request.cc;
            string subject = request.subject;
            string body = request.body;

            // send the email
            OutContractReport report = new OutContractReport();
            report.EmailPdf(usersid, webusersid, contractid, from, to, cc, subject, body);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void LoadContactEmails(dynamic request, dynamic response, dynamic session)
        {
            FwSqlCommand qry;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from dbo.funccompcontact2(@relatedtoid, @allcompanycontacts)");
            qry.Add(" where inactive <> 'T'");
            qry.Add("   and email    <> ''");
            qry.Add("order by lname");
            qry.AddParameter("@relatedtoid", request.orderid);
            qry.AddParameter("@allcompanycontacts", "F");

            response.contacts = qry.QueryToDynamicList2();
        }
        //---------------------------------------------------------------------------------------------
    }
}