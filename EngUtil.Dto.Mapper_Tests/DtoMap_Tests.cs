using engUtil.Dto.Mapper_Test.Entities;
using engUtil.Dto.Mapper_Test.Models;
using EngUtil.Dto.Genercis;
using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace EngUtil.Dto.Mapper_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DtoMap_Base_Mapping_Test()
        {
            // expression delegate
            Expression<Func<InvoiceHeader, InvoiceModel>>  expression = x => new InvoiceModel
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
                NetAmmountTotal = x.Ammount + (x.Ammount * (x.Tax * 0.01))
            };

            IDtoMap dtoMap = new DtoMap(typeof(InvoiceHeader), typeof(InvoiceModel), expression);

            var invoiceHeader = new InvoiceHeader
            {
                RecId = 1,
                InvoiceRef = "Invoice 1234",
                Vendor = 123,
                CreatedDate = DateTime.Now,
                DueDate = DateTime.Now,
                CurrencyType = "EUR",
                Qty = 10,
                Tax = 19,
                Ammount = 15
            };

            var transformed = dtoMap.MapObject(invoiceHeader); 

            Assert.Fail();
        }

        [Test]
        public void DtoMap_Generic_Mapping_Test()
        {
            var createdDate = DateTime.Now.AddDays(-14);

            IDtoMap<InvoiceHeader, InvoiceModel> dtoMap = new DtoMap<InvoiceHeader, InvoiceModel>(x => new InvoiceModel
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
                NetAmmountTotal = x.Ammount + (x.Ammount * (x.Tax * 0.01))
            });

            var invoiceHeader = new InvoiceHeader
            {
                RecId = 1,
                InvoiceRef = "Invoice 1234",
                Vendor = 123,
                CreatedDate = DateTime.Now,
                DueDate = DateTime.Now,
                CurrencyType = "EUR",
                Qty = 10,
                Tax = 19,
                Ammount = 15
            };

            var transformed = dtoMap.MapObject(invoiceHeader);

            Assert.Fail();
        }
    }
}