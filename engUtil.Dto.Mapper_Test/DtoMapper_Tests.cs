using engUtil.Dto.Mapper_Test.Entities;
using engUtil.Dto.Mapper_Test.FakeData;
using engUtil.Dto.Mapper_Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace engUtil.Dto.Mapper_Test
{
    [TestClass]
    public class DtoMapper_Tests
    {
        [TestMethod]
        public void CreateCodeMapping_Test()
        {
            var mapper = new Mapper();

            mapper.Configure(config =>
            {
                config.CreateMappingFor<InvoiceHeader, InvoiceModel>()
                    .AddDescription("Transform InvoiceHeaderEntity To InvoiceModel")
                    .AddMap(x => new InvoiceModel
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
                        InvoiceLines = config.MapTo<InvoiceLineModel>(x.Positions)
                    });

                config.CreateMappingFor<InvoicePosition, InvoiceLineModel>()
                    .AddDescription("InvoicePosition To InvoiceLineModel")
                    .AddMap(x => new InvoiceLineModel
                    {
                         InvoiceId = x.InvoiceId,
                         ItemId = x.ItemId,
                         LineNumber = x.LineNum,
                         Ammount = $"{x.Ammount:c}",
                         Qty = x.Qty,
                         SKU = x.SKU
                    });                
            });

            var invoices = Data.GetInvoices();               

            var result = invoices.Select(x=>  mapper.MapTo<InvoiceModel>(x)).ToList();

            Assert.IsTrue(result.FirstOrDefault(x=> x.Id == 1).TaxRate == 19);
        }
    }
}
