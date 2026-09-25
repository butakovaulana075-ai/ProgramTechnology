using System.Collections.Generic;

namespace Hospital
{
    public class InMemoryRepository
    {
        private List<Department> _departments;
        private List<Doctor> _doctors;
        private List<Patient> _patients;

        public InMemoryRepository()
        {
            _departments = new List<Department>
            {
                new Department(1, "Терапия", "Сидоров С.С."),
                new Department(2, "Хирургия", "Орлов О.О.")
            };

            _doctors = new List<Doctor>
            {
                new Doctor(1, "Сидоров С.С.", 1, "Терапевт"),
                new Doctor(2, "Орлов О.О.", 2, "Хирург"),
                new Doctor(3, "Кузнецова А.А.", 1, "Терапевт")
            };

            _patients = new List<Patient>
            {
                new Patient(1, "Петров П.П.", 1, "грипп", 58),
                new Patient(2, "Иванов И.И.", 1, "ангина", 34),
                new Patient(3, "Смирнов А.А.", 2, "бронхит", 72)
            };
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
    }
}