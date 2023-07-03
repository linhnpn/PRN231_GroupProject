using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Authenticate
{
    public class ObjectToken
    {
        public  string EmployeeID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
