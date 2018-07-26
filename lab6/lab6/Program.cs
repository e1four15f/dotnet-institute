using System;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository repository = RepositoryFactory.GetRepository(true);
            Console.WriteLine(repository);
            PrintInfo(repository);
            Console.Read();
        }

        public static void PrintInfo(IRepository repository)
        {
            Console.WriteLine("Bills");
            Console.WriteLine("{0, 15}{1, 15}{2, 15}{3, 15}",
                    "Клиент", "Дата счёта", "Номер счёта", "Сумма платежа");
            var bills = repository.GetBills();
            foreach (Bill bill in bills)
            {
                Console.WriteLine(bill);
            }

            Console.WriteLine("PayDocs");
            Console.WriteLine("{0, 15}{1, 15}{2, 15}{3, 15}",
                    "Клиент", "Дата платежа", "Номер платежа", "Сумма счёта");
            var payDocs = repository.GetPayDocs();
            foreach (PayDoc payDoc in payDocs)
            {
                Console.WriteLine(payDoc);
            }

            PaymentLogic pl = new PaymentLogic();
            Console.WriteLine("Payments");
            Console.WriteLine("{0, 15}{1, 15}{2, 15}{3, 15}{4, 15}{5, 15}",
                    "Клиент", "Дата счёта", "Номер счёта", "Дата платежа",
                    "Номер платежа", "Сумма счёта");
            var payments = pl.GetPayments(bills, payDocs);
            foreach (Payment payment in payments)
            {
                Console.WriteLine(payment);
            }
            repository.SetPayDocs(payments);

            Console.WriteLine("Done!");
        }
    }
}
