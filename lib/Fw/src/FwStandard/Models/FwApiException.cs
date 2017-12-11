namespace FwStandard.Models
{
    public class FwApiException
    {
        public int StatusCode { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
    }
}
