using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }

        public void SaveDoctor(Doctor doctor)
        {
            var existing = _context.Doctors.Find(doctor.Id);
            if (existing == null)
                _context.Doctors.Add(doctor);
            else
                _context.Entry(existing).CurrentValues.SetValues(doctor);

            _context.SaveChanges();
        }

        public List<Doctor> GetActiveDoctors()
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.IsActive)
                .ToList();
        }

        public Doctor? GetActiveDoctorById(Guid id)
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .SingleOrDefault(d => d.Id == id && d.IsActive);
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _context.Specialities
                .SingleOrDefault(s => s.Id == id);
        }
    }
}