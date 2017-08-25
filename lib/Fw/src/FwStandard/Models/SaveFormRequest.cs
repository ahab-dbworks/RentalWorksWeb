using System.Dynamic;

namespace FwStandard.Models
{
    public class SaveFormRequest
    {
        public dynamic fields { get; set; } = new ExpandoObject();
        public dynamic ids { get; set; } = new ExpandoObject();
        public dynamic miscfields { get; set; } = new ExpandoObject();
        public string mode { get; set; } = string.Empty;
        public string module { get; set; } = string.Empty;

        public SaveFormRequest()
        {

        }
    }
}
