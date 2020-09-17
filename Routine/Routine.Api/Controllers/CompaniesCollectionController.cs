using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Entities;
using Routine.Api.Models;
using Routine.Api.Models.Helpers;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesCollectionController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompaniesCollectionController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? 
                                 throw new ArgumentException(nameof(companyRepository));
            _mapper = mapper ?? 
                      throw new ArgumentException(nameof(mapper));
        }
        // 1,2,3,4
        // key1=value1,key2=value2,key3=value3
        [HttpGet("({ids})"), ActionName(nameof(GetCompaniesCollectionAsync))]
        public async Task<IActionResult> GetCompaniesCollectionAsync(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var entities = await _companyRepository.GetCompaniesAsync(ids);

            if (entities.Count() != ids.Count())
            {
                return NotFound();
            }

            var returnDtos = _mapper.Map<IEnumerable<CompanyDto>>(entities);
            return Ok(returnDtos);
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompaniesAsync(IEnumerable<CompanyAddDto> companies)
        {
            var entities = _mapper.Map<IEnumerable<Company>>(companies);

            foreach (var company in entities)
            {
                _companyRepository.AddCompany(company);
            }

            await _companyRepository.SaveAsync();

            var dtos = _mapper.Map<IEnumerable<CompanyDto>>(entities);
            var idsString = string.Join(",", dtos.Select(d => d.Id));

            return CreatedAtAction(nameof(GetCompaniesCollectionAsync), new { ids = idsString }, dtos);
        }
    }
}
