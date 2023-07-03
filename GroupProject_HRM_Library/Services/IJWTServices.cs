using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Services
{
    public interface IJWTServices
    {
        public string GenerateJWTToken(int employeeID, string username, string role);

        public ObjectToken ExtractToken(string token);
    }
}
