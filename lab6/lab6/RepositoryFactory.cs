using System;
using System.Configuration;

namespace lab6
{
    class RepositoryFactory 
    {
        public static IRepository GetRepository()
        {
            var tp = ConfigurationManager.AppSettings;
            string mode = tp["mode"].ToLower();
            if (mode == "sql")
            {
                string sqlConnect = tp["sqlConnect"];
                if (sqlConnect == "")
                {
                    throw new Exception("The sql connection info in App.config is not specified");
                }
                return new PaymentRepositorySQL(sqlConnect);
            }
            else if (mode == "local")
            {
                string path = tp["path"];
                if (path == "" || path == null)
                {
                    throw new Exception("The path to the files in App.config is not specified");
                }
                return new PaymentRepository(path + "Bills.xml",
                        path + "PayDocs.csv", path + "Payments.xml"); 
            }
            else
            {
                throw new Exception("Unknown operation mode set in App.config");
            }
        }
    }
}
