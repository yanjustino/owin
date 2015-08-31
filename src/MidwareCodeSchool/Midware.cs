using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MidwareCodeSchool
{
    using System.IO;
    using System.Text;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Midware
    {
        private AppFunc _next;

        public Midware(AppFunc next)
        {
            _next = Middleware();
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            await _next.Invoke(environment);
        }

        /// <summary>
        /// The Environment dictionary stores information about the request, the response, 
        /// and any relevant server state.  The server is responsible for providing body 
        /// streams and header collections for both the request and response in the initial call.  
        /// The application then populates the appropriate fields with response data, 
        /// writes the response body, and returns when done.
        /// </summary>
        /// <returns></returns>
        public AppFunc Middleware()
        {
            AppFunc result =
                (IDictionary<string, object> e) =>
                {

                    var response = (Stream)e["owin.ResponseBody"];
                    const string message = "Hello World from Owin";
                    var b = Encoding.UTF8.GetBytes(message);

                    var headers = (IDictionary<string, string[]>)e["owin.ResponseHeaders"];
                    headers["Content-Length"] = new[] { b.Length.ToString() };
                    headers["Content-Type"] = new[] { "text/html" };

                    return response.WriteAsync(b, 0, b.Length);
                };

            return result;
        }
    }
}
