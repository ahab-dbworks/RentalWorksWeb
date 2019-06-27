using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class WebUsers
    {
        [DataType(DataType.Text)]
        public string webusersid            { get; set; }

        [DataType(DataType.Text)]
        public string usersid               { get; set; }

        [DataType(DataType.Text)]
        public string contactid             { get; set; }

        [DataType(DataType.Text)]
        public string dealid                { get; set; }

        [DataType(DataType.Text)]
        public string name                  { get; set; }

        [DataType(DataType.Text)]
        public string username              { get; set; }

        [DataType(DataType.Text)]
        public string fullname              { get; set; }

        [DataType(DataType.Text)]
        public string email                 { get; set; }

        [DataType(DataType.Text)]
        public string changepasswordatlogin { get; set; }

        [DataType(DataType.Text)]
        public string primarydepartmentid   { get; set; }

        [DataType(DataType.Text)]
        public string rentaldepartmentid    { get; set; }

        [DataType(DataType.Text)]
        public string salesdepartmentid     { get; set; }

        [DataType(DataType.Text)]
        public string rentalagentusersid    { get; set; }

        [DataType(DataType.Text)]
        public string salesagentusersid     { get; set; }

        [DataType(DataType.Text)]
        public string partsdepartmentid     { get; set; }

        [DataType(DataType.Text)]
        public string labordepartmentid     { get; set; }

        [DataType(DataType.Text)]
        public string miscdepartmentid      { get; set; }

        [DataType(DataType.Text)]
        public string spacedepartmentid     { get; set; }

        [DataType(DataType.Text)]
        public string titletype             { get; set; }

        [DataType(DataType.Text)]
        public string title                 { get; set; }

        [DataType(DataType.Text)]
        public string department            { get; set; }

        [DataType(DataType.Text)]
        public string departmentid          { get; set; }

        [DataType(DataType.Text)]
        public string locationid            { get; set; }

        [DataType(DataType.Text)]
        public string location              { get; set; }

        [DataType(DataType.Text)]
        public string warehouseid           { get; set; }

        [DataType(DataType.Text)]
        public string warehouse             { get; set; }

        [DataType(DataType.Text)]
        public string office                { get; set; }

        [DataType(DataType.Text)]
        public string phoneextension        { get; set; }

        [DataType(DataType.Text)]
        public string fax                   { get; set; }

        [DataType(DataType.Text)]
        public string usertype              { get; set; }

        [DataType(DataType.Text)]
        public string statusCode            { get; set; }

        [DataType(DataType.Text)]
        public string statusMessage         { get; set; }

        [DataType(DataType.Text)]
        public string datestamp             { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}