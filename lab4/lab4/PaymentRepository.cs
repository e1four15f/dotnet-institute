using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace lab4
{
    class PaymentRepository
    {
        private string billsPath;
        private string payDocsPath;
        private string paymentsPath;

        public PaymentRepository(string billsPath, string paydocsPath, string paymentsPath)
        {
            this.billsPath = billsPath;
            this.payDocsPath = paydocsPath;
            this.paymentsPath = paymentsPath;
        }

        public IEnumerable<Bill> GetBills()
        {
            XDocument doc = XDocument.Load(billsPath);
            foreach (XElement e in doc.Root.Nodes())
            {
                Bill bill;
                try
                {
                    bill = new Bill(e.Attribute("Client").Value, 
                        DateTime.Parse(e.Attribute("Date").Value),
                        e.Attribute("Number").Value, 
                        Double.Parse(e.Attribute("Sum").Value, CultureInfo.InvariantCulture));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                yield return bill;
            }
        }

        public IEnumerable<PayDoc> GetPayDocs() 
        {
            string[] csv = File.ReadAllLines(payDocsPath);
            for (int i = 1; i < csv.Length; i++)
            {
                PayDoc payDoc;
                try
                {
                    string[] data = csv[i].Split(';');
                    payDoc = new PayDoc(data[0], DateTime.Parse(data[1]), data[2],
                        Double.Parse(data[3], CultureInfo.InvariantCulture));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                yield return payDoc;
            }
        }

        public void SetPayDocs(IEnumerable<Payment> payments)
        {
            XDocument doc = new XDocument(new XElement("Payments"));

            foreach (Payment payment in payments)
            {
                doc.Root.Add(new XElement("Payment",
                    new XAttribute("Client", payment.client),
                    new XAttribute("PayDocDate", payment.payDocDate.ToString("dd.MM.yyyy")),
                    new XAttribute("PayDocNumber", payment.payDocNumber),
                    new XAttribute("BillDate", payment.billDate.ToString("dd.MM.yyyy")),
                    new XAttribute("BillNumber", payment.billNumber),
                    new XAttribute("Sum", payment.sum)));
            }

            doc.Save(paymentsPath);
        }
    }
}
