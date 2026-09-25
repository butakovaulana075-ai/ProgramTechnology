using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int DoctorId { get; set; }
        public string Diagnosis { get; set; }
        public int Age { get; set; }

        public Patient(int id, string fullName, int doctorId, string diagnosis, int age)
        {
            if (fullName == null || fullName == "")
                throw new ArgumentException("ФИО пациента не может быть пустым");

            if (diagnosis == null || diagnosis == "")
                throw new ArgumentException("Диагноз не может быть пустым");

            if (age < 0 || age > 150)
                throw new ArgumentException("Возраст должен быть от 0 до 150");

            Id = id;
            FullName = fullName;
            DoctorId = doctorId;
            Diagnosis = diagnosis;
            Age = age;
        }
        public bool IsElderly
        {
            get { return Age > 60; }
        }
        public string GetInfo()
        {
            return $"{FullName} ({Age} лет, {Diagnosis})";
        }
    }
}
