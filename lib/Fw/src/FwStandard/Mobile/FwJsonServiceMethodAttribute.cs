namespace FwStandard.Mobile
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class FwJsonServiceMethodAttribute : System.Attribute
    {
        public string RequiredParameters { get; set; } = string.Empty;
        public string OptionalParameters { get; set; } = string.Empty;
    }
}
