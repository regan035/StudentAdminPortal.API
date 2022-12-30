using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Entities;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository studentRepository,IMapper mapper) 
        { 
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        //Get all student records
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await studentRepository.GetStudentsAsync();

            return Ok(mapper.Map<List<Student>>(students));
        }

        //Get a single student record by ID
        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            //Fetch student details
            var student = await studentRepository.GetStudentAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Student>(student));
        }


        //Update student
        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            if(await studentRepository.Exists(studentId)){


                var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<Models.Student>(request));

                if(updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }


                return NotFound();
            
        }

        //Delete student
        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await studentRepository.Exists(studentId))
            {
                var studentDelete = await studentRepository.DeleteStudent(studentId);
                return Ok(mapper.Map<Student>(studentDelete));
            }
            return NotFound();
        }

        //create student
        [HttpPost]
        [Route("[controller]/create")]
        public async Task<IActionResult> CreateStudetnAsync([FromBody] CreateStudentRequest request)
        {
            var student = await studentRepository.CreateStudent(mapper.Map<Models.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), 
                new {studentId = student.Id},
                mapper.Map<Student>(student));
        }
      
    }
}
