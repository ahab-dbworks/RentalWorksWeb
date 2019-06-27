using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    public class PersonModel
    {
        [DataType(DataType.Text)]
        public string personid { get; set; }

        [DataType(DataType.Text)]
        public string personname { get; set; }
    }
}