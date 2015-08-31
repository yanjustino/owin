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
        ///The primary interface in OWIN is called the application delegate or AppFunc. 
        /// An application delegate takes the IDictionary<string, object> environment and returns a Task 
        /// when it has finished processing.
        private AppFunc _next;

        public Midware(AppFunc next)
        {
            _next = Middleware();
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            await _next.Invoke(environment);
        }

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
