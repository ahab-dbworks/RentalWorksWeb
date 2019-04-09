using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fw.Json.Utilities;

namespace GateWorksMobile 
{
    public partial class EncryptUtility : System.Web.UI.Page 
    {
        protected void Page_Load(object sender, EventArgs e) 
        {
            //IPAddress[] ipAddresses;
            //bool isConnectingFromServer;

            //ipAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            //isConnectingFromServer = false;
            //for (int i = 0; i < ipAddresses.Length; i++)
            //{
            //    if (Request.UserHostAddress == ipAddresses[i].ToString())
            //    {
            //        isConnectingFromServer = true;
            //        break;
            //    }
            //}
            //if (!isConnectingFromServer)
            //{
            //    throw new Exception("This utility is only supported from the web server.");
            //}
            btnEncrypt.Click += BtnEncrypt_Click;
        }

        private void BtnEncrypt_Click(object sender, EventArgs e) {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<DatabaseConnection Name=\"RentalWorks\" Encrypted=\"true\">");
            xml.AppendLine("  <Server>" + FwCryptography.AjaxEncrypt2(txtServer.Text) + "</Server>");
            xml.AppendLine("  <Database>" + FwCryptography.AjaxEncrypt2(txtDatabase.Text) + "</Database>");
            xml.AppendLine("  <User>" + FwCryptography.AjaxEncrypt2(txtUser.Text) + "</User>");
            xml.AppendLine("  <Password>" + FwCryptography.AjaxEncrypt2(txtPassword.Text) + "</Password>");
            xml.AppendLine("</DatabaseConnection>");
            txtResult.Text = xml.ToString();
        }
    }
}