using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            using (EmployeeDBEntities db = new EmployeeDBEntities())
            {
                return db.Employees.ToList();
            }
        }
        public Employee Get(int id)
        {
            using (EmployeeDBEntities db = new EmployeeDBEntities())
            {
                return db.Employees.FirstOrDefault(n => n.ID == id);
            }
        }
    }
}
