using engUtil.Dto.Mapper_Test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace engUtil.Dto.Mapper_Test.FakeData
{
    public class InvoicePositionRepository : IRepository<InvoicePosition>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InvoicePosition> Get(Func<InvoicePosition, bool> filter)
        {
            return Data.GetInvoicePositions().Where(filter);
        }

        public IEnumerable<InvoicePosition> Get(Expression<Func<InvoicePosition, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public InvoicePosition Insert(InvoicePosition entity)
        {
            throw new NotImplementedException();
        }

        public void Update(InvoicePosition entity)
        {
            throw new NotImplementedException();
        }
    }
}