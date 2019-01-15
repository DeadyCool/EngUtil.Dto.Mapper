using engUtil.Dto.Mapper_Test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace engUtil.Dto.Mapper_Test.FakeData
{
    public static class Data
    {
        public static IEnumerable<InvoiceHeader> GetInvoices()
        {
            return new List<InvoiceHeader>
            {
                new InvoiceHeader(1,"1247945121", 55, DateTime.Parse("03.02.2017"), DateTime.Parse("17.02.2017"), "EUR", 14, 19, 182.93, GetInvoicePositions().Where(x=> x.InvoiceId == 1).ToList())
            };
        }

        public static IEnumerable<InvoicePosition> GetInvoicePositions()
        {
            return new List<InvoicePosition>
            {
                new InvoicePosition(1,1,"500077842889","UrbanGray.US.L",12.23,1),
                new InvoicePosition(1,2, "500400004658", "BlackGray.US.XL", 11.3, 3),
                new InvoicePosition(1,3,"500000007211","anthraBlack.US.L.L",70.9,4),
                new InvoicePosition(1,4,"504000404339","FRB/BLK.US.M",55.4,5),
                new InvoicePosition(1,5,"500004004972","blue/nights.US.S",33.1,1)
            };
        }
    }
}
