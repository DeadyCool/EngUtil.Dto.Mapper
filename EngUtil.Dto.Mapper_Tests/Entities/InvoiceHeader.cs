using System;
using System.Collections.Generic;

namespace engUtil.Dto.Mapper_Test.Entities
{
    public class InvoiceHeader
    {

        public InvoiceHeader() { }

        public InvoiceHeader(int recId, 
            string invoiceRef, 
            int vendor, 
            DateTime createdDate, 
            DateTime dueDate, 
            string currencyType,
            int qty, 
            double tax, 
            double ammount,
            IEnumerable<InvoicePosition> positions)
        {
            RecId = recId;
            InvoiceRef = invoiceRef;
            Vendor = vendor;
            CreatedDate = createdDate;
            DueDate = dueDate;
            CurrencyType = currencyType;
            Qty = qty;
            Tax = tax;
            Ammount = ammount;
            Positions = positions;
        }

        public int RecId { get; set; }

        public string InvoiceRef { get; set; }

        public int Vendor { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DueDate { get; set; }

        public string CurrencyType { get; set; }

        public int Qty { get; set; }

        public double Tax { get; set; }

        public double Ammount { get; set; }

        public IEnumerable<InvoicePosition> Positions { get; set;}
    }
}
