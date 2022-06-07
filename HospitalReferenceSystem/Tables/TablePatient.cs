using HospitalReferenceSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReferenceSystem.Tables
{
    internal class TablePatient
    {
        public List<Patient> GetAllPatients()
        {
            Task<DataSet> t = Request.RequestAsync("SELECT * FROM Sick");
            t.Wait();
            DataSet ds = t.Result;

            List<Patient> res = new List<Patient>();

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    object[] arr = row.ItemArray;

                    Patient patient = new Patient()
                    {
                        ID = (int)row[0],
                        FIO = (string)row[1],
                        DiagnosisID = (int)row[2],
                        WardID = (int)row[3],
                        DoctorID = (int)row[4],
                        Status = (string)row[5],
                        Login = (string)row[6],
                        Password = (string)row[7],
                    };
                }
            }

            return res;
        }
    }
}
