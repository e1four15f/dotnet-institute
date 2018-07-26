using System;
using System.Configuration;

namespace lab6
{
    class RepositoryFactory 
    {
        public static IRepository GetRepository(bool sql)
        {
            var tp = ConfigurationManager.AppSettings;

            if (sql)
            {
                string sqlConnect = tp["sqlConnect"];
                return new PaymentRepositorySQL(sqlConnect);
            }
            else
            {
                string path = tp["path"];
                return new PaymentRepository(path + "Bills.xml",
                        path + "PayDocs.csv", path + "Payments.xml"); 
            }
        }
    }
}
