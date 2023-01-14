using System;

namespace Business.Common
{
    public class ApiUrl : IApiUrl
    {
        public Uri Url { get; set; }

        public ApiUrl()
        {
            Url = new Uri("https://recruiting-api.newshore.es/api/flights/2");
        }

    }
}
