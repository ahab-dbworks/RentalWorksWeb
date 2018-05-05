<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QBOIntegration.aspx.cs" Inherits="TrakItWorksWeb.Integration.QBOIntegration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <link rel="stylesheet" href="QBOIntegration.css" />
    <script type="text/javascript" src="https://js.appcenter.intuit.com/content/ia/intuit.ipp.anywhere-1.3.0.js"></script>
    <script type="text/javascript" src="QBOIntegrationController.js"></script>
    <script>
        intuit.ipp.anywhere.setup({grantUrl:'<%= GrantUrl %>'});
    </script>
    <% if (HttpContext.Current.Session["accessToken"] != null) { Response.Write("<script> window.opener.location.reload();window.close();</script>"); } %>
    <title></title>
  </head>
  <body>
    <form id="qboconnect" runat="server">
      <asp:ScriptManager ID="scriptManager" runat="server" EnablePartialRendering="true" EnablePageMethods="true"/>
      <div class="connectcontainer">
        <div class="logocontainer">
          <div class="logo"></div>
        </div>
        <div class="rightcontainer">
          <div id="connecttoqbo" class="btn connecttoqbo" runat="server">Connect to QuickBooks</div>
          <div id="disconnectqbo" class="btn disconnectqbo" runat="server">Disconnect</div>
        </div>
      </div>
      <div class="connecteddate">
        <div class="date"><div class="datelabel">Last Connected:</div><div id="dateconnected" class="dateconnected" runat="server"></div></div>
        <div id="expire" class="expire" runat="server"><div class="expirelabel">Expires In:</div><div id="expiredays" class="expiredays" runat="server"></div></div>
      </div>
    </form>
  </body>
</html>
