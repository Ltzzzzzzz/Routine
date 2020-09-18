using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Entities;
using Routine.Api.Models;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    [Route("api/companies/{companyId}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public EmployeesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? 
                                 throw new ArgumentException(nameof(companyRepository));
            _mapper = mapper ?? 
                      throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompanyAsync(Guid companyId, [FromQuery]string sex, string q)
        {
            if (! await  _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employees = await _companyRepository.GetEmployeesAsync(companyId, sex, q);

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }
        [HttpGet("{employeeId}"), ActionName(nameof(GetEmployeeAsync))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeAsync(Guid companyId, EmployeeAddDto employee)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var entity = _mapper.Map<Employee>(employee);

            _companyRepository.AddEmployee(companyId, entity);
            await _companyRepository.SaveAsync();

            var returnDto = _mapper.Map<EmployeeDto>(entity);
            return CreatedAtAction(nameof(GetEmployeeAsync), new
                {
                    companyId,
                    employeeId = returnDto.Id
                }, returnDto);
        }

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeAsync(Guid companyId, Guid employeeId, EmployeeUpdateDto employee)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var entity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);

            if (entity == null)
            {
                var employeeToAddEntity = _mapper.Map<Employee>(employee);
                employeeToAddEntity.Id = employeeId;
                _companyRepository.AddEmployee(companyId, employeeToAddEntity);
                await _companyRepository.SaveAsync();
                var returnDto = _mapper.Map<EmployeeDto>(employeeToAddEntity);
                return CreatedAtAction(nameof(GetEmployeeAsync), new { companyId, employeeId }, returnDto);
            }

            var employeeEntity = _mapper.Map(employee, entity);

            _companyRepository.UpdateEmployee(employeeEntity);
            await _companyRepository.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompanyAsync(
            Guid companyId,
            Guid employeeId,
            JsonPatchDocument<EmployeeUpdateDto> pathDocument)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                var employeeDto = new EmployeeUpdateDto();
                pathDocument.ApplyTo(employeeDto, ModelState);
                if (!TryValidateModel(employeeDto))
                {
                    return ValidationProblem(ModelState);
                }

                var employeeToAdd = _mapper.Map<Employee>(employeeDto);
                employeeToAdd.Id = employeeId;
                _companyRepository.AddEmployee(companyId, employeeToAdd);
                await _companyRepository.SaveAsync();

                var returnDto = _mapper.Map<EmployeeDto>(employeeToAdd);
                return CreatedAtAction(nameof(GetEmployeeAsync), new { companyId, employeeId }, returnDto);
            }

            var dtoToPatch = _mapper.Map<EmployeeUpdateDto>(employeeEntity);
            // 需要处理验证错误
            pathDocument.ApplyTo(dtoToPatch, ModelState);

            if (!TryValidateModel(dtoToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(dtoToPatch, employeeEntity);
            _companyRepository.UpdateEmployee(employeeEntity);
            await _companyRepository.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompanyAsync(Guid companyId, Guid employeeId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                return NotFound();
            }

            _companyRepository.DeleteEmployee(employeeEntity);
            await _companyRepository.SaveAsync();
            return NoContent();
        }
    }
}
