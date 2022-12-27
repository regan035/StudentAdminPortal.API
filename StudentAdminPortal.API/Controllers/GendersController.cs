using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Entities;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        public GendersController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGenders()
        {
            var genderList = await studentRepository.GetGendersAsync();

            //check genderlist null or empty
            if(genderList == null || !genderList.Any())
            {
                return NotFound();
            }

            //using mapper to return genderlist from entity models
            return Ok(mapper.Map<List<Gender>>(genderList));
        }

    }
}
