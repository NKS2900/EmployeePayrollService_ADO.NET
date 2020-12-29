using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService_ADO.NET
{
    public class EmployeeRepo
    {
        public static string connectString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=payroll_service;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectString);

        public void GetAllRecords()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"select * from employee_payroll";

                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.BasicPay = Convert.ToDouble(dr.GetDecimal(2));
                            employeeModel.start_date = dr.GetDateTime(3);
                            employeeModel.gendre = Convert.ToChar(dr.GetString(4));
                            employeeModel.PhoneNumber = dr.GetString(5);
                            employeeModel.Address = dr.GetString(6);
                            employeeModel.Department = dr.GetString(7);
                            employeeModel.Deduction = dr.GetDouble(8);
                            employeeModel.TaxablePay = (float)dr.GetSqlSingle(9);
                            employeeModel.NetPay = (float)dr.GetSqlSingle(10);
                            employeeModel.Tax = dr.GetDouble(11);

                            Console.WriteLine(employeeModel.EmployeeID + " , " + employeeModel.EmployeeName + " , " + employeeModel.Address + " , " + employeeModel.gendre + " , " + employeeModel.Department + " , " + employeeModel.NetPay + " , " + employeeModel.start_date + " , " + employeeModel.PhoneNumber);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Table is Empty....");
                    }
                    dr.Close();
                    this.connection.Close();
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        public bool InsertEmployee(EmployeeModel empModel)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("payrollProcedure", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", empModel.EmployeeID);
                    command.Parameters.AddWithValue("@EmployeeName", empModel.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", empModel.BasicPay);
                    command.Parameters.AddWithValue("@StartDate", empModel.start_date);
                    command.Parameters.AddWithValue("@Gender", empModel.gendre);
                    command.Parameters.AddWithValue("@PhoneNumber", empModel.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", empModel.Address);
                    command.Parameters.AddWithValue("@Department", empModel.Department);
                    command.Parameters.AddWithValue("@Deduction", empModel.Deduction);
                    command.Parameters.AddWithValue("@TaxablePay", empModel.TaxablePay);
                    command.Parameters.AddWithValue("@NetPay", empModel.NetPay);
                    command.Parameters.AddWithValue("@Tax", empModel.Tax);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
