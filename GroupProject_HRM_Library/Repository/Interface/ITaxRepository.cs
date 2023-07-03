using GroupProject_HRM_Library.DTOs.Tax;
using GroupProject_HRM_Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface ITaxRepository
    {
        public Task CreateTaxRequestAsync(CreateTaxRequest request);
        public Task UpdateTaxRequestAsync(int id,UpdateTaxRequest request);
        public Task UpdateTaxStatusRequestAsync(int id, TaxEnum.TaxStatus status);
        public Task<GetTaxResponse> GetTaxResponseAsync(int id);
        public Task<List<GetTaxResponse>> GetTaxResponsesAsync();
        public Task<List<GetTaxResponse>> GetTaxResponsesSortedAsync(
            decimal? minSalary,
            decimal? maxSalary,
            double? minPercent = null,
            double? maxPercent = null,
            DateTime? addDate = null,
            TaxEnum.TaxStatus? status = null,
            TaxEnum.TaxOrderBy? orderBy = null);
        public Task DeleteTaxRequestAsync(int id);
    }
}
