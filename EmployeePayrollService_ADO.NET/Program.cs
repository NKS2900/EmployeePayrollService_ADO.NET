﻿using System;

namespace EmployeePayrollService_ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*****ADO.NET*****");
            Console.WriteLine("===================");
            EmployeeRepo emp = new EmployeeRepo();
            EmployeeModel empModel = new EmployeeModel();

            empModel.EmployeeID = 107;
            empModel.EmployeeName = "Ajay";
            empModel.BasicPay = 65000.00;
            empModel.start_date = new DateTime(2016, 07, 04);
            empModel.gendre = 'M';
            empModel.PhoneNumber = "1478596123";
            empModel.Address = "paritgalli";
            empModel.Department = "Marketing";
            empModel.Deduction = 6600.00;
            empModel.TaxablePay = 5500;
            empModel.NetPay = 4000;
            empModel.Tax = 5000.00;

            //bool result=emp.InsertEmployee(empModel);
            //emp.GetAllRecords();
            //emp.GetPerticularEmployeeData();
            emp.AggregateFunctionOperations();

        }


    }
}
