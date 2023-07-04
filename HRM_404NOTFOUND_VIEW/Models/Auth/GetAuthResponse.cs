using GroupProject_HRM_Library.DTOs.Authenticate;

namespace GroupProject_HRM_View.Models.Auth
{
    public class GetAuthResponse
    {
        public bool Success { get; set; }
        public AuthenResponse Data { get; set; }
    }
}
