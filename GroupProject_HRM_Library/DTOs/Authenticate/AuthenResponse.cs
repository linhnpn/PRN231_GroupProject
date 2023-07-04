using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Authenticate
{
    public class AuthenResponse
    {
        public int EmployeeID { get; set; }
        public string? RoleName { get; set; }
        public string? AccessToken { get; set; }
    }
}
