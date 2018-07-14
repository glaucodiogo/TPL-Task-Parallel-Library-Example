using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallelLibSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t1 = Task.Factory.StartNew(() => { DoSomeWork(1, 1000); });
            Task t2 = Task.Factory.StartNew(() => { DoSomeWork(2, 2000); });
            Task t3 = Task.Factory.StartNew(() => { DoSomeWork(3, 3000); });

            Task t4 = Task.Factory.StartNew(() => { return GetEmployee(); })
                .ContinueWith(t =>
                {
                    t.Result.CalculateTotalCostToCompany();
                    Console.WriteLine("{0}'s CTC is {1}", t.Result.Name, t.Result.TotalAllowance);
                });


            List<Task> taskList = new List<Task> { t1, t2, t3, t4 };

            Task.WaitAll(taskList.ToArray());

            Console.WriteLine("####################### Going to start Parallel.ForEach #######################");

            List<Employee> employees = GetEmployees();

            Parallel.ForEach(employees, (employee) =>
            {
                Console.WriteLine("started calculating CTC for {0}", employee.Name);
                employee.CalculateTotalCostToCompany();
                Console.WriteLine("calculated CTC for {0} is {1}", employee.Name, employee.TotalAllowance);
            });

            Console.WriteLine("####################### Checking parallel options #######################");

            ParallelOptions po = new ParallelOptions();
            po.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.ForEach(employees, po, (employee) =>
            {
                Console.WriteLine("Employee {0} processed by Task - {1}", employee.Name, Task.CurrentId);
            });

            Console.WriteLine("press any key to quit");
            Console.ReadLine();
        }

        static void DoSomeWork(int taskId, int delayTime)
        {
            Console.WriteLine("Task {0} started !!!!", taskId);
            Thread.Sleep(delayTime);
            Console.WriteLine("Task {0} completed...", taskId);
        }

        static Employee GetEmployee()
        {
            Employee e1 = new Employee
            {
                Id = 4,
                Name = "John",
                Allowance = 500,
                Salary = 1000
            };

            return e1;
        }

        static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            Employee emp1 = new Employee
            {
                Id = 1,
                Name = "Mark",
                Salary = 1000,
                Allowance = 500
            };
            employees.Add(emp1);

            Employee emp2 = new Employee
            {
                Id = 2,
                Name = "Sara",
                Salary = 500,
                Allowance = 250
            };
            employees.Add(emp2);

            Employee emp3 = new Employee
            {
                Id = 3,
                Name = "Alwin",
                Salary = 320,
                Allowance = 120
            };
            employees.Add(emp3);

            return employees;
        }
    }
}
