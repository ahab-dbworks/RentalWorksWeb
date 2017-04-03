using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace Fw.Json.Services
{
    public static class FwCreditCardService
    {
        //---------------------------------------------------------------------------------------------    
        #region CreditCard Transaction Return Types
        //---------------------------------------------------------------------------------------------
        public class ExpressResult
        {
            public string ExpressResponseType {get;set;}
            public string ExpressResponseCode {get;set;}
            public string ExpressResponseMessage {get;set;}
            public string ExpressTransactionDate {get;set;}
            public string ExpressTransactionTime {get;set;}
            public string ExpressTransactionTimezone {get;set;}
            public string HostResponseCode {get;set;}
            public string HostResponseMessage {get;set;}
            public ExpressCardResult Card {get;set;}
            public ExpressTransactionResult Transaction {get;set;}
            public ExpressResult SystemReversal {get;set;}
        
            public ExpressResult()
            {
                this.ExpressResponseCode        = string.Empty;
                this.ExpressResponseMessage     = string.Empty;
                this.ExpressTransactionDate     = string.Empty;
                this.ExpressTransactionTime     = string.Empty;
                this.ExpressTransactionTimezone = string.Empty;
                this.HostResponseCode           = string.Empty;
                this.HostResponseMessage        = string.Empty;
                this.Card                       = new ExpressCardResult();
                this.Transaction                = new ExpressTransactionResult();
                this.SystemReversal                   = null;
            }
        }
        //---------------------------------------------------------------------------------------------
        public class ExpressTransactionResult
        {
            public string AcquirerData {get;set;}
            public string ApprovalNumber {get;set;}
            public string ApprovedAmount {get;set;}
            public string ProcessorName {get;set;}
            public string TransactionID {get;set;}
            public string TransactionStatus {get;set;}
            public string TransactionStatusCode {get;set;}
            public string ReferenceNumber {get;set;}
            public string TicketNumber {get;set;}
            public string TransactionType {get;set;}

            public ExpressTransactionResult()
            {
                this.AcquirerData          = string.Empty;
                this.ApprovalNumber        = string.Empty;
                this.ApprovedAmount        = string.Empty;
                this.ProcessorName         = string.Empty;
                this.TransactionID         = string.Empty;
                this.TransactionStatus     = string.Empty;
                this.TransactionStatusCode = string.Empty;
                this.ReferenceNumber       = string.Empty;
                this.TicketNumber          = string.Empty;
            }
        }
        //---------------------------------------------------------------------------------------------
        public class ExpressCardResult
        {
            public string CardLogo {get;set;}

            public ExpressCardResult()
            {
                this.CardLogo = string.Empty;
            }
        }
        #endregion
        //---------------------------------------------------------------------------------------------
        #region JSON Web Services
        //---------------------------------------------------------------------------------------------
        public static EndpointAddress GetElementExpressUrl()
        {
            EndpointAddress remoteAddress;
            
            if (FwApplicationConfig.CurrentSite.Dev.UseElementCertUrl)
            {
                remoteAddress = new EndpointAddress("https://certtransaction.elementexpress.com/express.asmx");
            }
            else
            {
                remoteAddress = new EndpointAddress("https://transaction.elementexpress.com/express.asmx");
            }

            return remoteAddress;
        } 
        
        public static void CreditCardSale(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreditCardSale";
            string applicationid, applicationname;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "applicationname");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "applicationversion");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "accountid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "accounttoken");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "acceptorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "terminalid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "magneprintdata");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "referencenumber");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "ticketnumber");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "transactionamount");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "salestaxamount");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "accountnumber");
            applicationname = request.applicationname;
            if      (applicationname == "GateWorks")   applicationid = "2934";
            else if (applicationname == "RentalWorks") applicationid = "2935";
            else throw new ArgumentException("applicationName is invalid [" + METHOD_NAME + "]");
            response.Express = CreditCardSale(applicationid:      applicationid, 
                                              applicationname:    applicationname, 
                                              applicationversion: request.applicationversion,
                                              accountid:          request.accountid, 
                                              accounttoken:       request.accounttoken, 
                                              acceptorid:         request.acceptorid, 
                                              terminalid:         request.terminalid, 
                                              magneprintdata:     request.magneprintdata, 
                                              referencenumber:    request.referencenumber, 
                                              ticketnumber:       request.ticketnumber, 
                                              transactionamount:  request.transactionamount, 
                                              salestaxamount:     request.salestaxamount);
        }
        //---------------------------------------------------------------------------------------------
        public static void CreditCardFullReversal(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CreditCardFullReversal";
            string applicationid, applicationname;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "applicationname");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "applicationversion");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "accountid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "accounttoken");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "acceptorid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "terminalid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "referencenumber");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "ticketnumber");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "transactionamount");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "salestaxamount");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "transactionid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "accountnumber");
            applicationname    = request.applicationname;
            if      (applicationname == "GateWorks")   applicationid = "2934";
            else if (applicationname == "RentalWorks") applicationid = "2935";
            else throw new ArgumentException("applicationName is invalid [" + METHOD_NAME + "]");
            response.Express = CreditCardFullReversal(applicationid:      applicationid, 
                                                      applicationname:    applicationname, 
                                                      applicationversion: request.applicationversion, 
                                                      accountid:          request.accountid, 
                                                      accounttoken:       request.accounttoken, 
                                                      acceptorid:         request.acceptorid, 
                                                      terminalid:         request.terminalid, 
                                                      referencenumber:    request.referencenumber, 
                                                      ticketnumber:       request.ticketnumber, 
                                                      transactionamount:  request.transactionamount, 
                                                      salestaxamount:     request.salestaxamount, 
                                                      transactionid:      request.transactionid);
        }
        //---------------------------------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------------------------------
        #region CreditCard Transactions
        //---------------------------------------------------------------------------------------------
        public static ExpressResult CreditCardSale(string applicationid, string applicationname, string applicationversion, string accountid, 
            string accounttoken, string acceptorid, string terminalid, string magneprintdata, string referencenumber, string ticketnumber, 
            string transactionamount, string salestaxamount)
        {
            ExpressResult result;
            Binding binding;
            EndpointAddress remoteAddress;
            ElementExpress.ExpressSoapClient express;
            ElementExpress.Credentials credentials;
            ElementExpress.Application application;
            ElementExpress.Terminal terminal;
            ElementExpress.Card card;
            ElementExpress.Transaction transaction;
            ElementExpress.Address address;
            ElementExpress.ExtendedParameters[] extendedparamters;
            ElementExpress.Response response;

            binding       = new BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.Transport);
            binding.SendTimeout = new TimeSpan(0, 0, 65);
            remoteAddress = GetElementExpressUrl();
            express       = new ElementExpress.ExpressSoapClient(binding, remoteAddress);

            credentials = new ElementExpress.Credentials();
            credentials.AcceptorID   = acceptorid;
            credentials.AccountID    = accountid;
            credentials.AccountToken = accounttoken;
            
            application = new ElementExpress.Application();
            application.ApplicationID      = applicationid;
            application.ApplicationName    = applicationname;
            application.ApplicationVersion = applicationversion;
            
            terminal = new ElementExpress.Terminal();
            terminal.CardholderPresentCode    = ElementExpress.CardholderPresentCode.Present;
            terminal.CardInputCode            = ElementExpress.CardInputCode.MagstripeRead;
            terminal.CardPresentCode          = ElementExpress.CardPresentCode.Present;
            terminal.ConsentCode              = ElementExpress.ConsentCode.FaceToFace;
            terminal.CVVPresenceCode          = ElementExpress.CVVPresenceCode.UseDefault;
            terminal.CVVResponseType          = ElementExpress.CVVResponseType.Regular;
            terminal.MotoECICode              = ElementExpress.MotoECICode.UseDefault;
            terminal.TerminalCapabilityCode   = ElementExpress.TerminalCapabilityCode.MagstripeReader;
            terminal.TerminalEncryptionFormat = ElementExpress.EncryptionFormat.Format6;
            terminal.TerminalEnvironmentCode  = ElementExpress.TerminalEnvironmentCode.LocalAttended;
            terminal.TerminalID               = terminalid;
            terminal.TerminalType             = ElementExpress.TerminalType.PointOfSale;
            
            card = new ElementExpress.Card();
            card.EncryptedFormat = ElementExpress.EncryptionFormat.Format6;
            card.MagneprintData  = magneprintdata;
            
            transaction = new ElementExpress.Transaction();
            transaction.DuplicateCheckDisableFlag = ElementExpress.BooleanType.False;
            transaction.DuplicateOverrideFlag     = ElementExpress.BooleanType.False;
            transaction.MarketCode                = ElementExpress.MarketCode.Retail;
            transaction.PartialApprovedFlag       = ElementExpress.BooleanType.True;
            transaction.SalesTaxAmount            = salestaxamount;
            transaction.TransactionAmount         = transactionamount;
            transaction.ReferenceNumber           = referencenumber;
            transaction.TicketNumber              = ticketnumber;

            address           = new ElementExpress.Address();
            
            extendedparamters = new ElementExpress.ExtendedParameters[0];
            result = new ExpressResult();
            try
            {
                response = express.CreditCardSale(credentials, application, terminal, card, transaction, address, extendedparamters);
                result.ExpressResponseType               = "CreditCardSale";
                result.ExpressResponseCode               = response.ExpressResponseCode;
                result.ExpressResponseMessage            = response.ExpressResponseMessage;
                result.ExpressTransactionDate            = response.ExpressTransactionDate;
                result.ExpressTransactionTime            = response.ExpressTransactionTime;
                result.ExpressTransactionTimezone        = response.ExpressTransactionTimezone;
                result.HostResponseCode                  = response.HostResponseCode;
                result.HostResponseMessage               = response.HostResponseMessage;
                result.Card.CardLogo                     = response.Card.CardLogo;
                result.Transaction.AcquirerData          = response.Transaction.AcquirerData;
                result.Transaction.ApprovalNumber        = response.Transaction.ApprovalNumber;
                result.Transaction.ApprovedAmount        = response.Transaction.ApprovedAmount;
                result.Transaction.ProcessorName         = response.Transaction.ProcessorName;
                result.Transaction.TransactionID         = response.Transaction.TransactionID;
                result.Transaction.TransactionStatus     = response.Transaction.TransactionStatus;
                result.Transaction.TransactionStatusCode = response.Transaction.TransactionStatusCode;
                result.Transaction.ReferenceNumber       = response.Transaction.ReferenceNumber;
                result.Transaction.TicketNumber          = response.Transaction.TicketNumber;
                result.Transaction.TransactionType       = "Credit Card Sale";
                // Create a charge with $10.01 or $10.02 to test this.  Right now the response says that it needs to reverse the transaction,
                // It really needs to inform the user if the SystemReversal is successful or not 
                if ((response.ExpressResponseCode == "1001") || (response.ExpressResponseCode == "1002"))
                {
                    result.SystemReversal = CreditCardSystemReversal(applicationid, applicationname, applicationversion, accountid, accounttoken, acceptorid, 
                                                                     terminalid, magneprintdata, referencenumber, ticketnumber, transactionamount, salestaxamount);
                }
            }
            catch (TimeoutException ex)
            {
                for(int i = 0; i < 2; i++)
                {
                    result.SystemReversal = CreditCardSystemReversal(applicationid, applicationname, applicationversion, accountid, accounttoken, acceptorid, 
                     
                                                                    terminalid, magneprintdata, referencenumber, ticketnumber, transactionamount, salestaxamount);
                    if (result.SystemReversal.ExpressResponseCode == "0")
                    {
                        break;
                    }
                }
                throw ex;
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static ExpressResult CreditCardFullReversal(string applicationid, string applicationname, string applicationversion, string accountid, string accounttoken, string acceptorid, string terminalid, string referencenumber, string ticketnumber, string transactionamount, string salestaxamount, string transactionid)
        {
            ExpressResult result;
            Binding binding;
            EndpointAddress remoteAddress;
            ElementExpress.ExpressSoapClient express;
            ElementExpress.Credentials credentials;
            ElementExpress.Application application;
            ElementExpress.Terminal terminal;
            ElementExpress.Card card;
            ElementExpress.Transaction transaction;
            ElementExpress.ExtendedParameters[] extendedparamters;
            ElementExpress.Response response;

            binding       = new BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.Transport);
            binding.SendTimeout = new TimeSpan(0, 0, 65);
            remoteAddress = GetElementExpressUrl();
            express       = new ElementExpress.ExpressSoapClient(binding, remoteAddress);
            
            credentials = new ElementExpress.Credentials();
            credentials.AcceptorID   = acceptorid;
            credentials.AccountID    = accountid;
            credentials.AccountToken = accounttoken;
            
            application = new ElementExpress.Application();
            application.ApplicationID      = applicationid;
            application.ApplicationName    = applicationname;
            application.ApplicationVersion = applicationversion;
            
            terminal = new ElementExpress.Terminal();
            terminal.CardholderPresentCode    = ElementExpress.CardholderPresentCode.Present;
            terminal.CardInputCode            = ElementExpress.CardInputCode.MagstripeRead;
            terminal.CardPresentCode          = ElementExpress.CardPresentCode.Present;
            terminal.ConsentCode              = ElementExpress.ConsentCode.FaceToFace;
            terminal.CVVPresenceCode          = ElementExpress.CVVPresenceCode.UseDefault;
            terminal.CVVResponseType          = ElementExpress.CVVResponseType.Regular;
            terminal.MotoECICode              = ElementExpress.MotoECICode.UseDefault;
            terminal.TerminalCapabilityCode   = ElementExpress.TerminalCapabilityCode.MagstripeReader;
            terminal.TerminalEncryptionFormat = ElementExpress.EncryptionFormat.Format6;
            terminal.TerminalEnvironmentCode  = ElementExpress.TerminalEnvironmentCode.LocalAttended;
            terminal.TerminalID               = terminalid;
            terminal.TerminalType             = ElementExpress.TerminalType.PointOfSale;
            
            card = new ElementExpress.Card();
            
            transaction = new ElementExpress.Transaction();
            transaction.DuplicateCheckDisableFlag = ElementExpress.BooleanType.False;
            transaction.DuplicateOverrideFlag     = ElementExpress.BooleanType.False;
            transaction.MarketCode                = ElementExpress.MarketCode.Retail;
            transaction.PartialApprovedFlag       = ElementExpress.BooleanType.False;
            transaction.SalesTaxAmount            = salestaxamount;
            transaction.TransactionAmount         = transactionamount;
            transaction.TransactionID             = transactionid;
            transaction.ReversalType              = ElementExpress.ReversalType.Full;
            transaction.ReferenceNumber           = referencenumber;
            transaction.TicketNumber              = ticketnumber;
            
            extendedparamters = new ElementExpress.ExtendedParameters[0];
            
            response = express.CreditCardReversal(credentials, application, terminal, card, transaction, extendedparamters);
            result = new ExpressResult();
            result.ExpressResponseType               = "CreditCardFullReversal";
            result.ExpressResponseCode               = response.ExpressResponseCode;
            result.ExpressResponseMessage            = response.ExpressResponseMessage;
            result.ExpressTransactionDate            = response.ExpressTransactionDate;
            result.ExpressTransactionTime            = response.ExpressTransactionTime;
            result.ExpressTransactionTimezone        = response.ExpressTransactionTimezone;
            result.HostResponseCode                  = response.HostResponseCode;
            result.HostResponseMessage               = response.HostResponseMessage;
            result.Card.CardLogo                     = response.Card.CardLogo;
            result.Transaction.AcquirerData          = response.Transaction.AcquirerData;
            result.Transaction.ApprovalNumber        = response.Transaction.ApprovalNumber;
            result.Transaction.ApprovedAmount        = response.Transaction.ApprovedAmount;
            result.Transaction.ProcessorName         = response.Transaction.ProcessorName;
            result.Transaction.TransactionID         = response.Transaction.TransactionID;
            result.Transaction.TransactionStatus     = response.Transaction.TransactionStatus;
            result.Transaction.TransactionStatusCode = response.Transaction.TransactionStatusCode;
            result.Transaction.TransactionType       = "Credit Card Reversal";

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static ExpressResult CreditCardSystemReversal(string applicationid, string applicationname, string applicationversion, string accountid, 
            string accounttoken, string acceptorid, string terminalid, string magneprintdata, string referencenumber, string ticketnumber, 
            string transactionamount, string salestaxamount)
        {
            ExpressResult result;
            Binding binding;
            EndpointAddress remoteAddress;
            ElementExpress.ExpressSoapClient express;
            ElementExpress.Credentials credentials;
            ElementExpress.Application application;
            ElementExpress.Terminal terminal;
            ElementExpress.Card card;
            ElementExpress.Transaction transaction;
            ElementExpress.Address address;
            ElementExpress.ExtendedParameters[] extendedparamters;
            ElementExpress.Response response;

            binding       = new BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.Transport);
            binding.SendTimeout = new TimeSpan(0, 0, 65);
            remoteAddress = GetElementExpressUrl();
            express       = new ElementExpress.ExpressSoapClient(binding, remoteAddress);
            
            credentials = new ElementExpress.Credentials();
            credentials.AcceptorID   = acceptorid;
            credentials.AccountID    = accountid;
            credentials.AccountToken = accounttoken;
            
            application = new ElementExpress.Application();
            application.ApplicationID      = applicationid;
            application.ApplicationName    = applicationname;
            application.ApplicationVersion = applicationversion;
            
            terminal = new ElementExpress.Terminal();
            terminal.CardholderPresentCode    = ElementExpress.CardholderPresentCode.Present;
            terminal.CardInputCode            = ElementExpress.CardInputCode.MagstripeRead;
            terminal.CardPresentCode          = ElementExpress.CardPresentCode.Present;
            terminal.ConsentCode              = ElementExpress.ConsentCode.FaceToFace;
            terminal.CVVPresenceCode          = ElementExpress.CVVPresenceCode.UseDefault;
            terminal.CVVResponseType          = ElementExpress.CVVResponseType.Regular;
            terminal.MotoECICode              = ElementExpress.MotoECICode.UseDefault;
            terminal.TerminalCapabilityCode   = ElementExpress.TerminalCapabilityCode.MagstripeReader;
            terminal.TerminalEncryptionFormat = ElementExpress.EncryptionFormat.Format6;
            terminal.TerminalEnvironmentCode  = ElementExpress.TerminalEnvironmentCode.LocalAttended;
            terminal.TerminalID               = terminalid;
            terminal.TerminalType             = ElementExpress.TerminalType.PointOfSale;
            
            card = new ElementExpress.Card();
            card.EncryptedFormat = ElementExpress.EncryptionFormat.Format6;
            card.MagneprintData  = magneprintdata;
            
            transaction = new ElementExpress.Transaction();
            transaction.DuplicateCheckDisableFlag = ElementExpress.BooleanType.False;
            transaction.DuplicateOverrideFlag     = ElementExpress.BooleanType.False;
            transaction.MarketCode                = ElementExpress.MarketCode.Retail;
            transaction.PartialApprovedFlag       = ElementExpress.BooleanType.True;
            transaction.SalesTaxAmount            = salestaxamount;
            transaction.TransactionAmount         = transactionamount;
            transaction.ReversalType              = ElementExpress.ReversalType.System;
            transaction.ReferenceNumber           = referencenumber;
            transaction.TicketNumber              = ticketnumber;
            
            address           = new ElementExpress.Address();
            
            extendedparamters = new ElementExpress.ExtendedParameters[0];
            
            
            extendedparamters = new ElementExpress.ExtendedParameters[0];
            
            response = express.CreditCardReversal(credentials, application, terminal, card, transaction, extendedparamters);
            result = new ExpressResult();
            result.ExpressResponseType               = "CreditCardSystemReversal";
            result.ExpressResponseCode               = response.ExpressResponseCode;
            result.ExpressResponseMessage            = response.ExpressResponseMessage;
            result.ExpressTransactionDate            = response.ExpressTransactionDate;
            result.ExpressTransactionTime            = response.ExpressTransactionTime;
            result.ExpressTransactionTimezone        = response.ExpressTransactionTimezone;
            result.HostResponseCode                  = response.HostResponseCode;
            result.HostResponseMessage               = response.HostResponseMessage;
            result.Card.CardLogo                     = response.Card.CardLogo;
            result.Transaction.AcquirerData          = response.Transaction.AcquirerData;
            result.Transaction.ApprovalNumber        = response.Transaction.ApprovalNumber;
            result.Transaction.ApprovedAmount        = response.Transaction.ApprovedAmount;
            result.Transaction.ProcessorName         = response.Transaction.ProcessorName;
            result.Transaction.TransactionID         = response.Transaction.TransactionID;
            result.Transaction.TransactionStatus     = response.Transaction.TransactionStatus;
            result.Transaction.TransactionStatusCode = response.Transaction.TransactionStatusCode;
            result.Transaction.TransactionType       = "Credit Card Reversal";

            return result;
        }
        //---------------------------------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------------------------------
    }
}
