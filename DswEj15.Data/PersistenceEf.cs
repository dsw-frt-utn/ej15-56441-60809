using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly AppDbContext _context;

        public PersistenceEf(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Doctor>> GetAllActiveDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.IsActive)
                .ToListAsync();
        }
        public async Task<Doctor> GetDoctorByIdAsync(Guid id)
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive)!;
        }
        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task InactivateDoctorAsync(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                doctor.IsActive = false;
                _context.Doctors.Update(doctor);
                await _context.SaveChangesAsync();
            }
        }
        public Speciality? GetSpecialityById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public List<Doctor> GetActiveDoctors()
        {
            throw new NotImplementedException();
        }

        public Doctor? GetActiveDoctorById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}