using EmployeeRequestTrackerAPI.Exceptions;
using EmployeeRequestTrackerAPI.Interfaces;
using EmployeeRequestTrackerAPI.Models;
using EmployeeRequestTrackerAPI.Models.DTOs;
using EmployeeRequestTrackerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeRequestTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAdminRequests")]
        [ProducesResponseType(typeof(IList<Request>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<IList<Request>>> GetAdminRequests()
        {
            try
            {
                var requests = await _requestService.GetAdminRequests();
                return Ok(requests.ToList());
            }
            catch (NoRequestsFoundException ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }


        [Authorize(Roles = "Admin,User")]
        [HttpGet("GetUserRequests")]
        [ProducesResponseType(typeof(IList<Request>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<IList<Request>>> GetUserRequests()
        {
            try
            {
                var employeeClaimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var employeeId = Convert.ToInt32(employeeClaimId);
                var requests = await _requestService.GetUserRequests(employeeId);
                return Ok(requests.ToList());
            }
            catch (NoRequestsFoundException ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ProducesResponseType(typeof(IList<AddRequestDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ErrorModel))]

        public async Task<ActionResult<Employee>> Get(AddRequestDTO requestDTO)
        {
            try
            {
                var request = await _requestService.AddRequest(requestDTO);
                return Ok(request);
            }
            catch (UnableToAddRequestException ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
    }
}
