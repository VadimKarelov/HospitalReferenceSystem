using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReferenceSystem.Entities
{
    internal class Doctor
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public string Position { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
