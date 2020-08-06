<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncryptUtility.aspx.cs" Inherits="GateWorksMobile.EncryptUtility" ValidateRequest="false" EnableEventValidation="false"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" onsubmit="document.getElementsByClassName('txtResult')[0].value=''">
    <div>
      <div>Server:</div>
      <div><asp:TextBox id="txtServer" runat="server" TextMode="SingleLine" /></div>
      <div>Database:</div>
      <div><asp:TextBox id="txtDatabase" runat="server" TextMode="SingleLine" /></div>
      <div>User:</div>
      <div><asp:TextBox id="txtUser" runat="server" TextMode="SingleLine" /></div>
      <div>Password:</div>
      <div><asp:TextBox id="txtPassword" runat="server" TextMode="SingleLine" /></div>
      <div><asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" /></div>
      <div style="margin:20px 0 0 0;">Paste this in Application.Config below the Server and Database tags:</div>
      <div><asp:TextBox id="txtResult" CssClass="txtResult" runat="server" TextMode="MultiLine" Height="300px" Width="600px" /></div>
    </div>
    </form>
</body>
</html>
