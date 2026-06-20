using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel.DataAnnotations;
using Dsw2026Ej15.Domain.Exceptions;

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
           throw new ValidationException("Nombre y matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null)
        {
            throw new ValidationException("Especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);
        Console.WriteLine($"Médico creado con Id: {doctor.Id}");
        return Created();
    }

    [HttpGet]
    public IActionResult GetActiveDoctors()
    {
        var doctors = _persistence.GetActiveDoctors();
        var response = doctors.Select(d => new DoctorModel.Response(
            d.Name,
            d.LicenseNumber,
            d.Speciality?.Name
            ));
        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetDoctorById(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if(doctor == null)
            return NotFound("Medico no encontrado");
        var response = new DoctorModel.Response(
            doctor.Name,
            doctor.LicenseNumber,
            doctor?.Speciality?.Name
            );

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor == null)
            return NotFound("Medico no encontrado");

        doctor.IsActive = false;
        _persistence.SaveDoctor(doctor);
 
        return NoContent();
    }
} 