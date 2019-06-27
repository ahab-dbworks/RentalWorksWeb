using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetail
{
    public class OrderStatusItems
    {
        [DataType(DataType.Text)]
        public string rectype                             { get; set; }

        [DataType(DataType.Text)]
        public string masterid                            { get; set; }

        [DataType(DataType.Text)]
        public string masteritemid                        { get; set; }

        [DataType(DataType.Text)]
        public string masterno                            { get; set; }

        [DataType(DataType.Text)]
        public string description                         { get; set; }

        public int qtyordered                             { get; set; }

        public int qtystaged                              { get; set; }

        public int qtyout                                 { get; set; }

        public int qtyin { get; set; }

        public int qtystillout                            { get; set; }

        [DataType(DataType.Text)]
        public string itemorder                           { get; set; }

        [DataType(DataType.Text)]
        public string trackedby                           { get; set; }

        public List<OrderStatusItemDetail> physicalassets { get; set; }
    }
}