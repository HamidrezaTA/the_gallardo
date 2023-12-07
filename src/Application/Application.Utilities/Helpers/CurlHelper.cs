using System.Net.Http;

namespace Application.Utilities.Helpers
{
    public static class CurlHelper
    {
        public static string GetCurlCommand(HttpRequestMessage request)
        {
            string curlCommand = $"curl -X {request.Method.ToString().ToUpper()} {request.RequestUri}";

            foreach (var header in request.Headers)
            {
                curlCommand += $" -H '{header.Key}: {string.Join(", ", header.Value)}'";
            }

            if (request.Content != null)
            {
                string? contentType = request.Content.Headers.ContentType?.ToString();
                if (!string.IsNullOrEmpty(contentType))
                {
                    curlCommand += $" -H 'Content-Type: {contentType}'";
                }

                string content = request.Content.ReadAsStringAsync().Result;
                curlCommand += $" -d '{content}'";
            }

            return curlCommand;
        }
    }
}