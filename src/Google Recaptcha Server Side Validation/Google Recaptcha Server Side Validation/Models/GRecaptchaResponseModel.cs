namespace GoogleRecaptchaServerSideValidation.Models
{
    using Newtonsoft.Json;
    using System;

    public class GRecaptchaResponseModel
    {
        public bool Success { get; set; }


        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTimestamp { get; set; }


        public string Hostname { get; set; }


        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
