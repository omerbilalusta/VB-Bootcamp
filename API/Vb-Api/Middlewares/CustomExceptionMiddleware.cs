using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using Vb_Bootcamp.Services;

namespace Vb_Bootcamp.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request]     HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Log(message); //Console.WriteLine(message);
                await _next(context);
                watch.Stop();
                message = "[Response]    HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms";
                _loggerService.Log(message); //Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json"; // geriye dönülecek hata mesajının formatını belirledik.
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;  // geriye dönülecek hata kodunu belirledik.

            string message = "[Error]       HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms"; // geriye dönülecek hata mesajının içeriğini belirledik
            _loggerService.Log(message); //Console.WriteLine(message); //// hata mesajını ekrana yazdık

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Newtonsoft.Json.Formatting.None); // bir result değişkeni oluşturduk ve bu değişkenin Json türünde bir nesne olacağını ve içerisinde bizim validation kısmında hazırladığımız hata mesajını barındıracağını söyledik. barındıracağını söyledik.

            return context.Response.WriteAsync(result); // hata mesajını cilent'ın görmesi için return ettik.
        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}

