
using WorkForceHub.Domain.Enums;

namespace WorkForceHub.Domain.Entities
{
    public class Payroll
    {
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }

        public int PayrollYear { get; set; }

        public int PayrollMonth { get; set; }

        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }
        
        public PayrollStatus Status { get; set; } = PayrollStatus.Pending;
        
        public DateTime? PaymentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
