using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }


    [HttpPost]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            return BadRequest("Nombre y matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null)
        {
            return BadRequest("Especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);
        return Created();
    }

    [HttpGet]
    public IActionResult GetActiveDoctors()
    {
        var activeDoctors = _persistence.GetActiveDoctors();
        return Ok(activeDoctors);
    }

    [HttpGet("{id}")]
    public IActionResult GetDoctorById(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
      
        if (doctor == null)
        {
            return NotFound("El medico no se encuentra/inactivo");
        }
        var response = new
        {
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality!.Name
        };
        return Ok(response);
    }
}