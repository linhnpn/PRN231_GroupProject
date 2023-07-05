using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GroupProject_HRM_Library.DAO
{
    public class TaxDAO
    {
        private readonly HumanResourceManagementContext _dbContext;

        public TaxDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tax>> GetTaxesAsync()
        {
            try
            {
                return await _dbContext.Taxes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Tax>> GetTaxesSortedAsync(
            decimal? minSalary, 
            decimal? maxSalary, 
            double? minPercent = null, 
            double? maxPercent = null, 
            DateTime? addDate = null,
            int? status = null,
            string? orderBy = null)
        {
            try
            {
                var taxList = _dbContext.Taxes.AsQueryable();
                if (minSalary != null && maxSalary != null)
                {
                    taxList = taxList.Where(tax => tax.SalaryMin >= minSalary && tax.SalaryMax <= maxSalary);
                }
                if (minPercent != null && maxPercent != null)
                {
                    taxList = taxList.Where(tax => tax.Percent >= minPercent && tax.Percent <= maxPercent);
                }
                if(addDate != null)
                {
                    taxList = taxList.Where(tax => tax.Timestamp.Date.Equals(addDate.Value.Date));
                }
                if(status != null)
                {
                    taxList = taxList.Where(tax => tax.TaxStatus.Equals(status));
                }
                if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrWhiteSpace(orderBy))
                {

                    switch (orderBy)
                    {
                        case "AddDate_Asc":
                            taxList = taxList.OrderBy(h => h.Timestamp);
                            break;
                        case "AddDate_Desc":
                            taxList = taxList.OrderByDescending(h => h.Timestamp);
                            break;
                        case "Percent_Asc":
                            taxList = taxList.OrderBy(h => h.Percent);
                            break;
                        case "Percent_Desc":
                            taxList = taxList.OrderByDescending(h => h.Percent);
                            break;
                        case "MaxSalary_Asc":
                            taxList = taxList.OrderBy(h => h.SalaryMax);
                            break;
                        case "MaxSalary_Desc":
                            taxList = taxList.OrderByDescending(h => h.SalaryMax);
                            break;
                        case "MinSalary_Asc":
                            taxList = taxList.OrderBy(h => h.SalaryMin);
                            break;
                        case "MinSalary_Desc":
                            taxList = taxList.OrderByDescending(h => h.SalaryMin);
                            break;
                    }
                }
                else
                {
                    taxList = taxList.OrderBy(h => h.TaxID);
                }
                return await taxList.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Tax> GetTaxByIDAsync(int taxId)
        {
            try
            {
                return await _dbContext.Taxes.
                    FirstOrDefaultAsync(tax => tax.TaxID == taxId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddNewTaxAsync(Tax tax)
        {
            try
            {
                await _dbContext.Taxes.AddAsync(tax);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteTax(int taxID)
        {
            try
            {
                Tax? tax = _dbContext.Taxes.
                    FirstOrDefault(tax => tax.TaxID == taxID);
                _dbContext.Taxes.Remove(tax);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateTax(Tax tax)
        {
            try
            {
                var existTax = _dbContext.Taxes.Local.FirstOrDefault(t => t.TaxID.Equals(tax.TaxID));
                if (existTax != null)
                    _dbContext.Entry(existTax).State = EntityState.Detached;
                _dbContext.Entry<Tax>(tax).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Tax>> GetAllTax(int status)
        {
            try
            {
                return await this._dbContext.Taxes.Where(t => t.TaxStatus == status).OrderBy(x => x.SalaryMin).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}