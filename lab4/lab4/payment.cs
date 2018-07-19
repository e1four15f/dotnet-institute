using System;

namespace lab4
{
    class Payment 
    {
        public string client;
        public DateTime payDocDate;
        public string payDocNumber;
        public DateTime billDate;
        public string billNumber;
        public double sum;

        public Payment(string client, DateTime payDocDate, string payDocNumber,
            DateTime billDate, string billNumber, double sum)
        {
            this.client = client;
            this.payDocDate = payDocDate;
            this.payDocNumber = payDocNumber;
            this.billDate = billDate;
            this.billNumber = billNumber;
            this.sum = sum;
        }

        public override string ToString()
        {
            return String.Format("{0, 15}{1, 15}{2, 15}{3, 15}{4, 15}{5, 15}",
                    client, billDate.Date.ToShortDateString(),
                    billNumber, payDocDate.Date.ToShortDateString(),
                    payDocNumber, sum);
        }
    }
}
