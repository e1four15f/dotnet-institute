using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace lab6
{
    class PaymentRepositorySQL : IRepository
    {
        private string sqlConnect;

        public PaymentRepositorySQL(string sqlConnect)
        {
            this.sqlConnect = sqlConnect; 
        }

        public IEnumerable<Bill> GetBills()
        {
            using (SqlConnection cnn = new SqlConnection(sqlConnect))
            {
                cnn.Open();
                DataTable bills = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Bills", cnn);
                adapter.Fill(bills);

                foreach (DataRow row in bills.Rows)
                {
                    Bill bill = null;
                    try
                    {
                        bill = new Bill(row["client"].ToString(), 
                            Convert.ToDateTime(row["date"]), 
                            row["number"].ToString(), 
                            Convert.ToDouble(row["sum"]));
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
        }

        public IEnumerable<PayDoc> GetPayDocs()
        {
            using (SqlConnection cnn = new SqlConnection(sqlConnect))
            {
                cnn.Open();
                DataTable payDocs = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.PayDocs", cnn);
                adapter.Fill(payDocs);

                foreach (DataRow row in payDocs.Rows)
                {
                    PayDoc payDoc = null;
                    try
                    {
                        payDoc = new PayDoc(row["client"].ToString(),
                            Convert.ToDateTime(row["date"]),
                            row["number"].ToString(),
                            Convert.ToDouble(row["sum"]));
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
        }

        public void SetPayDocs(IEnumerable<Payment> payments)
        {
            using (SqlConnection cnn = new SqlConnection(sqlConnect))
            {
                cnn.Open();
                foreach (Payment payment in payments)
                { 
                    var insertBill = new SqlCommand(@"INSERT INTO dbo.Payments (PayDocId, BillId, Sum)
                        VALUES (@payDocId, @billId, @sum)", cnn);

                    var BillId = new SqlCommand(@"SELECT Bills.Id FROM dbo.Bills WHERE bills.Number = '" 
                        + payment.BillNumber + "'", cnn).ExecuteScalar();
                    var PayDocId = new SqlCommand(@"SELECT PayDocs.Id FROM dbo.PayDocs WHERE PayDocs.Number = '"
                        + payment.PayDocNumber + "'", cnn).ExecuteScalar();

                    insertBill.Parameters.AddWithValue("@billId", Convert.ToInt32(BillId));
                    insertBill.Parameters.AddWithValue("@payDocId", Convert.ToInt32(PayDocId));
                    insertBill.Parameters.AddWithValue("@sum", payment.sum);
                    insertBill.ExecuteNonQuery();
                }
            }
        }
    }
}
