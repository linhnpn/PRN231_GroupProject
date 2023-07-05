using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_Library.DTOs.Tax;
using GroupProject_HRM_Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GroupProject_HRM_View.Controllers
{
    public class TaxController : Controller
    {
        private readonly HttpClient client = null;
        private readonly string TaxAPIUrl = "";
        public TaxController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            TaxAPIUrl = "https://localhost:5000/api/Tax";
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateTax()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTax(CreateTaxRequest request)
        {
            if (ModelState.IsValid)
            {
                //var formData = new MultipartFormDataContent();
                //formData.Add(new StringContent(request.SalaryMin.ToString()), "SalaryMin");
                //formData.Add(new StringContent(request.SalaryMax.ToString()), "SalaryMax");
                //formData.Add(new StringContent(request.Percent.ToString()), "Percent");


                //HttpResponseMessage response = await client.PostAsync(TaxAPIUrl, formData);

                string strData = System.Text.Json.JsonSerializer.Serialize(request);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(TaxAPIUrl, contentData);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Insert successfully!";
                }
                else
                {
                    TempData["Message"] = "Error while calling WebAPI!";
                }
            }
            return Redirect("./Index");
        }

    }
}
