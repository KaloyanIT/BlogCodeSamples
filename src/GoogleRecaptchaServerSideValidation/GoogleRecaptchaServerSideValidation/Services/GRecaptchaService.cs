namespace GoogleRecaptchaServerSideValidation.Services
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GRecaptchaService : IGRecaptchaService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GRecaptchaService> _logger;
        private readonly GoogleRecaptchaConfiguration _googleRecaptchaConfiguration;

        public GRecaptchaService(
            HttpClient httpClient,
            ILogger<GRecaptchaService> logger,
            IOptions<GoogleRecaptchaConfiguration> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _googleRecaptchaConfiguration = options.Value;
        }

        public async Task<bool> Validate(GRecaptchaRequestModel requestModel)
        {
            try
            {
                var url = $"{_googleRecaptchaConfiguration.ApiUrl}?secret={_googleRecaptchaConfiguration.SecretKey}&response={requestModel.Token}&remoteIp={requestModel.RemoteIp}";
                var response = await _httpClient.PostAsync(url, null);

                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                var captchaVerification = JsonConvert.DeserializeObject<GRecaptchaResponseModel>(jsonString);

                return captchaVerification.Success;
            }
            catch (HttpRequestException hre)
            {
                //here you can invoke logger - depends on your needs

                return false;
            }
            catch (Exception ex)
            {
                //Invoke logger
                _logger.LogError(ex.Message);

                return false;
            }
        }
    }
}
