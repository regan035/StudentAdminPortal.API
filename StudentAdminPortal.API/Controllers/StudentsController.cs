using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Entities;
using StudentAdminPortal.API.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public StudentsController(IStudentRepository studentRepository,IMapper mapper,IImageRepository imageRepository) 
        { 
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
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
            //Fetch student details, pass studentId to the repository
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
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await studentRepository.AddStudent(mapper.Map<Models.Student>(request));

            //return an action name back to  line37 to fetch the student record after creation
            return CreatedAtAction(nameof(GetStudentAsync), 
                new {studentId = student.Id},
                mapper.Map<Student>(student));
        }


        //upload image
        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId,IFormFile profileImage)
        {
            //Check if student exists
            if(await studentRepository.Exists(studentId))
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                var fileImagePath = await imageRepository.Upload(profileImage, fileName);
                if (await studentRepository.UpdateProfileImage(studentId, fileImagePath))
                {
                    return Ok(fileImagePath);
                }
                
                return StatusCode(StatusCodes.Status500InternalServerError,"Error uploading image");
                
                
                //upload image to local storage
                
                //update the profile image path in database
            }


            return NotFound();
        }
        
       
     }
}
