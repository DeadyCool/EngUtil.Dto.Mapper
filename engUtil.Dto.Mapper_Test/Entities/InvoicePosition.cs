namespace engUtil.Dto.Mapper_Test.Entities
{
    public class InvoicePosition
    {
        public InvoicePosition(int invoiceId, int lineNum, string itemId, string sku, double ammount, double qty)
        {
            InvoiceId = invoiceId;
            LineNum = lineNum;
            ItemId = itemId;
            SKU = sku;
            Ammount = ammount;
            Qty = qty;
        }

        public int InvoiceId { get; set; }

        public int LineNum { get; set; }

        public string ItemId { get; set; }

        public string SKU { get; set; }

        public double Ammount { get; set; }

        public double Qty { get; set; }
    }
}
