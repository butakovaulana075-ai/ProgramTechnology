using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Head { get; set; }

        public Department(int id, string name, string head)
        {
            if (name == null || name == "")
                throw new ArgumentException("Название отделения не может быть пустым");

            if (head == null || head == "")
                throw new ArgumentException("Заведующий не может быть пустым");

            Id = id;
            Name = name;
            Head = head;
        }
        public string GetInfo()
        {
            return $"{Name} (зав.: {Head})";
        }
    }
}
