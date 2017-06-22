using Microsoft.AspNetCore.Mvc;
using RentalWorksLogic.Settings;
using System.Runtime.Serialization;

namespace RentalWorksApi2.Models
{
    public class CustomerStatusDto
    {
        [IgnoreDataMember]
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