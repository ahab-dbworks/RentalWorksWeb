using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.OrderType;

namespace RentalWorksWebApi.Modules.Settings.PoType
{
    public class PoTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeRecord poType = new OrderTypeRecord();
        PoTypeLoader poTypeLoader = new PoTypeLoader();
        public PoTypeLogic()
        {
            dataRecords.Add(poType);
            dataLoader = poTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PoTypeId { get { return poType.OrderTypeId; } set { poType.OrderTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PoType { get { return poType.OrderType; } set { poType.OrderType = value; } }

        public bool ApprovalNeededByRequired { get { return poType.Poapprovebyrequired; } set { poType.Poapprovebyrequired = value; } }
        public bool ImportanceRequired { get { return poType.Poimportancerequired; } set { poType.Poimportancerequired = value; } }
        public bool PayTypeRequired { get { return poType.Popaytyperequired; } set { poType.Popaytyperequired = value; } }
        public bool ProjectRequired { get { return poType.Poprojectrequired; } set { poType.Poprojectrequired = value; } }

        //sub rental fields
        //sub sales fields
        //purchase fields

        public string RentalPurchaseDefaultRate { get { return poType.Rentalpurchasedefaultrate; } set { poType.Rentalpurchasedefaultrate = value; } }
        public string SalesPurchaseDefaultRate { get { return poType.Salespurchasedefaultrate; } set { poType.Salespurchasedefaultrate = value; } }

        //labor/crew fields
        public bool HideCrewBreaks { get { return poType.Hidecrewbreaks; } set { poType.Hidecrewbreaks = value; } }
        public bool Break1Paid { get { return poType.Break1paId; } set { poType.Break1paId = value; } }
        public bool Break2Paid { get { return poType.Break2paId; } set { poType.Break2paId = value; } }
        public bool Break3Paid { get { return poType.Break3paId; } set { poType.Break3paId = value; } }

        //misc fields
        //sub-crew fields
        //sub-misc fields
        //repairfields

        public bool RwNetDefaultRental { get { return poType.Rwnetrental; } set { poType.Rwnetrental = value; } }
        public bool RwNetDefaultMisc { get { return poType.Rwnetmisc; } set { poType.Rwnetmisc = value; } }
        public bool RwNetDefaultLabor { get { return poType.Rwnetlabor; } set { poType.Rwnetlabor = value; } }


        //public string RentalordertypefieldsId { get { return poType.RentalordertypefieldsId; } set { poType.RentalordertypefieldsId = value; } }
        //public string SalesordertypefieldsId { get { return poType.SalesordertypefieldsId; } set { poType.SalesordertypefieldsId = value; } }
        //public string SpaceordertypefieldsId { get { return poType.SpaceordertypefieldsId; } set { poType.SpaceordertypefieldsId = value; } }
        //public string SubrentalordertypefieldsId { get { return poType.SubrentalordertypefieldsId; } set { poType.SubrentalordertypefieldsId = value; } }
        //public string SubsalesordertypefieldsId { get { return poType.SubsalesordertypefieldsId; } set { poType.SubsalesordertypefieldsId = value; } }
        //public string PurchaseordertypefieldsId { get { return poType.PurchaseordertypefieldsId; } set { poType.PurchaseordertypefieldsId = value; } }
        //public string LaborordertypefieldsId { get { return poType.LaborordertypefieldsId; } set { poType.LaborordertypefieldsId = value; } }
        //public string SublaborordertypefieldsId { get { return poType.SublaborordertypefieldsId; } set { poType.SublaborordertypefieldsId = value; } }
        //public string MiscordertypefieldsId { get { return poType.MiscordertypefieldsId; } set { poType.MiscordertypefieldsId = value; } }
        //public string SubmiscordertypefieldsId { get { return poType.SubmiscordertypefieldsId; } set { poType.SubmiscordertypefieldsId = value; } }
        //public string RepairordertypefieldsId { get { return poType.RepairordertypefieldsId; } set { poType.RepairordertypefieldsId = value; } }
        //public string VehicleordertypefieldsId { get { return poType.VehicleordertypefieldsId; } set { poType.VehicleordertypefieldsId = value; } }
        //public string RentalsaleordertypefieldsId { get { return poType.RentalsaleordertypefieldsId; } set { poType.RentalsaleordertypefieldsId = value; } }
        //public string LdordertypefieldsId { get { return poType.LdordertypefieldsId; } set { poType.LdordertypefieldsId = value; } }

        [JsonIgnore]
        public string OrdType { get { return poType.Ordtype; } set { poType.Ordtype = value; } }
        public decimal? OrderBy { get { return poType.Orderby; } set { poType.Orderby = value; } }
        public bool Inactive { get { return poType.Inactive; } set { poType.Inactive = value; } }
        public string DateStamp { get { return poType.DateStamp; } set { poType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public override void BeforeSave()
        {
            OrdType = "PO";
        }
        //------------------------------------------------------------------------------------ 
    }
}