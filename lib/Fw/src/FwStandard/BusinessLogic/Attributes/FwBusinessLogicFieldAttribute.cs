using System;

namespace FwStandard.BusinessLogic.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FwBusinessLogicFieldAttribute : Attribute
    {
        public readonly bool IsPrimaryKey;
        public readonly bool IsReadOnly;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwBusinessLogicFieldAttribute(bool isPrimaryKey = false, bool isReadOnly = false)
        {
            IsPrimaryKey = isPrimaryKey;
            IsReadOnly = isReadOnly;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
