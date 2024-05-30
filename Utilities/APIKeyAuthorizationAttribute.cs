using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIQuiz.Utilities
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class APIKeyAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private const string API_KEY_NAME = "X-QUIZ-API-KEY";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_NAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API Key is missing"
                };
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var apiKey = appSettings!.GetValue<string>(API_KEY_NAME);

            if (!apiKey!.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Invalid API Key"
                };
                return;
            }
        }
    }
}
