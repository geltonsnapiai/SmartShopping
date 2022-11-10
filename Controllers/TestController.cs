using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public TestController(ILogger<TestController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("image.jpeg")]
        public FileResult GetBorat()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes("Resources/borat.jpg");
            _logger.LogTrace("Image has been loaded to memory.");
            return new FileContentResult(fileBytes, "image/jpeg");
        }

        [HttpPost("ocr")]
        public async Task<ActionResult> GetBorat(IFormFile file)
        {
            _logger.LogTrace($"Received file {file.FileName} with size in bytes {file.Length}, type {file.ContentType}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent("TEST"), "api_key");
            content.Add(new StringContent("auto"), "recognizer");
            content.Add(new StreamContent(file.OpenReadStream()), "file"); 

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Host", "ocr.asprise.com");

            var response = await httpClient.PostAsync("https://ocr.asprise.com/api/v1/receipt", content);

            var stream = await response.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            return Ok(reader.ReadToEnd());
        }
    }
}
