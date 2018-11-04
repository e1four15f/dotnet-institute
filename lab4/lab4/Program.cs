using System;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "../../files/";
            PaymentRepository pr = new PaymentRepository(path + "Bills.xml",
                path + "PayDocs.csv", path + "Payments.xml");
            
            Console.WriteLine("Bills\n{0, 15}{1, 15}{2, 15}{3, 15}",
                    "Клиент", "Дата счёта", "Номер счёта", "Сумма платежа");
            var bills = pr.GetBills();
            foreach (Bill bill in bills)
            {
                Console.WriteLine(bill);
            }
            
            Console.WriteLine("PayDocs\n{0, 15}{1, 15}{2, 15}{3, 15}",
                    "Клиент", "Дата платежа", "Номер платежа", "Сумма счёта");
            var payDocs = pr.GetPayDocs();
            foreach (PayDoc payDoc in payDocs)
            {
                Console.WriteLine(payDoc);
            }
            
            PaymentLogic pl = new PaymentLogic();
            Console.WriteLine("Payments\n{0, 15}{1, 15}{2, 15}{3, 15}{4, 15}{5, 15}",
                    "Клиент", "Дата счёта", "Номер счёта", "Дата платежа",
                    "Номер платежа", "Сумма счёта");
            var payments = pl.GetPayments(bills, payDocs);
            foreach (Payment payment in payments)
            {
                Console.WriteLine(payment);
            }
            pr.SetPayDocs(payments);

            Console.WriteLine("Done!");
            Console.Read();
        }
    }
}