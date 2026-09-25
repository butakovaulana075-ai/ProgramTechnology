namespace Hospital;

internal class HospitalRepository
{
    private List<Department> _departments = new List<Department>();
    private List<Doctor> _doctors = new List<Doctor>();
    private List<Patient> _patients = new List<Patient>();

    public HospitalRepository(string folderPath)
    {
        LoadDepartments(folderPath + "/departments.csv");
        LoadDoctors(folderPath + "/doctors.csv");
        LoadPatients(folderPath + "/patients.csv");
    }

    private void LoadDepartments(string path)
    {
        string[] lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            string[] parts = line.Split(',');
            int id = int.Parse(parts[0]);
            string name = parts[1];
            string head = parts[2];
            _departments.Add(new Department(id, name, head));
        }
    }

    private void LoadDoctors(string path)
    {
        string[] lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            string[] parts = line.Split(',');
            int id = int.Parse(parts[0]);
            string fullName = parts[1];
            int departmentId = int.Parse(parts[2]);
            string specialty = parts[3];
            _doctors.Add(new Doctor(id, fullName, departmentId, specialty));
        }
    }

    private void LoadPatients(string path)
    {
        string[] lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            string[] parts = line.Split(',');
            int id = int.Parse(parts[0]);
            string fullName = parts[1];
            int doctorId = int.Parse(parts[2]);
            string diagnosis = parts[3];
            int age = int.Parse(parts[4]);
            _patients.Add(new Patient(id, fullName, doctorId, diagnosis, age));
        }
    }

    public List<Department> GetDepartments()
    {
        return _departments;
    }

    public List<Doctor> GetDoctors()
    {
        return _doctors;
    }

    public List<Patient> GetPatients()
    {
        return _patients;
    }

    public Doctor FindDoctor(string patientName)
    {
        foreach (var patient in _patients)
        {
            if (patient.FullName == patientName)
            {
                foreach (var doctor in _doctors)
                {
                    if (doctor.Id == patient.DoctorId)
                    {
                        return doctor;
                    }
                }
            }
        }
        return null;
    }

    public Department FindDepartment(string doctorName)
    {
        foreach (var doctor in _doctors)
        {
            if (doctor.FullName == doctorName)
            {
                foreach (var dept in _departments)
                {
                    if (dept.Id == doctor.DepartmentId)
                    {
                        return dept;
                    }
                }
            }
        }
        return null;
    }

    public double GetAverageAge()
    {
        if (_patients.Count == 0)
        {
            return 0;
        }

        int sum = 0;
        foreach (var patient in _patients)
        {
            sum = sum + patient.Age;
        }

        return (double)sum / _patients.Count;
    }

    public Dictionary<string, int> CountPatientsByDiagnosis()
    {
        var result = new Dictionary<string, int>();

        foreach (var patient in _patients)
        {
            if (result.ContainsKey(patient.Diagnosis))
            {
                result[patient.Diagnosis] = result[patient.Diagnosis] + 1;
            }
            else
            {
                result[patient.Diagnosis] = 1;
            }
        }

        return result;
    }

    public void PrintAllPatients()
    {
        foreach (var patient in _patients)
        {
            Doctor doctor = null;
            foreach (var d in _doctors)
            {
                if (d.Id == patient.DoctorId)
                {
                    doctor = d;
                }
            }

            Department dept = null;
            if (doctor != null)
            {
                foreach (var d in _departments)
                {
                    if (d.Id == doctor.DepartmentId)
                    {
                        dept = d;
                    }
                }
            }

            string doctorInfo = "—";
            if (doctor != null)
            {
                doctorInfo = doctor.GetInfo();
            }

            string deptName = "—";
            if (dept != null)
            {
                deptName = dept.Name;
            }

            Console.WriteLine("\"" + patient.GetInfo() + "\" — врач " + doctorInfo + ", отделение \"" + deptName + "\"");
        }
    }
}