using System;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using FwStandard.BusinessLogic;

namespace FwStandard.Modules.Administrator.Alert
{
    public static class AlertFunc
    {
        //public static List<AlertLogic> alerts;   // this would be a singular static list of all Alerts in the system
                                                   // Phase 2, we will convert this List into a Dictionary to make it even faster
        //-------------------------------------------------------------------------------------------------------        
        public static async void RefreshAlerts(FwApplicationConfig appConfig)
        {
            //this method would get called once on start up  (\lib\Fw\src\FwCore\Api\FwStartup.cs\ConfigureServices)
            // this method would also need to get called in the AfterSave method of AlertLogic.  Search for OnAfterSaveDuplicateRule for an example to copy

            // basically port/copy your FwBusinessLogic.refreshAlerts method here.
        }
        //-------------------------------------------------------------------------------------------------------        
        public static async void ProcessAlerts (FwApplicationConfig appConfig, string moduleName, object oldObject, object newObject, TDataRecordSaveMode saveMode)  // this would replace your FwBusinessLogic.CheckAlertsAsync(TDataRecordSaveMode saveMode)
        {
            //if the module has any Alerts associated
            //   iterate through each Alert definition and see if any apply to the existing data modification.  Use oldObject and newObject to compare
            //      send the email(s) if applicable
            return;
        }
        //-------------------------------------------------------------------------------------------------------        




        // inside FwBusinessLogic.SaveAsync, where you're calling CheckAlertsAsync, pull all of that code out and just call AlertFunc.ProcessAlerts(with params) to get the functionality
        // this way, all of the Alert logic will be in this one file.



        // since we are having trouble with Email, can you please add logic to log the alert to a new "webalertlog" table (hotfix 155 required for this)?
        // Add Record, Logic, Controller classes for "webalertlog"
        // add a new "History" tab on the Alert form. And a grid to display the webalertlog data - newest on top

    }
}
