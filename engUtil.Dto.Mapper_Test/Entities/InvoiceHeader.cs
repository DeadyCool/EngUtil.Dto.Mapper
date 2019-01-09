using System;

namespace engUtil.Dto.Mapper_Test.Entities
{
    public class InvoiceHeader
    {     
        public InvoiceHeader(int recId, string invoiceRef, int vendor, DateTime createdDate, DateTime dueDate, string currencyType, int qty, double tax, double ammount)
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
    }
}
