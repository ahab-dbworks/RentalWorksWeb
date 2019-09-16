using FwStandard.AppManager;
using System;
using System.Collections.Generic;

namespace WebApi.ApplicationManager
{
    public class AppManager : FwAppManager
    {
        protected override string Unabbreviate(string value)
        {
            string result = value;
            switch(value)
            {
                case "Rw": result = "RentalWorks"; break;
                case "Tw": result = "TrakitWorks"; break;
                case "S": result = "Standard"; break;
                case "P": result = "Pro"; break;
                case "E": result = "Enterprise"; break;
            }
            return result;
        }

        protected override string GetDefaultEditions()
        {
            return "Rw|S|P|E,Tw|S|P|E";
        }

        protected override List<Type> GetControllerTypes()
        {
            List<Type> appDataControllerTypes = this.LoadTypes(this.GetType().Assembly, "WebApi", typeof(WebApi.Controllers.AppDataController));
            List<Type> appReportControllerTypes = this.LoadTypes(this.GetType().Assembly, "WebApi", typeof(WebApi.Controllers.AppReportController));
            List<Type> controllerTypes = new List<Type>();
            controllerTypes.AddRange(appDataControllerTypes);
            controllerTypes.AddRange(appReportControllerTypes);

            return controllerTypes;
        }

        protected override List<Type> GetLogicTypes()
        {
            List<Type> logicTypes = this.LoadTypes(this.GetType().Assembly, "WebApi", typeof(WebApi.Logic.AppBusinessLogic));
            return logicTypes;
        }

        //protected override List<FwAmApplication> GetModules()
        //{
        //    List<FwAmApplication> apps = new List<FwAmApplication>();

        //    FwAmApplication appRentalWorksWeb = new FwAmApplication() { Id="21C5311E0B3944ED8F0BDA3F414DC8E9" };
        //    FwAmApplicationCategory catAgent = new FwAmApplicationCategory() { };
        //    FwAmApplicationCategory catInventory = new FwAmApplicationCategory() { };
        //    FwAmApplicationCategory catWarehouse = new FwAmApplicationCategory() { };
        //    FwAmApplicationCategory catUtilities = new FwAmApplicationCategory() { };
        //    FwAmApplicationCategory catAdministrator = new FwAmApplicationCategory() { };
        //    appRentalWorksWeb.Categories.Add(catAgent);
        //    appRentalWorksWeb.Categories.Add(catInventory);
        //    appRentalWorksWeb.Categories.Add(catWarehouse);
        //    appRentalWorksWeb.Categories.Add(catUtilities);
        //    appRentalWorksWeb.Categories.Add(catAdministrator);


        //    catAgent.Modules.Add(new FwAmModule() { Id="0E8C50AFA162491D9A61B4F4EBEFD6D1", Category="Agent", Name="Quote" });
        //    catAgent.Modules.Add(new FwAmModule() { Id="ED730D5FDE984F9290564F4B0BCD07FB", Category="Agent", Name="Order" });
        //    catAgent.Modules.Add(new FwAmModule() { Id="AD330C06C90C471B872D9D3797E8AD6E", Category="Agent", Name="Customer" });
        //    catAgent.Modules.Add(new FwAmModule() { Id="3F0BC04B1B774B789EAAE5894E033F06", Category="Agent", Name="Deal" });
        //    catAgent.Modules.Add(new FwAmModule() { Id="D14E0DE681DE4DB9BB0AB9A1F9C6AA2E", Category="Agent", Name="Vendor" });
        //    catAgent.Modules.Add(new FwAmModule() { Id="1C680E21E8B04C85AA20973DAE44EE10", Category="Agent", Name="Contact" });
        //    catAgent.Modules.Add(new FwAmModule() { Id="DE5DD1F05F884D88949B12C3788262D0", Category="Agent", Name="Purchase Order" });
        //    catAgent.Modules.Add(new FwAmModule() { Id="B49B194BD4154157B39556514BD7CD0B", Category="Agent", Name="Project" });

