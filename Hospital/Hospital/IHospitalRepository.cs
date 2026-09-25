using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital;

internal interface IHospitalRepository
{
    List<Department> GetDepartments();
    List<Doctor> GetDoctors();
    List<Patient> GetPatients();

    Doctor? FindDoctor(string patientName);
    Department? FindDepartment(string doctorName);
    double GetAverageAge();
    Dictionary<string, int> CountPatientsByDiagnosis();
    void PrintAllPatients();
}