using ClinicManagementAPI.Exceptions;
using ClinicManagementAPI.Interfaces;
using ClinicManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Doctor>>> Get()
        {
            try
            {
                var doctors = await _doctorService.GetDoctors();
                return Ok(doctors.ToList());
            }
            catch (NoDoctorsFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IList<Doctor>>> AddDoctor([FromBody]Doctor doctor)
        {
            try
            {
                var result = await _doctorService.AddDoctor(doctor);
                return Ok(result);
            }
            catch (NoSuchDoctorException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Doctor>> UpdateExperience(int id, [FromBody]int experience)
        {
            try
            {
                var doctor = await _doctorService.UpdateDoctorExperience(id, experience);
                return Ok(doctor);
            }
            catch (NoSuchDoctorException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDoctorBySpeciality")]
        public async Task<ActionResult<IList<Doctor>>> GetBySpeciality(string speciality)
        {
            try
            {
                var doctors = await _doctorService.GetDoctorBySpeciality(speciality);
                return Ok(doctors);
            }
            catch (NoDoctorsFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
