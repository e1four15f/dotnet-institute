using System;
using System.Collections.Generic;

namespace lab6
{
    class PaymentLogic
    {
        public IEnumerable<Payment> GetPayments(IEnumerable<Bill> bills, IEnumerable<PayDoc> payDocs)
        {
            List<Bill> billsList = new List<Bill>();
            foreach (Bill bill in bills)
            {
                billsList.Add(bill);
            }
            billsList.Sort((a, b) => a.date.CompareTo(b.date));

            List<PayDoc> payDocsList = new List<PayDoc>();
            foreach (PayDoc payDoc in payDocs)
            {
                payDocsList.Add(payDoc);
            }
            payDocsList.Sort((a, b) => a.date.CompareTo(b.date));

            List<Payment> paymentList = new List<Payment>();
            while (payDocsList.Count > 0 && billsList.Count > 0)
            {
                for (int i = 0; i < billsList.Count; i++)
                {
                    int index = payDocsList.FindIndex(x => x.client.Equals(billsList[i].client));
                    if (index > -1)
                    {
                        if (billsList[i].sum > payDocsList[index].sum)
                        {
                            billsList[i].sum -= payDocsList[index].sum;
                            paymentList.Add(new Payment(payDocsList[index].client,
                                payDocsList[index].date, payDocsList[index].number,
                                billsList[i].date, billsList[i].number, payDocsList[index].sum));
                            payDocsList.Remove(payDocsList[index]);
                        }
                        else if (billsList[i].sum == payDocsList[index].sum)
                        {
                            paymentList.Add(new Payment(payDocsList[index].client,
                                payDocsList[index].date, payDocsList[index].number,
                                billsList[i].date, billsList[i].number, billsList[i].sum));
                            billsList.Remove(billsList[i]);
                            payDocsList.Remove(payDocsList[index]);
                        }
                        else 
                        {
                            payDocsList[index].sum -= billsList[i].sum;
                            paymentList.Add(new Payment(payDocsList[index].client,
                                payDocsList[index].date, payDocsList[index].number,
                                billsList[i].date, billsList[i].number, billsList[i].sum));
                            billsList.Remove(billsList[i]);
                        }
                        break;
                    }
                    else
                    {
                        billsList.Remove(billsList[i]);
                    }
                }
            }

            foreach (Payment payment in paymentList)
            {
                yield return payment;
            }
        }
    }
}
