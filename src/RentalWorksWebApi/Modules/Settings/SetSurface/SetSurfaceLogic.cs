using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.SetSurface
{
    public class SetSurfaceLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        SetSurfaceRecord setSurface = new SetSurfaceRecord();
        public SetSurfaceLogic()
        {
            dataRecords.Add(setSurface);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SetSurfaceId { get { return setSurface.SetSurfaceId; } set { setSurface.SetSurfaceId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string SetSurface { get { return setSurface.SetSurface; } set { setSurface.SetSurface = value; } }
        public bool? Inactive { get { return setSurface.Inactive; } set { setSurface.Inactive = value; } }
        public string DateStamp { get { return setSurface.DateStamp; } set { setSurface.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
