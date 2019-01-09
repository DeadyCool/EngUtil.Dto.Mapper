using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace engUtil.Dto.Mapper_Test.FakeData
{
    public interface IRepository<T>
    {
        T Insert(T entity);

        IEnumerable<T> Get(Expression<Func<T, bool>> filter);

        void Update(T entity);

        void Delete(int id);
    }
}
