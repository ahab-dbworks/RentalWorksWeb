using System.Threading.Tasks;

namespace FwStandard.Mobile
{
    public class FwJsonRequestAction
    {
        //---------------------------------------------------------------------------------------------
        public delegate bool IsMatchDelegate(string requestPath);
        public delegate Task ActionDelegate(dynamic request, dynamic response, dynamic session);
        //---------------------------------------------------------------------------------------------
        public string[] Roles { get; set; }
        public IsMatchDelegate IsMatch { get; set; }
        public ActionDelegate OnMatch { get; set; }
        //---------------------------------------------------------------------------------------------
        public FwJsonRequestAction(string[] roles, IsMatchDelegate isMatch, ActionDelegate onMatch)
        {
            Roles = roles;
            IsMatch = isMatch;
            OnMatch = onMatch;
        }
        //---------------------------------------------------------------------------------------------
    }
}