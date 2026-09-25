using System;
using System.Collections.Generic;
using System.IO;

namespace Hospital
{
    public class CsvRepository
    {
        private string _basePath;

        public CsvRepository(string basePath)
        {
            _basePath = basePath;
        }

        public List<Department> GetDepartments()
        {
            List<Department> result = new List<Department>();
            string path = _basePath + "/departments.csv";
            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 3) continue;

                int id = int.Parse(parts[0]);
                string name = parts[1];
                string head = parts[2];
                result.Add(new Department(id, name, head));
            }
            return result;
        }

        public List<Doctor> GetDoctors()
        {
            List<Doctor> result = new List<Doctor>();
            string path = _basePath + "/doctors.csv";
            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 4) continue;

                int id = int.Parse(parts[0]);
                string fullName = parts[1];
                int departmentId = int.Parse(parts[2]);
                string specialty = parts[3];
                result.Add(new Doctor(id, fullName, departmentId, specialty));
            }
            return result;
        }

        public List<Patient> GetPatients()
        {
            List<Patient> result = new List<Patient>();
            string path = _basePath + "/patients.csv";
            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 5) continue;

                int id = int.Parse(parts[0]);
                string fullName = parts[1];
                int doctorId = int.Parse(parts[2]);
                string diagnosis = parts[3];
                int age = int.Parse(parts[4]);
                result.Add(new Patient(id, fullName, doctorId, diagnosis, age));
            }
            return result;
        }
    }
}