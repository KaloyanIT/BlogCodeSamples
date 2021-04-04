namespace GoogleRecaptchaServerSideValidation.Services
{
    using Models;
    using System.Threading.Tasks;

    public interface IGRecaptchaService
    {
        Task<bool> Validate(GRecaptchaRequestModel requestModel);
    }
}
