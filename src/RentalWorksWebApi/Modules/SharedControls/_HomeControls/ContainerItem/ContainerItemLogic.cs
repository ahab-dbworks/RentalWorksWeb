using FwStandard.AppManager;
using WebApi.Modules.Home.Item;

namespace WebApi.Modules.Home.ContainerItem
{
    public class ContainerItemLogic : ItemLogic
    {
        //jh 04/17/2019 inheriting this class from ItemLogic and overriding the IsPrimaryKey attribute.  
        //              SecurityId attributes are copied from base class.
        //              Discussed with Mike abuot a potential problem with security because of this
        //              No clear solution was determined.  Potentially need to create an abstract BaseItemLogic class an inherit both ItemLogic and ContainerItem from it and define separate SecurityIds for all Properties

        //------------------------------------------------------------------------------------ 
        ContainerItemLoader containerItemLoader = new ContainerItemLoader();
        ContainerItemBrowseLoader containerItemBrowseLoader = new ContainerItemBrowseLoader();
        public ContainerItemLogic()
        {
            dataLoader = containerItemLoader;
            browseLoader = containerItemBrowseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "ow9igbdLMgEw", IsPrimaryKey: false)]  
        public override string ItemId { get { return item.ItemId; } set { item.ItemId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "IIiYemJaXj9K9", IsPrimaryKey: true, IsReadOnly: true)]
        public override string ContainerItemId { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
