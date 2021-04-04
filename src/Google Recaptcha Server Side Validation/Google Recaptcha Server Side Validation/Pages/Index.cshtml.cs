namespace GoogleRecaptchaServerSideValidation.Pages
{
    using GoogleRecaptchaServerSideValidation.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Options;
    using Services;
    using System.Threading.Tasks;

    public class IndexModel : PageModel
    {
        private readonly IGRecaptchaService _gRecaptchaService;
        private readonly GoogleRecaptchaConfiguration _googleRecaptchaConfiguration;

        public IndexModel(
            IOptions<GoogleRecaptchaConfiguration> options,
            IGRecaptchaService gRecaptchaService)
        {
            _gRecaptchaService = gRecaptchaService;
            _googleRecaptchaConfiguration = options.Value;
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
            SiteKey = _googleRecaptchaConfiguration.SiteKey;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var requestModel = new GRecaptchaRequestModel();
            requestModel.Token = Token;
            requestModel.RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString();

            var result = await _gRecaptchaService.Validate(requestModel);

            if(!result)
            {
                Message = "Captcha Verification Successful";
            }
            else
            {
                Message = "Captcha Verification Error";
            }


            return Page();
        }
    }
}
