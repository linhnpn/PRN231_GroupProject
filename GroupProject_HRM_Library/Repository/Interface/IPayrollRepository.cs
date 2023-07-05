using GroupProject_HRM_Library.DTOs.Payroll;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface IPayrollRepository
    {
        public Task CreatePayrollRequestAsync(PayrollRequest request);
        public Task<List<GetPayrollResponse>> GetPayrollResponsesByEmplIDAsync(int imployeeID);
    }
}
