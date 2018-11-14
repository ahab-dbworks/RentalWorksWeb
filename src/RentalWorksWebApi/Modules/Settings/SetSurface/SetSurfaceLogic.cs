using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.SetSurface
{
    [FwLogic(Id:"6Obd1B41X0Ypg")]
    public class SetSurfaceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        SetSurfaceRecord setSurface = new SetSurfaceRecord();
        public SetSurfaceLogic()
        {
            dataRecords.Add(setSurface);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"GZvd3YXWT7h67", IsPrimaryKey:true)]
        public string SetSurfaceId { get { return setSurface.SetSurfaceId; } set { setSurface.SetSurfaceId = value; } }

        [FwLogicProperty(Id:"GZvd3YXWT7h67", IsRecordTitle:true)]
        public string SetSurface { get { return setSurface.SetSurface; } set { setSurface.SetSurface = value; } }

        [FwLogicProperty(Id:"k5Azr9vEbab7")]
        public bool? Inactive { get { return setSurface.Inactive; } set { setSurface.Inactive = value; } }

        [FwLogicProperty(Id:"OEMTKsWTKE27")]
        public string DateStamp { get { return setSurface.DateStamp; } set { setSurface.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }
}
