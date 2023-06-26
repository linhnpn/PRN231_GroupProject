﻿using GroupProject_HRM_Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DAO
{
    public class EmployeeDAO
    {
        private HumanResourceManagementContext _dbContext;
        public EmployeeDAO(HumanResourceManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Employee> GetEmployeeByIDAsync(int id)
        {
            try
            {
                return await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}