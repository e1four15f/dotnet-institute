using System;

namespace lab4
{
    class PayDoc
    {
        private string client;
        public string Client { get { return client; } }

        private DateTime date;
        public DateTime Date { get { return date; } }

        private string number;
        public string Number { get { return number; } }

        public double sum;

        public PayDoc(string client, DateTime date, string number, double sum)
        {
            this.client = client;
            this.date = date;
            this.number = number;
            this.sum = sum;
        }

        public override string ToString()
        {
            return String.Format("{0, 15}{1, 15}{2, 15}{3, 15}",
                    client, date.Date.ToShortDateString(), number, sum);
        }
    }
}