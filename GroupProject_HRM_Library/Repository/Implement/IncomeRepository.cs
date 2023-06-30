﻿using AutoMapper;
using GroupProject_HRM_Library.DTOs.Income;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Infrastructure;
using GroupProject_HRM_Library.Models;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GroupProject_HRM_Library.Repository.Implement
{
    public class IncomeRepository : IIncomeRepository
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private const double _bonusOT = 0.30;

        public IncomeRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateIncomeAsync(List<CreateIncomeEmployeeResponse> request1)
        {
            try
            {
                foreach (var request in request1)
                {
                    //check xem co nhan vien trong cty ko
                    Employee employee = await _unitOfWork.EmployeeDAO.GetEmployeeByIDAsync(request.EmployeeID);
                    if (employee == null)
                    {
                        throw new BadRequestException("The employee does not exist in the system at IncomeRequestRepo.");
                    }

                    //Map profile
                    Income income = _mapper.Map<Income>(request);

                    //check xem thang trc nhan vien co luong kh
                    Income incomeLastMonth = await _unitOfWork.IncomeDAO.GetLastIncomeByEmplIDAsync(request.EmployeeID);
                    if (incomeLastMonth == null) //neu khong co thi set ngay dau cua luong la ngay tham gia du an
                    {
                        EmployeeProject employeeProject = await _unitOfWork.EmployeeProjectDAO
                                                    .GetLastProjectJoinedByEmplIDAsync(request.EmployeeID);
                        if (employeeProject == null)
                        {
                            throw new BadRequestException("The employee does not exist in any project at IncomeRequestRepo.");
                        }
                        income.StartDate = employeeProject.StartDate;
                    }
                    else
                    {
                        //neu co thi set stardate income => ngay enddate cua income thang truoc + 1 day 
                        income.StartDate = incomeLastMonth.EndDate.AddDays(1);
                    }

                    income.EndDate = income.StartDate.AddMonths(1).AddDays(-1);

                    //get luong theo thang cua nhan vien
                    var payrollEmpl = await _unitOfWork.PayrollDAO.GetPayrollByEmpID(request.EmployeeID);
                    if (payrollEmpl == null)
                    {
                        throw new BadRequestException("The employee payroll does not exist in system at IncomeRequestRepo.");
                    }
                    //get luong theo ngay(tru t7 vs cn)
                    var dayInMonth = DateTime.DaysInMonth(income.StartDate.Year, income.StartDate.Month);
                    var getWorkDay = Enumerable.Range(1, dayInMonth)
                     .Select(i => new DateTime(income.StartDate.Year, income.StartDate.Month, i))
                     .Count(i => i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday);

                    var incomePerDay = payrollEmpl.IncomePerMonth / getWorkDay;

                    //get OT
                    var overtimeOfEmploye = await _unitOfWork.OvertimeLogDAO.GetOvertimeLogResponsesByEmplIDAndDateAsync
                                                        (request.EmployeeID, (int)OvertimeLogEnum.Status.APPROVE,
                                                        income.StartDate, income.EndDate);
                    int otHour = 0;
                    if (overtimeOfEmploye.Count == 0)
                    {
                        otHour = 0;
                    }
                    else
                    {
                        foreach (var otTime in overtimeOfEmploye)
                        {
                            otHour += otTime.Hours;
                        }
                    }

                    //Luong OT
                    var otSalary = otHour * ((incomePerDay / 8 * (decimal)_bonusOT) + incomePerDay / 8);

                    //GET Ngay nghi phep
                    var listDayoffApproved = await _unitOfWork.LeaveLogDAO.GetLeaveLogResponsesByEmplIDAndDateAsync
                                                    (request.EmployeeID, (int)LeaveLogEnum.Status.APPROVE
                                                    , income.StartDate, income.EndDate);
                    int dayoffApproved = 0;
                    if (listDayoffApproved.Count == 0)
                    {
                        dayoffApproved = 0;
                    }
                    else
                    {
                        dayoffApproved = listDayoffApproved.Count;
                        getWorkDay -= dayoffApproved;
                    }

                    //GET Ngay nghi ko phep
                    var listDayoffWithoutSalary = await _unitOfWork.LeaveLogDAO.GetLeaveLogResponsesByEmplIDAndDateAsync
                                        (request.EmployeeID, (int)LeaveLogEnum.Status.ABSENT_WITHOUT_LEAVE
                                        , income.StartDate, income.EndDate);
                    int dayoff = 0;
                    if (listDayoffWithoutSalary.Count == 0)
                    {
                        dayoff = 0;
                    }
                    else
                    {
                        dayoff = listDayoffApproved.Count;
                        //Lay % phat nghi khong phep
                    }

                    //GET MUC PHAT
                    IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                    var penalize = config["Penalize:ABSENT_WITHOUT_LEAVE"];

                    //GET BONUS
                    var listBonusOfEmpl = await _unitOfWork.BonusDAO.GetListBounusByEmpIDAndDate(request.EmployeeID, income.StartDate);
                    decimal bonusValue = 0;
                    if (listBonusOfEmpl.Count == 0)
                    {
                        bonusValue = 0;
                    }
                    else
                    {
                        foreach (var item in listBonusOfEmpl)
                        {
                            bonusValue += item.BonusValue;

                        }
                    }
                    //LUONG TRUOC THUE
                    var incomeBeforeTax = getWorkDay * incomePerDay + otSalary + bonusValue - (dayoff * incomePerDay) * Convert.ToDecimal(penalize);
                    income.PreviousRentIncome = incomeBeforeTax;

                    //LUONG SAU THUE
                    decimal taxValue = 0;
                    var listTax = await _unitOfWork.TaxDAO.GetAllTax((int)TaxStatusEnum.Status.ACTIVE);
                    if (listTax.Count == 0)
                    {
                        taxValue = 0;
                    }
                    else
                    {
                        foreach (var item in listTax)
                        {
                            taxValue += incomeBeforeTax * Convert.ToDecimal(item.Percent);
                        }
                    }

                    income.AfterRentIncome = incomeBeforeTax - taxValue;
                    await _unitOfWork.IncomeDAO.CreateIncomeAsync(income);
                    await _unitOfWork.CommitAsync();
                }


            }
            catch (Exception ex)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();

                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { ex.Message }
                };

                errors.Add(error);
                if (ex.Message.Contains("at IncomeRequestRepo."))
                {
                    throw new BadRequestException(JsonConvert.SerializeObject(errors));
                }
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<GetIncomeEmployeeResponse>> GetIncomeEmplAsync(int id)
        {
            try
            {
                List<Income> incomes = await this._unitOfWork.IncomeDAO.GetIncomeByEmplIDAsync(id);
                return this._mapper.Map<List<GetIncomeEmployeeResponse>>(incomes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
