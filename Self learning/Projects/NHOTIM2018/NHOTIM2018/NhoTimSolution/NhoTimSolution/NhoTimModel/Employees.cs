using System;
using System.Collections.Generic;

namespace NhoTimSolution.NhoTimModel
{
    public partial class Employees
    {
        public Employees()
        {
            Emp_Dep = new HashSet<Emp_Dep>();
            Employee_Group = new HashSet<Employee_Group>();
            InverseManager = new HashSet<Employees>();
            PostsNTP = new HashSet<PostsNTP>();
            ProductsNTP = new HashSet<ProductsNTP>();
        }

        public int EmpID { get; set; }
        public int FolderID { get; set; }
        public int? ManagerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Address { get; set; }
        public string Distric { get; set; }
        public string Ward { get; set; }
        public string Province { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int TrangThai { get; set; }

        public Folders Folder { get; set; }
        public Employees Manager { get; set; }
        public ICollection<Emp_Dep> Emp_Dep { get; set; }
        public ICollection<Employee_Group> Employee_Group { get; set; }
        public ICollection<Employees> InverseManager { get; set; }
        public ICollection<PostsNTP> PostsNTP { get; set; }
        public ICollection<ProductsNTP> ProductsNTP { get; set; }
    }
}
