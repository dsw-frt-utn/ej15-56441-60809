using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain
{
    public class Doctor : BaseEntity
    {
        public  required String Name { get; set; }
        public String? LicenseNumber { get; set; }
        public bool IsActive { get; set; }
        public Speciality? Speciality { get; set; }  
    }
}
