using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.DtoParameters;
using Routine.Api.Entities;
using Routine.Api.Models;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? 
                                 throw new ArgumentException(nameof(companyRepository));
            _mapper = mapper ?? 
                      throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompaniesAsync([FromQuery]CompanyDtoParameters parameters)
        {
            var companies = await _companyRepository.GetCompaniesAsync(parameters);
            // 映射 <> 里面是要输出的类型，()里面是原类型
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return Ok(companyDtos);
        }

        [HttpGet("{companyId}"), ActionName(nameof(GetCompany))] // api/Companies/{companyId}
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyId)
        {
            // 并发量大的时候有可能会出错
            //var exist = await _companyRepository.CompanyExistsAsync(companyId);
            //if (!exist)
            //{
            //    return NotFound();
            //}
            var company = await _companyRepository.GetCompanyAsync(companyId);
            // 这样强一点
            if (company == null)
            {
                return NotFound();
            }
            
            var companyDto = _mapper.Map<CompanyDto>(company);

            return Ok(companyDto);
        }
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompanyAsync([FromBody] CompanyAddDto company)
        {
            // post 需要返回201状态码
            var entity = _mapper.Map<Company>(company);
            _companyRepository.AddCompany(entity);
            await _companyRepository.SaveAsync();
            var returnDto = _mapper.Map<CompanyDto>(entity);
            return CreatedAtAction(nameof(GetCompany), new { companyId = returnDto.Id }, returnDto);
        }
    }
}
