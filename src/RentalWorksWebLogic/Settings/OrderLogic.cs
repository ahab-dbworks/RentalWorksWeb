//using FwStandard.BusinessLogic;
//using FwStandard.DataLayer;
using System;
using RentalWorksWebDataLayer.Settings;
//using AutoMapper;
//using System.Collections;
using FwStandard.Models;
using System.Collections.Generic;

namespace RentalWorksWebLogic.Settings
{
    public class OrderLogic : RwBusinessLogic
    {
        DealOrderRecord dealOrder = new DealOrderRecord();
        DealOrderDetailRecord dealOrderDetail = new DealOrderDetailRecord();
        //------------------------------------------------------------------------------------
        public OrderLogic()
        {
            dataRecords.Add(dealOrder);
            dataRecords.Add(dealOrderDetail);
        }
        //------------------------------------------------------------------------------------
        public string OrderId { get { return dealOrder.OrderId; } set { dealOrder.OrderId = value; dealOrderDetail.OrderId = value; } }
        public string OrderNumber { get { return dealOrder.OrderNumber; } set { dealOrder.OrderNumber = value; } }
        public string OrderDescription { get { return dealOrder.OrderDescription; } set { dealOrder.OrderDescription = value; } }
        public DateTime? OrderDate { get { return dealOrder.OrderDate; } set { dealOrder.OrderDate = value; } }
        public decimal MaximumCumulativeDiscount { get { return dealOrderDetail.MaximumCumulativeDiscount; } set { dealOrderDetail.MaximumCumulativeDiscount = value; } }
        public string PoApprovalStatusId { get { return dealOrderDetail.PoApprovalStatusId; } set { dealOrderDetail.PoApprovalStatusId = value; } }
        public DateTime? DateStamp { get { return dealOrder.DateStamp; } set { dealOrder.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override IEnumerable<T> Select<T>(BrowseRequestDto request)
        {
            IEnumerable<T> records = dealOrder.Select<T>(request);  //load main table records
            IEnumerable<DealOrderDetailRecord> details = dealOrderDetail.Select<DealOrderDetailRecord>(request);  //load secondary records and map values back to main table "records"
            foreach (dynamic rec in records)
            {
                OrderLogic l = ((OrderLogic)rec);
                foreach (DealOrderDetailRecord d in details)
                {
                    if (l.OrderId == d.OrderId)
                    {
                        l.MaximumCumulativeDiscount = d.MaximumCumulativeDiscount;
                        l.PoApprovalStatusId = d.PoApprovalStatusId;
                    }
                }
            }
            return records;
        }
        //------------------------------------------------------------------------------------
    }
}
