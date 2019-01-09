using System;
using System.Collections.Generic;
using System.Text;

namespace engUtil.Dto.Mapper_Test.Models
{
    public class InvoiceLineModel
    {
        public int InvoiceId { get; set; }

        public int LineNumber { get; set; }

        public string ItemId { get; set; }

        public string SKU { get; set; }

        public double Ammount { get; set; }

        public double Qty { get; set; }
    }
}
