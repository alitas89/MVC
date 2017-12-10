using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace UtilityLayer.Filters
{
    public class ApiResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            return BuildApiResponse(request, response);
        }

        private static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            object content = null;
            string errorMessage = string.Empty;
            string clientErrorMessage = string.Empty;

            ValidateResponse(response, ref content, ref errorMessage, ref clientErrorMessage);

            // Yeni response'u custom olarak oluşturmuş olduğumuz wrapper sınıf ile baştan oluşturuyoruz.
            var newResponse = CreateHttpResponseMessage(request, response, content, errorMessage, clientErrorMessage);

            // Header key'lerini baştan set et.
            foreach (var loopHeader in response.Headers)
            {
                newResponse.Headers.Add(loopHeader.Key, loopHeader.Value);
            }

            return newResponse;
        }

        private static HttpResponseMessage CreateHttpResponseMessage<T>(HttpRequestMessage request, HttpResponseMessage response, T content, string errorMessage, string clientErrorMessage)
        {
            return request.CreateResponse(response.StatusCode, new ApiResponse<T>(response.IsSuccessStatusCode, response.StatusCode, content, errorMessage, clientErrorMessage));
        }

        private static void ValidateResponse(HttpResponseMessage response, ref object content,
            ref string errorMessage, ref string clientErrorMessage)
        {
            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                HttpError error = content as HttpError;

                if (error != null)
                {
                    content = null;
                    StringBuilder sb = new StringBuilder();

                    foreach (var loopError in error)
                    {
                        if (loopError.Key == "ExceptionMessage")
                        {
                            clientErrorMessage = loopError.Value + "";
                        }
                        sb.Append(string.Format("{0}: {1} ", loopError.Key, loopError.Value));
                    }

                    errorMessage = sb.ToString();
                }
            }
        }
    }
}
