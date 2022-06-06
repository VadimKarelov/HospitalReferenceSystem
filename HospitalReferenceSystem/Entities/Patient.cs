using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReferenceSystem.Entities
{
    internal class Patient
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public int DiagnosisID { get; set; }
        public int WardID { get; set; }
        public int DoctorID { get; set; }
        public string Status { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
