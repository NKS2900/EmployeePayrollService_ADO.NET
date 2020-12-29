using System;

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
            emp.GetAllRecords();
        }
    }
}
