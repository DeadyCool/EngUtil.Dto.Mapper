using System;
using System.Collections.Generic;
using System.Text;

namespace engUtil.Dto.Mapper_Test.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public string InvoiceReferenceId { get; set; }

        public int SallerId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public string CurrencyCode { get; set; }

        public int QuantityTotal { get; set; }

        public double TaxRate { get; set; }

        public double Tax { get; set; }

        public double NetAmmountTotal { get; set; }
    }
}