        //    catInventory.Modules.Add(new FwAmModule() { Id="D752943029744FE9875BB9FFA69D57AB", Category="Inventory", Name="Rental Inventory" });
        //    catInventory.Modules.Add(new FwAmModule() { Id="B83EED776A304D04874FAC04CF50FC5B", Category="Inventory", Name="Sales Inventory" });
        //    catInventory.Modules.Add(new FwAmModule() { Id="FDB09589D34C49E8BB86D9C5751EFB43", Category="Inventory", Name="Parts Inventory" });
        //    catInventory.Modules.Add(new FwAmModule() { Id="CFF36AAAF2324B40B51D62D072DF84AF", Category="Inventory", Name="Asset" });
        //    catInventory.Modules.Add(new FwAmModule() { Id="0D293ACF5AB74E298D2D8883B355D4F6", Category="Inventory", Name="Container" });
        //    catInventory.Modules.Add(new FwAmModule() { Id="1A72938A1B364EE587098801166A1B4E", Category="Inventory", Name="Repair Order" });

        //    catWarehouse.Modules.Add(new FwAmModule() { Id="EF40F44A352D4BBC97FF4CD2E3C72ACC", Category="Warehouse", Name="Order Status" });
        //    catWarehouse.Modules.Add(new FwAmModule() { Id="B0569FEFF2584D81AB68ED31943FD613", Category="Warehouse", Name="Pick List" });
        //    catWarehouse.Modules.Add(new FwAmModule() { Id="DEB5E60CFB1546D49AB802E8966DAED4", Category="Warehouse", Name="Contract" });
        //    catWarehouse.Modules.Add(new FwAmModule() { Id="FD9E6A8F6738488287E84B1CD2B39A86", Category="Warehouse", Name="Staging / Check-Out" });
        //    catWarehouse.Modules.Add(new FwAmModule() { Id="EB3EF1E911864379982C4031BD806F00", Category="Warehouse", Name="Exchange" });
        //    catWarehouse.Modules.Add(new FwAmModule() { Id="F845C3F86EA64C22AAE9A35C1C43A27B", Category="Warehouse", Name="Check-In" });
        //    catWarehouse.Modules.Add(new FwAmModule() { Id="D05E46BB6D204E78BC2ECBBDF7264827", Category="Warehouse", Name="Receive from Vendor" });
        //    catWarehouse.Modules.Add(new FwAmModule() { Id="76E00AEA2E764841B1E62AB0B0314939", Category="Warehouse", Name="Return to Vendor" });
        //    catWarehouse.Modules.Add(new FwAmModule() { Id="8FFE92FBFC854E61A0C8C793E947EE6B", Category="Warehouse", Name="Assign Barcodes" });

        //    catUtilities.Modules.Add(new FwAmModule() { Id="A28A9A25699447DD983AFEAEF5E9B681", Category="Utilities", Name="Dashboard" });
        //    catUtilities.Modules.Add(new FwAmModule() { Id="A11D6D7E299447B9B131D4602C054EF6", Category="Utilities", Name="Dashboard Settings" });

        //    catAdministrator.Modules.Add(new FwAmModule() { Id="F0D6EA7E2FBC4441AD0BC05388272743", Category="Administrator", Name="Control" });
        //    catAdministrator.Modules.Add(new FwAmModule() { Id="2AA8D85D72CE41DAAFF4DAA8D5F1686A", Category="Administrator", Name="Custom Field", Editions="Rw|E,Tw|E" });
        //    catAdministrator.Modules.Add(new FwAmModule() { Id="70FF8A7EABDA44C78BF8C70A6010599A", Category="Administrator", Name="Duplicate Rule" });
        //    catAdministrator.Modules.Add(new FwAmModule() { Id="0443E3CBD1CF491D905314D71B42FD86", Category="Administrator", Name="Group" });
        //    catAdministrator.Modules.Add(new FwAmModule() { Id="A8D4A670F1834F8F9F25B97565140CAC", Category="Administrator", Name="User" });
        //    catAdministrator.Modules.Add(new FwAmModule() { Id="4907FC5856904B9B87D503FE47D22447", Category="Administrator", Name="Settings" });
        //    catAdministrator.Modules.Add(new FwAmModule() { Id="8A671561CADC41A999223B532903BA6E", Category="Administrator", Name="Reports" });
        //    catAdministrator.Modules.Add(new FwAmModule() { Id="DFDEAB007FB64BA5B6A1C770EAFAB01B", Category="Administrator", Name="Designer" });

        //    return appRentalWorksWeb;
        //}
    }
}
