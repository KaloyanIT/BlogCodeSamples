namespace GoogleRecaptchaServerSideValidation.Pages
{
    using GoogleRecaptchaServerSideValidation.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Services;
    using System.Threading.Tasks;

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IGRecaptchaService _gRecaptchaService;

        public IndexModel(ILogger<IndexModel> logger,
            IGRecaptchaService gRecaptchaService)
        {
            _logger = logger;
            _gRecaptchaService = gRecaptchaService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Text { get; set; }

        [BindProperty]
        public string Token { get; set; }

        public string SiteKey { get; set; }

        public string Message {get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {

            var requestModel = new GRecaptchaRequestModel();
            requestModel.Token = Token;
            requestModel.RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString();

            var result = await _gRecaptchaService.Validate(requestModel);

            if(result)
            {
                Message = "Captcha Verification Successful";
            }
            else
            {
                Message = "Captcha Verification Error";
            }


            return RedirectToPage("Index");
        }
    }
}
