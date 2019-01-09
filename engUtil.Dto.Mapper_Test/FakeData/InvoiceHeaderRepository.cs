using engUtil.Dto.Mapper_Test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace engUtil.Dto.Mapper_Test.FakeData
{
    public class InvoiceHeaderRepository : IRepository<InvoiceHeader>
    { 
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvoiceHeader> Get(Func<InvoiceHeader, bool> filter)
        {
            return Data.GetInvoices().Where(filter);               
        }

        public IEnumerable<InvoiceHeader> Get(Expression<Func<InvoiceHeader, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public InvoiceHeader Insert(InvoiceHeader entity)
        {
            throw new NotImplementedException();
        }

        public void Update(InvoiceHeader entity)
        {
            throw new NotImplementedException();
        }
    }
}
