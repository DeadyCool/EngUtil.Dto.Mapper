using engUtil.Dto.Mapper_Test.Entities;
using engUtil.Dto.Mapper_Test.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace engUtil.Dto.Mapper_Test.Mapping
{
    public class DtoMapDefinition : MapDefinition
    {
        public DtoMapDefinition(IMapper mapper) 
            : base(mapper)
        {
        }

        [Map]
        public Expression<Func<InvoiceHeader, InvoiceModel>> MapInvoiceHeaderToInvoiceModel
            => x =>
            new InvoiceModel
            {
                Id = x.RecId,
                InvoiceReferenceId = x.InvoiceRef,
                SallerId = x.Vendor,
                IssueDate = x.CreatedDate,
                DueDate = x.DueDate,
                CurrencyCode = x.CurrencyType,
                QuantityTotal = x.Qty,
                TaxRate = x.Tax,
                Tax = x.Ammount * (x.Tax * 0.01),
                NetAmmountTotal = x.Ammount + (x.Ammount * (x.Tax * 0.01)),
                Details = x.InvoicePositions.Select(p => MapTo<InvoiceLineModel>(p))
            };

        [Map]
        public Expression<Func<InvoicePosition, InvoiceLineModel>> MapInvoiceLineToInvoiceLineModel
            => x =>
            new InvoiceLineModel
            {
                InvoiceId = x.InvoiceId,
                SKU = x.SKU,
                ItemId = x.ItemId,
                Ammount = x.Ammount,
                LineNumber = x.LineNum,
                Qty = x.Qty
            };
    }
}
