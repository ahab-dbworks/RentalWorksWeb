using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.BusinessLogic
{
    public class FwBusinessLogicFieldDelta: IComparable<FwBusinessLogicFieldDelta>
    {
        public string FieldName { get; set; } = "";
        public object OldValue { get; set; } = null;
        public object NewValue { get; set; } = null;

        public FwBusinessLogicFieldDelta(string fieldName, object oldValue, object newValue)
        {
            this.FieldName = fieldName;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public int CompareTo(FwBusinessLogicFieldDelta that)
        {
            return this.FieldName.CompareTo(that.FieldName);
        }
    }
}
