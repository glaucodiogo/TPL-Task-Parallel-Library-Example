using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallelLibSample
{

    /// <summary>
    /// Employee class
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Holds Id of employee
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Holds name of employee
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Holds salary of employee
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Holds allowance for employee
        /// </summary>
        public int Allowance { get; set; }

        /// <summary>
        /// Holds total allowance of employee
        /// </summary>
        public int TotalAllowance { get; private set; }

        /// <summary>
        /// Calculate total cost of employee for company
        /// </summary>
        public void CalculateTotalCostToCompany()
        {
            this.TotalAllowance = this.Salary + this.Allowance;
            Thread.Sleep(this.TotalAllowance);
        }
    }
}
