using System;
using System.Collections.Generic;

namespace Hospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите источник данных:");
            Console.WriteLine("1 — InMemoryRepository");
            Console.WriteLine("2 — CsvRepository (папка data)");
            Console.Write("Ваш выбор: ");

            string? input = Console.ReadLine();

            List<Department> departments;
            List<Doctor> doctors;
            List<Patient> patients;

            int choice;
            bool parsed = int.TryParse(input, out choice);

            if (!parsed)
            {
                Console.WriteLine("Неверный выбор");
                return;
            }

            try
            {
                switch (choice)
                {
                    case 1:
                        InMemoryRepository mem = new InMemoryRepository();
                        departments = mem.GetDepartments();
                        doctors = mem.GetDoctors();
                        patients = mem.GetPatients();
                        break;

                    case 2:
                        CsvRepository csv = new CsvRepository("data");
                        departments = csv.GetDepartments();
                        doctors = csv.GetDoctors();
                        patients = csv.GetPatients();
                        break;

                    default:
                        Console.WriteLine("Неверный выбор");
                        return;
                }

                Console.WriteLine("1. FindDoctor(Петров П.П.): " + FindDoctor(patients, doctors, "Петров П.П."));
                Console.WriteLine("2. FindDepartment(Сидоров С.С.): " + FindDepartment(doctors, departments, "Сидоров С.С."));
                Console.WriteLine("3. GetAverageAge: " + GetAverageAge(patients) + " лет");

                Console.Write("4. CountPatientsByDiagnosis: ");
                PrintDiagnosisCounts(patients);

                Console.WriteLine("5. PrintAllPatients:");
                PrintAllPatients(patients, doctors, departments);

                Console.WriteLine();
                Console.WriteLine("Не найдено: FindDoctor(Неизвестный пациент) -> " + FindDoctor(patients, doctors, "Неизвестный пациент"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
                return;
            }
        }

        static string FindDoctor(List<Patient> patients, List<Doctor> doctors, string patientName)
        {
            if (patientName == null)
                throw new ArgumentNullException(nameof(patientName));

            foreach (Patient p in patients)
            {
                if (p.FullName == patientName)
                {
                    foreach (Doctor d in doctors)
                    {
                        if (d.Id == p.DoctorId)
                        {
                            return d.GetInfo();
                        }
                    }
                }
            }
            return "null";
        }

        static string FindDepartment(List<Doctor> doctors, List<Department> departments, string doctorName)
        {
            if (doctorName == null)
                throw new ArgumentNullException(nameof(doctorName));

            foreach (Doctor d in doctors)
            {
                if (d.FullName == doctorName)
                {
                    foreach (Department dep in departments)
                    {
                        if (dep.Id == d.DepartmentId)
                        {
                            return dep.GetInfo();
                        }
                    }
                }
            }
            return "null";
        }

        static double GetAverageAge(List<Patient> patients)
        {
            if (patients.Count == 0)
                return 0;

            int sum = 0;
            foreach (Patient p in patients)
            {
                sum += p.Age;
            }
            return (double)sum / patients.Count;
        }

        static void PrintDiagnosisCounts(List<Patient> patients)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            foreach (Patient p in patients)
            {
                if (counts.ContainsKey(p.Diagnosis))
                {
                    counts[p.Diagnosis]++;
                }
                else
                {
                    counts[p.Diagnosis] = 1;
                }
            }

            bool first = true;
            foreach (var pair in counts)
            {
                if (!first)
                {
                    Console.Write(", ");
                }
                Console.Write(pair.Key + " — " + pair.Value);
                first = false;
            }
            Console.WriteLine();
        }

        static void PrintAllPatients(List<Patient> patients, List<Doctor> doctors, List<Department> departments)
        {
            foreach (Patient p in patients)
            {
                Doctor? doctor = null;
                foreach (Doctor d in doctors)
                {
                    if (d.Id == p.DoctorId)
                    {
                        doctor = d;
                        break;
                    }
                }

                Department? dept = null;
                if (doctor != null)
                {
                    foreach (Department dep in departments)
                    {
                        if (dep.Id == doctor.DepartmentId)
                        {
                            dept = dep;
                            break;
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

                Console.WriteLine("\"" + p.GetInfo() + "\" — врач " + doctorInfo + ", отделение \"" + deptName + "\"");
            }
        }
    }
}