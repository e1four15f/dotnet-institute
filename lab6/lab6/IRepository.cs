using System;
using System.Collections.Generic;

namespace lab6
{
    interface IRepository
    {
        IEnumerable<Bill> GetBills();

        IEnumerable<PayDoc> GetPayDocs();

        void SetPayDocs(IEnumerable<Payment> payments);
    }
}
