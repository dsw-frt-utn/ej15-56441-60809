using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain
{
    public class Speciality : BaseEntity
    {
        public required String Name { get; set; }
        public String? Description { get; set; }
    }
}
