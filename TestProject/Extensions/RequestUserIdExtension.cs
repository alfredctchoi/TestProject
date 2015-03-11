using System;
using System.Linq;
using System.Net.Http;

namespace TestProject.Extensions
{
    public static class RequestUserIdExtension
    {

        public static int GetUserId(this HttpRequestMessage request)
        {
            var test = request.Headers.GetValues("tp-userId");
            return test == null ? 0 : Convert.ToInt32(test.First());
        }

    }
}