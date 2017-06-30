namespace FwStandard.SqlServer
{
    public class FwJsonDataTableColumn
    {
        //---------------------------------------------------------------------------------------------
        public string    Name               = string.Empty;
        public string    DataField          = string.Empty;
        public FwDataTypes DataType        = FwDataTypes.Text;
        public bool      IsUniqueId         = false;
        public bool      IsVisible          = false;
        //---------------------------------------------------------------------------------------------
        public FwJsonDataTableColumn()
        {
            
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataTableColumn(string dataField)
        {
            this.DataField  = dataField;
            this.IsUniqueId = true;
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataTableColumn(string dataField, bool encrypt)
        {
            this.DataField  = dataField;
            this.IsUniqueId = encrypt;
            this.IsVisible  = !encrypt;
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataTableColumn(string name, string dataField)
        {
            this.DataField  = dataField;
            this.IsVisible  = true;
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataTableColumn(string name, string dataField, FwDataTypes dataType)
        {
            this.DataField  = dataField;
            this.DataType   = dataType;
            this.IsVisible  = true;
        }
         //---------------------------------------------------------------------------------------------
        public FwJsonDataTableColumn(string name, string dataField, FwDataTypes dataType, bool isVisible, bool isUniqueId)
        {
            this.Name       = name;
            this.DataField  = dataField;
            this.DataType   = dataType;
            this.IsVisible  = isVisible;
            this.IsUniqueId = isUniqueId;
        }
        //---------------------------------------------------------------------------------------------
    }
}
