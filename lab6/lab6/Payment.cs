using System;

namespace lab6
{
    class Payment 
    {
        private string client;
        public string Client { get { return client; } }

        private DateTime payDocDate;
        public DateTime PayDocDate { get { return payDocDate; } }

        private string payDocNumber;
        public string PayDocNumber { get { return payDocNumber; } }

        private DateTime billDate;
        public DateTime BillDate { get { return billDate; } }

        private string billNumber;
        public string BillNumber { get { return billNumber; } }

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
