using RentalWorksLogic.Settings;

namespace RentalWorksApi2.Models
{
    public class CustomerStatusDto
    {
        public string customerStatusId { get; set; }
        public string customerStatus { get; set; }
        public string statusType { get; set; }
        public string creditStatusId { get; set; }
        public string dateStamp { get; set; }

        public CustomerStatusDto()
        {

        }
    }
}