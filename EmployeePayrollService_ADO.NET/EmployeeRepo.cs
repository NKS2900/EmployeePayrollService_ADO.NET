using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService_ADO.NET
{
    public class EmployeeRepo
    {
        public static string connectString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=payroll_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectString);

        public void CheckConnection()
        {
            try
            {
                using (this.connection)
                {
                    connection.Open();
                    Console.WriteLine("Databased_Connected....");
                }
            }
            catch
            {
                Console.WriteLine("Database_Not_connected!!!!");
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
