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
                Bill bill = null;
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
                    Console.ReadKey();
                    System.Environment.Exit(1);
                }
                yield return bill;
            }
        }

        public IEnumerable<PayDoc> GetPayDocs() 
        {
            string[] csv = File.ReadAllLines(payDocsPath);
            for (int i = 1; i < csv.Length; i++)
            {
                PayDoc payDoc = null;
                try
                {
                    string[] data = csv[i].Split(';');
                    payDoc = new PayDoc(data[0], DateTime.Parse(data[1]), data[2],
                        Double.Parse(data[3], CultureInfo.InvariantCulture));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    System.Environment.Exit(1);
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
                    new XAttribute("Client", payment.Client),
                    new XAttribute("PayDocDate", payment.PayDocDate.ToString("dd.MM.yyyy")),
                    new XAttribute("PayDocNumber", payment.PayDocNumber),
                    new XAttribute("BillDate", payment.BillDate.ToString("dd.MM.yyyy")),
                    new XAttribute("BillNumber", payment.BillNumber),
                    new XAttribute("Sum", payment.sum)));
            }

            doc.Save(paymentsPath);
        }
    }
}