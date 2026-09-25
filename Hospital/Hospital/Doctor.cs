using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    public class Doctor
    {
        public int Id {  get; set; }
        public string FullName { get; set; }
        public int DepartmentId { get; set; }
        public string Specialty {  get; set; }

        public Doctor(int id, string fullName, int departmentId, string specialty)
        {
            if (fullName == null || fullName == "")
                throw new ArgumentException("ФИО врача не может быть пустым");

            if (specialty == null || specialty == "")
                throw new ArgumentException("Специальность не может быть пустой");

            Id = id;
            FullName = fullName;
            DepartmentId = departmentId;
            Specialty = specialty;
        }
        public bool IsSurgeon
        {
            get { return Specialty == "Хирург"; }
        }
        public string GetInfo()
        {
            return $"{FullName} ({Specialty.ToLower()})";
        }
    }
}
