using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Routine.Api.Data;
using Routine.Api.DtoParameters;
using Routine.Api.Entities;

namespace Routine.Api.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;

        public CompanyRepository(RoutineDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<IEnumerable<Company>> GetCompaniesAsync(CompanyDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentException(nameof(parameters));
            }
            var companies = _context.Companies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.CompanyName))
            {
                parameters.CompanyName = parameters.CompanyName.Trim();
                companies = companies.Where(c => c.Name.Contains(parameters.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Introduction))
            {
                parameters.Introduction = parameters.Introduction.Trim();
                companies = companies.Where(c => c.Introduction.Contains(parameters.Introduction));
            }

            return await companies.ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentException(nameof(companyId));
            }

            return await _context.Companies
                .FirstOrDefaultAsync(c => c.Id == companyId);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentException(nameof(companyIds));
            }

            return await _context.Companies
                .Where(c => companyIds.Contains(c.Id))
                .OrderBy(c=>c.Name)
                .ToListAsync();
        }

        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentException(nameof(company));
            }

            company.Id = Guid.NewGuid();
            if (company.Employees.Count > 0)
            {
                foreach (var employee in company.Employees)
                {
                    employee.Id = Guid.NewGuid();
                }
            }
           
            _context.Companies.Add(company);
        }

        public void UpdateCompany(Company company)
        {

        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentException(nameof(company));
            }

            _context.Companies.Remove(company);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentException(nameof(companyId));
            }

            return await _context.Companies
                .AnyAsync(c => c.Id == companyId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string sex, string q)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentException(nameof(companyId));
            }

            var employees = _context.Employees.Where(e => e.CompanyId == companyId);

            if (!string.IsNullOrWhiteSpace(sex))
            {
                sex = sex.Trim();
                var gender = Enum.Parse<Gender>(sex);
                employees = employees.Where(e => e.Gender == gender);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                employees = employees.Where(e => e.FirstName.Contains(q) || e.LastName.Contains(q) || e.EmployeeNo.Contains(q));
            }

            return await employees.OrderBy(e=>e.EmployeeNo).ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentException(nameof(companyId));
            }
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentException(nameof(employeeId));
            }

            return await _context.Employees
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Id == employeeId);
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentException(nameof(companyId));
            }
            if (employee == null)
            {
                throw new ArgumentException(nameof(employee));
            }

            employee.Id = Guid.NewGuid();
            employee.CompanyId = companyId;
            _context.Employees.Add(employee);
        }
        public void UpdateEmployee(Employee employee)
        {

        }
        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentException(nameof(employee));
            }

            _context.Employees.Remove(employee);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
