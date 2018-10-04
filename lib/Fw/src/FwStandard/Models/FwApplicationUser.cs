namespace FwStandard.Models
{
    //---------------------------------------------------------------------------------------------
    public class FwApplicationUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    //---------------------------------------------------------------------------------------------
    public class FwIntegration
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
    //---------------------------------------------------------------------------------------------
}
