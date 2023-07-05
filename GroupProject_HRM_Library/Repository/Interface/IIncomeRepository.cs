using GroupProject_HRM_Library.DTOs.Income;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface IIncomeRepository
    {
        public Task<List<GetIncomeEmployeeResponse>> GetIncomeEmplAsync(int id);
        public Task CreateIncomeAsync(List<CreateIncomeEmployeeResponse> request);
        public Task CreateIncomeAsync();
    }
}
