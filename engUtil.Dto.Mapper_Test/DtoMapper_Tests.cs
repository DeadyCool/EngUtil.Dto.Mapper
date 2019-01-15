using engUtil.Dto.Mapper_Test.Entities;
using engUtil.Dto.Mapper_Test.FakeData;
using engUtil.Dto.Mapper_Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using engUtil.Dto.Mapper_Test.Mapping;

namespace engUtil.Dto.Mapper_Test
{
    [TestClass]
    public class DtoMapper_Tests
    {
        [TestMethod]
        public void CreateMapping_Test()
        {
            var mapper = new Mapper();
            mapper.Configure(config =>
            {
                // Map for invoices
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
                        Details = x.InvoicePositions.Select(m =>  mapper.MapTo<InvoiceLineModel>(m))
                    });
                
                // Map for invoice Positions
                config.CreateMappingFor<InvoicePosition, InvoiceLineModel>()
                    .AddDescription("Transform InvoicePositionEntity To InvoiceLineModel")
                    .AddMap(x => new InvoiceLineModel
                    {
                        InvoiceId = x.InvoiceId,
                        SKU = x.SKU,
                        ItemId = x.ItemId,
                        Ammount = x.Ammount,
                        LineNumber = x.LineNum,
                        Qty = x.Qty
                    });
            });

            var invoices = Data.GetInvoices();
            var result = invoices.Select(x=>  mapper.MapTo<InvoiceModel>(x)).ToList().FirstOrDefault(x => x.Id == 1);
            Assert.IsTrue(result.TaxRate == 19 && result.Details.Count() == 5);
        }

        [TestMethod]
        public void ScanMappings_Test()
        {
            var mapper = new DtoMapper();
            var invoices = Data.GetInvoices();
            var result = invoices.Select(x => mapper.MapTo<InvoiceModel>(x)).ToList().FirstOrDefault(x => x.Id == 1);
            Assert.IsTrue(result.TaxRate == 19 && result.Details.Count() == 5);
        }
    }
}
