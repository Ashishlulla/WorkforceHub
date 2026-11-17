namespace WorkForceHub.Application.DTOs.Payroll
{
    public class CreatePayrollDto
    {
        public int EmployeeId { get; set; }

        public int PayrollYear { get; set; }

        public int PayrollMonth { get; set; }


        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }    
    }
}
