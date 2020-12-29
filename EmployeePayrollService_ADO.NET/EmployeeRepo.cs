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

                            Console.WriteLine(employeeModel.EmployeeID + " , " + employeeModel.EmployeeName + " , " + employeeModel.Address + " , " + employeeModel.gendre + " , " + employeeModel.Department + " , " + employeeModel.NetPay + " , " + employeeModel.start_date + " , " + employeeModel.PhoneNumber
                                                + " , " + employeeModel.BasicPay + " , " + employeeModel.Address + " , " + employeeModel.Deduction + " , " + employeeModel.TaxablePay + " , " + employeeModel.Tax);
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

        public void GetPerticularEmployeeData()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    //Get Basic Pay for Perticular Employee
                    string queryToViewBasicPayOfPerticularEmp = @"SELECT basic_pay FROM employee_payroll WHERE name = 'Kiran'; ";
                    SqlCommand cmd = new SqlCommand(queryToViewBasicPayOfPerticularEmp, this.connection);

                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.BasicPay = Convert.ToDouble(dr.GetDecimal(0));

                            Console.WriteLine("Basic Pay for Kiran is : {0}", employeeModel.BasicPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Table is Empty....");
                    }
                    dr.Close();
                    this.connection.Close();

                    //Retrive Particular recors from Table
                    string EmployeeBetweenDate = @"SELECT * FROM employee_payroll WHERE start_date BETWEEN CAST('2018-01-01' as date) AND GETDATE(); ";
                    SqlCommand sqlcmd = new SqlCommand(EmployeeBetweenDate, this.connection);
                    this.connection.Open();
                    SqlDataReader sqldr = sqlcmd.ExecuteReader();

                    if (sqldr.HasRows)
                    {
                        Console.WriteLine("Joined Date Between: 2018-01-01 And 2020-12-29");
                        Console.WriteLine("\n");
                        while (sqldr.Read())
                        {
                            employeeModel.EmployeeID = sqldr.GetInt32(0);
                            employeeModel.EmployeeName = sqldr.GetString(1);
                            employeeModel.BasicPay = Convert.ToDouble(sqldr.GetDecimal(2));
                            employeeModel.start_date = sqldr.GetDateTime(3);
                            employeeModel.gendre = Convert.ToChar(sqldr.GetString(4));
                            employeeModel.PhoneNumber = sqldr.GetString(5);
                            employeeModel.Address = sqldr.GetString(6);
                            employeeModel.Department = sqldr.GetString(7);
                            employeeModel.Deduction = sqldr.GetDouble(8);
                            employeeModel.TaxablePay = (float)sqldr.GetSqlSingle(9);
                            employeeModel.NetPay = (float)sqldr.GetSqlSingle(10);
                            employeeModel.Tax = sqldr.GetDouble(11);

                            Console.WriteLine(employeeModel.EmployeeID + " , " + employeeModel.EmployeeName + " , " + employeeModel.Address + " , " + employeeModel.gendre + " , " + employeeModel.Department + " , " + employeeModel.NetPay + " , " + employeeModel.start_date + " , " + employeeModel.PhoneNumber
                                                + " , " + employeeModel.BasicPay + " , " + employeeModel.Address + " , " + employeeModel.Deduction + " , " + employeeModel.TaxablePay + " , " + employeeModel.Tax);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Table is Empty....");
                    }
                    //dr.Close();
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
    }
}
