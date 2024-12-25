using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatientsList.Application.Patients;
using PatientsList.Application.Patients.Create;
using PatientsList.Application.Patients.Edit;
using PatientsList.Application.Patients.GetByBirthDate;
using PatientsList.Application.Patients.GetById;
using PatientsList.Application.Patients.Remove;
using System.Web;
using System;

namespace PatientsList.Api.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly ISender _sender;

        public PatientsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("get")]
        public async Task<ActionResult<PatientInfoDto>> GetById(
            Guid id,
            CancellationToken token)
        {
            var result = await _sender.Send(
                new GetPatientByIdQuery(id),
                token);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpGet("getByBirthDate")]
        public async Task<ActionResult<PatientInfoDto>> GetById(
            [FromQuery] string[] date,
            CancellationToken token)
        {
            var result = await _sender.Send(
                new GetPatientsByBirthDateQuery(date),
                token);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Guid>> AddNew(
           [FromBody] NewPersonDto personInfo,
           CancellationToken token)
        {
            var result = await _sender.Send(
                new CreatePatientCommand(personInfo),
                token);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(
           [FromBody] EditPersonDto personInfo,
           CancellationToken token)
        {
            var result = await _sender.Send(
                new EditPatientCommand(personInfo),
                token);

            return result.IsSuccess
                ? Ok()
                : BadRequest(result.Error);
        }

        [HttpDelete("remove")]
        public async Task<ActionResult> Remove(
           Guid id,
           CancellationToken token)
        {
            var result = await _sender.Send(
                new RemovePatientCommand(id),
                token);

            return result.IsSuccess
                ? Ok()
                : BadRequest(result.Error);
        }
    }
}
